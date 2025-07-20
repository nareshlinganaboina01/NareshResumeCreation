<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="NareshResumeCreation.Home" %>

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Naresh Resume Creation - Home</title>
    
    <!-- Font Awesome for Icons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <style>
        /* Global Styling */
        body {
            font-family: 'Poppins', sans-serif;
            background: linear-gradient(135deg, #ff6b6b, #ffbe76, #80ed99);
            margin: 0;
            padding: 0;
            color: #2d1b3f;
        }

        /* Navigation Bar */
        .navbar {
            background: linear-gradient(135deg, #4a00e0, #8e2de2);
            padding: 10px 20px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.25);
            position: fixed;
            top: 0;
            width: 100%;
            z-index: 1000;
        }

        .navbar ul {
            list-style: none;
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: space-around;
            align-items: center;
        }

        .navbar ul li a, .navbar ul li .nav-link {
            color: #f3e8ff;
            text-decoration: none;
            font-weight: 600;
            font-size: 14px;
            padding: 8px 15px;
            transition: all 0.3s ease;
            border-radius: 5px;
        }

        .navbar ul li a:hover, .navbar ul li .nav-link:hover {
            background: linear-gradient(135deg, #7b3fe4, #b766f4);
            color: #fff;
        }

        .nav-link {
            background: none;
            border: none;
            display: inline-block;
        }

        /* Header Section */
        .header {
            background: linear-gradient(135deg, #ff9f1c, #ff6200);
            color: #fff;
            padding: 5px 20px;
            text-align: center;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.25);
            margin-top: 50px;
        }

        .header h1 {
            margin: 0;
            font-size: 32px;
            font-weight: 700;
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.3);
        }

        .header p {
            margin: 8px 0 0;
            font-size: 16px;
            font-weight: 400;
            max-width: 600px;
            margin-left: auto;
            margin-right: auto;
            text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.2);
        }

        /* Scrolling Text */
        .scrolling-text {
            margin-top: 15px;
            background: linear-gradient(135deg, rgba(255, 255, 255, 0.2), rgba(255, 255, 255, 0.1));
            padding: 8px;
            border-radius: 5px;
            font-size: 14px;
            font-weight: 500;
            color: #ffffff;
            white-space: nowrap;
            overflow: hidden;
            width: 100%;
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.2);
            position: relative;
            height: 30px;
        }

        .scrolling-text span {
            display: inline-block;
            position: absolute;
            padding: 6px 12px;
            animation: scroll 20s linear infinite;
        }

        @keyframes scroll {
            0% { left: 100%; transform: translateX(0); }
            50% { left: 0; transform: translateX(-100%); }
            100% { left: 100%; transform: translateX(0); }
        }

        /* Main Content */
        .main-content {
            padding: 40px 20px;
            margin-bottom: 80px;
            background: linear-gradient(135deg, rgba(255, 255, 255, 0.3), rgba(255, 255, 255, 0.1));
        }

        /* Container Styling */
        .container {
            background: linear-gradient(135deg, #fff7ed, #fef3c7);
            padding: 30px;
            border-radius: 20px;
            box-shadow: 0 8px 24px rgba(0, 0, 0, 0.2);
            margin: 20px auto;
            max-width: 800px;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
        }

        .container:hover {
            transform: translateY(-5px);
            box-shadow: 0 12px 32px rgba(0, 0, 0, 0.25);
        }

        h2, h3 {
            background: linear-gradient(135deg, #00d4ff, #007bff);
            color: #fff;
            padding: 15px;
            border-radius: 10px;
            margin-bottom: 20px;
            font-weight: 600;
            font-size: 24px;
            text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.3);
            text-align: center;
        }

        /* Form Fields */
        .form-group {
            margin-bottom: 25px;
            position: relative;
        }

        label {
            font-weight: 500;
            margin-bottom: 8px;
            display: block;
            color: #2d1b3f;
        }

        .input-container {
            position: relative;
            display: flex;
            align-items: center;
        }

        .input-container i {
            position: absolute;
            left: 12px;
            color: #6b46c1;
            font-size: 18px;
        }

        .input-field {
            width: 100%;
            padding: 12px 12px 12px 40px;
            border: 2px solid #d6bcfa;
            border-radius: 10px;
            font-size: 16px;
            transition: all 0.3s ease;
            background: linear-gradient(135deg, #f3e8ff, #e9d8fd);
            box-shadow: inset 0 2px 4px rgba(0, 0, 0, 0.1);
        }

        .input-field:focus {
            border-color: #007bff;
            box-shadow: 0 0 12px rgba(0, 123, 255, 0.5);
            background: #fff;
            outline: none;
        }

        .input-field[type="date"] {
            padding: 12px;
        }

        textarea.input-field {
            height: 100px;
            resize: vertical;
        }

        /* Buttons */
        .btn-submit, .btn-save, .btn-add, .btn-generate, .btn-logout {
            background: linear-gradient(135deg, #ff6b6b, #ff4757);
            color: #fff;
            border: none;
            padding: 14px;
            border-radius: 10px;
            font-size: 16px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            box-shadow: 0 4px 12px rgba(255, 107, 107, 0.4);
            width: 100%;
            max-width: 200px;
            margin: 10px auto;
            display: block;
        }

        .btn-submit:hover, .btn-save:hover, .btn-add:hover, .btn-generate:hover, .btn-logout:hover {
            background: linear-gradient(135deg, #ff4757, #ff6b6b);
            transform: scale(1.03);
            box-shadow: 0 6px 16px rgba(255, 107, 107, 0.5);
        }

        .btn-delete {
            background: linear-gradient(135deg, #e53e3e, #c53030);
            color: #fff;
            border: none;
            padding: 8px 16px;
            border-radius: 8px;
            font-size: 14px;
            cursor: pointer;
            transition: all 0.3s ease;
            box-shadow: 0 2px 8px rgba(229, 62, 62, 0.4);
        }

        .btn-delete:hover {
            background: linear-gradient(135deg, #c53030, #e53e3e);
            transform: scale(1.05);
        }

        .btn-cancel {
            background: linear-gradient(135deg, #a0aec0, #718096);
            color: #fff;
            border: none;
            padding: 14px;
            border-radius: 10px;
            font-size: 16px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            box-shadow: 0 4px 12px rgba(160, 174, 192, 0.4);
            width: 100%;
            max-width: 200px;
            margin: 10px auto;
            display: block;
        }

        .btn-cancel:hover {
            background: linear-gradient(135deg, #718096, #a0aec0);
            transform: scale(1.03);
        }

        /* Dropdown Styling */
        .dropdown {
            width: 100%;
            max-width: 200px;
            padding: 12px;
            border: 2px solid #d6bcfa;
            border-radius: 10px;
            font-size: 16px;
            background: linear-gradient(135deg, #f3e8ff, #e9d8fd);
            box-shadow: inset 0 2px 4px rgba(0, 0, 0, 0.1);
            margin: 10px auto;
            display: block;
            transition: all 0.3s ease;
        }

        .dropdown:focus {
            border-color: #007bff;
            box-shadow: 0 0 12px rgba(0, 123, 255, 0.5);
            background: #fff;
            outline: none;
        }

        /* Repeater Items */
        .employment-entry, .education-item, .website-item, .skill-item, .internship-item, .custom-item {
            background: linear-gradient(135deg, #f3e8ff, #e9d8fd);
            padding: 15px;
            border-radius: 10px;
            margin-bottom: 15px;
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
        }

        /* Error and Success Messages */
        .error-message {
            color: #9f1239;
            font-size: 14px;
            margin-top: 5px;
            display: block;
            font-weight: 500;
            text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.1);
        }

        .success-message {
            color: #2ecc71;
            font-size: 14px;
            margin-top: 15px;
            display: block;
            font-weight: 500;
        }

        /* About Section */
        .about-section {
            background: linear-gradient(135deg, #c4b5fd, #a78bfa);
            padding: 50px 20px;
            text-align: center;
            margin: 40px 0 80px 0;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.2);
        }

        .about-section h3 {
            background: linear-gradient(135deg, #80ed99, #fff7ed);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            font-size: 30px;
            margin-bottom: 20px;
            font-weight: 600;
        }

        .about-section p {
            max-width: 800px;
            margin: 0 auto;
            font-size: 16px;
            line-height: 1.7;
            color: #2d1b3f;
        }

        /* Footer Section */
        .footer {
            background: linear-gradient(135deg, #4a00e0, #8e2de2);
            color: #fff;
            text-align: center;
            padding: 1px 1px;
            box-shadow: 0 -2px 8px rgba(0, 0, 0, 0.25);
            position: fixed;
            bottom: 0;
            width: 100%;
            z-index: 1000;
        }

        .footer p {
            margin: 3px 0;
            font-size: 12px;
            text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.2);
        }

        .footer a {
            color: #fef3c7;
            text-decoration: none;
            font-weight: 500;
        }

        .footer a:hover {
            color: #fff;
            text-decoration: underline;
        }

        /* DateTime Styling */
        #datetime {
            font-size: 16px;
            font-weight: 600;
            background: rgba(0, 0, 0, 0.5);
            padding: 8px 15px;
            border-radius: 8px;
            display: inline-block;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.2);
            margin-top: 5px;
        }

        .day {
            animation: colorChangeDay 8s infinite;
        }

        .date {
            animation: colorChangeDate 8s infinite;
        }

        .time {
            animation: colorChangeTime 8s infinite;
        }

        @keyframes colorChangeDay {
            0% { color: #ff00ff; } /* Magenta */
            20% { color: #00ff00; } /* Lime */
            40% { color: #ff9900; } /* Orange */
            60% { color: #00ffff; } /* Cyan */
            80% { color: #ff3366; } /* Pinkish-red */
            100% { color: #ff00ff; } /* Back to magenta */
        }

        @keyframes colorChangeDate {
            0% { color: #00ff00; } /* Lime */
            20% { color: #ff9900; } /* Orange */
            40% { color: #00ffff; } /* Cyan */
            60% { color: #ff3366; } /* Pinkish-red */
            80% { color: #ff00ff; } /* Magenta */
            100% { color: #00ff00; } /* Back to lime */
        }

        @keyframes colorChangeTime {
            0% { color: #ff9900; } /* Orange */
            20% { color: #00ffff; } /* Cyan */
            40% { color: #ff3366; } /* Pinkish-red */
            60% { color: #ff00ff; } /* Magenta */
            80% { color: #00ff00; } /* Lime */
            100% { color: #ff9900; } /* Back to orange */
        }

        /* Dropdown Styling */
.dropdown {
    padding: 10px 15px;
    border-radius: 8px;
    border: 1px solid #ddd;
    background-color: #fff;
    font-size: 14px;
    color: #333;
    width: 100%;
    max-width: 300px;
    transition: border-color 0.3s ease, box-shadow 0.3s ease;
}

/* Focus and hover effect for dropdown */
.dropdown:hover, .dropdown:focus {
    border-color: #06b6d4;
    box-shadow: 0 0 5px rgba(6, 182, 212, 0.4);
    outline: none;
}
.resume-list {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }
        .resume-list th, .resume-list td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid #e0e0e0;
        }
        .resume-list th {
            background: #1a73e8;
            color: #fff;
            font-weight: 600;
        }
        .resume-list tr:hover {
            background: #e8f0fe;
        }
        .resume-list a {
            color: #1a73e8;
            text-decoration: none;
            font-weight: 500;
        }
        .resume-list a:hover {
            text-decoration: underline;
        }
        .no-resumes {
            text-align: center;
            color: #555;
            font-size: 16px;
            margin-top: 20px;
        }
        @media (max-width: 768px) {
            .navbar-container {
                flex-direction: column;
                padding: 10px;
            }

            .navbar-menu {
                flex-direction: column;
                align-items: center;
                margin-top: 10px;
            }

                .navbar-menu li {
                    margin: 5px 0;
                }

            .container {
                margin: 20px;
                padding: 20px;
            }

            .resume-list th, .resume-list td {
                padding: 8px;
            }

            .resume-list {
                width: 100%;
                border-collapse: collapse;
                margin-top: 20px;
            }

                .resume-list th, .resume-list td {
                    padding: 12px;
                    text-align: left;
                    border-bottom: 1px solid #e0e0e0;
                }

                .resume-list th {
                    background: #1a73e8;
                    color: #fff;
                    font-weight: 600;
                }

                .resume-list tr:hover {
                    background: #e8f0fe;
                }

                .resume-list a {
                    color: #1a73e8;
                    text-decoration: none;
                    font-weight: 500;
                }

                    .resume-list a:hover {
                        text-decoration: underline;
                    }

            .no-resumes {
                text-align: center;
                color: #555;
                font-size: 16px;
                margin-top: 20px;
            }

            @media (max-width: 768px) {
                .navbar-container {
                    flex-direction: column;
                    padding: 10px;
                }

                .navbar-menu {
                    flex-direction: column;
                    align-items: center;
                    margin-top: 10px;
                }

                    .navbar-menu li {
                        margin: 5px 0;
                    }

                .container {
                    margin: 20px;
                    padding: 20px;
                }

                .resume-list th, .resume-list td {
                    padding: 8px;
                }
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <!-- Navigation Bar -->
                <div class="navbar">
                    <ul>
                        <li><a href="Default.aspx">Home Page</a></li>
                        <li><a href="Login.aspx">Login</a></li>
                        <li><a href="Registration.aspx">Register</a></li>
                        
                        
                    <li><asp:LinkButton ID="lnkDownloads" runat="server" Text="Downloads" OnClick="lnkDownloads_Click" /></li>
                    <li><asp:LinkButton ID="lnkLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" CausesValidation="false" CssClass="nav-link"></asp:LinkButton></li>
                     
                    </ul>
                    </ul>
                </div>

                <!-- Header Section -->
                <div class="header">
                    <h1>Build Your Resume with Naresh Resume Creations</h1>
                    <p>Fill in your details to create a stunning resume!</p>
                    <div class="scrolling-text">
                        <span>Start crafting your future today – one section at a time!</span>
                    </div>
                </div>
                
                <!-- Main Content -->
                <div class="main-content">
                    <!-- Personal Details -->
                    <div class="container">
                        <h2>Personal Details</h2>
                        <div class="form-group">
                            <label for="txtJobTitle">Job Title</label>
                            <div class="input-container">
                                <i class="fa fa-briefcase"></i>
                                <asp:TextBox ID="txtJobTitle" runat="server" Placeholder="Enter your desired job title" CssClass="input-field"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator ControlToValidate="txtJobTitle" runat="server" ErrorMessage="Job Title is required" CssClass="error-message" Display="Dynamic" />
                        </div>

                        

                        <div class="form-group">
                            <label for="txtFirstName">First Name</label>
                            <div class="input-container">
                                <i class="fa fa-user"></i>
                                <asp:TextBox ID="txtFirstName" runat="server" Placeholder="Enter your first name" CssClass="input-field"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="First Name is required" CssClass="error-message" Display="Dynamic" />
                        </div>

                        <div class="form-group">
                            <label for="txtLastName">Last Name</label>
                            <div class="input-container">
                                <i class="fa fa-user"></i>
                                <asp:TextBox ID="txtLastName" runat="server" Placeholder="Enter your last name" CssClass="input-field"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLastName" ErrorMessage="Last Name is required" CssClass="error-message" Display="Dynamic" />
                        </div>

                        <div class="form-group">
                            <label for="txtEmail">Email</label>
                            <div class="input-container">
                                <i class="fa fa-envelope"></i>
                                <asp:TextBox ID="txtEmail" runat="server" Placeholder="Enter your email" CssClass="input-field"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator ControlToValidate="txtEmail" runat="server" ErrorMessage="Email is required" CssClass="error-message" Display="Dynamic" />
                            <asp:RegularExpressionValidator ControlToValidate="txtEmail" runat="server" ErrorMessage="Invalid email format" CssClass="error-message" Display="Dynamic" ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" />
                        </div>

                        <div class="form-group">
                            <label for="txtPhone">Phone</label>
                            <div class="input-container">
                                <i class="fa fa-phone"></i>
                                <asp:TextBox ID="txtPhone" runat="server" Placeholder="Enter your phone number" CssClass="input-field"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator ControlToValidate="txtPhone" runat="server" ErrorMessage="Phone is required" CssClass="error-message" Display="Dynamic" />
                        </div>

                        <div class="form-group">
                            <label for="txtCountry">Country</label>
                            <div class="input-container">
                                <i class="fa fa-globe"></i>
                                <asp:TextBox ID="txtCountry" runat="server" Placeholder="Enter your country" CssClass="input-field"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
    <label for="txtState">State</label>
    <div class="input-container">
        <i class="fa fa-globe"></i>
        <asp:TextBox ID="txtState" runat="server" Placeholder="Enter your State" CssClass="input-field"></asp:TextBox>
    </div>
</div>

                        <div class="form-group">
                            <label for="txtCity">City</label>
                            <div class="input-container">
                                <i class="fa fa-city"></i>
                                <asp:TextBox ID="txtCity" runat="server" Placeholder="Enter your city" CssClass="input-field"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtAddress">Address</label>
                            <div class="input-container">
                                <i class="fa fa-map-marker-alt"></i>
                                <asp:TextBox ID="txtAddress" runat="server" Placeholder="Enter your address" CssClass="input-field"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtPostalCode">Postal Code</label>
                            <div class="input-container">
                                <i class="fa fa-mail-bulk"></i>
                                <asp:TextBox ID="txtPostalCode" runat="server" Placeholder="Enter your postal code" CssClass="input-field"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtNationality">Nationality</label>
                            <div class="input-container">
                                <i class="fa fa-flag"></i>
                                <asp:TextBox ID="txtNationality" runat="server" Placeholder="Enter your nationality" CssClass="input-field"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtPlaceOfBirth">Place of Birth</label>
                            <div class="input-container">
                                <i class="fa fa-map-pin"></i>
                                <asp:TextBox ID="txtPlaceOfBirth" runat="server" Placeholder="Enter your place of birth" CssClass="input-field"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtDateOfBirth">Date of Birth</label>
                            <div class="input-container">
                                <i class="fa fa-calendar-alt"></i>
                                <asp:TextBox ID="txtDateOfBirth" runat="server" Placeholder="Select your date of birth" TextMode="Date" CssClass="input-field"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="txtSummary">Personal Summary</label>
                            <div class="input-container">
                                <i class="fa fa-comment"></i>
                                <asp:TextBox ID="txtSummary" runat="server" CssClass="input-field" TextMode="MultiLine" MaxLength="300" Placeholder="Enter your personal summary"></asp:TextBox>
                            </div>
                        </div>

                        <asp:Button ID="btnSavePersonal" runat="server" Text="Save" CssClass="btn-submit" OnClick="btnSavePersonal_Click" />
                        <asp:Label ID="lblMessage" runat="server" CssClass="success-message"></asp:Label>
                    </div>

                    <!-- Employment Status -->
                    <div class="container">
                        <h2>Employment Status</h2>
                        <div class="form-group">
                            <label>Are you currently employed or have work experience?</label>
                            <asp:RadioButtonList ID="rblEmploymentStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblEmploymentStatus_SelectedIndexChanged">
                                <asp:ListItem Text="Fresher (No work experience)" Value="Fresher" Selected="True" />
                                <asp:ListItem Text="Employed/Have work experience" Value="Employed" />
                            </asp:RadioButtonList>
                        </div>

                        <div id="employmentHistorySection" runat="server" visible="false">
                            <h3>Employment History</h3>
                            <asp:Repeater ID="rptEmployment" runat="server">
                                <ItemTemplate>
                                    <div class="employment-entry">
                                        <div class="employment-details">
                                            <strong><%# Eval("JobTitle") %></strong> at <%# Eval("Employer") %>
                                            (<%# Eval("StartDate", "{0:yyyy-MM-dd}") %> to 
                                            <%# Eval("EndDate") == null ? "Present" : Eval("EndDate", "{0:yyyy-MM-dd}") %>)
                                            <%# string.IsNullOrEmpty(Eval("City").ToString()) ? "" : "<p>Location: " + Eval("City") + "</p>" %>
                                            <%# string.IsNullOrEmpty(Eval("Description").ToString()) ? "" : "<p>" + Eval("Description") + "</p>" %>
                                        </div>
                                        <asp:Button ID="btnDeleteEmployment" runat="server" Text="❌ Delete" CssClass="btn-delete" CommandArgument='<%# Eval("Id") %>' OnCommand="btnDeleteEmployment_Command" />
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                            <asp:Button ID="btnAddEmployment" runat="server" Text="+ Add Employment" CssClass="btn-add" OnClick="btnAddEmployment_Click" />

                            <div id="employmentForm" runat="server" visible="false" class="employment-form">
                                <div class="form-group">
                                    <label for="TextJobTitle">Job Title</label>
                                    <div class="input-container">
                                        <i class="fa fa-briefcase"></i>
                                        <asp:TextBox ID="TextJobTitle" runat="server" Placeholder="Enter job title" CssClass="input-field" required></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="txtEmployer">Employer</label>
                                    <div class="input-container">
                                        <i class="fa fa-building"></i>
                                        <asp:TextBox ID="txtEmployer" runat="server" Placeholder="Enter employer" CssClass="input-field" required></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="txtStartDate">Joining Date</label>
                                    <div class="input-container">
                                        <i class="fa fa-calendar-alt"></i>
                                        <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" CssClass="input-field" required></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="txtEndDate">Ending Date (leave blank if current)</label>
                                    <div class="input-container">
                                        <i class="fa fa-calendar-alt"></i>
                                        <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" CssClass="input-field"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="txtEmpCity">City</label>
                                    <div class="input-container">
                                        <i class="fa fa-city"></i>
                                        <asp:TextBox ID="txtEmpCity" runat="server" Placeholder="Enter city" CssClass="input-field"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="txtDescription">Description</label>
                                    <div class="input-container">
                                        <i class="fa fa-comment"></i>
                                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Placeholder="Enter job description" CssClass="input-field"></asp:TextBox>
                                    </div>
                                </div>
                                <asp:Button ID="btnSaveEmployment" runat="server" Text="💾 Save" CssClass="btn-save" OnClick="btnSaveEmployment_Click" />
                                <asp:Button ID="btnCancelEmployment" runat="server" Text="✖ Cancel" CssClass="btn-cancel" OnClick="btnCancelEmployment_Click" CausesValidation="false" />
                                <asp:Label ID="lblError" runat="server" CssClass="error-message" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <!-- Education -->
                    <div class="container">
                        <h2>Education</h2>
                        <asp:Repeater ID="rptEducation" runat="server">
                            <ItemTemplate>
                                <div class="education-item">
                                    <strong><%# Eval("Degree") %></strong> at <%# Eval("SchoolName") %> 
                                    (<%# Eval("StartDate", "{0:yyyy-MM-dd}") %> to <%# Eval("EndDate", "{0:yyyy-MM-dd}") %>)
                                    <br />
                                    <%# Eval("Description") %>
                                    <asp:Button ID="btnDeleteEducation" runat="server" Text="Delete" CommandArgument='<%# Eval("Id") %>' OnCommand="btnDeleteEducation_Command" CssClass="btn-delete" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <asp:Button ID="btnAddEducation" runat="server" Text="Add Education" CssClass="btn-add" OnClick="btnAddEducation_Click" />

                        <div id="educationForm" class="education-form" runat="server" visible="false">
                            <div class="form-group">
                                <label for="txtSchoolName">School Name</label>
                                <div class="input-container">
                                    <i class="fa fa-school"></i>
                                    <asp:TextBox ID="txtSchoolName" runat="server" Placeholder="Enter school name" CssClass="input-field"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ControlToValidate="txtSchoolName" runat="server" ErrorMessage="School Name is required" CssClass="error-message" Display="Dynamic" />
                            </div>
                            <div class="form-group">
                                <label for="txtDegree">Degree</label>
                                <div class="input-container">
                                    <i class="fa fa-graduation-cap"></i>
                                    <asp:TextBox ID="txtDegree" runat="server" Placeholder="Enter degree" CssClass="input-field"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ControlToValidate="txtDegree" runat="server" ErrorMessage="Degree is required" CssClass="error-message" Display="Dynamic" />
                            </div>
                            <div class="form-group">
                                <label for="txtEduStartDate">Joining Date</label>
                                <div class="input-container">
                                    <i class="fa fa-calendar-alt"></i>
                                    <asp:TextBox ID="txtEduStartDate" runat="server" TextMode="Date" CssClass="input-field"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="txtEduEndDate">Ending Date</label>
                                <div class="input-container">
                                    <i class="fa fa-calendar-alt"></i>
                                    <asp:TextBox ID="txtEduEndDate" runat="server" TextMode="Date" CssClass="input-field"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="txtEduCity">City</label>
                                <div class="input-container">
                                    <i class="fa fa-city"></i>
                                    <asp:TextBox ID="txtEduCity" runat="server" Placeholder="Enter city" CssClass="input-field"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="txtEduDescription">Description</label>
                                <div class="input-container">
                                    <i class="fa fa-comment"></i>
                                    <asp:TextBox ID="txtEduDescription" runat="server" TextMode="MultiLine" Placeholder="Enter description" CssClass="input-field"></asp:TextBox>
                                </div>
                            </div>
                            <asp:Button ID="btnSaveEducation" runat="server" Text="Save" CssClass="btn-submit" OnClick="btnSaveEducation_Click" />
                            <asp:Label ID="lblEduError" runat="server" CssClass="error-message" Visible="false"></asp:Label>
                        </div>
                    </div>

                    <!-- Websites & Social Links -->
                    <div class="container">
                        <h3>Websites & Social Links</h3>
                        <asp:Repeater ID="rptWebsites" runat="server">
                            <ItemTemplate>
                                <div class="website-item">
                                    <b><%# Eval("Label") %></b>: 
                                    <a href='<%# Eval("Link") %>' target="_blank"><%# Eval("Link") %></a>
                                    <asp:Button ID="btnDeleteWebsite" runat="server" Text="Delete" CommandArgument='<%# Eval("Id") %>' OnCommand="btnDeleteWebsite_Command" CssClass="btn-delete" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <asp:Button ID="btnAddWebsite" runat="server" Text="Add Website" CssClass="btn-add" OnClick="btnAddWebsite_Click" />

                        <div id="websiteForm" class="website-form" runat="server" visible="false">
                            <div class="form-group">
                                <label for="txtLabel">Label</label>
                                <div class="input-container">
                                    <i class="fa fa-tag"></i>
                                    <asp:TextBox ID="txtLabel" runat="server" Placeholder="e.g. LinkedIn, Portfolio" CssClass="input-field"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ControlToValidate="txtLabel" runat="server" ErrorMessage="Label is required" CssClass="error-message" Display="Dynamic" />
                            </div>
                            <div class="form-group">
                                <label for="txtLink">URL</label>
                                <div class="input-container">
                                    <i class="fa fa-link"></i>
                                    <asp:TextBox ID="txtLink" runat="server" Placeholder="e.g. https://linkedin.com/in/yourprofile" CssClass="input-field"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ControlToValidate="txtLink" runat="server" ErrorMessage="URL is required" CssClass="error-message" Display="Dynamic" />
                                <asp:RegularExpressionValidator ControlToValidate="txtLink" runat="server" ValidationExpression="^(https?:\/\/[^\s]+)$" ErrorMessage="Enter a valid URL" CssClass="error-message" Display="Dynamic" />
                            </div>
                            <asp:Button ID="btnSaveWebsite" runat="server" Text="Save" CssClass="btn-submit" OnClick="btnSaveWebsite_Click" />
                            <asp:Label ID="lblWebsiteError" runat="server" CssClass="error-message" Visible="false"></asp:Label>
                        </div>
                    </div>

                    <!-- Skills -->
                    <div class="container">
                        <h3>Skills</h3>
                        <asp:Repeater ID="rptSkills" runat="server">
                            <ItemTemplate>
                                <div class="skill-item">
                                    <b><%# Eval("SkillName") %></b>
                                    <asp:Button ID="btnDeleteSkill" runat="server" Text="Delete" CommandArgument='<%# Eval("Id") %>' OnCommand="btnDeleteSkill_Command" CssClass="btn-delete" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <asp:Button ID="btnAddSkill" runat="server" Text="Add Skill" CssClass="btn-add" OnClick="btnAddSkill_Click" />

                        <div id="skillForm" class="skill-form" runat="server" visible="false">
                            <div class="form-group">
                                <label for="txtSkill">Skill</label>
                                <div class="input-container">
                                    <i class="fa fa-star"></i>
                                    <asp:TextBox ID="txtSkill" runat="server" Placeholder="Enter skill" CssClass="input-field"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ControlToValidate="txtSkill" runat="server" ErrorMessage="Skill is required" CssClass="error-message" Display="Dynamic" />
                            </div>
                            <asp:Button ID="btnSaveSkill" runat="server" Text="Save" CssClass="btn-submit" OnClick="btnSaveSkill_Click" />
                            <asp:Label ID="lblSkillError" runat="server" CssClass="error-message" Visible="false"></asp:Label>
                        </div>
                    </div>

                    <!-- Internships -->
                    <div class="container">
                        <h2>Internships</h2>
                        <asp:Repeater ID="rptInternships" runat="server">
                            <ItemTemplate>
                                <div class="internship-item">
                                    <strong><%# Eval("JobTitle") %></strong> at <%# Eval("CompanyName") %> 
                                    (<%# Eval("StartDate", "{0:yyyy-MM-dd}") %> to <%# Eval("EndDate", "{0:yyyy-MM-dd}") %>)
                                    <br />
                                    <%# Eval("Description") %>
                                    <asp:Button ID="btnDeleteInternship" runat="server" Text="Delete" CommandArgument='<%# Eval("Id") %>' OnCommand="btnDeleteInternship_Command" CssClass="btn-delete" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <asp:Button ID="btnAddInternship" runat="server" Text="Add Internship" CssClass="btn-add" OnClick="btnAddInternship_Click" />

                        <div id="internshipForm" class="internship-form" runat="server" visible="false">
                            <div class="form-group">
                                <label for="txtCompanyName">Company Name</label>
                                <div class="input-container">
                                    <i class="fa fa-building"></i>
                                    <asp:TextBox ID="txtCompanyName" runat="server" Placeholder="Enter company name" CssClass="input-field"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ControlToValidate="txtCompanyName" runat="server" ErrorMessage="Company Name is required" CssClass="error-message" Display="Dynamic" />
                            </div>
                            <div class="form-group">
                                <label for="InternshipJobTitle">Job Title</label>
                                <div class="input-container">
                                    <i class="fa fa-briefcase"></i>
                                    <asp:TextBox ID="InternshipJobTitle" runat="server" Placeholder="Enter job title" CssClass="input-field"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ControlToValidate="InternshipJobTitle" runat="server" ErrorMessage="Job Title is required" CssClass="error-message" Display="Dynamic" />
                            </div>
                            <div class="form-group">
                                <label for="txtIntStartDate">Joining Date</label>
                                <div class="input-container">
                                    <i class="fa fa-calendar-alt"></i>
                                    <asp:TextBox ID="txtIntStartDate" runat="server" TextMode="Date" CssClass="input-field"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="txtIntEndDate">Ending Date</label>
                                <div class="input-container">
                                    <i class="fa fa-calendar-alt"></i>
                                    <asp:TextBox ID="txtIntEndDate" runat="server" TextMode="Date" CssClass="input-field"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="txtIntCity">City</label>
                                <div class="input-container">
                                    <i class="fa fa-city"></i>
                                    <asp:TextBox ID="txtIntCity" runat="server" Placeholder="Enter city" CssClass="input-field"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="txtIntDescription">Description</label>
                                <div class="input-container">
                                    <i class="fa fa-comment"></i>
                                    <asp:TextBox ID="txtIntDescription" runat="server" TextMode="MultiLine" Placeholder="Enter description" CssClass="input-field"></asp:TextBox>
                                </div>
                            </div>
                            <asp:Button ID="btnSaveInternship" runat="server" Text="Save" CssClass="btn-submit" OnClick="btnSaveInternship_Click" />
                            <asp:Label ID="lblIntError" runat="server" CssClass="error-message" Visible="false"></asp:Label>
                        </div>
                    </div>

                    <!-- Courses -->
                    <div class="container">
                        <h3>Courses</h3>
                        <asp:Repeater ID="rptCourses" runat="server">
                            <ItemTemplate>
                                <div class="skill-item">
                                    <b><%# Eval("CourseName") %></b> - <%# Eval("Institution") %>  
                                    (<%# Eval("StartDate", "{0:yyyy-MM-dd}") %> to <%# Eval("EndDate", "{0:yyyy-MM-dd}") %>)
                                    <asp:Button ID="btnDeleteCourse" runat="server" Text="Delete" CommandArgument='<%# Eval("Id") %>' OnCommand="btnDeleteCourse_Command" CssClass="btn-delete" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <asp:Button ID="btnAddCourse" runat="server" Text="Add Course" CssClass="btn-add" OnClick="btnAddCourse_Click" />

                        <div id="courseForm" class="skill-form" runat="server" visible="false">
                            <div class="form-group">
                                <label for="txtCourseName">Course Name</label>
                                <div class="input-container">
                                    <i="fa fa-book"></i>
                                    <asp:TextBox ID="txtCourseName" runat="server" Placeholder="Enter course name" CssClass="input-field"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ControlToValidate="txtCourseName" runat="server" ErrorMessage="Course Name is required" CssClass="error-message" Display="Dynamic" />
                            </div>
                            <div class="form-group">
                                <label for="txtInstitution">Institution</label>
                                <div class="input-container">
                                    <i class="fa fa-university"></i>
                                    <asp:TextBox ID="txtInstitution" runat="server" Placeholder="Enter institution" CssClass="input-field"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ControlToValidate="txtInstitution" runat="server" ErrorMessage="Institution is required" CssClass="error-message" Display="Dynamic" />
                            </div>
                            <div class="form-group">
                                <label for="txtCourseStartDate">Joining Date</label>
                                <div class="input-container">
                                    <i class="fa fa-calendar-alt"></i>
                                    <asp:TextBox ID="txtCourseStartDate" runat="server" TextMode="Date" CssClass="input-field"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ControlToValidate="txtCourseStartDate" runat="server" ErrorMessage="Start Date is required" CssClass="error-message" Display="Dynamic" />
                            </div>
                            <div class="form-group">
                                <label for="txtCourseEndDate">Ending Date</label>
                                <div class="input-container">
                                    <i class="fa fa-calendar-alt"></i>
                                    <asp:TextBox ID="txtCourseEndDate" runat="server" TextMode="Date" CssClass="input-field"></asp:TextBox>
                                </div>
                            </div>
                            <asp:Button ID="btnSaveCourse" runat="server" Text="Save" CssClass="btn-submit" OnClick="btnSaveCourse_Click" />
                            <asp:Label ID="lblCourseError" runat="server" CssClass="error-message" Visible="false"></asp:Label>
                        </div>
                    </div>

                    <!-- Languages -->
                    <div class="container">
                        <h3>Languages</h3>
                        <asp:Repeater ID="rptLanguages" runat="server">
                            <ItemTemplate>
                                <div class="skill-item">
                                    <b><%# Eval("LanguageName") %></b>
                                    <asp:Button ID="btnDeleteLanguage" runat="server" Text="Delete" CommandArgument='<%# Eval("Id") %>' OnCommand="btnDeleteLanguage_Command" CssClass="btn-delete" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <asp:Button ID="btnAddLanguage" runat="server" Text="Add Language" CssClass="btn-add" OnClick="btnAddLanguage_Click" />

                        <div id="languageForm" class="skill-form" runat="server" visible="false">
                            <div class="form-group">
                                <label for="txtLanguageName">Language</label>
                                <div class="input-container">
                                    <i class="fa fa-language"></i>
                                    <asp:TextBox ID="txtLanguageName" runat="server" Placeholder="e.g. English, French" CssClass="input-field"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ControlToValidate="txtLanguageName" runat="server" ErrorMessage="Language is required" CssClass="error-message" Display="Dynamic" />
                            </div>
                            <asp:Button ID="btnSaveLanguage" runat="server" Text="Save" CssClass="btn-submit" OnClick="btnSaveLanguage_Click" />
                            <asp:Label ID="lblLangError" runat="server" CssClass="error-message" Visible="false"></asp:Label>
                        </div>
                    </div>

                    <!-- Hobbies -->
                    <div class="container">
                        <h3>Hobbies</h3>
                        <asp:Repeater ID="rptHobbies" runat="server">
                            <ItemTemplate>
                                <div class="skill-item">
                                    <b><%# Eval("Name") %></b>
                                    <asp:Button ID="btnDeleteHobby" runat="server" Text="Delete" CommandArgument='<%# Eval("Id") %>' OnCommand="btnDeleteHobby_Command" CssClass="btn-delete" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <asp:Button ID="btnAddHobby" runat="server" Text="Add Hobby" CssClass="btn-add" OnClick="btnAddHobby_Click" />

                        <div id="hobbyForm" class="skill-form" runat="server" visible="false">
                            <div class="form-group">
                                <label for="txtHobby">Hobby</label>
                                <div class="input-container">
                                    <i class="fa fa-heart"></i>
                                    <asp:TextBox ID="txtHobby" runat="server" Placeholder="e.g. painting, skydiving" CssClass="input-field"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ControlToValidate="txtHobby" runat="server" ErrorMessage="Hobby is required" CssClass="error-message" Display="Dynamic" />
                            </div>
                            <asp:Button ID="btnSaveHobby" runat="server" Text="Save" CssClass="btn-submit" OnClick="btnSaveHobby_Click" />
                            <asp:Label ID="lblHobbyError" runat="server" CssClass="error-message" Visible="false"></asp:Label>
                        </div>
                    </div>

                    <!-- Custom Selection -->
                    <div class="container">
                        <h2>Custom Selection</h2>
                        <asp:Repeater ID="rptCustomSelection" runat="server">
                            <ItemTemplate>
                                <div class="custom-item">
                                    <strong><%# Eval("Title") %></strong> 
                                    (<%# Eval("StartDate", "{0:yyyy-MM-dd}") %> to <%# Eval("EndDate", "{0:yyyy-MM-dd}") %>)
                                    <br />
                                    <%# Eval("Description") %>
                                    <asp:Button ID="btnDeleteCustom" runat="server" Text="Delete" CommandArgument='<%# Eval("Id") %>' OnCommand="btnDeleteCustom_Command" CssClass="btn-delete" />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <asp:Button ID="btnAddCustom" runat="server" Text="Add Custom Selection" CssClass="btn-add" OnClick="btnAddCustom_Click" />

                        <div id="customForm" class="custom-form" runat="server" visible="false">
                            <div class="form-group">
                                <label for="txtCustomTitle">Title</label>
                                <div class="input-container">
                                    <i class="fa fa-tag"></i>
                                    <asp:TextBox ID="txtCustomTitle" runat="server" Placeholder="Enter title" CssClass="input-field"></asp:TextBox>
                                </div>
                                <asp:RequiredFieldValidator ControlToValidate="txtCustomTitle" runat="server" ErrorMessage="Title is required" CssClass="error-message" Display="Dynamic" />
                            </div>
                            <div class="form-group">
                                <label for="txtCustomStartDate">Joining Date</label>
                                <div class="input-container">
                                    <i class="fa fa-calendar-alt"></i>
                                    <asp:TextBox ID="txtCustomStartDate" runat="server" TextMode="Date" CssClass="input-field"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="txtCustomEndDate">Ending Date</label>
                                <div class="input-container">
                                    <i class="fa fa-calendar-alt"></i>
                                    <asp:TextBox ID="txtCustomEndDate" runat="server" TextMode="Date" CssClass="input-field"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="txtCustomDescription">Description</label>
                                <div class="input-container">
                                    <i class="fa fa-comment"></i>
                                    <asp:TextBox ID="txtCustomDescription" runat="server" TextMode="MultiLine" Placeholder="Enter description" CssClass="input-field"></asp:TextBox>
                                </div>
                            </div>
                            <asp:Button ID="btnSaveCustom" runat="server" Text="Save" CssClass="btn-submit" OnClick="btnSaveCustom_Click" />
                            <asp:Label ID="lblCustomError" runat="server" CssClass="error-message" Visible="false"></asp:Label>
                        </div>
                    </div>

                    <!-- Generate Resume and Logout Buttons -->
                    <div class="container">
                        <asp:DropDownList ID="ddlResumeOptions" runat="server" CssClass="dropdown">
                            <asp:ListItem Text="Select Resume Type" Value="" Selected="True" />
                            <asp:ListItem Text="Modern Resume" Value="Resume.aspx" />
                            <asp:ListItem Text="Classic Resume" Value="Resume2.aspx" />
                            <asp:ListItem Text="Creative Resume" Value="Resume3.aspx" />
                            <asp:ListItem Text="Professional Resume" Value="Resume4.aspx" />
                            <asp:ListItem Text="Minimalist Resume" Value="Resume5.aspx" />
                            <asp:ListItem Text="Executive Resume" Value="Resume6.aspx" />
                            <asp:ListItem Text="Functional Resume" Value="Resume7.aspx" />
                            <asp:ListItem Text="Tech Resume" Value="Resume8.aspx" />
                            <asp:ListItem Text="Academic Resume" Value="Resume9.aspx" />
                            <asp:ListItem Text="Hybrid Resume" Value="Resume10.aspx" />
                             <asp:ListItem Text="Resume 11" Value="Resume11.aspx" />
                             
                        </asp:DropDownList>
                        <asp:Button ID="btnGenerateResume" runat="server" Text="Generate Resume" OnClick="btnGenerateResume_Click" CssClass="btn-generate" />
                        <asp:Button ID="btnLogoutt" runat="server" Text="Logout" OnClick="btnLogoutt_Click" CausesValidation="false" CssClass="btn-logout" />
                    </div>
                </div>
                

                <!-- About Section -->
                <div class="about-section" id="about">
                    <h3>About Us</h3>
                    <p>
                        Naresh Resume Creations is where creativity meets opportunity. Our platform offers 
                        stunning, colorful templates and easy-to-use tools to craft resumes that dazzle 
                        recruiters. Step into your future with a resume that’s as bold as you are! We are 
                        committed to helping job seekers stand out in a competitive market with unique 
                        designs and intuitive features that make resume-building a breeze.
                    </p>
                </div>

                <!-- Footer Section -->
                <div class="footer">
                    <p>Designed by @Naresh</p>
                    <p>Contact: <a href="mailto:nareshresumecreations@gmail.com">nareshresumecreations@gmail.com</a> | © 2025 Naresh Resume Creations</p>
                    <p id="datetime"></p>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>

    <script>
        // Update date and time dynamically
        function updateDateTime() {
            const now = new Date();
            const day = now.toLocaleString('en-US', { weekday: 'long' });
            const date = now.toLocaleString('en-US', { year: 'numeric', month: 'long', day: 'numeric' });
            const time = now.toLocaleString('en-US', { hour: '2-digit', minute: '2-digit', second: '2-digit' });

            document.getElementById('datetime').innerHTML =
                `<span class="day">${day}</span>, ` +
                `<span class="date">${date}</span> ` +
                `<span class="time">${time}</span>`;
        }

        updateDateTime();
        setInterval(updateDateTime, 1000);
    </script>
</body>
</html>