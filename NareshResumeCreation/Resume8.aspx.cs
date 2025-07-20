using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.pdf.draw;

namespace NareshResumeCreation
{
    public partial class Resume8 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            if (!IsPostBack)
            {
                ViewState["Mode"] = "View";
                LoadAllData();
            }
            ToggleMode();
        }

        private void LoadAllData()
        {
            LoadPersonalData();
            LoadEmploymentHistory();
            LoadEducationHistory();
            LoadSkills();
            LoadInternships();
            LoadCertifications();
            LoadLanguages();
            LoadHobbies();
            LoadCustomSelection();
            CheckEmploymentStatus();
        }

        private void ToggleMode()
        {
            string mode = ViewState["Mode"]?.ToString() ?? "View";
            pnlView.Visible = mode == "View";
            pnlEdit.Visible = mode == "Edit";
            btnEdit.Visible = mode == "View";
            btnSave.Visible = mode == "Edit";
            btnCancel.Visible = mode == "Edit";
            btnDownload.Visible = true;
            btnBack.Visible = true;

            if (mode == "Edit")
            {
                LoadEditData();
            }
        }

        private void LoadPersonalData()
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
                    text8JobTitle.Text = reader["JobTitle"].ToString();
                    text8FirstName.Text = reader["FirstName"].ToString();
                    text8LastName.Text = reader["LastName"].ToString();
                    text8Email.Text = reader["Email"].ToString();
                    text8Phone.Text = reader["Phone"].ToString();
                    text8DateOfBirth.Text = Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                    text8Country.Text = reader["Country"].ToString();
                    text8State.Text = reader["State"].ToString();
                    text8City.Text = reader["City"].ToString();
                    text8Address.Text = reader["Address"].ToString();
                    text8PostalCode.Text = reader["PostalCode"].ToString();
                    text8Nationality.Text = reader["Nationality"].ToString();
                    text8PlaceOfBirth.Text = reader["PlaceOfBirth"].ToString();
                    text8ProfessionalSummary.Text = reader["PersonalSummary"].ToString();
                }
                else
                {
                    SetDefaultValues();
                }
                reader.Close();
            }
        }

        private void SetDefaultValues()
        {
            text8JobTitle.Text = "No Data";
            text8FirstName.Text = "No Data";
            text8LastName.Text = "No Data";
            text8Email.Text = "No Data";
            text8Phone.Text = "No Data";
            text8DateOfBirth.Text = "No Data";
            text8Country.Text = "No Data";
            text8State.Text = "No Data";
            text8City.Text = "No Data";
            text8Address.Text = "No Data";
            text8PostalCode.Text = "No Data";
            text8Nationality.Text = "No Data";
            text8PlaceOfBirth.Text = "No Data";
            text8ProfessionalSummary.Text = "No Data";
        }

        private void LoadEditData()
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
                    txtEditJobTitle.Text = reader["JobTitle"].ToString();
                    txtEditFirstName.Text = reader["FirstName"].ToString();
                    txtEditLastName.Text = reader["LastName"].ToString();
                    txtEditEmail.Text = reader["Email"].ToString();
                    txtEditPhone.Text = reader["Phone"].ToString();
                    txtEditDateOfBirth.Text = Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                    txtEditCountry.Text = reader["Country"].ToString();
                    txtEditState.Text = reader["State"].ToString();
                    txtEditCity.Text = reader["City"].ToString();
                    txtEditAddress.Text = reader["Address"].ToString();
                    txtEditPostalCode.Text = reader["PostalCode"].ToString();
                    txtEditNationality.Text = reader["Nationality"].ToString();
                    txtEditPlaceOfBirth.Text = reader["PlaceOfBirth"].ToString();
                    txtEditProfessionalSummary.Text = reader["PersonalSummary"].ToString();
                }
                reader.Close();
            }

            // Load repeatable sections
            LoadEditSkills();
            LoadEditLanguages();
            LoadEditHobbies();
            LoadEditEmploymentHistory();
            LoadEditEducation();
            LoadEditCertifications();
            LoadEditCustomSelection();
            LoadEditInternships();

            // Set fresher status
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT IsFresher FROM Userss WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                conn.Open();
                object result = cmd.ExecuteScalar();
                chkFresher.Checked = result != null && Convert.ToBoolean(result);
                pnlEditEmploymentHistory.Visible = !chkFresher.Checked;
            }
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
                    bool isFresher = result != null && Convert.ToBoolean(result);
                    pnl8Fresher.Visible = isFresher;
                    pnl8EmploymentHistory.Visible = !isFresher;
                }
            }
        }

        private void LoadEmploymentHistory()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"SELECT ID, JobTitle, Employer, City, StartDate, EndDate, Description 
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
                    rpt8EmploymentHistory.DataSource = dt;
                    rpt8EmploymentHistory.DataBind();
                }
            }
        }

        private void LoadEditEmploymentHistory()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"SELECT ID, JobTitle, Employer, City, StartDate, EndDate, Description 
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
                    rptEditEmploymentHistory.DataSource = dt;
                    rptEditEmploymentHistory.DataBind();
                }
            }
        }

        private void LoadEducationHistory()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT ID, Degree, SchoolName, City, StartDate, EndDate, Description FROM Education WHERE UserID = @UserID ORDER BY StartDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        rpt8Education.DataSource = dt;
                        rpt8Education.DataBind();
                    }
                    else
                    {
                        rpt8Education.DataSource = new[] { new { ID = 0, Degree = "No education found", SchoolName = "", City = "", StartDate = "N/A", EndDate = "N/A", Description = "" } };
                        rpt8Education.DataBind();
                    }
                }
            }
        }

        private void LoadEditEducation()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT ID, Degree, SchoolName, City, StartDate, EndDate, Description FROM Education WHERE UserID = @UserID ORDER BY StartDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        rptEditEducation.DataSource = dt;
                        rptEditEducation.DataBind();
                    }
                    else
                    {
                        rptEditEducation.DataSource = new[] { new { ID = 0, Degree = "No education found", SchoolName = "", City = "", StartDate = "N/A", EndDate = "N/A", Description = "" } };
                        rptEditEducation.DataBind();
                    }
                }
            }
        }

        private void LoadSkills()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT ID, SkillName FROM Skills WHERE UserID = @UserID ORDER BY ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        rpt8Skills.DataSource = dt;
                        rpt8Skills.DataBind();
                    }
                    else
                    {
                        rpt8Skills.DataSource = new[] { new { ID = 0, SkillName = "No skills found" } };
                        rpt8Skills.DataBind();
                    }
                }
            }
        }

        private void LoadEditSkills()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT ID, SkillName FROM Skills WHERE UserID = @UserID ORDER BY ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        rptEditSkills.DataSource = dt;
                        rptEditSkills.DataBind();
                    }
                    else
                    {
                        rptEditSkills.DataSource = new[] { new { ID = 0, SkillName = "No skills found" } };
                        rptEditSkills.DataBind();
                    }
                }
            }
        }

        private void LoadLanguages()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT ID, LanguageName FROM Languages WHERE UserID = @UserID ORDER BY ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        rpt8Languages.DataSource = dt;
                        rpt8Languages.DataBind();
                    }
                    else
                    {
                        rpt8Languages.DataSource = new[] { new { ID = 0, LanguageName = "No languages found" } };
                        rpt8Languages.DataBind();
                    }
                }
            }
        }

        private void LoadEditLanguages()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT ID, LanguageName FROM Languages WHERE UserID = @UserID ORDER BY ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        rptEditLanguages.DataSource = dt;
                        rptEditLanguages.DataBind();
                    }
                    else
                    {
                        rptEditLanguages.DataSource = new[] { new { ID = 0, LanguageName = "No languages found" } };
                        rptEditLanguages.DataBind();
                    }
                }
            }
        }

        private void LoadHobbies()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT ID, Name FROM Hobbies WHERE UserID = @UserID ORDER BY ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        rpt8Hobbies.DataSource = dt;
                        rpt8Hobbies.DataBind();
                    }
                    else
                    {
                        rpt8Hobbies.DataSource = new[] { new { ID = 0, Name = "No hobbies found" } };
                        rpt8Hobbies.DataBind();
                    }
                }
            }
        }

        private void LoadEditHobbies()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT ID, Name FROM Hobbies WHERE UserID = @UserID ORDER BY ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        rptEditHobbies.DataSource = dt;
                        rptEditHobbies.DataBind();
                    }
                    else
                    {
                        rptEditHobbies.DataSource = new[] { new { ID = 0, Name = "No hobbies found" } };
                        rptEditHobbies.DataBind();
                    }
                }
            }
        }

        private void LoadCertifications()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT ID, CourseName, Institution, StartDate, EndDate FROM Courses WHERE UserID = @UserID ORDER BY StartDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        rpt8Certifications.DataSource = dt;
                        rpt8Certifications.DataBind();
                    }
                    else
                    {
                        rpt8Certifications.DataSource = new[] { new { ID = 0, CourseName = "No certifications found", Institution = "", StartDate = "N/A", EndDate = "N/A" } };
                        rpt8Certifications.DataBind();
                    }
                }
            }
        }

        private void LoadEditCertifications()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT ID, CourseName, Institution, StartDate, EndDate FROM Courses WHERE UserID = @UserID ORDER BY StartDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        rptEditCertifications.DataSource = dt;
                        rptEditCertifications.DataBind();
                    }
                    else
                    {
                        rptEditCertifications.DataSource = new[] { new { ID = 0, CourseName = "No certifications found", Institution = "", StartDate = "N/A", EndDate = "N/A" } };
                        rptEditCertifications.DataBind();
                    }
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
                        text8InternCompany.Text = reader["CompanyName"].ToString();
                        text8InternJobTitle.Text = reader["JobTitle"].ToString();
                        text8InternStartDate.Text = Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy");
                        text8InternEndDate.Text = Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy");
                        text8InternDescription.Text = reader["Description"].ToString();
                    }
                    else
                    {
                        text8InternCompany.Text = "N/A";
                        text8InternJobTitle.Text = "N/A";
                        text8InternStartDate.Text = "N/A";
                        text8InternEndDate.Text = "N/A";
                        text8InternDescription.Text = "No internship data available";
                    }
                    reader.Close();
                }
            }
        }

        private void LoadEditInternships()
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
                        txtEditInternCompany.Text = reader["CompanyName"].ToString();
                        txtEditInternJobTitle.Text = reader["JobTitle"].ToString();
                        txtEditInternStartDate.Text = Convert.ToDateTime(reader["StartDate"]).ToString("yyyy-MM-dd");
                        txtEditInternEndDate.Text = Convert.ToDateTime(reader["EndDate"]).ToString("yyyy-MM-dd");
                        txtEditInternDescription.Text = reader["Description"].ToString();
                    }
                    else
                    {
                        txtEditInternCompany.Text = "";
                        txtEditInternJobTitle.Text = "";
                        txtEditInternStartDate.Text = "";
                        txtEditInternEndDate.Text = "";
                        txtEditInternDescription.Text = "";
                    }
                    reader.Close();
                }
            }
        }

        private void LoadCustomSelection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT ID, Title, StartDate, EndDate, Description FROM CustomSelection WHERE UserID = @UserID ORDER BY StartDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        rpt8CustomSelection.DataSource = dt;
                        rpt8CustomSelection.DataBind();
                    }
                    else
                    {
                        rpt8CustomSelection.DataSource = new[] { new { ID = 0, Title = "No additional info", StartDate = "N/A", EndDate = "N/A", Description = "" } };
                        rpt8CustomSelection.DataBind();
                    }
                }
            }
        }

        private void LoadEditCustomSelection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT ID, Title, StartDate, EndDate, Description FROM CustomSelection WHERE UserID = @UserID ORDER BY StartDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        rptEditCustomSelection.DataSource = dt;
                        rptEditCustomSelection.DataBind();
                    }
                    else
                    {
                        rptEditCustomSelection.DataSource = new[] { new { ID = 0, Title = "No additional info", StartDate = "N/A", EndDate = "N/A", Description = "" } };
                        rptEditCustomSelection.DataBind();
                    }
                }
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ViewState["Mode"] = "Edit";
            ToggleMode();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["Mode"] = "View";
            ToggleMode();
            LoadAllData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SavePersonalData();
            SaveSkills();
            SaveLanguages();
            SaveHobbies();
            SaveEmploymentHistory();
            SaveEducation();
            SaveCertifications();
            SaveInternships();
            SaveCustomSelection();
            SaveFresherStatus();

            ViewState["Mode"] = "View";
            ToggleMode();
            LoadAllData();
        }

        private void SavePersonalData()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"INSERT INTO PersonalDetails (UserID, JobTitle, FirstName, LastName, Email, Phone, DateOfBirth, Country, State, City, Address, PostalCode, Nationality, PlaceOfBirth, PersonalSummary) 
                                VALUES (@UserID, @JobTitle, @FirstName, @LastName, @Email, @Phone, @DateOfBirth, @Country, @State, @City, @Address, @PostalCode, @Nationality, @PlaceOfBirth, @PersonalSummary)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.Parameters.AddWithValue("@JobTitle", txtEditJobTitle.Text);
                    cmd.Parameters.AddWithValue("@FirstName", txtEditFirstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtEditLastName.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEditEmail.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtEditPhone.Text);
                    cmd.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(txtEditDateOfBirth.Text));
                    cmd.Parameters.AddWithValue("@Country", txtEditCountry.Text);
                    cmd.Parameters.AddWithValue("@State", txtEditState.Text);
                    cmd.Parameters.AddWithValue("@City", txtEditCity.Text);
                    cmd.Parameters.AddWithValue("@Address", txtEditAddress.Text);
                    cmd.Parameters.AddWithValue("@PostalCode", txtEditPostalCode.Text);
                    cmd.Parameters.AddWithValue("@Nationality", txtEditNationality.Text);
                    cmd.Parameters.AddWithValue("@PlaceOfBirth", txtEditPlaceOfBirth.Text);
                    cmd.Parameters.AddWithValue("@PersonalSummary", txtEditProfessionalSummary.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void SaveSkills()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Delete existing skills
                string deleteQuery = "DELETE FROM Skills WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.ExecuteNonQuery();
                }

                // Save updated skills
                foreach (RepeaterItem item in rptEditSkills.Items)
                {
                    TextBox txtSkillName = item.FindControl("txtSkillName") as TextBox;
                    if (txtSkillName != null && !string.IsNullOrWhiteSpace(txtSkillName.Text))
                    {
                        string insertQuery = "INSERT INTO Skills (UserID, SkillName) VALUES (@UserID, @SkillName)";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.Parameters.AddWithValue("@SkillName", txtSkillName.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                // Save new skill
                if (!string.IsNullOrWhiteSpace(txtNewSkill.Text))
                {
                    string insertQuery = "INSERT INTO Skills (UserID, SkillName) VALUES (@UserID, @SkillName)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@SkillName", txtNewSkill.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        protected void btnAddSkill_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewSkill.Text))
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "INSERT INTO Skills (UserID, SkillName) VALUES (@UserID, @SkillName)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@SkillName", txtNewSkill.Text);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                txtNewSkill.Text = "";
                LoadEditSkills();
            }
        }

        protected void Skill_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "DELETE FROM Skills WHERE ID = @ID AND UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(e.CommandArgument));
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditSkills();
            }
        }

        private void SaveLanguages()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Delete existing languages
                string deleteQuery = "DELETE FROM Languages WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.ExecuteNonQuery();
                }

                // Save updated languages
                foreach (RepeaterItem item in rptEditLanguages.Items)
                {
                    TextBox txtLanguageName = item.FindControl("txtLanguageName") as TextBox;
                    if (txtLanguageName != null && !string.IsNullOrWhiteSpace(txtLanguageName.Text))
                    {
                        string insertQuery = "INSERT INTO Languages (UserID, LanguageName) VALUES (@UserID, @LanguageName)";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.Parameters.AddWithValue("@LanguageName", txtLanguageName.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                // Save new language
                if (!string.IsNullOrWhiteSpace(txtNewLanguage.Text))
                {
                    string insertQuery = "INSERT INTO Languages (UserID, LanguageName) VALUES (@UserID, @LanguageName)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@LanguageName", txtNewLanguage.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        protected void btnAddLanguage_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewLanguage.Text))
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "INSERT INTO Languages (UserID, LanguageName) VALUES (@UserID, @LanguageName)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@LanguageName", txtNewLanguage.Text);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                txtNewLanguage.Text = "";
                LoadEditLanguages();
            }
        }

        protected void Language_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "DELETE FROM Languages WHERE ID = @ID AND UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(e.CommandArgument));
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditLanguages();
            }
        }

        private void SaveHobbies()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Delete existing hobbies
                string deleteQuery = "DELETE FROM Hobbies WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.ExecuteNonQuery();
                }

                // Save updated hobbies
                foreach (RepeaterItem item in rptEditHobbies.Items)
                {
                    TextBox txtHobbyName = item.FindControl("txtHobbyName") as TextBox;
                    if (txtHobbyName != null && !string.IsNullOrWhiteSpace(txtHobbyName.Text))
                    {
                        string insertQuery = "INSERT INTO Hobbies (UserID, Name) VALUES (@UserID, @Name)";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.Parameters.AddWithValue("@Name", txtHobbyName.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                // Save new hobby
                if (!string.IsNullOrWhiteSpace(txtNewHobby.Text))
                {
                    string insertQuery = "INSERT INTO Hobbies (UserID, Name) VALUES (@UserID, @Name)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@Name", txtNewHobby.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        protected void btnAddHobby_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewHobby.Text))
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "INSERT INTO Hobbies (UserID, Name) VALUES (@UserID, @Name)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@Name", txtNewHobby.Text);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                txtNewHobby.Text = "";
                LoadEditHobbies();
            }
        }

        protected void Hobby_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "DELETE FROM Hobbies WHERE ID = @ID AND UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(e.CommandArgument));
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditHobbies();
            }
        }

        private void SaveEmploymentHistory()
        {
            if (chkFresher.Checked) return;

            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Delete existing employment history
                string deleteQuery = "DELETE FROM EmploymentHistoryy WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.ExecuteNonQuery();
                }

                // Save updated employment history
                foreach (RepeaterItem item in rptEditEmploymentHistory.Items)
                {
                    TextBox txtJobTitle = item.FindControl("txtJobTitle") as TextBox;
                    TextBox txtEmployer = item.FindControl("txtEmployer") as TextBox;
                    TextBox txtCity = item.FindControl("txtCity") as TextBox;
                    TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                    TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;
                    TextBox txtDescription = item.FindControl("txtDescription") as TextBox;

                    if (txtJobTitle != null && !string.IsNullOrWhiteSpace(txtJobTitle.Text))
                    {
                        string insertQuery = @"INSERT INTO EmploymentHistoryy (UserID, JobTitle, Employer, City, StartDate, EndDate, Description) 
                                              VALUES (@UserID, @JobTitle, @Employer, @City, @StartDate, @EndDate, @Description)";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.Parameters.AddWithValue("@JobTitle", txtJobTitle.Text);
                            cmd.Parameters.AddWithValue("@Employer", txtEmployer.Text);
                            cmd.Parameters.AddWithValue("@City", txtCity.Text);
                            cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtStartDate.Text));
                            cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrWhiteSpace(txtEndDate.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtEndDate.Text));
                            cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                // Save new employment
                if (!string.IsNullOrWhiteSpace(txtNewJobTitle.Text))
                {
                    string insertQuery = @"INSERT INTO EmploymentHistoryy (UserID, JobTitle, Employer, City, StartDate, EndDate, Description) 
                                          VALUES (@UserID, @JobTitle, @Employer, @City, @StartDate, @EndDate, @Description)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@JobTitle", txtNewJobTitle.Text);
                        cmd.Parameters.AddWithValue("@Employer", txtNewEmployer.Text);
                        cmd.Parameters.AddWithValue("@City", txtNewCity.Text);
                        cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtNewStartDate.Text));
                        cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrWhiteSpace(txtNewEndDate.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtNewEndDate.Text));
                        cmd.Parameters.AddWithValue("@Description", txtNewDescription.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        protected void btnAddEmployment_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewJobTitle.Text))
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"INSERT INTO EmploymentHistoryy (UserID, JobTitle, Employer, City, StartDate, EndDate, Description) 
                                    VALUES (@UserID, @JobTitle, @Employer, @City, @StartDate, @EndDate, @Description)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@JobTitle", txtNewJobTitle.Text);
                        cmd.Parameters.AddWithValue("@Employer", txtNewEmployer.Text);
                        cmd.Parameters.AddWithValue("@City", txtNewCity.Text);
                        cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtNewStartDate.Text));
                        cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrWhiteSpace(txtNewEndDate.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtNewEndDate.Text));
                        cmd.Parameters.AddWithValue("@Description", txtNewDescription.Text);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                txtNewJobTitle.Text = "";
                txtNewEmployer.Text = "";
                txtNewCity.Text = "";
                txtNewStartDate.Text = "";
                txtNewEndDate.Text = "";
                txtNewDescription.Text = "";
                LoadEditEmploymentHistory();
            }
        }

        protected void Employment_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "DELETE FROM EmploymentHistoryy WHERE ID = @ID AND UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(e.CommandArgument));
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditEmploymentHistory();
            }
        }

        private void SaveEducation()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Delete existing education
                string deleteQuery = "DELETE FROM Education WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.ExecuteNonQuery();
                }

                // Save updated education
                foreach (RepeaterItem item in rptEditEducation.Items)
                {
                    TextBox txtDegree = item.FindControl("txtDegree") as TextBox;
                    TextBox txtSchoolName = item.FindControl("txtSchoolName") as TextBox;
                    TextBox txtCity = item.FindControl("txtCity") as TextBox;
                    TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                    TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;
                    TextBox txtDescription = item.FindControl("txtDescription") as TextBox;

                    if (txtDegree != null && !string.IsNullOrWhiteSpace(txtDegree.Text))
                    {
                        string insertQuery = @"INSERT INTO Education (UserID, Degree, SchoolName, City, StartDate, EndDate, Description) 
                                              VALUES (@UserID, @Degree, @SchoolName, @City, @StartDate, @EndDate, @Description)";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.Parameters.AddWithValue("@Degree", txtDegree.Text);
                            cmd.Parameters.AddWithValue("@SchoolName", txtSchoolName.Text);
                            cmd.Parameters.AddWithValue("@City", txtCity.Text);
                            cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtStartDate.Text));
                            cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrWhiteSpace(txtEndDate.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtEndDate.Text));
                            cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                // Save new education
                if (!string.IsNullOrWhiteSpace(txtNewDegree.Text))
                {
                    string insertQuery = @"INSERT INTO Education (UserID, Degree, SchoolName, City, StartDate, EndDate, Description) 
                                          VALUES (@UserID, @Degree, @SchoolName, @City, @StartDate, @EndDate, @Description)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@Degree", txtNewDegree.Text);
                        cmd.Parameters.AddWithValue("@SchoolName", txtNewSchoolName.Text);
                        cmd.Parameters.AddWithValue("@City", txtNewEducationCity.Text);
                        cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtNewEducationStartDate.Text));
                        cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrWhiteSpace(txtNewEducationEndDate.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtNewEducationEndDate.Text));
                        cmd.Parameters.AddWithValue("@Description", txtNewEducationDescription.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        protected void btnAddEducation_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewDegree.Text))
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"INSERT INTO Education (UserID, Degree, SchoolName, City, StartDate, EndDate, Description) 
                                    VALUES (@UserID, @Degree, @SchoolName, @City, @StartDate, @EndDate, @Description)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@Degree", txtNewDegree.Text);
                        cmd.Parameters.AddWithValue("@SchoolName", txtNewSchoolName.Text);
                        cmd.Parameters.AddWithValue("@City", txtNewEducationCity.Text);
                        cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtNewEducationStartDate.Text));
                        cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrWhiteSpace(txtNewEducationEndDate.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtNewEducationEndDate.Text));
                        cmd.Parameters.AddWithValue("@Description", txtNewEducationDescription.Text);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                txtNewDegree.Text = "";
                txtNewSchoolName.Text = "";
                txtNewEducationCity.Text = "";
                txtNewEducationStartDate.Text = "";
                txtNewEducationEndDate.Text = "";
                txtNewEducationDescription.Text = "";
                LoadEditEducation();
            }
        }

        protected void Education_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "DELETE FROM Education WHERE ID = @ID AND UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(e.CommandArgument));
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditEducation();
            }
        }

        private void SaveCertifications()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Delete existing certifications
                string deleteQuery = "DELETE FROM Courses WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.ExecuteNonQuery();
                }

                // Save updated certifications
                foreach (RepeaterItem item in rptEditCertifications.Items)
                {
                    TextBox txtCourseName = item.FindControl("txtCourseName") as TextBox;
                    TextBox txtInstitution = item.FindControl("txtInstitution") as TextBox;
                    TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                    TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;

                    if (txtCourseName != null && !string.IsNullOrWhiteSpace(txtCourseName.Text))
                    {
                        string insertQuery = @"INSERT INTO Courses (UserID, CourseName, Institution, StartDate, EndDate) 
                                              VALUES (@UserID, @CourseName, @Institution, @StartDate, @EndDate)";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text);
                            cmd.Parameters.AddWithValue("@Institution", txtInstitution.Text);
                            cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtStartDate.Text));
                            cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrWhiteSpace(txtEndDate.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtEndDate.Text));
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                // Save new certification
                if (!string.IsNullOrWhiteSpace(txtNewCourseName.Text))
                {
                    string insertQuery = @"INSERT INTO Courses (UserID, CourseName, Institution, StartDate, EndDate) 
                                          VALUES (@UserID, @CourseName, @Institution, @StartDate, @EndDate)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@CourseName", txtNewCourseName.Text);
                        cmd.Parameters.AddWithValue("@Institution", txtNewInstitution.Text);
                        cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtNewCertStartDate.Text));
                        cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrWhiteSpace(txtNewCertEndDate.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtNewCertEndDate.Text));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        protected void btnAddCertification_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewCourseName.Text))
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"INSERT INTO Courses (UserID, CourseName, Institution, StartDate, EndDate) 
                                    VALUES (@UserID, @CourseName, @Institution, @StartDate, @EndDate)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@CourseName", txtNewCourseName.Text);
                        cmd.Parameters.AddWithValue("@Institution", txtNewInstitution.Text);
                        cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtNewCertStartDate.Text));
                        cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrWhiteSpace(txtNewCertEndDate.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtNewCertEndDate.Text));
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                txtNewCourseName.Text = "";
                txtNewInstitution.Text = "";
                txtNewCertStartDate.Text = "";
                txtNewCertEndDate.Text = "";
                LoadEditCertifications();
            }
        }

        protected void Certification_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "DELETE FROM Courses WHERE ID = @ID AND UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(e.CommandArgument));
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditCertifications();
            }
        }

        private void SaveInternships()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Delete existing internships
                string deleteQuery = "DELETE FROM Internships WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.ExecuteNonQuery();
                }

                // Save new internship
                if (!string.IsNullOrWhiteSpace(txtEditInternJobTitle.Text))
                {
                    string insertQuery = @"INSERT INTO Internships (UserID, JobTitle, CompanyName, StartDate, EndDate, Description) 
                                          VALUES (@UserID, @JobTitle, @CompanyName, @StartDate, @EndDate, @Description)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@JobTitle", txtEditInternJobTitle.Text);
                        cmd.Parameters.AddWithValue("@CompanyName", txtEditInternCompany.Text);
                        cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtEditInternStartDate.Text));
                        cmd.Parameters.AddWithValue("@EndDate", Convert.ToDateTime(txtEditInternEndDate.Text));
                        cmd.Parameters.AddWithValue("@Description", txtEditInternDescription.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private void SaveCustomSelection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Delete existing custom selections
                string deleteQuery = "DELETE FROM CustomSelection WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.ExecuteNonQuery();
                }

                // Save updated custom selections
                foreach (RepeaterItem item in rptEditCustomSelection.Items)
                {
                    TextBox txtTitle = item.FindControl("txtTitle") as TextBox;
                    TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                    TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;
                    TextBox txtDescription = item.FindControl("txtDescription") as TextBox;

                    if (txtTitle != null && !string.IsNullOrWhiteSpace(txtTitle.Text))
                    {
                        string insertQuery = @"INSERT INTO CustomSelection (UserID, Title, StartDate, EndDate, Description) 
                                              VALUES (@UserID, @Title, @StartDate, @EndDate, @Description)";
                        using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                            cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtStartDate.Text));
                            cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrWhiteSpace(txtEndDate.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtEndDate.Text));
                            cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                // Save new custom section
                if (!string.IsNullOrWhiteSpace(txtNewCustomTitle.Text))
                {
                    string insertQuery = @"INSERT INTO CustomSelection (UserID, Title, StartDate, EndDate, Description) 
                                          VALUES (@UserID, @Title, @StartDate, @EndDate, @Description)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@Title", txtNewCustomTitle.Text);
                        cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtNewCustomStartDate.Text));
                        cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrWhiteSpace(txtNewCustomEndDate.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtNewCustomEndDate.Text));
                        cmd.Parameters.AddWithValue("@Description", txtNewCustomDescription.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        protected void btnAddCustom_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewCustomTitle.Text))
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"INSERT INTO CustomSelection (UserID, Title, StartDate, EndDate, Description) 
                                    VALUES (@UserID, @Title, @StartDate, @EndDate, @Description)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@Title", txtNewCustomTitle.Text);
                        cmd.Parameters.AddWithValue("@StartDate", Convert.ToDateTime(txtNewCustomStartDate.Text));
                        cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrWhiteSpace(txtNewCustomEndDate.Text) ? (object)DBNull.Value : Convert.ToDateTime(txtNewCustomEndDate.Text));
                        cmd.Parameters.AddWithValue("@Description", txtNewCustomDescription.Text);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                txtNewCustomTitle.Text = "";
                txtNewCustomStartDate.Text = "";
                txtNewCustomEndDate.Text = "";
                txtNewCustomDescription.Text = "";
                LoadEditCustomSelection();
            }
        }

        protected void CustomSelection_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "DELETE FROM CustomSelection WHERE ID = @ID AND UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(e.CommandArgument));
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditCustomSelection();
            }
        }

        private void SaveFresherStatus()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "UPDATE Userss SET IsFresher = @IsFresher WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IsFresher", chkFresher.Checked);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        protected void chkFresher_CheckedChanged(object sender, EventArgs e)
        {
            pnlEditEmploymentHistory.Visible = !chkFresher.Checked;
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            btnDownload.Enabled = false;
            try
            {
                string resumeFolder = Server.MapPath("~/Resumes/");
                if (!Directory.Exists(resumeFolder))
                {
                    Directory.CreateDirectory(resumeFolder);
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    using (Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25))
                    {
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms);
                        pdfDoc.Open();

                        AddResumeContentToPdf(pdfDoc);

                        pdfDoc.Close();

                        // Generate unique filename with UserID
                        string userId = Session["UserID"]?.ToString() ?? "Unknown";
                        string firstName = text8FirstName.Text.Replace(" ", "_");
                        string lastName = text8LastName.Text.Replace(" ", "_");
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
                btnDownload.Enabled = true;
            }
        }

        private void AddResumeContentToPdf(Document pdfDoc)
        {
            try
            {
                // Define fonts to match HTML
                Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20);
                Font jobTitleFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                Font contactFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                Font sectionFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                Font contentFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

                // Line separator
                LineSeparator line = new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_LEFT, -1);

                // Two-column layout
                PdfPTable mainTable = new PdfPTable(2);
                mainTable.WidthPercentage = 100;
                mainTable.SetWidths(new float[] { 30f, 70f });

                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;

                // Left Column
                PdfPCell leftCell = new PdfPCell();
                leftCell.Border = Rectangle.NO_BORDER;
                leftCell.PaddingRight = 10f;

                // Header with Personal Info
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM PersonalDetails WHERE UserID = @UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string fullName = $"{reader["FirstName"]} {reader["LastName"]}".Trim();
                                leftCell.AddElement(new Paragraph(fullName, headerFont));

                                string jobTitle = reader["JobTitle"]?.ToString() ?? "";
                                leftCell.AddElement(new Paragraph(jobTitle, jobTitleFont) { SpacingBefore = 5 });

                                string email = reader["Email"]?.ToString() ?? "";
                                leftCell.AddElement(new Paragraph($"Email: {email}", contactFont) { SpacingBefore = 5 });

                                string phone = reader["Phone"]?.ToString() ?? "";
                                leftCell.AddElement(new Paragraph($"Phone: {phone}", contactFont) { SpacingBefore = 5 });

                                string address = $"{reader["Address"]?.ToString() ?? ""}, {reader["City"]?.ToString() ?? ""}, {reader["State"]?.ToString() ?? ""}, {reader["PostalCode"]?.ToString() ?? ""}, {reader["Country"]?.ToString() ?? ""}";
                                leftCell.AddElement(new Paragraph($"Address: {address}", contactFont) { SpacingBefore = 5 });

                                string dob = reader["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd") : "";
                                leftCell.AddElement(new Paragraph($"Date of Birth: {dob}", contactFont) { SpacingBefore = 5 });

                                string placeOfBirth = reader["PlaceOfBirth"]?.ToString() ?? "";
                                leftCell.AddElement(new Paragraph($"Place of Birth: {placeOfBirth}", contactFont) { SpacingBefore = 5 });

                                string nationality = reader["Nationality"]?.ToString() ?? "";
                                Paragraph nationalityPara = new Paragraph($"Nationality: {nationality}", contactFont) { SpacingBefore = 5 };
                                nationalityPara.SpacingAfter = 10f;
                                leftCell.AddElement(nationalityPara);
                                leftCell.AddElement(line);
                            }
                        }
                    }
                }

                // Skills
                leftCell.AddElement(new Paragraph("Skills", sectionFont) { SpacingBefore = 10 });
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT SkillName FROM Skills WHERE UserID = @UserID ORDER BY ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string skill = reader["SkillName"]?.ToString();
                                    if (!string.IsNullOrWhiteSpace(skill))
                                    {
                                        leftCell.AddElement(new Paragraph($"• {skill}", contentFont) { SpacingBefore = 5 });
                                    }
                                }
                            }
                        }
                    }
                }
                leftCell.AddElement(new Paragraph("") { SpacingAfter = 10f });
                leftCell.AddElement(line);

                // Languages
                leftCell.AddElement(new Paragraph("Languages", sectionFont) { SpacingBefore = 10 });
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT LanguageName FROM Languages WHERE UserID = @UserID ORDER BY ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string language = reader["LanguageName"]?.ToString();
                                    if (!string.IsNullOrWhiteSpace(language))
                                    {
                                        leftCell.AddElement(new Paragraph($"• {language}", contentFont) { SpacingBefore = 5 });
                                    }
                                }
                            }
                        }
                    }
                }
                leftCell.AddElement(new Paragraph("") { SpacingAfter = 10f });
                leftCell.AddElement(line);

                // Hobbies
                leftCell.AddElement(new Paragraph("Hobbies", sectionFont) { SpacingBefore = 10 });
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT Name FROM Hobbies WHERE UserID = @UserID ORDER BY ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string hobby = reader["Name"]?.ToString();
                                    if (!string.IsNullOrWhiteSpace(hobby))
                                    {
                                        leftCell.AddElement(new Paragraph($"• {hobby}", contentFont) { SpacingBefore = 5 });
                                    }
                                }
                            }
                        }
                    }
                }

                mainTable.AddCell(leftCell);

                // Right Column
                PdfPCell rightCell = new PdfPCell();
                rightCell.Border = Rectangle.NO_BORDER;
                rightCell.PaddingLeft = 10f;

                // Professional Summary
                rightCell.AddElement(new Paragraph("Professional Summary", sectionFont));
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT PersonalSummary FROM PersonalDetails WHERE UserID = @UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        string summary = cmd.ExecuteScalar()?.ToString() ?? "";
                        Paragraph summaryPara = new Paragraph(summary, contentFont) { SpacingBefore = 5 };
                        summaryPara.SpacingAfter = 10f;
                        rightCell.AddElement(summaryPara);
                        rightCell.AddElement(line);
                    }
                }

                // Employment History
                rightCell.AddElement(new Paragraph("Employment History", sectionFont) { SpacingBefore = 10 });
                bool isFresher = false;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT IsFresher FROM Userss WHERE UserID = @UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        object result = cmd.ExecuteScalar();
                        isFresher = result != null && Convert.ToBoolean(result);
                    }
                }

                if (isFresher)
                {
                    Paragraph fresherPara = new Paragraph("I am a fresher with no work experience.", contentFont) { SpacingBefore = 5 };
                    fresherPara.SpacingAfter = 10f;
                    rightCell.AddElement(fresherPara);
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT JobTitle, Employer, City, StartDate, EndDate, Description FROM EmploymentHistory WHERE UserID = @UserID ORDER BY StartDate DESC", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string jobTitle = reader["JobTitle"]?.ToString() ?? "";
                                    string employer = reader["Employer"]?.ToString() ?? "";
                                    string city = reader["City"]?.ToString() ?? "";
                                    string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                    string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                    string description = reader["Description"]?.ToString() ?? "";

                                    string text = $"I worked as {jobTitle} at {employer}, {city} between {startDate} and {endDate}. {description}";
                                    Paragraph empPara = new Paragraph(text, contentFont) { SpacingBefore = 5 };
                                    rightCell.AddElement(empPara);
                                }
                            }
                        }
                    }
                }
                rightCell.AddElement(line);

                // Education
                rightCell.AddElement(new Paragraph("Education", sectionFont) { SpacingBefore = 10 });
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT Degree, SchoolName, City, StartDate, EndDate, Description FROM Education WHERE UserID = @UserID ORDER BY StartDate DESC", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string degree = reader["Degree"]?.ToString() ?? "";
                                string schoolName = reader["SchoolName"]?.ToString() ?? "";
                                string city = reader["City"]?.ToString() ?? "";
                                string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                string description = reader["Description"]?.ToString() ?? "";

                                string text = $"I completed my {degree} from {schoolName}, {city} between {startDate} and {endDate}. {description}";
                                Paragraph eduPara = new Paragraph(text, contentFont) { SpacingBefore = 5 };
                                rightCell.AddElement(eduPara);
                            }
                        }
                    }
                }
                rightCell.AddElement(line);

                // Certifications
                rightCell.AddElement(new Paragraph("Certifications", sectionFont) { SpacingBefore = 10 });
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CourseName, Institution, StartDate, EndDate FROM Courses WHERE UserID = @UserID ORDER BY StartDate DESC", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string courseName = reader["CourseName"]?.ToString() ?? "";
                                string institution = reader["Institution"]?.ToString() ?? "";
                                string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";

                                string text = $"I completed {courseName} from {institution} between {startDate} and {endDate}.";
                                Paragraph certPara = new Paragraph(text, contentFont) { SpacingBefore = 5 };
                                rightCell.AddElement(certPara);
                            }
                        }
                    }
                }
                rightCell.AddElement(line);

                // Internships
                rightCell.AddElement(new Paragraph("Internships", sectionFont) { SpacingBefore = 10 });
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 CompanyName, JobTitle, StartDate, EndDate, Description FROM Internships WHERE UserID = @UserID ORDER BY ID DESC", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string company = reader["CompanyName"]?.ToString() ?? "";
                                string jobTitle = reader["JobTitle"]?.ToString() ?? "";
                                string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                string description = reader["Description"]?.ToString() ?? "";

                                string text = $"I worked as {jobTitle} at {company} between {startDate} and {endDate}. {description}";
                                Paragraph internPara = new Paragraph(text, contentFont) { SpacingBefore = 5, SpacingAfter = 10f };
                                rightCell.AddElement(internPara);
                            }
                            else
                            {
                                Paragraph noInternPara = new Paragraph("No internship data available", contentFont) { SpacingBefore = 5, SpacingAfter = 10f };
                                rightCell.AddElement(noInternPara);
                            }
                        }
                    }
                }
                rightCell.AddElement(line);

                // Additional Information
                rightCell.AddElement(new Paragraph("Additional Information", sectionFont) { SpacingBefore = 10 });
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT Title, StartDate, EndDate, Description FROM CustomSelection WHERE UserID = @UserID ORDER BY StartDate DESC", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string title = reader["Title"]?.ToString() ?? "";
                                string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                string description = reader["Description"]?.ToString() ?? "";

                                string text = $"I worked on {title} between {startDate} and {endDate}. {description}";
                                Paragraph customPara = new Paragraph(text, contentFont) { SpacingBefore = 5 };
                                rightCell.AddElement(customPara);
                            }
                        }
                    }
                }

                mainTable.AddCell(rightCell);
                pdfDoc.Add(mainTable);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding content to PDF: {ex.Message}\nStack Trace: {ex.StackTrace}", ex);
            }
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}