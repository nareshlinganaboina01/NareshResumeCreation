using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NareshResumeCreation
{
    public partial class Resume7 : Page
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
            btnDownload.Visible = mode == "View";
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
                    text7JobTitle.Text = reader["JobTitle"].ToString();
                    text7FirstName.Text = reader["FirstName"].ToString();
                    text7LastName.Text = reader["LastName"].ToString();
                    text7Email.Text = reader["Email"].ToString();
                    text7Phone.Text = reader["Phone"].ToString();
                    text7DateOfBirth.Text = Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                    text7Country.Text = reader["Country"].ToString();
                    text7State.Text = reader["State"].ToString();
                    text7City.Text = reader["City"].ToString();
                    text7Address.Text = reader["Address"].ToString();
                    text7PostalCode.Text = reader["PostalCode"].ToString();
                    text7Nationality.Text = reader["Nationality"].ToString();
                    text7PlaceOfBirth.Text = reader["PlaceOfBirth"].ToString();
                    text7ProfessionalSummary.Text = reader["PersonalSummary"].ToString();
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
            text7JobTitle.Text = "Not Specified";
            text7FirstName.Text = "Not Specified";
            text7LastName.Text = "Not Specified";
            text7Email.Text = "Not Specified";
            text7Phone.Text = "Not Specified";
            text7DateOfBirth.Text = "Not Specified";
            text7Country.Text = "Not Specified";
            text7State.Text = "Not Specified";
            text7City.Text = "Not Specified";
            text7Address.Text = "Not Specified";
            text7PostalCode.Text = "Not Specified";
            text7Nationality.Text = "Not Specified";
            text7PlaceOfBirth.Text = "Not Specified";
            text7ProfessionalSummary.Text = "Not Specified";
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
                    pnl7Fresher.Visible = isFresher;
                    pnl7EmploymentHistory.Visible = !isFresher;
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
                    rpt7EmploymentHistory.DataSource = dt;
                    rpt7EmploymentHistory.DataBind();
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
                        rpt7Education.DataSource = dt;
                        rpt7Education.DataBind();
                    }
                    else
                    {
                        rpt7Education.DataSource = new[] { new { Degree = "No qualifications recorded", SchoolName = "", City = "", StartDate = "N/A", EndDate = "N/A", Description = "" } };
                        rpt7Education.DataBind();
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
                        rpt7Skills.DataSource = dt;
                        rpt7Skills.DataBind();
                    }
                    else
                    {
                        rpt7Skills.DataSource = new[] { new { SkillName = "No skills recorded" } };
                        rpt7Skills.DataBind();
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
                        text7InternCompany.Text = reader["CompanyName"].ToString();
                        text7InternJobTitle.Text = reader["JobTitle"].ToString();
                        text7InternStartDate.Text = Convert.ToDateTime(reader["StartDate"]).ToString("MMMM yyyy");
                        text7InternEndDate.Text = string.IsNullOrEmpty(reader["EndDate"].ToString()) ? "Present" : Convert.ToDateTime(reader["EndDate"]).ToString("MMMM yyyy");
                        text7InternDescription.Text = reader["Description"].ToString();
                        pnlInternship.Visible = true;
                        pnlNoInternship.Visible = false;
                    }
                    else
                    {
                        pnlInternship.Visible = false;
                        pnlNoInternship.Visible = true;
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
                        txtEditInternEndDate.Text = string.IsNullOrEmpty(reader["EndDate"].ToString()) ? "" : Convert.ToDateTime(reader["EndDate"]).ToString("yyyy-MM-dd");
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
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        rpt7Certifications.DataSource = dt;
                        rpt7Certifications.DataBind();
                    }
                    else
                    {
                        rpt7Certifications.DataSource = new[] { new { CourseName = "No certifications recorded", Institution = "", StartDate = "N/A", EndDate = "N/A" } };
                        rpt7Certifications.DataBind();
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
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
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
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        rpt7Languages.DataSource = dt;
                        rpt7Languages.DataBind();
                    }
                    else
                    {
                        rpt7Languages.DataSource = new[] { new { LanguageName = "No languages recorded" } };
                        rpt7Languages.DataBind();
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
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
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
                string query = "SELECT ID, Name AS HobbyName FROM Hobbies WHERE UserID = @UserID ORDER BY ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        rpt7Hobbies.DataSource = dt;
                        rpt7Hobbies.DataBind();
                    }
                    else
                    {
                        rpt7Hobbies.DataSource = new[] { new { HobbyName = "No interests recorded" } };
                        rpt7Hobbies.DataBind();
                    }
                }
            }
        }

        private void LoadEditHobbies()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT ID, Name AS HobbyName FROM Hobbies WHERE UserID = @UserID ORDER BY ID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
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
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        rpt7CustomSelection.DataSource = dt;
                        rpt7CustomSelection.DataBind();
                    }
                    else
                    {
                        rpt7CustomSelection.DataSource = new[] { new { Title = "No achievements recorded", StartDate = "N/A", EndDate = "N/A", Description = "" } };
                        rpt7CustomSelection.DataBind();
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
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    rptEditCustomSelection.DataSource = dt;
                    rptEditCustomSelection.DataBind();
                }
            }
        }

        protected void chkFresher_CheckedChanged(object sender, EventArgs e)
        {
            pnlEditEmploymentHistory.Visible = !chkFresher.Checked;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ViewState["Mode"] = "Edit";
            ToggleMode();
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
            LoadAllData();
            ToggleMode();
        }

        private void SavePersonalData()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"INSERT INTO PersonalDetails (UserID, JobTitle, FirstName, LastName, Email, Phone, DateOfBirth, Country, State, City, Address, PostalCode, Nationality, PlaceOfBirth, PersonalSummary)
                                VALUES (@UserID, @JobTitle, @FirstName, @LastName, @Email, @Phone, @DateOfBirth, @Country, @State, @City, @Address, @PostalCode, @Nationality, @PlaceOfBirth, @PersonalSummary)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                cmd.Parameters.AddWithValue("@JobTitle", txtEditJobTitle.Text);
                cmd.Parameters.AddWithValue("@FirstName", txtEditFirstName.Text);
                cmd.Parameters.AddWithValue("@LastName", txtEditLastName.Text);
                cmd.Parameters.AddWithValue("@Email", txtEditEmail.Text);
                cmd.Parameters.AddWithValue("@Phone", txtEditPhone.Text);
                cmd.Parameters.AddWithValue("@DateOfBirth", txtEditDateOfBirth.Text);
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

        private void SaveSkills()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                foreach (RepeaterItem item in rptEditSkills.Items)
                {
                    TextBox txtSkillName = (TextBox)item.FindControl("txtSkillName");
                    string id = ((Button)item.FindControl("btnDeleteSkill")).CommandArgument;
                    string query = "UPDATE Skills SET SkillName = @SkillName WHERE ID = @ID AND UserID = @UserID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@SkillName", txtSkillName.Text);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void SaveLanguages()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                foreach (RepeaterItem item in rptEditLanguages.Items)
                {
                    TextBox txtLanguageName = (TextBox)item.FindControl("txtLanguageName");
                    string id = ((Button)item.FindControl("btnDeleteLanguage")).CommandArgument;
                    string query = "UPDATE Languages SET LanguageName = @LanguageName WHERE ID = @ID AND UserID = @UserID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@LanguageName", txtLanguageName.Text);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void SaveHobbies()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                foreach (RepeaterItem item in rptEditHobbies.Items)
                {
                    TextBox txtHobbyName = (TextBox)item.FindControl("txtHobbyName");
                    string id = ((Button)item.FindControl("btnDeleteHobby")).CommandArgument;
                    string query = "UPDATE Hobbies SET Name = @Name WHERE ID = @ID AND UserID = @UserID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", txtHobbyName.Text);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void SaveEmploymentHistory()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                foreach (RepeaterItem item in rptEditEmploymentHistory.Items)
                {
                    TextBox txtJobTitle = (TextBox)item.FindControl("txtJobTitle");
                    TextBox txtEmployer = (TextBox)item.FindControl("txtEmployer");
                    TextBox txtCity = (TextBox)item.FindControl("txtCity");
                    TextBox txtStartDate = (TextBox)item.FindControl("txtStartDate");
                    TextBox txtEndDate = (TextBox)item.FindControl("txtEndDate");
                    TextBox txtDescription = (TextBox)item.FindControl("txtDescription");
                    string id = ((Button)item.FindControl("btnDeleteEmployment")).CommandArgument;
                    string query = @"UPDATE EmploymentHistoryy SET JobTitle = @JobTitle, Employer = @Employer, City = @City, 
                                    StartDate = @StartDate, EndDate = @EndDate, Description = @Description 
                                    WHERE ID = @ID AND UserID = @UserID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@JobTitle", txtJobTitle.Text);
                    cmd.Parameters.AddWithValue("@Employer", txtEmployer.Text);
                    cmd.Parameters.AddWithValue("@City", txtCity.Text);
                    cmd.Parameters.AddWithValue("@StartDate", txtStartDate.Text);
                    cmd.Parameters.AddWithValue("@EndDate", txtEndDate.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void SaveEducation()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                foreach (RepeaterItem item in rptEditEducation.Items)
                {
                    TextBox txtDegree = (TextBox)item.FindControl("txtDegree");
                    TextBox txtSchoolName = (TextBox)item.FindControl("txtSchoolName");
                    TextBox txtCity = (TextBox)item.FindControl("txtCity");
                    TextBox txtStartDate = (TextBox)item.FindControl("txtStartDate");
                    TextBox txtEndDate = (TextBox)item.FindControl("txtEndDate");
                    TextBox txtDescription = (TextBox)item.FindControl("txtDescription");
                    string id = ((Button)item.FindControl("btnDeleteEducation")).CommandArgument;
                    string query = @"UPDATE Education SET Degree = @Degree, SchoolName = @SchoolName, City = @City, 
                                    StartDate = @StartDate, EndDate = @EndDate, Description = @Description 
                                    WHERE ID = @ID AND UserID = @UserID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Degree", txtDegree.Text);
                    cmd.Parameters.AddWithValue("@SchoolName", txtSchoolName.Text);
                    cmd.Parameters.AddWithValue("@City", txtCity.Text);
                    cmd.Parameters.AddWithValue("@StartDate", txtStartDate.Text);
                    cmd.Parameters.AddWithValue("@EndDate", txtEndDate.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void SaveCertifications()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                foreach (RepeaterItem item in rptEditCertifications.Items)
                {
                    TextBox txtCourseName = (TextBox)item.FindControl("txtCourseName");
                    TextBox txtInstitution = (TextBox)item.FindControl("txtInstitution");
                    TextBox txtStartDate = (TextBox)item.FindControl("txtStartDate");
                    TextBox txtEndDate = (TextBox)item.FindControl("txtEndDate");
                    string id = ((Button)item.FindControl("btnDeleteCertification")).CommandArgument;
                    string query = @"UPDATE Certifications SET CourseName = @CourseName, Institution = @Institution, 
                                    StartDate = @StartDate, EndDate = @EndDate 
                                    WHERE ID = @ID AND UserID = @UserID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text);
                    cmd.Parameters.AddWithValue("@Institution", txtInstitution.Text);
                    cmd.Parameters.AddWithValue("@StartDate", txtStartDate.Text);
                    cmd.Parameters.AddWithValue("@EndDate", txtEndDate.Text);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void SaveInternships()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = @"IF EXISTS (SELECT 1 FROM Internships WHERE UserID = @UserID)
                                UPDATE Internships SET CompanyName = @CompanyName, JobTitle = @JobTitle, 
                                StartDate = @StartDate, EndDate = @EndDate, Description = @Description 
                                WHERE UserID = @UserID
                                ELSE
                                INSERT INTO Internships (UserID, CompanyName, JobTitle, StartDate, EndDate, Description)
                                VALUES (@UserID, @CompanyName, @JobTitle, @StartDate, @EndDate, @Description)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                cmd.Parameters.AddWithValue("@CompanyName", txtEditInternCompany.Text);
                cmd.Parameters.AddWithValue("@JobTitle", txtEditInternJobTitle.Text);
                cmd.Parameters.AddWithValue("@StartDate", txtEditInternStartDate.Text);
                cmd.Parameters.AddWithValue("@EndDate", txtEditInternEndDate.Text);
                cmd.Parameters.AddWithValue("@Description", txtEditInternDescription.Text);
                cmd.ExecuteNonQuery();
            }
        }

        private void SaveCustomSelection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                foreach (RepeaterItem item in rptEditCustomSelection.Items)
                {
                    TextBox txtTitle = (TextBox)item.FindControl("txtTitle");
                    TextBox txtStartDate = (TextBox)item.FindControl("txtStartDate");
                    TextBox txtEndDate = (TextBox)item.FindControl("txtEndDate");
                    TextBox txtDescription = (TextBox)item.FindControl("txtDescription");
                    string id = ((Button)item.FindControl("btnDeleteCustom")).CommandArgument;
                    string query = @"UPDATE CustomSelection SET Title = @Title, StartDate = @StartDate, 
                                    EndDate = @EndDate, Description = @Description 
                                    WHERE ID = @ID AND UserID = @UserID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                    cmd.Parameters.AddWithValue("@StartDate", txtStartDate.Text);
                    cmd.Parameters.AddWithValue("@EndDate", txtEndDate.Text);
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void SaveFresherStatus()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "UPDATE Userss SET IsFresher = @IsFresher WHERE UserID = @UserID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IsFresher", chkFresher.Checked);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ViewState["Mode"] = "View";
            LoadAllData();
            ToggleMode();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void btnAddSkill_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewSkill.Text))
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "INSERT INTO Skills (UserID, SkillName) VALUES (@UserID, @SkillName)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.Parameters.AddWithValue("@SkillName", txtNewSkill.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
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
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadEditSkills();
            }
        }

        protected void btnAddEmployment_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewJobTitle.Text) && !string.IsNullOrEmpty(txtNewEmployer.Text))
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"INSERT INTO EmploymentHistoryy (UserID, JobTitle, Employer, City, StartDate, EndDate, Description)
                                    VALUES (@UserID, @JobTitle, @Employer, @City, @StartDate, @EndDate, @Description)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.Parameters.AddWithValue("@JobTitle", txtNewJobTitle.Text);
                    cmd.Parameters.AddWithValue("@Employer", txtNewEmployer.Text);
                    cmd.Parameters.AddWithValue("@City", txtNewCity.Text);
                    cmd.Parameters.AddWithValue("@StartDate", txtNewStartDate.Text);
                    cmd.Parameters.AddWithValue("@EndDate", txtNewEndDate.Text);
                    cmd.Parameters.AddWithValue("@Description", txtNewDescription.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
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
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadEditEmploymentHistory();
            }
        }

        protected void btnAddEducation_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewDegree.Text) && !string.IsNullOrEmpty(txtNewSchoolName.Text))
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"INSERT INTO Education (UserID, Degree, SchoolName, City, StartDate, EndDate, Description)
                                    VALUES (@UserID, @Degree, @SchoolName, @City, @StartDate, @EndDate, @Description)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.Parameters.AddWithValue("@Degree", txtNewDegree.Text);
                    cmd.Parameters.AddWithValue("@SchoolName", txtNewSchoolName.Text);
                    cmd.Parameters.AddWithValue("@City", txtNewEducationCity.Text);
                    cmd.Parameters.AddWithValue("@StartDate", txtNewEducationStartDate.Text);
                    cmd.Parameters.AddWithValue("@EndDate", txtNewEducationEndDate.Text);
                    cmd.Parameters.AddWithValue("@Description", txtNewEducationDescription.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
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
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadEditEducation();
            }
        }

        protected void btnAddCertification_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewCourseName.Text) && !string.IsNullOrEmpty(txtNewInstitution.Text))
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"INSERT INTO Certifications (UserID, CourseName, Institution, StartDate, EndDate)
                                    VALUES (@UserID, @CourseName, @Institution, @StartDate, @EndDate)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.Parameters.AddWithValue("@CourseName", txtNewCourseName.Text);
                    cmd.Parameters.AddWithValue("@Institution", txtNewInstitution.Text);
                    cmd.Parameters.AddWithValue("@StartDate", txtNewCertStartDate.Text);
                    cmd.Parameters.AddWithValue("@EndDate", txtNewCertEndDate.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
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
                    string query = "DELETE FROM Certifications WHERE ID = @ID AND UserID = @UserID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadEditCertifications();
            }
        }

        protected void btnAddLanguage_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewLanguage.Text))
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "INSERT INTO Languages (UserID, LanguageName) VALUES (@UserID, @LanguageName)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.Parameters.AddWithValue("@LanguageName", txtNewLanguage.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
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
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadEditLanguages();
            }
        }

        protected void btnAddHobby_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewHobby.Text))
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = "INSERT INTO Hobbies (UserID, Name) VALUES (@UserID, @Name)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.Parameters.AddWithValue("@Name", txtNewHobby.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
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
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadEditHobbies();
            }
        }

        protected void btnAddCustom_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewCustomTitle.Text))
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string query = @"INSERT INTO CustomSelection (UserID, Title, StartDate, EndDate, Description)
                                    VALUES (@UserID, @Title, @StartDate, @EndDate, @Description)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.Parameters.AddWithValue("@Title", txtNewCustomTitle.Text);
                    cmd.Parameters.AddWithValue("@StartDate", txtNewCustomStartDate.Text);
                    cmd.Parameters.AddWithValue("@EndDate", txtNewCustomEndDate.Text);
                    cmd.Parameters.AddWithValue("@Description", txtNewCustomDescription.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
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
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                LoadEditCustomSelection();
            }
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
                    using (Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10))
                    {
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms);
                        pdfDoc.Open();

                        AddResumeContentToPdf(pdfDoc);

                        pdfDoc.Close();

                        // Generate unique filename with UserID
                        string userId = Session["UserID"]?.ToString() ?? "Unknown";
                        string firstName = text7FirstName.Text.Replace(" ", "_");
                        string lastName = text7LastName.Text.Replace(" ", "_");
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
                // Define colors (same as Type 2)
                BaseColor headerStartColor = new BaseColor(30, 60, 114); // #1e3c72
                BaseColor headerEndColor = new BaseColor(42, 82, 152); // #2a5298
                BaseColor bodyBgColor = new BaseColor(248, 249, 250); // #f8f9fa
                BaseColor containerBgColor = new BaseColor(255, 255, 255); // #ffffff
                BaseColor sectionHeadingColor = new BaseColor(30, 60, 114); // #1e3c72
                BaseColor accentColor = new BaseColor(42, 82, 152); // #2a5298
                BaseColor textColor = new BaseColor(33, 37, 41); // #212529
                BaseColor descTextColor = new BaseColor(52, 58, 64); // #343a40
                BaseColor dateTextColor = new BaseColor(108, 117, 125); // #6c757d
                BaseColor itemTitleColor = new BaseColor(42, 82, 152); // #2a5298
                BaseColor whiteTextColor = new BaseColor(255, 255, 255); // #ffffff

                // Define fonts with increased sizes (same as Type 2)
                Font nameFont = FontFactory.GetFont("Arial", 20, Font.BOLD, whiteTextColor);
                Font jobTitleFont = FontFactory.GetFont("Arial", 11, Font.NORMAL, whiteTextColor);
                Font contactFont = FontFactory.GetFont("Arial", 8f, Font.NORMAL, whiteTextColor);
                Font sectionHeadingFont = FontFactory.GetFont("Arial", 12, Font.BOLD, sectionHeadingColor);
                Font itemTitleFont = FontFactory.GetFont("Arial", 10, Font.BOLD, itemTitleColor);
                Font itemDateFont = FontFactory.GetFont("Arial", 8f, Font.ITALIC, dateTextColor);
                Font contentFont = FontFactory.GetFont("Arial", 10f, Font.NORMAL, descTextColor);
                Font bulletFont = FontFactory.GetFont("Arial", 8f, Font.NORMAL, accentColor);

                // Reduce document margins (same as Type 2)
                pdfDoc.SetMargins(7.5f, 7.5f, 7.5f, 7.5f);

                // Database connection string (from Type 1)
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;

                // Resume container (same as Type 2)
                PdfPTable containerTable = new PdfPTable(1) { WidthPercentage = 95 };
                containerTable.DefaultCell.Border = Rectangle.NO_BORDER;
                containerTable.DefaultCell.BackgroundColor = containerBgColor;
                containerTable.DefaultCell.Padding = 7.5f;
                containerTable.SpacingBefore = 7.5f;
                containerTable.SpacingAfter = 7.5f;

                // Header (adapted from Type 1 with Type 2 styling)
                PdfPCell headerCell = new PdfPCell
                {
                    BackgroundColor = headerStartColor,
                    Border = Rectangle.NO_BORDER,
                    Padding = 7.5f
                };
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT FirstName, LastName, JobTitle, Email, Phone, Address, City, State, PostalCode, Country, DateOfBirth, PlaceOfBirth, Nationality FROM PersonalDetails WHERE UserID = @UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string fullName = $"{reader["FirstName"]} {reader["LastName"]}".Trim();
                                Paragraph namePara = new Paragraph(fullName, nameFont) { Alignment = Element.ALIGN_CENTER };
                                namePara.SpacingAfter = 2.25f;
                                namePara.Leading = 18;
                                headerCell.AddElement(namePara);

                                string jobTitle = reader["JobTitle"]?.ToString() ?? "";
                                Paragraph jobTitlePara = new Paragraph(jobTitle, jobTitleFont) { Alignment = Element.ALIGN_CENTER };
                                jobTitlePara.SpacingAfter = 3.75f;
                                jobTitlePara.Leading = 10;
                                headerCell.AddElement(jobTitlePara);

                                string email = reader["Email"]?.ToString() ?? "";
                                string phone = reader["Phone"]?.ToString() ?? "";
                                string address = $"{reader["Address"]?.ToString() ?? ""}, {reader["City"]?.ToString() ?? ""}, {reader["State"]?.ToString() ?? ""}, {reader["PostalCode"]?.ToString() ?? ""}, {reader["Country"]?.ToString() ?? ""}";
                                Paragraph contactPara = new Paragraph
                        {
                            new Chunk($"📧 {email}  ", contactFont),
                            new Chunk($"📞 {phone}  ", contactFont),
                            new Chunk($"📍 {address}", contactFont)
                        };
                                contactPara.Alignment = Element.ALIGN_CENTER;
                                contactPara.Leading = 8;
                                headerCell.AddElement(contactPara);
                            }
                        }
                    }
                }
                containerTable.AddCell(headerCell);

                // Content
                PdfPCell contentCell = new PdfPCell
                {
                    Border = Rectangle.NO_BORDER,
                    Padding = 7.5f
                };

                // Helper method for sections (same as Type 2)
                Action<string, Action<PdfPTable>> addSection = (title, addContent) =>
                {
                    PdfPTable sectionTable = new PdfPTable(1) { WidthPercentage = 100 };
                    sectionTable.DefaultCell.Border = Rectangle.NO_BORDER;
                    sectionTable.DefaultCell.PaddingBottom = 3.75f;

                    PdfPCell headingCell = new PdfPCell(new Phrase(title.ToUpper(), sectionHeadingFont))
                    {
                        Border = Rectangle.BOTTOM_BORDER,
                        BorderColor = accentColor,
                        BorderWidth = 1,
                        PaddingBottom = 2
                    };
                    sectionTable.AddCell(headingCell);

                    addContent(sectionTable);

                    sectionTable.SpacingAfter = 3.75f;
                    contentCell.AddElement(sectionTable);
                };

                // Career Objective (from Type 1)
                addSection("Career Objective", table =>
                {
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT PersonalSummary FROM PersonalDetails WHERE UserID = @UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            string summary = cmd.ExecuteScalar()?.ToString() ?? "";
                            Paragraph paragraph = new Paragraph(summary, contentFont)
                            {
                                Leading = 8f
                            };
                            PdfPCell cell = new PdfPCell(paragraph)
                            {
                                Border = Rectangle.NO_BORDER,
                                Padding = 2.25f
                            };
                            table.AddCell(cell);
                        }
                    }
                });

                // Key Competencies (from Type 1)
                addSection("Key Competencies", table =>
                {
                    PdfPTable listTable = new PdfPTable(2) { WidthPercentage = 100 };
                    listTable.SetWidths(new float[] { 5, 95 });
                    listTable.DefaultCell.Border = Rectangle.NO_BORDER;
                    listTable.DefaultCell.PaddingLeft = 6;
                    listTable.DefaultCell.PaddingTop = 1.5f;
                    bool hasSkills = false;
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
                                            listTable.AddCell(new Phrase("•", bulletFont));
                                            listTable.AddCell(new Phrase(skill, contentFont));
                                            hasSkills = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (!hasSkills)
                    {
                        listTable.AddCell("");
                        listTable.AddCell(new Phrase("No skills listed", contentFont));
                    }
                    table.AddCell(listTable);
                });

                // Professional Experience (from Type 1 with Type 2 styling)
                addSection("Professional Experience", table =>
                {
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
                        table.AddCell(new Phrase("I am seeking opportunities to apply my academic knowledge as a fresher.", contentFont));
                    }
                    else
                    {
                        bool hasEmployment = false;
                        using (SqlConnection conn = new SqlConnection(connStr))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand("SELECT JobTitle, Employer, City, StartDate, EndDate, Description FROM EmploymentHistory WHERE UserID = @UserID ORDER BY StartDate DESC", conn))
                            {
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            string jobTitle = reader["JobTitle"]?.ToString() ?? "";
                                            string employer = reader["Employer"]?.ToString() ?? "";
                                            string city = reader["City"]?.ToString() ?? "";
                                            string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "Present";
                                            string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                            string description = reader["Description"]?.ToString() ?? "";

                                            PdfPTable itemTable = new PdfPTable(1) { WidthPercentage = 100 };
                                            itemTable.DefaultCell.Border = Rectangle.NO_BORDER;
                                            itemTable.DefaultCell.PaddingBottom = 3;
                                            itemTable.AddCell(new Phrase($"{jobTitle}, {employer}, {city}", itemTitleFont));
                                            itemTable.AddCell(new Phrase($"{startDate} - {endDate}", itemDateFont));
                                            Paragraph descPara = new Paragraph(description, contentFont)
                                            {
                                                Leading = 8
                                            };
                                            itemTable.AddCell(descPara);
                                            table.AddCell(itemTable);
                                            hasEmployment = true;
                                        }
                                    }
                                }
                            }
                        }
                        if (!hasEmployment)
                        {
                            table.AddCell(new Phrase("No employment history found.", contentFont));
                        }
                    }
                });

                // Academic Qualifications (from Type 1 with Type 2 styling)
                addSection("Academic Qualifications", table =>
                {
                    bool hasEducation = false;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT Degree, SchoolName, City, StartDate, EndDate, Description FROM Education WHERE UserID = @UserID ORDER BY StartDate DESC", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        string degree = reader["Degree"]?.ToString() ?? "";
                                        string schoolName = reader["SchoolName"]?.ToString() ?? "";
                                        string city = reader["City"]?.ToString() ?? "";
                                        string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "Present";
                                        string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                        string description = reader["Description"]?.ToString() ?? "";

                                        PdfPTable itemTable = new PdfPTable(1) { WidthPercentage = 100 };
                                        itemTable.DefaultCell.Border = Rectangle.NO_BORDER;
                                        itemTable.DefaultCell.PaddingBottom = 3;
                                        Paragraph qualPara = new Paragraph($"I completed my {degree} from {schoolName}, {city} between {startDate} and {endDate}.", contentFont)
                                        {
                                            Leading = 8
                                        };
                                        itemTable.AddCell(qualPara);
                                        Paragraph descPara = new Paragraph(description, contentFont)
                                        {
                                            Leading = 8
                                        };
                                        itemTable.AddCell(descPara);
                                        table.AddCell(itemTable);
                                        hasEducation = true;
                                    }
                                }
                            }
                        }
                    }
                    if (!hasEducation)
                    {
                        table.AddCell(new Phrase("No education found.", contentFont));
                    }
                });

                // Professional Certifications (from Type 1 with Type 2 styling)
                addSection("Professional Certifications", table =>
                {
                    bool hasCertifications = false;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT CourseName, Institution, StartDate, EndDate FROM Courses WHERE UserID = @UserID ORDER BY StartDate DESC", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        string courseName = reader["CourseName"]?.ToString() ?? "";
                                        string institution = reader["Institution"]?.ToString() ?? "";
                                        string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "Present";
                                        string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";

                                        PdfPTable itemTable = new PdfPTable(1) { WidthPercentage = 100 };
                                        itemTable.DefaultCell.Border = Rectangle.NO_BORDER;
                                        itemTable.DefaultCell.PaddingBottom = 3;
                                        Paragraph certPara = new Paragraph($"I earned a {courseName} certification from {institution} between {startDate} and {endDate}.", contentFont)
                                        {
                                            Leading = 8
                                        };
                                        itemTable.AddCell(certPara);
                                        table.AddCell(itemTable);
                                        hasCertifications = true;
                                    }
                                }
                            }
                        }
                    }
                    if (!hasCertifications)
                    {
                        table.AddCell(new Phrase("No certifications found.", contentFont));
                    }
                });

                // Internship Experience (from Type 1 with Type 2 styling)
                addSection("Internship Experience", table =>
                {
                    PdfPTable itemTable = new PdfPTable(1) { WidthPercentage = 100 };
                    itemTable.DefaultCell.Border = Rectangle.NO_BORDER;
                    itemTable.DefaultCell.PaddingBottom = 3;
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
                                    string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "Present";
                                    string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                    string description = reader["Description"]?.ToString() ?? "";

                                    Paragraph internPara = new Paragraph($"I interned as {jobTitle} at {company} from {startDate} to {endDate}, where I {description}.", contentFont)
                                    {
                                        Leading = 8
                                    };
                                    itemTable.AddCell(internPara);
                                }
                                else
                                {
                                    itemTable.AddCell(new Phrase("I have no internship experience recorded.", contentFont));
                                }
                            }
                        }
                    }
                    table.AddCell(itemTable);
                });

                // Linguistic Proficiencies (from Type 1 with Type 2 styling)
                addSection("Linguistic Proficiencies", table =>
                {
                    PdfPTable listTable = new PdfPTable(2) { WidthPercentage = 100 };
                    listTable.SetWidths(new float[] { 5, 95 });
                    listTable.DefaultCell.Border = Rectangle.NO_BORDER;
                    listTable.DefaultCell.PaddingLeft = 6;
                    listTable.DefaultCell.PaddingTop = 1.5f;
                    bool hasLanguages = false;
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
                                            listTable.AddCell(new Phrase("•", bulletFont));
                                            listTable.AddCell(new Phrase(language, contentFont));
                                            hasLanguages = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (!hasLanguages)
                    {
                        listTable.AddCell("");
                        listTable.AddCell(new Phrase("No languages listed", contentFont));
                    }
                    table.AddCell(listTable);
                });

                // Personal Interests (from Type 1 with Type 2 styling)
                addSection("Personal Interests", table =>
                {
                    PdfPTable listTable = new PdfPTable(2) { WidthPercentage = 100 };
                    listTable.SetWidths(new float[] { 5, 95 });
                    listTable.DefaultCell.Border = Rectangle.NO_BORDER;
                    listTable.DefaultCell.PaddingLeft = 6;
                    listTable.DefaultCell.PaddingTop = 1.5f;
                    bool hasHobbies = false;
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
                                            listTable.AddCell(new Phrase("•", bulletFont));
                                            listTable.AddCell(new Phrase(hobby, contentFont));
                                            hasHobbies = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (!hasHobbies)
                    {
                        listTable.AddCell("");
                        listTable.AddCell(new Phrase("No interests listed", contentFont));
                    }
                    table.AddCell(listTable);
                });

                // Additional Achievements (from Type 1 with Type 2 styling)
                addSection("Additional Achievements", table =>
                {
                    bool hasCustom = false;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("SELECT Title, StartDate, EndDate, Description FROM CustomSelection WHERE UserID = @UserID ORDER BY StartDate DESC", conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        string title = reader["Title"]?.ToString() ?? "";
                                        string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "Present";
                                        string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                        string description = reader["Description"]?.ToString() ?? "";

                                        PdfPTable itemTable = new PdfPTable(1) { WidthPercentage = 100 };
                                        itemTable.DefaultCell.Border = Rectangle.NO_BORDER;
                                        itemTable.DefaultCell.PaddingBottom = 3;
                                        Paragraph achievePara = new Paragraph($"I achieved {title} from {startDate} to {endDate}, described as {description}.", contentFont)
                                        {
                                            Leading = 8
                                        };
                                        itemTable.AddCell(achievePara);
                                        table.AddCell(itemTable);
                                        hasCustom = true;
                                    }
                                }
                            }
                        }
                    }
                    if (!hasCustom)
                    {
                        table.AddCell(new Phrase("No achievements listed.", contentFont));
                    }
                });

                // Personal Details (from Type 1 with Type 2 styling)
                addSection("Personal Details", table =>
                {
                    PdfPTable detailsTable = new PdfPTable(1) { WidthPercentage = 100 };
                    detailsTable.DefaultCell.Border = Rectangle.NO_BORDER;
                    detailsTable.DefaultCell.PaddingBottom = 2;
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
                                    string dob = reader["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(reader["DateOfBirth"]).ToString("MMM yyyy") : "Present";
                                    detailsTable.AddCell(new Phrase($"Date of Birth: {dob}", contentFont));
                                    detailsTable.AddCell(new Phrase($"Place of Birth: {reader["PlaceOfBirth"]?.ToString() ?? ""}", contentFont));
                                    detailsTable.AddCell(new Phrase($"Nationality: {reader["Nationality"]?.ToString() ?? ""}", contentFont));
                                }
                            }
                        }
                    }
                    table.AddCell(detailsTable);
                });

                containerTable.AddCell(contentCell);
                pdfDoc.Add(containerTable);

                // Ensure single page (same as Type 2)
                if (pdfDoc.PageNumber > 1)
                {
                    throw new Exception("Content exceeds one page. Further compression or content truncation needed.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding content to PDF: {ex.Message}\nStack Trace: {ex.StackTrace}", ex);
            }
        }

        protected string FormatDate(object date)
        {
            if (date == DBNull.Value || date == null)
                return "Present";
            return Convert.ToDateTime(date).ToString("MMM yyyy");
        }
    }
}