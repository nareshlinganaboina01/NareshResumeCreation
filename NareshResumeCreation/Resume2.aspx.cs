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
    public partial class Resume2 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("Default.aspx");
                return;
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
                    text2JobTitle.Text = reader["JobTitle"].ToString();
                    text2FirstName.Text = reader["FirstName"].ToString();
                    text2LastName.Text = reader["LastName"].ToString();
                    text2Email.Text = reader["Email"].ToString();
                    text2Phone.Text = reader["Phone"].ToString();
                    text2DateOfBirth.Text = reader["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd") : "";
                    text2Country.Text = reader["Country"].ToString();
                    text2State.Text = reader["State"].ToString();
                    text2City.Text = reader["City"].ToString();
                    text2Address.Text = reader["Address"].ToString();
                    text2PostalCode.Text = reader["PostalCode"].ToString();
                    text2Nationality.Text = reader["Nationality"].ToString();
                    text2PlaceOfBirth.Text = reader["PlaceOfBirth"].ToString();
                    text2ProfessionalSummary.Text = reader["PersonalSummary"].ToString();
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
            text2JobTitle.Text = "No Data";
            text2FirstName.Text = "No Data";
            text2LastName.Text = "No Data";
            text2Email.Text = "No Data";
            text2Phone.Text = "No Data";
            text2DateOfBirth.Text = "";
            text2Country.Text = "No Data";
            text2State.Text = "No Data";
            text2City.Text = "No Data";
            text2Address.Text = "No Data";
            text2PostalCode.Text = "No Data";
            text2Nationality.Text = "No Data";
            text2PlaceOfBirth.Text = "No Data";
            text2ProfessionalSummary.Text = "No Data";
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
                    txtEditDateOfBirth.Text = reader["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd") : "";
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

            LoadEditSkills();
            LoadEditLanguages();
            LoadEditHobbies();
            LoadEditEmploymentHistory();
            LoadEditEducation();
            LoadEditCertifications();
            LoadEditCustomSelection();
            LoadEditInternships();

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
                    pnl2Fresher.Visible = isFresher;
                    pnl2EmploymentHistory.Visible = !isFresher;
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
                    rpt2EmploymentHistory.DataSource = dt;
                    rpt2EmploymentHistory.DataBind();
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
                        rpt2Education.DataSource = dt;
                        rpt2Education.DataBind();
                    }
                    else
                    {
                        rpt2Education.DataSource = new[] { new { Degree = "No education found", SchoolName = "", City = "", StartDate = "N/A", EndDate = "N/A", Description = "" } };
                        rpt2Education.DataBind();
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
                        rpt2Skills.DataSource = dt;
                        rpt2Skills.DataBind();
                    }
                    else
                    {
                        rpt2Skills.DataSource = new[] { new { SkillName = "No skills found" } };
                        rpt2Skills.DataBind();
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
                        text2InternCompany.Text = reader["CompanyName"].ToString();
                        text2InternJobTitle.Text = reader["JobTitle"].ToString();
                        text2InternStartDate.Text = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "N/A";
                        text2InternEndDate.Text = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "N/A";
                        text2InternDescription.Text = reader["Description"].ToString();
                    }
                    else
                    {
                        text2InternCompany.Text = "N/A";
                        text2InternJobTitle.Text = "N/A";
                        text2InternStartDate.Text = "N/A";
                        text2InternEndDate.Text = "N/A";
                        text2InternDescription.Text = "No internship data available";
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
                        txtEditInternStartDate.Text = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("yyyy-MM-dd") : "";
                        txtEditInternEndDate.Text = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("yyyy-MM-dd") : "";
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
                        rpt2Certifications.DataSource = dt;
                        rpt2Certifications.DataBind();
                    }
                    else
                    {
                        rpt2Certifications.DataSource = new[] { new { CourseName = "No certifications found", Institution = "", StartDate = "N/A", EndDate = "N/A" } };
                        rpt2Certifications.DataBind();
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
                        rpt2Languages.DataSource = dt;
                        rpt2Languages.DataBind();
                    }
                    else
                    {
                        rpt2Languages.DataSource = new[] { new { LanguageName = "No languages found" } };
                        rpt2Languages.DataBind();
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
                        rpt2Hobbies.DataSource = dt;
                        rpt2Hobbies.DataBind();
                    }
                    else
                    {
                        rpt2Hobbies.DataSource = new[] { new { Name = "No hobbies found" } };
                        rpt2Hobbies.DataBind();
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
                        rpt2CustomSelection.DataSource = dt;
                        rpt2CustomSelection.DataBind();
                    }
                    else
                    {
                        rpt2CustomSelection.DataSource = new[] { new { Title = "No additional info", StartDate = "N/A", EndDate = "N/A", Description = "" } };
                        rpt2CustomSelection.DataBind();
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
                if (string.IsNullOrWhiteSpace(txtEditFirstName.Text) || string.IsNullOrWhiteSpace(txtEditLastName.Text))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('First Name and Last Name are required.');", true);
                    return;
                }

                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // Update or Insert Personal Details
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
                        string id = (item.FindControl("btnDeleteSkill") as Button)?.CommandArgument;
                        if (!string.IsNullOrWhiteSpace(txtSkillName.Text) && !string.IsNullOrEmpty(id))
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
                        string id = (item.FindControl("btnDeleteLanguage") as Button)?.CommandArgument;
                        if (!string.IsNullOrWhiteSpace(txtLanguageName.Text) && !string.IsNullOrEmpty(id))
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
                        string id = (item.FindControl("btnDeleteHobby") as Button)?.CommandArgument;
                        if (!string.IsNullOrWhiteSpace(txtHobbyName.Text) && !string.IsNullOrEmpty(id))
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

                    // Update Employment History
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
                            string id = (item.FindControl("btnDeleteEmployment") as Button)?.CommandArgument;

                            if (!string.IsNullOrWhiteSpace(txtJobTitle.Text) && !string.IsNullOrEmpty(id))
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
                        string id = (item.FindControl("btnDeleteEducation") as Button)?.CommandArgument;

                        if (!string.IsNullOrWhiteSpace(txtDegree.Text) && !string.IsNullOrEmpty(id))
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
                        string id = (item.FindControl("btnDeleteCertification") as Button)?.CommandArgument;

                        if (!string.IsNullOrWhiteSpace(txtCourseName.Text) && !string.IsNullOrEmpty(id))
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
                        string id = (item.FindControl("btnDeleteCustom") as Button)?.CommandArgument;

                        if (!string.IsNullOrWhiteSpace(txtTitle.Text) && !string.IsNullOrEmpty(id))
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
                try
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
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Skill deleted successfully.');", true);
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No skill found with the specified ID.');", true);
                            }
                        }
                    }
                    LoadEditSkills();
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error deleting skill: {ex.Message}');", true);
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
                try
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
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Language deleted successfully.');", true);
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No language found with the specified ID.');", true);
                            }
                        }
                    }
                    LoadEditLanguages();
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error deleting language: {ex.Message}');", true);
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
                try
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
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Hobby deleted successfully.');", true);
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No hobby found with the specified ID.');", true);
                            }
                        }
                    }
                    LoadEditHobbies();
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error deleting hobby: {ex.Message}');", true);
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

        private void ClearNewEmploymentFields()
        {
            txtNewJobTitle.Text = "";
            txtNewEmployer.Text = "";
            txtNewCity.Text = "";
            txtNewStartDate.Text = "";
            txtNewEndDate.Text = "";
            txtNewDescription.Text = "";
        }

        protected void Employment_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
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
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Employment record deleted successfully.');", true);
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No employment record found with the specified ID.');", true);
                            }
                        }
                    }
                    LoadEditEmploymentHistory();
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error deleting employment record: {ex.Message}');", true);
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

        private void ClearNewEducationFields()
        {
            txtNewDegree.Text = "";
            txtNewSchoolName.Text = "";
            txtNewEducationCity.Text = "";
            txtNewEducationStartDate.Text = "";
            txtNewEducationEndDate.Text = "";
            txtNewEducationDescription.Text = "";
        }

        protected void Education_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
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
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Education record deleted successfully.');", true);
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No education record found with the specified ID.');", true);
                            }
                        }
                    }
                    LoadEditEducation();
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error deleting education record: {ex.Message}');", true);
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

        private void ClearNewCertificationFields()
        {
            txtNewCourseName.Text = "";
            txtNewInstitution.Text = "";
            txtNewCertStartDate.Text = "";
            txtNewCertEndDate.Text = "";
        }

        protected void Certification_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
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
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Certification deleted successfully.');", true);
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No certification found with the specified ID.');", true);
                            }
                        }
                    }
                    LoadEditCertifications();
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error deleting certification: {ex.Message}');", true);
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

        private void ClearNewCustomFields()
        {
            txtNewCustomTitle.Text = "";
            txtNewCustomStartDate.Text = "";
            txtNewCustomEndDate.Text = "";
            txtNewCustomDescription.Text = "";
        }

        protected void CustomSelection_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
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
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Custom section deleted successfully.');", true);
                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('No custom section found with the specified ID.');", true);
                            }
                        }
                    }
                    LoadEditCustomSelection();
                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error deleting custom section: {ex.Message}');", true);
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
                    using (Document pdfDoc = new Document(PageSize.A4, 8, 8, 8, 8))
                    {
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms);
                        pdfDoc.Open();

                        AddResumeContentToPdf(pdfDoc);

                        pdfDoc.Close();

                        // Generate unique filename with UserID
                        string userId = Session["UserID"]?.ToString() ?? "Unknown";
                        string firstName = text2FirstName.Text.Replace(" ", "_");
                        string lastName = text2LastName.Text.Replace(" ", "_");
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
                // Define colors matching type2 template
                BaseColor sidebarBgColor = new BaseColor(43, 45, 66);  // #2b2d42
                BaseColor accentColor = new BaseColor(239, 35, 60);   // #ef233c
                BaseColor mainBgColor = new BaseColor(248, 249, 250); // #f8f9fa
                BaseColor entryBgColor = new BaseColor(241, 243, 245); // #f1f3f5
                BaseColor darkTextColor = new BaseColor(43, 45, 66);  // #2b2d42
                BaseColor lightTextColor = new BaseColor(255, 255, 255); // White for sidebar

                // Define fonts with slightly increased sizes
                Font nameFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, lightTextColor);
                Font jobTitleFont = FontFactory.GetFont(FontFactory.HELVETICA, 11, new BaseColor(141, 153, 174));
                Font sectionFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 13, darkTextColor);
                Font entryContentFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, new BaseColor(73, 80, 87));
                Font sidebarFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, lightTextColor);
                Font skillFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, lightTextColor);

                // Create main table to simulate the two-column layout
                PdfPTable mainTable = new PdfPTable(2);
                mainTable.WidthPercentage = 100;
                mainTable.SetWidths(new float[] { 30, 70 });

                // ========== SIDEBAR COLUMN ==========
                PdfPCell sidebarCell = new PdfPCell();
                sidebarCell.BackgroundColor = sidebarBgColor;
                sidebarCell.Border = Rectangle.NO_BORDER;
                sidebarCell.Padding = 12;

                // Profile Header
                PdfPTable profileTable = new PdfPTable(1);
                profileTable.WidthPercentage = 100;

                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                string fullName = "";
                string jobTitle = "";
                string email = "";
                string phone = "";
                string address = "";
                string city = "";
                string state = "";
                string postalCode = "";
                string country = "";
                string dob = "";
                string placeOfBirth = "";
                string nationality = "";

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
                                fullName = $"{reader["FirstName"]} {reader["LastName"]}".Trim();
                                jobTitle = reader["JobTitle"]?.ToString() ?? text2JobTitle.Text?.Trim() ?? "";
                                email = reader["Email"]?.ToString() ?? text2Email.Text?.Trim() ?? "";
                                phone = reader["Phone"]?.ToString() ?? text2Phone.Text?.Trim() ?? "";
                                address = reader["Address"]?.ToString() ?? text2Address.Text?.Trim() ?? "";
                                city = reader["City"]?.ToString() ?? text2City.Text?.Trim() ?? "";
                                state = reader["State"]?.ToString() ?? text2State.Text?.Trim() ?? "";
                                postalCode = reader["PostalCode"]?.ToString() ?? text2PostalCode.Text?.Trim() ?? "";
                                country = reader["Country"]?.ToString() ?? text2Country.Text?.Trim() ?? "";
                                dob = reader["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd") : text2DateOfBirth.Text?.Trim() ?? "";
                                placeOfBirth = reader["PlaceOfBirth"]?.ToString() ?? text2PlaceOfBirth.Text?.Trim() ?? "";
                                nationality = reader["Nationality"]?.ToString() ?? text2Nationality.Text?.Trim() ?? "";
                            }
                        }
                    }
                }

                PdfPCell nameCell = new PdfPCell(new Phrase(fullName, nameFont));
                nameCell.Border = Rectangle.NO_BORDER;
                nameCell.HorizontalAlignment = Element.ALIGN_CENTER;
                nameCell.PaddingBottom = 6;
                profileTable.AddCell(nameCell);

                PdfPCell jobCell = new PdfPCell(new Phrase(jobTitle, jobTitleFont));
                jobCell.Border = Rectangle.NO_BORDER;
                jobCell.HorizontalAlignment = Element.ALIGN_CENTER;
                profileTable.AddCell(jobCell);

                sidebarCell.AddElement(profileTable);

                // Add spacing
                sidebarCell.AddElement(new Paragraph(" ") { SpacingAfter = 12 });

                // Contact Information
                AddSidebarSection(sidebarCell, "Contact", new string[]
                {
            $"✉️ {email}",
            $"📱 {phone}",
            $"🏠 {address}, {city}, {state}, {postalCode}, {country}"
                }, accentColor, sidebarFont);

                // Personal Information
                AddSidebarSection(sidebarCell, "Personal Info", new string[]
                {
            $"🎂 {dob}",
            $"🌍 {placeOfBirth}",
            $"📍 {nationality}"
                }, accentColor, sidebarFont);

                // Skills
                PdfPTable skillsSection = new PdfPTable(1);
                skillsSection.WidthPercentage = 100;

                PdfPCell skillsTitleCell = new PdfPCell(new Phrase("Skills",
                    FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, accentColor)));
                skillsTitleCell.Border = Rectangle.NO_BORDER;
                skillsSection.AddCell(skillsTitleCell);

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
                                        PdfPCell skillCell = new PdfPCell(new Phrase(skill, skillFont));
                                        skillCell.BackgroundColor = accentColor;
                                        skillCell.Border = Rectangle.NO_BORDER;
                                        skillCell.Padding = 5;
                                        skillCell.PaddingBottom = 3;
                                        skillCell.PaddingTop = 3;
                                        skillsSection.AddCell(skillCell);
                                    }
                                }
                            }
                            else
                            {
                                PdfPCell skillCell = new PdfPCell(new Phrase("No skills listed", skillFont));
                                skillCell.BackgroundColor = accentColor;
                                skillCell.Border = Rectangle.NO_BORDER;
                                skillCell.Padding = 5;
                                skillCell.PaddingBottom = 3;
                                skillCell.PaddingTop = 3;
                                skillsSection.AddCell(skillCell);
                            }
                        }
                    }
                }
                sidebarCell.AddElement(skillsSection);

                // Languages
                PdfPTable languagesSection = new PdfPTable(1);
                languagesSection.WidthPercentage = 100;

                PdfPCell languagesTitleCell = new PdfPCell(new Phrase("Languages",
                    FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, accentColor)));
                languagesTitleCell.Border = Rectangle.NO_BORDER;
                languagesSection.AddCell(languagesTitleCell);

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
                                        PdfPCell langCell = new PdfPCell(new Phrase(language, skillFont));
                                        langCell.BackgroundColor = accentColor;
                                        langCell.Border = Rectangle.NO_BORDER;
                                        langCell.Padding = 5;
                                        langCell.PaddingBottom = 3;
                                        langCell.PaddingTop = 3;
                                        languagesSection.AddCell(langCell);
                                    }
                                }
                            }
                            else
                            {
                                PdfPCell langCell = new PdfPCell(new Phrase("No languages listed", skillFont));
                                langCell.BackgroundColor = accentColor;
                                langCell.Border = Rectangle.NO_BORDER;
                                langCell.Padding = 5;
                                langCell.PaddingBottom = 3;
                                langCell.PaddingTop = 3;
                                languagesSection.AddCell(langCell);
                            }
                        }
                    }
                }
                sidebarCell.AddElement(languagesSection);

                // Hobbies
                PdfPTable hobbiesSection = new PdfPTable(1);
                hobbiesSection.WidthPercentage = 100;

                PdfPCell hobbiesTitleCell = new PdfPCell(new Phrase("Hobbies",
                    FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, accentColor)));
                hobbiesTitleCell.Border = Rectangle.NO_BORDER;
                hobbiesSection.AddCell(hobbiesTitleCell);

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
                                        PdfPCell hobbyCell = new PdfPCell(new Phrase(hobby, skillFont));
                                        hobbyCell.BackgroundColor = accentColor;
                                        hobbyCell.Border = Rectangle.NO_BORDER;
                                        hobbyCell.Padding = 5;
                                        hobbyCell.PaddingBottom = 3;
                                        hobbyCell.PaddingTop = 3;
                                        hobbiesSection.AddCell(hobbyCell);
                                    }
                                }
                            }
                            else
                            {
                                PdfPCell hobbyCell = new PdfPCell(new Phrase("No hobbies listed", skillFont));
                                hobbyCell.BackgroundColor = accentColor;
                                hobbyCell.Border = Rectangle.NO_BORDER;
                                hobbyCell.Padding = 5;
                                hobbyCell.PaddingBottom = 3;
                                hobbyCell.PaddingTop = 3;
                                hobbiesSection.AddCell(hobbyCell);
                            }
                        }
                    }
                }
                sidebarCell.AddElement(hobbiesSection);

                mainTable.AddCell(sidebarCell);

                // ========== MAIN CONTENT COLUMN ==========
                PdfPCell mainContentCell = new PdfPCell();
                mainContentCell.BackgroundColor = mainBgColor;
                mainContentCell.Border = Rectangle.NO_BORDER;
                mainContentCell.Padding = 12;

                // Professional Summary
                string summary = "";
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT PersonalSummary FROM PersonalDetails WHERE UserID = @UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        summary = cmd.ExecuteScalar()?.ToString() ?? text2ProfessionalSummary.Text?.Trim() ?? "";
                    }
                }
                AddMainSection(mainContentCell, "Professional Summary", summary,
                    sectionFont, entryContentFont, accentColor, entryBgColor);

                // Employment History
                PdfPTable employmentTable = new PdfPTable(1);
                employmentTable.WidthPercentage = 100;

                PdfPCell employmentTitleCell = new PdfPCell(new Phrase("Employment History", sectionFont));
                employmentTitleCell.Border = Rectangle.BOTTOM_BORDER;
                employmentTitleCell.BorderColor = accentColor;
                employmentTitleCell.BorderWidth = 2;
                employmentTitleCell.PaddingBottom = 6;
                employmentTable.AddCell(employmentTitleCell);

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
                    PdfPCell fresherCell = new PdfPCell(new Phrase("Fresher (No work experience)", entryContentFont));
                    fresherCell.Border = Rectangle.NO_BORDER;
                    fresherCell.BackgroundColor = entryBgColor;
                    fresherCell.Padding = 7;
                    fresherCell.BorderWidthLeft = 4;
                    fresherCell.BorderColorLeft = accentColor;
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
                                    PdfPCell entryCell = new PdfPCell(new Phrase("No employment history found.", entryContentFont));
                                    entryCell.BackgroundColor = entryBgColor;
                                    entryCell.BorderWidthLeft = 4;
                                    entryCell.BorderColorLeft = accentColor;
                                    entryCell.Padding = 7;
                                    entryCell.Border = Rectangle.NO_BORDER;
                                    employmentTable.AddCell(entryCell);
                                }
                                else
                                {
                                    while (reader.Read())
                                    {
                                        PdfPCell entryCell = new PdfPCell();
                                        entryCell.BackgroundColor = entryBgColor;
                                        entryCell.BorderWidthLeft = 4;
                                        entryCell.BorderColorLeft = accentColor;
                                        entryCell.Padding = 7;
                                        entryCell.Border = Rectangle.NO_BORDER;

                                        string jobTitleText = reader["JobTitle"]?.ToString() ?? "";
                                        string employer = reader["Employer"]?.ToString() ?? "";
                                        string cityText = reader["City"]?.ToString() ?? "";
                                        string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                        string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                        string description = reader["Description"]?.ToString() ?? "";

                                        string employmentText = $"I worked as {jobTitleText} at {employer}, {cityText} between {startDate} and {endDate}. {description}";
                                        entryCell.AddElement(new Paragraph(employmentText, entryContentFont));

                                        employmentTable.AddCell(entryCell);
                                        employmentTable.AddCell(new PdfPCell(new Phrase(" ")) { Border = Rectangle.NO_BORDER, Padding = 3 });
                                    }
                                }
                            }
                        }
                    }
                }
                mainContentCell.AddElement(employmentTable);

                // Education
                PdfPTable educationTable = new PdfPTable(1);
                educationTable.WidthPercentage = 100;

                PdfPCell educationTitleCell = new PdfPCell(new Phrase("Education", sectionFont));
                educationTitleCell.Border = Rectangle.BOTTOM_BORDER;
                educationTitleCell.BorderColor = accentColor;
                educationTitleCell.BorderWidth = 2;
                educationTitleCell.PaddingBottom = 6;
                educationTable.AddCell(educationTitleCell);

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
                                PdfPCell entryCell = new PdfPCell(new Phrase("No education found.", entryContentFont));
                                entryCell.BackgroundColor = entryBgColor;
                                entryCell.BorderWidthLeft = 4;
                                entryCell.BorderColorLeft = accentColor;
                                entryCell.Padding = 7;
                                entryCell.Border = Rectangle.NO_BORDER;
                                educationTable.AddCell(entryCell);
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    PdfPCell entryCell = new PdfPCell();
                                    entryCell.BackgroundColor = entryBgColor;
                                    entryCell.BorderWidthLeft = 4;
                                    entryCell.BorderColorLeft = accentColor;
                                    entryCell.Padding = 7;
                                    entryCell.Border = Rectangle.NO_BORDER;

                                    string degree = reader["Degree"]?.ToString() ?? "";
                                    string schoolName = reader["SchoolName"]?.ToString() ?? "";
                                    string cityText = reader["City"]?.ToString() ?? "";
                                    string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                    string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                    string description = reader["Description"]?.ToString() ?? "";

                                    string educationText = $"I completed my {degree} from {schoolName}, {cityText} between {startDate} and {endDate}. {description}";
                                    entryCell.AddElement(new Paragraph(educationText, entryContentFont));

                                    educationTable.AddCell(entryCell);
                                    educationTable.AddCell(new PdfPCell(new Phrase(" ")) { Border = Rectangle.NO_BORDER, Padding = 3 });
                                }
                            }
                        }
                    }
                }
                mainContentCell.AddElement(educationTable);

                // Certifications
                PdfPTable certTable = new PdfPTable(1);
                certTable.WidthPercentage = 100;

                PdfPCell certTitleCell = new PdfPCell(new Phrase("Certifications", sectionFont));
                certTitleCell.Border = Rectangle.BOTTOM_BORDER;
                certTitleCell.BorderColor = accentColor;
                certTitleCell.BorderWidth = 2;
                certTitleCell.PaddingBottom = 6;
                certTable.AddCell(certTitleCell);

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
                                PdfPCell entryCell = new PdfPCell(new Phrase("No certifications found.", entryContentFont));
                                entryCell.BackgroundColor = entryBgColor;
                                entryCell.BorderWidthLeft = 4;
                                entryCell.BorderColorLeft = accentColor;
                                entryCell.Padding = 7;
                                entryCell.Border = Rectangle.NO_BORDER;
                                certTable.AddCell(entryCell);
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    PdfPCell entryCell = new PdfPCell();
                                    entryCell.BackgroundColor = entryBgColor;
                                    entryCell.BorderWidthLeft = 4;
                                    entryCell.BorderColorLeft = accentColor;
                                    entryCell.Padding = 7;
                                    entryCell.Border = Rectangle.NO_BORDER;

                                    string courseName = reader["CourseName"]?.ToString() ?? "";
                                    string institution = reader["Institution"]?.ToString() ?? "";
                                    string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                    string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";

                                    string certText = $"I completed {courseName} from {institution} between {startDate} and {endDate}.";
                                    entryCell.AddElement(new Paragraph(certText, entryContentFont));

                                    certTable.AddCell(entryCell);
                                    certTable.AddCell(new PdfPCell(new Phrase(" ")) { Border = Rectangle.NO_BORDER, Padding = 3 });
                                }
                            }
                        }
                    }
                }
                mainContentCell.AddElement(certTable);

                // Internships
                PdfPTable internTable = new PdfPTable(1);
                internTable.WidthPercentage = 100;

                PdfPCell internTitleCell = new PdfPCell(new Phrase("Internships", sectionFont));
                internTitleCell.Border = Rectangle.BOTTOM_BORDER;
                internTitleCell.BorderColor = accentColor;
                internTitleCell.BorderWidth = 2;
                internTitleCell.PaddingBottom = 6;
                internTable.AddCell(internTitleCell);

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
                                PdfPCell entryCell = new PdfPCell();
                                entryCell.BackgroundColor = entryBgColor;
                                entryCell.BorderWidthLeft = 4;
                                entryCell.BorderColorLeft = accentColor;
                                entryCell.Padding = 7;
                                entryCell.Border = Rectangle.NO_BORDER;

                                string company = reader["CompanyName"]?.ToString() ?? "";
                                string jobTitleText = reader["JobTitle"]?.ToString() ?? "";
                                string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                string description = reader["Description"]?.ToString() ?? "";

                                string internText = $"I worked as {jobTitleText} at {company} between {startDate} and {endDate}. {description}";
                                entryCell.AddElement(new Paragraph(internText, entryContentFont));

                                internTable.AddCell(entryCell);
                            }
                            else
                            {
                                PdfPCell entryCell = new PdfPCell(new Phrase("No internship data available.", entryContentFont));
                                entryCell.BackgroundColor = entryBgColor;
                                entryCell.BorderWidthLeft = 4;
                                entryCell.BorderColorLeft = accentColor;
                                entryCell.Padding = 7;
                                entryCell.Border = Rectangle.NO_BORDER;
                                internTable.AddCell(entryCell);
                            }
                        }
                    }
                }
                mainContentCell.AddElement(internTable);

                // Additional Information
                PdfPTable customTable = new PdfPTable(1);
                customTable.WidthPercentage = 100;

                PdfPCell customTitleCell = new PdfPCell(new Phrase("Additional Information", sectionFont));
                customTitleCell.Border = Rectangle.BOTTOM_BORDER;
                customTitleCell.BorderColor = accentColor;
                customTitleCell.BorderWidth = 2;
                customTitleCell.PaddingBottom = 6;
                customTable.AddCell(customTitleCell);

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
                                PdfPCell entryCell = new PdfPCell(new Phrase("No additional information.", entryContentFont));
                                entryCell.BackgroundColor = entryBgColor;
                                entryCell.BorderWidthLeft = 4;
                                entryCell.BorderColorLeft = accentColor;
                                entryCell.Padding = 7;
                                entryCell.Border = Rectangle.NO_BORDER;
                                customTable.AddCell(entryCell);
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    PdfPCell entryCell = new PdfPCell();
                                    entryCell.BackgroundColor = entryBgColor;
                                    entryCell.BorderWidthLeft = 4;
                                    entryCell.BorderColorLeft = accentColor;
                                    entryCell.Padding = 7;
                                    entryCell.Border = Rectangle.NO_BORDER;

                                    string title = reader["Title"]?.ToString() ?? "";
                                    string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                    string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                    string description = reader["Description"]?.ToString() ?? "";

                                    string customText = $"I worked on {title} between {startDate} and {endDate}. {description}";
                                    entryCell.AddElement(new Paragraph(customText, entryContentFont));

                                    customTable.AddCell(entryCell);
                                    customTable.AddCell(new PdfPCell(new Phrase(" ")) { Border = Rectangle.NO_BORDER, Padding = 3 });
                                }
                            }
                        }
                    }
                }
                mainContentCell.AddElement(customTable);

                mainTable.AddCell(mainContentCell);
                pdfDoc.Add(mainTable);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding content to PDF: {ex.Message}\nStack Trace: {ex.StackTrace}", ex);
            }
        }

        private void AddSidebarSection(PdfPCell sidebarCell, string title, string[] items, BaseColor accentColor, Font font)
        {
            PdfPTable sectionTable = new PdfPTable(1);
            sectionTable.WidthPercentage = 100;

            PdfPCell titleCell = new PdfPCell(new Phrase(title,
                FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11, accentColor)));
            titleCell.Border = Rectangle.NO_BORDER;
            titleCell.PaddingBottom = 6;
            sectionTable.AddCell(titleCell);

            foreach (string item in items)
            {
                PdfPCell itemCell = new PdfPCell(new Phrase(item, font));
                itemCell.Border = Rectangle.NO_BORDER;
                itemCell.PaddingLeft = 10;
                sectionTable.AddCell(itemCell);
            }

            sectionTable.SpacingAfter = 12;
            sidebarCell.AddElement(sectionTable);
        }

        private void AddMainSection(PdfPCell mainCell, string title, string content, Font titleFont, Font contentFont, BaseColor borderColor, BaseColor bgColor)
        {
            PdfPTable sectionTable = new PdfPTable(1);
            sectionTable.WidthPercentage = 100;
            sectionTable.SpacingAfter = 12;

            PdfPCell titleCell = new PdfPCell(new Phrase(title, titleFont));
            titleCell.Border = Rectangle.BOTTOM_BORDER;
            titleCell.BorderColor = borderColor;
            titleCell.BorderWidth = 2;
            titleCell.PaddingBottom = 6;
            sectionTable.AddCell(titleCell);

            PdfPCell contentCell = new PdfPCell(new Phrase(content, contentFont));
            contentCell.BackgroundColor = bgColor;
            contentCell.BorderWidthLeft = 4;
            contentCell.BorderColorLeft = borderColor;
            contentCell.Padding = 7;
            contentCell.Border = Rectangle.NO_BORDER;
            sectionTable.AddCell(contentCell);

            mainCell.AddElement(sectionTable);
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

        private object Eval(string fieldName, RepeaterItem item)
        {
            return DataBinder.Eval(item.DataItem, fieldName);
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

    }
}
