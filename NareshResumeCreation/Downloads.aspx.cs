using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace NareshResumeCreation
{
    public partial class Downloads : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                LoadResumeList();
            }
        }

        private void LoadResumeList()
        {
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT FileName, DateCreated FROM ResumeFiles WHERE UserID = @UserID ORDER BY DateCreated DESC", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserId"]);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            // Bind the DataTable to the GridView
                            gvResumes.DataSource = dt;
                            gvResumes.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error loading resume list: {ex.Message}');", true);
            }
        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void lnkCreateResume_Click(object sender, EventArgs e)
        {
            Response.Redirect("Resume3.aspx");
        }

        protected void lnkDownloads_Click(object sender, EventArgs e)
        {
            // Already on Downloads page, no action needed
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        

        protected void lnkDelete_Command(object sender, CommandEventArgs e)
        {
            try
            {
                string fileName = e.CommandArgument.ToString();
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                string filePath = Server.MapPath($"~/Resumes/{fileName}");

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM ResumeFiles WHERE UserID = @UserID AND FileName = @FileName", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserId"]);
                        cmd.Parameters.AddWithValue("@FileName", fileName);
                        cmd.ExecuteNonQuery();
                    }
                }

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                LoadResumeList();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error deleting file: {ex.Message}');", true);
            }
        }
    }
}