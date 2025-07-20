using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Diagnostics;

namespace NareshResumeCreation
{
    public partial class Resume4 : Page
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
                    text4JobTitle.Text = reader["JobTitle"].ToString();
                    text4FirstName.Text = reader["FirstName"].ToString();
                    text4LastName.Text = reader["LastName"].ToString();
                    text4Email.Text = reader["Email"].ToString();
                    text4Phone.Text = reader["Phone"].ToString();
                    text4DateOfBirth.Text = Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                    text4Country.Text = reader["Country"].ToString();
                    text4State.Text = reader["State"].ToString();
                    text4City.Text = reader["City"].ToString();
                    text4Address.Text = reader["Address"].ToString();
                    text4PostalCode.Text = reader["PostalCode"].ToString();
                    text4Nationality.Text = reader["Nationality"].ToString();
                    text4PlaceOfBirth.Text = reader["PlaceOfBirth"].ToString();
                    text4ProfessionalSummary.Text = reader["PersonalSummary"].ToString();
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
            text4JobTitle.Text = "No Data";
            text4FirstName.Text = "No Data";
            text4LastName.Text = "No Data";
            text4Email.Text = "No Data";
            text4Phone.Text = "No Data";
            text4DateOfBirth.Text = "No Data";
            text4Country.Text = "No Data";
            text4State.Text = "No Data";
            text4City.Text = "No Data";
            text4Address.Text = "No Data";
            text4PostalCode.Text = "No Data";
            text4Nationality.Text = "No Data";
            text4PlaceOfBirth.Text = "No Data";
            text4ProfessionalSummary.Text = "No Data";
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
                    pnl4Fresher.Visible = isFresher;
                    pnl4EmploymentHistory.Visible = !isFresher;
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
                    rpt4EmploymentHistory.DataSource = dt;
                    rpt4EmploymentHistory.DataBind();
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
                        rpt4Education.DataSource = dt;
                        rpt4Education.DataBind();
                    }
                    else
                    {
                        rpt4Education.DataSource = new[] { new { Degree = "No education found", SchoolName = "", City = "", StartDate = "N/A", EndDate = "N/A", Description = "" } };
                        rpt4Education.DataBind();
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
                        rpt4Skills.DataSource = dt;
                        rpt4Skills.DataBind();
                    }
                    else
                    {
                        rpt4Skills.DataSource = new[] { new { SkillName = "No skills found" } };
                        rpt4Skills.DataBind();
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
                        text4InternCompany.Text = reader["CompanyName"].ToString();
                        text4InternJobTitle.Text = reader["JobTitle"].ToString();
                        text4InternStartDate.Text = Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy");
                        text4InternEndDate.Text = Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy");
                        text4InternDescription.Text = reader["Description"].ToString();
                    }
                    else
                    {
                        text4InternCompany.Text = "N/A";
                        text4InternJobTitle.Text = "N/A";
                        text4InternStartDate.Text = "N/A";
                        text4InternEndDate.Text = "N/A";
                        text4InternDescription.Text = "No internship data available";
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
                        rpt4Certifications.DataSource = dt;
                        rpt4Certifications.DataBind();
                    }
                    else
                    {
                        rpt4Certifications.DataSource = new[] { new { CourseName = "No certifications found", Institution = "", StartDate = "N/A", EndDate = "N/A" } };
                        rpt4Certifications.DataBind();
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
                        rpt4Languages.DataSource = dt;
                        rpt4Languages.DataBind();
                    }
                    else
                    {
                        rpt4Languages.DataSource = new[] { new { LanguageName = "No languages found" } };
                        rpt4Languages.DataBind();
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
                        rpt4Hobbies.DataSource = dt;
                        rpt4Hobbies.DataBind();
                    }
                    else
                    {
                        rpt4Hobbies.DataSource = new[] { new { Name = "No hobbies found" } };
                        rpt4Hobbies.DataBind();
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
                        rpt4CustomSelection.DataSource = dt;
                        rpt4CustomSelection.DataBind();
                    }
                    else
                    {
                        rpt4CustomSelection.DataSource = new[] { new { Title = "No additional info", StartDate = "N/A", EndDate = "N/A", Description = "" } };
                        rpt4CustomSelection.DataBind();
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
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
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
                    using (Document pdfDoc = new Document(PageSize.A4, 15, 15, 15, 15))
                    {
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms);
                        pdfDoc.Open();

                        AddResumeContentToPdf(pdfDoc, writer);

                        pdfDoc.Close();

                        // Generate unique filename with UserID
                        string userId = Session["UserID"]?.ToString() ?? "Unknown";
                        string firstName = text4FirstName.Text.Replace(" ", "_");
                        string lastName = text4LastName.Text.Replace(" ", "_");
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

        private void AddResumeContentToPdf(Document pdfDoc, PdfWriter writer)
        {
            try
            {
                // Define fonts and colors to match Type 2 template
                Font headerFont = FontFactory.GetFont("Arial", 24, Font.BOLD, BaseColor.WHITE);
                Font subHeaderFont = FontFactory.GetFont("Arial", 14, Font.NORMAL, new BaseColor(255, 255, 255, 230));
                Font sectionTitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD, new BaseColor(46, 204, 113)); // #2ecc71
                Font normalFont = FontFactory.GetFont("Arial", 11, Font.NORMAL, new BaseColor(52, 73, 94)); // #34495e
                Font listItemFont = FontFactory.GetFont("Arial", 11, Font.NORMAL, new BaseColor(52, 73, 94));
                BaseColor headerBgColor = new BaseColor(46, 204, 113); // #2ecc71
                BaseColor leftColumnBgColor = new BaseColor(236, 240, 241); // #ecf0f1
                BaseColor listItemBgColor = new BaseColor(213, 245, 227); // #d5f5e3
                BaseColor entryBgColor = new BaseColor(249, 251, 252); // #f9fbfc
                BaseColor entryBorderColor = new BaseColor(46, 204, 113); // #2ecc71
                BaseColor pageBgColor = new BaseColor(240, 242, 245); // #f0f2f5

                // Log session data
                Debug.WriteLine($"Session[UserID]: {Session["UserID"] ?? "null"}");

                // Set page background
                PdfContentByte cb = writer.DirectContent;
                cb.SetColorFill(pageBgColor);
                cb.Rectangle(0, 0, PageSize.A4.Width, PageSize.A4.Height);
                cb.Fill();

                // Main container table
                PdfPTable containerTable = new PdfPTable(1);
                containerTable.WidthPercentage = 95;
                containerTable.HorizontalAlignment = Element.ALIGN_CENTER;
                containerTable.DefaultCell.BackgroundColor = BaseColor.WHITE;
                containerTable.DefaultCell.Border = Rectangle.NO_BORDER;
                containerTable.SpacingBefore = 10;
                containerTable.SpacingAfter = 10;

                // Header
                PdfPCell headerCell = new PdfPCell();
                headerCell.BackgroundColor = headerBgColor;
                headerCell.Padding = 15;
                headerCell.Border = Rectangle.NO_BORDER;
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT FirstName, LastName, JobTitle FROM PersonalDetails WHERE UserID = @UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"] ?? DBNull.Value);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string fullName = $"{reader["FirstName"]} {reader["LastName"]}".Trim();
                                Paragraph namePara = new Paragraph(fullName, headerFont);
                                namePara.SpacingAfter = 5;
                                headerCell.AddElement(namePara);
                                string jobTitle = reader["JobTitle"]?.ToString()?.Trim() ?? "N/A";
                                headerCell.AddElement(new Paragraph(jobTitle, subHeaderFont));
                                Debug.WriteLine($"Header: Name={fullName}, JobTitle={jobTitle}");
                            }
                            else
                            {
                                headerCell.AddElement(new Paragraph("User Name", headerFont));
                                headerCell.AddElement(new Paragraph("N/A", subHeaderFont));
                                Debug.WriteLine("Header: No personal details found");
                            }
                        }
                    }
                }
                containerTable.AddCell(headerCell);

                // Two-column content wrapper
                PdfPTable contentTable = new PdfPTable(2);
                contentTable.WidthPercentage = 100;
                contentTable.SetWidths(new float[] { 1f, 2f });
                contentTable.DefaultCell.Border = Rectangle.NO_BORDER;
                contentTable.DefaultCell.Padding = 10;

                // Left Column
                PdfPCell leftColumnCell = new PdfPCell();
                leftColumnCell.BackgroundColor = leftColumnBgColor;
                leftColumnCell.Padding = 10;
                leftColumnCell.Border = Rectangle.NO_BORDER;

                // Contact Information
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT Email, Phone, Address, City, State, PostalCode, Country, DateOfBirth, PlaceOfBirth, Nationality FROM PersonalDetails WHERE UserID = @UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"] ?? DBNull.Value);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                leftColumnCell.AddElement(CreateSectionTitle("Contact", sectionTitleFont));
                                leftColumnCell.AddElement(new Paragraph($"✉️ {reader["Email"]?.ToString()?.Trim() ?? "N/A"}", normalFont));
                                leftColumnCell.AddElement(new Paragraph($"📱 {reader["Phone"]?.ToString()?.Trim() ?? "N/A"}", normalFont));
                                string address = $"{reader["Address"]?.ToString()?.Trim() ?? ""}, {reader["City"]?.ToString()?.Trim() ?? ""}, {reader["State"]?.ToString()?.Trim() ?? ""}, {reader["PostalCode"]?.ToString()?.Trim() ?? ""}, {reader["Country"]?.ToString()?.Trim() ?? ""}".Trim(',', ' ');
                                leftColumnCell.AddElement(new Paragraph($"🏠 {address}", normalFont));
                                leftColumnCell.AddElement(CreateSpacer(normalFont, 10));

                                leftColumnCell.AddElement(CreateSectionTitle("Personal Info", sectionTitleFont));
                                string dob = reader["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd") : "N/A";
                                leftColumnCell.AddElement(new Paragraph($"🎂 {dob}", normalFont));
                                leftColumnCell.AddElement(new Paragraph($"🌍 {reader["PlaceOfBirth"]?.ToString()?.Trim() ?? "N/A"}", normalFont));
                                leftColumnCell.AddElement(new Paragraph($"📍 {reader["Nationality"]?.ToString()?.Trim() ?? "N/A"}", normalFont));
                                leftColumnCell.AddElement(CreateSpacer(normalFont, 10));
                                Debug.WriteLine($"Contact: Email={reader["Email"]}, Phone={reader["Phone"]}, Address={address}");
                            }
                            else
                            {
                                Debug.WriteLine("Contact: No personal details found");
                            }
                        }
                    }
                }

                // Skills - Fixed implementation
                leftColumnCell.AddElement(CreateSectionTitle("Skills", sectionTitleFont));
                PdfPTable skillsTable = new PdfPTable(1);
                skillsTable.WidthPercentage = 100;

                foreach (RepeaterItem item in rpt4Skills.Items)
                {
                    Literal litSkill = item.FindControl("lit4Skill") as Literal;
                    if (litSkill != null && !string.IsNullOrWhiteSpace(litSkill.Text))
                    {
                        PdfPCell skillCell = new PdfPCell(new Phrase(litSkill.Text, listItemFont));
                        skillCell.BackgroundColor = listItemBgColor;
                        skillCell.Border = Rectangle.NO_BORDER;
                        skillCell.Padding = 5;
                        skillsTable.AddCell(skillCell);
                    }
                }
                leftColumnCell.AddElement(skillsTable);
                leftColumnCell.AddElement(CreateSpacer(listItemFont, 10f));


                // Languages - Fixed implementation
                leftColumnCell.AddElement(CreateSectionTitle("Languages", sectionTitleFont));
                PdfPTable languagesTable = new PdfPTable(1);
                languagesTable.WidthPercentage = 100;

                foreach (RepeaterItem item in rpt4Languages.Items)
                {
                    Literal litLanguage = item.FindControl("lit4Language") as Literal;
                    if (litLanguage != null && !string.IsNullOrWhiteSpace(litLanguage.Text))
                    {
                        PdfPCell languageCell = new PdfPCell(new Phrase(litLanguage.Text, listItemFont));
                        languageCell.BackgroundColor = listItemBgColor;
                        languageCell.Border = Rectangle.NO_BORDER;
                        languageCell.Padding = 5;
                        languagesTable.AddCell(languageCell);
                    }
                }
                leftColumnCell.AddElement(languagesTable);
                leftColumnCell.AddElement(CreateSpacer(listItemFont, 10f));


                // Hobbies - Fixed implementation
                leftColumnCell.AddElement(CreateSectionTitle("Hobbies", sectionTitleFont));
                PdfPTable hobbiesTable = new PdfPTable(1);
                hobbiesTable.WidthPercentage = 100;

                foreach (RepeaterItem item in rpt4Hobbies.Items)
                {
                    Literal litHobby = item.FindControl("lit4Hobby") as Literal;
                    if (litHobby != null && !string.IsNullOrWhiteSpace(litHobby.Text))
                    {
                        PdfPCell hobbyCell = new PdfPCell(new Phrase(litHobby.Text, listItemFont));
                        hobbyCell.BackgroundColor = listItemBgColor;
                        hobbyCell.Border = Rectangle.NO_BORDER;
                        hobbyCell.Padding = 5;
                        hobbiesTable.AddCell(hobbyCell);
                    }
                }
                leftColumnCell.AddElement(hobbiesTable);
                // Right Column
                PdfPCell rightColumnCell = new PdfPCell();
                rightColumnCell.Padding = 10;
                rightColumnCell.Border = Rectangle.NO_BORDER;

                // Professional Summary
                rightColumnCell.AddElement(CreateSectionTitle("Professional Summary", sectionTitleFont));
                PdfPTable summaryTable = CreateEntryTable();
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT PersonalSummary FROM PersonalDetails WHERE UserID = @UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"] ?? DBNull.Value);
                        string summary = cmd.ExecuteScalar()?.ToString()?.Trim() ?? "N/A";
                        summaryTable.AddCell(CreateEntryCell(summary, normalFont, entryBgColor, entryBorderColor));
                        Debug.WriteLine($"Professional Summary: {summary}");
                    }
                }
                rightColumnCell.AddElement(summaryTable);
                rightColumnCell.AddElement(CreateSpacer(normalFont, 10));

                // Employment History
                rightColumnCell.AddElement(CreateSectionTitle("Employment History", sectionTitleFont));
                PdfPTable employmentTable = CreateEntryTable();
                bool isFresher = false;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT IsFresher FROM Userss WHERE UserID = @UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"] ?? DBNull.Value);
                        object result = cmd.ExecuteScalar();
                        isFresher = result != null && Convert.ToBoolean(result);
                        Debug.WriteLine($"IsFresher: {isFresher}");
                    }
                    if (isFresher)
                    {
                        employmentTable.AddCell(CreateEntryCell("Fresher (No work experience)", normalFont, entryBgColor, entryBorderColor));
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT JobTitle, Employer, City, StartDate, EndDate, Description FROM EmploymentHistoryy WHERE UserID = @UserID ORDER BY StartDate DESC", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"] ?? DBNull.Value);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (!reader.HasRows)
                                {
                                    employmentTable.AddCell(CreateEntryCell("No employment history found.", normalFont, entryBgColor, entryBorderColor));
                                    Debug.WriteLine("Employment: No records found");
                                }
                                else
                                {
                                    while (reader.Read())
                                    {
                                        string jobTitle = reader["JobTitle"]?.ToString()?.Trim() ?? "N/A";
                                        string employer = reader["Employer"]?.ToString()?.Trim() ?? "N/A";
                                        string city = reader["City"]?.ToString()?.Trim() ?? "N/A";
                                        string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "N/A";
                                        string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                        string description = reader["Description"]?.ToString()?.Trim() ?? "";
                                        string employmentText = $"I worked as {jobTitle} at {employer}, {city} between {startDate} and {endDate}. {description}";
                                        employmentTable.AddCell(CreateEntryCell(employmentText, normalFont, entryBgColor, entryBorderColor));
                                        Debug.WriteLine($"Employment: {employmentText}");
                                    }
                                }
                            }
                        }
                    }
                }
                rightColumnCell.AddElement(employmentTable);
                rightColumnCell.AddElement(CreateSpacer(normalFont, 10));

                // Education
                rightColumnCell.AddElement(CreateSectionTitle("Education", sectionTitleFont));
                PdfPTable educationTable = CreateEntryTable();
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT Degree, SchoolName, City, StartDate, EndDate, Description FROM Education WHERE UserID = @UserID ORDER BY StartDate DESC", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"] ?? DBNull.Value);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                educationTable.AddCell(CreateEntryCell("No education found.", normalFont, entryBgColor, entryBorderColor));
                                Debug.WriteLine("Education: No records found");
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    string degree = reader["Degree"]?.ToString()?.Trim() ?? "N/A";
                                    string schoolName = reader["SchoolName"]?.ToString()?.Trim() ?? "N/A";
                                    string city = reader["City"]?.ToString()?.Trim() ?? "N/A";
                                    string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "N/A";
                                    string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                    string description = reader["Description"]?.ToString()?.Trim() ?? "";
                                    string educationText = $"I completed my {degree} from {schoolName}, {city} between {startDate} and {endDate}. {description}";
                                    educationTable.AddCell(CreateEntryCell(educationText, normalFont, entryBgColor, entryBorderColor));
                                    Debug.WriteLine($"Education: {educationText}");
                                }
                            }
                        }
                    }
                }
                rightColumnCell.AddElement(educationTable);
                rightColumnCell.AddElement(CreateSpacer(normalFont, 10));

                // Certifications
                rightColumnCell.AddElement(CreateSectionTitle("Certifications", sectionTitleFont));
                PdfPTable certificationsTable = CreateEntryTable();
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT CourseName, Institution, StartDate, EndDate FROM Courses WHERE UserID = @UserID ORDER BY StartDate DESC", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"] ?? DBNull.Value);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                certificationsTable.AddCell(CreateEntryCell("No certifications found.", normalFont, entryBgColor, entryBorderColor));
                                Debug.WriteLine("Certifications: No records found");
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    string courseName = reader["CourseName"]?.ToString()?.Trim() ?? "N/A";
                                    string institution = reader["Institution"]?.ToString()?.Trim() ?? "N/A";
                                    string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "N/A";
                                    string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                    string certText = $"I completed {courseName} from {institution} between {startDate} and {endDate}.";
                                    certificationsTable.AddCell(CreateEntryCell(certText, normalFont, entryBgColor, entryBorderColor));
                                    Debug.WriteLine($"Certification: {certText}");
                                }
                            }
                        }
                    }
                }
                rightColumnCell.AddElement(certificationsTable);
                rightColumnCell.AddElement(CreateSpacer(normalFont, 10));

                // Internships
                rightColumnCell.AddElement(CreateSectionTitle("Internships", sectionTitleFont));
                PdfPTable internshipsTable = CreateEntryTable();
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT TOP 1 CompanyName, JobTitle, StartDate, EndDate, Description FROM Internships WHERE UserID = @UserID ORDER BY ID DESC", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"] ?? DBNull.Value);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string company = reader["CompanyName"]?.ToString()?.Trim() ?? "N/A";
                                string jobTitle = reader["JobTitle"]?.ToString()?.Trim() ?? "N/A";
                                string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "N/A";
                                string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                string description = reader["Description"]?.ToString()?.Trim() ?? "";
                                string internshipText = $"I worked as {jobTitle} at {company} between {startDate} and {endDate}. {description}";
                                internshipsTable.AddCell(CreateEntryCell(internshipText, normalFont, entryBgColor, entryBorderColor));
                                Debug.WriteLine($"Internship: {internshipText}");
                            }
                            else
                            {
                                internshipsTable.AddCell(CreateEntryCell("No internship data available.", normalFont, entryBgColor, entryBorderColor));
                                Debug.WriteLine("Internship: No records found");
                            }
                        }
                    }
                }
                rightColumnCell.AddElement(internshipsTable);
                rightColumnCell.AddElement(CreateSpacer(normalFont, 10));

                // Additional Information
                rightColumnCell.AddElement(CreateSectionTitle("Additional Information", sectionTitleFont));
                PdfPTable customTable = CreateEntryTable();
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT Title, StartDate, EndDate, Description FROM CustomSelection WHERE UserID = @UserID ORDER BY StartDate DESC", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"] ?? DBNull.Value);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                customTable.AddCell(CreateEntryCell("No additional information.", normalFont, entryBgColor, entryBorderColor));
                                Debug.WriteLine("CustomSelection: No records found");
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    string title = reader["Title"]?.ToString()?.Trim() ?? "N/A";
                                    string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "N/A";
                                    string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                    string description = reader["Description"]?.ToString()?.Trim() ?? "";
                                    string customText = $"I worked on {title} between {startDate} and {endDate}. {description}";
                                    customTable.AddCell(CreateEntryCell(customText, normalFont, entryBgColor, entryBorderColor));
                                    Debug.WriteLine($"CustomSelection: {customText}");
                                }
                            }
                        }
                    }
                }
                rightColumnCell.AddElement(customTable);

                // Add columns to content table
                contentTable.AddCell(leftColumnCell);
                contentTable.AddCell(rightColumnCell);

                // Add content table to container
                PdfPCell contentCell = new PdfPCell(contentTable);
                contentCell.BackgroundColor = BaseColor.WHITE;
                contentCell.Border = Rectangle.NO_BORDER;
                containerTable.AddCell(contentCell);

                // Add container to document with split handling
                containerTable.SplitLate = false;
                containerTable.KeepTogether = false;
                pdfDoc.Add(containerTable);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PDF Error: {ex.Message}\nStack Trace: {ex.StackTrace}");
                throw new Exception($"Error adding content to PDF: {ex.Message}", ex);
            }
        }

        // Helper methods
        private Paragraph CreateSectionTitle(string title, Font font)
        {
            Paragraph para = new Paragraph(title, font);
            para.SpacingAfter = 8;
            return para;
        }

        private Paragraph CreateSpacer(Font font, float height)
        {
            Paragraph spacer = new Paragraph(" ", font);
            spacer.Leading = height;
            return spacer;
        }

        private PdfPTable CreateListTable()
        {
            PdfPTable table = new PdfPTable(1);
            table.WidthPercentage = 100;
            table.DefaultCell.Border = Rectangle.NO_BORDER;
            table.DefaultCell.Padding = 2;
            return table;
        }

        private PdfPCell CreateListItemCell(string text, Font font, BaseColor bgColor)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.BackgroundColor = bgColor;
            cell.Padding = 4;
            cell.Border = Rectangle.NO_BORDER;
            cell.FixedHeight = 18;
            return cell;
        }

        private PdfPTable CreateEntryTable()
        {
            PdfPTable table = new PdfPTable(1);
            table.WidthPercentage = 100;
            table.DefaultCell.Border = Rectangle.NO_BORDER;
            table.DefaultCell.Padding = 0;
            return table;
        }

        private PdfPCell CreateEntryCell(string text, Font font, BaseColor bgColor, BaseColor borderColor)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.BackgroundColor = bgColor;
            cell.BorderWidthLeft = 3;
            cell.BorderColorLeft = borderColor;
            cell.BorderWidthRight = 0;
            cell.BorderWidthTop = 0;
            cell.BorderWidthBottom = 0;
            cell.Padding = 8;
            cell.MinimumHeight = 20;
            return cell;
        }


        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}