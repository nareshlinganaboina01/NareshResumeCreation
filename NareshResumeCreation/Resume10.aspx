<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Resume10.aspx.cs" Inherits="NareshResumeCreation.Resume10" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modern Single-Column Resume</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style type="text/css">
        body {
            font-family: 'Segoe UI', Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f4f4f4;
            color: #333;
        }
        .resume-wrapper {
            max-width: 900px;
            margin: 20px auto;
            background-color: white;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
            overflow: hidden;
        }
        .header-section {
            background-color: #2c3e50;
            color: white;
            padding: 20px;
            text-align: left;
        }
        .header-section h1 {
            font-size: 28px;
            font-weight: 600;
            margin: 0 0 10px 0;
        }
        .header-section p {
            font-size: 14px;
            margin: 5px 0;
            opacity: 0.9;
        }
        .content-section {
            padding: 20px;
        }
        .section {
            margin-bottom: 20px;
        }
        .section h2 {
            font-size: 18px;
            font-weight: 600;
            color: #2c3e50;
            margin-bottom: 10px;
            border-bottom: 2px solid #2c3e50;
            padding-bottom: 5px;
        }
        .section p {
            font-size: 14px;
            margin: 5px 0;
            line-height: 1.6;
        }
        .list {
            list-style-type: square;
            padding-left: 20px;
            margin: 5px 0;
        }
        .list li {
            font-size: 14px;
            margin: 5px 0;
        }
        .side-by-side {
            display: flex;
            justify-content: space-between;
            flex-wrap: wrap;
        }
        .side-by-side .section {
            flex: 1;
            min-width: 28%;
            margin-right: 20px;
        }
        .side-by-side .section:last-child {
            margin-right: 0;
        }
        .edit-input, .edit-textarea {
            width: 100%;
            padding: 5px;
            font-size: 14px;
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
        .btn {
            padding: 12px 25px;
            font-size: 16px;
            cursor: pointer;
            margin: 10px;
            border: none;
            border-radius: 5px;
            transition: background-color 0.3s;
        }
        #btnEdit, #btnSave, #btnCancel, #btnDownload, .add-btn {
            background-color: #2c3e50;
            color: white;
        }
        #btnEdit:hover, #btnSave:hover, #btnCancel:hover, #btnDownload:hover, .add-btn:hover {
            background-color: #1a252f;
        }
        .delete-btn {
            background-color: #dc3545;
            color: white;
        }
        .delete-btn:hover {
            background-color: #c82333;
        }
        #btnBack {
            background-color: #7f8c8d;
            color: white;
        }
        #btnBack:hover {
            background-color: #6c7778;
        }
        .button-container {
            text-align: center;
            margin: 20px 0;
        }
        @media (max-width: 768px) {
            .side-by-side .section {
                min-width: 100%;
                margin-right: 0;
                margin-bottom: 20px;
            }
            .btn {
                display: inline-block;
                margin: 10px 5px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="resume-wrapper">
            <asp:Panel ID="pnlView" runat="server" Visible="true">
                <!-- Header -->
                <div class="header-section">
                    <h1><asp:Label ID="text10FirstName" runat="server" /> <asp:Label ID="text10LastName" runat="server" /></h1>
                    <p><asp:Label ID="text10JobTitle" runat="server" /></p>
                    <p>Email: <asp:Label ID="text10Email" runat="server" /> | Phone: <asp:Label ID="text10Phone" runat="server" /></p>
                    <p><asp:Label ID="text10Address" runat="server" />, <asp:Label ID="text10City" runat="server" />,<asp:Label ID="text10State" runat="server" />, <asp:Label ID="text10PostalCode" runat="server" />, <asp:Label ID="text10Country" runat="server" /></p>
                    <p>Date of Birth: <asp:Label ID="text10DateOfBirth" runat="server" /> | Place of Birth: <asp:Label ID="text10PlaceOfBirth" runat="server" /> | Nationality: <asp:Label ID="text10Nationality" runat="server" /></p>
                </div>

                <!-- Content -->
                <div class="content-section">
                    <!-- Professional Summary -->
                    <div class="section">
                        <h2>Professional Summary</h2>
                        <p><asp:Label ID="text10ProfessionalSummary" runat="server" /></p>
                    </div>

                    <!-- Skills, Languages, Hobbies Side by Side -->
                    <div class="side-by-side">
                        <!-- Skills -->
                        <div class="section">
                            <h2>Skills</h2>
                            <ul class="list">
                                <asp:Repeater ID="rpt10Skills" runat="server">
                                    <ItemTemplate>
                                        <li><asp:Literal ID="lit10Skill" runat="server" Text='<%# Eval("SkillName") %>' /></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>

                        <!-- Languages -->
                        <div class="section">
                            <h2>Languages</h2>
                            <ul class="list">
                                <asp:Repeater ID="rpt10Languages" runat="server">
                                    <ItemTemplate>
                                        <li><asp:Literal ID="lit10Language" runat="server" Text='<%# Eval("LanguageName") %>' /></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>

                        <!-- Hobbies -->
                        <div class="section">
                            <h2>Hobbies</h2>
                            <ul class="list">
                                <asp:Repeater ID="rpt10Hobbies" runat="server">
                                    <ItemTemplate>
                                        <li><asp:Literal ID="lit10Hobby" runat="server" Text='<%# Eval("Name") %>' /></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>

                    <!-- Employment History -->
                    <div class="section">
                        <h2>Employment History</h2>
                        <asp:Panel ID="pnl10Fresher" runat="server" Visible="false">
                            <p>I am a fresher with no work experience.</p>
                        </asp:Panel>
                        <asp:Panel ID="pnl10EmploymentHistory" runat="server" Visible="false">
                            <asp:Repeater ID="rpt10EmploymentHistory" runat="server">
                                <ItemTemplate>
                                    <p>
                                        I worked as a <strong><asp:Label ID="text10JobTitle" runat="server" Text='<%# Eval("JobTitle") %>' /></strong> 
                                        at <asp:Label ID="text10Employer" runat="server" Text='<%# Eval("Employer") %>' /> 
                                        from <asp:Label ID="lblStartDate" runat="server" Text='<%# FormatDate(Eval("StartDate")) %>' /> 
                                        <%# FormatDate(Eval("EndDate")) == "Present" && FormatDate(Eval("StartDate")) != "Present" ? "to present" : "to " + FormatDate(Eval("EndDate")) %>. 
                                        <asp:Label ID="text10Description" runat="server" Text='<%# FormatDescription(Eval("Description")) %>' />
                                    </p>
                                </ItemTemplate>
                            </asp:Repeater>
                        </asp:Panel>
                    </div>

                    <!-- Education -->
                    <div class="section">
                        <h2>Education</h2>
                        <asp:Repeater ID="rpt10Education" runat="server">
                            <ItemTemplate>
                                <p>
                                    I completed my <strong><asp:Label ID="text10Degree" runat="server" Text='<%# Eval("Degree") %>' /></strong> 
                                    from <asp:Label ID="text10SchoolName" runat="server" Text='<%# Eval("SchoolName") %>' />, 
                                    <asp:Label ID="text10City" runat="server" Text='<%# Eval("City") %>' /> 
                                    from <asp:Label ID="lblStartDate" runat="server" Text='<%# FormatDate(Eval("StartDate")) %>' /> 
                                    <%# FormatDate(Eval("EndDate")) == "Present" && FormatDate(Eval("StartDate")) != "Present" ? "to present" : "to " + FormatDate(Eval("EndDate")) %>. 
                                    <asp:Label ID="text10Description" runat="server" Text='<%# FormatDescription(Eval("Description")) %>' />
                                </p>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <!-- Certifications -->
                    <div class="section">
                        <h2>Certifications</h2>
                        <asp:Repeater ID="rpt10Certifications" runat="server">
                            <ItemTemplate>
                                <p>
                                    I earned a <strong><asp:Label ID="text10CourseName" runat="server" Text='<%# Eval("CourseName") %>' /></strong> 
                                    from <asp:Label ID="text10Institution" runat="server" Text='<%# Eval("Institution") %>' /> 
                                    from <asp:Label ID="lblStartDate" runat="server" Text='<%# FormatDate(Eval("StartDate")) %>' /> 
                                    <%# FormatDate(Eval("EndDate")) == "Present" && FormatDate(Eval("StartDate")) != "Present" ? "to present" : "to " + FormatDate(Eval("EndDate")) %>.
                                </p>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <!-- Internships -->
                    <div class="section">
                        <h2>Internships</h2>
                        <p>
                            I interned as a <strong><asp:Label ID="text10InternJobTitle" runat="server" /></strong> 
                            at <asp:Label ID="text10InternCompany" runat="server" /> 
                            from <asp:Label ID="text10InternStartDate" runat="server" /> 
                            to <asp:Label ID="text10InternEndDate" runat="server" />. 
                            <asp:Label ID="text10InternDescription" runat="server" />
                        </p>
                    </div>

                    <!-- Additional Information -->
                    <div class="section">
                        <h2>Additional Information</h2>
                        <asp:Repeater ID="rpt10CustomSelection" runat="server">
                            <ItemTemplate>
                                <p>
                                    I engaged in <strong><asp:Label ID="text10Title" runat="server" Text='<%# Eval("Title") %>' /></strong> 
                                    from <asp:Label ID="lblStartDate" runat="server" Text='<%# FormatDate(Eval("StartDate")) %>' /> 
                                    <%# FormatDate(Eval("EndDate")) == "Present" && FormatDate(Eval("StartDate")) != "Present" ? "to present" : "to " + FormatDate(Eval("EndDate")) %>. 
                                    <asp:Label ID="text10Description" runat="server" Text='<%# FormatDescription(Eval("Description")) %>' />
                                </p>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlEdit" runat="server" Visible="false">
                <!-- Header -->
                <div class="header-section">
                    <asp:TextBox ID="txtEditFirstName" runat="server" CssClass="edit-input" Placeholder="First Name" />
                    <asp:TextBox ID="txtEditLastName" runat="server" CssClass="edit-input" Placeholder="Last Name" />
                    <asp:TextBox ID="txtEditJobTitle" runat="server" CssClass="edit-input" Placeholder="Job Title" />
                    <p>Email: <asp:TextBox ID="txtEditEmail" runat="server" CssClass="edit-input" /></p>
                    <p>Phone: <asp:TextBox ID="txtEditPhone" runat="server" CssClass="edit-input" /></p>
                    <p>
                        <asp:TextBox ID="txtEditAddress" runat="server" CssClass="edit-input" Placeholder="Address" />,
                        <asp:TextBox ID="txtEditCity" runat="server" CssClass="edit-input" Placeholder="City" />,
                        <asp:TextBox ID="txtEditState" runat="server" CssClass="edit-input" Placeholder="State" />,
                        <asp:TextBox ID="txtEditPostalCode" runat="server" CssClass="edit-input" Placeholder="Postal Code" />,
                        <asp:TextBox ID="txtEditCountry" runat="server" CssClass="edit-input" Placeholder="Country" />
                    </p>
                    <p>
                        Date of Birth: <asp:TextBox ID="txtEditDateOfBirth" runat="server" CssClass="edit-input" TextMode="Date" /> |
                        Place of Birth: <asp:TextBox ID="txtEditPlaceOfBirth" runat="server" CssClass="edit-input" /> |
                        Nationality: <asp:TextBox ID="txtEditNationality" runat="server" CssClass="edit-input" />
                    </p>
                </div>

                <!-- Content -->
                <div class="content-section">
                    <!-- Professional Summary -->
                    <div class="section">
                        <h2>Professional Summary</h2>
                        <asp:TextBox ID="txtEditProfessionalSummary" runat="server" CssClass="edit-textarea" TextMode="MultiLine" />
                    </div>

                    <!-- Skills, Languages, Hobbies Side by Side -->
                    <div class="side-by-side">
                        <!-- Skills -->
                        <div class="section">
                            <h2>Skills</h2>
                            <asp:Repeater ID="rptEditSkills" runat="server">
                                <ItemTemplate>
                                    <div class="edit-list-item">
                                        <asp:TextBox ID="txtSkillName" runat="server" Text='<%# Eval("SkillName") %>' CssClass="edit-input" />
                                        <asp:Button ID="btnDeleteSkill" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Skill_Command" CssClass="btn delete-btn" />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:TextBox ID="txtNewSkill" runat="server" CssClass="edit-input" Placeholder="Add new skill" />
                            <asp:Button ID="btnAddSkill" runat="server" Text="Add Skill" OnClick="btnAddSkill_Click" CssClass="btn add-btn" />
                        </div>

                        <!-- Languages -->
                        <div class="section">
                            <h2>Languages</h2>
                            <asp:Repeater ID="rptEditLanguages" runat="server">
                                <ItemTemplate>
                                    <div class="edit-list-item">
                                        <asp:TextBox ID="txtLanguageName" runat="server" Text='<%# Eval("LanguageName") %>' CssClass="edit-input" />
                                        <asp:Button ID="btnDeleteLanguage" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Language_Command" CssClass="btn delete-btn" />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:TextBox ID="txtNewLanguage" runat="server" CssClass="edit-input" Placeholder="Add new language" />
                            <asp:Button ID="btnAddLanguage" runat="server" Text="Add Language" OnClick="btnAddLanguage_Click" CssClass="btn add-btn" />
                        </div>

                        <!-- Hobbies -->
                        <div class="section">
                            <h2>Hobbies</h2>
                            <asp:Repeater ID="rptEditHobbies" runat="server">
                                <ItemTemplate>
                                    <div class="edit-list-item">
                                        <asp:TextBox ID="txtHobbyName" runat="server" Text='<%# Eval("Name") %>' CssClass="edit-input" />
                                        <asp:Button ID="btnDeleteHobby" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Hobby_Command" CssClass="btn delete-btn" />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:TextBox ID="txtNewHobby" runat="server" CssClass="edit-input" Placeholder="Add new hobby" />
                            <asp:Button ID="btnAddHobby" runat="server" Text="Add Hobby" OnClick="btnAddHobby_Click" CssClass="btn add-btn" />
                        </div>
                    </div>

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
                                            <asp:Button ID="btnDeleteEmployment" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Employment_Command" CssClass="btn delete-btn" />
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
                                    <asp:Button ID="btnAddEmployment" runat="server" Text="Add Employment" OnClick="btnAddEmployment_Click" CssClass="btn add-btn" />
                                </p>
                            </div>
                        </asp:Panel>
                    </div>

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
                                        <asp:Button ID="btnDeleteEducation" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Education_Command" CssClass="btn delete-btn" />
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
                                <asp:Button ID="btnAddEducation" runat="server" Text="Add Education" OnClick="btnAddEducation_Click" CssClass="btn add-btn" />
                            </p>
                        </div>
                    </div>

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
                                        <asp:Button ID="btnDeleteCertification" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Certification_Command" CssClass="btn delete-btn" />
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
                                <asp:Button ID="btnAddCertification" runat="server" Text="Add Certification" OnClick="btnAddCertification_Click" CssClass="btn add-btn" />
                            </p>
                        </div>
                    </div>

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
                                        <asp:Button ID="btnDeleteCustom" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="CustomSelection_Command" CssClass="btn delete-btn" />
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
                                <asp:Button ID="btnAddCustom" runat="server" Text="Add Custom Section" OnClick="btnAddCustom_Click" CssClass="btn add-btn" />
                            </p>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <!-- Buttons -->
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