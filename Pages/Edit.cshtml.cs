using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Test.Pages
{
    public class EditModel : PageModel
    {
        public ClientInfo client = new ClientInfo();
        public string errormessage;
        public string successmesage;

        public void OnGet()
        {

            string id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=DESKTOP-C6S980N;Initial Catalog=Damilare2;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM client where id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                client.id = reader.GetInt32(0);
                                client.name = reader.GetString(1);
                                client.email = reader.GetString(2);
                                client.phone = reader.GetString(3);
                                client.address = reader.GetString(4);
                                client.created_at = "";
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

        public void OnPost() {
            client.id = Int32.Parse(Request.Form["id"].ToString());
            client.name = Request.Form["name"];
            client.email = Request.Form["email"];
            client.phone = Request.Form["phone"];
            client.address = Request.Form["address"];


            if (client.name.Length == 0 || client.address.Length == 0 || client.phone.Length == 0)
            {
                errormessage = "Please Fill all Input Fields Correctly";
                return;
            } else {
                try
                {
                    Console.WriteLine(client.id);
                    String connectionString = "Data Source=DESKTOP-C6S980N;Initial Catalog=Damilare2;Integrated Security=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        String sql = "UPDATE client " +
                                    "SET name=@name, email=@email, phone=@phone, address=@address " +
                                    "WHERE id=@id";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@name", client.name);
                            command.Parameters.AddWithValue("@email", client.email);
                            command.Parameters.AddWithValue("@phone", client.phone); 
                            command.Parameters.AddWithValue("@address", client.address);
                            command.Parameters.AddWithValue("@id", client.id);
                            command.ExecuteNonQuery();
                        }
                    }
                    Response.Redirect("/index");
                }
                catch (System.Exception ex)
                {
                    errormessage = ex.Message;
                }
            }
        }
    }
}