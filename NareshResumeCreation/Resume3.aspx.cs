using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace NareshResumeCreation
{
    public partial class Resume3 : Page
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
                    text3JobTitle.Text = reader["JobTitle"].ToString();
                    text3FirstName.Text = reader["FirstName"].ToString();
                    text3LastName.Text = reader["LastName"].ToString();
                    text3Email.Text = reader["Email"].ToString();
                    text3Phone.Text = reader["Phone"].ToString();
                    text3DateOfBirth.Text = Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                    text3Country.Text = reader["Country"].ToString();
                    text3State.Text = reader["State"].ToString();
                    text3City.Text = reader["City"].ToString();
                    text3Address.Text = reader["Address"].ToString();
                    text3PostalCode.Text = reader["PostalCode"].ToString();
                    text3Nationality.Text = reader["Nationality"].ToString();
                    text3PlaceOfBirth.Text = reader["PlaceOfBirth"].ToString();
                    text3ProfessionalSummary.Text = reader["PersonalSummary"].ToString();
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
            text3JobTitle.Text = "No Data";
            text3FirstName.Text = "No Data";
            text3LastName.Text = "No Data";
            text3Email.Text = "No Data";
            text3Phone.Text = "No Data";
            text3DateOfBirth.Text = "No Data";
            text3Country.Text = "No Data";
            text3State.Text = "No Data";
            text3City.Text = "No Data";
            text3Address.Text = "No Data";
            text3PostalCode.Text = "No Data";
            text3Nationality.Text = "No Data";
            text3PlaceOfBirth.Text = "No Data";
            text3ProfessionalSummary.Text = "No Data";
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
                    pnl3Fresher.Visible = isFresher;
                    pnl3EmploymentHistory.Visible = !isFresher;
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
                    rpt3EmploymentHistory.DataSource = dt;
                    rpt3EmploymentHistory.DataBind();
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
                        rpt3Education.DataSource = dt;
                        rpt3Education.DataBind();
                    }
                    else
                    {
                        rpt3Education.DataSource = new[] { new { Degree = "No education found", SchoolName = "", City = "", StartDate = "N/A", EndDate = "N/A", Description = "" } };
                        rpt3Education.DataBind();
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
                    rptEditEducation.DataSource = dt;
                    rptEditEducation.DataBind();
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
                        rpt3Skills.DataSource = dt;
                        rpt3Skills.DataBind();
                    }
                    else
                    {
                        rpt3Skills.DataSource = new[] { new { SkillName = "No skills found" } };
                        rpt3Skills.DataBind();
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
                    rptEditSkills.DataSource = dt;
                    rptEditSkills.DataBind();
                }
            }
        }

        private void LoadInternships()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT TOP 1 ID, CompanyName, JobTitle, StartDate, EndDate, Description FROM Internships WHERE UserID = @UserID ORDER BY ID DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        text3InternCompany.Text = reader["CompanyName"].ToString();
                        text3InternJobTitle.Text = reader["JobTitle"].ToString();
                        text3InternStartDate.Text = Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy");
                        text3InternEndDate.Text = Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy");
                        text3InternDescription.Text = reader["Description"].ToString();
                    }
                    else
                    {
                        text3InternCompany.Text = "N/A";
                        text3InternJobTitle.Text = "N/A";
                        text3InternStartDate.Text = "N/A";
                        text3InternEndDate.Text = "N/A";
                        text3InternDescription.Text = "No internship data available";
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
                string query = "SELECT TOP 1 ID, CompanyName, JobTitle, StartDate, EndDate, Description FROM Internships WHERE UserID = @UserID ORDER BY ID DESC";
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
                        rpt3Certifications.DataSource = dt;
                        rpt3Certifications.DataBind();
                    }
                    else
                    {
                        rpt3Certifications.DataSource = new[] { new { CourseName = "No certifications found", Institution = "", StartDate = "N/A", EndDate = "N/A" } };
                        rpt3Certifications.DataBind();
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
                    rptEditCertifications.DataSource = dt;
                    rptEditCertifications.DataBind();
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
                        rpt3Languages.DataSource = dt;
                        rpt3Languages.DataBind();
                    }
                    else
                    {
                        rpt3Languages.DataSource = new[] { new { LanguageName = "No languages found" } };
                        rpt3Languages.DataBind();
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
                    rptEditLanguages.DataSource = dt;
                    rptEditLanguages.DataBind();
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
                        rpt3Hobbies.DataSource = dt;
                        rpt3Hobbies.DataBind();
                    }
                    else
                    {
                        rpt3Hobbies.DataSource = new[] { new { Name = "No hobbies found" } };
                        rpt3Hobbies.DataBind();
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
                    rptEditHobbies.DataSource = dt;
                    rptEditHobbies.DataBind();
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
                        rpt3CustomSelection.DataSource = dt;
                        rpt3CustomSelection.DataBind();
                    }
                    else
                    {
                        rpt3CustomSelection.DataSource = new[] { new { Title = "No additional info", StartDate = "N/A", EndDate = "N/A", Description = "" } };
                        rpt3CustomSelection.DataBind();
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
                    rptEditCustomSelection.DataSource = dt;
                    rptEditCustomSelection.DataBind();
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
            LoadAllData();
            ToggleMode();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(txtEditFirstName.Text) || string.IsNullOrWhiteSpace(txtEditLastName.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('First Name and Last Name are required.');", true);
                    return;
                }

                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // Update Personal Details
                    string personalQuery = @"IF EXISTS (SELECT 1 FROM PersonalDetails WHERE UserID = @UserID)
                                            UPDATE PersonalDetails SET JobTitle = @JobTitle, FirstName = @FirstName, LastName = @LastName, 
                                                Email = @Email, Phone = @Phone, DateOfBirth = @DateOfBirth, Country = @Country, 
                                                State = @State, City = @City, Address = @Address, PostalCode = @PostalCode, 
                                                Nationality = @Nationality, PlaceOfBirth = @PlaceOfBirth, PersonalSummary = @PersonalSummary
                                            WHERE UserID = @UserID AND ID = (SELECT TOP 1 ID FROM PersonalDetails WHERE UserID = @UserID ORDER BY ID DESC)
                                            ELSE
                                            INSERT INTO PersonalDetails (UserID, JobTitle, FirstName, LastName, Email, Phone, DateOfBirth, 
                                                Country, State, City, Address, PostalCode, Nationality, PlaceOfBirth, PersonalSummary)
                                            VALUES (@UserID, @JobTitle, @FirstName, @LastName, @Email, @Phone, @DateOfBirth, 
                                                @Country, @State, @City, @Address, @PostalCode, @Nationality, @PlaceOfBirth, @PersonalSummary)";
                    using (SqlCommand cmd = new SqlCommand(personalQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@JobTitle", txtEditJobTitle.Text);
                        cmd.Parameters.AddWithValue("@FirstName", txtEditFirstName.Text);
                        cmd.Parameters.AddWithValue("@LastName", txtEditLastName.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEditEmail.Text);
                        cmd.Parameters.AddWithValue("@Phone", txtEditPhone.Text);
                        cmd.Parameters.AddWithValue("@DateOfBirth", string.IsNullOrEmpty(txtEditDateOfBirth.Text) ? DBNull.Value : (object)DateTime.Parse(txtEditDateOfBirth.Text));
                        cmd.Parameters.AddWithValue("@Country", txtEditCountry.Text);
                        cmd.Parameters.AddWithValue("@State", txtEditState.Text);
                        cmd.Parameters.AddWithValue("@City", txtEditCity.Text);
                        cmd.Parameters.AddWithValue("@Address", txtEditAddress.Text);
                        cmd.Parameters.AddWithValue("@PostalCode", txtEditPostalCode.Text);
                        cmd.Parameters.AddWithValue("@Nationality", txtEditNationality.Text);
                        cmd.Parameters.AddWithValue("@PlaceOfBirth", txtEditPlaceOfBirth.Text);
                        cmd.Parameters.AddWithValue("@PersonalSummary", txtEditProfessionalSummary.Text);
                        cmd.ExecuteNonQuery();
                    }

                    // Update Fresher Status
                    string fresherQuery = "UPDATE Userss SET IsFresher = @IsFresher WHERE UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(fresherQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@IsFresher", chkFresher.Checked);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.ExecuteNonQuery();
                    }

                    // Update Skills
                    foreach (RepeaterItem item in rptEditSkills.Items)
                    {
                        TextBox txtSkillName = item.FindControl("txtSkillName") as TextBox;
                        string id = (item.FindControl("btnDeleteSkill") as Button).CommandArgument;
                        if (!string.IsNullOrWhiteSpace(txtSkillName.Text))
                        {
                            string skillQuery = "UPDATE Skills SET SkillName = @SkillName WHERE ID = @ID AND UserID = @UserID";
                            using (SqlCommand cmd = new SqlCommand(skillQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@SkillName", txtSkillName.Text);
                                cmd.Parameters.AddWithValue("@ID", id);
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Update Languages
                    foreach (RepeaterItem item in rptEditLanguages.Items)
                    {
                        TextBox txtLanguageName = item.FindControl("txtLanguageName") as TextBox;
                        string id = (item.FindControl("btnDeleteLanguage") as Button).CommandArgument;
                        if (!string.IsNullOrWhiteSpace(txtLanguageName.Text))
                        {
                            string languageQuery = "UPDATE Languages SET LanguageName = @LanguageName WHERE ID = @ID AND UserID = @UserID";
                            using (SqlCommand cmd = new SqlCommand(languageQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@LanguageName", txtLanguageName.Text);
                                cmd.Parameters.AddWithValue("@ID", id);
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Update Hobbies
                    foreach (RepeaterItem item in rptEditHobbies.Items)
                    {
                        TextBox txtHobbyName = item.FindControl("txtHobbyName") as TextBox;
                        string id = (item.FindControl("btnDeleteHobby") as Button).CommandArgument;
                        if (!string.IsNullOrWhiteSpace(txtHobbyName.Text))
                        {
                            string hobbyQuery = "UPDATE Hobbies SET Name = @Name WHERE ID = @ID AND UserID = @UserID";
                            using (SqlCommand cmd = new SqlCommand(hobbyQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@Name", txtHobbyName.Text);
                                cmd.Parameters.AddWithValue("@ID", id);
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Update Employment History (if not fresher)
                    if (!chkFresher.Checked)
                    {
                        foreach (RepeaterItem item in rptEditEmploymentHistory.Items)
                        {
                            TextBox txtJobTitle = item.FindControl("txtJobTitle") as TextBox;
                            TextBox txtEmployer = item.FindControl("txtEmployer") as TextBox;
                            TextBox txtCity = item.FindControl("txtCity") as TextBox;
                            TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                            TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;
                            TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
                            string id = (item.FindControl("btnDeleteEmployment") as Button).CommandArgument;

                            if (!string.IsNullOrWhiteSpace(txtJobTitle.Text))
                            {
                                string employmentQuery = @"UPDATE EmploymentHistoryy SET JobTitle = @JobTitle, Employer = @Employer, City = @City, 
                                                        StartDate = @StartDate, EndDate = @EndDate, Description = @Description 
                                                        WHERE ID = @ID AND UserID = @UserID";
                                using (SqlCommand cmd = new SqlCommand(employmentQuery, conn))
                                {
                                    cmd.Parameters.AddWithValue("@JobTitle", txtJobTitle.Text);
                                    cmd.Parameters.AddWithValue("@Employer", txtEmployer.Text);
                                    cmd.Parameters.AddWithValue("@City", txtCity.Text);
                                    cmd.Parameters.AddWithValue("@StartDate", string.IsNullOrEmpty(txtStartDate.Text) ? DBNull.Value : (object)DateTime.Parse(txtStartDate.Text));
                                    cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrEmpty(txtEndDate.Text) ? DBNull.Value : (object)DateTime.Parse(txtEndDate.Text));
                                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                                    cmd.Parameters.AddWithValue("@ID", id);
                                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    // Update Education
                    foreach (RepeaterItem item in rptEditEducation.Items)
                    {
                        TextBox txtDegree = item.FindControl("txtDegree") as TextBox;
                        TextBox txtSchoolName = item.FindControl("txtSchoolName") as TextBox;
                        TextBox txtCity = item.FindControl("txtCity") as TextBox;
                        TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                        TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;
                        TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
                        string id = (item.FindControl("btnDeleteEducation") as Button).CommandArgument;

                        if (!string.IsNullOrWhiteSpace(txtDegree.Text))
                        {
                            string educationQuery = @"UPDATE Education SET Degree = @Degree, SchoolName = @SchoolName, City = @City, 
                                                    StartDate = @StartDate, EndDate = @EndDate, Description = @Description 
                                                    WHERE ID = @ID AND UserID = @UserID";
                            using (SqlCommand cmd = new SqlCommand(educationQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@Degree", txtDegree.Text);
                                cmd.Parameters.AddWithValue("@SchoolName", txtSchoolName.Text);
                                cmd.Parameters.AddWithValue("@City", txtCity.Text);
                                cmd.Parameters.AddWithValue("@StartDate", string.IsNullOrEmpty(txtStartDate.Text) ? DBNull.Value : (object)DateTime.Parse(txtStartDate.Text));
                                cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrEmpty(txtEndDate.Text) ? DBNull.Value : (object)DateTime.Parse(txtEndDate.Text));
                                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                                cmd.Parameters.AddWithValue("@ID", id);
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Update Certifications
                    foreach (RepeaterItem item in rptEditCertifications.Items)
                    {
                        TextBox txtCourseName = item.FindControl("txtCourseName") as TextBox;
                        TextBox txtInstitution = item.FindControl("txtInstitution") as TextBox;
                        TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                        TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;
                        string id = (item.FindControl("btnDeleteCertification") as Button).CommandArgument;

                        if (!string.IsNullOrWhiteSpace(txtCourseName.Text))
                        {
                            string certQuery = @"UPDATE Courses SET CourseName = @CourseName, Institution = @Institution, 
                                                StartDate = @StartDate, EndDate = @EndDate 
                                                WHERE ID = @ID AND UserID = @UserID";
                            using (SqlCommand cmd = new SqlCommand(certQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text);
                                cmd.Parameters.AddWithValue("@Institution", txtInstitution.Text);
                                cmd.Parameters.AddWithValue("@StartDate", string.IsNullOrEmpty(txtStartDate.Text) ? DBNull.Value : (object)DateTime.Parse(txtStartDate.Text));
                                cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrEmpty(txtEndDate.Text) ? DBNull.Value : (object)DateTime.Parse(txtEndDate.Text));
                                cmd.Parameters.AddWithValue("@ID", id);
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Update Internships
                    string internQuery = @"IF EXISTS (SELECT 1 FROM Internships WHERE UserID = @UserID)
                                          UPDATE Internships SET CompanyName = @CompanyName, JobTitle = @JobTitle, 
                                              StartDate = @StartDate, EndDate = @EndDate, Description = @Description
                                          WHERE UserID = @UserID AND ID = (SELECT TOP 1 ID FROM Internships WHERE UserID = @UserID ORDER BY ID DESC)
                                          ELSE
                                          INSERT INTO Internships (UserID, CompanyName, JobTitle, StartDate, EndDate, Description)
                                          VALUES (@UserID, @CompanyName, @JobTitle, @StartDate, @EndDate, @Description)";
                    using (SqlCommand cmd = new SqlCommand(internQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@CompanyName", txtEditInternCompany.Text);
                        cmd.Parameters.AddWithValue("@JobTitle", txtEditInternJobTitle.Text);
                        cmd.Parameters.AddWithValue("@StartDate", string.IsNullOrEmpty(txtEditInternStartDate.Text) ? DBNull.Value : (object)DateTime.Parse(txtEditInternStartDate.Text));
                        cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrEmpty(txtEditInternEndDate.Text) ? DBNull.Value : (object)DateTime.Parse(txtEditInternEndDate.Text));
                        cmd.Parameters.AddWithValue("@Description", txtEditInternDescription.Text);
                        cmd.ExecuteNonQuery();
                    }

                    // Update Custom Selection
                    foreach (RepeaterItem item in rptEditCustomSelection.Items)
                    {
                        TextBox txtTitle = item.FindControl("txtTitle") as TextBox;
                        TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                        TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;
                        TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
                        string id = (item.FindControl("btnDeleteCustom") as Button).CommandArgument;

                        if (!string.IsNullOrWhiteSpace(txtTitle.Text))
                        {
                            string customQuery = @"UPDATE CustomSelection SET Title = @Title, StartDate = @StartDate, 
                                                  EndDate = @EndDate, Description = @Description 
                                                  WHERE ID = @ID AND UserID = @UserID";
                            using (SqlCommand cmd = new SqlCommand(customQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                                cmd.Parameters.AddWithValue("@StartDate", string.IsNullOrEmpty(txtStartDate.Text) ? DBNull.Value : (object)DateTime.Parse(txtStartDate.Text));
                                cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrEmpty(txtEndDate.Text) ? DBNull.Value : (object)DateTime.Parse(txtEndDate.Text));
                                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                                cmd.Parameters.AddWithValue("@ID", id);
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                ViewState["Mode"] = "View";
                LoadAllData();
                ToggleMode();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Resume updated successfully.');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error saving resume: {ex.Message}');", true);
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
                        cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditSkills();
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
                        cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditLanguages();
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
                        cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditHobbies();
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
                        cmd.Parameters.AddWithValue("@StartDate", string.IsNullOrEmpty(txtNewStartDate.Text) ? DBNull.Value : (object)DateTime.Parse(txtNewStartDate.Text));
                        cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrEmpty(txtNewEndDate.Text) ? DBNull.Value : (object)DateTime.Parse(txtNewEndDate.Text));
                        cmd.Parameters.AddWithValue("@Description", txtNewDescription.Text);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                ClearNewEmploymentFields();
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
                        cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditEmploymentHistory();
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
                        cmd.Parameters.AddWithValue("@StartDate", string.IsNullOrEmpty(txtNewEducationStartDate.Text) ? DBNull.Value : (object)DateTime.Parse(txtNewEducationStartDate.Text));
                        cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrEmpty(txtNewEducationEndDate.Text) ? DBNull.Value : (object)DateTime.Parse(txtNewEducationEndDate.Text));
                        cmd.Parameters.AddWithValue("@Description", txtNewEducationDescription.Text);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                ClearNewEducationFields();
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
                        cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditEducation();
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
                        cmd.Parameters.AddWithValue("@StartDate", string.IsNullOrEmpty(txtNewCertStartDate.Text) ? DBNull.Value : (object)DateTime.Parse(txtNewCertStartDate.Text));
                        cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrEmpty(txtNewCertEndDate.Text) ? DBNull.Value : (object)DateTime.Parse(txtNewCertEndDate.Text));
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                ClearNewCertificationFields();
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
                        cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditCertifications();
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
                        cmd.Parameters.AddWithValue("@StartDate", string.IsNullOrEmpty(txtNewCustomStartDate.Text) ? DBNull.Value : (object)DateTime.Parse(txtNewCustomStartDate.Text));
                        cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrEmpty(txtNewCustomEndDate.Text) ? DBNull.Value : (object)DateTime.Parse(txtNewCustomEndDate.Text));
                        cmd.Parameters.AddWithValue("@Description", txtNewCustomDescription.Text);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                ClearNewCustomFields();
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
                        cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditCustomSelection();
            }
        }

        protected void chkFresher_CheckedChanged(object sender, EventArgs e)
        {
            pnlEditEmploymentHistory.Visible = !chkFresher.Checked;
        }

        private void ClearNewEmploymentFields()
        {
            txtNewJobTitle.Text = "";
            txtNewEmployer.Text = "";
            txtNewCity.Text = "";
            txtNewStartDate.Text = "";
            txtNewEndDate.Text = "";
            txtNewDescription.Text = "";
        }

        private void ClearNewEducationFields()
        {
            txtNewDegree.Text = "";
            txtNewSchoolName.Text = "";
            txtNewEducationCity.Text = "";
            txtNewEducationStartDate.Text = "";
            txtNewEducationEndDate.Text = "";
            txtNewEducationDescription.Text = "";
        }

        private void ClearNewCertificationFields()
        {
            txtNewCourseName.Text = "";
            txtNewInstitution.Text = "";
            txtNewCertStartDate.Text = "";
            txtNewCertEndDate.Text = "";
        }

        private void ClearNewCustomFields()
        {
            txtNewCustomTitle.Text = "";
            txtNewCustomStartDate.Text = "";
            txtNewCustomEndDate.Text = "";
            txtNewCustomDescription.Text = "";
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

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            btnDownload.Enabled = false;
            try
            {
                // Define the folder to save resumes
                string resumeFolder = Server.MapPath("~/Resumes/");
                if (!Directory.Exists(resumeFolder))
                {
                    Directory.CreateDirectory(resumeFolder);
                }

                // Generate the PDF
                using (MemoryStream ms = new MemoryStream())
                {
                    using (Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10))
                    {
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms);
                        pdfDoc.Open();

                        AddResumeContentToPdf(pdfDoc);

                        pdfDoc.Close();

                        // Generate unique filename with UserID
                        string userId = Session["UserID"]?.ToString() ?? "Unknown";
                        string firstName = text3FirstName.Text.Replace(" ", "_");
                        string lastName = text3LastName.Text.Replace(" ", "_");
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

                        // Send the PDF to the client for download
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
                // Define colors matching the template
                BaseColor headerBgColor = new BaseColor(26, 115, 232);
                BaseColor accentColor = new BaseColor(26, 115, 232);
                BaseColor entryBgColor = new BaseColor(250, 250, 250);
                BaseColor listBgColor = new BaseColor(232, 240, 254);
                BaseColor darkTextColor = new BaseColor(51, 51, 51);
                BaseColor lightTextColor = new BaseColor(255, 255, 255);

                // Define fonts
                Font nameFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, lightTextColor);
                Font jobTitleFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, lightTextColor);
                Font sectionFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, accentColor);
                Font entryContentFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, new BaseColor(85, 85, 85));
                Font sidebarFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, darkTextColor);
                Font listFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, darkTextColor);

                // Header
                PdfPTable headerTable = new PdfPTable(1);
                headerTable.WidthPercentage = 100;
                PdfPCell headerCell = new PdfPCell();
                headerCell.BackgroundColor = headerBgColor;
                headerCell.Border = Rectangle.NO_BORDER;
                headerCell.Padding = 15;

                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;

                // Header Section
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
                                headerCell.AddElement(new Paragraph(fullName, nameFont) { Alignment = Element.ALIGN_CENTER });

                                string jobTitle = reader["JobTitle"]?.ToString() ?? text3JobTitle.Text?.Trim() ?? "";
                                headerCell.AddElement(new Paragraph(jobTitle, jobTitleFont) { Alignment = Element.ALIGN_CENTER });
                            }
                            else
                            {
                                headerCell.AddElement(new Paragraph("User Name", nameFont) { Alignment = Element.ALIGN_CENTER });
                                headerCell.AddElement(new Paragraph("Job Title", jobTitleFont) { Alignment = Element.ALIGN_CENTER });
                            }
                        }
                    }
                }
                headerTable.AddCell(headerCell);
                pdfDoc.Add(headerTable);

                // Main content table
                PdfPTable mainTable = new PdfPTable(2);
                mainTable.WidthPercentage = 100;
                mainTable.SetWidths(new float[] { 35, 65 });

                // Left Column
                PdfPCell leftCell = new PdfPCell();
                leftCell.Border = Rectangle.NO_BORDER;
                leftCell.Padding = 10;

                // Contact Info
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
                                string email = reader["Email"]?.ToString() ?? text3Email.Text?.Trim() ?? "";
                                string phone = reader["Phone"]?.ToString() ?? text3Phone.Text?.Trim() ?? "";
                                string address = $"{reader["Address"] ?? text3Address.Text?.Trim() ?? ""}, {reader["City"] ?? text3City.Text?.Trim() ?? ""}, {reader["State"] ?? text3State.Text?.Trim() ?? ""}, {reader["PostalCode"] ?? text3PostalCode.Text?.Trim() ?? ""}, {reader["Country"] ?? text3Country.Text?.Trim() ?? ""}";

                                AddSidebarSection(leftCell, "Contact", new string[]
                                {
                            $"✉️ {email}",
                            $"📱 {phone}",
                            $"🏠 {address}"
                                }, accentColor, sidebarFont);

                                string dob = reader["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd") : text3DateOfBirth.Text?.Trim() ?? "";
                                string placeOfBirth = reader["PlaceOfBirth"]?.ToString() ?? text3PlaceOfBirth.Text?.Trim() ?? "";
                                string nationality = reader["Nationality"]?.ToString() ?? text3Nationality.Text?.Trim() ?? "";

                                AddSidebarSection(leftCell, "Personal Info", new string[]
                                {
                            $"🎂 {dob}",
                            $"🌍 {placeOfBirth}",
                            $"📍 {nationality}"
                                }, accentColor, sidebarFont);
                            }
                        }
                    }
                }

                // Skills
                PdfPTable skillsTable = new PdfPTable(1);
                skillsTable.WidthPercentage = 100;
                AddSectionTitle(skillsTable, "Skills", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, accentColor), accentColor);

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
                                        PdfPCell skillCell = new PdfPCell(new Phrase(skill, listFont));
                                        skillCell.BackgroundColor = listBgColor;
                                        skillCell.Border = Rectangle.NO_BORDER;
                                        skillCell.Padding = 6;
                                        skillsTable.AddCell(skillCell);
                                    }
                                }
                            }
                            else
                            {
                                PdfPCell skillCell = new PdfPCell(new Phrase("No skills listed", listFont));
                                skillCell.BackgroundColor = listBgColor;
                                skillCell.Border = Rectangle.NO_BORDER;
                                skillCell.Padding = 6;
                                skillsTable.AddCell(skillCell);
                            }
                        }
                    }
                }
                skillsTable.SpacingAfter = 10;
                leftCell.AddElement(skillsTable);

                // Languages
                PdfPTable languagesTable = new PdfPTable(1);
                languagesTable.WidthPercentage = 100;
                AddSectionTitle(languagesTable, "Languages", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, accentColor), accentColor);

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
                                        PdfPCell langCell = new PdfPCell(new Phrase(language, listFont));
                                        langCell.BackgroundColor = listBgColor;
                                        langCell.Border = Rectangle.NO_BORDER;
                                        langCell.Padding = 6;
                                        languagesTable.AddCell(langCell);
                                    }
                                }
                            }
                            else
                            {
                                PdfPCell langCell = new PdfPCell(new Phrase("No languages listed", listFont));
                                langCell.BackgroundColor = listBgColor;
                                langCell.Border = Rectangle.NO_BORDER;
                                langCell.Padding = 6;
                                languagesTable.AddCell(langCell);
                            }
                        }
                    }
                }
                languagesTable.SpacingAfter = 10;
                leftCell.AddElement(languagesTable);

                // Hobbies
                PdfPTable hobbiesTable = new PdfPTable(1);
                hobbiesTable.WidthPercentage = 100;
                AddSectionTitle(hobbiesTable, "Hobbies", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, accentColor), accentColor);

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
                                        PdfPCell hobbyCell = new PdfPCell(new Phrase(hobby, listFont));
                                        hobbyCell.BackgroundColor = listBgColor;
                                        hobbyCell.Border = Rectangle.NO_BORDER;
                                        hobbyCell.Padding = 6;
                                        hobbiesTable.AddCell(hobbyCell);
                                    }
                                }
                            }
                            else
                            {
                                PdfPCell hobbyCell = new PdfPCell(new Phrase("No hobbies listed", listFont));
                                hobbyCell.BackgroundColor = listBgColor;
                                hobbyCell.Border = Rectangle.NO_BORDER;
                                hobbyCell.Padding = 6;
                                hobbiesTable.AddCell(hobbyCell);
                            }
                        }
                    }
                }
                hobbiesTable.SpacingAfter = 10;
                leftCell.AddElement(hobbiesTable);

                mainTable.AddCell(leftCell);

                // Right Column
                PdfPCell rightCell = new PdfPCell();
                rightCell.Border = Rectangle.NO_BORDER;
                rightCell.Padding = 10;

                // Professional Summary
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT PersonalSummary FROM PersonalDetails WHERE UserID = @UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        string summary = cmd.ExecuteScalar()?.ToString() ?? text3ProfessionalSummary.Text?.Trim() ?? "";

                        PdfPTable summaryTable = new PdfPTable(1);
                        summaryTable.WidthPercentage = 100;
                        AddSectionTitle(summaryTable, "Professional Summary", sectionFont, accentColor);

                        PdfPCell summaryCell = new PdfPCell(new Phrase(summary, entryContentFont));
                        summaryCell.BackgroundColor = entryBgColor;
                        summaryCell.Border = Rectangle.NO_BORDER;
                        summaryCell.Padding = 8;
                        summaryTable.AddCell(summaryCell);

                        summaryTable.SpacingAfter = 10;
                        rightCell.AddElement(summaryTable);
                    }
                }

                // Employment History
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

                PdfPTable employmentTable = new PdfPTable(1);
                employmentTable.WidthPercentage = 100;
                AddSectionTitle(employmentTable, "Employment History", sectionFont, accentColor);

                if (isFresher)
                {
                    PdfPCell fresherCell = new PdfPCell(new Phrase("I am a fresher with no work experience.", entryContentFont));
                    fresherCell.BackgroundColor = entryBgColor;
                    fresherCell.Border = Rectangle.NO_BORDER;
                    fresherCell.Padding = 8;
                    employmentTable.AddCell(fresherCell);
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
                                if (!reader.HasRows)
                                {
                                    PdfPCell noHistoryCell = new PdfPCell(new Phrase("No employment history found.", entryContentFont));
                                    noHistoryCell.BackgroundColor = entryBgColor;
                                    noHistoryCell.Border = Rectangle.NO_BORDER;
                                    noHistoryCell.Padding = 8;
                                    employmentTable.AddCell(noHistoryCell);
                                }
                                else
                                {
                                    while (reader.Read())
                                    {
                                        string jobTitle = reader["JobTitle"]?.ToString() ?? "";
                                        string employer = reader["Employer"]?.ToString() ?? "";
                                        string city = reader["City"]?.ToString() ?? "";
                                        string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                        string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                        string description = reader["Description"]?.ToString() ?? "";
                                        string employmentText = $"I worked as {jobTitle} at {employer}, {city} between {startDate} and {endDate}. {description}";

                                        PdfPCell employmentCell = new PdfPCell(new Phrase(employmentText, entryContentFont));
                                        employmentCell.BackgroundColor = entryBgColor;
                                        employmentCell.Border = Rectangle.NO_BORDER;
                                        employmentCell.Padding = 8;
                                        employmentTable.AddCell(employmentCell);
                                    }
                                }
                            }
                        }
                    }
                }
                employmentTable.SpacingAfter = 10;
                rightCell.AddElement(employmentTable);

                // Education
                PdfPTable educationTable = new PdfPTable(1);
                educationTable.WidthPercentage = 100;
                AddSectionTitle(educationTable, "Education", sectionFont, accentColor);

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT Degree, SchoolName, City, StartDate, EndDate, Description FROM Education WHERE UserID = @UserID ORDER BY StartDate DESC", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                PdfPCell noEducationCell = new PdfPCell(new Phrase("No education found.", entryContentFont));
                                noEducationCell.BackgroundColor = entryBgColor;
                                noEducationCell.Border = Rectangle.NO_BORDER;
                                noEducationCell.Padding = 8;
                                educationTable.AddCell(noEducationCell);
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    string degree = reader["Degree"]?.ToString() ?? "";
                                    string schoolName = reader["SchoolName"]?.ToString() ?? "";
                                    string city = reader["City"]?.ToString() ?? "";
                                    string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                    string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                    string description = reader["Description"]?.ToString() ?? "";
                                    string educationText = $"I completed my {degree} from {schoolName}, {city} between {startDate} and {endDate}. {description}";

                                    PdfPCell educationCell = new PdfPCell(new Phrase(educationText, entryContentFont));
                                    educationCell.BackgroundColor = entryBgColor;
                                    educationCell.Border = Rectangle.NO_BORDER;
                                    educationCell.Padding = 8;
                                    educationTable.AddCell(educationCell);
                                }
                            }
                        }
                    }
                }
                educationTable.SpacingAfter = 10;
                rightCell.AddElement(educationTable);

                // Certifications
                PdfPTable certTable = new PdfPTable(1);
                certTable.WidthPercentage = 100;
                AddSectionTitle(certTable, "Certifications", sectionFont, accentColor);

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CourseName, Institution, StartDate, EndDate FROM Courses WHERE UserID = @UserID ORDER BY StartDate DESC", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                PdfPCell noCertCell = new PdfPCell(new Phrase("No certifications found.", entryContentFont));
                                noCertCell.BackgroundColor = entryBgColor;
                                noCertCell.Border = Rectangle.NO_BORDER;
                                noCertCell.Padding = 8;
                                certTable.AddCell(noCertCell);
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    string courseName = reader["CourseName"]?.ToString() ?? "";
                                    string institution = reader["Institution"]?.ToString() ?? "";
                                    string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                    string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                    string certText = $"I completed {courseName} from {institution} between {startDate} and {endDate}.";

                                    PdfPCell certCell = new PdfPCell(new Phrase(certText, entryContentFont));
                                    certCell.BackgroundColor = entryBgColor;
                                    certCell.Border = Rectangle.NO_BORDER;
                                    certCell.Padding = 8;
                                    certTable.AddCell(certCell);
                                }
                            }
                        }
                    }
                }
                certTable.SpacingAfter = 10;
                rightCell.AddElement(certTable);

                // Internships
                PdfPTable internTable = new PdfPTable(1);
                internTable.WidthPercentage = 100;
                AddSectionTitle(internTable, "Internships", sectionFont, accentColor);

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
                                string internText = $"I worked as {jobTitle} at {company} between {startDate} and {endDate}. {description}";

                                PdfPCell internCell = new PdfPCell(new Phrase(internText, entryContentFont));
                                internCell.BackgroundColor = entryBgColor;
                                internCell.Border = Rectangle.NO_BORDER;
                                internCell.Padding = 8;
                                internTable.AddCell(internCell);
                            }
                            else
                            {
                                PdfPCell noInternCell = new PdfPCell(new Phrase("No internship data available.", entryContentFont));
                                noInternCell.BackgroundColor = entryBgColor;
                                noInternCell.Border = Rectangle.NO_BORDER;
                                noInternCell.Padding = 8;
                                internTable.AddCell(noInternCell);
                            }
                        }
                    }
                }
                internTable.SpacingAfter = 10;
                rightCell.AddElement(internTable);

                // Additional Information
                PdfPTable customTable = new PdfPTable(1);
                customTable.WidthPercentage = 100;
                AddSectionTitle(customTable, "Additional Information", sectionFont, accentColor);

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT Title, StartDate, EndDate, Description FROM CustomSelection WHERE UserID = @UserID ORDER BY StartDate DESC", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                PdfPCell noCustomCell = new PdfPCell(new Phrase("No additional information.", entryContentFont));
                                noCustomCell.BackgroundColor = entryBgColor;
                                noCustomCell.Border = Rectangle.NO_BORDER;
                                noCustomCell.Padding = 8;
                                customTable.AddCell(noCustomCell);
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    string title = reader["Title"]?.ToString() ?? "";
                                    string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                    string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                    string description = reader["Description"]?.ToString() ?? "";
                                    string customText = $"I worked on {title} between {startDate} and {endDate}. {description}";

                                    PdfPCell customCell = new PdfPCell(new Phrase(customText, entryContentFont));
                                    customCell.BackgroundColor = entryBgColor;
                                    customCell.Border = Rectangle.NO_BORDER;
                                    customCell.Padding = 8;
                                    customTable.AddCell(customCell);
                                }
                            }
                        }
                    }
                }
                customTable.SpacingAfter = 10;
                rightCell.AddElement(customTable);

                mainTable.AddCell(rightCell);
                pdfDoc.Add(mainTable);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding content to PDF: {ex.Message}\nStack Trace: {ex.StackTrace}", ex);
            }
        }

        private void AddSidebarSection(PdfPCell cell, string title, string[] items, BaseColor accentColor, Font font)
        {
            PdfPTable sectionTable = new PdfPTable(1);
            sectionTable.WidthPercentage = 100;

            PdfPCell titleCell = new PdfPCell(new Phrase(title, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, accentColor)));
            titleCell.Border = Rectangle.BOTTOM_BORDER;
            titleCell.BorderColor = accentColor;
            titleCell.BorderWidth = 2;
            titleCell.PaddingBottom = 5;
            sectionTable.AddCell(titleCell);

            foreach (string item in items)
            {
                PdfPCell itemCell = new PdfPCell(new Phrase(item, font));
                itemCell.Border = Rectangle.NO_BORDER;
                itemCell.Padding = 5;
                sectionTable.AddCell(itemCell);
            }
            sectionTable.SpacingAfter = 10;
            cell.AddElement(sectionTable);
        }

        private void AddSectionTitle(PdfPTable table, string title, Font font, BaseColor borderColor)
        {
            PdfPCell titleCell = new PdfPCell(new Phrase(title, font));
            titleCell.Border = Rectangle.BOTTOM_BORDER;
            titleCell.BorderColor = borderColor;
            titleCell.BorderWidth = 2;
            titleCell.PaddingBottom = 5;
            table.AddCell(titleCell);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        private object Eval(string fieldName, RepeaterItem item)
        {
            return DataBinder.Eval(item.DataItem, fieldName);
        }
    }
}