using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CommandApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string conString = ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;
            SqlConnection sqlCon = new SqlConnection(conString);
            try
            {
                sqlCon.Open();

                var serverName = sqlCon.DataSource;
                var database = sqlCon.Database;
                var state = sqlCon.State;
                var connectiontTimeout = sqlCon.ConnectionTimeout;

                Console.WriteLine("Server Name: " + serverName);
                Console.WriteLine("Database Name: " + database);
                Console.WriteLine("Connection State: " + state);
                Console.WriteLine("Connection Timeout: " + connectiontTimeout + " sec.");
                Console.WriteLine();

                string selectQuery = "Select * from Customer";
                string insertQuery = "insert into customer values ('6','Vegeta','Namek')";
                string updateQuery = "update customer set lastname = 'Vivek' where id = '5'";
                string deleteQuery = "delete from customer where id = '5'";

                SqlCommand inCmd = new SqlCommand(insertQuery, sqlCon);
                inCmd.ExecuteNonQuery();
                Console.WriteLine("\nRecord Inserted..");

                SqlCommand upCmd = new SqlCommand(updateQuery, sqlCon);
                upCmd.ExecuteNonQuery();
                Console.WriteLine("\nRecord Updated..");

                SqlCommand delCmd = new SqlCommand(deleteQuery, sqlCon);
                delCmd.ExecuteNonQuery();
                Console.WriteLine("\nRecord Deleted..");

                Console.WriteLine("\nAfter Inserting New Records");

                SqlCommand cmd = new SqlCommand(selectQuery, sqlCon);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString());
                }
            }
            catch (SystemException ex)
            {
                Console.WriteLine("Exception: "+ex.Message);
            }
            finally
            {
                if (sqlCon.State == ConnectionState.Open)
                {
                    sqlCon.Close();
                }
            }
        }
    }
}
