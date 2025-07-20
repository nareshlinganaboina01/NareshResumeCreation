<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Resume7.aspx.cs" Inherits="NareshResumeCreation.Resume7" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Professional Resume</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style type="text/css">
        body {
            font-family: 'Arial', sans-serif;
            margin: 0;
            padding: 0;
            background-color: #f8f9fa;
            color: #212529;
            line-height: 1.6;
        }
        .resume-container {
            max-width: 900px;
            margin: 30px auto;
            background: #ffffff;
            box-shadow: 0 4px 20px rgba(0,0,0,0.08);
            border-radius: 12px;
            overflow: hidden;
        }
        .header {
            background: linear-gradient(135deg, #1e3c72 0%, #2a5298 100%);
            color: #ffffff;
            padding: 30px;
            text-align: center;
        }
        .header h1 {
            font-size: 32px;
            margin: 0;
            font-weight: 700;
            letter-spacing: 1px;
        }
        .header .job-title {
            font-size: 18px;
            font-style: normal;
            margin: 10px 0;
            opacity: 0.9;
        }
        .contact-info {
            font-size: 14px;
            margin-top: 15px;
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
            gap: 15px;
        }
        .contact-info p {
            margin: 0;
        }
        .content {
            padding: 30px;
        }
        .section {
            margin-bottom: 30px;
        }
        .section h2 {
            font-size: 20px;
            color: #1e3c72;
            border-bottom: 3px solid #2a5298;
            padding-bottom: 8px;
            margin-bottom: 15px;
            font-weight: 600;
            text-transform: uppercase;
            letter-spacing: 0.5px;
        }
        .item {
            margin-bottom: 20px;
        }
        .item-title {
            font-weight: 600;
            font-size: 16px;
            color: #2a5298;
        }
        .item-date {
            font-size: 14px;
            color: #6c757d;
            font-style: italic;
        }
        .item-desc {
            font-size: 14px;
            color: #343a40;
        }
        .list {
            list-style-type: none;
            padding: 0;
        }
        .list li {
            font-size: 14px;
            margin: 8px 0;
            position: relative;
            padding-left: 20px;
        }
        .list li::before {
            content: '•';
            position: absolute;
            left: 0;
            color: #2a5298;
            font-size: 18px;
        }
        .edit-input, .edit-textarea {
            width: 100%;
            padding: 10px;
            font-size: 14px;
            border: 1px solid #ced4da;
            border-radius: 6px;
            margin: 8px 0;
            box-sizing: border-box;
            transition: border-color 0.3s;
        }
        .edit-input:focus, .edit-textarea:focus {
            border-color: #2a5298;
            outline: none;
        }
        .edit-textarea {
            height: 120px;
            resize: vertical;
        }
        .edit-list-item {
            display: flex;
            align-items: center;
            margin: 10px 0;
            gap: 10px;
        }
        .edit-list-item input {
            flex-grow: 1;
        }
        .button-container {
            text-align: center;
            margin: 30px 0;
            display: flex;
            justify-content: center;
            gap: 10px;
            flex-wrap: wrap;
        }
        .btn {
            background: #2a5298;
            color: #ffffff;
            padding: 12px 24px;
            border: none;
            border-radius: 6px;
            cursor: pointer;
            font-size: 14px;
            font-weight: 500;
            transition: background 0.3s;
        }
        .btn:hover {
            background: #1e3c72;
        }
        .btn-delete {
            background: #dc3545;
        }
        .btn-delete:hover {
            background: #c82333;
        }
        @media (max-width: 768px) {
            .resume-container {
                margin: 15px;
            }
            .header {
                padding: 20px;
            }
            .content {
                padding: 20px;
            }
            .header h1 {
                font-size: 26px;
            }
            .header .job-title {
                font-size: 16px;
            }
            .contact-info {
                flex-direction: column;
                gap: 8px;
            }
            .btn {
                width: 100%;
                margin: 5px 0;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="resume-container">
            <asp:Panel ID="pnlView" runat="server" Visible="true">
                <!-- Header -->
                <div class="header">
                    <h1><asp:Label ID="text7FirstName" runat="server" /> <asp:Label ID="text7LastName" runat="server" /></h1>
                    <p class="job-title"><asp:Label ID="text7JobTitle" runat="server" /></p>
                    <div class="contact-info">
                        <p>📧 <asp:Label ID="text7Email" runat="server" /></p>
                        <p>📞 <asp:Label ID="text7Phone" runat="server" /></p>
                        <p>📍 <asp:Label ID="text7Address" runat="server" />, <asp:Label ID="text7City" runat="server" />, <asp:Label ID="text7State" runat="server" />,  <asp:Label ID="text7Country" runat="server" />, <asp:Label ID="text7PostalCode" runat="server" /></p>
                    </div>
                </div>

                <!-- Content -->
                <div class="content">
                    <!-- Career Objective -->
                    <div class="section">
                        <h2>Career Objective</h2>
                        <p><asp:Label ID="text7ProfessionalSummary" runat="server" /></p>
                    </div>

                    <!-- Skills -->
                    <div class="section">
                        <h2>Key Competencies</h2>
                        <ul class="list">
                            <asp:Repeater ID="rpt7Skills" runat="server">
                                <ItemTemplate>
                                    <li><asp:Literal ID="lit7Skill" runat="server" Text='<%# Eval("SkillName") %>' /></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>

                    <!-- Professional Experience -->
                    <div class="section">
                        <h2>Professional Experience</h2>
                        <asp:Panel ID="pnl7Fresher" runat="server" Visible="false">
                            <p>I am seeking opportunities to apply my academic knowledge as a fresher.</p>
                        </asp:Panel>
                        <asp:Panel ID="pnl7EmploymentHistory" runat="server" Visible="false">
                            <asp:Repeater ID="rpt7EmploymentHistory" runat="server">
                                <ItemTemplate>
                                    <div class="item">
                                        <div class="item-desc">
                                            I worked as <asp:Label ID="text7JobTitle" runat="server" Text='<%# Eval("JobTitle") %>' /> 
                                            at <asp:Label ID="text7Employer" runat="server" Text='<%# Eval("Employer") %>' /> 
                                            in <asp:Label ID="text7City" runat="server" Text='<%# Eval("City") %>' /> 
                                            from <%# Eval("StartDate", "{0:MMMM yyyy}") %> 
                                            to <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMMM yyyy}") %>, 
                                            where I <asp:Label ID="text7Description" runat="server" Text='<%# Eval("Description") %>' />.
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </asp:Panel>
                    </div>

                    <!-- Academic Qualifications -->
                    <div class="section">
                        <h2>Academic Qualifications</h2>
                        <asp:Repeater ID="rpt7Education" runat="server">
                            <ItemTemplate>
                                <div class="item">
                                    <div class="item-desc">
                                        I completed my <asp:Label ID="text7Degree" runat="server" Text='<%# Eval("Degree") %>' /> 
                                        from <asp:Label ID="text7SchoolName" runat="server" Text='<%# Eval("SchoolName") %>' /> 
                                        in <asp:Label ID="text7City" runat="server" Text='<%# Eval("City") %>' /> 
                                        between <%# Eval("StartDate", "{0:MMMM yyyy}") %> 
                                        and <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMMM yyyy}") %>. 
                                        <asp:Label ID="text7Description" runat="server" Text='<%# Eval("Description") %>' />.
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <!-- Certifications -->
                    <div class="section">
                        <h2>Professional Certifications</h2>
                        <asp:Repeater ID="rpt7Certifications" runat="server">
                            <ItemTemplate>
                                <div class="item">
                                    <div class="item-desc">
                                        I earned a <asp:Label ID="text7CourseName" runat="server" Text='<%# Eval("CourseName") %>' /> 
                                        certification from <asp:Label ID="text7Institution" runat="server" Text='<%# Eval("Institution") %>' /> 
                                        between <%# Eval("StartDate", "{0:MMMM yyyy}") %> 
                                        and <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMMM yyyy}") %>.
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <!-- Internship Experience -->
                    <div class="section">
                        <h2>Internship Experience</h2>
                        <div class="item">
                            <div class="item-desc">
                                <asp:Panel ID="pnlInternship" runat="server">
                                    I interned as <asp:Label ID="text7InternJobTitle" runat="server" /> 
                                    at <asp:Label ID="text7InternCompany" runat="server" /> 
                                    from <asp:Label ID="text7InternStartDate" runat="server" /> 
                                    to <asp:Label ID="text7InternEndDate" runat="server" />, 
                                    where I <asp:Label ID="text7InternDescription" runat="server" />.
                                </asp:Panel>
                                <asp:Panel ID="pnlNoInternship" runat="server" Visible="false">
                                    I have no internship experience recorded.
                                </asp:Panel>
                            </div>
                        </div>
                    </div>

                    <!-- Languages -->
                    <div class="section">
                        <h2>Linguistic Proficiencies</h2>
                        <ul class="list">
                            <asp:Repeater ID="rpt7Languages" runat="server">
                                <ItemTemplate>
                                    <li><asp:Literal ID="lit7Language" runat="server" Text='<%# Eval("LanguageName") %>' /></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>

                     <!-- Personal Interests -->
 <div class="section">
     <h2>Personal Interests</h2>
     <asp:Repeater ID="rpt7Hobbies" runat="server">
         <ItemTemplate>
             <div class="edit-list-item">
                 <asp:TextBox ID="txtHobbyName" runat="server" Text='<%# Eval("HobbyName") %>' CssClass="edit-input" />
<asp:Button ID="btnDeleteHobby" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Hobby_Command" CssClass="btn btn-delete" />
             </div>
         </ItemTemplate>
     </asp:Repeater>
     <asp:TextBox ID="TextBox1" runat="server" CssClass="edit-input" Placeholder="Add new interest" />
     <asp:Button ID="Button1" runat="server" Text="Add Interest" OnClick="btnAddHobby_Click" CssClass="btn" />
 </div>

                    <!-- Additional Achievements -->
                    <div class="section">
                        <h2>Additional Achievements</h2>
                        <asp:Repeater ID="rpt7CustomSelection" runat="server">
                            <ItemTemplate>
                                <div class="item">
                                    <div class="item-desc">
                                        I achieved <asp:Label ID="text7Title" runat="server" Text='<%# Eval("Title") %>' /> 
                                        from <%# Eval("StartDate", "{0:MMMM yyyy}") %> 
                                        to <%# string.IsNullOrEmpty(Eval("EndDate").ToString()) ? "Present" : Eval("EndDate", "{0:MMMM yyyy}") %>, 
                                        described as <asp:Label ID="text7Description" runat="server" Text='<%# Eval("Description") %>' />.
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>

                    <!-- Personal Details -->
                    <div class="section">
                        <h2>Personal Details</h2>
                        <p>Date of Birth: <asp:Label ID="text7DateOfBirth" runat="server" /></p>
                        <p>Place of Birth: <asp:Label ID="text7PlaceOfBirth" runat="server" /></p>
                        <p>Nationality: <asp:Label ID="text7Nationality" runat="server" /></p>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlEdit" runat="server" Visible="false">
                <!-- Edit Header -->
                <div class="header">
                    <asp:TextBox ID="txtEditFirstName" runat="server" CssClass="edit-input" Placeholder="First Name" />
                    <asp:TextBox ID="txtEditLastName" runat="server" CssClass="edit-input" Placeholder="Last Name" />
                    <asp:TextBox ID="txtEditJobTitle" runat="server" CssClass="edit-input" Placeholder="Professional Title" />
                    <div class="contact-info">
                        <p>Email: <asp:TextBox ID="txtEditEmail" runat="server" CssClass="edit-input" Placeholder="Email" /></p>
                        <p>Phone: <asp:TextBox ID="txtEditPhone" runat="server" CssClass="edit-input" Placeholder="Phone" /></p>
                        <p>Address: <asp:TextBox ID="txtEditAddress" runat="server" CssClass="edit-input" Placeholder="Address" />, 
                            <asp:TextBox ID="txtEditCity" runat="server" CssClass="edit-input" Placeholder="City" />,
                            <asp:TextBox ID="txtEditState" runat="server" CssClass="edit-input" Placeholder="State" />,
                            <asp:TextBox ID="txtEditPostalCode" runat="server" CssClass="edit-input" Placeholder="Postal Code" />, 
                            <asp:TextBox ID="txtEditCountry" runat="server" CssClass="edit-input" Placeholder="Country" /></p>
                    </div>
                </div>

                <!-- Edit Content -->
                <div class="content">
                    <!-- Career Objective -->
                    <div class="section">
                        <h2>Career Objective</h2>
                        <asp:TextBox ID="txtEditProfessionalSummary" runat="server" CssClass="edit-textarea" TextMode="MultiLine" Placeholder="Career Objective" />
                    </div>

                    <!-- Key Competencies -->
                    <div class="section">
                        <h2>Key Competencies</h2>
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

                    <!-- Professional Experience -->
                    <div class="section">
                        <h2>Professional Experience</h2>
                        <asp:CheckBox ID="chkFresher" runat="server" Text="I am a fresher (no professional experience)" AutoPostBack="true" OnCheckedChanged="chkFresher_CheckedChanged" />
                        <asp:Panel ID="pnlEditEmploymentHistory" runat="server" Visible="true">
                            <asp:Repeater ID="rptEditEmploymentHistory" runat="server">
                                <ItemTemplate>
                                    <div class="item">
                                        <p>
                                            Job Title: <asp:TextBox ID="txtJobTitle" runat="server" Text='<%# Eval("JobTitle") %>' CssClass="edit-input" /><br />
                                            Employer: <asp:TextBox ID="txtEmployer" runat="server" Text='<%# Eval("Employer") %>' CssClass="edit-input" /><br />
                                            City: <asp:TextBox ID="txtCity" runat="server" Text='<%# Eval("City") %>' CssClass="edit-input" /><br />
                                            Start Date: <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" TextMode="Date" /><br />
                                            End Date: <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" TextMode="Date" /><br />
                                            Responsibilities: <asp:TextBox ID="txtDescription" runat="server" Text='<%# Eval("Description") %>' CssClass="edit-textarea" TextMode="MultiLine" /><br />
                                            <asp:Button ID="btnDeleteEmployment" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Employment_Command" CssClass="btn btn-delete" />
                                        </p>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="item">
                                <p>
                                    Job Title: <asp:TextBox ID="txtNewJobTitle" runat="server" CssClass="edit-input" Placeholder="Job Title" /><br />
                                    Employer: <asp:TextBox ID="txtNewEmployer" runat="server" CssClass="edit-input" Placeholder="Employer" /><br />
                                    City: <asp:TextBox ID="txtNewCity" runat="server" CssClass="edit-input" Placeholder="City" /><br />
                                    Start Date: <asp:TextBox ID="txtNewStartDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                    End Date: <asp:TextBox ID="txtNewEndDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                    Responsibilities: <asp:TextBox ID="txtNewDescription" runat="server" CssClass="edit-textarea" TextMode="MultiLine" Placeholder="Responsibilities" /><br />
                                    <asp:Button ID="btnAddEmployment" runat="server" Text="Add Experience" OnClick="btnAddEmployment_Click" CssClass="btn" />
                                </p>
                            </div>
                        </asp:Panel>
                    </div>

                    <!-- Academic Qualifications -->
                    <div class="section">
                        <h2>Academic Qualifications</h2>
                        <asp:Repeater ID="rptEditEducation" runat="server">
                            <ItemTemplate>
                                <div class="item">
                                    <p>
                                        Degree: <asp:TextBox ID="txtDegree" runat="server" Text='<%# Eval("Degree") %>' CssClass="edit-input" /><br />
                                        Institution: <asp:TextBox ID="txtSchoolName" runat="server" Text='<%# Eval("SchoolName") %>' CssClass="edit-input" /><br />
                                        City: <asp:TextBox ID="txtCity" runat="server" Text='<%# Eval("City") %>' CssClass="edit-input" /><br />
                                        Start Date: <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" TextMode="Date" /><br />
                                        End Date: <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" TextMode="Date" /><br />
                                        Details: <asp:TextBox ID="txtDescription" runat="server" Text='<%# Eval("Description") %>' CssClass="edit-textarea" TextMode="MultiLine" /><br />
                                        <asp:Button ID="btnDeleteEducation" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Education_Command" CssClass="btn btn-delete" />
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="item">
                            <p>
                                Degree: <asp:TextBox ID="txtNewDegree" runat="server" CssClass="edit-input" Placeholder="Degree" /><br />
                                Institution: <asp:TextBox ID="txtNewSchoolName" runat="server" CssClass="edit-input" Placeholder="Institution" /><br />
                                City: <asp:TextBox ID="txtNewEducationCity" runat="server" CssClass="edit-input" Placeholder="City" /><br />
                                Start Date: <asp:TextBox ID="txtNewEducationStartDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                End Date: <asp:TextBox ID="txtNewEducationEndDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                Details: <asp:TextBox ID="txtNewEducationDescription" runat="server" CssClass="edit-textarea" TextMode="MultiLine" Placeholder="Details" /><br />
                                <asp:Button ID="btnAddEducation" runat="server" Text="Add Qualification" OnClick="btnAddEducation_Click" CssClass="btn" />
                            </p>
                        </div>
                    </div>

                    <!-- Professional Certifications -->
                    <div class="section">
                        <h2>Professional Certifications</h2>
                        <asp:Repeater ID="rptEditCertifications" runat="server">
                            <ItemTemplate>
                                <div class="item">
                                    <p>
                                        Certification: <asp:TextBox ID="txtCourseName" runat="server" Text='<%# Eval("CourseName") %>' CssClass="edit-input" /><br />
                                        Institution: <asp:TextBox ID="txtInstitution" runat="server" Text='<%# Eval("Institution") %>' CssClass="edit-input" /><br />
                                        Start Date: <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" TextMode="Date" /><br />
                                        End Date: <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" TextMode="Date" /><br />
                                        <asp:Button ID="btnDeleteCertification" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Certification_Command" CssClass="btn btn-delete" />
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="item">
                            <p>
                                Certification: <asp:TextBox ID="txtNewCourseName" runat="server" CssClass="edit-input" Placeholder="Certification Name" /><br />
                                Institution: <asp ="" 0;="" 1;="" 2;="" 3;="" 4;="" 5;="" 6;="" 7;="" 8;="" 9;="" 10;="" 11;="" 12;="" 13;="" 14;="" 15;="" 16;="" 17;="" 18;="" 19;="" 20;="" 21;="" 22;="" 23;="" 24;="" 25;="" 26;="" 27;="" 28;="" 29;="" 30;="" 31;="" 32;="" 33;="" 34;="" 35;="" 36;="" 37;="" 38;="" 39;="" 40;="" 41;="" 42;="" 43;="" 44;="" 45;="" 46;="" 47;="" 48;="" 49;="" 50;="" 51;="" 52;="" 53;="" 54;="" 55;="" 56;="" 57;="" 58;="" 59;="" 60;="" 61;="" 62;="" 63;="" 64;="" 65;="" 66;="" 67;="" 68;="" 69;="" 70;="" 71;="" 72;="" 73;="" 74;="" 75;="" 76;="" 77;="" 78;="" 79;="" 80;="" 81;="" 82;="" 83;="" 84;="" 85;="" 86;="" 87;="" 88;="" 89;="" 90;="" 91;="" 92;="" 93;="" 94;="" 95;="" 96;="" 97;="" 98;="" 99;="" 100;="" 101;="" 102;="" 103;="" 104;="" 105;="" 106;="" 107;="" 108;="" 109;="" 110;="" 111;="" 112;="" 113;="" 114;="" 115;="" 116;="" 117;="" 118;="" 119;="" 120;="" 121;="" 122;="" 123;="" 124;="" 125;="" 126;="" 127;="" 128;="" 129;="" 130;="" 131;="" 132;="" 133;="" 134;="" 135;="" 136;="" 137;="" 138;="" 139;="" 140;="" 141;="" 142;="" 143;="" 144;="" 145;="" 146;="" 147;="" 148;="" 149;="" 150;="" 151;="" 152;="" 153;="" 154;="" 155;="" 156;="" 157;="" 158;="" 159;="" 160;="" 161;="" 162;="" 163;="" 164;="" 165;="" 166;="" 167;="" 168;="" 169;="" 170;="" 171;="" 172;="" 173;="" 174;="" 175;="" 176;="" 177;="" 178;="" 179;="" 180;="" 181;="" 182;="" 183;="" 184;="" 185;="" 186;="" 187;="" 188;="" 189;="" 190;="" 191;="" 192;="" 193;="" 194;="" 195;="" 196;="" 197;="" 198;="" 199;="" 200;="" 201;="" 202;="" 203;="" 204;="" 205;="" 206;="" 207;="" 208;="" 209;="" 210;="" 211;="" 212;="" 213;="" 214;="" 215;="" 216;="" 217;="" 218;="" 219;="" 220;="" 221;="" 222;="" 223;="" 224;="" 225;="" 226;="" 227;="" 228;="" 229;="" 230;="" 231;="" 232;="" 233;="" 234;="" 235;="" 236;="" 237;="" 238;="" 239;="" 240;="" 241;="" 242;="" 243;="" 244;="" 245;="" 246;="" 247;="" 248;="" 249;="" 250;="" 251;="" 252;="" 253;="" 254;="" 255;="" 256;="" 257;="" 258;="" 259;="" 260;="" 261;="" 262;="" 263;="" 264;="" 265;="" 266;="" 267;="" 268;="" 269;="" 270;="" 271;="" 272;="" 273;="" 274;="" 275;="" 276;="" 277;="" 278;="" 279;="" 280;="" 281;="" 282;="" 283;="" 284;="" 285;="" 286;="" 287;="" 288;="" 289;="" 290;="" 291;="" 292;="" 293;="" 294;="" 295;="" 296;="" 297;="" 298;="" 299;="" 300;="" 301;="" 302;="" 303;="" 304;="" 305;="" 306;="" 307;="" 308;="" 309;="" 310;="" 311;="" 312;="" 313;="" 314;="" 315;="" 316;="" 317;="" 318;="" 319;="" 320;="" 321;="" 322;="" 323;="" 324;="" 325;="" 326;="" 327;="" 328;="" 329;="" 330;="" 331;="" 332;="" 333;="" 334;="" 335;="" 336;="" 337;="" 338;="" 339;="" 340;="" 341;="" 342;="" 343;="" 344;="" 345;="" 346;="" 347;="" 348;="" 349;="" 350;="" 351;="" 352;="" 353;="" 354;="" 355;="" 356;="" 357;="" 358;="" 359;="" 360;="" 361;="" 362;="" 363;="" 364;="" 365;="" 366;="" 367;="" 368;="" 369;="" 370;="" 371;="" 372;="" 373;="" 374;="" 375;="" 376;="" 377;="" 378;="" 379;="" 380;="" 381;="" 382;="" 383;="" 384;="" 385;="" 386;="" 387;="" 388;="" 389;="" 390;="" 391;="" 392;="" 393;="" 394;="" 395;="" 396;="" 397;="" 398;="" 399;="" 400;="" 401;="" 402;="" 403;="" 404;="" 405;="" 406;="" 407;="" 408;="" 409;="" 410;="" 411;="" 412;="" 413;="" 414;="" 415;="" 416;="" 417;="" 418;="" 419;="" 420;="" 421;="" 422;="" 423;="" 424;="" 425;="" 426;="" 427;="" 428;="" 429;="" 430;="" 431;="" 432;="" 433;="" 434;="" 435;="" 436;="" 437;="" 438;="" 439;="" 440;="" 441;="" 442;="" 443;="" 444;="" 445;="" 446;="" 447;="" 448;="" 449;="" 450;="" 451;="" 452;="" 453;="" 454;="" 455;="" 456;="" 457;="" 458;="" 459;="" 460;="" 461;="" 462;="" 463;="" 464;="" 465;="" 466;="" 467;="" 468;="" 469;="" 470;="" 471;="" 472;="" 473;="" 474;="" 475;="" 476;="" 477;="" 478;="" 479;="" 480;="" 481;="" 482;="" 483;="" 484;="" 485;="" 486;="" 487;="" 488;="" 489;="" 490;="" 491;="" 492;="" 493;="" 494;="" 495;="" 496;="" 497;="" 498;="" 499;="" 500;="" 501;="" 502;="" 503;="" 504;="" 505;="" 506;="" 507;="" 508;="" 509;="" 510;="" 511;="" 512;="" 513;="" 514;="" 515;="" 516;="" 517;="" 518;="" 519;="" 520;="" 521;="" 522;="" 523;="" 524;="" 525;="" 526;="" 527;="" 528;="" 529;="" 530;="" 531;="" 532;="" 533;="" 534;="" 535;="" 536;="" 537;="" 538;="" 539;="" 540;="" 541;="" 542;="" 543;="" 544;="" 545;="" 546;="" 547;="" 548;="" 549;="" 550;="" 551;="" 552;="" 553;="" 554;="" 555;="" 556;="" 557;="" 558;="" 559;="" 560;="" 561;="" 562;="" 563;="" 564;="" 565;="" 566;="" 567;="" 568;="" 569;="" 570;="" 571;="" 572;="" 573;="" 574;="" 575;="" 576;="" 577;="" 578;="" 579;="" 580;="" 581;="" 582;="" 583;="" 584;="" 585;="" 586;="" 587;="" 588;="" 589;="" 590;="" 591;="" 592;="" 593;="" 594;="" 595;="" 596;="" 597;="" 598;="" 599;="" 600;="" 601;="" 602;="" 603;="" 604;="" 605;="" 606;="" 607;="" 608;="" 609;="" 610;="" 611;="" 612;="" 613;="" 614;="" 615;="" 616;="" 617;="" 618;="" 619;="" 620;="" 621;="" 622;="" 623;="" 624;="" 625;="" 626;="" 627;="" 628;="" 629;="" 630;="" 631;="" 632;="" 633;="" 634;="" 635;="" 636;="" 637;="" 638;="" 639;="" 640;="" 641;="" 642;="" 643;="" 644;="" 645;="" 646;="" 647;="" 648;="" 649;="" 650;="" 651;="" 652;="" 653;="" 654;="" 655;="" 656;="" 657;="" 658;="" 659;="" 660;="" 661;="" 662;="" 663;="" 664;="" 665;="" 666;="" 667;="" 668;="" 669;="" 670;="" 671;="" 672;="" 673;="" 674;="" 675;="" 676;="" 677;="" 678;="" 679;="" 680;="" 681;="" 682;="" 683;="" 684;="" 685;="" 686;="" 687;="" 688;="" 689;="" 690;="" 691;="" 692;="" 693;="" 694;="" 695;="" 696;="" 697;="" 698;="" 699;="" 700;="" 701;="" 702;="" 703;="" 704;="" 705;="" 706;="" 707;="" 708;="" 709;="" 710;="" 711;="" 712;="" 713;="" 714;="" 715;="" 716;="" 717;="" 718;="" 719;="" 720;="" 721;="" 722;="" 723;="" 724;="" 725;="" 726;="" 727;="" 728;="" 729;="" 730;="" 731;="" 732;="" 733;="" 734;="" 735;="" 736;="" 737;="" 738;="" 739;="" 740;="" 741;="" 742;="" 743;="" 744;="" 745;="" 746;="" 747;="" 748;="" 749;="" 750;="" 751;="" 752;="" 753;="" 754;="" 755;="" 756;="" 757;="" 758;="" 759;="" 760;="" 761;="" 762;="" 763;="" 764;="" 765;="" 766;="" 767;="" 768;="" 769;="" 770;="" 771;="" 772;="" 773;="" 774;="" 775;="" 776;="" 777;="" 778;="" 779;="" 780;="" 781;="" 782;="" 783;="" 784;="" 785;="" 786;="" 787;="" 788;="" 789;="" 790;="" 791;="" 792;="" 793;="" 794;="" 795;="" 796;="" 797;="" 798;="" 799;="" 800;="" 801;="" 802;="" 803;="" 804;="" 805;="" 806;="" 807;="" 808;="" 809;="" 810;="" 811;="" 812;="" 813;="" 814;="" 815;="" 816;="" 817;="" 818;="" 819;="" 820;="" 821;="" 822;="" 823;="" 824;="" 825;="" 826;="" 827;="" 828;="" 829;="" 830;="" 831;="" 832;="" 833;="" 834;="" 835;="" 836;="" 837;="" 838;="" 839;="" 840;="" 841;="" 842;="" 843;="" 844;="" 845;="" 846;="" 847;="" 848;="" 849;="" 850;="" 851;="" 852;="" 853;="" 854;="" 855;="" 856;="" 857;="" 858;="" 859;="" 860;="" 861;="" 862;="" 863;="" 864;="" 865;="" 866;="" 867;="" 868;="" 869;="" 870;="" 871;="" 872;="" 873;="" 874;="" 875;="" 876;="" 877;="" 878;="" 879;="" 880;="" 881;="" 882;="" 883;="" 884;="" 885;="" 886;="" 887;="" 888;="" 889;="" 890;="" 891;="" 892;="" 893;="" 894;="" 895;="" 896;="" 897;="" 898;="" 899;="" 900;="" 901;="" 902;="" 903;="" 904;="" 905;="" 906;="" 907;="" 908;="" 909;="" 910;="" 911;="" 912;="" 913;="" 914;="" 915;="" 916;="" 917;="" 918;="" 919;="" 920;="" 921;="" 922;="" 923;="" 924;="" 925;="" 926;="" 927;="" 928;="" 929;="" 930;="" 931;="" 932;="" 933;="" 934;="" 935;="" 936;="" 937;="" 938;="" 939;="" 940;="" 941;="" 942;="" 943;="" 944;="" 945;="" 946;="" 947;="" 948;="" 949;="" 950;="" 951;="" 952;="" 953;="" 954;="" 955;="" 956;="" 957;="" 958;="" 959;="" 960;="" 961;="" 962;="" 963;="" 964;="" 965;="" 966;="" 967;="" 968;="" 969;="" 970;="" 971;="" 972;="" 973;="" 974;="" 975;="" 976;="" 977;="" 978;="" 979;="" 980;="" 981;="" 982;="" 983;="" 984;="" 985;="" 986;="" 987;="" 988;="" 989;="" 990;="" 991;="" 992;="" 993;="" 994;="" 995;="" 996;="" 997;="" 998;="" 999;="" 1000;="" 1001;="" 1002;="" 1003;="" 1004;="" 1005;="" 1006;="" 1007;="" 1008;="" 1009;="" 1010;="" 1011;="" 1012;="" 1013;="" 1014;="" 1015;="" 1016;="" 1017;="" 1018;="" 1019;="" 1020;="" 1021;="" 1022;="" 1023;="" 1024;="" 1025;="" 1026;="" 1027;="" 1028;="" 1029;="" 1030;="" 1031;="" 1032;="" 1033;="" 1034;="" 1035;="" 1036;="" 1037;="" 1038;="" 1039;="" 1040;="" 1041;="" 1042;="" 1043;="" 1044;="" 1045;="" 1046;="" 1047;="" 1048;="" 1049;="" 1050;="" 1051;="" 1052;="" 1053;="" 1054;="" 1055;="" 1056;="" 1057;="" 1058;="" 1059;="" 1060;="" 1061;="" 1062;="" 1063;="" 1064;="" 1065;="" 1066;="" 1067;="" 1068;="" 1069;="" 1070;="" 1071;="" 1072;="" 1073;="" 1074;="" 1075;="" 1076;="" 1077;="" 1078;="" 1079;="" 1080;="" 1081;="" 1082;="" 1083;="" 1084;="" 1085;="" 1086;="" 1087;="" 1088;="" 1089;="" 1090;="" 1091;="" 1092;="" 1093;="" 1094;="" 1095;="" 1096;="" 1097;="" 1098;="" 1099;="" 1100;="" 1101;="" 1102;="" 1103;="" 1104;="" 1105;="" 1106;="" 1107;="" 1108;="" 1109;="" 1110;="" 1111;="" 1112;="" 1113;="" 1114;="" 1115;="" 1116;="" 1117;="" 1118;="" 1119;="" 1120;="" 1121;="" 1122;="" 1123;="" 1124;="" 1125;="" 1126;="" 1127;="" 1128;="" 1129;="" 1130;="" 1131;="" 1132;="" 1133;="" 1134;="" 1135;="" 1136;="" 1137;="" 1138;="" 1139;="" 1140;="" 1141;="" 1142;="" 1143;="" 1144;="" 1145;="" 1146;="" 1147;="" 1148;="" 1149;="" 1150;="" 1151;="" 1152;="" 1153;="" 1154;="" 1155;="" 1156;="" 1157;="" 1158;="" 1159;="" 1160;="" 1161;="" 1162;="" 1163;="" 1164;="" 1165;="" 1166;="" 1167;="" 1168;="" 1169;="" 1170;="" 1171;="" 1172;="" 1173;="" 1174;="" 1175;="" 1176;="" 1177;="" 1178;="" 1179;="" 1180;="" 1181;="" 1182;="" 1183;="" 1184;="" 1185;="" 1186;="" 1187;="" 1188;="" 1189;="" 1190;="" 1191;="" 1192;="" 1193;="" 1194;="" 1195;="" 1196;="" 1197;="" 1198;="" 1199;="" 1200;="" 1201;="" 1202;="" 1203;="" 1204;="" 1205;="" 1206;="" 1207;="" 1208;="" 1209;="" 1210;="" 1211;="" 1212;="" 1213;="" 1214;="" 1215;="" 1216;="" 1217;="" 1218;="" 1219;="" 1220;="" 1221;="" 1222;="" 1223;="" 1224;="" 1225;="" 1226;="" 1227;="" 1228;="" 1229;="" 1230;="" 1231;="" 1232;="" 1233;="" 1234;="" 1235;="" 1236;="" 1237;="" 1238;="" 1239;="" 1240;="" 1241;="" 1242;="" 1243;="" 1244;="" 1245;="" 1246;="" 1247;="" 1248;="" 1249;="" 1250;="" 1251;="" 1252;="" 1253;="" 1254;="" 1255;="" 1256;="" 1257;="" 1258;="" 1259;="" 1260;="" 1261;="" 1262;="" 1263;="" 1264;="" 1265;="" 1266;="" 1267;="" 1268;="" 1269;="" 1270;="" 1271;="" 1272;="" 1273;="" 1274;="" 1275;="" 1276;="" 1277;="" 1278;="" 1279;="" 1280;="" 1281;="" 1282;="" 1283;="" 1284;="" 1285;="" 1286;="" 1287;="" 1288;="" 1289;="" 1290;="" 1291;="" 1292;="" 1293;="" 1294;="" 1295;="" 1296;="" 1297;="" 1298;="" 1299;="" 1300;="" 1301;="" 1302;="" 1303;="" 1304;="" 1305;="" 1306;="" 1307;="" 1308;="" 1309;="" 1310;="" 1311;="" 1312;="" 1313;="" 1314;="" 1315;="" 1316;="" 1317;="" 1318;="" 1319;="" 1320;="" 1321;="" 1322;="" 1323;="" 1324;="" 1325;="" 1326;="" 1327;="" 1328;="" 1329;="" 1330;="" 1331;="" 1332;="" 1333;="" 1334;="" 1335;="" 1336;="" 1337;="" 1338;="" 1339;="" 1340;="" 1341;="" 1342;="" 1343;="" 1344;="" 1345;="" 1346;="" 1347;="" 1348;="" 1349;="" 1350;="" 1351;="" 1352;="" 1353;="" 1354;="" 1355;="" 1356;="" 1357;="" 1358;="" 1359;="" 1360;="" 1361;="" 1362;="" 

Institution: <asp:TextBox ID="txtNewInstitution" runat="server" CssClass="edit-input" Placeholder="Institution" /><br />
                                Start Date: <asp:TextBox ID="txtNewCertStartDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                End Date: <asp:TextBox ID="txtNewCertEndDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                <asp:Button ID="btnAddCertification" runat="server" Text="Add Certification" OnClick="btnAddCertification_Click" CssClass="btn" />
                            </p>
                        </div>
                    </div>

                    <!-- Internship Experience -->
                    <div class="section">
                        <h2>Internship Experience</h2>
                        <div class="item">
                            <p>
                                Job Title: <asp:TextBox ID="txtEditInternJobTitle" runat="server" CssClass="edit-input" Placeholder="Job Title" /><br />
                                Company: <asp:TextBox ID="txtEditInternCompany" runat="server" CssClass="edit-input" Placeholder="Company" /><br />
                                Start Date: <asp:TextBox ID="txtEditInternStartDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                End Date: <asp:TextBox ID="txtEditInternEndDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                Responsibilities: <asp:TextBox ID="txtEditInternDescription" runat="server" CssClass="edit-textarea" TextMode="MultiLine" Placeholder="Responsibilities" />
                            </p>
                        </div>
                    </div>

                    <!-- Linguistic Proficiencies -->
                    <div class="section">
                        <h2>Linguistic Proficiencies</h2>
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

                    <!-- Personal Interests -->
                    <div class="section">
                        <h2>Personal Interests</h2>
                        <asp:Repeater ID="rptEditHobbies" runat="server">
                            <ItemTemplate>
                                <div class="edit-list-item">
                                    <asp:TextBox ID="txtHobbyName" runat="server" Text='<%# Eval("Name") %>' CssClass="edit-input" />
                                    <asp:Button ID="btnDeleteHobby" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="Hobby_Command" CssClass="btn btn-delete" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:TextBox ID="txtNewHobby" runat="server" CssClass="edit-input" Placeholder="Add new interest" />
                        <asp:Button ID="btnAddHobby" runat="server" Text="Add Interest" OnClick="btnAddHobby_Click" CssClass="btn" />
                    </div>

                    <!-- Additional Achievements -->
                    <div class="section">
                        <h2>Additional Achievements</h2>
                        <asp:Repeater ID="rptEditCustomSelection" runat="server">
                            <ItemTemplate>
                                <div class="item">
                                    <p>
                                        Title: <asp:TextBox ID="txtTitle" runat="server" Text='<%# Eval("Title") %>' CssClass="edit-input" /><br />
                                        Start Date: <asp:TextBox ID="txtStartDate" runat="server" Text='<%# Eval("StartDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" TextMode="Date" /><br />
                                        End Date: <asp:TextBox ID="txtEndDate" runat="server" Text='<%# Eval("EndDate", "{0:yyyy-MM-dd}") %>' CssClass="edit-input" TextMode="Date" /><br />
                                        Details: <asp:TextBox ID="txtDescription" runat="server" Text='<%# Eval("Description") %>' CssClass="edit-textarea" TextMode="MultiLine" /><br />
                                        <asp:Button ID="btnDeleteCustom" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' OnCommand="CustomSelection_Command" CssClass="btn btn-delete" />
                                    </p>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="item">
                            <p>
                                Title: <asp:TextBox ID="txtNewCustomTitle" runat="server" CssClass="edit-input" Placeholder="Achievement Title" /><br />
                                Start Date: <asp:TextBox ID="txtNewCustomStartDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                End Date: <asp:TextBox ID="txtNewCustomEndDate" runat="server" CssClass="edit-input" TextMode="Date" /><br />
                                Details: <asp:TextBox ID="txtNewCustomDescription" runat="server" CssClass="edit-textarea" TextMode="MultiLine" Placeholder="Details" /><br />
                                <asp:Button ID="btnAddCustom" runat="server" Text="Add Achievement" OnClick="btnAddCustom_Click" CssClass="btn" />
                            </p>
                        </div>
                    </div>

                    <!-- Personal Details -->
                    <div class="section">
                        <h2>Personal Details</h2>
                        <p>Date of Birth: <asp:TextBox ID="txtEditDateOfBirth" runat="server" CssClass="edit-input" TextMode="Date" /></p>
                        <p>Place of Birth: <asp:TextBox ID="txtEditPlaceOfBirth" runat="server" CssClass="edit-input" Placeholder="Place of Birth" /></p>
                        <p>Nationality: <asp:TextBox ID="txtEditNationality" runat="server" CssClass="edit-input" Placeholder="Nationality" /></p>
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