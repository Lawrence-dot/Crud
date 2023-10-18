using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Test.Pages
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listclients = new List<ClientInfo>();


        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=DESKTOP-C6S980N;Initial Catalog=Damilare2;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM client";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo info = new ClientInfo();
                                info.id = reader.GetInt32(0);
                                info.name = reader.GetString(1);
                                info.email = reader.GetString(2);
                                info.phone = reader.GetString(3);
                                info.address = reader.GetString(4);
                                info.created_at = "";
                                listclients.Add(info);
                            }
                        }
                    }
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }

    public class ClientInfo
    {
        public int id;

        public string name;

        public string address;

        public string email;

        public string phone;

        public string created_at;
    }
}