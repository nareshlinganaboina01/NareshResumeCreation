```html
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Resume6.aspx.cs" Inherits="NareshResumeCreation.Resume6" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Minimalist Dark Resume</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style type="text/css">
        body {
            font-family: 'Roboto', sans-serif;
            margin: 0;
            padding: 0;
            background: #1a1a1a;
            color: #e0e0e0;
            line-height: 1.7;
        }
        .resume-container {
            max-width: 850px;
            margin: 50px auto;
            background: #2b2b2b;
            border-radius: 15px;
            box-shadow: 0 12px 40px rgba(0, 0, 0, 0.3);
            overflow: hidden;
        }
        .header {
            background: #ff6f61;
            color: #fff;
            padding: 40px 30px;
            text-align: center;
            border-bottom: 5px solid #e65a4d;
        }
        .header h1 {
            font-size: 34px;
            margin: 0 0 10px 0;
            font-weight: 700;
            text-transform: uppercase;
        }
        .header .job-title {
            font-size: 16px;
            font-weight: 300;
            opacity: 0.9;
        }
        .content {
            padding: 30px;
        }
        .section {
            margin-bottom: 30px;
        }
        .section h3 {
            font-size: 20px;
            color: #ff6f61;
            margin-bottom: 15px;
            padding-bottom: 5px;
            border-bottom: 2px dashed #ff6f61;
            font-weight: 500;
            text-transform: uppercase;
        }
        .section p {
            margin: 6px 0;
            font-size: 14px;
        }
        .list {
            list-style: none;
            padding: 0;
            display: flex;
            flex-wrap: wrap;
            gap: 10px;
        }
        .list li {
            background: #3a3a3a;
            padding: 8px 15px;
            border-radius: 20px;
            font-size: 13px;
            transition: background 0.3s;
        }
        .list li:hover {
            background: #ff6f61;
        }
        .entry {
            margin-bottom: 20px;
            padding: 15px;
            background: #333333;
            border-radius: 8px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);
        }
        .entry p {
            margin: 0;
            font-size: 14px;
            color: #d0d0d0;
        }
        .edit-section {
            background: #3a3a3a;
            padding: 15px;
            border-radius: 8px;
            margin-bottom: 20px;
        }
        .edit-section input, .edit-section textarea {
            width: 100%;
            padding: 8px;
            margin: 5px 0;
            border: 1px solid #555;
            border-radius: 4px;
            background: #444;
            color: #e0e0e0;
        }
        .edit-section button {
            background: #ff6f61;
            color: #fff;
            padding: 8px 15px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            margin: 5px 0;
        }
        .edit-section button:hover {
            background: #e65a4d;
        }
        .btn {
            background: #ff6f61;
            color: #fff;
            padding: 12px 30px;
            border: none;
            border-radius: 25px;
            cursor: pointer;
            font-size: 16px;
            transition: background 0.3s, transform 0.2s;
            display: inline-block;
            margin: 10px;
        }
        .btn:hover {
            background: #e65a4d;
            transform: scale(1.05);
        }
        @media (max-width: 600px) {
            .resume-container {
                margin: 20px;
            }
            .content {
                padding: 20px;
            }
            .header {
                padding: 30px 20px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="resume-container">
            <!-- View Mode -->
            <asp:Panel ID="pnlView" runat="server">
                <!-- Header Section -->
                <div class="header">
                    <h1><asp:Label ID="text6FirstName" runat="server" /> <asp:Label ID="text6LastName" runat="server" /></h1>
                    <p class="job-title"><asp:Label ID="text6JobTitle" runat="server" /></p>
                </div>

                <!-- Content -->
                <div class="content">
                    <!-- Contact Information -->
                    <div class="section">
                        <h3>Contact</h3>
                        <p>Email: <asp:Label ID="text6Email" runat="server" /></p>
                        <p>Phone: <asp:Label ID="text6Phone" runat="server" /></p>
                        <p><asp:Label ID="text6Address" runat="server" />, <asp:Label ID="text6City" runat="server" />, <asp:Label ID="text6State" runat="server" />, <asp:Label ID="text6PostalCode" runat="server" />, <asp:Label ID="text6Country" runat="server" /></p>
                    </div>

                    <!-- Personal Information -->
                    <div class="section">
                        <h3>Personal Info</h3>
                        <p>Date of Birth: <asp:Label ID="text6DateOfBirth" runat="server" /></p>
                        <p>Place of Birth: <asp:Label ID="text6PlaceOfBirth" runat="server" /></p>
                        <p>Nationality: <asp:Label ID="text6Nationality" runat="server" /></p>
                    </div>

                    <!-- Professional Summary -->
                    <div class="section">
                        <h3>Professional Summary</h3>
                        <div class="entry">
                            <p><asp:Label ID="text6ProfessionalSummary" runat="server" /></p>
                        </div>
                    </div>

                    <!-- Skills -->
                    <div class="section">
                        <h3>Skills</h3>
                        <ul class="list">
                            <asp:Repeater ID="rpt6Skills" runat="server">
                                <ItemTemplate>
                                    <li><asp:Literal ID="lit6Skill" runat="server" Text='<%# Eval("SkillName") %>' /></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>

                    <!-- Languages -->
                    <div class="section">
                        <h3>Languages</h3>
                        <ul class="list">
                            <asp:Repeater ID="rpt6Languages" runat="server">
                                <ItemTemplate>
                                    <li><asp:Literal ID="lit6Language" runat="server" Text='<%# Eval("LanguageName") %>' /></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>

                    <!-- Hobbies -->
                    <div class="section">
                        <h3>Hobbies</h3>
                        <ul class="list">
                            <asp:Repeater ID="rpt6Hobbies" runat="server">
                                <ItemTemplate>
                                    <li><asp:Literal ID="lit6Hobby" runat="server" Text='<%# Eval("Name") %>' /></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>

                    <!-- Employment History -->
                    <div class="section">
                        <h3>Employment History</h3>
                        <asp:Panel ID="pnl6Fresher" runat="server" Visible="false">
                            <div class="entry">
                                <p>Fresher (No work experience)</p>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnl6EmploymentHistory" runat="server" Visible="false">
                            <asp:Repeater ID="rpt6EmploymentHistory" runat="server">
                                <ItemTemplate>
                                    <div class="entry">
                                        <p>
                                            I worked as <asp:Label ID="text6JobTitle" runat="server" Text='<%# Eval("JobTitle") %>' /> 
                                            at <asp:Label ID="text6Employer" runat="server" Text='<%# Eval("Employer") %>' />, 
                                            <asp:Label ID="text6City" runat="server" Text='<%# Eval("City") %>' /> 
                                            between <%# Eval("StartDate", "{0:MMM yyyy}") %> and 
                                            <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMM yyyy}") %>. 
                                            <asp:Label ID="text6Description" runat="server" Text='<%# Eval("Description") %>' />
                                        </p>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </asp:Panel>
                    </div>

                    <!-- Education -->
                    <div class="section">
                        <h3>Education</h3>
                        <asp:Repeater ID="rpt6Education" runat="server">
                            <ItemTemplate>
                                <div class="entry">
                                    <p>
                                        I completed my <asp:Label ID="text6Degree" runat="server" Text='<%# Eval("Degree") %>' /> 
                                        from <asp:Label ID="text6SchoolName" runat="server" Text='<%# Eval("SchoolName") %>' />, 
                                        <asp:Label ID="text6City" runat="server" Text='<%# Eval("City") %>' /> 
                                        between <%# Eval("StartDate", "{0:MMM yyyy}") %> and 
                                        <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMM yyyy}") %>. 
                                        <asp:Label ID="text6Description" runat="server" Text='<%# Eval("Description") %>' />
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <!-- Certifications -->
                    <div class="section">
                        <h3>Certifications</h3>
                        <asp:Repeater ID="rpt6Certifications" runat="server">
                            <ItemTemplate>
                                <div class="entry">
                                    <p>
                                        I completed <asp:Label ID="text6CourseName" runat="server" Text='<%# Eval("CourseName") %>' /> 
                                        from <asp:Label ID="text6Institution" runat="server" Text='<%# Eval("Institution") %>' /> 
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
                                I worked as <asp:Label ID="text6InternJobTitle" runat="server" /> 
                                at <asp:Label ID="text6InternCompany" runat="server" /> 
                                between <asp:Label ID="text6InternStartDate" runat="server" /> and 
                                <asp:Label ID="text6InternEndDate" runat="server" />. 
                                <asp:Label ID="text6InternDescription" runat="server" />
                            </p>
                        </div>
                    </div>

                    <!-- Additional Information -->
                    <div class="section">
                        <h3>Additional Information</h3>
                        <asp:Repeater ID="rpt6CustomSelection" runat="server">
                            <ItemTemplate>
                                <div class="entry">
                                    <p>
                                        I worked on <asp:Label ID="text6Title" runat="server" Text='<%# Eval("Title") %>' /> 
                                        between <%# Eval("StartDate", "{0:MMM yyyy}") %> and 
                                        <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMM yyyy}") %>. 
                                        <asp:Label ID="text6Description" runat="server" Text='<%# Eval("Description") %>' />
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </asp:Panel>

            <!-- Edit Mode -->
            <asp:Panel ID="pnlEdit" runat="server" Visible="false">
                <div class="content">
                    <!-- Personal Details -->
                    <div class="edit-section">
                        <h3>Edit Personal Details</h3>
                        <asp:TextBox ID="txtEditJobTitle" runat="server" Placeholder="Job Title" />
                        <asp:TextBox ID="txtEditFirstName" runat="server" Placeholder="First Name" />
                        <asp:TextBox ID="txtEditLastName" runat="server" Placeholder="Last Name" />
                        <asp:TextBox ID="txtEditEmail" runat="server" Placeholder="Email" />
                        <asp:TextBox ID="txtEditPhone" runat="server" Placeholder="Phone" />
                        <asp:TextBox ID="txtEditDateOfBirth" runat="server" Placeholder="Date of Birth (yyyy-MM-dd)" />
                        <asp:TextBox ID="txtEditCountry" runat="server" Placeholder="Country" />
                        <asp:TextBox ID="txtEditState" runat="server" Placeholder="State" />
                        <asp:TextBox ID="txtEditCity" runat="server" Placeholder="City" />
                        <asp:TextBox ID="txtEditAddress" runat="server" Placeholder="Address" />
                        <asp:TextBox ID="txtEditPostalCode" runat="server" Placeholder="Postal Code" />
                        <asp:TextBox ID="txtEditNationality" runat="server" Placeholder="Nationality" />
                        <asp:TextBox ID="txtEditPlaceOfBirth" runat="server" Placeholder="Place of Birth" />
                        <asp:TextBox ID="txtEditProfessionalSummary" runat="server" TextMode="MultiLine" Rows="4" Placeholder="Professional Summary" />
                    </div>

                    <!-- Skills -->
                    <div class="edit-section">
                        <h3>Edit Skills</h3>
                        <asp:Repeater ID="rptEditSkills" runat="server" OnItemCommand="Skill_Command">
                            <ItemTemplate>
                                <div>
                                    <asp:TextBox ID="txtSkillName" runat="server" Text='<%# Eval("SkillName") %>' />
                                    <asp:Button ID="btnDeleteSkill" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:TextBox ID="txtNewSkill" runat="server" Placeholder="New Skill" />
                        <asp:Button ID="btnAddSkill" runat="server" Text="Add Skill" OnClick="btnAddSkill_Click" />
                    </div>

                    <!-- Languages -->
                    <div class="edit-section">
                        <h3>Edit Languages</h3>
                        <asp:Repeater ID="rptEditLanguages" runat="server" OnItemCommand="Language_Command">
                            <ItemTemplate>
                                <div>
                                    <asp:TextBox ID="txtLanguageName" runat="server" Text='<%# Eval("LanguageName") %>' />
                                    <asp:Button ID="btnDeleteLanguage" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:TextBox ID="txtNewLanguage" runat="server" Placeholder="New Language" />
                        <asp:Button ID="btnAddLanguage" runat="server" Text="Add Language" OnClick="btnAddLanguage_Click" />
                    </div>

                    <!-- Hobbies -->
                    <div class="edit-section">
                        <h3>Edit Hobbies</h3>
                        <asp:Repeater ID="rptEditHobbies" runat="server" OnItemCommand="Hobby_Command">
                            <ItemTemplate>
                                <div>
                                    <asp:TextBox ID="txtHobbyName" runat="server" Text='<%# Eval("Name") %>' />
                                    <asp:Button ID="btnDeleteHobby" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:TextBox ID="txtNewHobby" runat="server" Placeholder="New Hobby" />
                        <asp:Button ID="btnAddHobby" runat="server" Text="Add Hobby" OnClick="btnAddHobby_Click" />
                    </div>

                    <!-- Employment History -->
                    <div class="edit-section">
                        <h3>Employment Status</h3>
                        <asp:CheckBox ID="chkFresher" runat="server" Text="Fresher (No work experience)" AutoPostBack="true" OnCheckedChanged="chkFresher_CheckedChanged" />
                        <asp:Panel ID="pnlEditEmploymentHistory" runat="server">
                            <h3>Edit Employment History</h3>
                            <asp:Repeater ID="rptEditEmploymentHistory" runat="server" OnItemCommand="Employment_Command">
                                <ItemTemplate>
                                    <div>
                                        <asp:TextBox ID="txtJobTitle" runat="server" Text='<%# Eval("JobTitle") %>' Placeholder="Job Title" />
                                        <asp:TextBox ID="txtEmployer" runat="server" Text='<%# Eval("Employer") %>' Placeholder="Employer" />
                                        <asp:TextBox ID="txtCity" runat="server" Text='<%# Eval("City") %>' Placeholder="City" />
                                        <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' Placeholder="Start Date (yyyy-MM-dd)" />
                                        <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' Placeholder="End Date (yyyy-MM-dd)" />
                                        <asp:TextBox ID="txtDescription" runat="server" Text='<%# Eval("Description") %>' TextMode="MultiLine" Rows="3" Placeholder="Description" />
                                        <asp:Button ID="btnDeleteEmployment" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <h4>Add New Employment</h4>
                            <asp:TextBox ID="txtNewJobTitle" runat="server" Placeholder="Job Title" />
                            <asp:TextBox ID="txtNewEmployer" runat="server" Placeholder="Employer" />
                            <asp:TextBox ID="txtNewCity" runat="server" Placeholder="City" />
                            <asp:TextBox ID="txtNewStartDate" runat="server" Placeholder="Start Date (yyyy-MM-dd)" />
                            <asp:TextBox ID="txtNewEndDate" runat="server" Placeholder="End Date (yyyy-MM-dd)" />
                            <asp:TextBox ID="txtNewDescription" runat="server" TextMode="MultiLine" Rows="3" Placeholder="Description" />
                            <asp:Button ID="btnAddEmployment" runat="server" Text="Add Employment" OnClick="btnAddEmployment_Click" />
                        </asp:Panel>
                    </div>

                    <!-- Education -->
                    <div class="edit-section">
                        <h3>Edit Education</h3>
                        <asp:Repeater ID="rptEditEducation" runat="server" OnItemCommand="Education_Command">
                            <ItemTemplate>
                                <div>
                                    <asp:TextBox ID="txtDegree" runat="server" Text='<%# Eval("Degree") %>' Placeholder="Degree" />
                                    <asp:TextBox ID="txtSchoolName" runat="server" Text='<%# Eval("SchoolName") %>' Placeholder="School Name" />
                                    <asp:TextBox ID="txtCity" runat="server" Text='<%# Eval("City") %>' Placeholder="City" />
                                    <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' Placeholder="Start Date (yyyy-MM-dd)" />
                                    <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' Placeholder="End Date (yyyy-MM-dd)" />
                                    <asp:TextBox ID="txtDescription" runat="server" Text='<%# Eval("Description") %>' TextMode="MultiLine" Rows="3" Placeholder="Description" />
                                    <asp:Button ID="btnDeleteEducation" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <h4>Add New Education</h4>
                        <asp:TextBox ID="txtNewDegree" runat="server" Placeholder="Degree" />
                        <asp:TextBox ID="txtNewSchoolName" runat="server" Placeholder="School Name" />
                        <asp:TextBox ID="txtNewEducationCity" runat="server" Placeholder="City" />
                        <asp:TextBox ID="txtNewEducationStartDate" runat="server" Placeholder="Start Date (yyyy-MM-dd)" />
                        <asp:TextBox ID="txtNewEducationEndDate" runat="server" Placeholder="End Date (yyyy-MM-dd)" />
                        <asp:TextBox ID="txtNewEducationDescription" runat="server" TextMode="MultiLine" Rows="3" Placeholder="Description" />
                        <asp:Button ID="btnAddEducation" runat="server" Text="Add Education" OnClick="btnAddEducation_Click" />
                    </div>

                    <!-- Certifications -->
                    <div class="edit-section">
                        <h3>Edit Certifications</h3>
                        <asp:Repeater ID="rptEditCertifications" runat="server" OnItemCommand="Certification_Command">
                            <ItemTemplate>
                                <div>
                                    <asp:TextBox ID="txtCourseName" runat="server" Text='<%# Eval("CourseName") %>' Placeholder="Course Name" />
                                    <asp:TextBox ID="txtInstitution" runat="server" Text='<%# Eval("Institution") %>' Placeholder="Institution" />
                                    <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' Placeholder="Start Date (yyyy-MM-dd)" />
                                    <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' Placeholder="End Date (yyyy-MM-dd)" />
                                    <asp:Button ID="btnDeleteCertification" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <h4>Add New Certification</h4>
                        <asp:TextBox ID="txtNewCourseName" runat="server" Placeholder="Course Name" />
                        <asp:TextBox ID="txtNewInstitution" runat="server" Placeholder="Institution" />
                        <asp:TextBox ID="txtNewCertStartDate" runat="server" Placeholder="Start Date (yyyy-MM-dd)" />
                        <asp:TextBox ID="txtNewCertEndDate" runat="server" Placeholder="End Date (yyyy-MM-dd)" />
                        <asp:Button ID="btnAddCertification" runat="server" Text="Add Certification" OnClick="btnAddCertification_Click" />
                    </div>

                    <!-- Internships -->
                    <div class="edit-section">
                        <h3>Edit Internship</h3>
                        <asp:TextBox ID="txtEditInternCompany" runat="server" Placeholder="Company Name" />
                        <asp:TextBox ID="txtEditInternJobTitle" runat="server" Placeholder="Job Title" />
                        <asp:TextBox ID="txtEditInternStartDate" runat="server" Placeholder="Start Date (yyyy-MM-dd)" />
                        <asp:TextBox ID="txtEditInternEndDate" runat="server" Placeholder="End Date (yyyy-MM-dd)" />
                        <asp:TextBox ID="txtEditInternDescription" runat="server" TextMode="MultiLine" Rows="3" Placeholder="Description" />
                    </div>

                    <!-- Additional Information -->
                    <div class="edit-section">
                        <h3>Edit Additional Information</h3>
                        <asp:Repeater ID="rptEditCustomSelection" runat="server" OnItemCommand="CustomSelection_Command">
                            <ItemTemplate>
                                <div>
                                    <asp:TextBox ID="txtTitle" runat="server" Text='<%# Eval("Title") %>' Placeholder="Title" />
                                    <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' Placeholder="Start Date (yyyy-MM-dd)" />
                                    <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' Placeholder="End Date (yyyy-MM-dd)" />
                                    <asp:TextBox ID="txtDescription" runat="server" Text='<%# Eval("Description") %>' TextMode="MultiLine" Rows="3" Placeholder="Description" />
                                    <asp:Button ID="btnDeleteCustom" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <h4>Add New Custom Selection</h4>
                        <asp:TextBox ID="txtNewCustomTitle" runat="server" Placeholder="Title" />
                        <asp:TextBox ID="txtNewCustomStartDate" runat="server" Placeholder="Start Date (yyyy-MM-dd)" />
                        <asp:TextBox ID="txtNewCustomEndDate" runat="server" Placeholder="End Date (yyyy-MM-dd)" />
                        <asp:TextBox ID="txtNewCustomDescription" runat="server" TextMode="MultiLine" Rows="3" Placeholder="Description" />
                        <asp:Button ID="btnAddCustom" runat="server" Text="Add Custom Selection" OnClick="btnAddCustom_Click" />
                    </div>
                </div>
            </asp:Panel>

            <!-- Buttons -->
            <asp:Button ID="btnEdit" runat="server" Text="Edit Resume" CssClass="btn" OnClick="btnEdit_Click" Visible="true" />
            <asp:Button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn" OnClick="btnSave_Click" Visible="false" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn" OnClick="btnCancel_Click" Visible="false" />
            <asp:Button ID="btnDownload" runat="server" Text="Download PDF" CssClass="btn" OnClick="btnDownload_Click" />
            <asp:Button ID="btnBack" runat="server" Text="Back to Home" CssClass="btn" OnClick="btnBack_Click" />
        </div>
    </form>
</body>
</html>