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
    public partial class Resume6 : Page
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
                    text6JobTitle.Text = reader["JobTitle"].ToString();
                    text6FirstName.Text = reader["FirstName"].ToString();
                    text6LastName.Text = reader["LastName"].ToString();
                    text6Email.Text = reader["Email"].ToString();
                    text6Phone.Text = reader["Phone"].ToString();
                    text6DateOfBirth.Text = Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                    text6Country.Text = reader["Country"].ToString();
                    text6State.Text = reader["State"].ToString();
                    text6City.Text = reader["City"].ToString();
                    text6Address.Text = reader["Address"].ToString();
                    text6PostalCode.Text = reader["PostalCode"].ToString();
                    text6Nationality.Text = reader["Nationality"].ToString();
                    text6PlaceOfBirth.Text = reader["PlaceOfBirth"].ToString();
                    text6ProfessionalSummary.Text = reader["PersonalSummary"].ToString();
                }
                else
                {
                    text6JobTitle.Text = "No Data";
                    text6FirstName.Text = "No Data";
                    text6LastName.Text = "No Data";
                    text6Email.Text = "No Data";
                    text6Phone.Text = "No Data";
                    text6DateOfBirth.Text = "No Data";
                    text6Country.Text = "No Data";
                    text6State.Text = "No Data";
                    text6City.Text = "No Data";
                    text6Address.Text = "No Data";
                    text6PostalCode.Text = "No Data";
                    text6Nationality.Text = "No Data";
                    text6PlaceOfBirth.Text = "No Data";
                    text6ProfessionalSummary.Text = "No Data";
                }
                reader.Close();
            }
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
            LoadEditInternships();
            LoadEditCustomSelection();

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
                    bool isFresher = true;
                    if (result != null && result != DBNull.Value)
                    {
                        isFresher = Convert.ToBoolean(result);
                    }
                    if (isFresher)
                    {
                        pnl6Fresher.Visible = true;
                        pnl6EmploymentHistory.Visible = false;
                    }
                    else
                    {
                        pnl6Fresher.Visible = false;
                        pnl6EmploymentHistory.Visible = true;
                        LoadEmploymentHistory();
                    }
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
                    rpt6EmploymentHistory.DataSource = dt;
                    rpt6EmploymentHistory.DataBind();
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
                        rpt6Education.DataSource = dt;
                        rpt6Education.DataBind();
                    }
                    else
                    {
                        rpt6Education.DataSource = new[] { new { Degree = "No education found", SchoolName = "", City = "", StartDate = "N/A", EndDate = "N/A", Description = "" } };
                        rpt6Education.DataBind();
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
                        rpt6Skills.DataSource = dt;
                        rpt6Skills.DataBind();
                    }
                    else
                    {
                        rpt6Skills.DataSource = new[] { new { SkillName = "No skills found" } };
                        rpt6Skills.DataBind();
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
                        text6InternCompany.Text = reader["CompanyName"].ToString();
                        text6InternJobTitle.Text = reader["JobTitle"].ToString();
                        text6InternStartDate.Text = Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy");
                        text6InternEndDate.Text = Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy");
                        text6InternDescription.Text = reader["Description"].ToString();
                    }
                    else
                    {
                        text6InternCompany.Text = "N/A";
                        text6InternJobTitle.Text = "N/A";
                        text6InternStartDate.Text = "N/A";
                        text6InternEndDate.Text = "N/A";
                        text6InternDescription.Text = "No internship data available";
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
                        rpt6Certifications.DataSource = dt;
                        rpt6Certifications.DataBind();
                    }
                    else
                    {
                        rpt6Certifications.DataSource = new[] { new { CourseName = "No certifications found", Institution = "", StartDate = "N/A", EndDate = "N/A" } };
                        rpt6Certifications.DataBind();
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
                        rpt6Languages.DataSource = dt;
                        rpt6Languages.DataBind();
                    }
                    else
                    {
                        rpt6Languages.DataSource = new[] { new { LanguageName = "No languages found" } };
                        rpt6Languages.DataBind();
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
                        rpt6Hobbies.DataSource = dt;
                        rpt6Hobbies.DataBind();
                    }
                    else
                    {
                        rpt6Hobbies.DataSource = new[] { new { Name = "No hobbies found" } };
                        rpt6Hobbies.DataBind();
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
                        rpt6CustomSelection.DataSource = dt;
                        rpt6CustomSelection.DataBind();
                    }
                    else
                    {
                        rpt6CustomSelection.DataSource = new[] { new { Title = "No additional info", StartDate = "N/A", EndDate = "N/A", Description = "" } };
                        rpt6CustomSelection.DataBind();
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

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (Document document = new Document(PageSize.A4, 15, 15, 15, 15))
                    {
                        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                        document.Open();

                        // Define colors matching type2 template
                        BaseColor darkBgColor = new BaseColor(43, 43, 43); // #2b2b2b
                        BaseColor accentColor = new BaseColor(255, 111, 97); // #ff6f61
                        BaseColor lightTextColor = new BaseColor(224, 224, 224); // #e0e0e0
                        BaseColor darkTextColor = new BaseColor(51, 51, 51); // #333333
                        BaseColor listItemBgColor = new BaseColor(58, 58, 58); // #3a3a3a

                        // Fonts (aligned with type2 styling)
                        Font headerFont = FontFactory.GetFont("Arial", 22, Font.BOLD, BaseColor.WHITE);
                        Font subHeaderFont = FontFactory.GetFont("Arial", 14, Font.NORMAL, new BaseColor(255, 255, 255, 230));
                        Font sectionFont = FontFactory.GetFont("Arial", 18, Font.BOLD, accentColor);
                        Font contentFont = FontFactory.GetFont("Arial", 11, Font.NORMAL, lightTextColor);
                        Font listItemFont = FontFactory.GetFont("Arial", 11, Font.NORMAL, lightTextColor);

                        // Create main container table
                        PdfPTable containerTable = new PdfPTable(1);
                        containerTable.WidthPercentage = 100;
                        containerTable.DefaultCell.Border = Rectangle.NO_BORDER;
                        containerTable.DefaultCell.BackgroundColor = darkBgColor;
                        containerTable.SpacingBefore = 0;
                        containerTable.SpacingAfter = 0;
                        containerTable.SplitRows = true;
                        containerTable.SplitLate = false;

                        // Header section
                        PdfPCell headerCell = new PdfPCell();
                        headerCell.BackgroundColor = accentColor;
                        headerCell.Padding = 10;
                        headerCell.Border = Rectangle.NO_BORDER;
                        headerCell.HorizontalAlignment = Element.ALIGN_CENTER;

                        string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                        string fullName = "";
                        string jobTitle = "";

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
                                        jobTitle = reader["JobTitle"]?.ToString() ?? text6JobTitle.Text?.Trim() ?? "";
                                    }
                                    else
                                    {
                                        fullName = "User Name";
                                    }
                                }
                            }
                        }

                        headerCell.AddElement(new Paragraph(fullName, headerFont) { SpacingAfter = 5 });
                        headerCell.AddElement(new Paragraph(jobTitle, subHeaderFont) { SpacingAfter = 5 });
                        containerTable.AddCell(headerCell);

                        // Content wrapper
                        PdfPTable contentWrapper = new PdfPTable(1);
                        contentWrapper.WidthPercentage = 100;
                        contentWrapper.DefaultCell.Border = Rectangle.NO_BORDER;
                        contentWrapper.DefaultCell.Padding = 5;
                        contentWrapper.SpacingBefore = 0;
                        contentWrapper.SpacingAfter = 0;

                        // Contact Information
                        PdfPTable contactTable = new PdfPTable(1);
                        contactTable.WidthPercentage = 100;
                        contactTable.DefaultCell.Border = Rectangle.NO_BORDER;
                        contactTable.DefaultCell.Padding = 3;

                        PdfPCell sectionTitle = new PdfPCell(new Phrase("Contact", sectionFont));
                        sectionTitle.Border = Rectangle.BOTTOM_BORDER;
                        sectionTitle.BorderWidthBottom = 1.5f;
                        sectionTitle.BorderColorBottom = accentColor;
                        sectionTitle.PaddingBottom = 3f;
                        contactTable.AddCell(sectionTitle);

                        using (SqlConnection conn = new SqlConnection(connStr))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT Email, Phone, Address, City, State, PostalCode, Country FROM PersonalDetails WHERE UserID = @UserID", conn))
                            {
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        string email = reader["Email"]?.ToString() ?? text6Email.Text?.Trim() ?? "";
                                        string phone = reader["Phone"]?.ToString() ?? text6Phone.Text?.Trim() ?? "";
                                        string address = reader["Address"]?.ToString() ?? text6Address.Text?.Trim() ?? "";
                                        string city = reader["City"]?.ToString() ?? text6City.Text?.Trim() ?? "";
                                        string state = reader["State"]?.ToString() ?? text6State.Text?.Trim() ?? "";
                                        string postalCode = reader["PostalCode"]?.ToString() ?? text6PostalCode.Text?.Trim() ?? "";
                                        string country = reader["Country"]?.ToString() ?? text6Country.Text?.Trim() ?? "";

                                        contactTable.AddCell(new Phrase($"Email: {email}", contentFont));
                                        contactTable.AddCell(new Phrase($"Phone: {phone}", contentFont));
                                        string fullAddress = $"{address}, {city}, {state}, {postalCode}, {country}".Trim(',', ' ');
                                        contactTable.AddCell(new Phrase(fullAddress, contentFont));
                                    }
                                }
                            }
                        }
                        contentWrapper.AddCell(contactTable);

                        // Personal Information
                        PdfPTable personalTable = new PdfPTable(1);
                        personalTable.WidthPercentage = 100;
                        personalTable.DefaultCell.Border = Rectangle.NO_BORDER;
                        personalTable.DefaultCell.Padding = 3;

                        sectionTitle = new PdfPCell(new Phrase("Personal Info", sectionFont));
                        sectionTitle.Border = Rectangle.BOTTOM_BORDER;
                        sectionTitle.BorderWidthBottom = 1.5f;
                        sectionTitle.BorderColorBottom = accentColor;
                        sectionTitle.PaddingBottom = 3f;
                        personalTable.AddCell(sectionTitle);

                        using (SqlConnection conn = new SqlConnection(connStr))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT DateOfBirth, PlaceOfBirth, Nationality FROM PersonalDetails WHERE UserID = @UserID", conn))
                            {
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        string dob = reader["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd") : text6DateOfBirth.Text?.Trim() ?? "";
                                        string placeOfBirth = reader["PlaceOfBirth"]?.ToString() ?? text6PlaceOfBirth.Text?.Trim() ?? "";
                                        string nationality = reader["Nationality"]?.ToString() ?? text6Nationality.Text?.Trim() ?? "";

                                        personalTable.AddCell(new Phrase($"Date of Birth: {dob}", contentFont));
                                        personalTable.AddCell(new Phrase($"Place of Birth: {placeOfBirth}", contentFont));
                                        personalTable.AddCell(new Phrase($"Nationality: {nationality}", contentFont));
                                    }
                                }
                            }
                        }
                        contentWrapper.AddCell(personalTable);

                        // Professional Summary
                        PdfPTable summaryTable = new PdfPTable(1);
                        summaryTable.WidthPercentage = 100;
                        summaryTable.DefaultCell.Border = Rectangle.NO_BORDER;
                        summaryTable.DefaultCell.Padding = 3;

                        sectionTitle = new PdfPCell(new Phrase("Professional Summary", sectionFont));
                        sectionTitle.Border = Rectangle.BOTTOM_BORDER;
                        sectionTitle.BorderWidthBottom = 1.5f;
                        sectionTitle.BorderColorBottom = accentColor;
                        sectionTitle.PaddingBottom = 3f;
                        summaryTable.AddCell(sectionTitle);

                        string summary = "";
                        using (SqlConnection conn = new SqlConnection(connStr))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT PersonalSummary FROM PersonalDetails WHERE UserID = @UserID", conn))
                            {
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                summary = cmd.ExecuteScalar()?.ToString() ?? text6ProfessionalSummary.Text?.Trim() ?? "";
                            }
                        }

                        PdfPCell summaryContent = new PdfPCell(new Phrase(summary, contentFont));
                        summaryContent.BackgroundColor = new BaseColor(51, 51, 51);
                        summaryContent.BorderWidthLeft = 2f;
                        summaryContent.BorderColorLeft = accentColor;
                        summaryContent.Padding = 8;
                        summaryTable.AddCell(summaryContent);
                        contentWrapper.AddCell(summaryTable);

                        // Skills
                        PdfPTable skillsTable = new PdfPTable(1);
                        skillsTable.WidthPercentage = 100;
                        skillsTable.DefaultCell.Border = Rectangle.NO_BORDER;
                        skillsTable.DefaultCell.Padding = 3;

                        sectionTitle = new PdfPCell(new Phrase("Skills", sectionFont));
                        sectionTitle.Border = Rectangle.BOTTOM_BORDER;
                        sectionTitle.BorderWidthBottom = 1.5f;
                        sectionTitle.BorderColorBottom = accentColor;
                        sectionTitle.PaddingBottom = 3f;
                        skillsTable.AddCell(sectionTitle);

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
                                                PdfPCell skillCell = new PdfPCell(new Phrase(skill, listItemFont));
                                                skillCell.BackgroundColor = listItemBgColor;
                                                skillCell.Border = Rectangle.NO_BORDER;
                                                skillCell.Padding = 5;
                                                skillCell.MinimumHeight = 20;
                                                skillCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                                skillsTable.AddCell(skillCell);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        PdfPCell skillCell = new PdfPCell(new Phrase("No skills listed", listItemFont));
                                        skillCell.BackgroundColor = listItemBgColor;
                                        skillCell.Border = Rectangle.NO_BORDER;
                                        skillCell.Padding = 5;
                                        skillCell.MinimumHeight = 20;
                                        skillCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                        skillsTable.AddCell(skillCell);
                                    }
                                }
                            }
                        }
                        contentWrapper.AddCell(skillsTable);

                        // Languages
                        PdfPTable languagesTable = new PdfPTable(1);
                        languagesTable.WidthPercentage = 100;
                        languagesTable.DefaultCell.Border = Rectangle.NO_BORDER;
                        languagesTable.DefaultCell.Padding = 3;

                        sectionTitle = new PdfPCell(new Phrase("Languages", sectionFont));
                        sectionTitle.Border = Rectangle.BOTTOM_BORDER;
                        sectionTitle.BorderWidthBottom = 1.5f;
                        sectionTitle.BorderColorBottom = accentColor;
                        sectionTitle.PaddingBottom = 3f;
                        languagesTable.AddCell(sectionTitle);

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
                                                PdfPCell languageCell = new PdfPCell(new Phrase(language, listItemFont));
                                                languageCell.BackgroundColor = listItemBgColor;
                                                languageCell.Border = Rectangle.NO_BORDER;
                                                languageCell.Padding = 5;
                                                languageCell.MinimumHeight = 20;
                                                languageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                                languagesTable.AddCell(languageCell);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        PdfPCell languageCell = new PdfPCell(new Phrase("No languages listed", listItemFont));
                                        languageCell.BackgroundColor = listItemBgColor;
                                        languageCell.Border = Rectangle.NO_BORDER;
                                        languageCell.Padding = 5;
                                        languageCell.MinimumHeight = 20;
                                        languageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                        languagesTable.AddCell(languageCell);
                                    }
                                }
                            }
                        }
                        contentWrapper.AddCell(languagesTable);

                        // Hobbies
                        PdfPTable hobbiesTable = new PdfPTable(1);
                        hobbiesTable.WidthPercentage = 100;
                        hobbiesTable.DefaultCell.Border = Rectangle.NO_BORDER;
                        hobbiesTable.DefaultCell.Padding = 3;

                        sectionTitle = new PdfPCell(new Phrase("Hobbies", sectionFont));
                        sectionTitle.Border = Rectangle.BOTTOM_BORDER;
                        sectionTitle.BorderWidthBottom = 1.5f;
                        sectionTitle.BorderColorBottom = accentColor;
                        sectionTitle.PaddingBottom = 3f;
                        hobbiesTable.AddCell(sectionTitle);

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
                                                PdfPCell hobbyCell = new PdfPCell(new Phrase(hobby, listItemFont));
                                                hobbyCell.BackgroundColor = listItemBgColor;
                                                hobbyCell.Border = Rectangle.NO_BORDER;
                                                hobbyCell.Padding = 5;
                                                hobbyCell.MinimumHeight = 20;
                                                hobbyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                                hobbiesTable.AddCell(hobbyCell);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        PdfPCell hobbyCell = new PdfPCell(new Phrase("No hobbies listed", listItemFont));
                                        hobbyCell.BackgroundColor = listItemBgColor;
                                        hobbyCell.Border = Rectangle.NO_BORDER;
                                        hobbyCell.Padding = 5;
                                        hobbyCell.MinimumHeight = 20;
                                        hobbyCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                        hobbiesTable.AddCell(hobbyCell);
                                    }
                                }
                            }
                        }
                        contentWrapper.AddCell(hobbiesTable);

                        // Employment History
                        PdfPTable employmentTable = new PdfPTable(1);
                        employmentTable.WidthPercentage = 100;
                        employmentTable.DefaultCell.Border = Rectangle.NO_BORDER;
                        employmentTable.DefaultCell.Padding = 3;

                        sectionTitle = new PdfPCell(new Phrase("Employment History", sectionFont));
                        sectionTitle.Border = Rectangle.BOTTOM_BORDER;
                        sectionTitle.BorderWidthBottom = 1.5f;
                        sectionTitle.BorderColorBottom = accentColor;
                        sectionTitle.PaddingBottom = 3f;
                        employmentTable.AddCell(sectionTitle);

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
                            PdfPCell fresherCell = new PdfPCell(new Phrase("Fresher (No work experience)", contentFont));
                            fresherCell.BackgroundColor = new BaseColor(51, 51, 51);
                            fresherCell.BorderWidthLeft = 2f;
                            fresherCell.BorderColorLeft = accentColor;
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
                                            PdfPCell empCell = new PdfPCell(new Phrase("No employment history found.", contentFont));
                                            empCell.BackgroundColor = new BaseColor(51, 51, 51);
                                            empCell.BorderWidthLeft = 2f;
                                            empCell.BorderColorLeft = accentColor;
                                            empCell.Padding = 8;
                                            employmentTable.AddCell(empCell);
                                        }
                                        else
                                        {
                                            while (reader.Read())
                                            {
                                                string empJobTitle = reader["JobTitle"]?.ToString() ?? "N/A";
                                                string employer = reader["Employer"]?.ToString() ?? "N/A";
                                                string city = reader["City"]?.ToString() ?? "N/A";
                                                string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                                string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                                string description = reader["Description"]?.ToString() ?? "";

                                                string entry = $"I worked as {empJobTitle} at {employer}, {city} between {startDate} and {endDate}. {description}".Trim();

                                                PdfPCell empCell = new PdfPCell(new Phrase(entry, contentFont));
                                                empCell.BackgroundColor = new BaseColor(51, 51, 51);
                                                empCell.BorderWidthLeft = 2f;
                                                empCell.BorderColorLeft = accentColor;
                                                empCell.Padding = 8;
                                                employmentTable.AddCell(empCell);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        contentWrapper.AddCell(employmentTable);

                        // Education
                        PdfPTable educationTable = new PdfPTable(1);
                        educationTable.WidthPercentage = 100;
                        educationTable.DefaultCell.Border = Rectangle.NO_BORDER;
                        educationTable.DefaultCell.Padding = 3;

                        sectionTitle = new PdfPCell(new Phrase("Education", sectionFont));
                        sectionTitle.Border = Rectangle.BOTTOM_BORDER;
                        sectionTitle.BorderWidthBottom = 1.5f;
                        sectionTitle.BorderColorBottom = accentColor;
                        sectionTitle.PaddingBottom = 3f;
                        educationTable.AddCell(sectionTitle);

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
                                        PdfPCell eduCell = new PdfPCell(new Phrase("No education found.", contentFont));
                                        eduCell.BackgroundColor = new BaseColor(51, 51, 51);
                                        eduCell.BorderWidthLeft = 2f;
                                        eduCell.BorderColorLeft = accentColor;
                                        eduCell.Padding = 8;
                                        educationTable.AddCell(eduCell);
                                    }
                                    else
                                    {
                                        while (reader.Read())
                                        {
                                            string degree = reader["Degree"]?.ToString() ?? "N/A";
                                            string school = reader["SchoolName"]?.ToString() ?? "N/A";
                                            string city = reader["City"]?.ToString() ?? "N/A";
                                            string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                            string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                            string description = reader["Description"]?.ToString() ?? "";

                                            string entry = $"I completed my {degree} from {school}, {city} between {startDate} and {endDate}. {description}".Trim();

                                            PdfPCell eduCell = new PdfPCell(new Phrase(entry, contentFont));
                                            eduCell.BackgroundColor = new BaseColor(51, 51, 51);
                                            eduCell.BorderWidthLeft = 2f;
                                            eduCell.BorderColorLeft = accentColor;
                                            eduCell.Padding = 8;
                                            educationTable.AddCell(eduCell);
                                        }
                                    }
                                }
                            }
                        }
                        contentWrapper.AddCell(educationTable);

                        // Certifications
                        PdfPTable certTable = new PdfPTable(1);
                        certTable.WidthPercentage = 100;
                        certTable.DefaultCell.Border = Rectangle.NO_BORDER;
                        certTable.DefaultCell.Padding = 3;

                        sectionTitle = new PdfPCell(new Phrase("Certifications", sectionFont));
                        sectionTitle.Border = Rectangle.BOTTOM_BORDER;
                        sectionTitle.BorderWidthBottom = 1.5f;
                        sectionTitle.BorderColorBottom = accentColor;
                        sectionTitle.PaddingBottom = 3f;
                        certTable.AddCell(sectionTitle);

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
                                        PdfPCell certCell = new PdfPCell(new Phrase("No certifications found.", contentFont));
                                        certCell.BackgroundColor = new BaseColor(51, 51, 51);
                                        certCell.BorderWidthLeft = 2f;
                                        certCell.BorderColorLeft = accentColor;
                                        certCell.Padding = 8;
                                        certTable.AddCell(certCell);
                                    }
                                    else
                                    {
                                        while (reader.Read())
                                        {
                                            string course = reader["CourseName"]?.ToString() ?? "N/A";
                                            string institution = reader["Institution"]?.ToString() ?? "N/A";
                                            string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                            string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";

                                            string entry = $"I completed {course} from {institution} between {startDate} and {endDate}.".Trim();

                                            PdfPCell certCell = new PdfPCell(new Phrase(entry, contentFont));
                                            certCell.BackgroundColor = new BaseColor(51, 51, 51);
                                            certCell.BorderWidthLeft = 2f;
                                            certCell.BorderColorLeft = accentColor;
                                            certCell.Padding = 8;
                                            certTable.AddCell(certCell);
                                        }
                                    }
                                }
                            }
                        }
                        contentWrapper.AddCell(certTable);

                        // Internships
                        PdfPTable internTable = new PdfPTable(1);
                        internTable.WidthPercentage = 100;
                        internTable.DefaultCell.Border = Rectangle.NO_BORDER;
                        internTable.DefaultCell.Padding = 3;

                        sectionTitle = new PdfPCell(new Phrase("Internships", sectionFont));
                        sectionTitle.Border = Rectangle.BOTTOM_BORDER;
                        sectionTitle.BorderWidthBottom = 1.5f;
                        sectionTitle.BorderColorBottom = accentColor;
                        sectionTitle.PaddingBottom = 3f;
                        internTable.AddCell(sectionTitle);

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
                                        string company = reader["CompanyName"]?.ToString() ?? "N/A";
                                        string internJobTitle = reader["JobTitle"]?.ToString() ?? "N/A";
                                        string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                        string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                        string description = reader["Description"]?.ToString() ?? "";

                                        string entry = $"I worked as {internJobTitle} at {company} between {startDate} and {endDate}. {description}".Trim();

                                        PdfPCell internCell = new PdfPCell(new Phrase(entry, contentFont));
                                        internCell.BackgroundColor = new BaseColor(51, 51, 51);
                                        internCell.BorderWidthLeft = 2f;
                                        internCell.BorderColorLeft = accentColor;
                                        internCell.Padding = 8;
                                        internTable.AddCell(internCell);
                                    }
                                    else
                                    {
                                        PdfPCell noInternCell = new PdfPCell(new Phrase("No internship data available.", contentFont));
                                        noInternCell.BackgroundColor = new BaseColor(51, 51, 51);
                                        noInternCell.BorderWidthLeft = 2f;
                                        noInternCell.BorderColorLeft = accentColor;
                                        noInternCell.Padding = 8;
                                        internTable.AddCell(noInternCell);
                                    }
                                }
                            }
                        }
                        contentWrapper.AddCell(internTable);

                        // Additional Information
                        PdfPTable customTable = new PdfPTable(1);
                        customTable.WidthPercentage = 100;
                        customTable.DefaultCell.Border = Rectangle.NO_BORDER;
                        customTable.DefaultCell.Padding = 3;

                        sectionTitle = new PdfPCell(new Phrase("Additional Information", sectionFont));
                        sectionTitle.Border = Rectangle.BOTTOM_BORDER;
                        sectionTitle.BorderWidthBottom = 1.5f;
                        sectionTitle.BorderColorBottom = accentColor;
                        sectionTitle.PaddingBottom = 3f;
                        customTable.AddCell(sectionTitle);

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
                                        PdfPCell customCell = new PdfPCell(new Phrase("No additional information.", contentFont));
                                        customCell.BackgroundColor = new BaseColor(51, 51, 51);
                                        customCell.BorderWidthLeft = 2f;
                                        customCell.BorderColorLeft = accentColor;
                                        customCell.Padding = 8;
                                        customTable.AddCell(customCell);
                                    }
                                    else
                                    {
                                        while (reader.Read())
                                        {
                                            string title = reader["Title"]?.ToString() ?? "N/A";
                                            string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                            string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                            string description = reader["Description"]?.ToString() ?? "";

                                            string entry = $"I worked on {title} between {startDate} and {endDate}. {description}".Trim();

                                            PdfPCell customCell = new PdfPCell(new Phrase(entry, contentFont));
                                            customCell.BackgroundColor = new BaseColor(51, 51, 51);
                                            customCell.BorderWidthLeft = 2f;
                                            customCell.BorderColorLeft = accentColor;
                                            customCell.Padding = 8;
                                            customTable.AddCell(customCell);
                                        }
                                    }
                                }
                            }
                        }
                        contentWrapper.AddCell(customTable);

                        // Add content to container
                        containerTable.AddCell(contentWrapper);

                        // Add container to document
                        document.Add(containerTable);
                        document.Close();

                        // Generate unique filename with UserID
                        string userId = Session["UserID"]?.ToString() ?? "Unknown";
                        string firstName = text6FirstName.Text.Replace(" ", "_");
                        string lastName = text6LastName.Text.Replace(" ", "_");
                        string fileName = $"Resume_{userId}_{firstName}_{lastName}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                        string filePath = Path.Combine(resumeFolder, fileName);

                        // Save PDF to ~/Resumes/
                        byte[] bytes = memoryStream.ToArray();
                        File.WriteAllBytes(filePath, bytes);

                        // Insert record into ResumeFiles
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
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.BinaryWrite(bytes);
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
    }
}