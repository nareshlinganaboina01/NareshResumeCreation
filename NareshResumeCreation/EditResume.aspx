<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditResume.aspx.cs" Inherits="NareshResumeCreation.EditResume" %>

<!DOCTYPE html>
<html>
<head>
    <title>Edit Resume</title>
    <style>
        body {
            font-family: 'Poppins', sans-serif;
            margin: 0;
            padding: 0;
            background: linear-gradient(120deg, #f6d365, #fda085);
            color: #2c3e50;
        }
        .edit-container {
            max-width: 1000px;
            margin: 40px auto;
            padding: 40px;
            background: #ffffff;
            border-radius: 15px;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);
        }
        h2 {
            font-size: 24px;
            color: #e67e22;
            margin-bottom: 20px;
            text-transform: uppercase;
        }
        .section {
            margin-bottom: 30px;
            padding: 20px;
            background: #f2f6fa;
            border-radius: 10px;
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
        .save-button, .cancel-button {
            background: linear-gradient(45deg, #e74c3c, #c0392b);
            color: #fff;
            padding: 15px 30px;
            border: none;
            border-radius: 30px;
            cursor: pointer;
            font-size: 18px;
            margin: 10px;
        }
        .save-button:hover, .cancel-button:hover {
            transform: scale(1.1);
            box-shadow: 0 8px 20px rgba(0,0,0,0.2);
        }
        .cancel-button {
            background: linear-gradient(45deg, #7f8c8d, #95a5a6);
        }
        .button-container {
            text-align: center;
            margin-top: 30px;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="edit-container">
            <!-- Personal Details -->
            <div class="section">
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

            <!-- Education -->
            <div class="section">
                <h2>Education</h2>
                <asp:Repeater ID="rptEducation" runat="server" OnItemCommand="rptEducation_ItemCommand">
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

            <!-- Employment History -->
            <div class="section">
                <h2>Employment History</h2>
                <asp:Repeater ID="rptEmploymentHistory" runat="server" OnItemCommand="rptEmploymentHistory_ItemCommand">
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

            <!-- Skills -->
            <div class="section">
                <h2>Skills</h2>
                <asp:Repeater ID="rptSkills" runat="server" OnItemCommand="rptSkills_ItemCommand">
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

            <!-- Languages -->
            <div class="section">
                <h2>Languages</h2>
                <asp:Repeater ID="rptLanguages" runat="server" OnItemCommand="rptLanguages_ItemCommand">
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

            <!-- Certifications -->
            <div class="section">
                <h2>Certifications</h2>
                <asp:Repeater ID="rptCertifications" runat="server" OnItemCommand="rptCertifications_ItemCommand">
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

            <!-- Internships -->
            <div class="section">
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

            <!-- Hobbies -->
            <div class="section">
                <h2>Hobbies</h2>
                <asp:Repeater ID="rptHobbies" runat="server" OnItemCommand="rptHobbies_ItemCommand">
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

            <!-- Additional Information -->
            <div class="section">
                <h2>Additional Information</h2>
                <asp:Repeater ID="rptCustomSelection" runat="server" OnItemCommand="rptCustomSelection_ItemCommand">
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

            <!-- Buttons -->
            <div class="button-container">
                <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="btnSave_Click" CssClass="save-button" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="cancel-button" />
            </div>
        </div>
    </form>
</body>
</html>