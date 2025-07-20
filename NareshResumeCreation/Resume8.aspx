<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Resume8.aspx.cs" Inherits="NareshResumeCreation.Resume8" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Two-Column Resume</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style type="text/css">
        body {
            font-family: Arial, sans-serif;
            margin: 25px;
            line-height: 1.5;
        }
        .resume-wrapper {
            max-width: 900px;
            margin: 0 auto;
            display: flex;
            flex-wrap: wrap;
        }
        .left-column {
            width: 30%;
            padding-right: 10px;
        }
        .right-column {
            width: 70%;
            padding-left: 10px;
        }
        .header-section h1 {
            font-size: 20px;
            font-weight: bold;
            margin: 0 0 5px 0;
        }
        .header-section p {
            font-size: 10px;
            margin: 5px 0;
        }
        .section {
            margin-bottom: 10px;
        }
        .section h2 {
            font-size: 14px;
            font-weight: bold;
            margin-bottom: 5px;
        }
        .section p {
            font-size: 10px;
            margin: 5px 0;
        }
        .three-column {
            display: flex;
            justify-content: space-between;
            margin-top: 10px;
        }
        .three-column div {
            width: 30%;
        }
        .list {
            list-style-type: disc;
            padding-left: 15px;
            margin: 5px 0;
        }
        .list li {
            font-size: 10px;
            margin: 5px 0;
        }
        hr {
            border: 0;
            border-top: 1px solid #000;
            margin: 10px 0;
        }
        #btnDownload {
            padding: 8px 15px;
            font-size: 14px;
            cursor: pointer;
            display: block;
            margin: 20px auto 0;
        }
        .edit-input, .edit-textarea {
            width: 100%;
            padding: 5px;
            font-size: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
            margin: 5px 0;
        }
        .edit-textarea {
            height: 60px;
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
            padding: 8px 15px;
            font-size: 14px;
            cursor: pointer;
            margin: 0 10px;
        }
        @media (max-width: 600px) {
            .left-column, .right-column {
                width: 100%;
                padding: 0;
            }
            .three-column {
                flex-direction: column;
            }
            .three-column div {
                width: 100%;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="resume-wrapper">
            <asp:Panel ID="pnlView" runat="server" Visible="true">
                <!-- Left Column -->
                <div class="left-column">
                    <div class="header-section">
                        <h1><asp:Label ID="text8FirstName" runat="server" /> <asp:Label ID="text8LastName" runat="server" /></h1>
                        <p><asp:Label ID="text8JobTitle" runat="server" /></p>
                        <p>Email: <asp:Label ID="text8Email" runat="server" /></p>
                        <p>Phone: <asp:Label ID="text8Phone" runat="server" /></p>
                        <p>Address: <asp:Label ID="text8Address" runat="server" />, 
                            <asp:Label ID="text8City" runat="server" />, 
                            <asp:Label ID="text8State" runat="server" />, 
                            <asp:Label ID="text8PostalCode" runat="server" />, 
                            <asp:Label ID="text8Country" runat="server" /></p>
                        <p>Date of Birth: <asp:Label ID="text8DateOfBirth" runat="server" /></p>
                        <p>Place of Birth: <asp:Label ID="text8PlaceOfBirth" runat="server" /></p>
                        <p>Nationality: <asp:Label ID="text8Nationality" runat="server" /></p>
                    </div>
                    <hr />
                    <div class="section three-column">
                        <!-- Skills -->
                        <div>
                            <h2>Skills</h2>
                            <ul class="list">
                                <asp:Repeater ID="rpt8Skills" runat="server">
                                    <ItemTemplate>
                                        <li><asp:Literal ID="lit8Skill" runat="server" Text='<%# Eval("SkillName") %>' /></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>

                        <!-- Languages -->
                        <div>
                            <h2>Languages</h2>
                            <ul class="list">
                                <asp:Repeater ID="rpt8Languages" runat="server">
                                    <ItemTemplate>
                                        <li><asp:Literal ID="lit8Language" runat="server" Text='<%# Eval("LanguageName") %>' /></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>

                        <!-- Hobbies -->
                        <div>
                            <h2>Hobbies</h2>
                            <ul class="list">
                                <asp:Repeater ID="rpt8Hobbies" runat="server">
                                    <ItemTemplate>
                                        <li><asp:Literal ID="lit8Hobby" runat="server" Text='<%# Eval("Name") %>' /></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>

                <!-- Right Column -->
                <div class="right-column">
                    <!-- Professional Summary -->
                    <div class="section">
                        <h2>Professional Summary</h2>
                        <p><asp:Label ID="text8ProfessionalSummary" runat="server" /></p>
                    </div>
                    <hr />

                    <!-- Employment History -->
                    <div class="section">
                        <h2>Employment History</h2>
                        <asp:Panel ID="pnl8Fresher" runat="server" Visible="false">
                            <p>I am a fresher with no work experience.</p>
                        </asp:Panel>
                        <asp:Panel ID="pnl8EmploymentHistory" runat="server" Visible="false">
                            <asp:Repeater ID="rpt8EmploymentHistory" runat="server">
                                <ItemTemplate>
                                    <p>
                                        I worked as <asp:Label ID="text8JobTitle" runat="server" Text='<%# Eval("JobTitle") %>' /> 
                                        at <asp:Label ID="text8Employer" runat="server" Text='<%# Eval("Employer") %>' /> 
                                        between <%# Eval("StartDate", "{0:MMM yyyy}") %> and 
                                        <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMM yyyy}") %>. 
                                        <asp:Label ID="text8Description" runat="server" Text='<%# Eval("Description") %>' />
                                    </p>
                                </ItemTemplate>
                            </asp:Repeater>
                        </asp:Panel>
                    </div>
                    <hr />

                    <!-- Education -->
                    <div class="section">
                        <h2>Education</h2>
                        <asp:Repeater ID="rpt8Education" runat="server">
                            <ItemTemplate>
                                <p>
                                    I completed my <asp:Label ID="text8Degree" runat="server" Text='<%# Eval("Degree") %>' /> 
                                    from <asp:Label ID="text8SchoolName" runat="server" Text='<%# Eval("SchoolName") %>' />, 
                                    <asp:Label ID="text8City" runat="server" Text='<%# Eval("City") %>' /> 
                                    between <%# Eval("StartDate", "{0:MMM yyyy}") %> and 
                                    <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMM yyyy}") %>. 
                                    <asp:Label ID="text8Description" runat="server" Text='<%# Eval("Description") %>' />
                                </p>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <hr />

                    <!-- Certifications -->
                    <div class="section">
                        <h2>Certifications</h2>
                        <asp:Repeater ID="rpt8Certifications" runat="server">
                            <ItemTemplate>
                                <p>
                                    I completed <asp:Label ID="text8CourseName" runat="server" Text='<%# Eval("CourseName") %>' /> 
                                    from <asp:Label ID="text8Institution" runat="server" Text='<%# Eval("Institution") %>' /> 
                                    between <%# Eval("StartDate", "{0:MMM yyyy}") %> and 
                                    <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMM yyyy}") %>.
                                </p>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <hr />

                    <!-- Internships -->
                    <div class="section">
                        <h2>Internships</h2>
                        <p>
                            I worked as <asp:Label ID="text8InternJobTitle" runat="server" /> 
                            at <asp:Label ID="text8InternCompany" runat="server" /> 
                            between <asp:Label ID="text8InternStartDate" runat="server" /> and 
                            <asp:Label ID="text8InternEndDate" runat="server" />. 
                            <asp:Label ID="text8InternDescription" runat="server" />
                        </p>
                    </div>
                    <hr />

                    <!-- Additional Information -->
                    <div class="section">
                        <h2>Additional Information</h2>
                        <asp:Repeater ID="rpt8CustomSelection" runat="server">
                            <ItemTemplate>
                                <p>
                                    I worked on <asp:Label ID="text8Title" runat="server" Text='<%# Eval("Title") %>' /> 
                                    between <%# Eval("StartDate", "{0:MMM yyyy}") %> and 
                                    <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMM yyyy}") %>. 
                                    <asp:Label ID="text8Description" runat="server" Text='<%# Eval("Description") %>' />
                                </p>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlEdit" runat="server" Visible="false">
                <!-- Left Column -->
                <div class="left-column">
                    <div class="header-section">
                        <asp:TextBox ID="txtEditFirstName" runat="server" CssClass="edit-input" />
                        <asp:TextBox ID="txtEditLastName" runat="server" CssClass="edit-input" />
                        <asp:TextBox ID="txtEditJobTitle" runat="server" CssClass="edit-input" Placeholder="Job Title" />
                        <p>Email: <asp:TextBox ID="txtEditEmail" runat="server" CssClass="edit-input" /></p>
                        <p>Phone: <asp:TextBox ID="txtEditPhone" runat="server" CssClass="edit-input" /></p>
                        <p>Address: <asp:TextBox ID="txtEditAddress" runat="server" CssClass="edit-input" />, 
                            <asp:TextBox ID="txtEditCity" runat="server" CssClass="edit-input" />, 
                            <asp:TextBox ID="txtEditState" runat="server" CssClass="edit-input" />, 
                            <asp:TextBox ID="txtEditPostalCode" runat="server" CssClass="edit-input" />, 
                            <asp:TextBox ID="txtEditCountry" runat="server" CssClass="edit-input" /></p>
                        <p>Date of Birth: <asp:TextBox ID="txtEditDateOfBirth" runat="server" CssClass="edit-input" TextMode="Date" /></p>
                        <p>Place of Birth: <asp:TextBox ID="txtEditPlaceOfBirth" runat="server" CssClass="edit-input" /></p>
                        <p>Nationality: <asp:TextBox ID="txtEditNationality" runat="server" CssClass="edit-input" /></p>
                    </div>
                    <hr />
                    <div class="section three-column">
                        <!-- Skills -->
                        <div>
                            <h2>Skills</h2>
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
                        <div>
                            <h2>Languages</h2>
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
                        <div>
                            <h2>Hobbies</h2>
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
                </div>

                <!-- Right Column -->
                <div class="right-column">
                    <!-- Professional Summary -->
                    <div class="section">
                        <h2>Professional Summary</h2>
                        <asp:TextBox ID="txtEditProfessionalSummary" runat="server" CssClass="edit-textarea" TextMode="MultiLine" />
                    </div>
                    <hr />

                    <!-- Employment History -->
                    <div class="section">
                        <h2>Employment History</h2>
                        <asp:CheckBox ID="chkFresher" runat="server" Text="I am a fresher (no work experience)" AutoPostBack="true" OnCheckedChanged="chkFresher_CheckedChanged" />
                        <asp:Panel ID="pnlEditEmploymentHistory" runat="server" Visible="true">
                            <asp:Repeater ID="rptEditEmploymentHistory" runat="server">
                                <ItemTemplate>
                                    <div class="section">
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
                            <div class="section">
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
                    <hr />

                    <!-- Education -->
                    <div class="section">
                        <h2>Education</h2>
                        <asp:Repeater ID="rptEditEducation" runat="server">
                            <ItemTemplate>
                                <div class="section">
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
                        <div class="section">
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
                    <hr />

                    <!-- Certifications -->
                    <div class="section">
                        <h2>Certifications</h2>
                        <asp:Repeater ID="rptEditCertifications" runat="server">
                            <ItemTemplate>
                                <div class="section">
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
                        <div class="section">
                            <p>
                                Course: <asp:TextBox ID="txtNewCourseName" runat="server" CssClass="edit-input" /><br />
                                Institution: <asp:TextBox ID="txtNewInstitution" runat="server" CssClass="edit-input" /><br />
                                Start Date: <asp:TextBox ID="txtNewCertStartDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                End Date: <asp:TextBox ID="txtNewCertEndDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                <asp:Button ID="btnAddCertification" runat="server" Text="Add Certification" OnClick="btnAddCertification_Click" CssClass="btn" />
                            </p>
                        </div>
                    </div>
                    <hr />

                    <!-- Internships -->
                    <div class="section">
                        <h2>Internships</h2>
                        <div class="section">
                            <p>
                                Job Title: <asp:TextBox ID="txtEditInternJobTitle" runat="server" CssClass="edit-input" /><br />
                                Company: <asp:TextBox ID="txtEditInternCompany" runat="server" CssClass="edit-input" /><br />
                                Start Date: <asp:TextBox ID="txtEditInternStartDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                End Date: <asp:TextBox ID="txtEditInternEndDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                Description: <asp:TextBox ID="txtEditInternDescription" runat="server" CssClass="edit-textarea" TextMode="MultiLine" />
                            </p>
                        </div>
                    </div>
                    <hr />

                    <!-- Additional Information -->
                    <div class="section">
                        <h2>Additional Information</h2>
                        <asp:Repeater ID="rptEditCustomSelection" runat="server">
                            <ItemTemplate>
                                <div class="section">
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
                        <div class="section">
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