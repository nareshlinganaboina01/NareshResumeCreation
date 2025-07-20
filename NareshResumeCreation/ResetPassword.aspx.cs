using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace NareshResumeCreation
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string token = Request.QueryString["token"];
                if (string.IsNullOrEmpty(token))
                {
                    lblResetMessage.Text = "Invalid or missing reset token.";
                    btnSubmitReset.Enabled = false;
                }
                else
                {
                    // Validate token
                    string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        string query = "SELECT UserId, Expiry FROM PasswordResetTokens WHERE Token=@Token";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Token", token);
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            DateTime expiry = Convert.ToDateTime(reader["Expiry"]);
                            if (expiry < DateTime.Now)
                            {
                                lblResetMessage.Text = "This reset link has expired.";
                                btnSubmitReset.Enabled = false;
                            }
                        }
                        else
                        {
                            lblResetMessage.Text = "Invalid reset token.";
                            btnSubmitReset.Enabled = false;
                        }
                    }
                }
            }
        }

        protected void btnSubmitReset_Click(object sender, EventArgs e)
        {
            string token = Request.QueryString["token"];
            string newPassword = txtNewPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (newPassword != confirmPassword)
            {
                lblResetMessage.Text = "Passwords do not match.";
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Validate token again
                string query = "SELECT UserId, Expiry FROM PasswordResetTokens WHERE Token=@Token";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Token", token);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int userId = Convert.ToInt32(reader["UserId"]);
                    DateTime expiry = Convert.ToDateTime(reader["Expiry"]);
                    reader.Close();

                    if (expiry < DateTime.Now)
                    {
                        lblResetMessage.Text = "This reset link has expired.";
                        return;
                    }

                    // Update password
                    string newPasswordHash = HashPassword(newPassword);
                    string updateQuery = "UPDATE Userss SET PasswordHash=@PasswordHash WHERE UserId=@UserId";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@PasswordHash", newPasswordHash);
                    updateCmd.Parameters.AddWithValue("@UserId", userId);
                    updateCmd.ExecuteNonQuery();

                    // Delete used token
                    string deleteQuery = "DELETE FROM PasswordResetTokens WHERE Token=@Token";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                    deleteCmd.Parameters.AddWithValue("@Token", token);
                    deleteCmd.ExecuteNonQuery();

                    lblResetMessage.Text = "Password reset successfully. You can now log in.";
                    btnSubmitReset.Enabled = false;
                }
                else
                {
                    lblResetMessage.Text = "Invalid reset token.";
                }
            }
        }

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