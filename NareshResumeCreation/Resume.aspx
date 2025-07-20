<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Resume.aspx.cs" Inherits="NareshResumeCreation.Resume" %>

<!DOCTYPE html>
<html>
<head>
    <title>Creative Resume</title>
    <style>
        body {
            font-family: 'Poppins', sans-serif;
            margin: 0;
            padding: 0;
            background: linear-gradient(120deg, #f6d365, #fda085);
            color: #2c3e50;
            line-height: 1.7;
        }
        .resume-container {
            max-width: 950px;
            margin: 40px auto;
            padding: 50px;
            background: linear-gradient(to right, #ffffff, #f2f6fa);
            border-radius: 20px;
            box-shadow: 0 15px 40px rgba(0, 0, 0, 0.2);
            position: relative;
            overflow: hidden;
        }
        .header-section {
            background: linear-gradient(45deg, #34495e, #2c3e50);
            color: #fff;
            padding: 50px;
            border-radius: 15px 15px 0 0;
            text-align: center;
            position: relative;
        }
        .header-section::after {
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: url('data:image/svg+xml;utf8,<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1440 320"><path fill="rgba(255,255,255,0.1)" fill-opacity="1" d="M0,160L48,176C96,192,192,224,288,213.3C384,203,480,149,576,149.3C672,149,768,203,864,213.3C960,224,1056,192,1152,181.3C1248,171,1344,181,1392,186.7L1440,192L1440,0L1392,0C1344,0,1248,0,1152,0C1056,0,960,0,864,0C768,0,672,0,576,0C480,0,384,0,288,0C192,0,96,0,48,0L0,0Z"></path></svg>');
            z-index: 0;
        }
        h1 {
            font-size: 42px;
            margin: 0;
            text-transform: uppercase;
            letter-spacing: 3px;
            font-weight: 700;
            z-index: 1;
            position: relative;
        }
        .job-title {
            font-size: 22px;
            color: #bdc3c7;
            font-weight: 300;
            margin: 10px 0;
            z-index: 1;
            position: relative;
        }
        h2 {
            font-size: 24px;
            color: #fff;
            background: linear-gradient(90deg, #e67e22, #d35400);
            padding: 12px 25px;
            margin: 40px 0 25px;
            border-radius: 10px;
            text-transform: uppercase;
            letter-spacing: 2px;
            box-shadow: 0 5px 15px rgba(0,0,0,0.15);
            width: fit-content;
        }
        .section {
            margin-bottom: 40px;
            padding: 30px;
            background: rgba(255, 255, 255, 0.95);
            border-radius: 15px;
            box-shadow: 0 8px 20px rgba(0,0,0,0.1);
            transition: all 0.3s ease;
        }
        .section:hover {
            transform: translateY(-8px);
            box-shadow: 0 12px 25px rgba(0,0,0,0.15);
        }
        .contact-info {
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
            gap: 25px;
            margin-top: 25px;
        }
        .contact-item {
            display: flex;
            align-items: center;
            font-size: 16px;
            color: #ecf0f1;
            background: rgba(0,0,0,0.2);
            padding: 10px 20px;
            border-radius: 30px;
            box-shadow: 0 3px 10px rgba(0,0,0,0.1);
        }
        .icon {
            margin-right: 12px;
            font-size: 20px;
            color: #f1c40f;
        }
        .skills-list, .languages-list, .hobbies-list {
            list-style-type: none;
            padding: 0;
            display: flex;
            flex-wrap: wrap;
            gap: 15px;
        }
        .skills-list li, .languages-list li, .hobbies-list li {
            background: linear-gradient(135deg, #3498db, #2980b9);
            color: #fff;
            padding: 12px 20px;
            border-radius: 30px;
            font-size: 15px;
            box-shadow: 0 3px 8px rgba(0,0,0,0.1);
            transition: transform 0.2s;
        }
        .skills-list li:hover, .languages-list li:hover, .hobbies-list li:hover {
            transform: scale(1.05);
        }
        .bullet-points {
            list-style-type: none;
            padding-left: 25px;
        }
        .bullet-points li {
            margin-bottom: 20px;
            position: relative;
            padding-left: 30px;
            color: #34495e;
        }
        .bullet-points li:before {
            content: '➤';
            position: absolute;
            left: 0;
            color: #e74c3c;
            font-size: 16px;
        }
        .two-column {
            display: flex;
            justify-content: space-between;
            gap: 30px;
        }
        .two-column .column {
            width: 48%;
        }
        .employment-section .job-entry {
            margin-bottom: 30px;
            padding: 25px;
            background: #fefefe;
            border-radius: 12px;
            box-shadow: 0 5px 15px rgba(0,0,0,0.05);
        }
        .job-title {
            font-weight: 600;
            font-size: 20px;
            color: #2c3e50;
        }
        .company-name {
            color: #c0392b;
            font-weight: bold;
            font-size: 16px;
        }
        .date-range {
            color: #7f8c8d;
            font-style: italic;
            font-size: 15px;
        }
        .button-container {
            display: flex;
            justify-content: center;
            gap: 20px;
            margin-top: 30px;
        }
        .action-button {
            background: linear-gradient(45deg, #e74c3c, #c0392b);
            color: #fff;
            padding: 15px 30px;
            border: none;
            border-radius: 30px;
            cursor: pointer;
            font-size: 18px;
            font-weight: 500;
            transition: all 0.3s ease;
        }
        .action-button:hover {
            transform: scale(1.1);
            box-shadow: 0 8px 20px rgba(0,0,0,0.2);
        }
        #btnEdit {
            background: linear-gradient(45deg, #3498db, #2980b9);
        }
        #btnSave, #btnCancel {
            background: linear-gradient(45deg, #2ecc71, #27ae60);
        }
        #btnCancel {
            background: linear-gradient(45deg, #7f8c8d, #95a5a6);
        }
        #btnBack {
            background: linear-gradient(45deg, #7f8c8d, #95a5a6);
        }
        .edit-section {
            display: none;
            margin-bottom: 30px;
            padding: 20px;
            background: #f2f6fa;
            border-radius: 10px;
        }
        .edit-mode .edit-section {
            display: block;
        }
        .edit-mode .display-section {
            display: none;
        }
        .form-group {
            margin-bottom: 20px;
        }
        .form-group label {
            display: block;
            font-weight: 600;
            margin-bottom: 5px;
        }
        .form-group input, .form-group textarea, .form-group select {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
            font-size: 16px;
        }
        .form-group textarea {
            height: 100px;
        }
        .add-button {
            background: #3498db;
            color: #fff;
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            margin-top: 10px;
        }
        .add-button:hover {
            background: #2980b9;
        }
        .repeater-section {
            margin-top: 20px;
        }
        .repeater-item {
            background: #fff;
            padding: 15px;
            border-radius: 8px;
            margin-bottom: 15px;
            box-shadow: 0 3px 10px rgba(0,0,0,0.1);
        }
        .remove-button {
            background: #e74c3c;
            color: #fff;
            padding: 5px 10px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }
        .action-button.edit-btn {
    background: linear-gradient(45deg, #3498db, #2980b9);
}
.action-button.save-btn {
    background: linear-gradient(45deg, #2ecc71, #27ae60);
}
.action-button.cancel-btn {
    background: linear-gradient(45deg, #7f8c8d, #95a5a6);
}
.action-button.back-btn {
    background: linear-gradient(45deg, #7f8c8d, #95a5a6);
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="resume-container" runat="server" id="resumeContainer">
            <!-- Header Section (Display) -->
            <div class="header-section display-section">
                <h1><asp:Label runat="server" ID="lblFirstName" /> <asp:Label runat="server" ID="lblLastName" /></h1>
                <p class="job-title"><asp:Label runat="server" ID="lblJobTitle" /></p>
                <div class="contact-info">
                    <div class="contact-item"><span class="icon">📧</span><asp:Label runat="server" ID="lblEmail" /></div>
                    <div class="contact-item"><span class="icon">📞</span><asp:Label runat="server" ID="lblPhone" /></div>
                    <div class="contact-item"><span class="icon">📍</span><asp:Label runat="server" ID="lblAddress" />, <asp:Label runat="server" ID="lblCity" />,<asp:Label runat="server" ID="lblState" />, <asp:Label runat="server" ID="lblPlaceOfBirth" />, <asp:Label runat="server" ID="lblPostalCode" />, <asp:Label runat="server" ID="lblCountry" /></div>
                    <div class="contact-item"><span class="icon">🎂</span><asp:Label runat="server" ID="lblDateOfBirth" /></div>
                    <div class="contact-item"><span class="icon">🌐</span><asp:Label runat="server" ID="lblNationality" /></div>
                </div>
            </div>

            <!-- Header Section (Edit) -->
            <div class="edit-section">
                <h2>Personal Details</h2>
                <div class="form-group">
                    <label>First Name</label>
                    <asp:TextBox ID="txtFirstName" runat="server" />
                </div>
                <div class="form-group">
                    <label>Last Name</label>
                    <asp:TextBox ID="txtLastName" runat="server" />
                </div>
                <div class="form-group">
                    <label>Job Title</label>
                    <asp:TextBox ID="txtJobTitle" runat="server" />
                </div>
                <div class="form-group">
                    <label>Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" />
                </div>
                <div class="form-group">
                    <label>Phone</label>
                    <asp:TextBox ID="txtPhone" runat="server" TextMode="Phone" />
                </div>
                <div class="form-group">
                    <label>Date of Birth</label>
                    <asp:TextBox ID="txtDateOfBirth" runat="server" TextMode="Date" />
                </div>
                <div class="form-group">
                    <label>Address</label>
                    <asp:TextBox ID="txtAddress" runat="server" />
                </div>
                <div class="form-group">
                    <label>City</label>
                    <asp:TextBox ID="txtCity" runat="server" />
                </div>
                <div class="form-group">
                    <label>State</label>
                    <asp:TextBox ID="txtState" runat="server" />
                </div>
                <div class="form-group">
                    <label>Postal Code</label>
                    <asp:TextBox ID="txtPostalCode" runat="server" />
                </div>
                <div class="form-group">
                    <label>Country</label>
                    <asp:TextBox ID="txtCountry" runat="server" />
                </div>
                <div class="form-group">
                    <label>Nationality</label>
                    <asp:TextBox ID="txtNationality" runat="server" />
                </div>
                <div class="form-group">
                    <label>Place of Birth</label>
                    <asp:TextBox ID="txtPlaceOfBirth" runat="server" />
                </div>
                <div class="form-group">
                    <label>Professional Summary</label>
                    <asp:TextBox ID="txtProfessionalSummary" runat="server" TextMode="MultiLine" />
                </div>
                <div class="form-group">
                    <label>Is Fresher?</label>
                    <asp:CheckBox ID="chkIsFresher" runat="server" />
                </div>
            </div>

            <!-- Professional Summary (Display) -->
            <div class="section display-section">
                <h2>Professional Summary</h2>
                <p><asp:Label runat="server" ID="lblProfessionalSummary" /></p>
            </div>

            <!-- Education (Display) -->
            <div class="section display-section">
                <h2>Education</h2>
                <ul class="bullet-points">
                    <asp:Repeater ID="rptEducation" runat="server">
                        <ItemTemplate>
                            <li>I completed my <asp:Label ID="lblDegree" runat="server" Text='<%# Eval("Degree") %>' /> from <span class="company-name"><asp:Label ID="lblSchoolName" runat="server" Text='<%# Eval("SchoolName") %>' /></span>, <asp:Label ID="lblCity" runat="server" Text='<%# Eval("City") %>' /> between <asp:Label ID="lblStartDate" runat="server" Text='<%# FormatDate(Eval("StartDate").ToString()) %>' /> and <asp:Label ID="lblEndDate" runat="server" Text='<%# FormatDate(Eval("EndDate").ToString()) %>' />. <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' /></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>

            <!-- Education (Edit) -->
            <div class="edit-section">
                <h2>Education</h2>
                <asp:Repeater ID="rptEducationEdit" runat="server" OnItemCommand="rptEducationEdit_ItemCommand">
                    <ItemTemplate>
                        <div class="repeater-item">
                            <div class="form-group">
                                <label>Degree</label>
                                <asp:TextBox ID="txtDegree" runat="server" Text='<%# Eval("Degree") %>' />
                            </div>
                            <div class="form-group">
                                <label>School Name</label>
                                <asp:TextBox ID="txtSchoolName" runat="server" Text='<%# Eval("SchoolName") %>' />
                            </div>
                            <div class="form-group">
                                <label>City</label>
                                <asp:TextBox ID="txtCity" runat="server" Text='<%# Eval("City") %>' />
                            </div>
                            <div class="form-group">
                                <label>Start Date</label>
                                <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' TextMode="Date" />
                            </div>
                            <div class="form-group">
                                <label>End Date</label>
                                <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' TextMode="Date" />
                            </div>
                            <div class="form-group">
                                <label>Description</label>
                                <asp:TextBox ID="txtDescription" runat="server" Text='<%# Eval("Description") %>' TextMode="MultiLine" />
                            </div>
                            <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandName="Remove" CommandArgument='<%# Eval("ID") %>' CssClass="remove-button" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Button ID="btnAddEducation" runat="server" Text="Add Education" OnClick="btnAddEducation_Click" CssClass="add-button" />
            </div>

            <!-- Skills & Languages (Display) -->
            <div class="two-column display-section">
                <div class="column">
                    <div class="section">
                        <h2>Skills</h2>
                        <ul class="skills-list">
                            <asp:Repeater ID="rptSkills" runat="server">
                                <ItemTemplate>
                                    <li><asp:Literal ID="litSkill" runat="server" Text='<%# Eval("SkillName") %>' /></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
                <div class="column">
                    <div class="section">
                        <h2>Languages</h2>
                        <ul class="languages-list">
                            <asp:Repeater ID="rptLanguages" runat="server">
                                <ItemTemplate>
                                    <li><asp:Literal ID="litLanguage" runat="server" Text='<%# Eval("LanguageName") %>' /></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </div>

            <!-- Skills (Edit) -->
            <div class="edit-section">
                <h2>Skills</h2>
                <asp:Repeater ID="rptSkillsEdit" runat="server" OnItemCommand="rptSkillsEdit_ItemCommand">
                    <ItemTemplate>
                        <div class="repeater-item">
                            <div class="form-group">
                                <label>Skill Name</label>
                                <asp:TextBox ID="txtSkillName" runat="server" Text='<%# Eval("SkillName") %>' />
                            </div>
                            <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandName="Remove" CommandArgument='<%# Eval("ID") %>' CssClass="remove-button" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Button ID="btnAddSkill" runat="server" Text="Add Skill" OnClick="btnAddSkill_Click" CssClass="add-button" />
            </div>

            <!-- Languages (Edit) -->
            <div class="edit-section">
                <h2>Languages</h2>
                <asp:Repeater ID="rptLanguagesEdit" runat="server" OnItemCommand="rptLanguagesEdit_ItemCommand">
                    <ItemTemplate>
                        <div class="repeater-item">
                            <div class="form-group">
                                <label>Language Name</label>
                                <asp:TextBox ID="txtLanguageName" runat="server" Text='<%# Eval("LanguageName") %>' />
                            </div>
                            <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandName="Remove" CommandArgument='<%# Eval("ID") %>' CssClass="remove-button" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Button ID="btnAddLanguage" runat="server" Text="Add Language" OnClick="btnAddLanguage_Click" CssClass="add-button" />
            </div>

            <!-- Employment History (Display) -->
<div class="employment-section section display-section">
    <h2>Employment History</h2>
    <asp:Panel ID="pnlFresher" runat="server" Visible="false">
        <p>Fresher (No work experience)</p>
    </asp:Panel>
    <asp:Panel ID="pnlEmploymentHistory" runat="server" Visible="false">
        <asp:Repeater ID="rptEmploymentHistory" runat="server">
            <ItemTemplate>
                <div class="job-entry">
                    <p class="job-title"><asp:Label ID="lblJobTitle" runat="server" Text='<%# Eval("JobTitle") %>' /></p>
                    <p><span class="company-name"><asp:Label ID="lblEmployer" runat="server" Text='<%# Eval("Employer") %>' /></span> • <asp:Label ID="lblCity" runat="server" Text='<%# Eval("City") %>' /></p>
                    <p class="date-range"><%# FormatEmploymentDate(Eval("StartDate")) %> - <%# FormatEmploymentDate(Eval("EndDate")) %></p>
                    <p><asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' /></p>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </asp:Panel>
</div>

<!-- Employment History (Edit) -->
<div class="edit-section">
    <h2>Employment History</h2>
    <asp:Repeater ID="rptEmploymentHistoryEdit" runat="server" OnItemCommand="rptEmploymentHistoryEdit_ItemCommand">
        <ItemTemplate>
            <div class="repeater-item">
                <div class="form-group">
                    <label>Job Title</label>
                    <asp:TextBox ID="txtJobTitle" runat="server" Text='<%# Eval("JobTitle") %>' />
                </div>
                <div class="form-group">
                    <label>Employer</label>
                    <asp:TextBox ID="txtEmployer" runat="server" Text='<%# Eval("Employer") %>' />
                </div>
                <div class="form-group">
                    <label>City</label>
                    <asp:TextBox ID="txtCity" runat="server" Text='<%# Eval("City") %>' />
                </div>
                <div class="form-group">
                    <label>Start Date</label>
                    <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' TextMode="Date" />
                </div>
                <div class="form-group">
                    <label>End Date</label>
                    <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' TextMode="Date" />
                </div>
                <div class="form-group">
                    <label>Description</label>
                    <asp:TextBox ID="txtDescription" runat="server" Text='<%# Eval("Description") %>' TextMode="MultiLine" />
                </div>
                <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandName="Remove" CommandArgument='<%# Eval("ID") %>' CssClass="remove-button" />
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <asp:Button ID="btnAddEmployment" runat="server" Text="Add Employment" OnClick="btnAddEmployment_Click" CssClass="add-button" />
</div>

            <!-- Certifications (Display) -->
            <div class="section display-section">
                <h2>Certifications</h2>
                <ul class="bullet-points">
                    <asp:Repeater ID="rptCertifications" runat="server">
                        <ItemTemplate>
                            <li><asp:Label ID="lblCourseName" runat="server" Text='<%# Eval("CourseName") %>' /> from <span class="company-name"><asp:Label ID="lblInstitution" runat="server" Text='<%# Eval("Institution") %>' /></span> (<asp:Label ID="lblStartDate" runat="server" Text='<%# FormatDate(Eval("StartDate").ToString()) %>' /> - <asp:Label ID="lblEndDate" runat="server" Text='<%# FormatDate(Eval("EndDate").ToString()) %>' />)</li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>

            <!-- Certifications (Edit) -->
            <div class="edit-section">
                <h2>Certifications</h2>
                <asp:Repeater ID="rptCertificationsEdit" runat="server" OnItemCommand="rptCertificationsEdit_ItemCommand">
                    <ItemTemplate>
                        <div class="repeater-item">
                            <div class="form-group">
                                <label>Course Name</label>
                                <asp:TextBox ID="txtCourseName" runat="server" Text='<%# Eval("CourseName") %>' />
                            </div>
                            <div class="form-group">
                                <label>Institution</label>
                                <asp:TextBox ID="txtInstitution" runat="server" Text='<%# Eval("Institution") %>' />
                            </div>
                            <div class="form-group">
                                <label>Start Date</label>
                                <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' TextMode="Date" />
                            </div>
                            <div class="form-group">
                                <label>End Date</label>
                                <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' TextMode="Date" />
                            </div>
                            <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandName="Remove" CommandArgument='<%# Eval("ID") %>' CssClass="remove-button" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Button ID="btnAddCertification" runat="server" Text="Add Certification" OnClick="btnAddCertification_Click" CssClass="add-button" />
            </div>

            <!-- Internships & Hobbies (Display) -->
            <div class="two-column display-section">
                <div class="column">
                    <div class="section">
                        <h2>Internships</h2>
                        <div class="job-entry">
                            <p class="job-title"><asp:Label runat="server" ID="lblInternJobTitle" /></p>
                            <p>at <span class="company-name"><asp:Label runat="server" ID="lblInternCompany" /></span></p>
                            <p class="date-range"><asp:Label runat="server" ID="lblInternStartDate" /> - <asp:Label runat="server" ID="lblInternEndDate" /></p>
                            <p><asp:Label runat="server" ID="lblInternDescription" /></p>
                        </div>
                    </div>
                </div>
                <div class="column">
                    <div class="section">
                        <h2>Hobbies</h2>
                        <ul class="hobbies-list">
                            <asp:Repeater ID="rptHobbies" runat="server">
                                <ItemTemplate>
                                    <li><asp:Literal ID="litHobby" runat="server" Text='<%# Eval("Name") %>' /></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
            </div>

            <!-- Internships (Edit) -->
            <div class="edit-section">
                <h2>Internships</h2>
                <div class="form-group">
                    <label>Job Title</label>
                    <asp:TextBox ID="txtInternJobTitle" runat="server" />
                </div>
                <div class="form-group">
                    <label>Company Name</label>
                    <asp:TextBox ID="txtInternCompany" runat="server" />
                </div>
                <div class="form-group">
                    <label>Start Date</label>
                    <asp:TextBox ID="txtInternStartDate" runat="server" TextMode="Date" />
                </div>
                <div class="form-group">
                    <label>End Date</label>
                    <asp:TextBox ID="txtInternEndDate" runat="server" TextMode="Date" />
                </div>
                <div class="form-group">
                    <label>Description</label>
                    <asp:TextBox ID="txtInternDescription" runat="server" TextMode="MultiLine" />
                </div>
            </div>

            <!-- Hobbies (Edit) -->
            <div class="edit-section">
                <h2>Hobbies</h2>
                <asp:Repeater ID="rptHobbiesEdit" runat="server" OnItemCommand="rptHobbiesEdit_ItemCommand">
                    <ItemTemplate>
                        <div class="repeater-item">
                            <div class="form-group">
                                <label>Hobby Name</label>
                                <asp:TextBox ID="txtHobbyName" runat="server" Text='<%# Eval("Name") %>' />
                            </div>
                            <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandName="Remove" CommandArgument='<%# Eval("ID") %>' CssClass="remove-button" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Button ID="btnAddHobby" runat="server" Text="Add Hobby" OnClick="btnAddHobby_Click" CssClass="add-button" />
            </div>

            <!-- Additional Information (Display) -->
            <div class="section display-section">
                <h2>Additional Information</h2>
                <ul class="bullet-points">
                    <asp:Repeater ID="rptCustomSelection" runat="server">
                        <ItemTemplate>
                            <li><asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>' /> (<asp:Label ID="lblStartDate" runat="server" Text='<%# FormatDate(Eval("StartDate").ToString()) %>' /> - <asp:Label ID="lblEndDate" runat="server" Text='<%# FormatDate(Eval("EndDate").ToString()) %>' />): <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' /></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>

            <!-- Additional Information (Edit) -->
            <div class="edit-section">
                <h2>Additional Information</h2>
                <asp:Repeater ID="rptCustomSelectionEdit" runat="server" OnItemCommand="rptCustomSelectionEdit_ItemCommand">
                    <ItemTemplate>
                        <div class="repeater-item">
                            <div class="form-group">
                                <label>Title</label>
                                <asp:TextBox ID="txtTitle" runat="server" Text='<%# Eval("Title") %>' />
                            </div>
                            <div class="form-group">
                                <label>Start Date</label>
                                <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' TextMode="Date" />
                            </div>
                            <div class="form-group">
                                <label>End Date</label>
                                <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' TextMode="Date" />
                            </div>
                            <div class="form-group">
                                <label>Description</label>
                                <asp:TextBox ID="txtDescription" runat="server" Text='<%# Eval("Description") %>' TextMode="MultiLine" />
                            </div>
                            <asp:Button ID="btnRemove" runat="server" Text="Remove" CommandName="Remove" CommandArgument='<%# Eval("ID") %>' CssClass="remove-button" />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Button ID="btnAddCustomSelection" runat="server" Text="Add Custom Section" OnClick="btnAddCustomSelection_Click" CssClass="add-button" />
            </div>

            <!-- Button Container -->
            <div class="button-container">
    <asp:Button ID="btnPrint" runat="server" Text="Download PDF" OnClick="btnPrint_Click" CssClass="action-button display-section" />
    <asp:Button ID="btnEdit" runat="server" Text="Edit Resume" OnClick="btnEdit_Click" CssClass="action-button display-section edit-btn" />
<asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="btnSave_Click" CssClass="action-button edit-section save-btn" />
<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="action-button edit-section cancel-btn" />
<asp:Button ID="btnBack" runat="server" Text="Back to Home" OnClick="btnBack_Click" CssClass="action-button back-btn" /></div>
        </div>
    </form>
</body>
</html>