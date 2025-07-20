using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace YourNamespace
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Initially show request OTP panel only
            if (!IsPostBack)
            {
                pnlRequestOTP.Visible = true;
                pnlResetPassword.Visible = false;
                lblForgotMessage.Text = "";
                lblForgotMessageReset.Text = "";
            }
        }

        private string GenerateOTP()
        {
            Random rand = new Random();
            return rand.Next(100000, 999999).ToString();
        }

        protected void btnSendForgotOTP_Click(object sender, EventArgs e)
        {
            string contact = txtForgotEmailPhone.Text.Trim();

            if (string.IsNullOrEmpty(contact))
            {
                lblForgotMessage.ForeColor = System.Drawing.Color.Red;
                lblForgotMessage.Text = "Please enter your Email or Phone number.";
                return;
            }

            string otp = GenerateOTP();
            Session["ForgotOTP"] = otp;
            Session["ForgotContact"] = contact;

            try
            {
                if (contact.Contains("@"))
                {
                    // Send OTP via email using SMTP (example with Gmail SMTP)
                    MailMessage msg = new MailMessage("youremail@example.com", contact);
                    msg.Subject = "Reset Password OTP";
                    msg.Body = "Your OTP for password reset is: " + otp;

                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.Credentials = new NetworkCredential("youremail@example.com", "yourpassword");
                    smtp.EnableSsl = true;
                    smtp.Send(msg);

                    lblForgotMessage.ForeColor = System.Drawing.Color.Green;
                    lblForgotMessage.Text = "OTP sent to your email.";
                }
                else
                {
                    // Simulate SMS OTP for phone numbers (replace with real SMS API if needed)
                    lblForgotMessage.ForeColor = System.Drawing.Color.Green;
                    lblForgotMessage.Text = "Simulated OTP (for phone): " + otp;
                }
            }
            catch (Exception ex)
            {
                lblForgotMessage.ForeColor = System.Drawing.Color.Red;
                lblForgotMessage.Text = "Error sending OTP: " + ex.Message;
            }
        }

        protected void btnVerifyForgotOTP_Click(object sender, EventArgs e)
        {
            string enteredOtp = txtForgotOTP.Text.Trim();

            if (Session["ForgotOTP"] != null && enteredOtp == Session["ForgotOTP"].ToString())
            {
                lblForgotMessageReset.ForeColor = System.Drawing.Color.Green;
                lblForgotMessageReset.Text = "OTP Verified. Please reset your password.";
                pnlResetPassword.Visible = true;
                pnlRequestOTP.Visible = false;
            }
            else
            {
                lblForgotMessageReset.ForeColor = System.Drawing.Color.Red;
                lblForgotMessageReset.Text = "Invalid OTP.";
            }
        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();
            string contact = Session["ForgotContact"]?.ToString();

            if (newPassword != confirmPassword)
            {
                lblForgotMessageReset.ForeColor = System.Drawing.Color.Red;
                lblForgotMessageReset.Text = "Passwords do not match.";
                return;
            }

            if (string.IsNullOrEmpty(contact))
            {
                lblForgotMessageReset.ForeColor = System.Drawing.Color.Red;
                lblForgotMessageReset.Text = "Session expired. Please try again.";
                return;
            }

            // Update password in DB (assuming a Users table with Email/Phone and Password fields)
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "UPDATE Users SET Password = @Password WHERE Email = @Contact OR Phone = @Contact";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Password", newPassword);
                    cmd.Parameters.AddWithValue("@Contact", contact);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        lblForgotMessageReset.ForeColor = System.Drawing.Color.Green;
                        lblForgotMessageReset.Text = "Password has been reset successfully. You may now login.";
                        pnlResetPassword.Visible = false;
                        pnlRequestOTP.Visible = true;
                    }
                    else
                    {
                        lblForgotMessageReset.ForeColor = System.Drawing.Color.Red;
                        lblForgotMessageReset.Text = "User not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblForgotMessageReset.ForeColor = System.Drawing.Color.Red;
                lblForgotMessageReset.Text = "Error: " + ex.Message;
            }
        }
    }
}
