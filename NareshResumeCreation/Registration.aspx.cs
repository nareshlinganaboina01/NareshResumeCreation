using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace NareshResumeCreation
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                lblRegisterMessage.Text = "Passwords do not match!";
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Check if email already exists
                string checkUserQuery = "SELECT COUNT(*) FROM Userss WHERE Email=@Email";
                SqlCommand checkCmd = new SqlCommand(checkUserQuery, conn);
                checkCmd.Parameters.AddWithValue("@Email", txtEmailPhone.Text);
                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    lblRegisterMessage.Text = "Email already exists!";
                    return;
                }

                // Insert new user
                string hashedPassword = HashPassword(txtPassword.Text);
                string query = "INSERT INTO Userss (FirstName, LastName, Email, PasswordHash) " +
                               "VALUES (@FirstName, @LastName, @Email, @PasswordHash)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmailPhone.Text);
                cmd.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                cmd.ExecuteNonQuery();

                lblRegisterMessage.ForeColor = System.Drawing.Color.Green;
                lblRegisterMessage.Text = "Registration successful! Redirecting to login...";
                Response.Redirect("Login.aspx");
            }
        }

        // Function to hash password using SHA256
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}