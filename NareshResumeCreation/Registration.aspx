<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="NareshResumeCreation.Registration" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register - Naresh Resume Creations</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" />
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }
        body {
            font-family: 'Poppins', sans-serif;
            background: linear-gradient(135deg, #ff6b6b, #ffbe76, #80ed99);
            color: #2d1b3f;
            min-height: 100vh;
            max-height: 100vh;
            overflow: hidden;
            display: flex;
            flex-direction: column;
        }
        .navbar {
            background: linear-gradient(135deg, #4a00e0, #8e2de2);
            padding: 8px 15px;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.25);
            position: fixed;
            top: 0;
            width: 100%;
            z-index: 1000;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }
        .navbar .brand {
            color: #f3e8ff;
            font-size: 18px;
            font-weight: 600;
            text-decoration: none;
            display: flex;
            align-items: center;
            gap: 6px;
        }
        .navbar .brand i {
            font-size: 22px;
        }
        .navbar ul {
            list-style: none;
            display: flex;
            gap: 15px;
            margin: 0;
            padding: 0;
        }
        .navbar ul li a {
            color: #f3e8ff;
            text-decoration: none;
            font-size: 13px;
            font-weight: 500;
            padding: 6px 12px;
            border-radius: 5px;
            transition: all 0.3s ease;
            transform: scale(1);
        }
        .navbar ul li a:hover {
            background: linear-gradient(135deg, #7b3fe4, #b766f4);
            transform: scale(1.1);
        }
        .welcome-section {
            display: flex;
            min-height: 100vh;
            max-height: 100vh;
            width: 100%;
            background: linear-gradient(135deg, rgba(255, 247, 237, 0.9), rgba(254, 243, 199, 0.9));
            position: relative;
            z-index: 1;
            padding-top: 40px;
            padding-bottom: 40px;
        }
        .welcome-section::before {
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: url('data:image/svg+xml,%3Csvg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 100 100"%3E%3Cpolygon points="50,10 90,90 10,90" fill="rgba(255,255,255,0.2)"/%3E%3C/svg%3E');
            background-size: 80px;
            opacity: 0.3;
            animation: rotate 25s linear infinite;
            z-index: -1;
        }
        @keyframes rotate {
            0% { transform: rotate(0deg); }
            100% { transform: rotate(360deg); }
        }
        .main-content {
            display: flex;
            flex: 1;
            width: 100%;
            padding: 10px 20px;
            justify-content: center;
            align-items: center;
            gap: 20px;
        }
        .register-container {
            background: linear-gradient(135deg, #fff7ed, #fef3c7);
            padding: 20px;
            border-radius: 15px;
            box-shadow: 0 8px 20px rgba(0, 0, 0, 0.2);
            max-width: 380px;
            width: 100%;
            text-align: center;
            transition: transform 0.3s ease;
        }
        .register-container:hover {
            transform: translateY(-3px);
        }
        h2 {
            background: linear-gradient(135deg, #00d4ff, #007bff);
            color: #fff;
            padding: 10px;
            border-radius: 8px;
            margin-bottom: 15px;
            font-size: 20px;
            font-weight: 600;
        }
        .form-group {
            margin-bottom: 15px;
            text-align: left;
        }
        label {
            font-weight: 500;
            font-size: 12px;
            color: #2d1b3f;
            margin-bottom: 5px;
            display: block;
        }
        .input-container {
            position: relative;
        }
        .input-container i {
            position: absolute;
            left: 10px;
            top: 50%;
            transform: translateY(-50%);
            color: #6b46c1;
            font-size: 14px;
        }
        .input-field {
            width: 100%;
            padding: 8px 8px 8px 30px;
            border: 2px solid #d6bcfa;
            border-radius: 8px;
            font-size: 13px;
            background: linear-gradient(135deg, #f3e8ff, #e9d8fd);
            transition: all 0.3s ease;
        }
        .input-field:focus {
            border-color: #007bff;
            box-shadow: 0 0 8px rgba(0, 123, 255, 0.5);
            background: #fff;
            outline: none;
        }
        .name-fields {
            display: flex;
            gap: 10px;
        }
        .name-fields .form-group {
            flex: 1;
            margin-bottom: 0;
        }
        .password-strength {
            font-size: 11px;
            margin-top: 5px;
            color: #666;
            text-align: left;
        }
        .btn-register {
            background: linear-gradient(135deg, #ff6b6b, #ff4757);
            color: #fff;
            border: none;
            padding: 8px;
            border-radius: 6px;
            font-size: 13px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            box-shadow: 0 3px 8px rgba(255, 107, 107, 0.4);
            width: 100%;
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 5px;
        }
        .btn-register:hover {
            background: linear-gradient(135deg, #ff4757, #ff6b6b);
            transform: scale(1.05);
            box-shadow: 0 4px 10px rgba(255, 107, 107, 0.5);
        }
        .error-message {
            color: #9f1239;
            font-size: 11px;
            margin-top: 10px;
            display: block;
        }
        .login-link {
            margin-top: 10px;
            font-size: 11px;
            color: #2d1b3f;
        }
        .login-link a {
            color: #ff6200;
            text-decoration: none;
            font-weight: 600;
        }
        .login-link a:hover {
            color: #ff9f1c;
            text-decoration: underline;
        }
        .info-section {
            max-width: 400px;
            display: flex;
            flex-direction: column;
            gap: 10px;
        }
        .header-section {
            text-align: center;
        }
        .header-section h1 {
            font-size: 22px;
            font-weight: 700;
            color: #2d1b3f;
            margin-bottom: 5px;
            animation: fadeIn 1s ease-in;
        }
        .header-section p {
            font-size: 11px;
            color: #2d1b3f;
            max-width: 350px;
            margin: 0 auto;
            animation: fadeIn 1.5s ease-in;
        }
        .header-image {
            max-width: 60px;
            margin: 5px auto;
        }
        .header-image img {
            width: 100%;
            border-radius: 8px;
        }
        @keyframes fadeIn {
            0% { opacity: 0; transform: translateY(10px); }
            100% { opacity: 1; transform: translateY(0); }
        }
        .about-section {
            background: linear-gradient(135deg, #f3e8ff, #e9d8fd);
            padding: 10px;
            border-radius: 8px;
            box-shadow: 0 3px 8px rgba(0, 0, 0, 0.15);
            text-align: center;
        }
        .about-section h3 {
            font-size: 16px;
            color: #2d1b3f;
            margin-bottom: 5px;
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 5px;
            animation: fadeIn 2s ease-in;
        }
        .about-section h3 i {
            color: #ff6b6b;
            font-size: 18px;
        }
        .about-section p {
            font-size: 11px;
            color: #2d1b3f;
            line-height: 1.5;
            margin-bottom: 8px;
            animation: fadeIn 2.5s ease-in;
        }
        .about-section ul {
            list-style: none;
            padding: 0;
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
            gap: 8px;
        }
        .about-section ul li {
            background: linear-gradient(135deg, #fff7ed, #fef3c7);
            padding: 4px 8px;
            border-radius: 10px;
            font-size: 10px;
            color: #2d1b3f;
            box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1);
        }
        .cta-banner {
            background: linear-gradient(135deg, #00d4ff, #007bff);
            color: #f3e8ff;
            padding: 6px;
            border-radius: 6px;
            text-align: center;
            max-width: 350px;
            margin: 5px auto;
            box-shadow: 0 2px 6px rgba(0, 123, 255, 0.4);
        }
        .cta-banner p {
            font-size: 11px;
            font-weight: 500;
            margin: 0;
        }
        .footer {
            background: linear-gradient(135deg, #4a00e0, #8e2de2);
            color: #f3e8ff;
            text-align: center;
            padding: 6px;
            box-shadow: 0 -2px 5px rgba(0, 0, 0, 0.25);
            width: 100%;
            z-index: 2;
        }
        .footer p {
            font-size: 10px;
            margin: 2px 0;
        }
        .footer a {
            color: #fef3c7;
            text-decoration: none;
        }
        .footer a:hover {
            text-decoration: underline;
        }
        #datetime {
            font-size: 10px;
            background: rgba(0, 0, 0, 0.5);
            padding: 4px 8px;
            border-radius: 5px;
            display: inline-block;
        }
        .day, .date, .time {
            animation: colorChange 8s infinite;
        }
        @keyframes colorChange {
            0% { color: #ff00ff; }
            20% { color: #00ff00; }
            40% { color: #ff9900; }
            60% { color: #00ffff; }
            80% { color: #ff3366; }
            100% { color: #ff00ff; }
        }
        @media (max-width: 768px) {
            .navbar {
                padding: 6px 10px;
                flex-direction: column;
                gap: 6px;
            }
            .navbar .brand {
                font-size: 14px;
            }
            .navbar .brand i {
                font-size: 18px;
            }
            .navbar ul {
                gap: 8px;
            }
            .navbar ul li a {
                font-size: 11px;
                padding: 5px 8px;
            }
            .main-content {
                flex-direction: column;
                padding: 8px 15px;
                gap: 10px;
            }
            .register-container, .info-section {
                max-width: 90%;
            }
            .register-container {
                padding: 15px;
            }
            h2 {
                font-size: 18px;
                padding: 8px;
            }
            .form-group {
                margin-bottom: 12px;
            }
            label {
                font-size: 11px;
            }
            .input-field {
                padding: 7px 7px 7px 28px;
                font-size: 12px;
            }
            .input-container i {
                font-size: 13px;
            }
            .btn-register {
                padding: 7px;
                font-size: 12px;
            }
            .error-message, .login-link {
                font-size: 10px;
            }
            .password-strength {
                font-size: 10px;
            }
            .header-section h1 {
                font-size: 18px;
            }
            .header-section p {
                font-size: 10px;
            }
            .header-image {
                max-width: 50px;
            }
            .about-section {
                padding: 8px;
            }
            .about-section h3 {
                font-size: 14px;
            }
            .about-section h3 i {
                font-size: 16px;
            }
            .about-section p {
                font-size: 10px;
            }
            .about-section ul li {
                font-size: 9px;
                padding: 3px 6px;
            }
            .cta-banner p {
                font-size: 10px;
            }
            .footer p, #datetime {
                font-size: 8px;
            }
        }
    </style>
    <script>
        // Password Strength Checker
        document.getElementById('<%= txtPassword.ClientID %>').addEventListener('input', function () {
            var strengthIndicator = document.getElementById('password-strength');
            var password = this.value;

            if (password.length < 6) {
                strengthIndicator.textContent = "Weak Password!";
                strengthIndicator.style.color = "#ff4757";
            } else {
                strengthIndicator.textContent = "Strong Password!";
                strengthIndicator.style.color = "#2ecc71";
            }
        });

        // Confirm Password Match
        document.getElementById('<%= txtConfirmPassword.ClientID %>').addEventListener('input', function () {
            var password = document.getElementById('<%= txtPassword.ClientID %>').value;
            var confirmPassword = this.value;

            if (password !== confirmPassword) {
                this.style.borderColor = "#ff4757";
            } else {
                this.style.borderColor = "#2ecc71";
            }
        });

        // DateTime Update
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
</head>
<body>
    <form id="form1" runat="server">
        <!-- Navigation Bar -->
        <div class="navbar">
            <a href="Default.aspx" class="brand"><i class="fas fa-file-alt"></i> Naresh Resume Creations</a>
            <ul>
                <li><a href="Login.aspx">Login</a></li>
                <li><a href="Registration.aspx">Register</a></li>
                
            </ul>
        </div>

        <!-- Main Content -->
        <div class="welcome-section">
            <div class="main-content">
                <!-- Registration Form -->
                <div class="register-container">
                    <h2>Register</h2>
                    <div class="form-group name-fields">
                        <div class="form-group">
                            <label for="txtFirstName">First Name:</label>
                            <div class="input-container">
                                <i class="fa fa-user"></i>
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="input-field" Placeholder="Enter your first name" required></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="txtLastName">Last Name:</label>
                            <div class="input-container">
                                <i class="fa fa-user"></i>
                                <asp:TextBox ID="txtLastName" runat="server" CssClass="input-field" Placeholder="Enter your last name" required></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="txtEmailPhone">Email :</label>
                        <div class="input-container">
                            <i class="fa fa-envelope"></i>
                            <asp:TextBox ID="txtEmailPhone" runat="server" CssClass="input-field" Placeholder="Enter email" required></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="txtPassword">Password:</label>
                        <div class="input-container">
                            <i class="fa fa-lock"></i>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="input-field" Placeholder="Enter your password" TextMode="Password" required></asp:TextBox>
                        </div>
                        <span id="password-strength" class="password-strength"></span>
                    </div>
                    <div class="form-group">
                        <label for="txtConfirmPassword">Confirm Password:</label>
                        <div class="input-container">
                            <i class="fa fa-lock"></i>
                            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="input-field" Placeholder="Confirm your password" TextMode="Password" required></asp:TextBox>
                        </div>
                    </div>
                    <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn-register" OnClick="btnRegister_Click" />
                    <asp:Label ID="lblRegisterMessage" runat="server" CssClass="error-message"></asp:Label>
                    <div class="login-link">
                        Already have an account? <a href="Login.aspx">Login here</a>
                    </div>
                </div>
                <!-- Info Section with About Section -->
                <div class="info-section">
                    <div class="header-section">
                        <div class="header-image">
                            <img src="data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 100 100' fill='none' stroke='%232d1b3f' stroke-width='2'%3E%3Crect x='10' y='10' width='80' height='80' rx='5'/%3E%3Cline x1='20' y1='25' x2='80' y2='25'/%3E%3Cline x1='20' y1='35' x2='80' y2='35'/%3E%3Crect x='20' y='45' width='60' height='35'/%3E%3C/svg%3E" alt="Resume Icon" />
                        </div>
                        <h1>About Our Platform</h1>
                        <p>Discover why Naresh Resume Creations is your go-to for resume building!</p>
                    </div>
                    <div class="cta-banner">
                        <p>Resume Ready in 5 Minutes!</p>
                    </div>
                    <div class="about-section" id="about">
                        <h3><i class="fas fa-star"></i> Why Choose Us?</h3>
                        <p>
                            Naresh Resume Creations empowers job seekers with stunning, ATS-friendly resumes. Our intuitive platform offers vibrant templates and easy editing tools to make you stand out.
                        </p>
                        <ul>
                            <li>ATS-Optimized</li>
                            <li>Custom Designs</li>
                            <li>Quick Editing</li>
                            <li>PDF Export</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>

        <!-- Footer -->
        <div class="footer">
            <p>Designed by @Naresh</p>
            <p>Contact: <a href="mailto:nareshresumecreations@gmail.com">nareshresumecreations@gmail.com</a> | © 2025 Naresh Resume Creations</p>
            <p id="datetime"></p>
        </div>
    </form>
</body>
</html>