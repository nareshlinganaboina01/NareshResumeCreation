using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net.WebSockets;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using WebGrease.Activities;

namespace NareshResumeCreation
{
    public partial class Home : System.Web.UI.Page
    {
        // Sample List to store employment history (Replace with Database)
        protected static List<Employment> employmentList = new List<Employment>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            if (!IsPostBack)
            {
                // Load employment status and history on initial page load
                LoadEmploymentStatus();
                if (rblEmploymentStatus.SelectedValue == "Employed")
                {
                    employmentHistorySection.Visible = true;
                    LoadEmploymentHistory();
                }
                LoadPersonalDetails();
            }
            if (!IsPostBack)
            {
                LoadEducation();
            }
            /*if (!IsPostBack)
            {
                LoadWebsites();
            }
            if (!IsPostBack)
            {
                LoadSkills();
            }
            if (!IsPostBack)
            {
                LoadCourses();
                LoadInternships();
                LoadLanguages();
                LoadHobbies();
                LoadReferences();
                LoadCustomSelection();
            }*/
            /*if (!IsPostBack)
            {
                BindEmploymentHistory();
            }*/
            
            if (!IsPostBack)
                BindEducation();
            /*
            if (!IsPostBack)
                BindWebsites();
            if (!IsPostBack)
                BindSkills();
            if (!IsPostBack)
                BindCourses();
            */
            
        }


      

        protected void lnkDownloads_Click(object sender, EventArgs e)
        {
            Response.Redirect("Downloads.aspx");
        }

        

        

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            // Clear session and redirect to login page
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

        
        private void LoadPersonalDetails()
        {
            if (Session["UserId"] == null) return;

            int userId;
            if (!int.TryParse(Session["UserId"].ToString(), out userId)) return;

            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT JobTitle, FirstName, LastName, Email, Phone, Country, State, City, Address, PostalCode, Nationality, PlaceOfBirth, DateOfBirth, PersonalSummary FROM PersonalDetails WHERE UserId = @UserId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        txtJobTitle.Text = reader["JobTitle"].ToString();
                        txtFirstName.Text = reader["FirstName"].ToString();
                        txtLastName.Text = reader["LastName"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtPhone.Text = reader["Phone"].ToString();
                        txtCountry.Text = reader["Country"].ToString();
                        txtState.Text = reader["State"].ToString();
                        txtCity.Text = reader["City"].ToString();
                        txtAddress.Text = reader["Address"].ToString();
                        txtPostalCode.Text = reader["PostalCode"].ToString();
                        txtNationality.Text = reader["Nationality"].ToString();
                        txtPlaceOfBirth.Text = reader["PlaceOfBirth"].ToString();
                        txtDateOfBirth.Text = reader["DateOfBirth"] == DBNull.Value ? "" : Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                        txtSummary.Text = reader["PersonalSummary"].ToString();
                    }
                    reader.Close();
                }
            }
        }
        protected void btnSavePersonal_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserId"] == null)
                {
                    lblMessage.Text = "Error: User not logged in.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                int userId;
                if (!int.TryParse(Session["UserId"].ToString(), out userId))
                {
                    lblMessage.Text = "Error: Invalid UserId.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                string jobTitle = txtJobTitle.Text;
                string firstName = txtFirstName.Text;
                string lastName = txtLastName.Text;
                string email = txtEmail.Text;
                string phone = txtPhone.Text;
                string country = txtCountry.Text;
                string state = txtState.Text;
                string city = txtCity.Text;
                string address = txtAddress.Text;
                string postalCode = txtPostalCode.Text;
                string nationality = txtNationality.Text;
                string placeOfBirth = txtPlaceOfBirth.Text;
                string dateOfBirth = txtDateOfBirth.Text;
                string personalSummary = txtSummary.Text;

                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    // Check if user already has personal details
                    string checkQuery = "SELECT COUNT(*) FROM PersonalDetails WHERE UserId = @UserId";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@UserId", userId);
                        conn.Open();
                        int count = (int)checkCmd.ExecuteScalar();
                        conn.Close();

                        string query;
                        if (count > 0)
                        {
                            // Update existing record
                            query = @"UPDATE PersonalDetails SET 
                            JobTitle = @JobTitle, FirstName = @FirstName, LastName = @LastName, 
                            Email = @Email, Phone = @Phone, Country = @Country, State = @State, 
                            City = @City, Address = @Address, PostalCode = @PostalCode, 
                            Nationality = @Nationality, PlaceOfBirth = @PlaceOfBirth, 
                            DateOfBirth = @DateOfBirth, PersonalSummary = @PersonalSummary 
                            WHERE UserId = @UserId";
                        }
                        else
                        {
                            // Insert new record
                            query = @"INSERT INTO PersonalDetails 
                            (UserId, JobTitle, FirstName, LastName, Email, Phone, Country, State, 
                            City, Address, PostalCode, Nationality, PlaceOfBirth, DateOfBirth, PersonalSummary) 
                            VALUES 
                            (@UserId, @JobTitle, @FirstName, @LastName, @Email, @Phone, @Country, @State, 
                            @City, @Address, @PostalCode, @Nationality, @PlaceOfBirth, @DateOfBirth, @PersonalSummary)";
                        }

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserId", userId);
                            cmd.Parameters.AddWithValue("@JobTitle", jobTitle);
                            cmd.Parameters.AddWithValue("@FirstName", firstName);
                            cmd.Parameters.AddWithValue("@LastName", lastName);
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.Parameters.AddWithValue("@Phone", phone);
                            cmd.Parameters.AddWithValue("@Country", country);
                            cmd.Parameters.AddWithValue("@State", state);
                            cmd.Parameters.AddWithValue("@City", city);
                            cmd.Parameters.AddWithValue("@Address", address);
                            cmd.Parameters.AddWithValue("@PostalCode", postalCode);
                            cmd.Parameters.AddWithValue("@Nationality", nationality);
                            cmd.Parameters.AddWithValue("@PlaceOfBirth", placeOfBirth);
                            cmd.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                            cmd.Parameters.AddWithValue("@PersonalSummary", personalSummary);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                lblMessage.Text = "Personal details saved successfully!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void LoadEmploymentStatus()
        {
            if (Session["UserId"] == null) return;

            int userId;
            if (!int.TryParse(Session["UserId"].ToString(), out userId)) return;

            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // First check if column exists
                string checkColumnQuery = @"
            SELECT COUNT(*) 
            FROM INFORMATION_SCHEMA.COLUMNS 
            WHERE TABLE_NAME = 'Userss' 
            AND COLUMN_NAME = 'IsFresher'";

                bool columnExists = (int)new SqlCommand(checkColumnQuery, conn).ExecuteScalar() > 0;

                if (columnExists)
                {
                    string query = "SELECT IsFresher FROM Userss WHERE UserId=@UserId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        var result = cmd.ExecuteScalar();

                        // Handle all possible cases:
                        // 1. Column exists but is NULL -> default to true
                        // 2. Column exists with value -> use that value
                        // 3. Column doesn't exist -> default to true
                        bool isFresher = !columnExists ||
                                        result == null ||
                                        result == DBNull.Value ||
                                        Convert.ToBoolean(result);

                        rblEmploymentStatus.SelectedValue = isFresher ? "Fresher" : "Employed";
                        employmentHistorySection.Visible = !isFresher;
                    }
                }
                else
                {
                    // Column doesn't exist - default to Fresher
                    rblEmploymentStatus.SelectedValue = "Fresher";
                    employmentHistorySection.Visible = false;
                }
            }
        }
        protected void rblEmploymentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isFresher = rblEmploymentStatus.SelectedValue == "Fresher";
            employmentHistorySection.Visible = !isFresher;

            if (Session["UserId"] == null) return;

            int userId;
            if (!int.TryParse(Session["UserId"].ToString(), out userId)) return;

            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "UPDATE Userss SET IsFresher=@IsFresher WHERE UserId=@UserId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IsFresher", isFresher);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                }
            }

            if (!isFresher)
            {
                LoadEmploymentHistory();
            }
        }
        private void LoadEmploymentHistory()
        {
            if (Session["UserId"] == null) return;

            int userId;
            if (!int.TryParse(Session["UserId"].ToString(), out userId)) return;

            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = @"SELECT Id, JobTitle, Employer, StartDate, 
                        EndDate, City, Description 
                        FROM EmploymentHistoryy 
                        WHERE UserId=@UserId 
                        ORDER BY StartDate DESC";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    // Create a DataTable to hold the results
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    // Convert DataTable to List<Employment> with proper NULL handling
                    var employmentList = new List<Employment>();
                    foreach (DataRow row in dt.Rows)
                    {
                        employmentList.Add(new Employment
                        {
                            Id = Convert.ToInt32(row["Id"]),
                            JobTitle = row["JobTitle"].ToString(),
                            Employer = row["Employer"].ToString(),
                            StartDate = Convert.ToDateTime(row["StartDate"]),
                            EndDate = row["EndDate"] == DBNull.Value ?
                                      null : (DateTime?)Convert.ToDateTime(row["EndDate"]),
                            City = row["City"] == DBNull.Value ?
                                   string.Empty : row["City"].ToString(),
                            Description = row["Description"] == DBNull.Value ?
                                         string.Empty : row["Description"].ToString()
                        });
                    }

                    rptEmployment.DataSource = employmentList;
                    rptEmployment.DataBind();
                }
            }
        }

        protected void btnAddEmployment_Click(object sender, EventArgs e)
        {
            employmentForm.Visible = true;
            btnAddEmployment.Visible = false;

            // Set default start date to today
            txtStartDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        }

        protected void btnCancelEmployment_Click(object sender, EventArgs e)
        {
            ClearEmploymentForm();
            employmentForm.Visible = false;
            btnAddEmployment.Visible = true;
            lblError.Visible = false;
        }
        protected void btnSaveEmployment_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                lblError.Text = "Error: User is not logged in.";
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Visible = true;
                return;
            }

            int userId;
            if (!int.TryParse(Session["UserId"].ToString(), out userId))
            {
                lblError.Text = "Error: Invalid UserId.";
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Visible = true;
                return;
            }

            // Validate required fields
            if (string.IsNullOrEmpty(txtJobTitle.Text) || string.IsNullOrEmpty(txtEmployer.Text))
            {
                lblError.Text = "Job Title and Employer are required!";
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Visible = true;
                return;
            }

            DateTime startDate;
            if (!DateTime.TryParse(txtStartDate.Text, out startDate))
            {
                lblError.Text = "Error: Invalid Start Date.";
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Visible = true;
                return;
            }

            DateTime? endDate = null;
            if (!string.IsNullOrEmpty(txtEndDate.Text))
            {
                DateTime parsedEndDate;
                if (DateTime.TryParse(txtEndDate.Text, out parsedEndDate))
                {
                    endDate = parsedEndDate;
                }
                else
                {
                    lblError.Text = "Error: Invalid End Date.";
                    lblError.ForeColor = System.Drawing.Color.Red;
                    lblError.Visible = true;
                    return;
                }
            }

            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = @"INSERT INTO EmploymentHistoryy 
                            (UserId, JobTitle, Employer, StartDate, EndDate, City, Description) 
                            VALUES 
                            (@UserId, @JobTitle, @Employer, @StartDate, @EndDate, @City, @Description)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@JobTitle", TextJobTitle.Text);
                    cmd.Parameters.AddWithValue("@Employer", txtEmployer.Text);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@City", txtEmpCity.Text ?? "");
                    cmd.Parameters.AddWithValue("@Description", txtDescription.Text ?? "");

                    cmd.ExecuteNonQuery();
                }
            }

            // Show success message
            lblError.Text = "Employment details saved successfully!";
            lblError.ForeColor = System.Drawing.Color.Green;
            lblError.Visible = true;

            // Refresh employment history
            LoadEmploymentHistory();
            ClearEmploymentForm();
            employmentForm.Visible = false;
            btnAddEmployment.Visible = true;
        }

        protected void btnDeleteEmployment_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "DELETE FROM EmploymentHistoryy WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
            LoadEmploymentHistory();
        }
        /*private void BindEmploymentHistory()
        {
            rptEmployment.DataSource = employmentList;
            rptEmployment.DataBind();
        }*/

        private void ClearEmploymentForm()
        {
            TextJobTitle.Text = "";
            txtEmployer.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            txtEmpCity.Text = "";
            txtDescription.Text = "";
        }
        public class Employment
        {
            public int Id { get; set; }
            public string JobTitle { get; set; }
            public string Employer { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public string City { get; set; }
            public string Description { get; set; }
        }
        protected List<Education> EducationList
        {
            get { return (List<Education>)Session["EducationList"] ?? new List<Education>(); }
            set { Session["EducationList"] = value; }
        }
        private void LoadEducation()
        {
            if (Session["UserId"] == null) return;

            int userId = Convert.ToInt32(Session["UserId"]);
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT Id, SchoolName, Degree, StartDate, EndDate, City, Description FROM Education WHERE UserId = @UserId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    rptEducation.DataSource = reader;
                    rptEducation.DataBind();
                }
            }
        }
        


        protected void btnAddEducation_Click(object sender, EventArgs e)
        {
            educationForm.Visible = true;
        }
        protected void btnSaveEducation_Click(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(txtSchoolName.Text) || string.IsNullOrWhiteSpace(txtDegree.Text) || string.IsNullOrWhiteSpace(txtEduStartDate.Text))
            {
                lblEduError.Text = "Error: School Name, Degree, and Start Date are required!";
                lblEduError.ForeColor = System.Drawing.Color.Red;
                lblEduError.Visible = true;
                return;
            }

            // Check if user is logged in
            if (Session["UserId"] == null)
            {
                lblEduError.Text = "Error: User is not logged in.";
                lblEduError.ForeColor = System.Drawing.Color.Red;
                lblEduError.Visible = true;
                return;
            }

            // Validate UserId from Session
            int userId;
            if (!int.TryParse(Session["UserId"].ToString(), out userId))
            {
                lblEduError.Text = "Error: Invalid UserId.";
                lblEduError.ForeColor = System.Drawing.Color.Red;
                lblEduError.Visible = true;
                return;
            }

            // Parse Start Date safely
            DateTime startDate;
            if (!DateTime.TryParse(txtEduStartDate.Text, out startDate))
            {
                lblEduError.Text = "Error: Invalid Start Date.";
                lblEduError.ForeColor = System.Drawing.Color.Red;
                lblEduError.Visible = true;
                return;
            }

            // Parse End Date safely
            DateTime? endDate = null;
            if (!string.IsNullOrEmpty(txtEduEndDate.Text))
            {
                DateTime parsedEndDate;
                if (DateTime.TryParse(txtEduEndDate.Text, out parsedEndDate))
                {
                    endDate = parsedEndDate;
                }
                else
                {
                    lblEduError.Text = "Error: Invalid End Date.";
                    lblEduError.ForeColor = System.Drawing.Color.Red;
                    lblEduError.Visible = true;
                    return;
                }
            }

            // Add to local education list (for UI updates)
            EducationList.Add(new Education
            {
                Id = EducationList.Count + 1,
                SchoolName = txtSchoolName.Text,
                Degree = txtDegree.Text,
                StartDate = startDate,
                EndDate = endDate,
                Description = txtEduDescription.Text
            });

            BindEducation();

            // Database connection
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "INSERT INTO Education (UserId, SchoolName, Degree, StartDate, EndDate, City, Description) " +
                               "VALUES (@UserId, @SchoolName, @Degree, @StartDate, @EndDate, @City, @Description)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@SchoolName", txtSchoolName.Text);
                    cmd.Parameters.AddWithValue("@Degree", txtDegree.Text);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);

                    if (endDate.HasValue)
                        cmd.Parameters.AddWithValue("@EndDate", endDate);
                    else
                        cmd.Parameters.AddWithValue("@EndDate", DBNull.Value); // Fixed: Handle empty EndDate properly

                    cmd.Parameters.AddWithValue("@City", txtEduCity.Text);
                    cmd.Parameters.AddWithValue("@Description", txtEduDescription.Text);

                    cmd.ExecuteNonQuery();
                }
            }

            // Show success message
            lblEduError.Text = "Education details saved successfully!";
            lblEduError.ForeColor = System.Drawing.Color.Green;
            lblEduError.Visible = true;

            // Hide education form and refresh list
            educationForm.Visible = false;
            LoadEducation();
        }


        private void BindEducation()
        {
            rptEducation.DataSource = EducationList;
            rptEducation.DataBind();
        }


        protected void btnDeleteEducation_Command(object sender, CommandEventArgs e)
        {
            int educationId = Convert.ToInt32(e.CommandArgument);
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "DELETE FROM Education WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", educationId);
                    cmd.ExecuteNonQuery();
                }
            }

            LoadEducation(); // Refresh List
        }
        public class Education
        {
            public int Id { get; set; }
            public string SchoolName { get; set; }
            public string Degree { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public string Description { get; set; }
        }
        
        protected List<Website> WebsiteList
        {
            get { return (List<Website>)Session["WebsiteList"] ?? new List<Website>(); }
            set { Session["WebsiteList"] = value; }
        }
        private void LoadWebsites()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Websites WHERE UserId=@UserId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
                SqlDataReader reader = cmd.ExecuteReader();
                rptWebsites.DataSource = reader;
                rptWebsites.DataBind();
            }
        }
        protected void btnAddWebsite_Click(object sender, EventArgs e)
        {
            websiteForm.Visible = true;
        }
        protected void btnSaveWebsite_Click(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(txtLabel.Text) || string.IsNullOrWhiteSpace(txtLink.Text))
            {
                lblWebsiteError.Text = "Error: Both Label and Link are required!";
                lblWebsiteError.ForeColor = System.Drawing.Color.Red;
                lblWebsiteError.Visible = true;
                return;
            }

            // Validate User Session
            if (Session["UserId"] == null)
            {
                lblWebsiteError.Text = "Error: User is not logged in.";
                lblWebsiteError.ForeColor = System.Drawing.Color.Red;
                lblWebsiteError.Visible = true;
                return;
            }

            // Validate UserId conversion
            int userId;
            if (!int.TryParse(Session["UserId"].ToString(), out userId))
            {
                lblWebsiteError.Text = "Error: Invalid UserId.";
                lblWebsiteError.ForeColor = System.Drawing.Color.Red;
                lblWebsiteError.Visible = true;
                return;
            }

            // Add to local list for UI update
            WebsiteList.Add(new Website
            {
                Id = WebsiteList.Count + 1,
                Label = txtLabel.Text,
                Link = txtLink.Text
            });

            BindWebsites();

            // Save to Database
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO Websites (UserId, Label, Link) VALUES (@UserId, @Label, @Link)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Label", txtLabel.Text);
                    cmd.Parameters.AddWithValue("@Link", txtLink.Text);

                    cmd.ExecuteNonQuery();
                }
            }

            // Show success message
            lblWebsiteError.Text = "Website link saved successfully!";
            lblWebsiteError.ForeColor = System.Drawing.Color.Green;
            lblWebsiteError.Visible = true;

            // Hide form and refresh list
            websiteForm.Visible = false;
            LoadWebsites();
        }

        protected void btnDeleteWebsite_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "DELETE FROM Websites WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
            LoadWebsites();
        }
        private void BindWebsites()
        {
            rptWebsites.DataSource = WebsiteList;
            rptWebsites.DataBind();
        }

        public class Website
        {
            public int Id { get; set; }
            public string Label { get; set; }
            public string Link { get; set; }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Clear session without validation
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
        protected List<Skill> SkillList
        {
            get { return (List<Skill>)Session["SkillList"] ?? new List<Skill>(); }
            set { Session["SkillList"] = value; }
        }
        private void LoadSkills()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Skills WHERE UserId=@UserId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
                SqlDataReader reader = cmd.ExecuteReader();
                rptSkills.DataSource = reader;
                rptSkills.DataBind();
            }
        }
        protected void btnAddSkill_Click(object sender, EventArgs e)
        {
            skillForm.Visible = true;
        }
        protected void btnSaveSkill_Click(object sender, EventArgs e)
        {
            // Validate required field
            if (string.IsNullOrWhiteSpace(txtSkill.Text))
            {
                lblSkillError.Text = "Error: Skill name is required!";
                lblSkillError.ForeColor = System.Drawing.Color.Red;
                lblSkillError.Visible = true;
                return;
            }

            // Validate User Session
            if (Session["UserId"] == null)
            {
                lblSkillError.Text = "Error: User is not logged in.";
                lblSkillError.ForeColor = System.Drawing.Color.Red;
                lblSkillError.Visible = true;
                return;
            }

            // Validate UserId conversion
            int userId;
            if (!int.TryParse(Session["UserId"].ToString(), out userId))
            {
                lblSkillError.Text = "Error: Invalid UserId.";
                lblSkillError.ForeColor = System.Drawing.Color.Red;
                lblSkillError.Visible = true;
                return;
            }

            // Add to local list for UI update
            SkillList.Add(new Skill
            {
                Id = SkillList.Count + 1,
                SkillName = txtSkill.Text
            });

            BindSkills();

            // Save to Database
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO Skills (UserId, SkillName) VALUES (@UserId, @SkillName)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@SkillName", txtSkill.Text);

                    cmd.ExecuteNonQuery();
                }
            }

            // Show success message
            lblSkillError.Text = "Skill saved successfully!";
            lblSkillError.ForeColor = System.Drawing.Color.Green;
            lblSkillError.Visible = true;

            // Hide form and refresh list
            skillForm.Visible = false;
            LoadSkills();
        }

        private void BindSkills()
        {
            rptSkills.DataSource = SkillList;
            rptSkills.DataBind();
        }
        protected void btnDeleteSkill_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "DELETE FROM Skills WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
            LoadSkills();
        }
        public class Skill
        {
            public int Id { get; set; }
            public string SkillName { get; set; }
        }



        protected List<Course> CourseList
        {
            get { return (List<Course>)Session["CourseList"] ?? new List<Course>(); }
            set { Session["CourseList"] = value; }
        }

        private void LoadCourses()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Courses WHERE UserId=@UserId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
                SqlDataReader reader = cmd.ExecuteReader();
                rptCourses.DataSource = reader;
                rptCourses.DataBind();
            }
        }

        protected void btnAddCourse_Click(object sender, EventArgs e)
        {
            courseForm.Visible = true;
        }

        protected void btnSaveCourse_Click(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(txtCourseName.Text) ||
                string.IsNullOrWhiteSpace(txtInstitution.Text) ||
                string.IsNullOrWhiteSpace(txtCourseStartDate.Text))
            {
                lblCourseError.Text = "Error: Course Name, Institution, and Start Date are required!";
                lblCourseError.ForeColor = System.Drawing.Color.Red;
                lblCourseError.Visible = true;
                return;
            }

            // Validate User Session
            if (Session["UserId"] == null)
            {
                lblCourseError.Text = "Error: User is not logged in.";
                lblCourseError.ForeColor = System.Drawing.Color.Red;
                lblCourseError.Visible = true;
                return;
            }

            // Validate and Convert UserId
            int userId;
            if (!int.TryParse(Session["UserId"].ToString(), out userId))
            {
                lblCourseError.Text = "Error: Invalid UserId.";
                lblCourseError.ForeColor = System.Drawing.Color.Red;
                lblCourseError.Visible = true;
                return;
            }

            // Add to local list for UI update
            CourseList.Add(new Course
            {
                Id = CourseList.Count + 1,
                CourseName = txtCourseName.Text,
                Institution = txtInstitution.Text,
                StartDate = DateTime.Parse(txtCourseStartDate.Text),
                EndDate = string.IsNullOrWhiteSpace(txtCourseEndDate.Text) ? (DateTime?)null : DateTime.Parse(txtCourseEndDate.Text)
            });

            BindCourses();

            // Save to Database
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO Courses (UserId, CourseName, Institution, StartDate, EndDate) " +
                               "VALUES (@UserId, @CourseName, @Institution, @StartDate, @EndDate)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text);
                    cmd.Parameters.AddWithValue("@Institution", txtInstitution.Text);
                    cmd.Parameters.AddWithValue("@StartDate", DateTime.Parse(txtCourseStartDate.Text));

                    // Handle EndDate correctly
                    if (string.IsNullOrWhiteSpace(txtCourseEndDate.Text))
                        cmd.Parameters.AddWithValue("@EndDate", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@EndDate", DateTime.Parse(txtCourseEndDate.Text));

                    cmd.ExecuteNonQuery();
                }
            }

            // Show success message
            lblCourseError.Text = "Course saved successfully!";
            lblCourseError.ForeColor = System.Drawing.Color.Green;
            lblCourseError.Visible = true;

            // Hide form and refresh courses list
            courseForm.Visible = false;
            LoadCourses();
        }


        private void BindCourses()
        {
            rptCourses.DataSource = CourseList;
            rptCourses.DataBind();
        }

        protected void btnDeleteCourse_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "DELETE FROM Courses WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
            LoadCourses();
        }

        public class Course
        {
            public int Id { get; set; }
            public string CourseName { get; set; }
            public string Institution { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }

        protected List<Internship> InternshipList
{
    get { return (List<Internship>)Session["InternshipList"] ?? new List<Internship>(); }
    set { Session["InternshipList"] = value; }
}

private void LoadInternships()
{
    if (Session["UserId"] == null) return;

    int userId = Convert.ToInt32(Session["UserId"]);
    string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;

    using (SqlConnection conn = new SqlConnection(connStr))
    {
        conn.Open();
        string query = "SELECT Id, CompanyName, JobTitle, StartDate, EndDate, City, Description FROM Internships WHERE UserId = @UserId";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@UserId", userId);
            SqlDataReader reader = cmd.ExecuteReader();
            rptInternships.DataSource = reader;
            rptInternships.DataBind();
        }
    }
}

protected void btnAddInternship_Click(object sender, EventArgs e)
{
    internshipForm.Visible = true;
}

        protected void btnSaveInternship_Click(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(txtCompanyName.Text) ||
                string.IsNullOrWhiteSpace(InternshipJobTitle.Text) ||
                string.IsNullOrWhiteSpace(txtIntStartDate.Text))
            {
                lblIntError.Text = "Error: Company Name, Job Title, and Start Date are required!";
                lblIntError.ForeColor = System.Drawing.Color.Red;
                lblIntError.Visible = true;
                return;
            }

            // Validate User Session
            if (Session["UserId"] == null)
            {
                lblIntError.Text = "Error: User is not logged in.";
                lblIntError.ForeColor = System.Drawing.Color.Red;
                lblIntError.Visible = true;
                return;
            }

            // Validate and Convert UserId
            int userId;
            if (!int.TryParse(Session["UserId"].ToString(), out userId))
            {
                lblIntError.Text = "Error: Invalid UserId.";
                lblIntError.ForeColor = System.Drawing.Color.Red;
                lblIntError.Visible = true;
                return;
            }

            // Add to local list for UI update
            InternshipList.Add(new Internship
            {
                Id = InternshipList.Count + 1,
                CompanyName = txtCompanyName.Text,
                JobTitle = InternshipJobTitle.Text,
                StartDate = DateTime.Parse(txtIntStartDate.Text),
                EndDate = string.IsNullOrWhiteSpace(txtIntEndDate.Text) ? (DateTime?)null : DateTime.Parse(txtIntEndDate.Text),
                Description = txtIntDescription.Text
            });

            BindInternships();

            // Save to Database
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO Internships (UserId, CompanyName, JobTitle, StartDate, EndDate, City, Description) " +
                               "VALUES (@UserId, @CompanyName, @JobTitle, @StartDate, @EndDate, @City, @Description)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@CompanyName", txtCompanyName.Text);
                    cmd.Parameters.AddWithValue("@JobTitle", InternshipJobTitle.Text);
                    cmd.Parameters.AddWithValue("@StartDate", DateTime.Parse(txtIntStartDate.Text));

                    // Handle EndDate correctly
                    if (string.IsNullOrWhiteSpace(txtIntEndDate.Text))
                        cmd.Parameters.AddWithValue("@EndDate", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@EndDate", DateTime.Parse(txtIntEndDate.Text));

                    cmd.Parameters.AddWithValue("@City", txtIntCity.Text);
                    cmd.Parameters.AddWithValue("@Description", txtIntDescription.Text);

                    cmd.ExecuteNonQuery();
                }
            }

            // Show success message
            lblIntError.Text = "Internship saved successfully!";
            lblIntError.ForeColor = System.Drawing.Color.Green;
            lblIntError.Visible = true;

            // Hide form and refresh internship list
            internshipForm.Visible = false;
            LoadInternships();
        }


        private void BindInternships()
{
    rptInternships.DataSource = InternshipList;
    rptInternships.DataBind();
}

protected void btnDeleteInternship_Command(object sender, CommandEventArgs e)
{
    int internshipId = Convert.ToInt32(e.CommandArgument);
    string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;

    using (SqlConnection conn = new SqlConnection(connStr))
    {
        conn.Open();
        string query = "DELETE FROM Internships WHERE Id = @Id";

        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@Id", internshipId);
            cmd.ExecuteNonQuery();
        }
    }

    LoadInternships(); // Refresh List
}

public class Internship
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string JobTitle { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Description { get; set; }
}



        protected List<Language> LanguageList
        {
            get { return (List<Language>)Session["LanguageList"] ?? new List<Language>(); }
            set { Session["LanguageList"] = value; }
        }

        private void LoadLanguages()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Languages WHERE UserId=@UserId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
                SqlDataReader reader = cmd.ExecuteReader();
                rptLanguages.DataSource = reader;
                rptLanguages.DataBind();
            }
        }

        protected void btnAddLanguage_Click(object sender, EventArgs e)
        {
            languageForm.Visible = true;
        }

        protected void btnSaveLanguage_Click(object sender, EventArgs e)
        {
            // Validate required field
            if (string.IsNullOrWhiteSpace(txtLanguageName.Text))
            {
                lblLangError.Text = "Error: Language name is required!";
                lblLangError.ForeColor = System.Drawing.Color.Red;
                lblLangError.Visible = true;
                return;
            }

            // Validate User Session
            if (Session["UserId"] == null)
            {
                lblLangError.Text = "Error: User is not logged in.";
                lblLangError.ForeColor = System.Drawing.Color.Red;
                lblLangError.Visible = true;
                return;
            }

            // Validate and Convert UserId
            int userId;
            if (!int.TryParse(Session["UserId"].ToString(), out userId))
            {
                lblLangError.Text = "Error: Invalid UserId.";
                lblLangError.ForeColor = System.Drawing.Color.Red;
                lblLangError.Visible = true;
                return;
            }

            // Add to local list for UI update
            LanguageList.Add(new Language
            {
                Id = LanguageList.Count + 1,
                LanguageName = txtLanguageName.Text
            });

            BindLanguages();

            // Save to Database
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO Languages (UserId, LanguageName) VALUES (@UserId, @LanguageName)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@LanguageName", txtLanguageName.Text);
                    cmd.ExecuteNonQuery();
                }
            }

            // Show success message
            lblLangError.Text = "Language saved successfully!";
            lblLangError.ForeColor = System.Drawing.Color.Green;
            lblLangError.Visible = true;

            // Hide form and refresh language list
            languageForm.Visible = false;
            LoadLanguages();
        }


        private void BindLanguages()
        {
            rptLanguages.DataSource = LanguageList;
            rptLanguages.DataBind();
        }

        protected void btnDeleteLanguage_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "DELETE FROM Languages WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
            LoadLanguages();
        }

        public class Language
        {
            public int Id { get; set; }
            public string LanguageName { get; set; }
        }

        protected List<Hobby> HobbyList
        {
            get { return (List<Hobby>)Session["HobbyList"] ?? new List<Hobby>(); }
            set { Session["HobbyList"] = value; }
        }

        private void LoadHobbies()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Hobbies WHERE UserId=@UserId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
                SqlDataReader reader = cmd.ExecuteReader();
                rptHobbies.DataSource = reader;
                rptHobbies.DataBind();
            }
        }

        protected void btnAddHobby_Click(object sender, EventArgs e)
        {
            hobbyForm.Visible = true;
        }

        protected void btnSaveHobby_Click(object sender, EventArgs e)
        {
            // Validate required field
            if (string.IsNullOrWhiteSpace(txtHobby.Text))
            {
                lblHobbyError.Text = "Error: Hobby name is required!";
                lblHobbyError.ForeColor = System.Drawing.Color.Red;
                lblHobbyError.Visible = true;
                return;
            }

            // Validate User Session
            if (Session["UserId"] == null)
            {
                lblHobbyError.Text = "Error: User is not logged in.";
                lblHobbyError.ForeColor = System.Drawing.Color.Red;
                lblHobbyError.Visible = true;
                return;
            }

            // Validate and Convert UserId
            int userId;
            if (!int.TryParse(Session["UserId"].ToString(), out userId))
            {
                lblHobbyError.Text = "Error: Invalid UserId.";
                lblHobbyError.ForeColor = System.Drawing.Color.Red;
                lblHobbyError.Visible = true;
                return;
            }

            // Add to local list for UI update
            HobbyList.Add(new Hobby
            {
                Id = HobbyList.Count + 1,
                Name = txtHobby.Text
            });

            BindHobbies();

            // Save to Database
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO Hobbies (UserId, Name) VALUES (@UserId, @Name)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Name", txtHobby.Text);
                    cmd.ExecuteNonQuery();
                }
            }

            // Show success message
            lblHobbyError.Text = "Hobby saved successfully!";
            lblHobbyError.ForeColor = System.Drawing.Color.Green;
            lblHobbyError.Visible = true;

            // Hide form and refresh hobby list
            hobbyForm.Visible = false;
            LoadHobbies();
        }


        private void BindHobbies()
        {
            rptHobbies.DataSource = HobbyList;
            rptHobbies.DataBind();
        }

        protected void btnDeleteHobby_Command(object sender, CommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "DELETE FROM Hobbies WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
            LoadHobbies();
        }

        public class Hobby
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        /*
        protected List<Reference> ReferenceList
        {
            get { return (List<Reference>)Session["ReferenceList"] ?? new List<Reference>(); }
            set { Session["ReferenceList"] = value; }
        }

        private void LoadReferences()
        {
            if (Session["UserId"] == null) return;

            int userId = Convert.ToInt32(Session["UserId"]);
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT Id, Name, Company, Phone, Email, Description FROM [Referencess] WHERE UserId = @UserId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    rptReferences.DataSource = reader;
                    rptReferences.DataBind();
                }

            }
        }
        protected void btnAddReference_Click(object sender, EventArgs e)
        {
            referenceForm.Visible = true;
        }
        protected void btnSaveReference_Click(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(txtRefName.Text) ||
                string.IsNullOrWhiteSpace(txtRefCompany.Text) ||
                string.IsNullOrWhiteSpace(txtRefPhone.Text) ||
                string.IsNullOrWhiteSpace(txtRefEmail.Text))
            {
                lblRefError.Text = "Error: All fields are required!";
                lblRefError.ForeColor = System.Drawing.Color.Red;
                lblRefError.Visible = true;
                return;
            }

            // Validate User Session
            if (Session["UserId"] == null)
            {
                lblRefError.Text = "Error: User is not logged in.";
                lblRefError.ForeColor = System.Drawing.Color.Red;
                lblRefError.Visible = true;
                return;
            }

            // Validate and Convert UserId
            int userId;
            if (!int.TryParse(Session["UserId"].ToString(), out userId))
            {
                lblRefError.Text = "Error: Invalid UserId.";
                lblRefError.ForeColor = System.Drawing.Color.Red;
                lblRefError.Visible = true;
                return;
            }

            // Add to the local reference list
            ReferenceList.Add(new Reference
            {
                Id = ReferenceList.Count + 1,
                Name = txtRefName.Text,
                Company = txtRefCompany.Text,
                Phone = txtRefPhone.Text,
                Email = txtRefEmail.Text,
                Description = txtRefDescription.Text
            });

            // Save to Database
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "INSERT INTO Referencess (UserId, Name, Company, Phone, Email, Description) " +
                               "VALUES (@UserId, @Name, @Company, @Phone, @Email, @Description)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Name", txtRefName.Text);
                    cmd.Parameters.AddWithValue("@Company", txtRefCompany.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtRefPhone.Text);
                    cmd.Parameters.AddWithValue("@Email", txtRefEmail.Text);
                    cmd.Parameters.AddWithValue("@Description", txtRefDescription.Text);

                    cmd.ExecuteNonQuery();
                }
            }

            // Show success message
            lblRefError.Text = "Reference saved successfully!";
            lblRefError.ForeColor = System.Drawing.Color.Green;
            lblRefError.Visible = true;

            // Hide form and refresh reference list
            referenceForm.Visible = false;
            LoadReferences();
        }

        protected void btnDeleteReference_Command(object sender, CommandEventArgs e)
        {
            int referenceId = Convert.ToInt32(e.CommandArgument);
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "DELETE FROM Referencess WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", referenceId);
                    cmd.ExecuteNonQuery();
                }
            }

            LoadReferences(); // Refresh List
        }

        public class Reference
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Company { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Description { get; set; }
        }
        */
        public class CustomSelection
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public string Description { get; set; }
        }

        // Retrieve the list from Session or initialize it
        private List<CustomSelection> CustomSelections
        {
            get
            {
                if (Session["CustomSelections"] == null)
                    Session["CustomSelections"] = new List<CustomSelection>();

                return (List<CustomSelection>)Session["CustomSelections"];
            }
            set { Session["CustomSelections"] = value; }
        }


        private void LoadCustomSelection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM CustomSelection WHERE UserId=@UserId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserId", Session["UserId"]);
                rptCustomSelection.DataSource = cmd.ExecuteReader();
                rptCustomSelection.DataBind();
            }
        }
        // Save Custom Selection
        protected void btnSaveCustom_Click(object sender, EventArgs e)
        {
            lblCustomError.Visible = false; // Hide error by default

            // Validate required title field
            if (string.IsNullOrWhiteSpace(txtCustomTitle.Text))
            {
                lblCustomError.Text = "Title is required!";
                lblCustomError.ForeColor = System.Drawing.Color.Red;
                lblCustomError.Visible = true;
                return;
            }

            // Validate User Session
            if (Session["UserId"] == null)
            {
                lblCustomError.Text = "Error: User is not logged in.";
                lblCustomError.ForeColor = System.Drawing.Color.Red;
                lblCustomError.Visible = true;
                return;
            }

            // Validate and Convert UserId
            int userId;
            if (!int.TryParse(Session["UserId"].ToString(), out userId))
            {
                lblCustomError.Text = "Error: Invalid UserId.";
                lblCustomError.ForeColor = System.Drawing.Color.Red;
                lblCustomError.Visible = true;
                return;
            }

            // Parse dates (if provided)
            DateTime? startDate = null;
            DateTime? endDate = null;

            if (!string.IsNullOrWhiteSpace(txtCustomStartDate.Text))
            {
                if (!DateTime.TryParse(txtCustomStartDate.Text, out DateTime parsedStartDate))
                {
                    lblCustomError.Text = "Invalid Start Date format. Use YYYY-MM-DD.";
                    lblCustomError.ForeColor = System.Drawing.Color.Red;
                    lblCustomError.Visible = true;
                    return;
                }
                startDate = parsedStartDate;
            }

            if (!string.IsNullOrWhiteSpace(txtCustomEndDate.Text))
            {
                if (!DateTime.TryParse(txtCustomEndDate.Text, out DateTime parsedEndDate))
                {
                    lblCustomError.Text = "Invalid End Date format. Use YYYY-MM-DD.";
                    lblCustomError.ForeColor = System.Drawing.Color.Red;
                    lblCustomError.Visible = true;
                    return;
                }
                endDate = parsedEndDate;
            }

            // Add to session-based list
            CustomSelections.Add(new CustomSelection
            {
                Id = CustomSelections.Count + 1,
                Title = txtCustomTitle.Text,
                StartDate = startDate,
                EndDate = endDate,
                Description = txtCustomDescription.Text
            });

            // Insert into Database with Exception Handling
            try
            {
                string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "INSERT INTO CustomSelection (UserId, Title, StartDate, EndDate, Description) " +
                                   "VALUES (@UserId, @Title, @StartDate, @EndDate, @Description)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@Title", txtCustomTitle.Text);
                        cmd.Parameters.AddWithValue("@StartDate", startDate ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@EndDate", endDate ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(txtCustomDescription.Text) ? (object)DBNull.Value : txtCustomDescription.Text);
                        cmd.ExecuteNonQuery();
                    }
                }

                // Show success message
                lblCustomError.Text = "Custom selection saved successfully!";
                lblCustomError.ForeColor = System.Drawing.Color.Green;
                lblCustomError.Visible = true;

                // Hide the form and refresh the list
                customForm.Visible = false;
                LoadCustomSelection();
            }
            catch (Exception ex)
            {
                lblCustomError.Text = "Database error: " + ex.Message;
                lblCustomError.ForeColor = System.Drawing.Color.Red;
                lblCustomError.Visible = true;
            }
        }

        // Bind the Custom Selection Repeater
        private void BindCustomSelection()
        {
            rptCustomSelection.DataSource = CustomSelections;
            rptCustomSelection.DataBind();
        }
        // Show the Custom Selection form when clicking "Add Custom Selection"
        protected void btnAddCustom_Click(object sender, EventArgs e)
        {
            customForm.Visible = true;
        }


        protected void btnDeleteCustom_Command(object sender, CommandEventArgs e)
        {
            int customId = Convert.ToInt32(e.CommandArgument);
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "DELETE FROM CustomSelection WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", customId);
                cmd.ExecuteNonQuery();
            }
            LoadCustomSelection(); // Refresh the list
        }
        protected void btnGenerateResume_Click(object sender, EventArgs e)
        {
            string selectedPage = ddlResumeOptions.SelectedValue;
            if (!string.IsNullOrEmpty(selectedPage))
            {
                Response.Redirect(selectedPage);
            }
            else
            {
                // Optionally, show an error message if no option is selected
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please select a resume type.');", true);
            }
        }

        protected void btnLogoutt_Click(object sender, EventArgs e)
        {
            // Clear session without validation
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
