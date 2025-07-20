<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Resume9.aspx.cs" Inherits="NareshResumeCreation.Resume9" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Single-Column Resume</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style type="text/css">
        body {
            font-family: Arial, sans-serif;
            margin: 25px;
            line-height: 1.5;
        }
        .resume-wrapper {
            max-width: 800px;
            margin: 0 auto;
        }
        .header-section {
            text-align: center;
            margin-bottom: 20px;
        }
        .header-section h1 {
            font-size: 24px;
            font-weight: bold;
            margin: 0 0 5px 0;
        }
        .header-section p {
            font-size: 12px;
            margin: 5px 0;
        }
        .section {
            margin-bottom: 15px;
        }
        .section h2 {
            font-size: 16px;
            font-weight: bold;
            margin-bottom: 5px;
            color: #333;
        }
        .section p {
            font-size: 12px;
            margin: 5px 0;
        }
        .list {
            list-style-type: disc;
            padding-left: 20px;
            margin: 5px 0;
        }
        .list li {
            font-size: 12px;
            margin: 5px 0;
        }
        hr {
            border: 0;
            border-top: 1px solid #ccc;
            margin: 15px 0;
        }
        .button {
            padding: 10px 20px;
            font-size: 14px;
            cursor: pointer;
            margin: 10px 5px;
            background-color: #007bff;
            color: white;
            border: none;
            border-radius: 5px;
            display: inline-block;
        }
        .button:hover {
            background-color: #0056b3;
        }
        .edit-input {
            width: 100%;
            font-size: 12px;
            padding: 5px;
            margin: 5px 0;
            border: 1px solid #ccc;
            border-radius: 4px;
            box-sizing: border-box;
        }
        .edit-list-input {
            width: 80%;
            font-size: 12px;
            padding: 3px;
            margin: 3px 0;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
        .add-button {
            font-size: 12px;
            padding: 3px 6px;
            margin-left: 5px;
            cursor: pointer;
            background-color: #f0f0f0;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
        .add-button:hover {
            background-color: #e0e0e0;
        }
        .edit-section {
            margin-bottom: 20px;
        }
        .edit-item {
            margin-bottom: 10px;
            border-bottom: 1px solid #eee;
            padding-bottom: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="resume-wrapper">
            <!-- Header -->
            <div class="header-section">
                <asp:Panel ID="pnlViewPersonal" runat="server" Visible="true">
                    <h1><asp:Label ID="text9FirstName" runat="server" /> <asp:Label ID="text9LastName" runat="server" /></h1>
                    <p><asp:Label ID="text9JobTitle" runat="server" /></p>
                    <p>Email: <asp:Label ID="text9Email" runat="server" /> | Phone: <asp:Label ID="text9Phone" runat="server" /></p>
                    <p><asp:Label ID="text9Address" runat="server" />, <asp:Label ID="text9City" runat="server" />,<asp:Label ID="text9State" runat="server" />, <asp:Label ID="text9PostalCode" runat="server" />, <asp:Label ID="text9Country" runat="server" /></p>
                    <p>Date of Birth: <asp:Label ID="text9DateOfBirth" runat="server" /> | Place of Birth: <asp:Label ID="text9PlaceOfBirth" runat="server" /> | Nationality: <asp:Label ID="text9Nationality" runat="server" /></p>
                    <p>LinkedIn: <asp:HyperLink ID="lnkLinkedIn" runat="server" Target="_blank" /> | GitHub: <asp:HyperLink ID="lnkGitHub" runat="server" Target="_blank" /></p>
                </asp:Panel>
                <asp:Panel ID="pnlEditPersonal" runat="server" Visible="false">
                    <h2>Personal Details</h2>
                    <asp:TextBox ID="txtEditFirstName" runat="server" CssClass="edit-input" Placeholder="First Name" />
                    <asp:TextBox ID="txtEditLastName" runat="server" CssClass="edit-input" Placeholder="Last Name" />
                    <asp:TextBox ID="txtEditJobTitle" runat="server" CssClass="edit-input" Placeholder="Job Title" />
                    <asp:TextBox ID="txtEditEmail" runat="server" CssClass="edit-input" Placeholder="Email" />
                    <asp:TextBox ID="txtEditPhone" runat="server" CssClass="edit-input" Placeholder="Phone" />
                    <asp:TextBox ID="txtEditAddress" runat="server" CssClass="edit-input" Placeholder="Address" />
                    <asp:TextBox ID="txtEditCity" runat="server" CssClass="edit-input" Placeholder="City" />
                    <asp:TextBox ID="txtEditState" runat="server" CssClass="edit-input" Placeholder="State" />
                    <asp:TextBox ID="txtEditPostalCode" runat="server" CssClass="edit-input" Placeholder="Postal Code" />
                    <asp:TextBox ID="txtEditCountry" runat="server" CssClass="edit-input" Placeholder="Country" />
                    <asp:TextBox ID="txtEditDateOfBirth" runat="server" CssClass="edit-input" Placeholder="Date of Birth (yyyy-MM-dd)" />
                    <asp:TextBox ID="txtEditPlaceOfBirth" runat="server" CssClass="edit-input" Placeholder="Place of Birth" />
                    <asp:TextBox ID="txtEditNationality" runat="server" CssClass="edit-input" Placeholder="Nationality" />
                    <asp:TextBox ID="txtEditLinkedIn" runat="server" CssClass="edit-input" Placeholder="LinkedIn URL" />
                    <asp:TextBox ID="txtEditGitHub" runat="server" CssClass="edit-input" Placeholder="GitHub URL" />
                </asp:Panel>
            </div>
            <hr />

            <!-- Professional Summary -->
            <div class="section">
                <h2>Professional Summary</h2>
                <asp:Panel ID="pnlViewSummary" runat="server" Visible="true">
                    <p><asp:Label ID="text9ProfessionalSummary" runat="server" /></p>
                </asp:Panel>
                <asp:Panel ID="pnlEditSummary" runat="server" Visible="false">
                    <asp:TextBox ID="txtEditProfessionalSummary" runat="server" TextMode="MultiLine" Rows="4" CssClass="edit-input" Placeholder="Professional Summary" />
                </asp:Panel>
            </div>
            <hr />

            <!-- Skills -->
            <div class="section">
                <h2>Skills</h2>
                <asp:Panel ID="pnlViewSkills" runat="server" Visible="true">
                    <ul class="list">
                        <asp:Repeater ID="rpt9Skills" runat="server">
                            <ItemTemplate>
                                <li><asp:Literal ID="lit9Skill" runat="server" Text='<%# Eval("SkillName") %>' /></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </asp:Panel>
                <asp:Panel ID="pnlEditSkills" runat="server" Visible="false">
                    <asp:Repeater ID="rptEditSkills" runat="server" OnItemCommand="rptEditSkills_ItemCommand">
                        <ItemTemplate>
                            <div>
                                <asp:TextBox ID="txtSkillName" runat="server" Text='<%# Eval("SkillName") %>' CssClass="edit-list-input" />
                                <asp:HiddenField ID="hdnSkillId" runat="server" Value='<%# Eval("ID") %>' />
                                <asp:Button ID="btnDeleteSkill" runat="server" Text="X" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' CssClass="add-button" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:TextBox ID="txtNewSkill" runat="server" CssClass="edit-list-input" Placeholder="New Skill" />
                    <asp:Button ID="btnAddSkill" runat="server" Text="Add" OnClick="btnAddSkill_Click" CssClass="add-button" />
                </asp:Panel>
            </div>
            <hr />

            <!-- Languages -->
            <div class="section">
                <h2>Languages</h2>
                <asp:Panel ID="pnlViewLanguages" runat="server" Visible="true">
                    <ul class="list">
                        <asp:Repeater ID="rpt9Languages" runat="server">
                            <ItemTemplate>
                                <li><asp:Literal ID="lit9Language" runat="server" Text='<%# Eval("LanguageName") %>' /></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </asp:Panel>
                <asp:Panel ID="pnlEditLanguages" runat="server" Visible="false">
                    <asp:Repeater ID="rptEditLanguages" runat="server" OnItemCommand="rptEditLanguages_ItemCommand">
                        <ItemTemplate>
                            <div>
                                <asp:TextBox ID="txtLanguageName" runat="server" Text='<%# Eval("LanguageName") %>' CssClass="edit-list-input" />
                                <asp:HiddenField ID="hdnLanguageId" runat="server" Value='<%# Eval("ID") %>' />
                                <asp:Button ID="btnDeleteLanguage" runat="server" Text="X" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' CssClass="add-button" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:TextBox ID="txtNewLanguage" runat="server" CssClass="edit-list-input" Placeholder="New Language" />
                    <asp:Button ID="btnAddLanguage" runat="server" Text="Add" OnClick="btnAddLanguage_Click" CssClass="add-button" />
                </asp:Panel>
            </div>
            <hr />

            <!-- Hobbies -->
            <div class="section">
                <h2>Hobbies</h2>
                <asp:Panel ID="pnlViewHobbies" runat="server" Visible="true">
                    <ul class="list">
                        <asp:Repeater ID="rpt9Hobbies" runat="server">
                            <ItemTemplate>
                                <li><asp:Literal ID="lit9Hobby" runat="server" Text='<%# Eval("Name") %>' /></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </asp:Panel>
                <asp:Panel ID="pnlEditHobbies" runat="server" Visible="false">
                    <asp:Repeater ID="rptEditHobbies" runat="server" OnItemCommand="rptEditHobbies_ItemCommand">
                        <ItemTemplate>
                            <div>
                                <asp:TextBox ID="txtHobbyName" runat="server" Text='<%# Eval("Name") %>' CssClass="edit-list-input" />
                                <asp:HiddenField ID="hdnHobbyId" runat="server" Value='<%# Eval("ID") %>' />
                                <asp:Button ID="btnDeleteHobby" runat="server" Text="X" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' CssClass="add-button" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:TextBox ID="txtNewHobby" runat="server" CssClass="edit-list-input" Placeholder="New Hobby" />
                    <asp:Button ID="btnAddHobby" runat="server" Text="Add" OnClick="btnAddHobby_Click" CssClass="add-button" />
                </asp:Panel>
            </div>
            <hr />

            <!-- Employment History -->
            <div class="section">
                <h2>Employment History</h2>
                <asp:Panel ID="pnlViewEmployment" runat="server" Visible="true">
                    <asp:Panel ID="pnl9Fresher" runat="server" Visible="false">
                        <p>I am a fresher with no work experience.</p>
                    </asp:Panel>
                    <asp:Panel ID="pnl9EmploymentHistory" runat="server" Visible="false">
                        <asp:Repeater ID="rpt9EmploymentHistory" runat="server">
                            <ItemTemplate>
                                <p>
                                    I worked as <asp:Label ID="text9JobTitle" runat="server" Text='<%# Eval("JobTitle") %>' /> 
                                    at <asp:Label ID="text9Employer" runat="server" Text='<%# Eval("Employer") %>' /> 
                                    between <%# Eval("StartDate", "{0:MMM yyyy}") %> and 
                                    <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMM yyyy}") %>. 
                                    <asp:Label ID="text9Description" runat="server" Text='<%# Eval("Description") %>' />
                                </p>
                            </ItemTemplate>
                        </asp:Repeater>
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel ID="pnlEditEmployment" runat="server" Visible="false">
                    <asp:CheckBox ID="chkIsFresher" runat="server" Text="I am a fresher with no work experience" AutoPostBack="true" OnCheckedChanged="chkIsFresher_CheckedChanged" />
                    <asp:Panel ID="pnlEditEmploymentHistory" runat="server">
                        <asp:Repeater ID="rptEditEmploymentHistory" runat="server" OnItemCommand="rptEditEmploymentHistory_ItemCommand">
                            <ItemTemplate>
                                <div class="edit-item">
                                    <asp:TextBox ID="txtJobTitle" runat="server" Text='<%# Eval("JobTitle") %>' CssClass="edit-input" Placeholder="Job Title" />
                                    <asp:TextBox ID="txtEmployer" runat="server" Text='<%# Eval("Employer") %>' CssClass="edit-input" Placeholder="Employer" />
                                    <asp:TextBox ID="txtCity" runat="server" Text='<%# Eval("City") %>' CssClass="edit-input" Placeholder="City" />
                                    <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" Placeholder="Start Date (yyyy-MM-dd)" />
                                    <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" Placeholder="End Date (yyyy-MM-dd) or leave blank for Present" />
                                    <asp:TextBox ID="txtDescription" runat="server" Text='<%# Eval("Description") %>' TextMode="MultiLine" Rows="3" CssClass="edit-input" Placeholder="Description" />
                                    <asp:HiddenField ID="hdnEmploymentId" runat="server" Value='<%# Eval("ID") %>' />
                                    <asp:Button ID="btnDeleteEmployment" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' CssClass="add-button" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <h3>Add New Employment</h3>
                        <asp:TextBox ID="txtNewJobTitle" runat="server" CssClass="edit-input" Placeholder="Job Title" />
                        <asp:TextBox ID="txtNewEmployer" runat="server" CssClass="edit-input" Placeholder="Employer" />
                        <asp:TextBox ID="txtNewEmploymentCity" runat="server" CssClass="edit-input" Placeholder="City" />
                        <asp:TextBox ID="txtNewEmploymentStartDate" runat="server" CssClass="edit-input" Placeholder="Start Date (yyyy-MM-dd)" />
                        <asp:TextBox ID="txtNewEmploymentEndDate" runat="server" CssClass="edit-input" Placeholder="End Date (yyyy-MM-dd) or leave blank for Present" />
                        <asp:TextBox ID="txtNewEmploymentDescription" runat="server" TextMode="MultiLine" Rows="3" CssClass="edit-input" Placeholder="Description" />
                        <asp:Button ID="btnAddEmployment" runat="server" Text="Add Employment" OnClick="btnAddEmployment_Click" CssClass="add-button" />
                    </asp:Panel>
                </asp:Panel>
            </div>
            <hr />

            <!-- Education -->
            <div class="section">
                <h2>Education</h2>
                <asp:Panel ID="pnlViewEducation" runat="server" Visible="true">
                    <asp:Repeater ID="rpt9Education" runat="server">
                        <ItemTemplate>
                            <p>
                                I completed my <asp:Label ID="text9Degree" runat="server" Text='<%# Eval("Degree") %>' /> 
                                from <asp:Label ID="text9SchoolName" runat="server" Text='<%# Eval("SchoolName") %>' />, 
                                <asp:Label ID="text9City" runat="server" Text='<%# Eval("City") %>' /> 
                                between <%# Eval("StartDate", "{0:MMM yyyy}") %> and 
                                <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMM yyyy}") %>. 
                                <asp:Label ID="text9Description" runat="server" Text='<%# Eval("Description") %>' />
                            </p>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>
                <asp:Panel ID="pnlEditEducation" runat="server" Visible="false">
                    <asp:Repeater ID="rptEditEducation" runat="server" OnItemCommand="rptEditEducation_ItemCommand">
                        <ItemTemplate>
                            <div class="edit-item">
                                <asp:TextBox ID="txtDegree" runat="server" Text='<%# Eval("Degree") %>' CssClass="edit-input" Placeholder="Degree" />
                                <asp:TextBox ID="txtSchoolName" runat="server" Text='<%# Eval("SchoolName") %>' CssClass="edit-input" Placeholder="School Name" />
                                <asp:TextBox ID="txtCity" runat="server" Text='<%# Eval("City") %>' CssClass="edit-input" Placeholder="City" />
                                <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" Placeholder="Start Date (yyyy-MM-dd)" />
                                <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" Placeholder="End Date (yyyy-MM-dd) or leave blank for Present" />
                                <asp:TextBox ID="txtDescription" runat="server" Text='<%# Eval("Description") %>' TextMode="MultiLine" Rows="3" CssClass="edit-input" Placeholder="Description" />
                                <asp:HiddenField ID="hdnEducationId" runat="server" Value='<%# Eval("ID") %>' />
                                <asp:Button ID="btnDeleteEducation" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' CssClass="add-button" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <h3>Add New Education</h3>
                    <asp:TextBox ID="txtNewDegree" runat="server" CssClass="edit-input" Placeholder="Degree" />
                    <asp:TextBox ID="txtNewSchoolName" runat="server" CssClass="edit-input" Placeholder="School Name" />
                    <asp:TextBox ID="txtNewEducationCity" runat="server" CssClass="edit-input" Placeholder="City" />
                    <asp:TextBox ID="txtNewEducationStartDate" runat="server" CssClass="edit-input" Placeholder="Start Date (yyyy-MM-dd)" />
                    <asp:TextBox ID="txtNewEducationEndDate" runat="server" CssClass="edit-input" Placeholder="End Date (yyyy-MM-dd) or leave blank for Present" />
                    <asp:TextBox ID="txtNewEducationDescription" runat="server" TextMode="MultiLine" Rows="3" CssClass="edit-input" Placeholder="Description" />
                    <asp:Button ID="btnAddEducation" runat="server" Text="Add Education" OnClick="btnAddEducation_Click" CssClass="add-button" />
                </asp:Panel>
            </div>
            <hr />

            <!-- Certifications -->
            <div class="section">
                <h2>Certifications</h2>
                <asp:Panel ID="pnlViewCertifications" runat="server" Visible="true">
                    <asp:Repeater ID="rpt9Certifications" runat="server">
                        <ItemTemplate>
                            <p>
                                I completed <asp:Label ID="text9CourseName" runat="server" Text='<%# Eval("CourseName") %>' /> 
                                from <asp:Label ID="text9Institution" runat="server" Text='<%# Eval("Institution") %>' /> 
                                between <%# Eval("StartDate", "{0:MMM yyyy}") %> and 
                                <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMM yyyy}") %>.
                            </p>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>
                <asp:Panel ID="pnlEditCertifications" runat="server" Visible="false">
                    <asp:Repeater ID="rptEditCertifications" runat="server" OnItemCommand="rptEditCertifications_ItemCommand">
                        <ItemTemplate>
                            <div class="edit-item">
                                <asp:TextBox ID="txtCourseName" runat="server" Text='<%# Eval("CourseName") %>' CssClass="edit-input" Placeholder="Course Name" />
                                <asp:TextBox ID="txtInstitution" runat="server" Text='<%# Eval("Institution") %>' CssClass="edit-input" Placeholder="Institution" />
                                <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" Placeholder="Start Date (yyyy-MM-dd)" />
                                <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" Placeholder="End Date (yyyy-MM-dd) or leave blank for Present" />
                                <asp:HiddenField ID="hdnCertificationId" runat="server" Value='<%# Eval("ID") %>' />
                                <asp:Button ID="btnDeleteCertification" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' CssClass="add-button" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <h3>Add New Certification</h3>
                    <asp:TextBox ID="txtNewCourseName" runat="server" CssClass="edit-input" Placeholder="Course Name" />
                    <asp:TextBox ID="txtNewInstitution" runat="server" CssClass="edit-input" Placeholder="Institution" />
                    <asp:TextBox ID="txtNewCertificationStartDate" runat="server" CssClass="edit-input" Placeholder="Start Date (yyyy-MM-dd)" />
                    <asp:TextBox ID="txtNewCertificationEndDate" runat="server" CssClass="edit-input" Placeholder="End Date (yyyy-MM-dd) or leave blank for Present" />
                    <asp:Button ID="btnAddCertification" runat="server" Text="Add Certification" OnClick="btnAddCertification_Click" CssClass="add-button" />
                </asp:Panel>
            </div>
            <hr />

            <!-- Internships -->
            <div class="section">
                <h2>Internships</h2>
                <asp:Panel ID="pnlViewInternship" runat="server" Visible="true">
                    <p>
                        I worked as <asp:Label ID="text9InternJobTitle" runat="server" /> 
                        at <asp:Label ID="text9InternCompany" runat="server" /> 
                        between <asp:Label ID="text9InternStartDate" runat="server" /> and 
                        <asp:Label ID="text9InternEndDate" runat="server" />. 
                        <asp:Label ID="text9InternDescription" runat="server" />
                    </p>
                </asp:Panel>
                <asp:Panel ID="pnlEditInternship" runat="server" Visible="false">
                    <asp:TextBox ID="txtEditInternJobTitle" runat="server" CssClass="edit-input" Placeholder="Internship Job Title" />
                    <asp:TextBox ID="txtEditInternCompany" runat="server" CssClass="edit-input" Placeholder="Company Name" />
                    <asp:TextBox ID="txtEditInternStartDate" runat="server" CssClass="edit-input" Placeholder="Start Date (yyyy-MM-dd)" />
                    <asp:TextBox ID="txtEditInternEndDate" runat="server" CssClass="edit-input" Placeholder="End Date (yyyy-MM-dd)" />
                    <asp:TextBox ID="txtEditInternDescription" runat="server" TextMode="MultiLine" Rows="3" CssClass="edit-input" Placeholder="Description" />
                </asp:Panel>
            </div>
            <hr />

            

            <!-- Additional Information -->
            <div class="section">
                <h2>Additional Information</h2>
                <asp:Panel ID="pnlViewCustomSelection" runat="server" Visible="true">
                    <asp:Repeater ID="rpt9CustomSelection" runat="server">
                        <ItemTemplate>
                            <p>
                                I worked on <asp:Label ID="text9Title" runat="server" Text='<%# Eval("Title") %>' /> 
                                between <%# Eval("StartDate", "{0:MMM yyyy}") %> and 
                                <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMM yyyy}") %>. 
                                <asp:Label ID="text9Description" runat="server" Text='<%# Eval("Description") %>' />
                            </p>
                        </ItemTemplate>
                    </asp:Repeater>
                </asp:Panel>
                <asp:Panel ID="pnlEditCustomSelection" runat="server" Visible="false">
                    <asp:Repeater ID="rptEditCustomSelection" runat="server" OnItemCommand="rptEditCustomSelection_ItemCommand">
                        <ItemTemplate>
                            <div class="edit-item">
                                <asp:TextBox ID="txtTitle" runat="server" Text='<%# Eval("Title") %>' CssClass="edit-input" Placeholder="Title" />
                                <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" Placeholder="Start Date (yyyy-MM-dd)" />
                                <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" Placeholder="End Date (yyyy-MM-dd) or leave blank for Present" />
                                <asp:TextBox ID="txtDescription" runat="server" Text='<%# Eval("Description") %>' TextMode="MultiLine" Rows="3" CssClass="edit-input" Placeholder="Description" />
                                <asp:HiddenField ID="hdnCustomId" runat="server" Value='<%# Eval("ID") %>' />
                                <asp:Button ID="btnDeleteCustom" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' CssClass="add-button" />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <h3>Add New Additional Information</h3>
                    <asp:TextBox ID="txtNewCustomTitle" runat="server" CssClass="edit-input" Placeholder="Title" />
                    <asp:TextBox ID="txtNewCustomStartDate" runat="server" CssClass="edit-input" Placeholder="Start Date (yyyy-MM-dd)" />
                    <asp:TextBox ID="txtNewCustomEndDate" runat="server" CssClass="edit-input" Placeholder="End Date (yyyy-MM-dd) or leave blank for Present" />
                    <asp:TextBox ID="txtNewCustomDescription" runat="server" TextMode="MultiLine" Rows="3" CssClass="edit-input" Placeholder="Description" />
                    <asp:Button ID="btnAddCustom" runat="server" Text="Add Additional Info" OnClick="btnAddCustom_Click" CssClass="add-button" />
                </asp:Panel>
            </div>

            <!-- Buttons -->
            <asp:Button ID="btnToggleEdit" runat="server" Text="Edit Resume" OnClick="btnToggleEdit_Click" CssClass="button" />
       
            <asp:Button ID="btnDownload" runat="server" Text="Download PDF" OnClick="btnDownload_Click" CssClass="button" />
            <asp:Button ID="btnBack" runat="server" Text="Back to Home" OnClick="btnBack_Click" CssClass="button" />
            <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="btnSave_Click" Visible="false" CssClass="button" />
        </div>
    </form>
</body>
</html>