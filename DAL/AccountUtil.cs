using CabBooking.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CabBooking.DAL
{
    public class AccountUtil
    {
        /*
         * Conn = connection string
         */
        readonly SqlConnection Conn = new SqlConnection($"Data Source=localhost;Initial Catalog={SoftwareConfig.DBName};Integrated Security=True");

        public AccountModel Login(string Username, string Password)
        {
            /*
             * Creating object of modal UserInfo to store return values
             */
            AccountModel returnobj = new AccountModel();

            /*
             * Setting variable `ID` to 0
             * 
             * If user exist then `ID` will be that perticular users `ID` 
             */
            returnobj.ID = 0;
            try
            {

                string query = "SELECT * FROM admin WHERE username=@Username AND password=@Password";
                SqlCommand cmd = new SqlCommand(query, Conn);

                cmd.Parameters.Add(new SqlParameter("Username", Username));
                cmd.Parameters.Add(new SqlParameter("Password", Password));

                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    returnobj.ID = Convert.ToInt32(dt.Rows[0]["id"]);
                    returnobj.Name = Convert.ToString(dt.Rows[0]["name"]);
                    returnobj.Email = Convert.ToString(dt.Rows[0]["email"]);
                }
            }
            catch (Exception ex)
            {
            }
            return returnobj;

        }
    }
}