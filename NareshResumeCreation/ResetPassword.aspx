<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="NareshResumeCreation.ResetPassword" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Password - Naresh Resume Creation</title>
    
    <!-- Font Awesome for Icons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css">
    
    <style>
        /* Global Styling */
        body {
            font-family: 'Poppins', sans-serif;
            background: linear-gradient(135deg, #ff6b6b, #ffbe76, #80ed99);
            margin: 0;
            padding: 0;
            color: #2d1b3f;
        }

        /* Main Content */
        .main-content {
            padding: 40px 20px;
            margin-bottom: 80px;
            display: flex;
            justify-content: center;
            align-items: center;
            background: linear-gradient(135deg, rgba(255, 255, 255, 0.3), rgba(255, 255, 255, 0.1));
            min-height: 100vh;
        }

        /* Form Container */
        .reset-container {
            background: linear-gradient(135deg, #fff7ed, #fef3c7);
            padding: 40px;
            border-radius: 20px;
            box-shadow: 0 8px 24px rgba(0, 0, 0, 0.2);
            width: 100%;
            max-width: 420px;
            text-align: center;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
        }

        .reset-container:hover {
            transform: translateY(-5px);
            box-shadow: 0 12px 32px rgba(0, 0, 0, 0.25);
        }

        /* Heading */
        h2 {
            background: linear-gradient(135deg, #00d4ff, #007bff);
            color: #fff;
            padding: 15px;
            border-radius: 10px;
            margin-bottom: 30px;
            font-weight: 600;
            font-size: 24px;
            text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.3);
        }

        /* Form Fields */
        .form-group {
            margin-bottom: 25px;
            text-align: left;
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

        /* Reset Button */
        .btn-reset {
            background: linear-gradient(135deg, #06b6d4, #00d4ff);
            color: #fff;
            border: none;
            padding: 14px;
            border-radius: 10px;
            font-size: 16px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            box-shadow: 0 4px 12px rgba(6, 182, 212, 0.4);
            width: 100%;
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 10px;
        }

        .btn-reset:hover {
            background: linear-gradient(135deg, #00d4ff, #06b6d4);
            transform: scale(1.03);
            box-shadow: 0 6px 16px rgba(6, 182, 212, 0.5);
        }

        /* Error Message */
        .error-message {
            color: #9f1239;
            font-size: 14px;
            margin-top: 15px;
            display: block;
            font-weight: 500;
            text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.1);
        }

        /* Back to Login Link */
        .login-link {
            margin-top: 20px;
            font-size: 14px;
            color: #4c1d95;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main-content">
            <div class="reset-container">
                <h2>Reset Password</h2>
                
                <div class="form-group">
                    <label for="txtNewPassword">New Password:</label>
                    <div class="input-container">
                        <i class="fa fa-lock"></i>
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="input-field" Placeholder="Enter new password" TextMode="Password" required></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <label for="txtConfirmPassword">Confirm Password:</label>
                    <div class="input-container">
                        <i class="fa fa-lock"></i>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="input-field" Placeholder="Confirm new password" TextMode="Password" required></asp:TextBox>
                    </div>
                </div>

                <asp:Button ID="btnSubmitReset" runat="server" Text="Reset Password" CssClass="btn-reset" OnClick="btnSubmitReset_Click" />
                
                <asp:Label ID="lblResetMessage" runat="server" CssClass="error-message"></asp:Label>
                
                <div class="login-link">
                    <a href="Login.aspx">Back to Login</a>
                </div>
            </div>
        </div>
    </form>
</body>
</html>