<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="YourNamespace.ForgotPassword" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forgot Password</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 30px; }
        input[type=text], input[type=password], textarea {
            padding: 8px; width: 300px; margin: 5px 0 10px 0; border-radius: 4px; border: 1px solid #ccc;
        }
        button, input[type=submit] {
            background-color: #4CAF50; color: white; padding: 10px 18px; border: none; border-radius: 5px;
            cursor: pointer; font-size: 16px;
        }
        button:hover, input[type=submit]:hover {
            background-color: #45a049;
        }
        .message {
            margin: 10px 0; font-weight: bold;
        }
        .green { color: green; }
        .red { color: red; }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <!-- Panel to request OTP -->
        <asp:Panel ID="pnlRequestOTP" runat="server" Visible="true">
            <asp:Label ID="lblForgotMessage" runat="server" CssClass="message red"></asp:Label><br />
            <asp:TextBox ID="txtForgotEmailPhone" runat="server" Placeholder="Enter your Email or Phone"></asp:TextBox><br />
            <asp:Button ID="btnSendForgotOTP" runat="server" Text="Send OTP" OnClick="btnSendForgotOTP_Click" />
        </asp:Panel>

        <br />

        <!-- Panel to verify OTP and reset password -->
        <asp:Panel ID="pnlResetPassword" runat="server" Visible="false">
            <asp:Label ID="lblForgotMessageReset" runat="server" CssClass="message red"></asp:Label><br />

            <asp:TextBox ID="txtForgotOTP" runat="server" Placeholder="Enter OTP"></asp:TextBox><br />
            <asp:Button ID="btnVerifyForgotOTP" runat="server" Text="Verify OTP" OnClick="btnVerifyForgotOTP_Click" /><br /><br />

            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" Placeholder="New Password"></asp:TextBox><br />
            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" Placeholder="Confirm Password"></asp:TextBox><br />
            <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" OnClick="btnResetPassword_Click" />
        </asp:Panel>

    </form>
</body>
</html>
