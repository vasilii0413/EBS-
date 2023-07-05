using Microsoft.Data.SqlClient;

namespace FoodDelivery.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users;

        static UserConstants()
        {
            Users = new List<UserModel>();
            string connectionString = @"Data Source=DESKTOP-07K3JBC;Initial Catalog=FoodDelivery;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"; // Înlocuiește cu stringul de conexiune la baza de date

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM UserAndRole"; //UserAndRole->view

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserModel user = new UserModel()
                            {
                                Email = reader["email"].ToString(),
                                Password = reader["password"].ToString(),
                                FirstName = reader["firstName"].ToString(),
                                LastName = reader["lastName"].ToString(),
                                Role = reader["roleName"].ToString()
                            };

                            Users.Add(user);
                        }
                    }
                }
            }
        }
    }
}
