using CabBooking.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CabBooking.DAL
{
    public class DbUtil
    {
        /*
        * Conn = connection string
        */
        SqlConnection Conn = new SqlConnection("Data Source=localhost;Initial Catalog=CabBooking;Integrated Security=True");

        internal int AddVehicle(VehicleModel model)
        {
            int rows = 0;
            try
            {
                string query = "INSERT INTO vehicle (type, mobile, drivername, regno, km, extrahr)" +
                        " VALUES(@type, @mobile, @drivername, @regno, @km, @extrahr)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("type", model.Type));
                cmd.Parameters.Add(new SqlParameter("mobile", model.Mobile));
                cmd.Parameters.Add(new SqlParameter("drivername", model.Drivername));
                cmd.Parameters.Add(new SqlParameter("regno", model.Regno));
                cmd.Parameters.Add(new SqlParameter("km", model.Km));
                cmd.Parameters.Add(new SqlParameter("extrahr", model.Extrahr));

                Conn.Open();

                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {

            }
            finally
            {
                Conn.Close();
            }
            return rows;
        }

        internal List<VehicleModel> GetVehicles()
        {
            DataTable td = new DataTable();
            List<VehicleModel> list = new List<VehicleModel>();
            try
            {
                string sqlquery = "SELECT * FROM vehicle ORDER BY vehicleID DESC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    VehicleModel obj = new VehicleModel();

                    obj.VehicleID = Convert.ToInt32(row["vehicleID"]);
                    obj.Type = Convert.ToString(row["type"]);
                    obj.Drivername = Convert.ToString(row["drivername"]);
                    obj.Mobile = Convert.ToString(row["mobile"]);
                    obj.Regno = Convert.ToString(row["regno"]);
                    obj.Km = Convert.ToInt64(row["km"]);
                    obj.Extrahr = Convert.ToInt64(row["extrahr"]);

                    list.Add(obj);
                }
            }
            catch (Exception)
            { }
            finally
            {
                Conn.Close();
            }
            return list;
        }

        internal VehicleModel GetVehicleByID(int id)
        {
            DataTable td = new DataTable();
            VehicleModel obj = new VehicleModel();
            try
            {
                string sqlquery = "SELECT * FROM vehicle where vehicleID = @vehicleID";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("vehicleID", id));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                Conn.Open();

                adp.Fill(td);

                obj.VehicleID = Convert.ToInt32(td.Rows[0]["vehicleID"]);
                obj.Type = Convert.ToString(td.Rows[0]["type"]);
                obj.Drivername = Convert.ToString(td.Rows[0]["drivername"]);
                obj.Mobile = Convert.ToString(td.Rows[0]["mobile"]);
                obj.Regno = Convert.ToString(td.Rows[0]["regno"]);
                obj.Km = Convert.ToInt64(td.Rows[0]["km"]);
                obj.Extrahr = Convert.ToInt64(td.Rows[0]["extrahr"]);

            }
            catch (Exception)
            { }
            finally
            {
                Conn.Close();
            }
            return obj;

        }

        internal int UpdateVehicle(VehicleModel model)
        {
            int rows = 0;
            try
            {

                string query = "UPDATE vehicle SET type= @type, drivername = @drivername, mobile = @mobile, regno = @regno, km = @km, extrahr = @extrahr where vehicleID = @vehicleID";

                SqlCommand cmd = new SqlCommand(query, Conn);
                cmd.Parameters.Add(new SqlParameter("type", model.Type));
                cmd.Parameters.Add(new SqlParameter("mobile", model.Mobile));
                cmd.Parameters.Add(new SqlParameter("drivername", model.Drivername));
                cmd.Parameters.Add(new SqlParameter("regno", model.Regno));
                cmd.Parameters.Add(new SqlParameter("km", model.Km));
                cmd.Parameters.Add(new SqlParameter("extrahr", model.Extrahr));

                cmd.Parameters.Add(new SqlParameter("vehicleID", model.VehicleID));
                Conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Conn.Close();
            }
            return rows;
        }

        internal bool InsertTrip(TripModel tripModel)
        {
            bool result = false;
            try
            {
                /*
                 * Inserting into `trip` table
                 */
                string query = "INSERT INTO trip (tripID, clientname, mobile, pickuploc, droploc, email, vehicleID, dateofbooking, datein, dateout, pickuptime, droptime, status)" +
                        " VALUES (@tripID, @clientname, @mobile, @pickuploc, @droploc, @email, @vehicleID, @dateofbooking, @datein, @dateout, @pickuptime, @droptime, @status)";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("tripID", tripModel.TripID));
                cmd.Parameters.Add(new SqlParameter("clientname", tripModel.ClientName));
                cmd.Parameters.Add(new SqlParameter("mobile", tripModel.Mobile));
                cmd.Parameters.Add(new SqlParameter("email", tripModel.Email));
                cmd.Parameters.Add(new SqlParameter("pickuploc", tripModel.PickupLoc));
                cmd.Parameters.Add(new SqlParameter("droploc", tripModel.DropLoc));
                cmd.Parameters.Add(new SqlParameter("vehicleID", tripModel.VehicleID));
                cmd.Parameters.Add(new SqlParameter("dateofbooking", tripModel.DateOfBooking));
                cmd.Parameters.Add(new SqlParameter("datein", tripModel.DateIn));
                cmd.Parameters.Add(new SqlParameter("dateout", tripModel.DateOut));
                cmd.Parameters.Add(new SqlParameter("pickuptime", tripModel.PickupTime));
                cmd.Parameters.Add(new SqlParameter("droptime", tripModel.DropTime));
                cmd.Parameters.Add(new SqlParameter("status", tripModel.Status));

                /*
                 * Inserting into `tripdetails` table
                 */
                string query2 = "INSERT INTO tripdetails (tripID) VALUES (@tripID)";
                SqlCommand cmd1 = new SqlCommand(query2, Conn);
                cmd1.Parameters.Add(new SqlParameter("tripID", tripModel.TripID));

                Conn.Open();

                int rows = cmd.ExecuteNonQuery();
                int rows1 = cmd1.ExecuteNonQuery();

                if (rows != 0 && rows1 != 0)
                {
                    result = true;
                }
            }
            catch(Exception ex)
            {
                result = false;
            }
            finally
            {
                Conn.Close();
            }
            return result;
        }

        internal TripModel GetTripByID(int id)
        {
            DataTable td = new DataTable();
            TripModel tripModel = new TripModel();
            try
            {
                string sqlquery = "SELECT * FROM trip where tripID = @tripID";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                cmd.Parameters.Add(new SqlParameter("tripID", id));
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                
                string sqlquery1 = "SELECT * FROM tripdetails where tripID = @tripID";
                SqlCommand cmd1 = new SqlCommand(sqlquery1, Conn);
                cmd.Parameters.Add(new SqlParameter("tripID", id));
                SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

                Conn.Open();

                adp.Fill(td);
                adp1.Fill(td);

                tripModel.TripID = Convert.ToInt32(td.Rows[0]["tripID"]);
                tripModel.ClientName = Convert.ToString(td.Rows[0]["clientname"]);
                tripModel.Mobile = Convert.ToString(td.Rows[0]["mobile"]);
                tripModel.PickupLoc = Convert.ToString(td.Rows[0]["pickuploc"]);
                tripModel.DropLoc = Convert.ToString(td.Rows[0]["droploc"]);
                tripModel.VehicleID = Convert.ToInt32(td.Rows[0]["vehicleID"]);
                tripModel.DateOfBooking = Convert.ToDateTime(td.Rows[0]["dateofbooking"]);
                tripModel.DateIn = Convert.ToDateTime(td.Rows[0]["datein"]);
                tripModel.DateOut = Convert.ToDateTime(td.Rows[0]["dateout"]);
                tripModel.PickupTime = TimeSpan.Parse(Convert.ToString(td.Rows[0]["pickuptime"]));
                tripModel.DropTime = TimeSpan.Parse(Convert.ToString(td.Rows[0]["droptime"]));
                tripModel.Status = Convert.ToInt32(td.Rows[0]["status"]);

                tripModel.ExtraKm = Convert.ToInt64(td.Rows[0]["extrakm"]);
                tripModel.ExtraHr = Convert.ToInt64(td.Rows[0]["extrahr"]);
                tripModel.PkgCharges = Convert.ToInt64(td.Rows[0]["packagecharges"]);
                tripModel.PkgDetails = Convert.ToString(td.Rows[0]["packagedetails"]);
                tripModel.PickupKm = Convert.ToInt64(td.Rows[0]["pickupkm"]);
                tripModel.DropKm = Convert.ToInt64(td.Rows[0]["dropkm"]);
                tripModel.Toll = Convert.ToInt64(td.Rows[0]["toll"]);
                tripModel.NightCharges = Convert.ToInt64(td.Rows[0]["nightcharges"]);
                tripModel.Bata = Convert.ToInt64(td.Rows[0]["bata"]);
                tripModel.IntraTax = Convert.ToInt64(td.Rows[0]["intrastatetax"]);
                tripModel.Cgst = Convert.ToInt64(td.Rows[0]["cgst"]);
                tripModel.Sgst = Convert.ToInt64(td.Rows[0]["sgst"]);

            }
            catch (Exception)
            { }
            finally
            {
                Conn.Close();
            }
            return tripModel;

        }

        internal List<TripModel> GetPendingTrips()
        {
            DataTable td = new DataTable();
            List<TripModel> list = new List<TripModel>();
            try
            {
                string sqlquery = "SELECT * FROM trip WHERE status = 1 ORDER BY datein DESC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    TripModel tripModel = new TripModel();

                    tripModel.TripID = Convert.ToInt32(row["tripID"]);
                    tripModel.ClientName = Convert.ToString(row["clientname"]);
                    tripModel.Mobile = Convert.ToString(row["mobile"]);
                    tripModel.PickupLoc = Convert.ToString(row["pickuploc"]);
                    tripModel.DropLoc = Convert.ToString(row["droploc"]);
                    tripModel.VehicleID = Convert.ToInt32(row["vehicleID"]);
                    tripModel.DateOfBooking = Convert.ToDateTime(row["dateofbooking"]);
                    tripModel.DateIn = Convert.ToDateTime(row["datein"]);
                    tripModel.DateOut = Convert.ToDateTime(row["dateout"]);
                    tripModel.PickupTime = TimeSpan.Parse(Convert.ToString(row["pickuptime"]));
                    tripModel.DropTime = TimeSpan.Parse(Convert.ToString(row["droptime"]));
                    tripModel.Status = Convert.ToInt32(row["status"]);
                    //tripModel.GrandTotal = Convert.ToInt64(row["grandtotal"]);

                    list.Add(tripModel);
                }
            }
            catch (Exception ex)
            { }
            finally
            {
                Conn.Close();
            }
            return list;
        }
        
        internal List<TripModel> GetCompletedTrips()
        {
            DataTable td = new DataTable();
            List<TripModel> list = new List<TripModel>();
            try
            {
                string sqlquery = "SELECT * FROM trip WHERE status = 2 ORDER BY dateout DESC";
                SqlCommand cmd = new SqlCommand(sqlquery, Conn);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                Conn.Open();
                adp.Fill(td);
                foreach (DataRow row in td.Rows)
                {
                    TripModel tripModel = new TripModel();

                    tripModel.TripID = Convert.ToInt32(row["tripID"]);
                    tripModel.ClientName = Convert.ToString(row["clientname"]);
                    tripModel.Mobile = Convert.ToString(row["mobile"]);
                    tripModel.PickupLoc = Convert.ToString(row["pickuploc"]);
                    tripModel.DropLoc = Convert.ToString(row["droploc"]);
                    tripModel.VehicleID = Convert.ToInt32(row["vehicleID"]);
                    tripModel.DateOfBooking = Convert.ToDateTime(row["dateofbooking"]);
                    tripModel.DateIn = Convert.ToDateTime(row["datein"]);
                    tripModel.DateOut = Convert.ToDateTime(row["dateout"]);
                    tripModel.PickupTime = TimeSpan.Parse(Convert.ToString(row["pickuptime"]));
                    tripModel.DropTime = TimeSpan.Parse(Convert.ToString(row["droptime"]));
                    tripModel.Status = Convert.ToInt32(row["status"]);
                    tripModel.GrandTotal = Convert.ToInt64(row["grandtotal"]);

                    list.Add(tripModel);
                }
            }
            catch (Exception)
            { }
            finally
            {
                Conn.Close();
            }
            return list;
        }

    }
}