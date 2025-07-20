using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Linq;

namespace NareshResumeCreation
{
    public partial class Resume : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Deafault.aspx");
            }
            if (!IsPostBack)
            {
                LoadData();
                LoadEmploymentHistory();
                LoadEducation();
                LoadSkills();
                LoadLanguages();
                LoadCertifications();
                LoadInternships();
                LoadHobbies();
                LoadCustomSelection();
                CheckEmploymentStatus();
                ViewState["IsEditMode"] = false;
            }
            ToggleEditMode();
        }

        private void ToggleEditMode()
        {
            bool isEditMode = ViewState["IsEditMode"] != null && (bool)ViewState["IsEditMode"];
            resumeContainer.Attributes["class"] = isEditMode ? "resume-container edit-mode" : "resume-container";
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ViewState["IsEditMode"] = true;
            LoadEditData();
            ToggleEditMode();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["IsEditMode"] = false;
            LoadData();
            LoadEmploymentHistory();
            LoadEducation();
            LoadSkills();
            LoadLanguages();
            LoadCertifications();
            LoadInternships();
            LoadHobbies();
            LoadCustomSelection();
            CheckEmploymentStatus();
            ToggleEditMode();
        }

        private void LoadData()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT TOP 1 * FROM PersonalDetails WHERE UserID = @UserID ORDER BY ID DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblFirstName.Text = reader["FirstName"].ToString();
                    lblLastName.Text = reader["LastName"].ToString();
                    lblJobTitle.Text = reader["JobTitle"].ToString();
                    lblEmail.Text = reader["Email"].ToString();
                    lblPhone.Text = reader["Phone"].ToString();
                    lblDateOfBirth.Text = Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                    lblAddress.Text = reader["Address"].ToString();
                    lblCity.Text = reader["City"].ToString();
                    lblState.Text = reader["State"].ToString();
                    lblPostalCode.Text = reader["PostalCode"].ToString();
                    lblCountry.Text = reader["Country"].ToString();
                    lblNationality.Text = reader["Nationality"].ToString();
                    lblPlaceOfBirth.Text = reader["PlaceOfBirth"].ToString();
                    lblProfessionalSummary.Text = reader["PersonalSummary"].ToString();
                }
                else
                {
                    lblFirstName.Text = "No Data";
                    lblLastName.Text = "No Data";
                    lblJobTitle.Text = "No Data";
                    lblEmail.Text = "No Data";
                    lblPhone.Text = "No Data";
                    lblDateOfBirth.Text = "No Data";
                    lblAddress.Text = "No Data";
                    lblCity.Text = "No Data";
                    lblState.Text = "No Data";
                    lblPostalCode.Text = "No Data";
                    lblCountry.Text = "No Data";
                    lblNationality.Text = "No Data";
                    lblPlaceOfBirth.Text = "No Data";
                    lblProfessionalSummary.Text = "No Data";
                }
                reader.Close();
            }
        }

        private void LoadEditData()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // Load Personal Details
                string query = "SELECT TOP 1 * FROM PersonalDetails WHERE UserID = @UserID ORDER BY ID DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtFirstName.Text = reader["FirstName"].ToString();
                    txtLastName.Text = reader["LastName"].ToString();
                    txtJobTitle.Text = reader["JobTitle"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    txtPhone.Text = reader["Phone"].ToString();
                    txtDateOfBirth.Text = Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                    txtAddress.Text = reader["Address"].ToString();
                    txtCity.Text = reader["City"].ToString();
                    txtState.Text = reader["State"].ToString();
                    txtPostalCode.Text = reader["PostalCode"].ToString();
                    txtCountry.Text = reader["Country"].ToString();
                    txtNationality.Text = reader["Nationality"].ToString();
                    txtPlaceOfBirth.Text = reader["PlaceOfBirth"].ToString();
                    txtProfessionalSummary.Text = reader["PersonalSummary"].ToString();
                }
                reader.Close();

                // Load IsFresher
                string checkStatusQuery = "SELECT IsFresher FROM Userss WHERE UserID = @UserID";
                cmd = new SqlCommand(checkStatusQuery, conn);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                chkIsFresher.Checked = Convert.ToBoolean(cmd.ExecuteScalar());
            }

            // Load repeater data for edit mode
            LoadEducationEdit();
            LoadEmploymentHistoryEdit();
            LoadSkillsEdit();
            LoadLanguagesEdit();
            LoadCertificationsEdit();
            LoadHobbiesEdit();
            LoadCustomSelectionEdit();
        }

        private void CheckEmploymentStatus()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string checkStatusQuery = "SELECT IsFresher FROM Userss WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(checkStatusQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    bool isFresher = true;
                    if (result != null && result != DBNull.Value)
                    {
                        isFresher = Convert.ToBoolean(result);
                    }
                    if (isFresher)
                    {
                        pnlFresher.Visible = true;
                        pnlEmploymentHistory.Visible = false;
                    }
                    else
                    {
                        pnlFresher.Visible = false;
                        pnlEmploymentHistory.Visible = true;
                    }
                }
            }
        }

        private void LoadEmploymentHistory()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"SELECT JobTitle, Employer, City, StartDate, EndDate, Description 
                                FROM EmploymentHistoryy WHERE UserID = @UserID ORDER BY StartDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    rptEmploymentHistory.DataSource = dt;
                    rptEmploymentHistory.DataBind();
                }
            }
        }

        private void LoadEmploymentHistoryEdit()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"SELECT * FROM EmploymentHistoryy WHERE UserID = @UserID ORDER BY StartDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    rptEmploymentHistoryEdit.DataSource = dt;
                    rptEmploymentHistoryEdit.DataBind();
                }
            }
        }

        private void LoadEducation()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Education WHERE UserID = @UserID ORDER BY StartDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        rptEducation.DataSource = dt;
                        rptEducation.DataBind();
                    }
                    else
                    {
                        rptEducation.DataSource = new[] { new { Degree = "No education found", SchoolName = "", City = "", StartDate = "N/A", EndDate = "N/A", Description = "" } };
                        rptEducation.DataBind();
                    }
                }
            }
        }

        private void LoadEducationEdit()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Education WHERE UserID = @UserID ORDER BY StartDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    rptEducationEdit.DataSource = dt;
                    rptEducationEdit.DataBind();
                }
            }
        }

        private void LoadSkills()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT SkillName FROM Skills WHERE UserID = @UserID ORDER BY ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        rptSkills.DataSource = dt;
                        rptSkills.DataBind();
                    }
                    else
                    {
                        rptSkills.DataSource = new[] { new { SkillName = "No skills found" } };
                        rptSkills.DataBind();
                    }
                }
            }
        }

        private void LoadSkillsEdit()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Skills WHERE UserID = @UserID ORDER BY ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    rptSkillsEdit.DataSource = dt;
                    rptSkillsEdit.DataBind();
                }
            }
        }

        private void LoadLanguages()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT LanguageName FROM Languages WHERE UserID = @UserID ORDER BY ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        rptLanguages.DataSource = dt;
                        rptLanguages.DataBind();
                    }
                    else
                    {
                        rptLanguages.DataSource = new[] { new { LanguageName = "No languages found" } };
                        rptLanguages.DataBind();
                    }
                }
            }
        }

        private void LoadLanguagesEdit()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Languages WHERE UserID = @UserID ORDER BY ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    rptLanguagesEdit.DataSource = dt;
                    rptLanguagesEdit.DataBind();
                }
            }
        }

        private void LoadCertifications()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Courses WHERE UserID = @UserID ORDER BY StartDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        rptCertifications.DataSource = dt;
                        rptCertifications.DataBind();
                    }
                    else
                    {
                        rptCertifications.DataSource = new[] { new { CourseName = "No certifications found", Institution = "", StartDate = "N/A", EndDate = "N/A" } };
                        rptCertifications.DataBind();
                    }
                }
            }
        }

        private void LoadCertificationsEdit()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Courses WHERE UserID = @UserID ORDER BY StartDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    rptCertificationsEdit.DataSource = dt;
                    rptCertificationsEdit.DataBind();
                }
            }
        }

        private void LoadInternships()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT TOP 1 * FROM Internships WHERE UserID = @UserID ORDER BY ID DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lblInternCompany.Text = reader["CompanyName"].ToString();
                        lblInternJobTitle.Text = reader["JobTitle"].ToString();
                        lblInternStartDate.Text = Convert.ToDateTime(reader["StartDate"]).ToString("yyyy-MM-dd");
                        lblInternEndDate.Text = Convert.ToDateTime(reader["EndDate"]).ToString("yyyy-MM-dd");
                        lblInternDescription.Text = reader["Description"].ToString();
                        txtInternCompany.Text = reader["CompanyName"].ToString();
                        txtInternJobTitle.Text = reader["JobTitle"].ToString();
                        txtInternStartDate.Text = Convert.ToDateTime(reader["StartDate"]).ToString("yyyy-MM-dd");
                        txtInternEndDate.Text = Convert.ToDateTime(reader["EndDate"]).ToString("yyyy-MM-dd");
                        txtInternDescription.Text = reader["Description"].ToString();
                    }
                    else
                    {
                        lblInternCompany.Text = "N/A";
                        lblInternJobTitle.Text = "N/A";
                        lblInternStartDate.Text = "N/A";
                        lblInternEndDate.Text = "N/A";
                        lblInternDescription.Text = "No internship data available";
                        txtInternCompany.Text = "";
                        txtInternJobTitle.Text = "";
                        txtInternStartDate.Text = "";
                        txtInternEndDate.Text = "";
                        txtInternDescription.Text = "";
                    }
                    reader.Close();
                }
            }
        }

        private void LoadHobbies()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT Name FROM Hobbies WHERE UserID = @UserID ORDER BY ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        rptHobbies.DataSource = dt;
                        rptHobbies.DataBind();
                    }
                    else
                    {
                        rptHobbies.DataSource = new[] { new { Name = "No hobbies found" } };
                        rptHobbies.DataBind();
                    }
                }
            }
        }

        private void LoadHobbiesEdit()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Hobbies WHERE UserID = @UserID ORDER BY ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    rptHobbiesEdit.DataSource = dt;
                    rptHobbiesEdit.DataBind();
                }
            }
        }

        private void LoadCustomSelection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM CustomSelection WHERE UserID = @UserID ORDER BY StartDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        rptCustomSelection.DataSource = dt;
                        rptCustomSelection.DataBind();
                    }
                    else
                    {
                        rptCustomSelection.DataSource = new[] { new { Title = "No additional info", StartDate = "N/A", EndDate = "N/A", Description = "" } };
                        rptCustomSelection.DataBind();
                    }
                }
            }
        }

        private void LoadCustomSelectionEdit()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM CustomSelection WHERE UserID = @UserID ORDER BY StartDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    rptCustomSelectionEdit.DataSource = dt;
                    rptCustomSelectionEdit.DataBind();
                }
            }
        }

        protected void btnAddEducation_Click(object sender, EventArgs e)
        {
            DataTable dt = GetEducationDataTable();
            dt.Rows.Add(0, "", "", "", DateTime.Now, DateTime.Now, "", Session["UserID"]);
            rptEducationEdit.DataSource = dt;
            rptEducationEdit.DataBind();
        }

        protected void btnAddEmployment_Click(object sender, EventArgs e)
        {
            DataTable dt = GetEmploymentDataTable();
            dt.Rows.Add(0, "", "", "", DateTime.Now, DateTime.Now, "", Session["UserID"]);
            rptEmploymentHistoryEdit.DataSource = dt;
            rptEmploymentHistoryEdit.DataBind();
        }

        protected void btnAddSkill_Click(object sender, EventArgs e)
        {
            DataTable dt = GetSkillsDataTable();
            dt.Rows.Add(0, "", Session["UserID"]);
            rptSkillsEdit.DataSource = dt;
            rptSkillsEdit.DataBind();
        }

        protected void btnAddLanguage_Click(object sender, EventArgs e)
        {
            DataTable dt = GetLanguagesDataTable();
            dt.Rows.Add(0, "", Session["UserID"]);
            rptLanguagesEdit.DataSource = dt;
            rptLanguagesEdit.DataBind();
        }

        protected void btnAddCertification_Click(object sender, EventArgs e)
        {
            DataTable dt = GetCertificationsDataTable();
            dt.Rows.Add(0, "", "", DateTime.Now, DateTime.Now, Session["UserID"]);
            rptCertificationsEdit.DataSource = dt;
            rptCertificationsEdit.DataBind();
        }

        protected void btnAddHobby_Click(object sender, EventArgs e)
        {
            DataTable dt = GetHobbiesDataTable();
            dt.Rows.Add(0, "", Session["UserID"]);
            rptHobbiesEdit.DataSource = dt;
            rptHobbiesEdit.DataBind();
        }

        protected void btnAddCustomSelection_Click(object sender, EventArgs e)
        {
            DataTable dt = GetCustomSelectionDataTable();
            dt.Rows.Add(0, "", DateTime.Now, DateTime.Now, "", Session["UserID"]);
            rptCustomSelectionEdit.DataSource = dt;
            rptCustomSelectionEdit.DataBind();
        }

        protected void rptEducationEdit_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                if (id > 0)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        string query = "DELETE FROM Education WHERE ID = @ID AND UserID = @UserID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEducationEdit();
            }
        }

        protected void rptEmploymentHistoryEdit_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                if (id > 0)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        string query = "DELETE FROM EmploymentHistoryy WHERE ID = @ID AND UserID = @UserID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEmploymentHistoryEdit();
            }
        }

        protected void rptSkillsEdit_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                if (id > 0)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        string query = "DELETE FROM Skills WHERE ID = @ID AND UserID = @UserID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadSkillsEdit();
            }
        }

        protected void rptLanguagesEdit_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                if (id > 0)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        string query = "DELETE FROM Languages WHERE ID = @ID AND UserID = @UserID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadLanguagesEdit();
            }
        }

        protected void rptCertificationsEdit_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                if (id > 0)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        string query = "DELETE FROM Courses WHERE ID = @ID AND UserID = @UserID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadCertificationsEdit();
            }
        }

        protected void rptHobbiesEdit_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                if (id > 0)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        string query = "DELETE FROM Hobbies WHERE ID = @ID AND UserID = @UserID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadHobbiesEdit();
            }
        }

        protected void rptCustomSelectionEdit_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                if (id > 0)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        string query = "DELETE FROM CustomSelection WHERE ID = @ID AND UserID = @UserID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadCustomSelectionEdit();
            }
        }

        private DataTable GetEducationDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Degree", typeof(string));
            dt.Columns.Add("SchoolName", typeof(string));
            dt.Columns.Add("City", typeof(string));
            dt.Columns.Add("StartDate", typeof(DateTime));
            dt.Columns.Add("EndDate", typeof(DateTime));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("UserID", typeof(string));
            foreach (RepeaterItem item in rptEducationEdit.Items)
            {
                TextBox txtDegree = item.FindControl("txtDegree") as TextBox;
                TextBox txtSchoolName = item.FindControl("txtSchoolName") as TextBox;
                TextBox txtCity = item.FindControl("txtCity") as TextBox;
                TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;
                TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
                dt.Rows.Add(0, txtDegree.Text, txtSchoolName.Text, txtCity.Text,
                    DateTime.TryParse(txtStartDate.Text, out DateTime startDate) ? startDate : DateTime.Now,
                    DateTime.TryParse(txtEndDate.Text, out DateTime endDate) ? endDate : DateTime.Now,
                    txtDescription.Text, Session["UserID"]);
            }
            return dt;
        }

        private DataTable GetEmploymentDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("JobTitle", typeof(string));
            dt.Columns.Add("Employer", typeof(string));
            dt.Columns.Add("City", typeof(string));
            dt.Columns.Add("StartDate", typeof(DateTime));
            dt.Columns.Add("EndDate", typeof(DateTime));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("UserID", typeof(string));
            foreach (RepeaterItem item in rptEmploymentHistoryEdit.Items)
            {
                TextBox txtJobTitle = item.FindControl("txtJobTitle") as TextBox;
                TextBox txtEmployer = item.FindControl("txtEmployer") as TextBox;
                TextBox txtCity = item.FindControl("txtCity") as TextBox;
                TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;
                TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
                dt.Rows.Add(0, txtJobTitle.Text, txtEmployer.Text, txtCity.Text,
                    DateTime.TryParse(txtStartDate.Text, out DateTime startDate) ? startDate : DateTime.Now,
                    DateTime.TryParse(txtEndDate.Text, out DateTime endDate) ? endDate : DateTime.Now,
                    txtDescription.Text, Session["UserID"]);
            }
            return dt;
        }

        private DataTable GetSkillsDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("SkillName", typeof(string));
            dt.Columns.Add("UserID", typeof(string));
            foreach (RepeaterItem item in rptSkillsEdit.Items)
            {
                TextBox txtSkillName = item.FindControl("txtSkillName") as TextBox;
                dt.Rows.Add(0, txtSkillName.Text, Session["UserID"]);
            }
            return dt;
        }

        private DataTable GetLanguagesDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("LanguageName", typeof(string));
            dt.Columns.Add("UserID", typeof(string));
            foreach (RepeaterItem item in rptLanguagesEdit.Items)
            {
                TextBox txtLanguageName = item.FindControl("txtLanguageName") as TextBox;
                dt.Rows.Add(0, txtLanguageName.Text, Session["UserID"]);
            }
            return dt;
        }

        private DataTable GetCertificationsDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("CourseName", typeof(string));
            dt.Columns.Add("Institution", typeof(string));
            dt.Columns.Add("StartDate", typeof(DateTime));
            dt.Columns.Add("EndDate", typeof(DateTime));
            dt.Columns.Add("UserID", typeof(string));
            foreach (RepeaterItem item in rptCertificationsEdit.Items)
            {
                TextBox txtCourseName = item.FindControl("txtCourseName") as TextBox;
                TextBox txtInstitution = item.FindControl("txtInstitution") as TextBox;
                TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;
                dt.Rows.Add(0, txtCourseName.Text, txtInstitution.Text,
                    DateTime.TryParse(txtStartDate.Text, out DateTime startDate) ? startDate : DateTime.Now,
                    DateTime.TryParse(txtEndDate.Text, out DateTime endDate) ? endDate : DateTime.Now,
                    Session["UserID"]);
            }
            return dt;
        }

        private DataTable GetHobbiesDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("UserID", typeof(string));
            foreach (RepeaterItem item in rptHobbiesEdit.Items)
            {
                TextBox txtHobbyName = item.FindControl("txtHobbyName") as TextBox;
                dt.Rows.Add(0, txtHobbyName.Text, Session["UserID"]);
            }
            return dt;
        }

        private DataTable GetCustomSelectionDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("StartDate", typeof(DateTime));
            dt.Columns.Add("EndDate", typeof(DateTime));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("UserID", typeof(string));
            foreach (RepeaterItem item in rptCustomSelectionEdit.Items)
            {
                TextBox txtTitle = item.FindControl("txtTitle") as TextBox;
                TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;
                TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
                dt.Rows.Add(0, txtTitle.Text,
                    DateTime.TryParse(txtStartDate.Text, out DateTime startDate) ? startDate : DateTime.Now,
                    DateTime.TryParse(txtEndDate.Text, out DateTime endDate) ? endDate : DateTime.Now,
                    txtDescription.Text, Session["UserID"]);
            }
            return dt;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    // Update Personal Details
                    string updatePersonalQuery = @"UPDATE PersonalDetails SET 
                        FirstName = @FirstName, LastName = @LastName, JobTitle = @JobTitle, Email = @Email, 
                        Phone = @Phone, DateOfBirth = @DateOfBirth, Address = @Address, City = @City, 
                        State = @State, PostalCode = @PostalCode, Country = @Country, Nationality = @Nationality, 
                        PlaceOfBirth = @PlaceOfBirth, PersonalSummary = @PersonalSummary
                        WHERE UserID = @UserID AND ID = (SELECT TOP 1 ID FROM PersonalDetails WHERE UserID = @UserID ORDER BY ID DESC)";
                    using (SqlCommand cmd = new SqlCommand(updatePersonalQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                        cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                        cmd.Parameters.AddWithValue("@JobTitle", txtJobTitle.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@DateOfBirth", DateTime.Parse(txtDateOfBirth.Text));
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@City", txtCity.Text);
                        cmd.Parameters.AddWithValue("@State", txtState.Text);
                        cmd.Parameters.AddWithValue("@PostalCode", txtPostalCode.Text);
                        cmd.Parameters.AddWithValue("@Country", txtCountry.Text);
                        cmd.Parameters.AddWithValue("@Nationality", txtNationality.Text);
                        cmd.Parameters.AddWithValue("@PlaceOfBirth", txtPlaceOfBirth.Text);
                        cmd.Parameters.AddWithValue("@PersonalSummary", txtProfessionalSummary.Text);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            string insertPersonalQuery = @"INSERT INTO PersonalDetails 
                                (FirstName, LastName, JobTitle, Email, Phone, DateOfBirth, Address, City, State, PostalCode, Country, Nationality, PlaceOfBirth, PersonalSummary, UserID)
                                VALUES (@FirstName, @LastName, @JobTitle, @Email, @Phone, @DateOfBirth, @Address, @City, @State, @PostalCode, @Country, @Nationality, @PlaceOfBirth, @PersonalSummary, @UserID)";
                            using (SqlCommand insertCmd = new SqlCommand(insertPersonalQuery, conn, transaction))
                            {
                                insertCmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                                insertCmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                                insertCmd.Parameters.AddWithValue("@JobTitle", txtJobTitle.Text);
                                insertCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                                insertCmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                                insertCmd.Parameters.AddWithValue("@DateOfBirth", DateTime.Parse(txtDateOfBirth.Text));
                                insertCmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                                insertCmd.Parameters.AddWithValue("@City", txtCity.Text);
                                insertCmd.Parameters.AddWithValue("@State", txtState.Text);
                                insertCmd.Parameters.AddWithValue("@PostalCode", txtPostalCode.Text);
                                insertCmd.Parameters.AddWithValue("@Country", txtCountry.Text);
                                insertCmd.Parameters.AddWithValue("@Nationality", txtNationality.Text);
                                insertCmd.Parameters.AddWithValue("@PlaceOfBirth", txtPlaceOfBirth.Text);
                                insertCmd.Parameters.AddWithValue("@PersonalSummary", txtProfessionalSummary.Text);
                                insertCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Update IsFresher
                    string updateFresherQuery = "UPDATE Userss SET IsFresher = @IsFresher WHERE UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(updateFresherQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@IsFresher", chkIsFresher.Checked);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.ExecuteNonQuery();
                    }

                    // Clear existing records
                    string deleteEducationQuery = "DELETE FROM Education WHERE UserID = @UserID";
                    string deleteEmploymentQuery = "DELETE FROM EmploymentHistoryy WHERE UserID = @UserID";
                    string deleteSkillsQuery = "DELETE FROM Skills WHERE UserID = @UserID";
                    string deleteLanguagesQuery = "DELETE FROM Languages WHERE UserID = @UserID";
                    string deleteCertificationsQuery = "DELETE FROM Courses WHERE UserID = @UserID";
                    string deleteHobbiesQuery = "DELETE FROM Hobbies WHERE UserID = @UserID";
                    string deleteCustomQuery = "DELETE FROM CustomSelection WHERE UserID = @UserID";
                    string deleteInternshipsQuery = "DELETE FROM Internships WHERE UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(deleteEducationQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = new SqlCommand(deleteEmploymentQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = new SqlCommand(deleteSkillsQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = new SqlCommand(deleteLanguagesQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = new SqlCommand(deleteCertificationsQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = new SqlCommand(deleteHobbiesQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = new SqlCommand(deleteCustomQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = new SqlCommand(deleteInternshipsQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.ExecuteNonQuery();
                    }

                    // Insert Education
                    foreach (RepeaterItem item in rptEducationEdit.Items)
                    {
                        TextBox txtDegree = item.FindControl("txtDegree") as TextBox;
                        TextBox txtSchoolName = item.FindControl("txtSchoolName") as TextBox;
                        TextBox txtCity = item.FindControl("txtCity") as TextBox;
                        TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                        TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;
                        TextBox txtDescription = item.FindControl("txtDescription") as TextBox;

                        if (!string.IsNullOrWhiteSpace(txtDegree.Text) && !string.IsNullOrWhiteSpace(txtSchoolName.Text))
                        {
                            string insertQuery = @"INSERT INTO Education (Degree, SchoolName, City, StartDate, EndDate, Description, UserID)
                                                VALUES (@Degree, @SchoolName, @City, @StartDate, @EndDate, @Description, @UserID)";
                            using (SqlCommand cmd = new SqlCommand(insertQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Degree", txtDegree.Text);
                                cmd.Parameters.AddWithValue("@SchoolName", txtSchoolName.Text);
                                cmd.Parameters.AddWithValue("@City", txtCity.Text);
                                cmd.Parameters.AddWithValue("@StartDate", DateTime.Parse(txtStartDate.Text));
                                cmd.Parameters.AddWithValue("@EndDate", DateTime.Parse(txtEndDate.Text));
                                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Insert Employment History
                    foreach (RepeaterItem item in rptEmploymentHistoryEdit.Items)
                    {
                        TextBox txtJobTitle = item.FindControl("txtJobTitle") as TextBox;
                        TextBox txtEmployer = item.FindControl("txtEmployer") as TextBox;
                        TextBox txtCity = item.FindControl("txtCity") as TextBox;
                        TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                        TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;
                        TextBox txtDescription = item.FindControl("txtDescription") as TextBox;

                        if (!string.IsNullOrWhiteSpace(txtJobTitle.Text) && !string.IsNullOrWhiteSpace(txtEmployer.Text))
                        {
                            string insertQuery = @"INSERT INTO EmploymentHistoryy (JobTitle, Employer, City, StartDate, EndDate, Description, UserID)
                                                VALUES (@JobTitle, @Employer, @City, @StartDate, @EndDate, @Description, @UserID)";
                            using (SqlCommand cmd = new SqlCommand(insertQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@JobTitle", txtJobTitle.Text);
                                cmd.Parameters.AddWithValue("@Employer", txtEmployer.Text);
                                cmd.Parameters.AddWithValue("@City", txtCity.Text);
                                cmd.Parameters.AddWithValue("@StartDate", DateTime.Parse(txtStartDate.Text));
                                cmd.Parameters.AddWithValue("@EndDate", DateTime.Parse(txtEndDate.Text));
                                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Insert Skills
                    foreach (RepeaterItem item in rptSkillsEdit.Items)
                    {
                        TextBox txtSkillName = item.FindControl("txtSkillName") as TextBox;

                        if (!string.IsNullOrWhiteSpace(txtSkillName.Text))
                        {
                            string insertQuery = @"INSERT INTO Skills (SkillName, UserID) VALUES (@SkillName, @UserID)";
                            using (SqlCommand cmd = new SqlCommand(insertQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@SkillName", txtSkillName.Text);
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Insert Languages
                    foreach (RepeaterItem item in rptLanguagesEdit.Items)
                    {
                        TextBox txtLanguageName = item.FindControl("txtLanguageName") as TextBox;

                        if (!string.IsNullOrWhiteSpace(txtLanguageName.Text))
                        {
                            string insertQuery = @"INSERT INTO Languages (LanguageName, UserID) VALUES (@LanguageName, @UserID)";
                            using (SqlCommand cmd = new SqlCommand(insertQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@LanguageName", txtLanguageName.Text);
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Insert Certifications
                    foreach (RepeaterItem item in rptCertificationsEdit.Items)
                    {
                        TextBox txtCourseName = item.FindControl("txtCourseName") as TextBox;
                        TextBox txtInstitution = item.FindControl("txtInstitution") as TextBox;
                        TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                        TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;

                        if (!string.IsNullOrWhiteSpace(txtCourseName.Text) && !string.IsNullOrWhiteSpace(txtInstitution.Text))
                        {
                            string insertQuery = @"INSERT INTO Courses (CourseName, Institution, StartDate, EndDate, UserID)
                                                VALUES (@CourseName, @Institution, @StartDate, @EndDate, @UserID)";
                            using (SqlCommand cmd = new SqlCommand(insertQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text);
                                cmd.Parameters.AddWithValue("@Institution", txtInstitution.Text);
                                cmd.Parameters.AddWithValue("@StartDate", DateTime.Parse(txtStartDate.Text));
                                cmd.Parameters.AddWithValue("@EndDate", DateTime.Parse(txtEndDate.Text));
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Insert Internships
                    if (!string.IsNullOrWhiteSpace(txtInternJobTitle.Text) && !string.IsNullOrWhiteSpace(txtInternCompany.Text))
                    {
                        string insertQuery = @"INSERT INTO Internships (JobTitle, CompanyName, StartDate, EndDate, Description, UserID)
                                            VALUES (@JobTitle, @CompanyName, @StartDate, @EndDate, @Description, @UserID)";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@JobTitle", txtInternJobTitle.Text);
                            cmd.Parameters.AddWithValue("@CompanyName", txtInternCompany.Text);
                            cmd.Parameters.AddWithValue("@StartDate", DateTime.Parse(txtInternStartDate.Text));
                            cmd.Parameters.AddWithValue("@EndDate", DateTime.Parse(txtInternEndDate.Text));
                            cmd.Parameters.AddWithValue("@Description", txtInternDescription.Text);
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Insert Hobbies
                    foreach (RepeaterItem item in rptHobbiesEdit.Items)
                    {
                        TextBox txtHobbyName = item.FindControl("txtHobbyName") as TextBox;

                        if (!string.IsNullOrWhiteSpace(txtHobbyName.Text))
                        {
                            string insertQuery = @"INSERT INTO Hobbies (Name, UserID) VALUES (@Name, @UserID)";
                            using (SqlCommand cmd = new SqlCommand(insertQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Name", txtHobbyName.Text);
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Insert Custom Selection
                    foreach (RepeaterItem item in rptCustomSelectionEdit.Items)
                    {
                        TextBox txtTitle = item.FindControl("txtTitle") as TextBox;
                        TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                        TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;
                        TextBox txtDescription = item.FindControl("txtDescription") as TextBox;

                        if (!string.IsNullOrWhiteSpace(txtTitle.Text))
                        {
                            string insertQuery = @"INSERT INTO CustomSelection (Title, StartDate, EndDate, Description, UserID)
                                                VALUES (@Title, @StartDate, @EndDate, @Description, @UserID)";
                            using (SqlCommand cmd = new SqlCommand(insertQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                                cmd.Parameters.AddWithValue("@StartDate", DateTime.Parse(txtStartDate.Text));
                                cmd.Parameters.AddWithValue("@EndDate", DateTime.Parse(txtEndDate.Text));
                                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    transaction.Commit();
                    ViewState["IsEditMode"] = false;
                    LoadData();
                    LoadEmploymentHistory();
                    LoadEducation();
                    LoadSkills();
                    LoadLanguages();
                    LoadCertifications();
                    LoadInternships();
                    LoadHobbies();
                    LoadCustomSelection();
                    CheckEmploymentStatus();
                    ToggleEditMode();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Optionally log the error
                    Response.Write("Error: " + ex.Message);
                }
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            btnPrint.Enabled = false;
            try
            {
                string resumeFolder = Server.MapPath("~/Resumes/");
                if (!Directory.Exists(resumeFolder))
                {
                    Directory.CreateDirectory(resumeFolder);
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    using (Document pdfDoc = new Document(PageSize.A4, 20, 20, 20, 20))
                    {
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms);
                        pdfDoc.Open();

                        AddResumeContentToPdf(pdfDoc, writer);

                        pdfDoc.Close();

                        // Generate unique filename with UserID
                        string userId = Session["UserID"]?.ToString() ?? "Unknown";
                        string firstName = lblFirstName.Text.Replace(" ", "_");
                        string lastName = lblLastName.Text.Replace(" ", "_");
                        string fileName = $"Resume_{userId}_{firstName}_{lastName}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                        string filePath = Path.Combine(resumeFolder, fileName);

                        // Save PDF to ~/Resumes/
                        byte[] pdfBytes = ms.ToArray();
                        File.WriteAllBytes(filePath, pdfBytes);

                        // Insert record into ResumeFiles
                        string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                        using (SqlConnection conn = new SqlConnection(connStr))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("INSERT INTO ResumeFiles (UserID, FileName, FilePath, DateCreated) VALUES (@UserID, @FileName, @FilePath, @DateCreated)", conn))
                            {
                                cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Session["UserID"]));
                                cmd.Parameters.AddWithValue("@FileName", fileName);
                                cmd.Parameters.AddWithValue("@FilePath", $"~/Resumes/{fileName}");
                                cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        // Stream PDF to user
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", $"attachment;filename={fileName}");
                        Response.BinaryWrite(pdfBytes);
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error generating PDF: {ex.Message}');", true);
            }
            finally
            {
                btnPrint.Enabled = true;
            }
        }

        private void AddResumeContentToPdf(Document pdfDoc, PdfWriter writer)
        {
            // Define fonts with reduced sizes
            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20, BaseColor.WHITE); // Reduced from 28
            Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.WHITE); // Reduced from 20
            Font subHeaderFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, new BaseColor(236, 240, 241)); // Reduced from 14
            Font sectionFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, BaseColor.WHITE); // Reduced from 16
            Font contentFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, new BaseColor(44, 62, 80)); // Reduced from 12
            Font contactFont = FontFactory.GetFont(FontFactory.HELVETICA, 9, new BaseColor(44, 62, 80)); // Reduced from 11

            // Resume Title
            PdfPTable titleTable = new PdfPTable(1);
            titleTable.WidthPercentage = 100;
            PdfPCell titleCell = new PdfPCell(new Phrase("Resume", titleFont))
            {
                BackgroundColor = new BaseColor(231, 76, 60),
                Padding = 10, // Reduced from 20
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = Rectangle.NO_BORDER
            };
            titleTable.AddCell(titleCell);
            pdfDoc.Add(titleTable);

            // Header
            PdfPTable headerTable = new PdfPTable(1);
            headerTable.WidthPercentage = 100;
            headerTable.SpacingAfter = 8f; // Reduced from 15f
            PdfPCell headerCell = new PdfPCell()
            {
                BackgroundColor = new BaseColor(52, 73, 94),
                Padding = 15, // Reduced from 25
                Border = Rectangle.NO_BORDER
            };
            headerCell.AddElement(new Paragraph($"{lblFirstName.Text} {lblLastName.Text}", headerFont));
            headerCell.AddElement(new Paragraph(lblJobTitle.Text, subHeaderFont));
            headerTable.AddCell(headerCell);
            pdfDoc.Add(headerTable);

            // Contact Info
            PdfPTable contactTable = new PdfPTable(2);
            contactTable.WidthPercentage = 100;
            contactTable.SpacingAfter = 5f; // Reduced from 10f
            contactTable.AddCell(new PdfPCell(new Phrase($"Email: {lblEmail.Text}", contactFont)) { Border = Rectangle.NO_BORDER, Padding = 3 }); // Reduced from 5
            contactTable.AddCell(new PdfPCell(new Phrase($"Phone: {lblPhone.Text}", contactFont)) { Border = Rectangle.NO_BORDER, Padding = 3 });
            contactTable.AddCell(new PdfPCell(new Phrase($"Address: {lblAddress.Text}, {lblCity.Text}, {lblCountry.Text}, {lblPostalCode.Text}", contactFont)) { Border = Rectangle.NO_BORDER, Colspan = 2, Padding = 3 });
            contactTable.AddCell(new PdfPCell(new Phrase($"DOB: {lblDateOfBirth.Text}", contactFont)) { Border = Rectangle.NO_BORDER, Padding = 3 });
            contactTable.AddCell(new PdfPCell(new Phrase($"Nationality: {lblNationality.Text}", contactFont)) { Border = Rectangle.NO_BORDER, Padding = 3 });
            pdfDoc.Add(contactTable);

            // Professional Summary
            AddSectionToPdf(pdfDoc, "Professional Summary", lblProfessionalSummary.Text, new BaseColor(230, 126, 34), sectionFont, contentFont);

            // Education (Sentence Format)
            AddEducationToPdf(pdfDoc, sectionFont, contentFont);

            // Skills, Languages, Hobbies (Side by Side)
            AddSkillsLanguagesHobbiesToPdf(pdfDoc, sectionFont, contentFont);

            // Employment History
            AddEmploymentHistoryToPdf(pdfDoc, sectionFont, contentFont);

            // Certifications (Sentence Format)
            AddCertificationsToPdf(pdfDoc, sectionFont, contentFont);

            // Internships
            AddSectionToPdf(pdfDoc, "Internships", $"{lblInternJobTitle.Text} at {lblInternCompany.Text} ({FormatDate(lblInternStartDate.Text)} - {FormatDate(lblInternEndDate.Text)})\n{lblInternDescription.Text}", new BaseColor(241, 196, 15), sectionFont, contentFont);

            // Additional Information
            AddSectionToPdf(pdfDoc, "Additional Information", "", new BaseColor(46, 204, 113), sectionFont, contentFont, true);
        }

        private void AddSectionToPdf(Document pdfDoc, string title, string content, BaseColor bgColor, Font sectionFont, Font contentFont, bool isCustom = false)
        {
            PdfPTable sectionTable = new PdfPTable(1);
            sectionTable.WidthPercentage = 100;
            sectionTable.SpacingBefore = 5f; // Reduced from 10f
            PdfPCell headerCell = new PdfPCell(new Phrase(title, sectionFont))
            {
                BackgroundColor = bgColor,
                Padding = 8, // Reduced from 12
                Border = Rectangle.NO_BORDER
            };
            sectionTable.AddCell(headerCell);

            if (!isCustom && !string.IsNullOrWhiteSpace(content))
            {
                sectionTable.AddCell(new PdfPCell(new Phrase(content, contentFont))
                {
                    Padding = 10, // Reduced from 20
                    Border = Rectangle.NO_BORDER,
                    BackgroundColor = new BaseColor(245, 248, 250)
                });
            }
            else if (isCustom)
            {
                foreach (RepeaterItem item in rptCustomSelection.Items)
                {
                    Label lblTitle = item.FindControl("lblTitle") as Label;
                    Label lblStartDate = item.FindControl("lblStartDate") as Label;
                    Label lblEndDate = item.FindControl("lblEndDate") as Label;
                    Label lblDescription = item.FindControl("lblDescription") as Label;
                    if (lblTitle != null)
                    {
                        string text = $"{lblTitle.Text} ({FormatDate(lblStartDate.Text)} - {FormatDate(lblEndDate.Text)}): {lblDescription.Text}";
                        sectionTable.AddCell(new PdfPCell(new Phrase(text, contentFont))
                        {
                            Padding = 10, // Reduced from 20
                            Border = Rectangle.NO_BORDER,
                            BackgroundColor = new BaseColor(245, 248, 250)
                        });
                    }
                }
            }
            pdfDoc.Add(sectionTable);
        }

        private void AddEducationToPdf(Document pdfDoc, Font sectionFont, Font contentFont)
        {
            PdfPTable educationTable = new PdfPTable(1);
            educationTable.WidthPercentage = 100;
            educationTable.SpacingBefore = 5f; // Reduced from 10f

            educationTable.AddCell(new PdfPCell(new Phrase("Education", sectionFont))
            {
                BackgroundColor = new BaseColor(241, 196, 15),
                Padding = 8, // Reduced from 12
                Border = Rectangle.NO_BORDER
            });

            foreach (RepeaterItem item in rptEducation.Items)
            {
                Label lblDegree = item.FindControl("lblDegree") as Label;
                Label lblSchoolName = item.FindControl("lblSchoolName") as Label;
                Label lblCity = item.FindControl("lblCity") as Label;
                Label lblStartDate = item.FindControl("lblStartDate") as Label;
                Label lblEndDate = item.FindControl("lblEndDate") as Label;
                Label lblDescription = item.FindControl("lblDescription") as Label;

                string text = $"I completed my {lblDegree.Text} from {lblSchoolName.Text}, {lblCity.Text} between {lblStartDate.Text} and {lblEndDate.Text}. {lblDescription.Text}";
                educationTable.AddCell(new PdfPCell(new Phrase(text, contentFont))
                {
                    Padding = 10, // Reduced from 20
                    Border = Rectangle.NO_BORDER,
                    BackgroundColor = new BaseColor(245, 248, 250)
                });
            }
            pdfDoc.Add(educationTable);
        }

        private void AddSkillsLanguagesHobbiesToPdf(Document pdfDoc, Font sectionFont, Font contentFont)
        {
            PdfPTable skillsLangHobbyTable = new PdfPTable(3);
            skillsLangHobbyTable.WidthPercentage = 100;
            skillsLangHobbyTable.SpacingBefore = 5f; // Reduced from 10f

            // Skills
            PdfPCell skillsCell = new PdfPCell()
            {
                BackgroundColor = new BaseColor(52, 152, 219),
                Padding = 8, // Reduced from 12
                Border = Rectangle.NO_BORDER
            };
            skillsCell.AddElement(new Paragraph("Skills", sectionFont));
            string skills = string.Join(", ", rptSkills.Items.Cast<RepeaterItem>().Select(i => (i.FindControl("litSkill") as Literal)?.Text));
            skillsCell.AddElement(new Paragraph(skills, contentFont));
            skillsLangHobbyTable.AddCell(skillsCell);

            // Languages
            PdfPCell langCell = new PdfPCell()
            {
                BackgroundColor = new BaseColor(52, 152, 219),
                Padding = 8, // Reduced from 12
                Border = Rectangle.NO_BORDER
            };
            langCell.AddElement(new Paragraph("Languages", sectionFont));
            string languages = string.Join(", ", rptLanguages.Items.Cast<RepeaterItem>().Select(i => (i.FindControl("litLanguage") as Literal)?.Text));
            langCell.AddElement(new Paragraph(languages, contentFont));
            skillsLangHobbyTable.AddCell(langCell);

            // Hobbies
            PdfPCell hobbyCell = new PdfPCell()
            {
                BackgroundColor = new BaseColor(52, 152, 219),
                Padding = 8, // Reduced from 12
                Border = Rectangle.NO_BORDER
            };
            hobbyCell.AddElement(new Paragraph("Hobbies", sectionFont));
            string hobbies = string.Join(", ", rptHobbies.Items.Cast<RepeaterItem>().Select(i => (i.FindControl("litHobby") as Literal)?.Text));
            hobbyCell.AddElement(new Paragraph(hobbies, contentFont));
            skillsLangHobbyTable.AddCell(hobbyCell);

            pdfDoc.Add(skillsLangHobbyTable);
        }

        private void AddEmploymentHistoryToPdf(Document pdfDoc, Font sectionFont, Font contentFont)
        {
            PdfPTable employmentTable = new PdfPTable(1);
            employmentTable.WidthPercentage = 100;
            employmentTable.SpacingBefore = 5f; // Reduced from 10f

            employmentTable.AddCell(new PdfPCell(new Phrase("Employment History", sectionFont))
            {
                BackgroundColor = new BaseColor(155, 89, 182),
                Padding = 8, // Reduced from 12
                Border = Rectangle.NO_BORDER
            });

            if (pnlFresher.Visible)
            {
                employmentTable.AddCell(new PdfPCell(new Phrase("Fresher (No work experience)", contentFont))
                {
                    Padding = 10, // Reduced from 20
                    Border = Rectangle.NO_BORDER,
                    BackgroundColor = new BaseColor(245, 248, 250)
                });
            }
            else
            {
                foreach (RepeaterItem item in rptEmploymentHistory.Items)
                {
                    Label lblJobTitle = item.FindControl("lblJobTitle") as Label;
                    Label lblEmployer = item.FindControl("lblEmployer") as Label;
                    Label lblCity = item.FindControl("lblCity") as Label;
                    Label lblDescription = item.FindControl("lblDescription") as Label;

                    string text = $"{lblJobTitle.Text} at {lblEmployer.Text}, {lblCity.Text} ({FormatEmploymentDate(Eval("StartDate", item))} - {FormatEmploymentDate(Eval("EndDate", item))})\n{lblDescription.Text}";
                    employmentTable.AddCell(new PdfPCell(new Phrase(text, contentFont))
                    {
                        Padding = 10, // Reduced from 20
                        Border = Rectangle.NO_BORDER,
                        BackgroundColor = new BaseColor(245, 248, 250)
                    });
                }
            }
            pdfDoc.Add(employmentTable);
        }

        private void AddCertificationsToPdf(Document pdfDoc, Font sectionFont, Font contentFont)
        {
            PdfPTable certTable = new PdfPTable(1);
            certTable.WidthPercentage = 100;
            certTable.SpacingBefore = 5f; // Reduced from 10f

            certTable.AddCell(new PdfPCell(new Phrase("Certifications", sectionFont))
            {
                BackgroundColor = new BaseColor(46, 204, 113),
                Padding = 8, // Reduced from 12
                Border = Rectangle.NO_BORDER
            });

            foreach (RepeaterItem item in rptCertifications.Items)
            {
                Label lblCourseName = item.FindControl("lblCourseName") as Label;
                Label lblInstitution = item.FindControl("lblInstitution") as Label;
                Label lblStartDate = item.FindControl("lblStartDate") as Label;
                Label lblEndDate = item.FindControl("lblEndDate") as Label;

                string text = $"I completed the {lblCourseName.Text} certification from {lblInstitution.Text} between {lblStartDate.Text} and {lblEndDate.Text}.";
                certTable.AddCell(new PdfPCell(new Phrase(text, contentFont))
                {
                    Padding = 10, // Reduced from 20
                    Border = Rectangle.NO_BORDER,
                    BackgroundColor = new BaseColor(245, 248, 250)
                });
            }
            pdfDoc.Add(certTable);
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
        private object Eval(string fieldName, RepeaterItem item)
        {
            return DataBinder.Eval(item.DataItem, fieldName);
        }
        protected string FormatEmploymentDate(object dateValue)
        {
            if (dateValue == null || dateValue == DBNull.Value)
                return "Present";
            DateTime date;
            if (DateTime.TryParse(dateValue.ToString(), out date))
            {
                return date.ToString("MMM yyyy");
            }
            return "Present";
        }

        protected string FormatDate(string dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString) || dateString == "N/A")
                return "Present";
            if (DateTime.TryParse(dateString, out DateTime date))
                return date.ToString("MMM yyyy");
            return "Present";
        }
    }
}