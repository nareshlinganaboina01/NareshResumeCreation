using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using System.IO;

namespace NareshResumeCreation
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LogError("Page_Load started.");

            if (!IsPostBack)
            {
                LogError("Page_Load: Not a postback, page initialized.");
            }
            else
            {
                LogError("Page_Load: Postback detected.");

                // Example only: Check if 'txtEmail' key in ViewState is dirty (optional)
                if (ViewState["txtEmail"] != null)
                {
                    bool isDirty = ViewState.IsItemDirty("txtEmail");
                    LogError($"ViewState: txtEmail is {(isDirty ? "Dirty" : "Clean")}");
                }

                LogError($"Form data: Email={Request.Form["txtEmail"]}, Password={Request.Form["txtPassword"]}");
            }

            LogError("Page_Load completed.");
        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            LogError("btnLogin_Click started.");
            try
            {
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text.Trim();
                LogError($"Login attempt for email: {email}");

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    lblLoginMessage.Text = "Please enter both email and password.";
                    LogError("Empty email or password.");
                    return;
                }

                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                if (string.IsNullOrEmpty(connStr))
                {
                    lblLoginMessage.Text = "Database configuration error.";
                    LogError("NareshResumeDB connection string missing.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    try
                    {
                        conn.Open();
                        LogError("Database connection opened.");
                    }
                    catch (Exception ex)
                    {
                        lblLoginMessage.Text = "Database connection failed.";
                        LogError($"Database connection error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                        return;
                    }

                    string query = "SELECT UserId, PasswordHash FROM Userss WHERE Email=@Email";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        try
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                LogError("Executed SELECT query.");
                                if (reader.Read())
                                {
                                    string storedHash = reader["PasswordHash"].ToString();
                                    int userId = Convert.ToInt32(reader["UserId"]);
                                    LogError($"UserId {userId} found for email: {email}");
                                    reader.Close();

                                    if (VerifyPassword(password, storedHash))
                                    {
                                        LogError($"Password verified for UserId {userId}.");
                                        Session["UserId"] = userId;
                                        LogError("Redirecting to Home.aspx.");
                                        Response.Redirect("Home.aspx");
                                    }
                                    else
                                    {
                                        lblLoginMessage.Text = "Invalid email or password.";
                                        LogError("Password verification failed.");
                                    }
                                }
                                else
                                {
                                    lblLoginMessage.Text = "Invalid email or password.";
                                    LogError($"No user found for email: {email}");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            lblLoginMessage.Text = "Error querying database.";
                            LogError($"Query error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblLoginMessage.Text = "Unexpected error. Please try again.";
                LogError($"Unexpected error in btnLogin_Click: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
            finally
            {
                LogError("btnLogin_Click completed.");
            }
        }

        

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            LogError("btnResetPassword_Click started.");
            try
            {
                string email = txtResetEmail.Text.Trim();
                LogError($"Reset password attempt for email: {email}");

                if (string.IsNullOrEmpty(email))
                {
                    lblResetMessage.Text = "Please enter an email address.";
                    LogError("Empty email.");
                    return;
                }

                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                if (string.IsNullOrEmpty(connStr))
                {
                    lblResetMessage.Text = "Database configuration error.";
                    LogError("NareshResumeDB connection string missing.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    try
                    {
                        conn.Open();
                        LogError("Database connection opened.");
                    }
                    catch (Exception ex)
                    {
                        lblResetMessage.Text = "Database connection failed.";
                        LogError($"Database connection error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                        return;
                    }

                    string query = "SELECT UserId FROM Userss WHERE Email=@Email";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int userId = Convert.ToInt32(reader["UserId"]);
                                reader.Close();
                                LogError($"UserId {userId} found for email: {email}");

                                string token = Guid.NewGuid().ToString();
                                DateTime expiry = DateTime.Now.AddHours(1);
                                LogError($"Generated token: {token}, expiry: {expiry}");

                                string insertQuery = "INSERT INTO PasswordResetTokens (UserId, Token, Expiry) VALUES (@UserId, @Token, @Expiry)";
                                using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                                {
                                    insertCmd.Parameters.AddWithValue("@UserId", userId);
                                    insertCmd.Parameters.AddWithValue("@Token", token);
                                    insertCmd.Parameters.AddWithValue("@Expiry", expiry);
                                    insertCmd.ExecuteNonQuery();
                                    LogError("Token inserted into PasswordResetTokens table.");
                                }

                                try
                                {
                                    string resetLink = $"{Request.Url.GetLeftPart(UriPartial.Authority)}/ResetPassword.aspx?token={token}";
                                    LogError($"Generated reset link: {resetLink}");
                                    SendResetEmail(email, resetLink).GetAwaiter().GetResult();
                                    lblResetMessage.Text = "A password reset link has been sent to your email.";
                                    LogError($"Reset link sent to {email}.");
                                }
                                catch (Exception ex)
                                {
                                    lblResetMessage.Text = "Error sending email. Please try again later.";
                                    LogError($"Error sending reset email: {ex.Message}\nStackTrace: {ex.StackTrace}");
                                }
                            }
                            else
                            {
                                lblResetMessage.Text = "Email not found.";
                                LogError($"No user found for email: {email}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblResetMessage.Text = "Unexpected error. Please try again.";
                LogError($"Unexpected error in btnResetPassword_Click: {ex.Message}\nStackTrace: {ex.StackTrace}");
            }
            finally
            {
                LogError("btnResetPassword_Click completed.");
            }
        }

        private async Task SendResetEmail(string email, string resetLink)
        {
            LogError("SendResetEmail started.");
            try
            {
                var apiKey = ConfigurationManager.AppSettings["SendGridApiKey"];
                if (string.IsNullOrEmpty(apiKey))
                {
                    throw new Exception("SendGrid API key missing.");
                }
                LogError("SendGrid API key retrieved.");

                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("nareshresumecreations@gmail.com", "Naresh Resume Creations");
                var subject = "Password Reset Request";
                var to = new EmailAddress(email);
                var plainTextContent = $@"Hello,
You requested a password reset. Click the link below to reset your password:
{resetLink}
This link will expire in 1 hour.
If you did not request this, please ignore this email.
Best regards,
Naresh Resume Creation Team";
                var htmlContent = $@"<p>Hello,</p>
                           <p>You requested a password reset. Click the link below to reset your password:</p>
                           <p><a href='{resetLink}'>Reset Password</a></p>
                           <p>This link will expire in 1 hour.</p>
                           <p>If you did not request this, please ignore this email.</p>
                           <p>Best regards,<br>Naresh Resume Creation Team</p>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                LogError("SendGrid email message created.");

                var response = await client.SendEmailAsync(msg);
                LogError($"SendGrid API response: {response.StatusCode}");
                if (response.StatusCode != System.Net.HttpStatusCode.Accepted && response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    var errorBody = await response.Body.ReadAsStringAsync();
                    throw new Exception($"SendGrid API failed: {response.StatusCode}: {errorBody}");
                }
                LogError("SendGrid email sent.");
            }
            catch (Exception ex)
            {
                LogError($"Error in SendResetEmail for {email}: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
            finally
            {
                LogError("SendResetEmail completed.");
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            LogError("VerifyPassword started.");
            try
            {
                string enteredHash = HashPassword(enteredPassword);
                bool result = enteredHash == storedHash;
                LogError($"VerifyPassword result: {result}");
                return result;
            }
            catch (Exception ex)
            {
                LogError($"Error in VerifyPassword: {ex.Message}\nStackTrace: {ex.StackTrace}");
                return false;
            }
        }

        private string HashPassword(string password)
        {
            LogError("HashPassword started.");
            try
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    string hash = builder.ToString();
                    LogError("HashPassword completed.");
                    return hash;
                }
            }
            catch (Exception ex)
            {
                LogError($"Error in HashPassword: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }

        private void LogError(string message)
        {
            try
            {
                string logPath = Server.MapPath("~/Logs/ErrorLog.txt");
                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} IST: {message}\n";
                File.AppendAllText(logPath, logMessage);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to log: {ex.Message}");
            }
        }
    }
}