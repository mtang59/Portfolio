using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FenceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create the connection to the resource!
            // This is the connection, that is established and
            // will be available throughout this block.
            using (SqlConnection conn = new SqlConnection())
            {
                // Create the connectionString
                // Trusted_Connection is used to denote the connection uses Windows Authentication
                conn.ConnectionString = "Server=MICHAEL;Database=FenceAppDB;Trusted_Connection=true";
                conn.Open();

                string query = "SELECT ID, firstName, lastName, phoneNumber, emailAddress, address FROM Customers";
                SqlCommand command = new SqlCommand(query, conn);

                /* Get the rows and display on the screen! 
                 * This section of the code has the basic code
                 * that will display the content from the Database Table
                 * on the screen using an SqlDataReader. */

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("ID\tfirstName\tlastName\tphoneNumber\temailAddress\taddress\t");
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0} \t | {1} \t\t {2} \t\t {3} \t\t {4} \t\t {5}",
                            reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]));
                    }
                }
                Console.WriteLine("Data displayed! Now press enter to move to the next section!");
                Console.ReadLine();
                Console.Clear();

                // Retrieve the current maximum ID from the Customers table
                SqlCommand maxIdCommand = new SqlCommand("SELECT MAX(ID) FROM Customers", conn);
                object result = maxIdCommand.ExecuteScalar();
                int maxId = (result != DBNull.Value) ? Convert.ToInt32(result) : -1;
                int nextId = maxId + 1;

                /* Above code was used to display the data from the Database table!
                 * This following section explains the key features to use 
                 * to add the data to the table. This is an example of another
                 * SQL Command (INSERT INTO), this will teach the usage of parameters and connection.*/

                // Create the command, to insert the data into the Table!
                // this is a simple INSERT INTO command!

                int ID = nextId;
                Console.WriteLine("Enter the first name:");
                string firstName = Console.ReadLine();
                Console.WriteLine("Enter the last name:");
                string lastName = Console.ReadLine();
                Console.WriteLine("Enter the phone number:");
                string phoneNumber = Console.ReadLine();
                Console.WriteLine("Enter the email address:");
                string emailAddress = Console.ReadLine();
                Console.WriteLine("Enter the address:");
                string address = Console.ReadLine();
                query = "INSERT INTO Customers (ID, firstName, lastName, phoneNumber, emailAddress, address) " +
                    "VALUES (@ID, @firstName, @lastName, @phoneNumber, @emailAddress, @address)";
                SqlCommand insertCommand = new SqlCommand(query, conn);

                // In the command, there are some parameters denoted by @, you can 
                // change their value on a condition, in my code they're hardcoded.

                insertCommand.Parameters.AddWithValue("@ID", ID);
                insertCommand.Parameters.AddWithValue("@firstName", firstName);
                insertCommand.Parameters.AddWithValue("@lastName", lastName);
                insertCommand.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                insertCommand.Parameters.AddWithValue("@emailAddress", emailAddress);
                insertCommand.Parameters.AddWithValue("@address", address);

                // Execute the command, and print the values of the columns affected through
                // the command executed.

                Console.WriteLine("Commands executed! Total rows affected are " + insertCommand.ExecuteNonQuery());
                Console.WriteLine("Done! Press enter to move to the next step");
                Console.ReadLine();
                Console.Clear();
            // Final step, close the resources flush dispose them. ReadLine to prevent the console from closing.
            Console.ReadLine();
        }
    }
}}