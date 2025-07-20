using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Linq;
using System.Web;

namespace NareshResumeCreation
{
    public partial class Resume10 : Page
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
                    text10JobTitle.Text = reader["JobTitle"].ToString();
                    text10FirstName.Text = reader["FirstName"].ToString();
                    text10LastName.Text = reader["LastName"].ToString();
                    text10Email.Text = reader["Email"].ToString();
                    text10Phone.Text = reader["Phone"].ToString();
                    text10DateOfBirth.Text = Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                    text10Country.Text = reader["Country"].ToString();
                    text10State.Text = reader["State"].ToString();
                    text10City.Text = reader["City"].ToString();
                    text10Address.Text = reader["Address"].ToString();
                    text10PostalCode.Text = reader["PostalCode"].ToString();
                    text10Nationality.Text = reader["Nationality"].ToString();
                    text10PlaceOfBirth.Text = reader["PlaceOfBirth"].ToString();
                    text10ProfessionalSummary.Text = reader["PersonalSummary"].ToString();
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
            text10JobTitle.Text = "No Data";
            text10FirstName.Text = "No Data";
            text10LastName.Text = "No Data";
            text10Email.Text = "No Data";
            text10Phone.Text = "No Data";
            text10DateOfBirth.Text = "No Data";
            text10Country.Text = "No Data";
            text10State.Text = "No Data";
            text10City.Text = "No Data";
            text10Address.Text = "No Data";
            text10PostalCode.Text = "No Data";
            text10Nationality.Text = "No Data";
            text10PlaceOfBirth.Text = "No Data";
            text10ProfessionalSummary.Text = "No Data";
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
                    pnl10Fresher.Visible = isFresher;
                    pnl10EmploymentHistory.Visible = !isFresher;
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
                    rpt10EmploymentHistory.DataSource = dt;
                    rpt10EmploymentHistory.DataBind();
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
                        rpt10Education.DataSource = dt;
                        rpt10Education.DataBind();
                    }
                    else
                    {
                        rpt10Education.DataSource = new[] { new { Degree = "No education found", SchoolName = "", City = "", StartDate = DBNull.Value, EndDate = DBNull.Value, Description = "" } };
                        rpt10Education.DataBind();
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
                        rpt10Skills.DataSource = dt;
                        rpt10Skills.DataBind();
                    }
                    else
                    {
                        rpt10Skills.DataSource = new[] { new { SkillName = "No skills found" } };
                        rpt10Skills.DataBind();
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
                        text10InternCompany.Text = reader["CompanyName"].ToString();
                        text10InternJobTitle.Text = reader["JobTitle"].ToString();
                        text10InternStartDate.Text = Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy");
                        string endDate = reader["EndDate"] == DBNull.Value ? "Present" : Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy");
                        text10InternEndDate.Text = endDate == "Present" && text10InternStartDate.Text != "Present" ? "present" : endDate;
                        text10InternDescription.Text = FormatDescription(reader["Description"]);
                    }
                    else
                    {
                        text10InternCompany.Text = "N/A";
                        text10InternJobTitle.Text = "N/A";
                        text10InternStartDate.Text = "N/A";
                        text10InternEndDate.Text = "N/A";
                        text10InternDescription.Text = "No internship data available";
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
                        txtEditInternEndDate.Text = reader["EndDate"] == DBNull.Value ? "" : Convert.ToDateTime(reader["EndDate"]).ToString("yyyy-MM-dd");
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
                        rpt10Certifications.DataSource = dt;
                        rpt10Certifications.DataBind();
                    }
                    else
                    {
                        rpt10Certifications.DataSource = new[] { new { CourseName = "No certifications found", Institution = "", StartDate = DBNull.Value, EndDate = DBNull.Value } };
                        rpt10Certifications.DataBind();
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
                        rpt10Languages.DataSource = dt;
                        rpt10Languages.DataBind();
                    }
                    else
                    {
                        rpt10Languages.DataSource = new[] { new { LanguageName = "No languages found" } };
                        rpt10Languages.DataBind();
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
                        rpt10Hobbies.DataSource = dt;
                        rpt10Hobbies.DataBind();
                    }
                    else
                    {
                        rpt10Hobbies.DataSource = new[] { new { Name = "No hobbies found" } };
                        rpt10Hobbies.DataBind();
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
                        rpt10CustomSelection.DataSource = dt;
                        rpt10CustomSelection.DataBind();
                    }
                    else
                    {
                        rpt10CustomSelection.DataSource = new[] { new { Title = "No additional info", StartDate = DBNull.Value, EndDate = DBNull.Value, Description = "" } };
                        rpt10CustomSelection.DataBind();
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

                    string fresherQuery = "UPDATE Userss SET IsFresher = @IsFresher WHERE UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(fresherQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@IsFresher", chkFresher.Checked);
                        cmd.Parameters.AddWithValue("@UserTween Resume10 and Resume10.aspx.csID", Session["UserID"]);
                        cmd.ExecuteNonQuery();
                    }

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
                    string query = @"CLOSURE OF Resume10 and Resume10.aspx.csINSERT INTO CustomSelection (UserID, Title, StartDate, EndDate, Description)
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
            if (chkFresher.Checked)
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "DELETE FROM EmploymentHistoryy WHERE UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditEmploymentHistory();
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
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
                        string firstName = text10FirstName.Text.Replace(" ", "_");
                        string lastName = text10LastName.Text.Replace(" ", "_");
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
                // Fonts
                Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20, BaseColor.WHITE);
                Font jobTitleFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.WHITE);
                Font contactFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.WHITE);
                Font sectionFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14, new BaseColor(44, 62, 80));
                Font contentFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);
                Font boldContentFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.BLACK);

                // Helper method to create an underlined section title
                PdfPTable CreateUnderlinedTitle(string titleText, float spacingBefore)
                {
                    PdfPTable titleTable = new PdfPTable(1);
                    titleTable.WidthPercentage = 100;
                    titleTable.SpacingBefore = spacingBefore;

                    PdfPCell titleCell = new PdfPCell(new Phrase(titleText, sectionFont));
                    titleCell.Border = Rectangle.BOTTOM_BORDER;
                    titleCell.BorderWidthBottom = 1f;
                    titleCell.BorderColorBottom = new BaseColor(44, 62, 80);
                    titleCell.PaddingBottom = 5f;
                    titleCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    titleTable.AddCell(titleCell);

                    return titleTable;
                }

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
                                PdfPTable headerTable = new PdfPTable(1);
                                headerTable.WidthPercentage = 100;
                                PdfPCell headerCell = new PdfPCell();
                                headerCell.BackgroundColor = new BaseColor(44, 62, 80);
                                headerCell.Border = Rectangle.NO_BORDER;
                                headerCell.Padding = 10;

                                string fullName = $"{reader["FirstName"]} {reader["LastName"]}".Trim();
                                headerCell.AddElement(new Paragraph(fullName, headerFont) { Alignment = Element.ALIGN_LEFT });

                                string jobTitle = reader["JobTitle"]?.ToString() ?? "";
                                headerCell.AddElement(new Paragraph(jobTitle, jobTitleFont) { Alignment = Element.ALIGN_LEFT, SpacingBefore = 5 });

                                string contact = $"Email: {reader["Email"]?.ToString() ?? ""} | Phone: {reader["Phone"]?.ToString() ?? ""}";
                                headerCell.AddElement(new Paragraph(contact, contactFont) { Alignment = Element.ALIGN_LEFT, SpacingBefore = 5 });

                                string address = $"{reader["Address"]?.ToString() ?? ""}, {reader["City"]?.ToString() ?? ""}, {reader["State"]?.ToString() ?? ""}, {reader["PostalCode"]?.ToString() ?? ""}, {reader["Country"]?.ToString() ?? ""}";
                                headerCell.AddElement(new Paragraph(address, contactFont) { Alignment = Element.ALIGN_LEFT, SpacingBefore = 5 });

                                string dob = reader["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd") : "";
                                string personalDetails = $"Date of Birth: {dob} | Place of Birth: {reader["PlaceOfBirth"]?.ToString() ?? ""} | Nationality: {reader["Nationality"]?.ToString() ?? ""}";
                                headerCell.AddElement(new Paragraph(personalDetails, contactFont) { Alignment = Element.ALIGN_LEFT, SpacingBefore = 5 });

                                headerTable.AddCell(headerCell);
                                pdfDoc.Add(headerTable);
                            }
                        }
                    }
                }

                // Professional Summary
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT PersonalSummary FROM PersonalDetails WHERE UserID = @UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        string summary = cmd.ExecuteScalar()?.ToString() ?? "";
                        pdfDoc.Add(CreateUnderlinedTitle("Professional Summary", 15));
                        pdfDoc.Add(new Paragraph(summary, contentFont) { SpacingBefore = 5, SpacingAfter = 10 });
                    }
                }

                // Skills, Languages, Hobbies Side by Side
                PdfPTable sideBySideTable = new PdfPTable(3);
                sideBySideTable.WidthPercentage = 100;
                sideBySideTable.SetWidths(new float[] { 1f, 1f, 1f });
                sideBySideTable.SpacingBefore = 10;

                // Skills Column
                PdfPCell skillsCell = new PdfPCell();
                skillsCell.Border = Rectangle.NO_BORDER;
                PdfPTable skillsTitleTable = CreateUnderlinedTitle("Skills", 0);
                skillsCell.AddElement(skillsTitleTable);
                List skillsList = new List(List.UNORDERED, 10f);
                skillsList.SetListSymbol("• ");

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
                                        skillsList.Add(new iTextSharp.text.ListItem(skill, contentFont));
                                    }
                                }
                            }
                            else
                            {
                                skillsList.Add(new iTextSharp.text.ListItem("No skills listed", contentFont));
                            }
                        }
                    }
                }
                skillsCell.AddElement(skillsList);
                sideBySideTable.AddCell(skillsCell);

                // Languages Column
                PdfPCell languagesCell = new PdfPCell();
                languagesCell.Border = Rectangle.NO_BORDER;
                PdfPTable languagesTitleTable = CreateUnderlinedTitle("Languages", 0);
                languagesCell.AddElement(languagesTitleTable);
                List languagesList = new List(List.UNORDERED, 10f);
                languagesList.SetListSymbol("• ");

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
                                        languagesList.Add(new iTextSharp.text.ListItem(language, contentFont));
                                    }
                                }
                            }
                            else
                            {
                                languagesList.Add(new iTextSharp.text.ListItem("No languages listed", contentFont));
                            }
                        }
                    }
                }
                languagesCell.AddElement(languagesList);
                sideBySideTable.AddCell(languagesCell);

                // Hobbies Column
                PdfPCell hobbiesCell = new PdfPCell();
                hobbiesCell.Border = Rectangle.NO_BORDER;
                PdfPTable hobbiesTitleTable = CreateUnderlinedTitle("Hobbies", 0);
                hobbiesCell.AddElement(hobbiesTitleTable);
                List hobbiesList = new List(List.UNORDERED, 10f);
                hobbiesList.SetListSymbol("• ");

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
                                        hobbiesList.Add(new iTextSharp.text.ListItem(hobby, contentFont));
                                    }
                                }
                            }
                            else
                            {
                                hobbiesList.Add(new iTextSharp.text.ListItem("No hobbies listed", contentFont));
                            }
                        }
                    }
                }
                hobbiesCell.AddElement(hobbiesList);
                sideBySideTable.AddCell(hobbiesCell);

                pdfDoc.Add(sideBySideTable);

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

                pdfDoc.Add(CreateUnderlinedTitle("Employment History", 15));
                if (isFresher)
                {
                    pdfDoc.Add(new Paragraph("I am a fresher with no work experience.", contentFont) { SpacingBefore = 5, SpacingAfter = 10 });
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
                                    pdfDoc.Add(new Paragraph("No employment history found.", contentFont) { SpacingBefore = 5, SpacingAfter = 10 });
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

                                        Paragraph empPara = new Paragraph();
                                        empPara.Add(new Chunk("I worked as a ", contentFont));
                                        empPara.Add(new Chunk(jobTitle, boldContentFont));
                                        empPara.Add(new Chunk($" at {employer}, {city} from {startDate} to {endDate}. {description}", contentFont));
                                        empPara.SpacingBefore = 5;
                                        empPara.SpacingAfter = 5;
                                        pdfDoc.Add(empPara);
                                    }
                                }
                            }
                        }
                    }
                }

                // Education
                pdfDoc.Add(CreateUnderlinedTitle("Education", 10));
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
                                pdfDoc.Add(new Paragraph("No education found.", contentFont) { SpacingBefore = 5, SpacingAfter = 10 });
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

                                    Paragraph eduPara = new Paragraph();
                                    eduPara.Add(new Chunk("I completed my ", contentFont));
                                    eduPara.Add(new Chunk(degree, boldContentFont));
                                    eduPara.Add(new Chunk($" from {schoolName}, {city} from {startDate} to {endDate}. {description}", contentFont));
                                    eduPara.SpacingBefore = 5;
                                    eduPara.SpacingAfter = 5;
                                    pdfDoc.Add(eduPara);
                                }
                            }
                        }
                    }
                }

                // Certifications
                pdfDoc.Add(CreateUnderlinedTitle("Certifications", 10));
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
                                pdfDoc.Add(new Paragraph("No certifications found.", contentFont) { SpacingBefore = 5, SpacingAfter = 10 });
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    string courseName = reader["CourseName"]?.ToString() ?? "";
                                    string institution = reader["Institution"]?.ToString() ?? "";
                                    string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                    string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";

                                    Paragraph certPara = new Paragraph();
                                    certPara.Add(new Chunk("I earned a ", contentFont));
                                    certPara.Add(new Chunk(courseName, boldContentFont));
                                    certPara.Add(new Chunk($" from {institution} from {startDate} to {endDate}.", contentFont));
                                    certPara.SpacingBefore = 5;
                                    certPara.SpacingAfter = 5;
                                    pdfDoc.Add(certPara);
                                }
                            }
                        }
                    }
                }

                // Internships
                pdfDoc.Add(CreateUnderlinedTitle("Internships", 10));
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

                                Paragraph internPara = new Paragraph();
                                internPara.Add(new Chunk("I interned as a ", contentFont));
                                internPara.Add(new Chunk(jobTitle, boldContentFont));
                                internPara.Add(new Chunk($" at {company} from {startDate} to {endDate}. {description}", contentFont));
                                internPara.SpacingBefore = 5;
                                internPara.SpacingAfter = 10;
                                pdfDoc.Add(internPara);
                            }
                            else
                            {
                                pdfDoc.Add(new Paragraph("No internship data available", contentFont) { SpacingBefore = 5, SpacingAfter = 10 });
                            }
                        }
                    }
                }

                // Additional Information
                pdfDoc.Add(CreateUnderlinedTitle("Additional Information", 10));
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
                                pdfDoc.Add(new Paragraph("No additional information.", contentFont) { SpacingBefore = 5, SpacingAfter = 10 });
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    string title = reader["Title"]?.ToString() ?? "";
                                    string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                    string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                    string description = reader["Description"]?.ToString() ?? "";

                                    Paragraph customPara = new Paragraph();
                                    customPara.Add(new Chunk("I engaged in ", contentFont));
                                    customPara.Add(new Chunk(title, boldContentFont));
                                    customPara.Add(new Chunk($" from {startDate} to {endDate}. {description}", contentFont));
                                    customPara.SpacingBefore = 5;
                                    customPara.SpacingAfter = 5;
                                    pdfDoc.Add(customPara);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding content to PDF: {ex.Message}\nStack Trace: {ex.StackTrace}", ex);
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

        protected string FormatDate(object date)
        {
            if (date == DBNull.Value || date == null)
                return "Present";
            return Convert.ToDateTime(date).ToString("MMM yyyy");

        }


        protected string FormatDescription(object description)
        {
            return description == DBNull.Value || description == null ? "" : description.ToString();
        }
    }
}