<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Downloads.aspx.cs" Inherits="NareshResumeCreation.Downloads" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Downloads - Naresh Resume Creation</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css">
    <style type="text/css">
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

        .navbar-container {
            max-width: 1000px;
            margin: 0 auto;
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 0 20px;
        }

        .navbar-brand {
            color: #f3e8ff;
            font-size: 24px;
            font-weight: 600;
            text-decoration: none;
        }

        .navbar-menu {
            list-style: none;
            margin: 0;
            padding: 0;
            display: flex;
        }

        .navbar-menu li {
            margin-left: 20px;
        }

        .navbar-menu a, .navbar-menu input[type="submit"] {
            color: #f3e8ff;
            text-decoration: none;
            font-weight: 600;
            font-size: 14px;
            padding: 8px 15px;
            border-radius: 5px;
            background: none;
            border: none;
            cursor: pointer;
            transition: all 0.3s ease;
        }

        .navbar-menu a:hover, .navbar-menu input[type="submit"]:hover,
        .navbar-menu a.active, .navbar-menu input[type="submit"].active {
            background: linear-gradient(135deg, #7b3fe4, #b766f4);
            color: #fff;
        }

        /* Header Section */
        .header {
            background: linear-gradient(135deg, #ff9f1c, #ff6200);
            color: #fff;
            padding: 20px;
            text-align: center;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.25);
            margin-top: 70px;
        }

        .header h1 {
            margin: 0;
            font-size: 32px;
            font-weight: 700;
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.3);
        }

        /* Main Content */
        .container {
            max-width: 1000px;
            margin: 40px auto 80px auto;
            background: linear-gradient(135deg, #fff7ed, #fef3c7);
            border-radius: 20px;
            box-shadow: 0 8px 24px rgba(0, 0, 0, 0.2);
            padding: 40px;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
        }

        .container:hover {
            transform: translateY(-5px);
            box-shadow: 0 12px 32px rgba(0, 0, 0, 0.25);
        }

        h2 {
            background: linear-gradient(135deg, #00d4ff, #007bff);
            color: #fff;
            padding: 15px;
            border-radius: 10px;
            margin-bottom: 30px;
            font-weight: 600;
            font-size: 24px;
            text-shadow: 1px 1px 2px rgba(0, 0, 0, 0.3);
            text-align: center;
        }

        /* Resume List */
        .resume-list {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
            background: linear-gradient(135deg, #f3e8ff, #e9d8fd);
            border-radius: 10px;
            overflow: hidden;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.2);
        }

        .resume-list th, .resize-list td {
            padding: 15px;
            text-align: left;
            border-bottom: 1px solid #d6bcfa;
        }

        .resume-list th {
            background: linear-gradient(135deg, #4a00e0, #8e2de2);
            color: #f3e8ff;
            font-weight: 600;
        }

        .resume-list tr:hover {
            background: linear-gradient(135deg, #e9d8fd, #d6bcfa);
        }

        .resume-list a, .resume-list input[type="submit"] {
            color: #ff6200;
            text-decoration: none;
            font-weight: 500;
            padding: 8px 12px;
            border-radius: 5px;
            transition: all 0.3s ease;
            display: inline-flex;
            align-items: center;
            gap: 8px;
        }

        .resume-list a:hover, .resume-list input[type="submit"]:hover {
            color: #ff9f1c;
            background: linear-gradient(135deg, #fff7ed, #fef3c7);
            text-decoration: none;
        }

        .resume-list i {
            color: #6b46c1;
            font-size: 16px;
        }

        .no-resumes {
            text-align: center;
            color: #9f1239;
            font-size: 16px;
            margin-top: 20px;
            font-weight: 500;
            text-shadow: 1px 1px 1px rgba(0, 0, 0, 0.1);
        }

        /* Button */
        .btn {
            background: linear-gradient(135deg, #ff6b6b, #ff4757);
            color: #fff;
            padding: 14px;
            border: none;
            border-radius: 10px;
            font-size: 16px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            box-shadow: 0 4px 12px rgba(255, 107, 107, 0.4);
            width: 100%;
            max-width: 300px;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            gap: 10px;
            text-decoration: none;
            margin: 20px auto;
        }

        .btn:hover {
            background: linear-gradient(135deg, #ff4757, #ff6b6b);
            transform: scale(1.03);
            box-shadow: 0 6px 16px rgba(255, 107, 107, 0.5);
        }

        /* Footer Section */
        .footer {
            background: linear-gradient(135deg, #4a00e0, #8e2de2);
            color: #fff;
            text-align: center;
            padding: 5px 20px;
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
            0% { color: #ff00ff; }
            20% { color: #00ff00; }
            40% { color: #ff9900; }
            60% { color: #00ffff; }
            80% { color: #ff3366; }
            100% { color: #ff00ff; }
        }

        @keyframes colorChangeDate {
            0% { color: #00ff00; }
            20% { color: #ff9900; }
            40% { color: #00ffff; }
            60% { color: #ff3366; }
            80% { color: #ff00ff; }
            100% { color: #00ff00; }
        }

        @keyframes colorChangeTime {
            0% { color: #ff9900; }
            20% { color: #00ffff; }
            40% { color: #ff3366; }
            60% { color: #ff00ff; }
            80% { color: #00ff00; }
            100% { color: #ff9900; }
        }

        /* Responsive Design */
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
                padding: 10px;
                font-size: 14px;
            }
            .btn {
                width: 100%;
                max-width: none;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Navigation Bar -->
        <div class="navbar">
            <div class="navbar-container">
                <a href="Home.aspx" class="navbar-brand">Naresh Resume Creation</a>
                <ul class="navbar-menu">
                    <li><asp:LinkButton ID="lnkHome" runat="server" Text="Home" OnClick="lnkHome_Click" /></li>
                     <li><asp:LinkButton ID="lnkDownloads" runat="server" Text="Downloads" OnClick="lnkDownloads_Click" CssClass="active" /></li>
                    <li><asp:LinkButton ID="lnkLogout" runat="server" Text="Logout" OnClick="lnkLogout_Click" /></li>
                </ul>
            </div>
        </div>

        <!-- Header Section -->
        <div class="header">
            <h1>Downloaded Resumes</h1>
        </div>

        <!-- Main Content -->
        <div class="container">
            <h2>Your Resumes</h2>
            <asp:GridView ID="gvResumes" runat="server" AutoGenerateColumns="False" CssClass="resume-list" 
                EmptyDataText="No resumes have been downloaded yet." EmptyDataRowStyle-CssClass="no-resumes">
                <Columns>
                    <asp:BoundField DataField="FileName" HeaderText="Resume File" />
                    <asp:TemplateField HeaderText="Date Modified">
                        <ItemTemplate>
                            <%# Eval("DateCreated", "{0:MMM dd, yyyy HH:mm:ss}") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkDownload" runat="server" NavigateUrl='<%# "~/Resumes/" + Eval("FileName") %>' 
                                Text="View/Download" CssClass="download-link">
                                <i class="fa fa-download"></i> View/Download
                            </asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandArgument='<%# Eval("FileName") %>' 
                                OnCommand="lnkDelete_Command" OnClientClick="return confirm('Are you sure you want to delete this resume?');">
                                <i class="fa fa-trash"></i> Delete
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            
        </div>

        <!-- Footer Section -->
        <div class="footer">
            <p>Designed by @Naresh</p>
            <p>Contact: <a href="mailto:nareshresumecreations@gmail.com">nareshresumecreations@gmail.com</a> | © 2025 Naresh Resume Creation</p>
            <p id="datetime"></p>
        </div>
    </form>

    <script>
        // JavaScript to update date, time, and day
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

        // Update immediately and then every second
        updateDateTime();
        setInterval(updateDateTime, 1000);
    </script>
</body>
</html>