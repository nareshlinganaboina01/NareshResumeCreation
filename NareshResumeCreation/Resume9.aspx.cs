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
using System.Web;
using System.Text.RegularExpressions;

namespace NareshResumeCreation
{
    public partial class Resume9 : Page
    {
        private bool IsEditMode
        {
            get => ViewState["IsEditMode"] != null && (bool)ViewState["IsEditMode"];
            set => ViewState["IsEditMode"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            if (!IsPostBack)
            {
                IsEditMode = false;
                LoadAllData();
                UpdateEditMode();
            }
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

        private void UpdateEditMode()
        {
            btnToggleEdit.Text = IsEditMode ? "View Resume" : "Edit Resume";
            btnSave.Visible = IsEditMode;
            btnDownload.Visible = !IsEditMode;
            pnlViewPersonal.Visible = !IsEditMode;
            pnlEditPersonal.Visible = IsEditMode;
            pnlViewSummary.Visible = !IsEditMode;
            pnlEditSummary.Visible = IsEditMode;
            pnlViewSkills.Visible = !IsEditMode;
            pnlEditSkills.Visible = IsEditMode;
            pnlViewLanguages.Visible = !IsEditMode;
            pnlEditLanguages.Visible = IsEditMode;
            pnlViewHobbies.Visible = !IsEditMode;
            pnlEditHobbies.Visible = IsEditMode;
            pnlViewEmployment.Visible = !IsEditMode;
            pnlEditEmployment.Visible = IsEditMode;
            pnlViewEducation.Visible = !IsEditMode;
            pnlEditEducation.Visible = IsEditMode;
            pnlViewCertifications.Visible = !IsEditMode;
            pnlEditCertifications.Visible = IsEditMode;
            pnlViewInternship.Visible = !IsEditMode;
            pnlEditInternship.Visible = IsEditMode;
            
            pnlViewCustomSelection.Visible = !IsEditMode;
            pnlEditCustomSelection.Visible = IsEditMode;

            if (IsEditMode)
            {
                PopulateEditControls();
            }
        }

        private void PopulateEditControls()
        {
            txtEditFirstName.Text = text9FirstName.Text;
            txtEditLastName.Text = text9LastName.Text;
            txtEditJobTitle.Text = text9JobTitle.Text;
            txtEditEmail.Text = text9Email.Text;
            txtEditPhone.Text = text9Phone.Text;
            txtEditAddress.Text = text9Address.Text;
            txtEditCity.Text = text9City.Text;
            txtEditState.Text = text9State.Text;
            txtEditPostalCode.Text = text9PostalCode.Text;
            txtEditCountry.Text = text9Country.Text;
            txtEditDateOfBirth.Text = text9DateOfBirth.Text;
            txtEditPlaceOfBirth.Text = text9PlaceOfBirth.Text;
            txtEditNationality.Text = text9Nationality.Text;
            txtEditLinkedIn.Text = lnkLinkedIn.NavigateUrl;
            txtEditGitHub.Text = lnkGitHub.NavigateUrl;
            txtEditProfessionalSummary.Text = text9ProfessionalSummary.Text;
            txtEditInternJobTitle.Text = text9InternJobTitle.Text;
            txtEditInternCompany.Text = text9InternCompany.Text;
            txtEditInternStartDate.Text = text9InternStartDate.Text != "N/A" && DateTime.TryParse(text9InternStartDate.Text, out DateTime start)
                ? start.ToString("yyyy-MM-dd")
                : "";
            txtEditInternEndDate.Text = text9InternEndDate.Text != "N/A" && DateTime.TryParse(text9InternEndDate.Text, out DateTime end)
                ? end.ToString("yyyy-MM-dd")
                : "";
            txtEditInternDescription.Text = text9InternDescription.Text;

            LoadEditRepeaters();
        }

        private void LoadEditRepeaters()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Skills
                using (SqlCommand cmd = new SqlCommand("SELECT ID, SkillName FROM Skills WHERE UserID = @UserID ORDER BY ID", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    DataTable dt = new DataTable();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    rptEditSkills.DataSource = dt;
                    rptEditSkills.DataBind();
                }
                // Languages
                using (SqlCommand cmd = new SqlCommand("SELECT ID, LanguageName FROM Languages WHERE UserID = @UserID ORDER BY ID", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    DataTable dt = new DataTable();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    rptEditLanguages.DataSource = dt;
                    rptEditLanguages.DataBind();
                }
                // Hobbies
                using (SqlCommand cmd = new SqlCommand("SELECT ID, Name FROM Hobbies WHERE UserID = @UserID ORDER BY ID", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    DataTable dt = new DataTable();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    rptEditHobbies.DataSource = dt;
                    rptEditHobbies.DataBind();
                }
                // Employment History
                using (SqlCommand cmd = new SqlCommand("SELECT ID, JobTitle, Employer, City, StartDate, EndDate, Description FROM EmploymentHistory WHERE UserID = @UserID ORDER BY StartDate DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    DataTable dt = new DataTable();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    rptEditEmploymentHistory.DataSource = dt;
                    rptEditEmploymentHistory.DataBind();
                }
                // Education
                using (SqlCommand cmd = new SqlCommand("SELECT ID, Degree, SchoolName, City, StartDate, EndDate, Description FROM Education WHERE UserID = @UserID ORDER BY StartDate DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    DataTable dt = new DataTable();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    rptEditEducation.DataSource = dt;
                    rptEditEducation.DataBind();
                }
                // Certifications
                using (SqlCommand cmd = new SqlCommand("SELECT ID, CourseName, Institution, StartDate, EndDate FROM Courses WHERE UserID = @UserID ORDER BY StartDate DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    DataTable dt = new DataTable();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    rptEditCertifications.DataSource = dt;
                    rptEditCertifications.DataBind();
                }
                
                // Custom Selection
                using (SqlCommand cmd = new SqlCommand("SELECT ID, Title, StartDate, EndDate, Description FROM CustomSelection WHERE UserID = @UserID ORDER BY StartDate DESC", conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
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

        private void LoadPersonalData()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT TOP 1 * FROM PersonalDetails WHERE UserID = @UserID ORDER BY ID DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            text9JobTitle.Text = reader["JobTitle"]?.ToString() ?? "No Data";
                            text9FirstName.Text = reader["FirstName"]?.ToString() ?? "No Data";
                            text9LastName.Text = reader["LastName"]?.ToString() ?? "No Data";
                            text9Email.Text = reader["Email"]?.ToString() ?? "No Data";
                            text9Phone.Text = reader["Phone"]?.ToString() ?? "No Data";
                            text9DateOfBirth.Text = reader["DateOfBirth"] != DBNull.Value && DateTime.TryParse(reader["DateOfBirth"].ToString(), out DateTime dob)
                                ? dob.ToString("yyyy-MM-dd")
                                : "N/A";
                            text9Country.Text = reader["Country"]?.ToString() ?? "No Data";
                            text9State.Text = reader["State"]?.ToString() ?? "No Data";
                            text9City.Text = reader["City"]?.ToString() ?? "No Data";
                            text9Address.Text = reader["Address"]?.ToString() ?? "No Data";
                            text9PostalCode.Text = reader["PostalCode"]?.ToString() ?? "No Data";
                            text9Nationality.Text = reader["Nationality"]?.ToString() ?? "No Data";
                            text9PlaceOfBirth.Text = reader["PlaceOfBirth"]?.ToString() ?? "No Data";
                            text9ProfessionalSummary.Text = reader["PersonalSummary"]?.ToString() ?? "No Data";
                            }
                        else
                        {
                            SetDefaultValues();
                        }
                    }
                }
            }
        }

        private void SetDefaultValues()
        {
            text9JobTitle.Text = "No Data";
            text9FirstName.Text = "No Data";
            text9LastName.Text = "No Data";
            text9Email.Text = "No Data";
            text9Phone.Text = "No Data";
            text9DateOfBirth.Text = "N/A";
            text9Country.Text = "No Data";
            text9State.Text = "No Data";
            text9City.Text = "No Data";
            text9Address.Text = "No Data";
            text9PostalCode.Text = "No Data";
            text9Nationality.Text = "No Data";
            text9PlaceOfBirth.Text = "No Data";
            text9ProfessionalSummary.Text = "No Data";
            
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
                    pnl9Fresher.Visible = isFresher;
                    pnl9EmploymentHistory.Visible = !isFresher;
                }
            }
        }

        private void LoadEmploymentHistory()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"SELECT JobTitle, Employer, City, StartDate, EndDate, Description 
                                FROM EmploymentHistory WHERE UserID = @UserID ORDER BY StartDate DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    DataTable dt = new DataTable();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    rpt9EmploymentHistory.DataSource = dt;
                    rpt9EmploymentHistory.DataBind();
                }
            }
        }

        private void LoadEducationHistory()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT Degree, SchoolName, City, StartDate, EndDate, Description FROM Education WHERE UserID = @UserID ORDER BY StartDate DESC";
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
                        rpt9Education.DataSource = dt;
                    }
                    else
                    {
                        DataTable fallbackDt = new DataTable();
                        fallbackDt.Columns.Add("Degree", typeof(string));
                        fallbackDt.Columns.Add("SchoolName", typeof(string));
                        fallbackDt.Columns.Add("City", typeof(string));
                        fallbackDt.Columns.Add("StartDate", typeof(DateTime));
                        fallbackDt.Columns.Add("EndDate", typeof(DateTime));
                        fallbackDt.Columns.Add("Description", typeof(string));
                        DataRow row = fallbackDt.NewRow();
                        row["Degree"] = "No education found";
                        row["SchoolName"] = "";
                        row["City"] = "";
                        row["StartDate"] = DBNull.Value;
                        row["EndDate"] = DBNull.Value;
                        row["Description"] = "";
                        fallbackDt.Rows.Add(row);
                        rpt9Education.DataSource = fallbackDt;
                    }
                    rpt9Education.DataBind();
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
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        rpt9Skills.DataSource = dt;
                    }
                    else
                    {
                        DataTable fallbackDt = new DataTable();
                        fallbackDt.Columns.Add("SkillName", typeof(string));
                        DataRow row = fallbackDt.NewRow();
                        row["SkillName"] = "No skills found";
                        fallbackDt.Rows.Add(row);
                        rpt9Skills.DataSource = fallbackDt;
                    }
                    rpt9Skills.DataBind();
                }
            }
        }

        private void LoadInternships()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT TOP 1 CompanyName, JobTitle, StartDate, EndDate, Description FROM Internships WHERE UserID = @UserID ORDER BY ID DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            text9InternCompany.Text = reader["CompanyName"]?.ToString() ?? "N/A";
                            text9InternJobTitle.Text = reader["JobTitle"]?.ToString() ?? "N/A";
                            text9InternStartDate.Text = reader["StartDate"] != DBNull.Value && DateTime.TryParse(reader["StartDate"].ToString(), out DateTime startDate)
                                ? startDate.ToString("MMM yyyy")
                                : "N/A";
                            text9InternEndDate.Text = reader["EndDate"] != DBNull.Value && DateTime.TryParse(reader["EndDate"].ToString(), out DateTime endDate)
                                ? endDate.ToString("MMM yyyy")
                                : "N/A";
                            text9InternDescription.Text = reader["Description"]?.ToString() ?? "No internship data available";
                        }
                        else
                        {
                            text9InternCompany.Text = "N/A";
                            text9InternJobTitle.Text = "N/A";
                            text9InternStartDate.Text = "N/A";
                            text9InternEndDate.Text = "N/A";
                            text9InternDescription.Text = "No internship data available";
                        }
                    }
                }
            }
        }

        private void LoadCertifications()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT CourseName, Institution, StartDate, EndDate FROM Courses WHERE UserID = @UserID ORDER BY StartDate DESC";
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
                        rpt9Certifications.DataSource = dt;
                    }
                    else
                    {
                        DataTable fallbackDt = new DataTable();
                        fallbackDt.Columns.Add("CourseName", typeof(string));
                        fallbackDt.Columns.Add("Institution", typeof(string));
                        fallbackDt.Columns.Add("StartDate", typeof(DateTime));
                        fallbackDt.Columns.Add("EndDate", typeof(DateTime));
                        DataRow row = fallbackDt.NewRow();
                        row["CourseName"] = "No certifications found";
                        row["Institution"] = "";
                        row["StartDate"] = DBNull.Value;
                        row["EndDate"] = DBNull.Value;
                        fallbackDt.Rows.Add(row);
                        rpt9Certifications.DataSource = fallbackDt;
                    }
                    rpt9Certifications.DataBind();
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
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        rpt9Languages.DataSource = dt;
                    }
                    else
                    {
                        DataTable fallbackDt = new DataTable();
                        fallbackDt.Columns.Add("LanguageName", typeof(string));
                        DataRow row = fallbackDt.NewRow();
                        row["LanguageName"] = "No languages found";
                        fallbackDt.Rows.Add(row);
                        rpt9Languages.DataSource = fallbackDt;
                    }
                    rpt9Languages.DataBind();
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
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);
                    }
                    if (dt.Rows.Count > 0)
                    {
                        rpt9Hobbies.DataSource = dt;
                    }
                    else
                    {
                        DataTable fallbackDt = new DataTable();
                        fallbackDt.Columns.Add("Name", typeof(string));
                        DataRow row = fallbackDt.NewRow();
                        row["Name"] = "No hobbies found";
                        fallbackDt.Rows.Add(row);
                        rpt9Hobbies.DataSource = fallbackDt;
                    }
                    rpt9Hobbies.DataBind();
                }
            }
        }

        

        private void LoadCustomSelection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT Title, StartDate, EndDate, Description FROM CustomSelection WHERE UserID = @UserID ORDER BY StartDate DESC";
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
                        rpt9CustomSelection.DataSource = dt;
                    }
                    else
                    {
                        DataTable fallbackDt = new DataTable();
                        fallbackDt.Columns.Add("Title", typeof(string));
                        fallbackDt.Columns.Add("StartDate", typeof(DateTime));
                        fallbackDt.Columns.Add("EndDate", typeof(DateTime));
                        fallbackDt.Columns.Add("Description", typeof(string));
                        DataRow row = fallbackDt.NewRow();
                        row["Title"] = "No additional info";
                        row["StartDate"] = DBNull.Value;
                        row["EndDate"] = DBNull.Value;
                        row["Description"] = "";
                        fallbackDt.Rows.Add(row);
                        rpt9CustomSelection.DataSource = fallbackDt;
                    }
                    rpt9CustomSelection.DataBind();
                }
            }
        }

        protected void btnToggleEdit_Click(object sender, EventArgs e)
        {
            IsEditMode = !IsEditMode;
            UpdateEditMode();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SavePersonalDetails();
                SaveSkills();
                SaveLanguages();
                SaveHobbies();
                SaveEmploymentHistory();
                SaveEducation();
                SaveCertifications();
                SaveInternship();
                
                SaveCustomSelection();
                SaveFresherStatus();
                IsEditMode = false;
                LoadAllData();
                UpdateEditMode();
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Changes saved successfully!');", true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error saving changes: {ex.Message}');", true);
            }
        }

        private void SavePersonalDetails()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"IF EXISTS (SELECT 1 FROM PersonalDetails WHERE UserID = @UserID)
                                UPDATE PersonalDetails SET FirstName = @FirstName, LastName = @LastName, JobTitle = @JobTitle, Email = @Email, Phone = @Phone,
                                    Address = @Address, City = @City, State = @State, PostalCode = @PostalCode, Country = @Country, DateOfBirth = @DateOfBirth,
                                    PlaceOfBirth = @PlaceOfBirth, Nationality = @Nationality, PersonalSummary = @PersonalSummary, LinkedIn = @LinkedIn, GitHub = @GitHub
                                WHERE UserID = @UserID
                                ELSE
                                INSERT INTO PersonalDetails (UserID, FirstName, LastName, JobTitle, Email, Phone, Address, City, State, PostalCode, Country, DateOfBirth, PlaceOfBirth, Nationality, PersonalSummary, LinkedIn, GitHub)
                                VALUES (@UserID, @FirstName, @LastName, @JobTitle, @Email, @Phone, @Address, @City, @State, @PostalCode, @Country, @DateOfBirth, @PlaceOfBirth, @Nationality, @PersonalSummary, @LinkedIn, @GitHub)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.Parameters.AddWithValue("@FirstName", txtEditFirstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", txtEditLastName.Text);
                    cmd.Parameters.AddWithValue("@JobTitle", txtEditJobTitle.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEditEmail.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtEditPhone.Text);
                    cmd.Parameters.AddWithValue("@Address", txtEditAddress.Text);
                    cmd.Parameters.AddWithValue("@City", txtEditCity.Text);
                    cmd.Parameters.AddWithValue("@State", txtEditState.Text);
                    cmd.Parameters.AddWithValue("@PostalCode", txtEditPostalCode.Text);
                    cmd.Parameters.AddWithValue("@Country", txtEditCountry.Text);
                    cmd.Parameters.AddWithValue("@DateOfBirth", DateTime.TryParse(txtEditDateOfBirth.Text, out DateTime dob) ? dob : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PlaceOfBirth", txtEditPlaceOfBirth.Text);
                    cmd.Parameters.AddWithValue("@Nationality", txtEditNationality.Text);
                    cmd.Parameters.AddWithValue("@PersonalSummary", txtEditProfessionalSummary.Text);
                    cmd.Parameters.AddWithValue("@LinkedIn", txtEditLinkedIn.Text);
                    cmd.Parameters.AddWithValue("@GitHub", txtEditGitHub.Text);
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
                // Update existing skills
                foreach (RepeaterItem item in rptEditSkills.Items)
                {
                    var txtSkillName = item.FindControl("txtSkillName") as TextBox;
                    var hdnSkillId = item.FindControl("hdnSkillId") as HiddenField;
                    if (txtSkillName != null && hdnSkillId != null && !string.IsNullOrWhiteSpace(txtSkillName.Text))
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE Skills SET SkillName = @SkillName WHERE ID = @ID AND UserID = @UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@SkillName", txtSkillName.Text);
                            cmd.Parameters.AddWithValue("@ID", hdnSkillId.Value);
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                // Add new skill
                if (!string.IsNullOrWhiteSpace(txtNewSkill.Text))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Skills (UserID, SkillName) VALUES (@UserID, @SkillName)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@SkillName", txtNewSkill.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        protected void rptEditSkills_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Skills WHERE ID = @ID AND UserID = @UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditRepeaters();
            }
        }

        protected void btnAddSkill_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewSkill.Text))
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Skills (UserID, SkillName) VALUES (@UserID, @SkillName)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@SkillName", txtNewSkill.Text);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                txtNewSkill.Text = "";
                LoadEditRepeaters();
            }
        }

        private void SaveLanguages()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Update existing languages
                foreach (RepeaterItem item in rptEditLanguages.Items)
                {
                    var txtLanguageName = item.FindControl("txtLanguageName") as TextBox;
                    var hdnLanguageId = item.FindControl("hdnLanguageId") as HiddenField;
                    if (txtLanguageName != null && hdnLanguageId != null && !string.IsNullOrWhiteSpace(txtLanguageName.Text))
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE Languages SET LanguageName = @LanguageName WHERE ID = @ID AND UserID = @UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@LanguageName", txtLanguageName.Text);
                            cmd.Parameters.AddWithValue("@ID", hdnLanguageId.Value);
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                // Add new language
                if (!string.IsNullOrWhiteSpace(txtNewLanguage.Text))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Languages (UserID, LanguageName) VALUES (@UserID, @LanguageName)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@LanguageName", txtNewLanguage.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        protected void rptEditLanguages_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Languages WHERE ID = @ID AND UserID = @UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditRepeaters();
            }
        }

        protected void btnAddLanguage_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewLanguage.Text))
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Languages (UserID, LanguageName) VALUES (@UserID, @LanguageName)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@LanguageName", txtNewLanguage.Text);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                txtNewLanguage.Text = "";
                LoadEditRepeaters();
            }
        }

        private void SaveHobbies()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Update existing hobbies
                foreach (RepeaterItem item in rptEditHobbies.Items)
                {
                    var txtHobbyName = item.FindControl("txtHobbyName") as TextBox;
                    var hdnHobbyId = item.FindControl("hdnHobbyId") as HiddenField;
                    if (txtHobbyName != null && hdnHobbyId != null && !string.IsNullOrWhiteSpace(txtHobbyName.Text))
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE Hobbies SET Name = @Name WHERE ID = @ID AND UserID = @UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@Name", txtHobbyName.Text);
                            cmd.Parameters.AddWithValue("@ID", hdnHobbyId.Value);
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                // Add new hobby
                if (!string.IsNullOrWhiteSpace(txtNewHobby.Text))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Hobbies (UserID, Name) VALUES (@UserID, @Name)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@Name", txtNewHobby.Text);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        protected void rptEditHobbies_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Hobbies WHERE ID = @ID AND UserID = @UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditRepeaters();
            }
        }

        protected void btnAddHobby_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewHobby.Text))
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Hobbies (UserID, Name) VALUES (@UserID, @Name)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@Name", txtNewHobby.Text);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                txtNewHobby.Text = "";
                LoadEditRepeaters();
            }
        }

        private void SaveEmploymentHistory()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Update existing employment history
                foreach (RepeaterItem item in rptEditEmploymentHistory.Items)
                {
                    var txtJobTitle = item.FindControl("txtJobTitle") as TextBox;
                    var txtEmployer = item.FindControl("txtEmployer") as TextBox;
                    var txtCity = item.FindControl("txtCity") as TextBox;
                    var txtStartDate = item.FindControl("txtStartDate") as TextBox;
                    var txtEndDate = item.FindControl("txtEndDate") as TextBox;
                    var txtDescription = item.FindControl("txtDescription") as TextBox;
                    var hdnEmploymentId = item.FindControl("hdnEmploymentId") as HiddenField;

                    if (txtJobTitle != null && hdnEmploymentId != null && !string.IsNullOrWhiteSpace(txtJobTitle.Text))
                    {
                        if (!DateTime.TryParse(txtStartDate.Text, out DateTime startDate))
                        {
                            throw new Exception("Invalid start date format in employment history.");
                        }
                        DateTime? endDate = string.IsNullOrWhiteSpace(txtEndDate.Text) ? (DateTime?)null : DateTime.Parse(txtEndDate.Text);

                        using (SqlCommand cmd = new SqlCommand(@"UPDATE EmploymentHistory SET JobTitle = @JobTitle, Employer = @Employer, City = @City, 
                                                                StartDate = @StartDate, EndDate = @EndDate, Description = @Description 
                                                                WHERE ID = @ID AND UserID = @UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@JobTitle", txtJobTitle.Text);
                            cmd.Parameters.AddWithValue("@Employer", txtEmployer.Text ?? "");
                            cmd.Parameters.AddWithValue("@City", txtCity.Text ?? "");
                            cmd.Parameters.AddWithValue("@StartDate", startDate);
                            cmd.Parameters.AddWithValue("@EndDate", endDate ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Description", txtDescription.Text ?? "");
                            cmd.Parameters.AddWithValue("@ID", hdnEmploymentId.Value);
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                // Add new employment
                if (!string.IsNullOrWhiteSpace(txtNewJobTitle.Text) && !chkIsFresher.Checked)
                {
                    if (!DateTime.TryParse(txtNewEmploymentStartDate.Text, out DateTime newStartDate))
                    {
                        throw new Exception("Invalid start date format for new employment.");
                    }
                    DateTime? newEndDate = string.IsNullOrWhiteSpace(txtNewEmploymentEndDate.Text) ? (DateTime?)null : DateTime.Parse(txtNewEmploymentEndDate.Text);

                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO EmploymentHistory (UserID, JobTitle, Employer, City, StartDate, EndDate, Description) 
                                                            VALUES (@UserID, @JobTitle, @Employer, @City, @StartDate, @EndDate, @Description)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@JobTitle", txtNewJobTitle.Text);
                        cmd.Parameters.AddWithValue("@Employer", txtNewEmployer.Text ?? "");
                        cmd.Parameters.AddWithValue("@City", txtNewEmploymentCity.Text ?? "");
                        cmd.Parameters.AddWithValue("@StartDate", newStartDate);
                        cmd.Parameters.AddWithValue("@EndDate", newEndDate ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Description", txtNewEmploymentDescription.Text ?? "");
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        protected void rptEditEmploymentHistory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM EmploymentHistory WHERE ID = @ID AND UserID = @UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditRepeaters();
            }
        }

        protected void btnAddEmployment_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewJobTitle.Text) && !chkIsFresher.Checked)
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    if (!DateTime.TryParse(txtNewEmploymentStartDate.Text, out DateTime startDate))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid start date format for new employment.');", true);
                        return;
                    }
                    DateTime? endDate = string.IsNullOrWhiteSpace(txtNewEmploymentEndDate.Text) ? (DateTime?)null : DateTime.Parse(txtNewEmploymentEndDate.Text);

                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO EmploymentHistory (UserID, JobTitle, Employer, City, StartDate, EndDate, Description) 
                                                            VALUES (@UserID, @JobTitle, @Employer, @City, @StartDate, @EndDate, @Description)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@JobTitle", txtNewJobTitle.Text);
                        cmd.Parameters.AddWithValue("@Employer", txtNewEmployer.Text ?? "");
                        cmd.Parameters.AddWithValue("@City", txtNewEmploymentCity.Text ?? "");
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Description", txtNewEmploymentDescription.Text ?? "");
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                txtNewJobTitle.Text = "";
                txtNewEmployer.Text = "";
                txtNewEmploymentCity.Text = "";
                txtNewEmploymentStartDate.Text = "";
                txtNewEmploymentEndDate.Text = "";
                txtNewEmploymentDescription.Text = "";
                LoadEditRepeaters();
            }
        }

        protected void chkIsFresher_CheckedChanged(object sender, EventArgs e)
        {
            pnlEditEmploymentHistory.Visible = !chkIsFresher.Checked;
            if (chkIsFresher.Checked)
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM EmploymentHistory WHERE UserID = @UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditRepeaters();
            }
        }

        private void SaveEducation()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Update existing education
                foreach (RepeaterItem item in rptEditEducation.Items)
                {
                    var txtDegree = item.FindControl("txtDegree") as TextBox;
                    var txtSchoolName = item.FindControl("txtSchoolName") as TextBox;
                    var txtCity = item.FindControl("txtCity") as TextBox;
                    var txtStartDate = item.FindControl("txtStartDate") as TextBox;
                    var txtEndDate = item.FindControl("txtEndDate") as TextBox;
                    var txtDescription = item.FindControl("txtDescription") as TextBox;
                    var hdnEducationId = item.FindControl("hdnEducationId") as HiddenField;

                    if (txtDegree != null && hdnEducationId != null && !string.IsNullOrWhiteSpace(txtDegree.Text))
                    {
                        if (!DateTime.TryParse(txtStartDate.Text, out DateTime startDate))
                        {
                            throw new Exception("Invalid start date format in education.");
                        }
                        DateTime? endDate = string.IsNullOrWhiteSpace(txtEndDate.Text) ? (DateTime?)null : DateTime.Parse(txtEndDate.Text);

                        using (SqlCommand cmd = new SqlCommand(@"UPDATE Education SET Degree = @Degree, SchoolName = @SchoolName, City = @City, 
                                                                StartDate = @StartDate, EndDate = @EndDate, Description = @Description 
                                                                WHERE ID = @ID AND UserID = @UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@Degree", txtDegree.Text);
                            cmd.Parameters.AddWithValue("@SchoolName", txtSchoolName.Text ?? "");
                            cmd.Parameters.AddWithValue("@City", txtCity.Text ?? "");
                            cmd.Parameters.AddWithValue("@StartDate", startDate);
                            cmd.Parameters.AddWithValue("@EndDate", endDate ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Description", txtDescription.Text ?? "");
                            cmd.Parameters.AddWithValue("@ID", hdnEducationId.Value);
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                // Add new education
                if (!string.IsNullOrWhiteSpace(txtNewDegree.Text))
                {
                    if (!DateTime.TryParse(txtNewEducationStartDate.Text, out DateTime newStartDate))
                    {
                        throw new Exception("Invalid start date format for new education.");
                    }
                    DateTime? newEndDate = string.IsNullOrWhiteSpace(txtNewEducationEndDate.Text) ? (DateTime?)null : DateTime.Parse(txtNewEducationEndDate.Text);

                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO Education (UserID, Degree, SchoolName, City, StartDate, EndDate, Description) 
                                                            VALUES (@UserID, @Degree, @SchoolName, @City, @StartDate, @EndDate, @Description)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@Degree", txtNewDegree.Text);
                        cmd.Parameters.AddWithValue("@SchoolName", txtNewSchoolName.Text ?? "");
                        cmd.Parameters.AddWithValue("@City", txtNewEducationCity.Text ?? "");
                        cmd.Parameters.AddWithValue("@StartDate", newStartDate);
                        cmd.Parameters.AddWithValue("@EndDate", newEndDate ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Description", txtNewEducationDescription.Text ?? "");
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        protected void rptEditEducation_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Education WHERE ID = @ID AND UserID = @UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditRepeaters();
            }
        }

        protected void btnAddEducation_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewDegree.Text))
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    if (!DateTime.TryParse(txtNewEducationStartDate.Text, out DateTime startDate))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid start date format for new education.');", true);
                        return;
                    }
                    DateTime? endDate = string.IsNullOrWhiteSpace(txtNewEducationEndDate.Text) ? (DateTime?)null : DateTime.Parse(txtNewEducationEndDate.Text);

                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO Education (UserID, Degree, SchoolName, City, StartDate, EndDate, Description) 
                                                            VALUES (@UserID, @Degree, @SchoolName, @City, @StartDate, @EndDate, @Description)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@Degree", txtNewDegree.Text);
                        cmd.Parameters.AddWithValue("@SchoolName", txtNewSchoolName.Text ?? "");
                        cmd.Parameters.AddWithValue("@City", txtNewEducationCity.Text ?? "");
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Description", txtNewEducationDescription.Text ?? "");
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
                LoadEditRepeaters();
            }
        }

        private void SaveCertifications()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Update existing certifications
                foreach (RepeaterItem item in rptEditCertifications.Items)
                {
                    var txtCourseName = item.FindControl("txtCourseName") as TextBox;
                    var txtInstitution = item.FindControl("txtInstitution") as TextBox;
                    var txtStartDate = item.FindControl("txtStartDate") as TextBox;
                    var txtEndDate = item.FindControl("txtEndDate") as TextBox;
                    var hdnCertificationId = item.FindControl("hdnCertificationId") as HiddenField;

                    if (txtCourseName != null && hdnCertificationId != null && !string.IsNullOrWhiteSpace(txtCourseName.Text))
                    {
                        if (!DateTime.TryParse(txtStartDate.Text, out DateTime startDate))
                        {
                            throw new Exception("Invalid start date format in certifications.");
                        }
                        DateTime? endDate = string.IsNullOrWhiteSpace(txtEndDate.Text) ? (DateTime?)null : DateTime.Parse(txtEndDate.Text);

                        using (SqlCommand cmd = new SqlCommand(@"UPDATE Courses SET CourseName = @CourseName, Institution = @Institution, 
                                                                StartDate = @StartDate, EndDate = @EndDate 
                                                                WHERE ID = @ID AND UserID = @UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text);
                            cmd.Parameters.AddWithValue("@Institution", txtInstitution.Text ?? "");
                            cmd.Parameters.AddWithValue("@StartDate", startDate);
                            cmd.Parameters.AddWithValue("@EndDate", endDate ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@ID", hdnCertificationId.Value);
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                // Add new certification
                if (!string.IsNullOrWhiteSpace(txtNewCourseName.Text))
                {
                    if (!DateTime.TryParse(txtNewCertificationStartDate.Text, out DateTime newStartDate))
                    {
                        throw new Exception("Invalid start date format for new certification.");
                    }
                    DateTime? newEndDate = string.IsNullOrWhiteSpace(txtNewCertificationEndDate.Text) ? (DateTime?)null : DateTime.Parse(txtNewCertificationEndDate.Text);

                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO Courses (UserID, CourseName, Institution, StartDate, EndDate) 
                                                            VALUES (@UserID, @CourseName, @Institution, @StartDate, @EndDate)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@CourseName", txtNewCourseName.Text);
                        cmd.Parameters.AddWithValue("@Institution", txtNewInstitution.Text ?? "");
                        cmd.Parameters.AddWithValue("@StartDate", newStartDate);
                        cmd.Parameters.AddWithValue("@EndDate", newEndDate ?? (object)DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        protected void rptEditCertifications_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Courses WHERE ID = @ID AND UserID = @UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditRepeaters();
            }
        }

        protected void btnAddCertification_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewCourseName.Text))
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    if (!DateTime.TryParse(txtNewCertificationStartDate.Text, out DateTime startDate))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid start date format for new certification.');", true);
                        return;
                    }
                    DateTime? endDate = string.IsNullOrWhiteSpace(txtNewCertificationEndDate.Text) ? (DateTime?)null : DateTime.Parse(txtNewCertificationEndDate.Text);

                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO Courses (UserID, CourseName, Institution, StartDate, EndDate) 
                                                            VALUES (@UserID, @CourseName, @Institution, @StartDate, @EndDate)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@CourseName", txtNewCourseName.Text);
                        cmd.Parameters.AddWithValue("@Institution", txtNewInstitution.Text ?? "");
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate ?? (object)DBNull.Value);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                txtNewCourseName.Text = "";
                txtNewInstitution.Text = "";
                txtNewCertificationStartDate.Text = "";
                txtNewCertificationEndDate.Text = "";
                LoadEditRepeaters();
            }
        }

        private void SaveInternship()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"IF EXISTS (SELECT 1 FROM Internships WHERE UserID = @UserID)
                                UPDATE Internships SET CompanyName = @CompanyName, JobTitle = @JobTitle, StartDate = @StartDate, EndDate = @EndDate, Description = @Description
                                WHERE UserID = @UserID
                                ELSE
                                INSERT INTO Internships (UserID, CompanyName, JobTitle, StartDate, EndDate, Description)
                                VALUES (@UserID, @CompanyName, @JobTitle, @StartDate, @EndDate, @Description)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    cmd.Parameters.AddWithValue("@CompanyName", txtEditInternCompany.Text);
                    cmd.Parameters.AddWithValue("@JobTitle", txtEditInternJobTitle.Text);
                    cmd.Parameters.AddWithValue("@StartDate", DateTime.TryParse(txtEditInternStartDate.Text, out DateTime startDate) ? startDate : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@EndDate", DateTime.TryParse(txtEditInternEndDate.Text, out DateTime endDate) ? endDate : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Description", txtEditInternDescription.Text ?? "");
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        
        private void SaveCustomSelection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Update existing custom selections
                foreach (RepeaterItem item in rptEditCustomSelection.Items)
                {
                    var txtTitle = item.FindControl("txtTitle") as TextBox;
                    var txtStartDate = item.FindControl("txtStartDate") as TextBox;
                    var txtEndDate = item.FindControl("txtEndDate") as TextBox;
                    var txtDescription = item.FindControl("txtDescription") as TextBox;
                    var hdnCustomId = item.FindControl("hdnCustomId") as HiddenField;

                    if (txtTitle != null && hdnCustomId != null && !string.IsNullOrWhiteSpace(txtTitle.Text))
                    {
                        if (!DateTime.TryParse(txtStartDate.Text, out DateTime startDate))
                        {
                            throw new Exception("Invalid start date format in additional information.");
                        }
                        DateTime? endDate = string.IsNullOrWhiteSpace(txtEndDate.Text) ? (DateTime?)null : DateTime.Parse(txtEndDate.Text);

                        using (SqlCommand cmd = new SqlCommand(@"UPDATE CustomSelection SET Title = @Title, StartDate = @StartDate, EndDate = @EndDate, Description = @Description 
                                                                WHERE ID = @ID AND UserID = @UserID", conn))
                        {
                            cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                            cmd.Parameters.AddWithValue("@StartDate", startDate);
                            cmd.Parameters.AddWithValue("@EndDate", endDate ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Description", txtDescription.Text ?? "");
                            cmd.Parameters.AddWithValue("@ID", hdnCustomId.Value);
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                // Add new custom selection
                if (!string.IsNullOrWhiteSpace(txtNewCustomTitle.Text))
                {
                    if (!DateTime.TryParse(txtNewCustomStartDate.Text, out DateTime newStartDate))
                    {
                        throw new Exception("Invalid start date format for new additional information.");
                    }
                    DateTime? newEndDate = string.IsNullOrWhiteSpace(txtNewCustomEndDate.Text) ? (DateTime?)null : DateTime.Parse(txtNewCustomEndDate.Text);

                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO CustomSelection (UserID, Title, StartDate, EndDate, Description) 
                                                            VALUES (@UserID, @Title, @StartDate, @EndDate, @Description)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@Title", txtNewCustomTitle.Text);
                        cmd.Parameters.AddWithValue("@StartDate", newStartDate);
                        cmd.Parameters.AddWithValue("@EndDate", newEndDate ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Description", txtNewCustomDescription.Text ?? "");
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        protected void rptEditCustomSelection_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM CustomSelection WHERE ID = @ID AND UserID = @UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", e.CommandArgument);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEditRepeaters();
            }
        }

        protected void btnAddCustom_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewCustomTitle.Text))
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    if (!DateTime.TryParse(txtNewCustomStartDate.Text, out DateTime startDate))
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Invalid start date format for new additional information.');", true);
                        return;
                    }
                    DateTime? endDate = string.IsNullOrWhiteSpace(txtNewCustomEndDate.Text) ? (DateTime?)null : DateTime.Parse(txtNewCustomEndDate.Text);

                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO CustomSelection (UserID, Title, StartDate, EndDate, Description) 
                                                            VALUES (@UserID, @Title, @StartDate, @EndDate, @Description)", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.Parameters.AddWithValue("@Title", txtNewCustomTitle.Text);
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Description", txtNewCustomDescription.Text ?? "");
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                txtNewCustomTitle.Text = "";
                txtNewCustomStartDate.Text = "";
                txtNewCustomEndDate.Text = "";
                txtNewCustomDescription.Text = "";
                LoadEditRepeaters();
            }
        }

        private void SaveFresherStatus()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "UPDATE Users SET IsFresher = @IsFresher WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IsFresher", chkIsFresher.Checked);
                    cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
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
                        string firstName = text9FirstName.Text.Replace(" ", "_");
                        string lastName = text9LastName.Text.Replace(" ", "_");
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
                // Fonts (px to pt: 1px ≈ 0.75pt)
                Font titleFont = FontFactory.GetFont("Arial", 18f, Font.BOLD); // ~24px
                Font sectionFont = FontFactory.GetFont("Arial", 12f, Font.BOLD); // ~16px
                Font normalFont = FontFactory.GetFont("Arial", 9f, Font.NORMAL); // ~12px
                Font smallFont = FontFactory.GetFont("Arial", 9f, Font.NORMAL); // ~12px for contact details

                // Line separator (1px solid #ccc)
                LineSeparator line = new LineSeparator(1f, 100f, BaseColor.LIGHT_GRAY, Element.ALIGN_CENTER, -2f);

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
                                Paragraph header = new Paragraph(fullName, titleFont);
                                header.Alignment = Element.ALIGN_CENTER;
                                header.SpacingAfter = 3.75f; // ~5px
                                pdfDoc.Add(header);

                                string jobTitle = reader["JobTitle"]?.ToString() ?? text9JobTitle.Text?.Trim() ?? "";
                                pdfDoc.Add(new Paragraph(jobTitle, normalFont) { Alignment = Element.ALIGN_CENTER, SpacingAfter = 3.75f });

                                string contact = $"Email: {reader["Email"] ?? text9Email.Text?.Trim() ?? ""} | Phone: {reader["Phone"] ?? text9Phone.Text?.Trim() ?? ""}";
                                pdfDoc.Add(new Paragraph(contact, smallFont) { Alignment = Element.ALIGN_CENTER, SpacingAfter = 3.75f });

                                string address = $"{reader["Address"] ?? text9Address.Text?.Trim() ?? ""}, {reader["City"] ?? text9City.Text?.Trim() ?? ""}, {reader["State"] ?? text9State.Text?.Trim() ?? ""}, {reader["PostalCode"] ?? text9PostalCode.Text?.Trim() ?? ""}, {reader["Country"] ?? text9Country.Text?.Trim() ?? ""}";
                                pdfDoc.Add(new Paragraph(address, smallFont) { Alignment = Element.ALIGN_CENTER, SpacingAfter = 3.75f });

                                string dob = reader["DateOfBirth"] != DBNull.Value ? Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd") : text9DateOfBirth.Text?.Trim() ?? "";
                                string personalDetails = $"Date of Birth: {dob} | Place of Birth: {reader["PlaceOfBirth"] ?? text9PlaceOfBirth.Text?.Trim() ?? ""} | Nationality: {reader["Nationality"] ?? text9Nationality.Text?.Trim() ?? ""}";
                                pdfDoc.Add(new Paragraph(personalDetails, smallFont) { Alignment = Element.ALIGN_CENTER, SpacingAfter = 3.75f });

                                }
                            else
                            {
                                pdfDoc.Add(new Paragraph("User Name", titleFont) { Alignment = Element.ALIGN_CENTER, SpacingAfter = 11.25f });
                            }
                        }
                    }
                }
                pdfDoc.Add(new Chunk(line));

                // Professional Summary
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT PersonalSummary FROM PersonalDetails WHERE UserID = @UserID", conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        string summary = cmd.ExecuteScalar()?.ToString() ?? text9ProfessionalSummary.Text?.Trim() ?? "";
                        pdfDoc.Add(new Paragraph("Professional Summary", sectionFont) { SpacingBefore = 11.25f, SpacingAfter = 3.75f });
                        pdfDoc.Add(new Paragraph(summary, normalFont) { SpacingAfter = 11.25f });
                    }
                }
                pdfDoc.Add(new Chunk(line));

                // Create a table for Skills, Languages, and Hobbies (3 columns)
                PdfPTable skillsLanguagesHobbiesTable = new PdfPTable(3);
                skillsLanguagesHobbiesTable.WidthPercentage = 100;
                skillsLanguagesHobbiesTable.SetWidths(new float[] { 1, 1, 1 });
                skillsLanguagesHobbiesTable.SpacingBefore = 5f;
                skillsLanguagesHobbiesTable.SpacingAfter = 10f;

                // Skills Column
                PdfPCell skillsCell = new PdfPCell();
                skillsCell.Border = Rectangle.NO_BORDER;
                skillsCell.Padding = 5f;

                Paragraph skillsHeader = new Paragraph("SKILLS", sectionFont);
                skillsHeader.SpacingAfter = 5f;
                skillsCell.AddElement(skillsHeader);

                iTextSharp.text.List skillsList = new iTextSharp.text.List(false, false, 10f);
                skillsList.SetListSymbol("\u2022");

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
                                        skillsList.Add(new iTextSharp.text.ListItem(skill, normalFont));
                                    }
                                }
                            }
                            else
                            {
                                skillsList.Add(new iTextSharp.text.ListItem("No skills listed", normalFont));
                            }
                        }
                    }
                }
                skillsCell.AddElement(skillsList);
                skillsLanguagesHobbiesTable.AddCell(skillsCell);

                // Languages Column
                PdfPCell languagesCell = new PdfPCell();
                languagesCell.Border = Rectangle.NO_BORDER;
                languagesCell.Padding = 5f;

                Paragraph languagesHeader = new Paragraph("LANGUAGES", sectionFont);
                languagesHeader.SpacingAfter = 5f;
                languagesCell.AddElement(languagesHeader);

                iTextSharp.text.List languagesList = new iTextSharp.text.List(false, false, 10f);
                languagesList.SetListSymbol("\u2022");

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
                                        languagesList.Add(new iTextSharp.text.ListItem(language, normalFont));
                                    }
                                }
                            }
                            else
                            {
                                languagesList.Add(new iTextSharp.text.ListItem("No languages listed", normalFont));
                            }
                        }
                    }
                }
                languagesCell.AddElement(languagesList);
                skillsLanguagesHobbiesTable.AddCell(languagesCell);

                // Hobbies Column
                PdfPCell hobbiesCell = new PdfPCell();
                hobbiesCell.Border = Rectangle.NO_BORDER;
                hobbiesCell.Padding = 5f;

                Paragraph hobbiesHeader = new Paragraph("HOBBIES", sectionFont);
                hobbiesHeader.SpacingAfter = 5f;
                hobbiesCell.AddElement(hobbiesHeader);

                iTextSharp.text.List hobbiesList = new iTextSharp.text.List(false, false, 10f);
                hobbiesList.SetListSymbol("\u2022");

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
                                        hobbiesList.Add(new iTextSharp.text.ListItem(hobby, normalFont));
                                    }
                                }
                            }
                            else
                            {
                                hobbiesList.Add(new iTextSharp.text.ListItem("No hobbies listed", normalFont));
                            }
                        }
                    }
                }
                hobbiesCell.AddElement(hobbiesList);
                skillsLanguagesHobbiesTable.AddCell(hobbiesCell);

                pdfDoc.Add(skillsLanguagesHobbiesTable);
                pdfDoc.Add(new Chunk(line));

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
                pdfDoc.Add(new Paragraph("Employment History", sectionFont) { SpacingBefore = 11.25f, SpacingAfter = 3.75f });
                if (isFresher)
                {
                    pdfDoc.Add(new Paragraph("I am a fresher with no work experience.", normalFont) { SpacingAfter = 11.25f });
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
                                    pdfDoc.Add(new Paragraph("No employment history found.", normalFont) { SpacingAfter = 11.25f });
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
                                        Paragraph employment = new Paragraph($"I worked as {jobTitle} at {employer}, {city} between {startDate} and {endDate}. {description}", normalFont);
                                        employment.SpacingAfter = 3.75f;
                                        pdfDoc.Add(employment);
                                    }
                                }
                            }
                        }
                    }
                    pdfDoc.Add(new Paragraph("") { SpacingAfter = 11.25f });
                }
                pdfDoc.Add(new Chunk(line));

                // Education
                pdfDoc.Add(new Paragraph("Education", sectionFont) { SpacingBefore = 11.25f, SpacingAfter = 3.75f });
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
                                pdfDoc.Add(new Paragraph("No education found.", normalFont) { SpacingAfter = 11.25f });
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
                                    Paragraph education = new Paragraph($"I completed my {degree} from {schoolName}, {city} between {startDate} and {endDate}. {description}", normalFont);
                                    education.SpacingAfter = 3.75f;
                                    pdfDoc.Add(education);
                                }
                            }
                        }
                    }
                }
                pdfDoc.Add(new Paragraph("") { SpacingAfter = 11.25f });
                pdfDoc.Add(new Chunk(line));

                // Certifications
                pdfDoc.Add(new Paragraph("Certifications", sectionFont) { SpacingBefore = 11.25f, SpacingAfter = 3.75f });
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
                                pdfDoc.Add(new Paragraph("No certifications found.", normalFont) { SpacingAfter = 11.25f });
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    string courseName = reader["CourseName"]?.ToString() ?? "";
                                    string institution = reader["Institution"]?.ToString() ?? "";
                                    string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                    string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                    Paragraph certification = new Paragraph($"I completed {courseName} from {institution} between {startDate} and {endDate}.", normalFont);
                                    certification.SpacingAfter = 3.75f;
                                    pdfDoc.Add(certification);
                                }
                            }
                        }
                    }
                }
                pdfDoc.Add(new Paragraph("") { SpacingAfter = 11.25f });
                pdfDoc.Add(new Chunk(line));

                // Internships
                pdfDoc.Add(new Paragraph("Internships", sectionFont) { SpacingBefore = 11.25f, SpacingAfter = 3.75f });
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
                                Paragraph internship = new Paragraph($"I worked as {jobTitle} at {company} between {startDate} and {endDate}. {description}", normalFont);
                                internship.SpacingAfter = 3.75f;
                                pdfDoc.Add(internship);
                            }
                            else
                            {
                                pdfDoc.Add(new Paragraph("No internship data available.", normalFont) { SpacingAfter = 3.75f });
                            }
                        }
                    }
                }
                pdfDoc.Add(new Paragraph("") { SpacingAfter = 11.25f });
                pdfDoc.Add(new Chunk(line));

                // Additional Information
                pdfDoc.Add(new Paragraph("Additional Information", sectionFont) { SpacingBefore = 11.25f, SpacingAfter = 3.75f });
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
                                pdfDoc.Add(new Paragraph("No additional information.", normalFont) { SpacingAfter = 11.25f });
                            }
                            else
                            {
                                while (reader.Read())
                                {
                                    string title = reader["Title"]?.ToString() ?? "";
                                    string startDate = reader["StartDate"] != DBNull.Value ? Convert.ToDateTime(reader["StartDate"]).ToString("MMM yyyy") : "";
                                    string endDate = reader["EndDate"] != DBNull.Value ? Convert.ToDateTime(reader["EndDate"]).ToString("MMM yyyy") : "Present";
                                    string description = reader["Description"]?.ToString() ?? "";
                                    Paragraph custom = new Paragraph($"I worked on {title} between {startDate} and {endDate}. {description}", normalFont);
                                    custom.SpacingAfter = 3.75f;
                                    pdfDoc.Add(custom);
                                }
                            }
                        }
                    }
                }
                pdfDoc.Add(new Paragraph("") { SpacingAfter = 11.25f });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding content to PDF: {ex.Message}\nStack Trace: {ex.StackTrace}", ex);
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}