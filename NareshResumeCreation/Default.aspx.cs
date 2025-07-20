using System;
using System.Web.UI;

namespace NareshResumeCreation
{
    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // No authentication check, allowing all users to access the page
        }

        protected void btnGetStarted_Click(object sender, EventArgs e)
        {
            // Redirect to resume creation page
            Response.Redirect("Registration.aspx");
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Redirect to Login page
            Response.Redirect("Login.aspx");
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Redirect to Register page
            Response.Redirect("Registration.aspx");
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            // Server-side validation for contact form
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string message = txtMessage.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(message))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "formMessage",
                    "document.getElementById('formMessage').textContent = 'Please fill in all fields.'; " +
                    "document.getElementById('formMessage').classList.remove('success'); " +
                    "document.getElementById('formMessage').classList.add('error'); " +
                    "document.getElementById('formMessage').style.display = 'block';", true);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "formMessage",
                    "document.getElementById('formMessage').textContent = 'Please enter a valid email address.'; " +
                    "document.getElementById('formMessage').classList.remove('success'); " +
                    "document.getElementById('formMessage').classList.add('error'); " +
                    "document.getElementById('formMessage').style.display = 'block';", true);
                return;
            }

            // Placeholder for form processing (e.g., save to database or send email)
            txtName.Text = "";
            txtEmail.Text = "";
            txtMessage.Text = "";
            ClientScript.RegisterStartupScript(this.GetType(), "formMessage",
                "document.getElementById('formMessage').textContent = 'Thank you for your message! We will get back to you soon.'; " +
                "document.getElementById('formMessage').classList.remove('error'); " +
                "document.getElementById('formMessage').classList.add('success'); " +
                "document.getElementById('formMessage').style.display = 'block';", true);
        }
    }
}