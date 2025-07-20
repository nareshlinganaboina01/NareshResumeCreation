<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Resume5.aspx.cs" Inherits="NareshResumeCreation.Resume5" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bold Two-Column Resume</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style type="text/css">
        body {
            font-family: 'Verdana', sans-serif;
            margin: 0;
            padding: 0;
            background: #e9ecef;
            color: #2c3e50;
            line-height: 1.6;
        }
        .resume-container {
            max-width: 1000px;
            margin: 30px auto;
            background: #fff;
            border-radius: 8px;
            box-shadow: 0 8px 25px rgba(0, 0, 0, 0.15);
            overflow: hidden; /* Clearfix for floated children */
        }
        .resume-container::after {
            content: "";
            display: table;
            clear: both; /* Clearfix to prevent layout collapse */
        }
        .sidebar {
            width: 35%; /* Fixed 35% width */
            float: left;
            background: #e74c3c;
            color: #fff;
            padding: 25px;
            box-sizing: border-box;
        }
        .main-content {
            width: 65%; /* Fixed 65% width */
            float: right;
            padding: 25px;
            background: #fff;
            box-sizing: border-box;
        }
        .sidebar h1 {
            font-size: 28px;
            margin: 0 0 10px 0;
            font-weight: 700;
            text-transform: uppercase;
        }
        .sidebar .job-title {
            font-size: 14px;
            font-weight: 300;
            opacity: 0.9;
            margin-bottom: 20px;
        }
        .sidebar-section {
            margin-bottom: 20px;
        }
        .sidebar-section h3 {
            font-size: 16px;
            margin-bottom: 10px;
            font-weight: 600;
            border-bottom: 2px solid #fff;
            padding-bottom: 5px;
        }
        .sidebar-section p {
            font-size: 13px;
            margin: 5px 0;
        }
        .section {
            margin-bottom: 25px;
        }
        .section h3 {
            font-size: 18px;
            color: #e74c3c;
            margin-bottom: 12px;
            font-weight: 600;
            border-bottom: 2px dashed #e74c3c;
            padding-bottom: 5px;
        }
        .section p {
            font-size: 13px;
            margin: 5px 0;
        }
        .list {
            list-style: none;
            padding: 0;
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
        }
        .list li {
            background: #ecf0f1;
            padding: 6px 12px;
            border-radius: 4px;
            font-size: 12px;
            transition: background 0.3s;
        }
        .list li:hover {
            background: #e74c3c;
            color: #fff;
        }
        .entry {
            margin-bottom: 15px;
            padding: 10px;
            background: #f9f9f9;
            border-radius: 5px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.05);
        }
        .entry p {
            margin: 0;
            font-size: 13px;
            color: #34495e;
        }
        .edit-input, .edit-textarea {
            width: 100%;
            padding: 5px;
            font-size: 13px;
            border: 1px solid #ccc;
            border-radius: 4px;
            margin: 5px 0;
            box-sizing: border-box;
        }
        .edit-textarea {
            height: 80px;
            resize: vertical;
        }
        .edit-list-item {
            display: flex;
            align-items: center;
            margin: 6px 0;
        }
        .edit-list-item input {
            flex-grow: 1;
            margin-right: 10px;
        }
        .button-container {
            clear: both; /* Ensure buttons appear below floated columns */
            text-align: center;
            margin: 20px 0;
            padding: 10px;
            background: #fff;
        }
        .btn {
            background: #e74c3c;
            color: #fff;
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
            transition: background 0.3s;
            margin: 0 10px;
        }
        .btn:hover {
            background: #c0392b;
        }
        .btn-delete {
            background: #dc3545;
        }
        .btn-delete:hover {
            background: #b02a37;
        }
        /* Responsive Design */
        @media (max-width: 768px) {
            .sidebar, .main-content {
                width: 100%; /* Full width on mobile */
                float: none; /* Remove floats */
            }
            .button-container {
                display: flex;
                flex-wrap: wrap;
                justify-content: center;
            }
            .btn {
                margin: 10px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="resume-container">
            <asp:Panel ID="pnlView" runat="server" Visible="true">
                <!-- Sidebar -->
                <div class="sidebar">
                    <h1><asp:Label ID="text5FirstName" runat="server" /> <asp:Label ID="text5LastName" runat="server" /></h1>
                    <p class="job-title"><asp:Label ID="text5JobTitle" runat="server" /></p>

                    <!-- Contact Information -->
                    <div class="sidebar-section">
                        <h3>Contact</h3>
                        <p>✉️ <asp:Label ID="text5Email" runat="server" /></p>
                        <p>📱 <asp:Label ID="text5Phone" runat="server" /></p>
                        <p>🏠 <asp:Label ID="text5Address" runat="server" />, 
                           <asp:Label ID="text5City" runat="server" />, 
                           <asp:Label ID="text5State" runat="server" />, 
                           <asp:Label ID="text5PostalCode" runat="server" />, 
                           <asp:Label ID="text5Country" runat="server" /></p>
                    </div>

                    <!-- Personal Information -->
                    <div class="sidebar-section">
                        <h3>Personal Info</h3>
                        <p>🎂 <asp:Label ID="text5DateOfBirth" runat="server" /></p>
                        <p>🌍 <asp:Label ID="text5PlaceOfBirth" runat="server" /></p>
                        <p>📍 <asp:Label ID="text5Nationality" runat="server" /></p>
                    </div>

                    <!-- Skills -->
                    <div class="sidebar-section">
                        <h3>Skills</h3>
                        <ul class="list">
                            <asp:Repeater ID="rpt5Skills" runat="server">
                                <ItemTemplate>
                                    <li><asp:Literal ID="lit5Skill" runat="server" Text='<%# Eval("SkillName") %>' /></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>

                    <!-- Languages -->
                    <div class="sidebar-section">
                        <h3>Languages</h3>
                        <ul class="list">
                            <asp:Repeater ID="rpt5Languages" runat="server">
                                <ItemTemplate>
                                    <li><asp:Literal ID="lit5Language" runat="server" Text='<%# Eval("LanguageName") %>' /></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>

                <!-- Main Content -->
                <div class="main-content">
                    <!-- Professional Summary -->
                    <div class="section">
                        <h3>Professional Summary</h3>
                        <div class="entry">
                            <p><asp:Label ID="text5ProfessionalSummary" runat="server" /></p>
                        </div>
                    </div>

                    <!-- Employment History -->
                    <div class="section">
                        <h3>Employment History</h3>
                        <asp:Panel ID="pnl5Fresher" runat="server" Visible="false">
                            <div class="entry">
                                <p>Fresher (No work experience)</p>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnl5EmploymentHistory" runat="server" Visible="false">
                            <asp:Repeater ID="rpt5EmploymentHistory" runat="server">
                                <ItemTemplate>
                                    <div class="entry">
                                        <p>
                                            I worked as <asp:Label ID="text5JobTitle" runat="server" Text='<%# Eval("JobTitle") %>' /> 
                                            at <asp:Label ID="text5Employer" runat="server" Text='<%# Eval("Employer") %>' />, 
                                            <asp:Label ID="text5City" runat="server" Text='<%# Eval("City") %>' /> 
                                            between <%# Eval("StartDate", "{0:MMM yyyy}") %> and 
                                            <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMM yyyy}") %>. 
                                            <asp:Label ID="text5Description" runat="server" Text='<%# Eval("Description") %>' />
                                        </p>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </asp:Panel>
                    </div>

                    <!-- Education -->
                    <div class="section">
                        <h3>Education</h3>
                        <asp:Repeater ID="rpt5Education" runat="server">
                            <ItemTemplate>
                                <div class="entry">
                                    <p>
                                        I completed my <asp:Label ID="text5Degree" runat="server" Text='<%# Eval("Degree") %>' /> 
                                        from <asp:Label ID="text5SchoolName" runat="server" Text='<%# Eval("SchoolName") %>' />, 
                                        <asp:Label ID="text5City" runat="server" Text='<%# Eval("City") %>' /> 
                                        between <%# Eval("StartDate", "{0:MMM yyyy}") %> and 
                                        <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMM yyyy}") %>. 
                                        <asp:Label ID="text5Description" runat="server" Text='<%# Eval("Description") %>' />
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <!-- Certifications -->
                    <div class="section">
                        <h3>Certifications</h3>
                        <asp:Repeater ID="rpt5Certifications" runat="server">
                            <ItemTemplate>
                                <div class="entry">
                                    <p>
                                        I completed <asp:Label ID="text5CourseName" runat="server" Text='<%# Eval("CourseName") %>' /> 
                                        from <asp:Label ID="text5Institution" runat="server" Text='<%# Eval("Institution") %>' /> 
                                        between <%# Eval("StartDate", "{0:MMM yyyy}") %> and 
                                        <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMM yyyy}") %>.
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <!-- Internships -->
                    <div class="section">
                        <h3>Internships</h3>
                        <div class="entry">
                            <p>
                                I worked as <asp:Label ID="text5InternJobTitle" runat="server" /> 
                                at <asp:Label ID="text5InternCompany" runat="server" /> 
                                between <asp:Label ID="text5InternStartDate" runat="server" /> and 
                                <asp:Label ID="text5InternEndDate" runat="server" />. 
                                <asp:Label ID="text5InternDescription" runat="server" />
                            </p>
                        </div>
                    </div>

                    <!-- Hobbies -->
                    <div class="section">
                        <h3>Hobbies</h3>
                        <ul class="list">
                            <asp:Repeater ID="rpt5Hobbies" runat="server">
                                <ItemTemplate>
                                    <li><asp:Literal ID="lit5Hobby" runat="server" Text='<%# Eval("Name") %>' /></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>

                    <!-- Additional Information -->
                    <div class="section">
                        <h3>Additional Information</h3>
                        <asp:Repeater ID="rpt5CustomSelection" runat="server">
                            <ItemTemplate>
                                <div class="entry">
                                    <p>
                                        I worked on <asp:Label ID="text5Title" runat="server" Text='<%# Eval("Title") %>' /> 
                                        between <%# Eval("StartDate", "{0:MMM yyyy}") %> and 
                                        <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMM yyyy}") %>. 
                                        <asp:Label ID="text5Description" runat="server" Text='<%# Eval("Description") %>' />
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlEdit" runat="server" Visible="false">
                <!-- Sidebar -->
                <div class="sidebar">
                    <asp:TextBox ID="txtEditFirstName" runat="server" CssClass="edit-input" Placeholder="First Name" />
                    <asp:TextBox ID="txtEditLastName" runat="server" CssClass="edit-input" Placeholder="Last Name" />
                    <asp:TextBox ID="txtEditJobTitle" runat="server" CssClass="edit-input" Placeholder="Job Title" />

                    <!-- Contact Information -->
                    <div class="sidebar-section">
                        <h3>Contact</h3>
                        <p>✉️ <asp:TextBox ID="txtEditEmail" runat="server" CssClass="edit-input" Placeholder="Email" /></p>
                        <p>📱 <asp:TextBox ID="txtEditPhone" runat="server" CssClass="edit-input" Placeholder="Phone" /></p>
                        <p>🏠 <asp:TextBox ID="txtEditAddress" runat="server" CssClass="edit-input" Placeholder="Address" />, 
                           <asp:TextBox ID="txtEditCity" runat="server" CssClass="edit-input" Placeholder="City" />, 
                           <asp:TextBox ID="txtEditState" runat="server" CssClass="edit-input" Placeholder="State" />, 
                           <asp:TextBox ID="txtEditPostalCode" runat="server" CssClass="edit-input" Placeholder="Postal Code" />, 
                           <asp:TextBox ID="txtEditCountry" runat="server" CssClass="edit-input" Placeholder="Country" /></p>
                    </div>

                    <!-- Personal Information -->
                    <div class="sidebar-section">
                        <h3>Personal Info</h3>
                        <p>🎂 <asp:TextBox ID="txtEditDateOfBirth" runat="server" CssClass="edit-input" TextMode="Date" /></p>
                        <p>🌍 <asp:TextBox ID="txtEditPlaceOfBirth" runat="server" CssClass="edit-input" Placeholder="Place of Birth" /></p>
                        <p>📍 <asp:TextBox ID="txtEditNationality" runat="server" CssClass="edit-input" Placeholder="Nationality" /></p>
                    </div>

                    <!-- Skills -->
                    <div class="sidebar-section">
                        <h3>Skills</h3>
                        <asp:Repeater ID="rptEditSkills" runat="server">
                            <ItemTemplate>
                                <div class="edit-list-item">
                                    <asp:TextBox ID="txtSkillName" runat="server" Text='<%# Eval("SkillName") %>' CssClass="edit-input" />
                                    <asp:Button ID="btnDeleteSkill" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Skill_Command" CssClass="btn btn-delete" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:TextBox ID="txtNewSkill" runat="server" CssClass="edit-input" Placeholder="Add new skill" />
                        <asp:Button ID="btnAddSkill" runat="server" Text="Add Skill" OnClick="btnAddSkill_Click" CssClass="btn" />
                    </div>

                    <!-- Languages -->
                    <div class="sidebar-section">
                        <h3>Languages</h3>
                        <asp:Repeater ID="rptEditLanguages" runat="server">
                            <ItemTemplate>
                                <div class="edit-list-item">
                                    <asp:TextBox ID="txtLanguageName" runat="server" Text='<%# Eval("LanguageName") %>' CssClass="edit-input" />
                                    <asp:Button ID="btnDeleteLanguage" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Language_Command" CssClass="btn btn-delete" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:TextBox ID="txtNewLanguage" runat="server" CssClass="edit-input" Placeholder="Add new language" />
                        <asp:Button ID="btnAddLanguage" runat="server" Text="Add Language" OnClick="btnAddLanguage_Click" CssClass="btn" />
                    </div>
                </div>

                <!-- Main Content -->
                <div class="main-content">
                    <!-- Professional Summary -->
                    <div class="section">
                        <h3>Professional Summary</h3>
                        <div class="entry">
                            <asp:TextBox ID="txtEditProfessionalSummary" runat="server" CssClass="edit-textarea" TextMode="MultiLine" Placeholder="Professional Summary" />
                        </div>
                    </div>

                    <!-- Employment History -->
                    <div class="section">
                        <h3>Employment History</h3>
                        <asp:CheckBox ID="chkFresher" runat="server" Text="I am a fresher (no work experience)" AutoPostBack="true" OnCheckedChanged="chkFresher_CheckedChanged" />
                        <asp:Panel ID="pnlEditEmploymentHistory" runat="server" Visible="true">
                            <asp:Repeater ID="rptEditEmploymentHistory" runat="server">
                                <ItemTemplate>
                                    <div class="entry">
                                        <p>
                                            Job Title: <asp:TextBox ID="txtJobTitle" runat="server" Text='<%# Eval("JobTitle") %>' CssClass="edit-input" /><br />
                                            Employer: <asp:TextBox ID="txtEmployer" runat="server" Text='<%# Eval("Employer") %>' CssClass="edit-input" /><br />
                                            City: <asp:TextBox ID="txtCity" runat="server" Text='<%# Eval("City") %>' CssClass="edit-input" /><br />
                                            Start Date: <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" TextMode="Date" /><br />
                                            End Date: <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" TextMode="Date" /><br />
                                            Description: <asp:TextBox ID="txtDescription" runat="server" Text='<%# Eval("Description") %>' CssClass="edit-textarea" TextMode="MultiLine" /><br />
                                            <asp:Button ID="btnDeleteEmployment" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Employment_Command" CssClass="btn btn-delete" />
                                        </p>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="entry">
                                <p>
                                    Job Title: <asp:TextBox ID="txtNewJobTitle" runat="server" CssClass="edit-input" Placeholder="Job Title" /><br />
                                    Employer: <asp:TextBox ID="txtNewEmployer" runat="server" CssClass="edit-input" Placeholder="Employer" /><br />
                                    City: <asp:TextBox ID="txtNewCity" runat="server" CssClass="edit-input" Placeholder="City" /><br />
                                    Start Date: <asp:TextBox ID="txtNewStartDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                    End Date: <asp:TextBox ID="txtNewEndDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                    Description: <asp:TextBox ID="txtNewDescription" runat="server" CssClass="edit-textarea" TextMode="MultiLine" Placeholder="Description" /><br />
                                    <asp:Button ID="btnAddEmployment" runat="server" Text="Add Employment" OnClick="btnAddEmployment_Click" CssClass="btn" />
                                </p>
                            </div>
                        </asp:Panel>
                    </div>

                    <!-- Education -->
                    <div class="section">
                        <h3>Education</h3>
                        <asp:Repeater ID="rptEditEducation" runat="server">
                            <ItemTemplate>
                                <div class="entry">
                                    <p>
                                        Degree: <asp:TextBox ID="txtDegree" runat="server" Text='<%# Eval("Degree") %>' CssClass="edit-input" /><br />
                                        School: <asp:TextBox ID="txtSchoolName" runat="server" Text='<%# Eval("SchoolName") %>' CssClass="edit-input" /><br />
                                        City: <asp:TextBox ID="txtCity" runat="server" Text='<%# Eval("City") %>' CssClass="edit-input" /><br />
                                        Start Date: <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" TextMode="Date" /><br />
                                        End Date: <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" TextMode="Date" /><br />
                                        Description: <asp:TextBox ID="txtDescription" runat="server" Text='<%# Eval("Description") %>' CssClass="edit-textarea" TextMode="MultiLine" /><br />
                                        <asp:Button ID="btnDeleteEducation" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Education_Command" CssClass="btn btn-delete" />
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="entry">
                            <p>
                                Degree: <asp:TextBox ID="txtNewDegree" runat="server" CssClass="edit-input" Placeholder="Degree" /><br />
                                School: <asp:TextBox ID="txtNewSchoolName" runat="server" CssClass="edit-input" Placeholder="School Name" /><br />
                                City: <asp:TextBox ID="txtNewEducationCity" runat="server" CssClass="edit-input" Placeholder="City" /><br />
                                Start Date: <asp:TextBox ID="txtNewEducationStartDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                End Date: <asp:TextBox ID="txtNewEducationEndDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                Description: <asp:TextBox ID="txtNewEducationDescription" runat="server" CssClass="edit-textarea" TextMode="MultiLine" Placeholder="Description" /><br />
                                <asp:Button ID="btnAddEducation" runat="server" Text="Add Education" OnClick="btnAddEducation_Click" CssClass="btn" />
                            </p>
                        </div>
                    </div>

                    <!-- Certifications -->
                    <div class="section">
                        <h3>Certifications</h3>
                        <asp:Repeater ID="rptEditCertifications" runat="server">
                            <ItemTemplate>
                                <div class="entry">
                                    <p>
                                        Course: <asp:TextBox ID="txtCourseName" runat="server" Text='<%# Eval("CourseName") %>' CssClass="edit-input" /><br />
                                        Institution: <asp:TextBox ID="txtInstitution" runat="server" Text='<%# Eval("Institution") %>' CssClass="edit-input" /><br />
                                        Start Date: <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" TextMode="Date" /><br />
                                        End Date: <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" TextMode="Date" /><br />
                                        <asp:Button ID="btnDeleteCertification" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Certification_Command" CssClass="btn btn-delete" />
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="entry">
                            <p>
                                Course: <asp:TextBox ID="txtNewCourseName" runat="server" CssClass="edit-input" Placeholder="Course Name" /><br />
                                Institution: <asp:TextBox ID="txtNewInstitution" runat="server" CssClass="edit-input" Placeholder="Institution" /><br />
                                Start Date: <asp:TextBox ID="txtNewCertStartDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                End Date: <asp:TextBox ID="txtNewCertEndDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                <asp:Button ID="btnAddCertification" runat="server" Text="Add Certification" OnClick="btnAddCertification_Click" CssClass="btn" />
                            </p>
                        </div>
                    </div>

                    <!-- Internships -->
                    <div class="section">
                        <h3>Internships</h3>
                        <div class="entry">
                            <p>
                                Job Title: <asp:TextBox ID="txtEditInternJobTitle" runat="server" CssClass="edit-input" Placeholder="Job Title" /><br />
                                Company: <asp:TextBox ID="txtEditInternCompany" runat="server" CssClass="edit-input" Placeholder="Company" /><br />
                                Start Date: <asp:TextBox ID="txtEditInternStartDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                End Date: <asp:TextBox ID="txtEditInternEndDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                Description: <asp:TextBox ID="txtEditInternDescription" runat="server" CssClass="edit-textarea" TextMode="MultiLine" Placeholder="Description" />
                            </p>
                        </div>
                    </div>

                    <!-- Hobbies -->
                    <div class="section">
                        <h3>Hobbies</h3>
                        <asp:Repeater ID="rptEditHobbies" runat="server">
                            <ItemTemplate>
                                <div class="edit-list-item">
                                    <asp:TextBox ID="txtHobbyName" runat="server" Text='<%# Eval("Name") %>' CssClass="edit-input" />
                                    <asp:Button ID="btnDeleteHobby" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Hobby_Command" CssClass="btn btn-delete" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:TextBox ID="txtNewHobby" runat="server" CssClass="edit-input" Placeholder="Add new hobby" />
                        <asp:Button ID="btnAddHobby" runat="server" Text="Add Hobby" OnClick="btnAddHobby_Click" CssClass="btn" />
                    </div>

                    <!-- Additional Information -->
                    <div class="section">
                        <h3>Additional Information</h3>
                        <asp:Repeater ID="rptEditCustomSelection" runat="server">
                            <ItemTemplate>
                                <div class="entry">
                                    <p>
                                        Title: <asp:TextBox ID="txtTitle" runat="server" Text='<%# Eval("Title") %>' CssClass="edit-input" /><br />
                                        Start Date: <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" TextMode="Date" /><br />
                                        End Date: <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" TextMode="Date" /><br />
                                        Description: <asp:TextBox ID="txtDescription" runat="server" Text='<%# Eval("Description") %>' CssClass="edit-textarea" TextMode="MultiLine" /><br />
                                        <asp:Button ID="btnDeleteCustom" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="CustomSelection_Command" CssClass="btn btn-delete" />
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="entry">
                            <p>
                                Title: <asp:TextBox ID="txtNewCustomTitle" runat="server" CssClass="edit-input" Placeholder="Title" /><br />
                                Start Date: <asp:TextBox ID="txtNewCustomStartDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                End Date: <asp:TextBox ID="txtNewCustomEndDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                Description: <asp:TextBox ID="txtNewCustomDescription" runat="server" CssClass="edit-textarea" TextMode="MultiLine" Placeholder="Description" /><br />
                                <asp:Button ID="btnAddCustom" runat="server" Text="Add Custom Section" OnClick="btnAddCustom_Click" CssClass="btn" />
                            </p>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <!-- Button Container -->
            <div class="button-container">
                <asp:Button ID="btnEdit" runat="server" Text="Edit Resume" OnClick="btnEdit_Click" CssClass="btn" Visible="true" />
                <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="btnSave_Click" CssClass="btn" Visible="false" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn" Visible="false" />
                <asp:Button ID="btnDownload" runat="server" Text="Download PDF" OnClick="btnDownload_Click" CssClass="btn" />
                <asp:Button ID="btnBack" runat="server" Text="Back to Home" OnClick="btnBack_Click" CssClass="btn" />
            </div>
        </div>
    </form>
</body>
</html>
