<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Resume2.aspx.cs" Inherits="NareshResumeCreation.Resume2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modern Resume</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style type="text/css">
        body {
            font-family: 'Arial', sans-serif;
            margin: 0;
            padding: 0;
            background: #e9ecef;
            color: #212529;
            line-height: 1.6;
        }
        .resume-wrapper {
            max-width: 900px;
            margin: 30px auto;
            background: #fff;
            border-radius: 10px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
            display: flex;
            overflow: hidden;
        }
        .sidebar {
            width: 30%;
            background: #2b2d42;
            color: #fff;
            padding: 30px;
            flex-shrink: 0;
        }
        .main-content {
            width: 70%;
            padding: 40px;
            background: #f8f9fa;
            box-sizing: border-box;
            flex-grow: 1;
        }
        .profile-header {
            text-align: center;
            margin-bottom: 30px;
        }
        .profile-header h1 {
            font-size: 28px;
            margin: 0;
            font-weight: 700;
        }
        .profile-header .job-title {
            font-size: 16px;
            color: #8d99ae;
            margin-top: 5px;
        }
        .sidebar-section {
            margin-bottom: 25px;
        }
        .sidebar-section h3 {
            font-size: 16px;
            color: #ef233c;
            margin-bottom: 15px;
            text-transform: uppercase;
            letter-spacing: 1px;
        }
        .sidebar-section p {
            margin: 8px 0;
            font-size: 13px;
        }
        .sidebar-section .icon {
            color: #ef233c;
            margin-right: 10px;
        }
        .skills-list, .languages-list, .hobbies-list {
            list-style: none;
            padding: 0;
        }
        .skills-list li, .languages-list li, .hobbies-list li {
            background: #ef233c;
            padding: 6px 12px;
            margin: 5px 0;
            border-radius: 5px;
            font-size: 13px;
            transition: transform 0.2s;
        }
        .skills-list li:hover, .languages-list li:hover, .hobbies-list li:hover {
            transform: translateX(5px);
        }
        .main-section {
            margin-bottom: 30px;
            padding: 20px;
            background: #fff;
            border-radius: 8px;
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.05);
            transition: box-shadow 0.3s;
        }
        .main-section:hover {
            box-shadow: 0 4px 16px rgba(0, 0, 0, 0.1);
        }
        .main-section h2 {
            font-size: 20px;
            color: #2b2d42;
            border-bottom: 2px solid #ef233c;
            padding-bottom: 8px;
            margin: 0 0 15px 0;
            text-transform: uppercase;
            letter-spacing: 1px;
        }
        .entry {
            margin-bottom: 15px;
            padding: 15px;
            border-left: 4px solid #ef233c;
            background: #f1f3f5;
            border-radius: 4px;
        }
        .entry-content {
            font-size: 13px;
            color: #495057;
            margin: 0;
        }
        .btn {
            background: #ef233c;
            color: #fff;
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-size: 14px;
            transition: background 0.3s, transform 0.2s;
            margin: 10px 5px;
        }
        .btn:hover {
            background: #d90429;
            transform: scale(1.05);
        }
        .btn-back {
            background: #6c757d;
        }
        .btn-back:hover {
            background: #5a6268;
        }
        .edit-section {
            margin-bottom: 20px;
        }
        .edit-section h3 {
            font-size: 18px;
            color: #2b2d42;
            margin-bottom: 10px;
        }
        .edit-section input[type="text"],
        .edit-section input[type="date"],
        .edit-section textarea {
            width: 100%;
            padding: 8px;
            margin: 5px 0;
            border: 1px solid #ced4da;
            border-radius: 4px;
            box-sizing: border-box;
        }
        .edit-section textarea {
            height: 100px;
            resize: vertical;
        }
        .repeater-item {
            padding: 10px;
            margin-bottom: 10px;
            border: 1px solid #dee2e6;
            border-radius: 4px;
            background: #f8f9fa;
        }
        .btn-delete {
            background: #dc3545;
            color: #fff;
            padding: 5px 10px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 12px;
            margin-left: 10px;
        }
        .btn-delete:hover {
            background: #c82333;
        }
        .add-section {
            margin-top: 10px;
        }
        .add-section input[type="text"],
        .add-section input[type="date"],
        .add-section textarea {
            width: calc(100% - 90px);
            display: inline-block;
            margin-right: 10px;
        }
        .add-section .btn {
            width: 80px;
            padding: 8px;
        }
        .checkbox-section {
            margin-bottom: 20px;
        }
        .checkbox-section label {
            font-size: 14px;
            margin-left: 5px;
        }
        @media (max-width: 768px) {
            .resume-wrapper {
                flex-direction: column;
                margin: 15px;
            }
            .sidebar, .main-content {
                width: 100%;
            }
            .sidebar {
                padding: 20px;
            }
            .main-content {
                padding: 20px;
            }
            .add-section input[type="text"],
            .add-section input[type="date"],
            .add-section textarea {
                width: 100%;
                margin-bottom: 10px;
            }
            .add-section .btn {
                width: 100%;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- View Mode -->
        <asp:Panel ID="pnlView" runat="server">
            <div class="resume-wrapper">
                <!-- Sidebar Section -->
                <div class="sidebar">
                    <div class="profile-header">
                        <h1><asp:Label ID="text2FirstName" runat="server" /> <asp:Label ID="text2LastName" runat="server" /></h1>
                        <p class="job-title"><asp:Label ID="text2JobTitle" runat="server" /></p>
                    </div>
                    
                    <!-- Contact Information -->
                    <div class="sidebar-section">
                        <h3>Contact</h3>
                        <p><span class="icon">✉️</span><asp:Label ID="text2Email" runat="server" /></p>
                        <p><span class="icon">📱</span><asp:Label ID="text2Phone" runat="server" /></p>
                        <p><span class="icon">🏠</span><asp:Label ID="text2Address" runat="server" />, 
                           <asp:Label ID="text2City" runat="server" />, 
                           <asp:Label ID="text2State" runat="server" />, 
                           <asp:Label ID="text2PostalCode" runat="server" />, 
                           <asp:Label ID="text2Country" runat="server" /></p>
                    </div>
                    
                    <!-- Personal Information -->
                    <div class="sidebar-section">
                        <h3>Personal Info</h3>
                        <p><span class="icon">🎂</span><asp:Label ID="text2DateOfBirth" runat="server" /></p>
                        <p><span class="icon">🌍</span><asp:Label ID="text2PlaceOfBirth" runat="server" /></p>
                        <p><span class="icon">📍</span><asp:Label ID="text2Nationality" runat="server" /></p>
                    </div>
                    
                    <!-- Skills -->
                    <div class="sidebar-section">
                        <h3>Skills</h3>
                        <ul class="skills-list">
                            <asp:Repeater ID="rpt2Skills" runat="server">
                                <ItemTemplate>
                                    <li><asp:Literal ID="lit2Skill" runat="server" Text='<%# Eval("SkillName") %>' /></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    
                    <!-- Languages -->
                    <div class="sidebar-section">
                        <h3>Languages</h3>
                        <ul class="languages-list">
                            <asp:Repeater ID="rpt2Languages" runat="server">
                                <ItemTemplate>
                                    <li><asp:Literal ID="lit2Language" runat="server" Text='<%# Eval("LanguageName") %>' /></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    
                    <!-- Hobbies -->
                    <div class="sidebar-section">
                        <h3>Hobbies</h3>
                        <ul class="hobbies-list">
                            <asp:Repeater ID="rpt2Hobbies" runat="server">
                                <ItemTemplate>
                                    <li><asp:Literal ID="lit2Hobby" runat="server" Text='<%# Eval("Name") %>' /></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>

                <!-- Main Content Section -->
                <div class="main-content">
                    <!-- Professional Summary -->
                    <div class="main-section">
                        <h2>Professional Summary</h2>
                        <p class="entry-content"><asp:Label ID="text2ProfessionalSummary" runat="server" /></p>
                    </div>

                    <!-- Employment History -->
                    <div class="main-section">
                        <h2>Employment History</h2>
                        <asp:Panel ID="pnl2Fresher" runat="server" Visible="false">
                            <p class="entry-content">Fresher (No work experience)</p>
                        </asp:Panel>
                        <asp:Panel ID="pnl2EmploymentHistory" runat="server" Visible="false">
                            <asp:Repeater ID="rpt2EmploymentHistory" runat="server">
                                <ItemTemplate>
                                    <div class="entry">
                                        <p class="entry-content">
                                            I worked as <asp:Label ID="text2JobTitle" runat="server" Text='<%# Eval("JobTitle") %>' /> 
                                            at <asp:Label ID="text2Employer" runat="server" Text='<%# Eval("Employer") %>' />, 
                                            <asp:Label ID="text2City" runat="server" Text='<%# Eval("City") %>' /> 
                                            between <%# Eval("StartDate", "{0:MMM yyyy}") %> and 
                                            <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMM yyyy}") %>. 
                                            <asp:Label ID="text2Description" runat="server" Text='<%# Eval("Description") %>' />
                                        </p>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </asp:Panel>
                    </div>

                    <!-- Education -->
                    <div class="main-section">
                        <h2>Education</h2>
                        <asp:Repeater ID="rpt2Education" runat="server">
                            <ItemTemplate>
                                <div class="entry">
                                    <p class="entry-content">
                                        I completed my <asp:Label ID="text2Degree" runat="server" Text='<%# Eval("Degree") %>' /> 
                                        from <asp:Label ID="text2SchoolName" runat="server" Text='<%# Eval("SchoolName") %>' />, 
                                        <asp:Label ID="text2City" runat="server" Text='<%# Eval("City") %>' /> 
                                        between <%# Eval("StartDate", "{0:MMM yyyy}") %> and 
                                        <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMM yyyy}") %>. 
                                        <asp:Label ID="text2Description" runat="server" Text='<%# Eval("Description") %>' />
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <!-- Certifications -->
                    <div class="main-section">
                        <h2>Certifications</h2>
                        <asp:Repeater ID="rpt2Certifications" runat="server">
                            <ItemTemplate>
                                <div class="entry">
                                    <p class="entry-content">
                                        I completed <asp:Label ID="text2CourseName" runat="server" Text='<%# Eval("CourseName") %>' /> 
                                        from <asp:Label ID="text2Institution" runat="server" Text='<%# Eval("Institution") %>' /> 
                                        between <%# Eval("StartDate", "{0:MMM yyyy}") %> and 
                                        <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMM yyyy}") %>.
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <!-- Internships -->
                    <div class="main-section">
                        <h2>Internships</h2>
                        <div class="entry">
                            <p class="entry-content">
                                I worked as <asp:Label ID="text2InternJobTitle" runat="server" /> 
                                at <asp:Label ID="text2InternCompany" runat="server" /> 
                                between <asp:Label ID="text2InternStartDate" runat="server" /> and 
                                <asp:Label ID="text2InternEndDate" runat="server" />. 
                                <asp:Label ID="text2InternDescription" runat="server" />
                            </p>
                        </div>
                    </div>

                    <!-- Additional Information -->
                    <div class="main-section">
                        <h2>Additional Information</h2>
                        <asp:Repeater ID="rpt2CustomSelection" runat="server">
                            <ItemTemplate>
                                <div class="entry">
                                    <p class="entry-content">
                                        I worked on <asp:Label ID="text2Title" runat="server" Text='<%# Eval("Title") %>' /> 
                                        between <%# Eval("StartDate", "{0:MMM yyyy}") %> and 
                                        <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMM yyyy}") %>. 
                                        <asp:Label ID="text2Description" runat="server" Text='<%# Eval("Description") %>' />
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <!-- Edit Mode -->
        <asp:Panel ID="pnlEdit" runat="server" Visible="false" CssClass="resume-wrapper">
            <div class="main-content" style="width: 100%;">
                <!-- Personal Details -->
                <div class="edit-section">
                    <h3>Personal Details</h3>
                    <asp:TextBox ID="txtEditJobTitle" runat="server" placeholder="Job Title" />
                    <asp:TextBox ID="txtEditFirstName" runat="server" placeholder="First Name" />
                    <asp:TextBox ID="txtEditLastName" runat="server" placeholder="Last Name" />
                    <asp:TextBox ID="txtEditEmail" runat="server" placeholder="Email" />
                    <asp:TextBox ID="txtEditPhone" runat="server" placeholder="Phone" />
                    <asp:TextBox ID="txtEditDateOfBirth" runat="server" TextMode="Date" placeholder="Date of Birth" />
                    <asp:TextBox ID="txtEditCountry" runat="server" placeholder="Country" />
                    <asp:TextBox ID="txtEditState" runat="server" placeholder="State" />
                    <asp:TextBox ID="txtEditCity" runat="server" placeholder="City" />
                    <asp:TextBox ID="txtEditAddress" runat="server" placeholder="Address" />
                    <asp:TextBox ID="txtEditPostalCode" runat="server" placeholder="Postal Code" />
                    <asp:TextBox ID="txtEditNationality" runat="server" placeholder="Nationality" />
                    <asp:TextBox ID="txtEditPlaceOfBirth" runat="server" placeholder="Place of Birth" />
                    <asp:TextBox ID="txtEditProfessionalSummary" runat="server" TextMode="MultiLine" placeholder="Professional Summary" />
                </div>

                <!-- Fresher Status -->
                <div class="checkbox-section">
                    <asp:CheckBox ID="chkFresher" runat="server" AutoPostBack="true" OnCheckedChanged="chkFresher_CheckedChanged" />
                    <label for="chkFresher">I am a Fresher (No work experience)</label>
                </div>

                <!-- Skills -->
                <div class="edit-section">
                    <h3>Skills</h3>
                    <asp:Repeater ID="rptEditSkills" runat="server" OnItemCommand="Skill_Command">
                        <ItemTemplate>
                            <div class="repeater-item">
                                <asp:TextBox ID="txtSkillName" runat="server" Text='<%# Eval("SkillName") %>' placeholder="Skill Name" />
                                <asp:Button ID="btnDeleteSkill" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' CssClass="btn-delete" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="add-section">
                        <asp:TextBox ID="txtNewSkill" runat="server" placeholder="New Skill" />
                        <asp:Button ID="btnAddSkill" runat="server" Text="Add" OnClick="btnAddSkill_Click" CssClass="btn" />
                    </div>
                </div>

                <!-- Languages -->
                <div class="edit-section">
                    <h3>Languages</h3>
                    <asp:Repeater ID="rptEditLanguages" runat="server" OnItemCommand="Language_Command">
                        <ItemTemplate>
                            <div class="repeater-item">
                                <asp:TextBox ID="txtLanguageName" runat="server" Text='<%# Eval("LanguageName") %>' placeholder="Language Name" />
                                <asp:Button ID="btnDeleteLanguage" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' CssClass="btn-delete" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="add-section">
                        <asp:TextBox ID="txtNewLanguage" runat="server" placeholder="New Language" />
                        <asp:Button ID="btnAddLanguage" runat="server" Text="Add" OnClick="btnAddLanguage_Click" CssClass="btn" />
                    </div>
                </div>

                <!-- Hobbies -->
                <div class="edit-section">
                    <h3>Hobbies</h3>
                    <asp:Repeater ID="rptEditHobbies" runat="server" OnItemCommand="Hobby_Command">
                        <ItemTemplate>
                            <div class="repeater-item">
                                <asp:TextBox ID="txtHobbyName" runat="server" Text='<%# Eval("Name") %>' placeholder="Hobby Name" />
                                <asp:Button ID="btnDeleteHobby" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' CssClass="btn-delete" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="add-section">
                        <asp:TextBox ID="txtNewHobby" runat="server" placeholder="New Hobby" />
                        <asp:Button ID="btnAddHobby" runat="server" Text="Add" OnClick="btnAddHobby_Click" CssClass="btn" />
                    </div>
                </div>

                <!-- Employment History -->
                <asp:Panel ID="pnlEditEmploymentHistory" runat="server">
                    <div class="edit-section">
                        <h3>Employment History</h3>
                        <asp:Repeater ID="rptEditEmploymentHistory" runat="server" OnItemCommand="Employment_Command">
                            <ItemTemplate>
                                <div class="repeater-item">
                                    <asp:TextBox ID="txtJobTitle" runat="server" Text='<%# Eval("JobTitle") %>' placeholder="Job Title" />
                                    <asp:TextBox ID="txtEmployer" runat="server" Text='<%# Eval("Employer") %>' placeholder="Employer" />
                                    <asp:TextBox ID="txtCity" runat="server" Text='<%# Eval("City") %>' placeholder="City" />
                                    <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' placeholder="Start Date" />
                                    <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' placeholder="End Date" />
                                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Text='<%# Eval("Description") %>' placeholder="Description" />
                                    <asp:Button ID="btnDeleteEmployment" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' CssClass="btn-delete" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="add-section">
                            <asp:TextBox ID="txtNewJobTitle" runat="server" placeholder="Job Title" />
                            <asp:TextBox ID="txtNewEmployer" runat="server" placeholder="Employer" />
                            <asp:TextBox ID="txtNewCity" runat="server" placeholder="City" />
                            <asp:TextBox ID="txtNewStartDate" runat="server" TextMode="Date" placeholder="Start Date" />
                            <asp:TextBox ID="txtNewEndDate" runat="server" TextMode="Date" placeholder="End Date" />
                            <asp:TextBox ID="txtNewDescription" runat="server" TextMode="MultiLine" placeholder="Description" />
                            <asp:Button ID="btnAddEmployment" runat="server" Text="Add" OnClick="btnAddEmployment_Click" CssClass="btn" />
                        </div>
                    </div>
                </asp:Panel>

                <!-- Education -->
                <div class="edit-section">
                    <h3>Education</h3>
                    <asp:Repeater ID="rptEditEducation" runat="server" OnItemCommand="Education_Command">
                        <ItemTemplate>
                            <div class="repeater-item">
                                <asp:TextBox ID="txtDegree" runat="server" Text='<%# Eval("Degree") %>' placeholder="Degree" />
                                <asp:TextBox ID="txtSchoolName" runat="server" Text='<%# Eval("SchoolName") %>' placeholder="School Name" />
                                <asp:TextBox ID="txtCity" runat="server" Text='<%# Eval("City") %>' placeholder="City" />
                                <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' placeholder="Start Date" />
                                <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' placeholder="End Date" />
                                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Text='<%# Eval("Description") %>' placeholder="Description" />
                                <asp:Button ID="btnDeleteEducation" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' CssClass="btn-delete" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="add-section">
                        <asp:TextBox ID="txtNewDegree" runat="server" placeholder="Degree" />
                        <asp:TextBox ID="txtNewSchoolName" runat="server" placeholder="School Name" />
                        <asp:TextBox ID="txtNewEducationCity" runat="server" placeholder="City" />
                        <asp:TextBox ID="txtNewEducationStartDate" runat="server" TextMode="Date" placeholder="Start Date" />
                        <asp:TextBox ID="txtNewEducationEndDate" runat="server" TextMode="Date" placeholder="End Date" />
                        <asp:TextBox ID="txtNewEducationDescription" runat="server" TextMode="MultiLine" placeholder="Description" />
                        <asp:Button ID="btnAddEducation" runat="server" Text="Add" OnClick="btnAddEducation_Click" CssClass="btn" />
                    </div>
                </div>

                <!-- Certifications -->
                <div class="edit-section">
                    <h3>Certifications</h3>
                    <asp:Repeater ID="rptEditCertifications" runat="server" OnItemCommand="Certification_Command">
                        <ItemTemplate>
                            <div class="repeater-item">
                                <asp:TextBox ID="txtCourseName" runat="server" Text='<%# Eval("CourseName") %>' placeholder="Course Name" />
                                <asp:TextBox ID="txtInstitution" runat="server" Text='<%# Eval("Institution") %>' placeholder="Institution" />
                                <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' placeholder="Start Date" />
                                <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' placeholder="End Date" />
                                <asp:Button ID="btnDeleteCertification" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' CssClass="btn-delete" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="add-section">
                        <asp:TextBox ID="txtNewCourseName" runat="server" placeholder="Course Name" />
                        <asp:TextBox ID="txtNewInstitution" runat="server" placeholder="Institution" />
                        <asp:TextBox ID="txtNewCertStartDate" runat="server" TextMode="Date" placeholder="Start Date" />
                        <asp:TextBox ID="txtNewCertEndDate" runat="server" TextMode="Date" placeholder="End Date" />
                        <asp:Button ID="btnAddCertification" runat="server" Text="Add" OnClick="btnAddCertification_Click" CssClass="btn" />
                    </div>
                </div>

                <!-- Internships -->
                <div class="edit-section">
                    <h3>Internships</h3>
                    <asp:TextBox ID="txtEditInternCompany" runat="server" placeholder="Company Name" />
                    <asp:TextBox ID="txtEditInternJobTitle" runat="server" placeholder="Job Title" />
                    <asp:TextBox ID="txtEditInternStartDate" runat="server" TextMode="Date" placeholder="Start Date" />
                    <asp:TextBox ID="txtEditInternEndDate" runat="server" TextMode="Date" placeholder="End Date" />
                    <asp:TextBox ID="txtEditInternDescription" runat="server" TextMode="MultiLine" placeholder="Description" />
                </div>

                <!-- Additional Information -->
                <div class="edit-section">
                    <h3>Additional Information</h3>
                    <asp:Repeater ID="rptEditCustomSelection" runat="server" OnItemCommand="CustomSelection_Command">
                        <ItemTemplate>
                            <div class="repeater-item">
                                <asp:TextBox ID="txtTitle" runat="server" Text='<%# Eval("Title") %>' placeholder="Title" />
                                <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' placeholder="Start Date" />
                                <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' placeholder="End Date" />
                                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Text='<%# Eval("Description") %>' placeholder="Description" />
                                <asp:Button ID="btnDeleteCustom" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' CssClass="btn-delete" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="add-section">
                        <asp:TextBox ID="txtNewCustomTitle" runat="server" placeholder="Title" />
                        <asp:TextBox ID="txtNewCustomStartDate" runat="server" TextMode="Date" placeholder="Start Date" />
                        <asp:TextBox ID="txtNewCustomEndDate" runat="server" TextMode="Date" placeholder="End Date" />
                        <asp:TextBox ID="txtNewCustomDescription" runat="server" TextMode="MultiLine" placeholder="Description" />
                        <asp:Button ID="btnAddCustom" runat="server" Text="Add" OnClick="btnAddCustom_Click" CssClass="btn" />
                    </div>
                </div>

                <!-- Action Buttons -->
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-back" />
            </div>
        </asp:Panel>

        <!-- Action Buttons for View Mode -->
        <asp:Button ID="btnEdit" runat="server" Text="Edit Resume" OnClick="btnEdit_Click" CssClass="btn" />
        <asp:Button ID="btnDownload" runat="server" Text="Download PDF" OnClick="btnDownload_Click" CssClass="btn" />
        <asp:Button ID="btnBack" runat="server" Text="Back to Home" OnClick="btnBack_Click" CssClass="btn btn-back" />
    </form>
</body>
</html>