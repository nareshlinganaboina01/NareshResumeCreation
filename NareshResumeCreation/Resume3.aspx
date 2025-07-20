<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Resume3.aspx.cs" Inherits="NareshResumeCreation.Resume3" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Professional Resume</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style type="text/css">
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            margin: 0;
            padding: 0;
            background: #f4f4f9;
            color: #333;
            line-height: 1.5;
        }
        .resume-container {
            max-width: 1000px;
            margin: 40px auto;
            background: #fff;
            border-radius: 8px;
            box-shadow: 0 6px 20px rgba(0, 0, 0, 0.1);
            overflow: hidden;
        }
        .header {
            background: #1a73e8;
            color: #fff;
            padding: 40px 30px;
            text-align: center;
        }
        .header h1 {
            font-size: 32px;
            margin: 0;
            font-weight: 600;
        }
        .header .job-title {
            font-size: 18px;
            font-weight: 300;
            margin-top: 8px;
        }
        .content-wrapper {
            display: flex;
            flex-direction: row;
            padding: 30px;
        }
        .left-column {
            width: 35%;
            padding-right: 20px;
            border-right: 1px solid #e0e0e0;
        }
        .right-column {
            width: 65%;
            padding-left: 20px;
        }
        .section {
            margin-bottom: 25px;
        }
        .section h3 {
            font-size: 18px;
            color: #1a73e8;
            margin-bottom: 12px;
            border-bottom: 2px solid #1a73e8;
            padding-bottom: 5px;
            text-transform: uppercase;
        }
        .section p {
            margin: 5px 0;
            font-size: 14px;
        }
        .list {
            list-style: none;
            padding: 0;
        }
        .list li {
            background: #e8f0fe;
            padding: 8px 12px;
            margin: 6px 0;
            border-radius: 4px;
            font-size: 14px;
            transition: background 0.3s;
        }
        .list li:hover {
            background: #d1e0ff;
        }
        .entry {
            margin-bottom: 20px;
            padding: 15px;
            background: #fafafa;
            border-radius: 6px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.05);
        }
        .entry p {
            margin: 0;
            font-size: 14px;
            color: #555;
        }
        .edit-input, .edit-textarea {
            width: 100%;
            padding: 5px;
            font-size: 14px;
            border: 1px solid #ccc;
            border-radius: 4px;
            margin: 5px 0;
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
            text-align: center;
            margin: 30px 0;
        }
        .btn {
            background: #1a73e8;
            color: #fff;
            padding: 12px 25px;
            border: none;
            border-radius: 6px;
            cursor: pointer;
            font-size: 16px;
            transition: background 0.3s, transform 0.2s;
            margin: 0 10px;
        }
        .btn:hover {
            background: #1557b0;
            transform: translateY(-2px);
        }
        @media (max-width: 768px) {
            .content-wrapper {
                flex-direction: column;
                padding: 20px;
            }
            .left-column, .right-column {
                width: 100%;
                padding: 0;
                border-right: none;
            }
            .left-column {
                margin-bottom: 20px;
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
                <!-- Header Section -->
                <div class="header">
                    <h1><asp:Label ID="text3FirstName" runat="server" /> <asp:Label ID="text3LastName" runat="server" /></h1>
                    <p class="job-title"><asp:Label ID="text3JobTitle" runat="server" /></p>
                </div>

                <!-- Content Wrapper -->
                <div class="content-wrapper">
                    <!-- Left Column -->
                    <div class="left-column">
                        <!-- Contact Information -->
                        <div class="section">
                            <h3>Contact</h3>
                            <p>✉️ <asp:Label ID="text3Email" runat="server" /></p>
                            <p>📱 <asp:Label ID="text3Phone" runat="server" /></p>
                            <p>🏠 <asp:Label ID="text3Address" runat="server" />, 
                               <asp:Label ID="text3City" runat="server" />, 
                               <asp:Label ID="text3State" runat="server" />, 
                               <asp:Label ID="text3PostalCode" runat="server" />, 
                               <asp:Label ID="text3Country" runat="server" /></p>
                        </div>

                        <!-- Personal Information -->
                        <div class="section">
                            <h3>Personal Info</h3>
                            <p>🎂 <asp:Label ID="text3DateOfBirth" runat="server" /></p>
                            <p>🌍 <asp:Label ID="text3PlaceOfBirth" runat="server" /></p>
                            <p>📍 <asp:Label ID="text3Nationality" runat="server" /></p>
                        </div>

                        <!-- Skills -->
                        <div class="section">
                            <h3>Skills</h3>
                            <ul class="list">
                                <asp:Repeater ID="rpt3Skills" runat="server">
                                    <ItemTemplate>
                                        <li><asp:Literal ID="lit3Skill" runat="server" Text='<%# Eval("SkillName") %>' /></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>

                        <!-- Languages -->
                        <div class="section">
                            <h3>Languages</h3>
                            <ul class="list">
                                <asp:Repeater ID="rpt3Languages" runat="server">
                                    <ItemTemplate>
                                        <li><asp:Literal ID="lit3Language" runat="server" Text='<%# Eval("LanguageName") %>' /></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>

                        <!-- Hobbies -->
                        <div class="section">
                            <h3>Hobbies</h3>
                            <ul class="list">
                                <asp:Repeater ID="rpt3Hobbies" runat="server">
                                    <ItemTemplate>
                                        <li><asp:Literal ID="lit3Hobby" runat="server" Text='<%# Eval("Name") %>' /></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>

                    <!-- Right Column -->
                    <div class="right-column">
                        <!-- Professional Summary -->
                        <div class="section">
                            <h3>Professional Summary</h3>
                            <div class="entry">
                                <p><asp:Label ID="text3ProfessionalSummary" runat="server" /></p>
                            </div>
                        </div>

                        <!-- Employment History -->
                        <div class="section">
                            <h3>Employment History</h3>
                            <asp:Panel ID="pnl3Fresher" runat="server" Visible="false">
                                <div class="entry">
                                    <p>Fresher (No work experience)</p>
                                </div>
                            </asp:Panel>
                            <asp:Panel ID="pnl3EmploymentHistory" runat="server" Visible="false">
                                <asp:Repeater ID="rpt3EmploymentHistory" runat="server">
                                    <ItemTemplate>
                                        <div class="entry">
                                            <p>
                                                I worked as <asp:Label ID="text3JobTitle" runat="server" Text='<%# Eval("JobTitle") %>' /> 
                                                at <asp:Label ID="text3Employer" runat="server" Text='<%# Eval("Employer") %>' />, 
                                                <asp:Label ID="text3City" runat="server" Text='<%# Eval("City") %>' /> 
                                                between <%# Eval("StartDate", "{0:MMM yyyy}") %> and 
                                                <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMM yyyy}") %>. 
                                                <asp:Label ID="text3Description" runat="server" Text='<%# Eval("Description") %>' />
                                            </p>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                        </div>

                        <!-- Education -->
                        <div class="section">
                            <h3>Education</h3>
                            <asp:Repeater ID="rpt3Education" runat="server">
                                <ItemTemplate>
                                    <div class="entry">
                                        <p>
                                            I completed my <asp:Label ID="text3Degree" runat="server" Text='<%# Eval("Degree") %>' /> 
                                            from <asp:Label ID="text3SchoolName" runat="server" Text='<%# Eval("SchoolName") %>' />, 
                                            <asp:Label ID="text3City" runat="server" Text='<%# Eval("City") %>' /> 
                                            between <%# Eval("StartDate", "{0:MMM yyyy}") %> and 
                                            <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMM yyyy}") %>. 
                                            <asp:Label ID="text3Description" runat="server" Text='<%# Eval("Description") %>' />
                                        </p>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>

                        <!-- Certifications -->
                        <div class="section">
                            <h3>Certifications</h3>
                            <asp:Repeater ID="rpt3Certifications" runat="server">
                                <ItemTemplate>
                                    <div class="entry">
                                        <p>
                                            I completed <asp:Label ID="text3CourseName" runat="server" Text='<%# Eval("CourseName") %>' /> 
                                            from <asp:Label ID="text3Institution" runat="server" Text='<%# Eval("Institution") %>' /> 
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
                                    I worked as <asp:Label ID="text3InternJobTitle" runat="server" /> 
                                    at <asp:Label ID="text3InternCompany" runat="server" /> 
                                    between <asp:Label ID="text3InternStartDate" runat="server" /> and 
                                    <asp:Label ID="text3InternEndDate" runat="server" />. 
                                    <asp:Label ID="text3InternDescription" runat="server" />
                                </p>
                            </div>
                        </div>

                        <!-- Additional Information -->
                        <div class="section">
                            <h3>Additional Information</h3>
                            <asp:Repeater ID="rpt3CustomSelection" runat="server">
                                <ItemTemplate>
                                    <div class="entry">
                                        <p>
                                            I worked on <asp:Label ID="text3Title" runat="server" Text='<%# Eval("Title") %>' /> 
                                            between <%# Eval("StartDate", "{0:MMM yyyy}") %> and 
                                            <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMM yyyy}") %>. 
                                            <asp:Label ID="text3Description" runat="server" Text='<%# Eval("Description") %>' />
                                        </p>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlEdit" runat="server" Visible="false">
                <!-- Header Section -->
                <div class="header">
                    <asp:TextBox ID="txtEditFirstName" runat="server" CssClass="edit-input" />
                    <asp:TextBox ID="txtEditLastName" runat="server" CssClass="edit-input" />
                    <asp:TextBox ID="txtEditJobTitle" runat="server" CssClass="edit-input" Placeholder="Job Title" />
                </div>

                <!-- Content Wrapper -->
                <div class="content-wrapper">
                    <!-- Left Column -->
                    <div class="left-column">
                        <!-- Contact Information -->
                        <div class="section">
                            <h3>Contact</h3>
                            <p>✉️ <asp:TextBox ID="txtEditEmail" runat="server" CssClass="edit-input" /></p>
                            <p>📱 <asp:TextBox ID="txtEditPhone" runat="server" CssClass="edit-input" /></p>
                            <p>🏠 <asp:TextBox ID="txtEditAddress" runat="server" CssClass="edit-input" />, 
                               <asp:TextBox ID="txtEditCity" runat="server" CssClass="edit-input" />, 
                               <asp:TextBox ID="txtEditState" runat="server" CssClass="edit-input" />, 
                               <asp:TextBox ID="txtEditPostalCode" runat="server" CssClass="edit-input" />, 
                               <asp:TextBox ID="txtEditCountry" runat="server" CssClass="edit-input" /></p>
                        </div>

                        <!-- Personal Information -->
                        <div class="section">
                            <h3>Personal Info</h3>
                            <p>🎂 <asp:TextBox ID="txtEditDateOfBirth" runat="server" CssClass="edit-input" TextMode="Date" /></p>
                            <p>🌍 <asp:TextBox ID="txtEditPlaceOfBirth" runat="server" CssClass="edit-input" /></p>
                            <p>📍 <asp:TextBox ID="txtEditNationality" runat="server" CssClass="edit-input" /></p>
                        </div>

                        <!-- Skills -->
                        <div class="section">
                            <h3>Skills</h3>
                            <asp:Repeater ID="rptEditSkills" runat="server">
                                <ItemTemplate>
                                    <div class="edit-list-item">
                                        <asp:TextBox ID="txtSkillName" runat="server" Text='<%# Eval("SkillName") %>' CssClass="edit-input" />
                                        <asp:Button ID="btnDeleteSkill" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Skill_Command" CssClass="btn" style="background: #dc3545;" />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:TextBox ID="txtNewSkill" runat="server" CssClass="edit-input" Placeholder="Add new skill" />
                            <asp:Button ID="btnAddSkill" runat="server" Text="Add Skill" OnClick="btnAddSkill_Click" CssClass="btn" />
                        </div>

                        <!-- Languages -->
                        <div class="section">
                            <h3>Languages</h3>
                            <asp:Repeater ID="rptEditLanguages" runat="server">
                                <ItemTemplate>
                                    <div class="edit-list-item">
                                        <asp:TextBox ID="txtLanguageName" runat="server" Text='<%# Eval("LanguageName") %>' CssClass="edit-input" />
                                        <asp:Button ID="btnDeleteLanguage" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Language_Command" CssClass="btn" style="background: #dc3545;" />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:TextBox ID="txtNewLanguage" runat="server" CssClass="edit-input" Placeholder="Add new language" />
                            <asp:Button ID="btnAddLanguage" runat="server" Text="Add Language" OnClick="btnAddLanguage_Click" CssClass="btn" />
                        </div>

                        <!-- Hobbies -->
                        <div class="section">
                            <h3>Hobbies</h3>
                            <asp:Repeater ID="rptEditHobbies" runat="server">
                                <ItemTemplate>
                                    <div class="edit-list-item">
                                        <asp:TextBox ID="txtHobbyName" runat="server" Text='<%# Eval("Name") %>' CssClass="edit-input" />
                                        <asp:Button ID="btnDeleteHobby" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Hobby_Command" CssClass="btn" style="background: #dc3545;" />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:TextBox ID="txtNewHobby" runat="server" CssClass="edit-input" Placeholder="Add new hobby" />
                            <asp:Button ID="btnAddHobby" runat="server" Text="Add Hobby" OnClick="btnAddHobby_Click" CssClass="btn" />
                        </div>
                    </div>

                    <!-- Right Column -->
                    <div class="right-column">
                        <!-- Professional Summary -->
                        <div class="section">
                            <h3>Professional Summary</h3>
                            <div class="entry">
                                <asp:TextBox ID="txtEditProfessionalSummary" runat="server" CssClass="edit-textarea" TextMode="MultiLine" />
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
                                                <asp:Button ID="btnDeleteEmployment" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Employment_Command" CssClass="btn" style="background: #dc3545;" />
                                            </p>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div class="entry">
                                    <p>
                                        Job Title: <asp:TextBox ID="txtNewJobTitle" runat="server" CssClass="edit-input" /><br />
                                        Employer: <asp:TextBox ID="txtNewEmployer" runat="server" CssClass="edit-input" /><br />
                                        City: <asp:TextBox ID="txtNewCity" runat="server" CssClass="edit-input" /><br />
                                        Start Date: <asp:TextBox ID="txtNewStartDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                        End Date: <asp:TextBox ID="txtNewEndDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                        Description: <asp:TextBox ID="txtNewDescription" runat="server" CssClass="edit-textarea" TextMode="MultiLine" /><br />
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
                                            <asp:Button ID="btnDeleteEducation" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Education_Command" CssClass="btn" style="background: #dc3545;" />
                                        </p>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="entry">
                                <p>
                                    Degree: <asp:TextBox ID="txtNewDegree" runat="server" CssClass="edit-input" /><br />
                                    School: <asp:TextBox ID="txtNewSchoolName" runat="server" CssClass="edit-input" /><br />
                                    City: <asp:TextBox ID="txtNewEducationCity" runat="server" CssClass="edit-input" /><br />
                                    Start Date: <asp:TextBox ID="txtNewEducationStartDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                    End Date: <asp:TextBox ID="txtNewEducationEndDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                    Description: <asp:TextBox ID="txtNewEducationDescription" runat="server" CssClass="edit-textarea" TextMode="MultiLine" /><br />
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
                                            <asp:Button ID="btnDeleteCertification" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Certification_Command" CssClass="btn" style="background: #dc3545;" />
                                        </p>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="entry">
                                <p>
                                    Course: <asp:TextBox ID="txtNewCourseName" runat="server" CssClass="edit-input" /><br />
                                    Institution: <asp:TextBox ID="txtNewInstitution" runat="server" CssClass="edit-input" /><br />
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
                                    Job Title: <asp:TextBox ID="txtEditInternJobTitle" runat="server" CssClass="edit-input" /><br />
                                    Company: <asp:TextBox ID="txtEditInternCompany" runat="server" CssClass="edit-input" /><br />
                                    Start Date: <asp:TextBox ID="txtEditInternStartDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                    End Date: <asp:TextBox ID="txtEditInternEndDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                    Description: <asp:TextBox ID="txtEditInternDescription" runat="server" CssClass="edit-textarea" TextMode="MultiLine" />
                                </p>
                            </div>
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
                                            <asp:Button ID="btnDeleteCustom" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="CustomSelection_Command" CssClass="btn" style="background: #dc3545;" />
                                        </p>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="entry">
                                <p>
                                    Title: <asp:TextBox ID="txtNewCustomTitle" runat="server" CssClass="edit-input" /><br />
                                    Start Date: <asp:TextBox ID="txtNewCustomStartDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                    End Date: <asp:TextBox ID="txtNewCustomEndDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                    Description: <asp:TextBox ID="txtNewCustomDescription" runat="server" CssClass="edit-textarea" TextMode="MultiLine" /><br />
                                    <asp:Button ID="btnAddCustom" runat="server" Text="Add Custom Section" OnClick="btnAddCustom_Click" CssClass="btn" />
                                </p>
                            </div>
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