using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace NareshResumeCreation
{
    public partial class EditResume : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            if (!IsPostBack)
            {
                LoadPersonalDetails();
                LoadEducation();
                LoadEmploymentHistory();
                LoadSkills();
                LoadLanguages();
                LoadCertifications();
                LoadInternships();
                LoadHobbies();
                LoadCustomSelection();
            }
        }

        private void LoadPersonalDetails()
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
                    txtFirstName.Text = reader["FirstName"].ToString();
                    txtLastName.Text = reader["LastName"].ToString();
                    txtJobTitle.Text = reader["JobTitle"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    txtPhone.Text = reader["Phone"].ToString();
                    txtDateOfBirth.Text = Convert.ToDateTime(reader["DateOfBirth"]).ToString("yyyy-MM-dd");
                    txtAddress.Text = reader["Address"].ToString();
                    txtCity.Text = reader["City"].ToString();
                    txtState.Text = reader["State"].ToString();
                    txtPostalCode.Text = reader["PostalCode"].ToString();
                    txtCountry.Text = reader["Country"].ToString();
                    txtNationality.Text = reader["Nationality"].ToString();
                    txtPlaceOfBirth.Text = reader["PlaceOfBirth"].ToString();
                    txtProfessionalSummary.Text = reader["PersonalSummary"].ToString();
                }
                reader.Close();

                string checkStatusQuery = "SELECT IsFresher FROM Userss WHERE UserID = @UserID";
                cmd = new SqlCommand(checkStatusQuery, conn);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                chkIsFresher.Checked = Convert.ToBoolean(cmd.ExecuteScalar());
            }
        }

        private void LoadEducation()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Education WHERE UserID = @UserID ORDER BY StartDate DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                conn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                rptEducation.DataSource = dt;
                rptEducation.DataBind();
            }
        }

        private void LoadEmploymentHistory()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM EmploymentHistoryy WHERE UserID = @UserID ORDER BY StartDate DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                conn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                rptEmploymentHistory.DataSource = dt;
                rptEmploymentHistory.DataBind();
            }
        }

        private void LoadSkills()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Skills WHERE UserID = @UserID ORDER BY ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                conn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                rptSkills.DataSource = dt;
                rptSkills.DataBind();
            }
        }

        private void LoadLanguages()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Languages WHERE UserID = @UserID ORDER BY ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                conn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                rptLanguages.DataSource = dt;
                rptLanguages.DataBind();
            }
        }

        private void LoadCertifications()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Courses WHERE UserID = @UserID ORDER BY StartDate DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                conn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                rptCertifications.DataSource = dt;
                rptCertifications.DataBind();
            }
        }

        private void LoadInternships()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT TOP 1 * FROM Internships WHERE UserID = @UserID ORDER BY ID DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtInternJobTitle.Text = reader["JobTitle"].ToString();
                    txtInternCompany.Text = reader["CompanyName"].ToString();
                    txtInternStartDate.Text = Convert.ToDateTime(reader["StartDate"]).ToString("yyyy-MM-dd");
                    txtInternEndDate.Text = Convert.ToDateTime(reader["EndDate"]).ToString("yyyy-MM-dd");
                    txtInternDescription.Text = reader["Description"].ToString();
                }
                reader.Close();
            }
        }

        private void LoadHobbies()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM Hobbies WHERE UserID = @UserID ORDER BY ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                conn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                rptHobbies.DataSource = dt;
                rptHobbies.DataBind();
            }
        }

        private void LoadCustomSelection()
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM CustomSelection WHERE UserID = @UserID ORDER BY StartDate DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                conn.Open();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                rptCustomSelection.DataSource = dt;
                rptCustomSelection.DataBind();
            }
        }

        protected void btnAddEducation_Click(object sender, EventArgs e)
        {
            DataTable dt = GetEducationDataTable();
            dt.Rows.Add(0, "", "", "", DateTime.Now, DateTime.Now, "", Session["UserID"]);
            rptEducation.DataSource = dt;
            rptEducation.DataBind();
        }

        protected void btnAddEmployment_Click(object sender, EventArgs e)
        {
            DataTable dt = GetEmploymentDataTable();
            dt.Rows.Add(0, "", "", "", DateTime.Now, DateTime.Now, "", Session["UserID"]);
            rptEmploymentHistory.DataSource = dt;
            rptEmploymentHistory.DataBind();
        }

        protected void btnAddSkill_Click(object sender, EventArgs e)
        {
            DataTable dt = GetSkillsDataTable();
            dt.Rows.Add(0, "", Session["UserID"]);
            rptSkills.DataSource = dt;
            rptSkills.DataBind();
        }

        protected void btnAddLanguage_Click(object sender, EventArgs e)
        {
            DataTable dt = GetLanguagesDataTable();
            dt.Rows.Add(0, "", Session["UserID"]);
            rptLanguages.DataSource = dt;
            rptLanguages.DataBind();
        }

        protected void btnAddCertification_Click(object sender, EventArgs e)
        {
            DataTable dt = GetCertificationsDataTable();
            dt.Rows.Add(0, "", "", DateTime.Now, DateTime.Now, Session["UserID"]);
            rptCertifications.DataSource = dt;
            rptCertifications.DataBind();
        }

        protected void btnAddHobby_Click(object sender, EventArgs e)
        {
            DataTable dt = GetHobbiesDataTable();
            dt.Rows.Add(0, "", Session["UserID"]);
            rptHobbies.DataSource = dt;
            rptHobbies.DataBind();
        }

        protected void btnAddCustomSelection_Click(object sender, EventArgs e)
        {
            DataTable dt = GetCustomSelectionDataTable();
            dt.Rows.Add(0, "", DateTime.Now, DateTime.Now, "", Session["UserID"]);
            rptCustomSelection.DataSource = dt;
            rptCustomSelection.DataBind();
        }

        protected void rptEducation_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                if (id > 0)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        string query = "DELETE FROM Education WHERE ID = @ID AND UserID = @UserID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEducation();
            }
        }

        protected void rptEmploymentHistory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                if (id > 0)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        string query = "DELETE FROM EmploymentHistoryy WHERE ID = @ID AND UserID = @UserID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadEmploymentHistory();
            }
        }

        protected void rptSkills_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                if (id > 0)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        string query = "DELETE FROM Skills WHERE ID = @ID AND UserID = @UserID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadSkills();
            }
        }

        protected void rptLanguages_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                if (id > 0)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        string query = "DELETE FROM Languages WHERE ID = @ID AND UserID = @UserID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadLanguages();
            }
        }

        protected void rptCertifications_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                if (id > 0)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        string query = "DELETE FROM Courses WHERE ID = @ID AND UserID = @UserID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadCertifications();
            }
        }

        protected void rptHobbies_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                if (id > 0)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        string query = "DELETE FROM Hobbies WHERE ID = @ID AND UserID = @UserID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadHobbies();
            }
        }

        protected void rptCustomSelection_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                if (id > 0)
                {
                    string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        string query = "DELETE FROM CustomSelection WHERE ID = @ID AND UserID = @UserID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadCustomSelection();
            }
        }

        private DataTable GetEducationDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Degree", typeof(string));
            dt.Columns.Add("SchoolName", typeof(string));
            dt.Columns.Add("City", typeof(string));
            dt.Columns.Add("StartDate", typeof(DateTime));
            dt.Columns.Add("EndDate", typeof(DateTime));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("UserID", typeof(string));
            foreach (RepeaterItem item in rptEducation.Items)
            {
                TextBox txtDegree = item.FindControl("txtDegree") as TextBox;
                TextBox txtSchoolName = item.FindControl("txtSchoolName") as TextBox;
                TextBox txtCity = item.FindControl("txtCity") as TextBox;
                TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;
                TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
                dt.Rows.Add(0, txtDegree.Text, txtSchoolName.Text, txtCity.Text,
                    DateTime.TryParse(txtStartDate.Text, out DateTime startDate) ? startDate : DateTime.Now,
                    DateTime.TryParse(txtEndDate.Text, out DateTime endDate) ? endDate : DateTime.Now,
                    txtDescription.Text, Session["UserID"]);
            }
            return dt;
        }

        private DataTable GetEmploymentDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("JobTitle", typeof(string));
            dt.Columns.Add("Employer", typeof(string));
            dt.Columns.Add("City", typeof(string));
            dt.Columns.Add("StartDate", typeof(DateTime));
            dt.Columns.Add("EndDate", typeof(DateTime));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("UserID", typeof(string));
            foreach (RepeaterItem item in rptEmploymentHistory.Items)
            {
                TextBox txtJobTitle = item.FindControl("txtJobTitle") as TextBox;
                TextBox txtEmployer = item.FindControl("txtEmployer") as TextBox;
                TextBox txtCity = item.FindControl("txtCity") as TextBox;
                TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;
                TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
                dt.Rows.Add(0, txtJobTitle.Text, txtEmployer.Text, txtCity.Text,
                    DateTime.TryParse(txtStartDate.Text, out DateTime startDate) ? startDate : DateTime.Now,
                    DateTime.TryParse(txtEndDate.Text, out DateTime endDate) ? endDate : DateTime.Now,
                    txtDescription.Text, Session["UserID"]);
            }
            return dt;
        }

        private DataTable GetSkillsDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("SkillName", typeof(string));
            dt.Columns.Add("UserID", typeof(string));
            foreach (RepeaterItem item in rptSkills.Items)
            {
                TextBox txtSkillName = item.FindControl("txtSkillName") as TextBox;
                dt.Rows.Add(0, txtSkillName.Text, Session["UserID"]);
            }
            return dt;
        }

        private DataTable GetLanguagesDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("LanguageName", typeof(string));
            dt.Columns.Add("UserID", typeof(string));
            foreach (RepeaterItem item in rptLanguages.Items)
            {
                TextBox txtLanguageName = item.FindControl("txtLanguageName") as TextBox;
                dt.Rows.Add(0, txtLanguageName.Text, Session["UserID"]);
            }
            return dt;
        }

        private DataTable GetCertificationsDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("CourseName", typeof(string));
            dt.Columns.Add("Institution", typeof(string));
            dt.Columns.Add("StartDate", typeof(DateTime));
            dt.Columns.Add("EndDate", typeof(DateTime));
            dt.Columns.Add("UserID", typeof(string));
            foreach (RepeaterItem item in rptCertifications.Items)
            {
                TextBox txtCourseName = item.FindControl("txtCourseName") as TextBox;
                TextBox txtInstitution = item.FindControl("txtInstitution") as TextBox;
                TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;
                dt.Rows.Add(0, txtCourseName.Text, txtInstitution.Text,
                    DateTime.TryParse(txtStartDate.Text, out DateTime startDate) ? startDate : DateTime.Now,
                    DateTime.TryParse(txtEndDate.Text, out DateTime endDate) ? endDate : DateTime.Now,
                    Session["UserID"]);
            }
            return dt;
        }

        private DataTable GetHobbiesDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("UserID", typeof(string));
            foreach (RepeaterItem item in rptHobbies.Items)
            {
                TextBox txtHobbyName = item.FindControl("txtHobbyName") as TextBox;
                dt.Rows.Add(0, txtHobbyName.Text, Session["UserID"]);
            }
            return dt;
        }

        private DataTable GetCustomSelectionDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("StartDate", typeof(DateTime));
            dt.Columns.Add("EndDate", typeof(DateTime));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("UserID", typeof(string));
            foreach (RepeaterItem item in rptCustomSelection.Items)
            {
                TextBox txtTitle = item.FindControl("txtTitle") as TextBox;
                TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;
                TextBox txtDescription = item.FindControl("txtDescription") as TextBox;
                dt.Rows.Add(0, txtTitle.Text,
                    DateTime.TryParse(txtStartDate.Text, out DateTime startDate) ? startDate : DateTime.Now,
                    DateTime.TryParse(txtEndDate.Text, out DateTime endDate) ? endDate : DateTime.Now,
                    txtDescription.Text, Session["UserID"]);
            }
            return dt;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["NareshResumeDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    // Update Personal Details
                    string updatePersonalQuery = @"UPDATE PersonalDetails SET 
                        FirstName = @FirstName, LastName = @LastName, JobTitle = @JobTitle, Email = @Email, 
                        Phone = @Phone, DateOfBirth = @DateOfBirth, Address = @Address, City = @City, 
                        State = @State, PostalCode = @PostalCode, Country = @Country, Nationality = @Nationality, 
                        PlaceOfBirth = @PlaceOfBirth, PersonalSummary = @PersonalSummary
                        WHERE UserID = @UserID AND ID = (SELECT TOP 1 ID FROM PersonalDetails WHERE UserID = @UserID ORDER BY ID DESC)";
                    using (SqlCommand cmd = new SqlCommand(updatePersonalQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                        cmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                        cmd.Parameters.AddWithValue("@JobTitle", txtJobTitle.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@DateOfBirth", DateTime.Parse(txtDateOfBirth.Text));
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@City", txtCity.Text);
                        cmd.Parameters.AddWithValue("@State", txtState.Text);
                        cmd.Parameters.AddWithValue("@PostalCode", txtPostalCode.Text);
                        cmd.Parameters.AddWithValue("@Country", txtCountry.Text);
                        cmd.Parameters.AddWithValue("@Nationality", txtNationality.Text);
                        cmd.Parameters.AddWithValue("@PlaceOfBirth", txtPlaceOfBirth.Text);
                        cmd.Parameters.AddWithValue("@PersonalSummary", txtProfessionalSummary.Text);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            // Insert new record if no existing record
                            string insertPersonalQuery = @"INSERT INTO PersonalDetails 
                                (FirstName, LastName, JobTitle, Email, Phone, DateOfBirth, Address, City, State, PostalCode, Country, Nationality, PlaceOfBirth, PersonalSummary, UserID)
                                VALUES (@FirstName, @LastName, @JobTitle, @Email, @Phone, @DateOfBirth, @Address, @City, @State, @PostalCode, @Country, @Nationality, @PlaceOfBirth, @PersonalSummary, @UserID)";
                            using (SqlCommand insertCmd = new SqlCommand(insertPersonalQuery, conn, transaction))
                            {
                                insertCmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
                                insertCmd.Parameters.AddWithValue("@LastName", txtLastName.Text);
                                insertCmd.Parameters.AddWithValue("@JobTitle", txtJobTitle.Text);
                                insertCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                                insertCmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                                insertCmd.Parameters.AddWithValue("@DateOfBirth", DateTime.Parse(txtDateOfBirth.Text));
                                insertCmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                                insertCmd.Parameters.AddWithValue("@City", txtCity.Text);
                                insertCmd.Parameters.AddWithValue("@State", txtState.Text);
                                insertCmd.Parameters.AddWithValue("@PostalCode", txtPostalCode.Text);
                                insertCmd.Parameters.AddWithValue("@Country", txtCountry.Text);
                                insertCmd.Parameters.AddWithValue("@Nationality", txtNationality.Text);
                                insertCmd.Parameters.AddWithValue("@PlaceOfBirth", txtPlaceOfBirth.Text);
                                insertCmd.Parameters.AddWithValue("@PersonalSummary", txtProfessionalSummary.Text);
                                insertCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Update IsFresher
                    string updateFresherQuery = "UPDATE Userss SET IsFresher = @IsFresher WHERE UserID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(updateFresherQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@IsFresher", chkIsFresher.Checked);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        cmd.ExecuteNonQuery();
                    }

                    // Update Education
                    foreach (RepeaterItem item in rptEducation.Items)
                    {
                        TextBox txtDegree = item.FindControl("txtDegree") as TextBox;
                        TextBox txtSchoolName = item.FindControl("txtSchoolName") as TextBox;
                        TextBox txtCity = item.FindControl("txtCity") as TextBox;
                        TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                        TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;
                        TextBox txtDescription = item.FindControl("txtDescription") as TextBox;

                        string insertEducationQuery = @"INSERT INTO Education 
                            (Degree, SchoolName, City, StartDate, EndDate, Description, UserID)
                            VALUES (@Degree, @SchoolName, @City, @StartDate, @EndDate, @Description, @UserID)";
                        using (SqlCommand cmd = new SqlCommand(insertEducationQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Degree", txtDegree.Text);
                            cmd.Parameters.AddWithValue("@SchoolName", txtSchoolName.Text);
                            cmd.Parameters.AddWithValue("@City", txtCity.Text);
                            cmd.Parameters.AddWithValue("@StartDate", DateTime.Parse(txtStartDate.Text));
                            cmd.Parameters.AddWithValue("@EndDate", DateTime.Parse(txtEndDate.Text));
                            cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Update Employment History
                    if (!chkIsFresher.Checked)
                    {
                        foreach (RepeaterItem item in rptEmploymentHistory.Items)
                        {
                            TextBox txtJobTitle = item.FindControl("txtJobTitle") as TextBox;
                            TextBox txtEmployer = item.FindControl("txtEmployer") as TextBox;
                            TextBox txtCity = item.FindControl("txtCity") as TextBox;
                            TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                            TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;
                            TextBox txtDescription = item.FindControl("txtDescription") as TextBox;

                            string insertEmploymentQuery = @"INSERT INTO EmploymentHistoryy 
                                (JobTitle, Employer, City, StartDate, EndDate, Description, UserID)
                                VALUES (@JobTitle, @Employer, @City, @StartDate, @EndDate, @Description, @UserID)";
                            using (SqlCommand cmd = new SqlCommand(insertEmploymentQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@JobTitle", txtJobTitle.Text);
                                cmd.Parameters.AddWithValue("@Employer", txtEmployer.Text);
                                cmd.Parameters.AddWithValue("@City", txtCity.Text);
                                cmd.Parameters.AddWithValue("@StartDate", DateTime.Parse(txtStartDate.Text));
                                cmd.Parameters.AddWithValue("@EndDate", DateTime.Parse(txtEndDate.Text));
                                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Update Skills
                    foreach (RepeaterItem item in rptSkills.Items)
                    {
                        TextBox txtSkillName = item.FindControl("txtSkillName") as TextBox;
                        string insertSkillQuery = "INSERT INTO Skills (SkillName, UserID) VALUES (@SkillName, @UserID)";
                        using (SqlCommand cmd = new SqlCommand(insertSkillQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@SkillName", txtSkillName.Text);
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Update Languages
                    foreach (RepeaterItem item in rptLanguages.Items)
                    {
                        TextBox txtLanguageName = item.FindControl("txtLanguageName") as TextBox;
                        string insertLanguageQuery = "INSERT INTO Languages (LanguageName, UserID) VALUES (@LanguageName, @UserID)";
                        using (SqlCommand cmd = new SqlCommand(insertLanguageQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@LanguageName", txtLanguageName.Text);
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Update Certifications
                    foreach (RepeaterItem item in rptCertifications.Items)
                    {
                        TextBox txtCourseName = item.FindControl("txtCourseName") as TextBox;
                        TextBox txtInstitution = item.FindControl("txtInstitution") as TextBox;
                        TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                        TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;

                        string insertCertificationQuery = @"INSERT INTO Courses 
                            (CourseName, Institution, StartDate, EndDate, UserID)
                            VALUES (@CourseName, @Institution, @StartDate, @EndDate, @UserID)";
                        using (SqlCommand cmd = new SqlCommand(insertCertificationQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@CourseName", txtCourseName.Text);
                            cmd.Parameters.AddWithValue("@Institution", txtInstitution.Text);
                            cmd.Parameters.AddWithValue("@StartDate", DateTime.Parse(txtStartDate.Text));
                            cmd.Parameters.AddWithValue("@EndDate", DateTime.Parse(txtEndDate.Text));
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Update Internships
                    string updateInternshipQuery = @"UPDATE Internships SET 
                        JobTitle = @JobTitle, CompanyName = @CompanyName, StartDate = @StartDate, 
                        EndDate = @EndDate, Description = @Description
                        WHERE UserID = @UserID AND ID = (SELECT TOP 1 ID FROM Internships WHERE UserID = @UserID ORDER BY ID DESC)";
                    using (SqlCommand cmd = new SqlCommand(updateInternshipQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@JobTitle", txtInternJobTitle.Text);
                        cmd.Parameters.AddWithValue("@CompanyName", txtInternCompany.Text);
                        cmd.Parameters.AddWithValue("@StartDate", DateTime.Parse(txtInternStartDate.Text));
                        cmd.Parameters.AddWithValue("@EndDate", DateTime.Parse(txtInternEndDate.Text));
                        cmd.Parameters.AddWithValue("@Description", txtInternDescription.Text);
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            string insertInternshipQuery = @"INSERT INTO Internships 
                                (JobTitle, CompanyName, StartDate, EndDate, Description, UserID)
                                VALUES (@JobTitle, @CompanyName, @StartDate, @EndDate, @Description, @UserID)";
                            using (SqlCommand insertCmd = new SqlCommand(insertInternshipQuery, conn, transaction))
                            {
                                insertCmd.Parameters.AddWithValue("@JobTitle", txtInternJobTitle.Text);
                                insertCmd.Parameters.AddWithValue("@CompanyName", txtInternCompany.Text);
                                insertCmd.Parameters.AddWithValue("@StartDate", DateTime.Parse(txtInternStartDate.Text));
                                insertCmd.Parameters.AddWithValue("@EndDate", DateTime.Parse(txtInternEndDate.Text));
                                insertCmd.Parameters.AddWithValue("@Description", txtInternDescription.Text);
                                insertCmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Update Hobbies
                    foreach (RepeaterItem item in rptHobbies.Items)
                    {
                        TextBox txtHobbyName = item.FindControl("txtHobbyName") as TextBox;
                        string insertHobbyQuery = "INSERT INTO Hobbies (Name, UserID) VALUES (@Name, @UserID)";
                        using (SqlCommand cmd = new SqlCommand(insertHobbyQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Name", txtHobbyName.Text);
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Update Custom Selection
                    foreach (RepeaterItem item in rptCustomSelection.Items)
                    {
                        TextBox txtTitle = item.FindControl("txtTitle") as TextBox;
                        TextBox txtStartDate = item.FindControl("txtStartDate") as TextBox;
                        TextBox txtEndDate = item.FindControl("txtEndDate") as TextBox;
                        TextBox txtDescription = item.FindControl("txtDescription") as TextBox;

                        string insertCustomQuery = @"INSERT INTO CustomSelection 
                            (Title, StartDate, EndDate, Description, UserID)
                            VALUES (@Title, @StartDate, @EndDate, @Description, @UserID)";
                        using (SqlCommand cmd = new SqlCommand(insertCustomQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                            cmd.Parameters.AddWithValue("@StartDate", DateTime.Parse(txtStartDate.Text));
                            cmd.Parameters.AddWithValue("@EndDate", DateTime.Parse(txtEndDate.Text));
                            cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    Response.Redirect("Resume.aspx");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Error saving data: {ex.Message}');", true);
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Resume.aspx");
        }
    }
}