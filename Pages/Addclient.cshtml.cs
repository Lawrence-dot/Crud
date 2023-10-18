using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Test.Pages
{
    public class CreateModel : PageModel
    {
        public ClientInfo client = new ClientInfo();

        public String errorMessage = "hgh";

        String successMessage = "";

        public void OnGet()
        {

        }

        public void OnPost()
        {
            client.name = Request.Form["name"];
            client.email = Request.Form["email"];
            client.phone = Request.Form["phone"];
            client.address = Request.Form["address"];

            if (client.name.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            } else {
                successMessage = "Client Added Succesfully";
                try
                {
                    String connectionString = "Data Source=DESKTOP-C6S980N;Initial Catalog=Damilare2;Integrated Security=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        String sql = "INSERT INTO client" + "(name, email, phone, address) VALUES " + "(@name, @email, @phone, @address)";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                              command.Parameters.AddWithValue("@name", client.name);
                              command.Parameters.AddWithValue("@email", client.email);
                              command.Parameters.AddWithValue("@phone", client.phone); 
                              command.Parameters.AddWithValue("@address", client.address);
                              command.ExecuteNonQuery();
                        }
                    }
                    Response.Redirect("/index");
                }

                catch (Exception e)
                {
                    errorMessage = e.Message;
                }
            }
        }
    }
}