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
    public partial class Resume11 : Page
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
                    text11JobTitle.Text = reader["JobTitle"].ToString();
                    text11FirstName.Text = reader["FirstName"].ToString();
                    text11LastName.Text = reader["LastName"].ToString();
                    text11Email.Text = reader["Email"].ToString();
                    text11Phone.Text = reader["Phone"].ToString();
                    text11DateOfBirth.Text = Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                    text11Country.Text = reader["Country"].ToString();
                    text11State.Text = reader["State"].ToString();
                    text11City.Text = reader["City"].ToString();
                    text11Address.Text = reader["Address"].ToString();
                    text11PostalCode.Text = reader["PostalCode"].ToString();
                    text11Nationality.Text = reader["Nationality"].ToString();
                    text11ProfessionalSummary.Text = reader["PersonalSummary"].ToString();
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
            text11JobTitle.Text = "No Data";
            text11FirstName.Text = "No Data";
            text11LastName.Text = "No Data";
            text11Email.Text = "No Data";
            text11Phone.Text = "No Data";
            text11DateOfBirth.Text = "No Data";
            text11Country.Text = "No Data";
            text11State.Text = "No Data";
            text11City.Text = "No Data";
            text11Address.Text = "No Data";
            text11PostalCode.Text = "No Data";
            text11Nationality.Text = "No Data";
            text11ProfessionalSummary.Text = "No Data";
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
                    pnl11Fresher.Visible = isFresher;
                    pnl11EmploymentHistory.Visible = !isFresher;
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
                    rpt11EmploymentHistory.DataSource = dt;
                    rpt11EmploymentHistory.DataBind();
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
                        rpt11Education.DataSource = dt;
                        rpt11Education.DataBind();
                    }
                    else
                    {
                        rpt11Education.DataSource = new[] { new { Degree = "No education found", SchoolName = "", City = "", StartDate = DBNull.Value, EndDate = DBNull.Value, Description = "" } };
                        rpt11Education.DataBind();
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
                        rpt11Skills.DataSource = dt;
                        rpt11Skills.DataBind();
                    }
                    else
                    {
                        rpt11Skills.DataSource = new[] { new { SkillName = "No skills found" } };
                        rpt11Skills.DataBind();
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
                        text11InternCompany.Text = reader["CompanyName"].ToString();
                        text11InternJobTitle.Text = reader["JobTitle"].ToString();
                        text11InternStartDate.Text = Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy");
                        string endDate = reader["EndDate"] == DBNull.Value ? "Present" : Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy");
                        text11InternEndDate.Text = endDate == "Present" && text11InternStartDate.Text != "Present" ? "Present" : endDate;
                        text11InternDescription.Text = FormatDescription(reader["Description"]);
                    }
                    else
                    {
                        text11InternCompany.Text = "N/A";
                        text11InternJobTitle.Text = "N/A";
                        text11InternStartDate.Text = "N/A";
                        text11InternEndDate.Text = "N/A";
                        text11InternDescription.Text = "No internship data available";
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
                        rpt11Certifications.DataSource = dt;
                        rpt11Certifications.DataBind();
                    }
                    else
                    {
                        rpt11Certifications.DataSource = new[] { new { CourseName = "No certifications found", Institution = "", StartDate = DBNull.Value, EndDate = DBNull.Value } };
                        rpt11Certifications.DataBind();
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
                        rpt11Languages.DataSource = dt;
                        rpt11Languages.DataBind();
                    }
                    else
                    {
                        rpt11Languages.DataSource = new[] { new { LanguageName = "No languages found" } };
                        rpt11Languages.DataBind();
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
                        rpt11Hobbies.DataSource = dt;
                        rpt11Hobbies.DataBind();
                    }
                    else
                    {
                        rpt11Hobbies.DataSource = new[] { new { Name = "No hobbies found" } };
                        rpt11Hobbies.DataBind();
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
                        rpt11CustomSelection.DataSource = dt;
                        rpt11CustomSelection.DataBind();
                    }
                    else
                    {
                        rpt11CustomSelection.DataSource = new[] { new { Title = "No additional info", StartDate = DBNull.Value, EndDate = DBNull.Value, Description = "" } };
                        rpt11CustomSelection.DataBind();
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
                                                Nationality = @Nationality, PersonalSummary = @PersonalSummary
                                            WHERE UserID = @UserID AND ID = (SELECT TOP 1 ID FROM PersonalDetails WHERE UserID = @UserID ORDER BY ID DESC)
                                            ELSE
                                            INSERT INTO PersonalDetails (UserID, JobTitle, FirstName, LastName, Email, Phone, DateOfBirth, 
                                                Country, State, City, Address, PostalCode, Nationality, PersonalSummary)
                                            VALUES (@UserID, @JobTitle, @FirstName, @LastName, @Email, @Phone, @DateOfBirth, 
                                                @Country, @State, @City, @Address, @PostalCode, @Nationality, @PersonalSummary)";
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
                        string id = (item.FindControl("btnDeleteCertification") as Button).CommandArgument;

                        if (!string.IsNullOrWhiteSpace(txtCourseName.Text))
                        {
                            string certQuery = @"UPDATE Courses SET CourseName = @CourseName, Institution = @Institution, 
                                                StartDate = @StartDate 
                                                WHERE ID = @ID AND UserID = @UserID";
                            using (SqlCommand cmd = new SqlCommand(certQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text);
                                cmd.Parameters.AddWithValue("@Institution", txtInstitution.Text);
                                cmd.Parameters.AddWithValue("@StartDate", string.IsNullOrEmpty(txtStartDate.Text) ? DBNull.Value : (object)DateTime.Parse(txtStartDate.Text));
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
                {
                    string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        string query = @"INSERT INTO Courses (UserID, CourseName, Institution, StartDate)
                                    VALUES (@UserID, @CourseName, @Institution, @StartDate)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.Parameters.AddWithValue("@CourseName", txtNewCourseName.Text);
                            cmd.Parameters.AddWithValue("@Institution", txtNewInstitution.Text);
                            cmd.Parameters.AddWithValue("@StartDate", string.IsNullOrEmpty(txtNewCertStartDate.Text) ? DBNull.Value : (object)DateTime.Parse(txtNewCertStartDate.Text));
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    ClearNewCertificationFields();
                    LoadEditCertifications();
                }
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

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Resume.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                    document.Open();
                    AddResumeContentToPdf(document);
                    document.Close();
                    Response.BinaryWrite(memoryStream.ToArray());
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error generating PDF: {ex.Message}');", true);
            }
        }

        private void AddResumeContentToPdf(Document document)
        {
            Font headerFont = FontFactory.GetFont("Arial", 16, Font.BOLD, BaseColor.WHITE);
            Font titleFont = FontFactory.GetFont("Arial", 12, Font.BOLD, new BaseColor(26, 60, 94));
            Font normalFont = FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK);
            Font subHeaderFont = FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK);

            PdfPCell CreateCell(string text, Font font, BaseColor bgColor, int colspan = 1, bool center = false)
            {
                PdfPCell cell = new PdfPCell(new Phrase(text, font));
                cell.BackgroundColor = bgColor;
                cell.Colspan = colspan;
                cell.Padding = 5;
                cell.Border = PdfPCell.NO_BORDER;
                if (center) cell.HorizontalAlignment = Element.ALIGN_CENTER;
                return cell;
            }

            PdfPTable headerTable = new PdfPTable(1);
            headerTable.WidthPercentage = 100;
            headerTable.AddCell(CreateCell($"{text11FirstName.Text} {text11LastName.Text}", headerFont, new BaseColor(26, 60, 94), 1, true));
            headerTable.AddCell(CreateCell(text11JobTitle.Text, normalFont, new BaseColor(26, 60, 94), 1, true));
            headerTable.AddCell(CreateCell($"Email: {text11Email.Text} | Phone: {text11Phone.Text}", normalFont, new BaseColor(26, 60, 94), 1, true));
            headerTable.AddCell(CreateCell($"{text11Address.Text}, {text11City.Text}, {text11State.Text}, {text11PostalCode.Text}, {text11Country.Text}", normalFont, new BaseColor(26, 60, 94), 1, true));
            headerTable.AddCell(CreateCell($"Date of Birth: {text11DateOfBirth.Text} | Nationality: {text11Nationality.Text}", normalFont, new BaseColor(26, 60, 94), 1, true));
            document.Add(headerTable);

            document.Add(new Paragraph(" ", normalFont));

            PdfPTable summaryTable = new PdfPTable(1);
            summaryTable.WidthPercentage = 100;
            summaryTable.AddCell(CreateCell("Professional Summary", titleFont, BaseColor.WHITE));
            summaryTable.AddCell(CreateCell(text11ProfessionalSummary.Text, normalFont, BaseColor.WHITE));
            document.Add(summaryTable);

            document.Add(new Paragraph(" ", normalFont));

            PdfPTable employmentTable = new PdfPTable(1);
            employmentTable.WidthPercentage = 100;
            employmentTable.AddCell(CreateCell("Employment History", titleFont, BaseColor.WHITE));
            if (pnl11Fresher.Visible)
            {
                employmentTable.AddCell(CreateCell("I am a fresher with no work experience.", normalFont, BaseColor.WHITE));
            }
            else
            {
                foreach (RepeaterItem item in rpt11EmploymentHistory.Items)
                {
                    Label lblJobTitle = item.FindControl("text11JobTitle") as Label;
                    Label lblEmployer = item.FindControl("text11Employer") as Label;
                    Label lblCity = item.FindControl("text11City") as Label;
                    Label lblStartDate = item.FindControl("lblStartDate") as Label;
                    Literal litEndDate = item.FindControl("litEndDate") as Literal;
                    Label lblDescription = item.FindControl("text11Description") as Label;

                    string endDateText = litEndDate != null ? litEndDate.Text : "Present";
                    employmentTable.AddCell(CreateCell($"{lblJobTitle.Text} at {lblEmployer.Text}, {lblCity.Text}", subHeaderFont, BaseColor.WHITE));
                    employmentTable.AddCell(CreateCell($"{lblStartDate.Text} - {endDateText}", normalFont, BaseColor.WHITE));
                    employmentTable.AddCell(CreateCell(lblDescription.Text, normalFont, BaseColor.WHITE));
                }
            }
            document.Add(employmentTable);

            document.Add(new Paragraph(" ", normalFont));

            PdfPTable educationTable = new PdfPTable(1);
            educationTable.WidthPercentage = 100;
            educationTable.AddCell(CreateCell("Education", titleFont, BaseColor.WHITE));
            foreach (RepeaterItem item in rpt11Education.Items)
            {
                Label lblDegree = item.FindControl("text11Degree") as Label;
                Label lblSchoolName = item.FindControl("text11SchoolName") as Label;
                Label lblCity = item.FindControl("text11City") as Label;
                Label lblStartDate = item.FindControl("lblStartDate") as Label;
                Literal litEndDate = item.FindControl("litEndDate") as Literal;
                Label lblDescription = item.FindControl("text11Description") as Label;

                string endDateText = litEndDate != null ? litEndDate.Text : "Present";
                educationTable.AddCell(CreateCell($"{lblDegree.Text}, {lblSchoolName.Text}, {lblCity.Text}", subHeaderFont, BaseColor.WHITE));
                educationTable.AddCell(CreateCell($"{lblStartDate.Text} - {endDateText}", normalFont, BaseColor.WHITE));
                educationTable.AddCell(CreateCell(lblDescription.Text, normalFont, BaseColor.WHITE));
            }
            document.Add(educationTable);

            document.Add(new Paragraph(" ", normalFont));

            PdfPTable internshipTable = new PdfPTable(1);
            internshipTable.WidthPercentage = 100;
            internshipTable.AddCell(CreateCell("Internships", titleFont, BaseColor.WHITE));
            internshipTable.AddCell(CreateCell($"{text11InternJobTitle.Text} at {text11InternCompany.Text}", subHeaderFont, BaseColor.WHITE));
            internshipTable.AddCell(CreateCell($"{text11InternStartDate.Text} - {text11InternEndDate.Text}", normalFont, BaseColor.WHITE));
            internshipTable.AddCell(CreateCell(text11InternDescription.Text, normalFont, BaseColor.WHITE));
            document.Add(internshipTable);

            document.Add(new Paragraph(" ", normalFont));

            PdfPTable skillsTable = new PdfPTable(3);
            skillsTable.WidthPercentage = 100;
            skillsTable.SetWidths(new float[] { 1, 1, 1 });

            skillsTable.AddCell(CreateCell("Skills", titleFont, BaseColor.WHITE));
            skillsTable.AddCell(CreateCell("Languages", titleFont, BaseColor.WHITE));
            skillsTable.AddCell(CreateCell("Certifications", titleFont, BaseColor.WHITE));

            string skillsText = string.Join("\n", rpt11Skills.Items.Cast<RepeaterItem>().Select(item => (item.FindControl("lit11Skill") as Literal).Text));
            string languagesText = string.Join("\n", rpt11Languages.Items.Cast<RepeaterItem>().Select(item => (item.FindControl("lit11Language") as Literal).Text));
            string certificationsText = string.Join("\n", rpt11Certifications.Items.Cast<RepeaterItem>().Select(item =>
                $"{(item.FindControl("text11CourseName") as Label).Text}, {(item.FindControl("text11Institution") as Label).Text}, {(item.FindControl("lblStartDate") as Label).Text}"));

            skillsTable.AddCell(CreateCell(skillsText, normalFont, BaseColor.WHITE));
            skillsTable.AddCell(CreateCell(languagesText, normalFont, BaseColor.WHITE));
            skillsTable.AddCell(CreateCell(certificationsText, normalFont, BaseColor.WHITE));
            document.Add(skillsTable);

            document.Add(new Paragraph(" ", normalFont));

            PdfPTable hobbiesTable = new PdfPTable(1);
            hobbiesTable.WidthPercentage = 100;
            hobbiesTable.AddCell(CreateCell("Hobbies", titleFont, BaseColor.WHITE));
            string hobbiesText = string.Join("\n", rpt11Hobbies.Items.Cast<RepeaterItem>().Select(item => (item.FindControl("lit11Hobby") as Literal).Text));
            hobbiesTable.AddCell(CreateCell(hobbiesText, normalFont, BaseColor.WHITE));
            document.Add(hobbiesTable);

            document.Add(new Paragraph(" ", normalFont));

            PdfPTable customTable = new PdfPTable(1);
            customTable.WidthPercentage = 100;
            customTable.AddCell(CreateCell("Additional Information", titleFont, BaseColor.WHITE));
            foreach (RepeaterItem item in rpt11CustomSelection.Items)
            {
                Label lblTitle = item.FindControl("text11Title") as Label;
                Label lblStartDate = item.FindControl("lblStartDate") as Label;
                Literal litEndDate = item.FindControl("litEndDate") as Literal;
                Label lblDescription = item.FindControl("text11Description") as Label;

                string endDateText = litEndDate != null ? litEndDate.Text : "Present";
                customTable.AddCell(CreateCell(lblTitle.Text, subHeaderFont, BaseColor.WHITE));
                customTable.AddCell(CreateCell($"{lblStartDate.Text} - {endDateText}", normalFont, BaseColor.WHITE));
                customTable.AddCell(CreateCell(lblDescription.Text, normalFont, BaseColor.WHITE));
            }
            document.Add(customTable);
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
        }

        private void ClearNewCustomFields()
        {
            txtNewCustomTitle.Text = "";
            txtNewCustomStartDate.Text = "";
            txtNewCustomEndDate.Text = "";
            txtNewCustomDescription.Text = "";
        }

        public string FormatDate(object date)
        {
            if (date == DBNull.Value || date == null) return "Present";
            DateTime parsedDate;
            if (DateTime.TryParse(date.ToString(), out parsedDate))
            {
                return parsedDate.ToString("MMM yyyy");
            }
            return date.ToString();
        }

        public string FormatDescription(object description)
        {
            if (description == null || string.IsNullOrWhiteSpace(description.ToString())) return "No description provided.";
            return description.ToString().Replace("\n", "<br />");
        }
    }
}