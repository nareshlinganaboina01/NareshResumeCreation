
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NareshResumeCreation.Default" %>

<!DOCTYPE html>
<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="Create professional, ATS-friendly resumes with Naresh Resume Creations. Start building your dream career today!" />
    <title>Welcome to Naresh Resume Creations</title>
    <!-- Font Awesome for Icons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.1/css/all.min.css" integrity="sha512-DTOQO9RWCH3ppGqcWaEA1BIZOC6xxalwEsw9c2QQeAIftl+Vegovlnee1c9QX4TctnWMn13TZye+giMm8e2LwA==" crossorigin="anonymous" referrerpolicy="no-referrer" />
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
            display: flex;
            flex-direction: column;
        }
        .navbar {
            background: linear-gradient(135deg, #4a00e0, #8e2de2);
            padding: 10px 20px;
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
            font-size: 20px;
            font-weight: 600;
            text-decoration: none;
            display: flex;
            align-items: center;
            gap: 8px;
        }
        .navbar .brand i {
            font-size: 24px;
        }
        .navbar ul {
            list-style: none;
            display: flex;
            gap: 20px;
            margin: 0;
            padding: 0;
        }
        .navbar ul li a {
            color: #f3e8ff;
            text-decoration: none;
            font-size: 14px;
            font-weight: 500;
            padding: 8px 15px;
            border-radius: 6px;
            transition: all 0.3s ease;
        }
        .navbar ul li a:hover {
            background: linear-gradient(135deg, #7b3fe4, #b766f4);
            transform: scale(1.05);
        }
        .welcome-section {
            flex: 1;
            width: 100%;
            background: linear-gradient(135deg, rgba(255, 247, 237, 0.95), rgba(254, 243, 199, 0.95));
            position: relative;
            z-index: 1;
            padding-top: 70px;
            padding-bottom: 20px;
            overflow-y: auto;
            scroll-behavior: smooth;
        }
        .welcome-section::before {
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: url('data:image/svg+xml,%3Csvg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 100 100"%3E%3Cpolygon points="50,10 90,90 10,90" fill="rgba(255,255,255,0.2)"/%3E%3C/svg%3E');
            background-size: 100px;
            opacity: 0.2;
            animation: rotate 30s linear infinite;
            z-index: -1;
        }
        @keyframes rotate {
            0% { transform: rotate(0deg); }
            100% { transform: rotate(360deg); }
        }
        .header-section {
            padding: 20px;
            text-align: center;
            position: relative;
            z-index: 2;
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 20px;
        }
        .header-logo {
            font-size: 40px;
            color: #ff6b6b;
            text-shadow: 0 0 12px rgba(255, 107, 107, 0.6);
            animation: glow 2s ease-in-out infinite;
        }
        @keyframes glow {
            0% { text-shadow: 0 0 12px rgba(255, 107, 107, 0.6); }
            50% { text-shadow: 0 0 20px rgba(255, 107, 107, 0.9); }
            100% { text-shadow: 0 0 12px rgba(255, 107, 107, 0.6); }
        }
        .header-section h1 {
            font-size: 28px;
            font-weight: 700;
            color: #2d1b3f;
            margin: 0;
            animation: fadeIn 1s ease-in;
        }
        .header-section p {
            font-size: 14px;
            font-weight: 400;
            color: #2d1b3f;
            max-width: 450px;
            margin: 10px auto 0;
            animation: fadeIn 1.5s ease-in;
        }
        .header-image {
            max-width: 100px;
        }
        .header-image img {
            width: 100%;
            border-radius: 10px;
            box-shadow: 0 2px 6px rgba(0, 0, 0, 0.2);
        }
        @keyframes fadeIn {
            0% { opacity: 0; transform: translateY(15px); }
            100% { opacity: 1; transform: translateY(0); }
        }
        .content-section {
            padding: 20px;
            display: flex;
            flex-direction: column;
            align-items: center;
            z-index: 2;
            gap: 20px;
        }
        .about-resume, .about-section, .contact-section {
            max-width: 700px;
            text-align: center;
            margin-bottom: 20px;
        }
        .about-resume h2, .about-section h2, .contact-section h2 {
            font-size: 18px;
            font-weight: 600;
            color: #2d1b3f;
            margin-bottom: 10px;
            animation: fadeIn 2s ease-in;
        }
        .about-resume p, .about-section p, .contact-section p {
            font-size: 14px;
            line-height: 1.6;
            color: #2d1b3f;
            margin-bottom: 10px;
            animation: fadeIn 2.5s ease-in;
        }
        .about-resume ul {
            list-style: none;
            padding: 0;
            display: flex;
            flex-wrap: wrap;
            justify-content: center;
            gap: 10px;
        }
        .about-resume ul li {
            background: linear-gradient(135deg, #f3e8ff, #e9d8fd);
            padding: 6px 12px;
            border-radius: 15px;
            font-size: 12px;
            color: #2d1b3f;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.15);
        }
        .contact-section .contact-info {
            display: flex;
            flex-direction: column;
            gap: 10px;
            align-items: center;
        }
        .contact-section .contact-info a, .contact-section .contact-info span {
            font-size: 14px;
            color: #2d1b3f;
            text-decoration: none;
            display: flex;
            align-items: center;
            gap: 6px;
        }
        .contact-section .contact-info a:hover {
            color: #ff6b6b;
            text-decoration: underline;
        }
        .contact-section .contact-form {
            display: flex;
            flex-direction: column;
            gap: 10px;
            max-width: 450px;
            margin: 15px auto;
        }
        .contact-section .contact-form input,
        .contact-section .contact-form textarea {
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 6px;
            font-size: 14px;
            width: 100%;
            transition: border-color 0.3s ease;
        }
        .contact-section .contact-form input:focus,
        .contact-section .contact-form textarea:focus {
            border-color: #ff6b6b;
            outline: none;
        }
        .contact-section .contact-form button {
            background: linear-gradient(135deg, #ff6b6b, #ff4757);
            color: #fff;
            border: none;
            padding: 10px;
            border-radius: 6px;
            font-size: 14px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
        }
        .contact-section .contact-form button:hover {
            background: linear-gradient(135deg, #ff4757, #ff6b6b);
            transform: scale(1.05);
            box-shadow: 0 4px 10px rgba(255, 107, 107, 0.5);
        }
        .contact-section .form-message {
            font-size: 12px;
            color: #2d1b3f;
            text-align: center;
            display: none;
        }
        .contact-section .form-message.success {
            color: #28a745;
        }
        .contact-section .form-message.error {
            color: #ff6b6b;
        }
        .cta-banner {
            background: linear-gradient(135deg, #00d4ff, #007bff);
            color: #f3e8ff;
            padding: 10px;
            border-radius: 8px;
            text-align: center;
            max-width: 600px;
            margin: 10px auto;
            box-shadow: 0 3px 8px rgba(0, 123, 255, 0.4);
            animation: fadeIn 3s ease-in;
        }
        .cta-banner p {
            font-size: 14px;
            font-weight: 500;
            margin: 0;
        }
        .stats-section {
            display: flex;
            justify-content: center;
            gap: 20px;
            margin: 10px 0;
        }
        .stat {
            text-align: center;
            color: #2d1b3f;
        }
        .stat h3 {
            font-size: 16px;
            font-weight: 600;
            animation: countUp 2s ease-out;
        }
        .stat p {
            font-size: 12px;
        }
        @keyframes countUp {
            0% { transform: translateY(10px); opacity: 0; }
            100% { transform: translateY(0); opacity: 1; }
        }
        .testimonial-carousel {
            max-width: 600px;
            text-align: center;
            margin: 10px auto;
            position: relative;
            overflow: hidden;
            height: 70px;
        }
        .testimonial {
            position: absolute;
            width: 100%;
            opacity: 0;
            transition: opacity 1s ease;
        }
        .testimonial.active {
            opacity: 1;
        }
        .testimonial p {
            font-size: 13px;
            color: #2d1b3f;
            font-style: italic;
            margin-bottom: 5px;
        }
        .testimonial span {
            font-size: 11px;
            color: #ff6b6b;
            font-weight: 500;
        }
        .btn-container {
            display: flex;
            justify-content: center;
            gap: 10px;
            animation: fadeIn 3s ease-in;
            margin: 20px 0;
        }
        .btn {
            background: linear-gradient(135deg, #ff6b6b, #ff4757);
            color: #fff;
            border: none;
            padding: 12px 30px;
            border-radius: 6px;
            font-size: 16px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            box-shadow: 0 3px 8px rgba(255, 107, 107, 0.4);
            text-decoration: none;
            display: inline-flex;
            align-items: center;
            gap: 6px;
            z-index: 10;
        }
        .btn:hover {
            background: linear-gradient(135deg, #ff4757, #ff6b6b);
            transform: scale(1.1);
            box-shadow: 0 4px 12px rgba(255, 107, 107, 0.5);
        }
        .btn i {
            font-size: 18px;
        }
        .footer {
            background: linear-gradient(135deg, #4a00e0, #8e2de2);
            color: #f3e8ff;
            text-align: center;
            padding: 15px;
            box-shadow: 0 -2px 5px rgba(0, 0, 0, 0.25);
            width: 100%;
            z-index: 1000;
            position: fixed;
            bottom: 0;
        }
        .footer p {
            font-size: 12px;
            margin: 3px 0;
        }
        .footer a {
            color: #fef3c7;
            text-decoration: none;
            font-weight: 500;
        }
        .footer a:hover {
            text-decoration: underline;
        }
        .footer .social-icons {
            display: flex;
            justify-content: center;
            gap: 10px;
            margin-top: 5px;
        }
        .footer .social-icons a {
            color: #f3e8ff;
            font-size: 14px;
            transition: all 0.3s ease;
        }
        .footer .social-icons a:hover {
            color: #fef3c7;
        }
        .btn-container {
            display: flex;
            gap: 8px;
            animation: fadeIn 3s ease-in;
        }
        .btn {
            background: linear-gradient(135deg, #ff6b6b, #ff4757);
            color: #fff;
            border: none;
            padding: 7px 20px;
            border-radius: 6px;
            font-size: 13px;
            font-weight: 600;
            cursor: pointer;
            transition: all 0.3s ease;
            box-shadow: 0 3px 8px rgba(255, 107, 107, 0.4);
            text-decoration: none;
            display: inline-flex;
            align-items: center;
            gap: 5px;
        }
        .btn:hover {
            background: linear-gradient(135deg, #ff4757, #ff6b6b);
            transform: scale(1.1);
            box-shadow: 0 4px 10px rgba(255, 107, 107, 0.5);
        }
        .btn i {
            font-size: 14px;
        }
        .btn-register {
            background: linear-gradient(135deg, #00d4ff, #007bff);
        }
        .btn-register:hover {
            background: linear-gradient(135deg, #007bff, #00d4ff);
            box-shadow: 0 4px 10px rgba(0, 123, 255, 0.5);
        }
        @media (max-width: 768px) {
            .navbar {
                padding: 8px 15px;
                flex-direction: column;
                gap: 8px;
            }
            .navbar .brand {
                font-size: 16px;
            }
            .navbar .brand i {
                font-size: 20px;
            }
            .navbar ul {
                gap: 10px;
            }
            .navbar ul li a {
                font-size: 12px;
                padding: 6px 10px;
            }
            .welcome-section {
                padding-top: 80px;
                padding-bottom: 15px;
            }
            .header-section {
                padding: 15px;
                flex-direction: column;
                gap: 10px;
            }
            .header-logo {
                font-size: 32px;
            }
            .header-section h1 {
                font-size: 22px;
            }
            .header-section p {
                font-size: 12px;
                margin: 5px auto 0;
            }
            .header-image {
                max-width: 80px;
            }
            .content-section {
                padding: 15px;
                gap: 15px;
            }
            .about-resume h2, .about-section h2, .contact-section h2 {
                font-size: 16px;
            }
            .about-resume p, .about-section p, .contact-section p {
                font-size: 12px;
            }
            .about-resume ul li {
                font-size: 10px;
                padding: 5px 10px;
            }
            .contact-section .contact-info a, .contact-section .contact-info span {
                font-size: 12px;
            }
            .contact-section .contact-form input,
            .contact-section .contact-form textarea {
                font-size: 12px;
                padding: 8px;
            }
            .contact-section .contact-form button {
                font-size: 12px;
                padding: 8px;
            }
            .cta-banner {
                padding: 8px;
            }
            .cta-banner p {
                font-size: 12px;
            }
            .stats-section {
                gap: 15px;
            }
            .stat h3 {
                font-size: 14px;
            }
            .stat p {
                font-size: 10px;
            }
            .testimonial-carousel {
                height: 60px;
            }
            .testimonial p {
                font-size: 11px;
            }
            .testimonial span {
                font-size: 10px;
            }
            .btn {
                padding: 10px 20px;
                font-size: 14px;
            }
            .btn-container {
                gap: 8px;
                margin: 15px 0;
            }
            .footer {
                padding: 10px;
            }
            .footer p {
                font-size: 10px;
            }
            .footer .social-icons a {
                font-size: 12px;
            }
        }
    </style>
    <script>
        document.addEventListener('DOMContentLoaded', () => {
            // Testimonial carousel
            const testimonials = document.querySelectorAll('.testimonial');
            let current = 0;

            function showTestimonial(index) {
                testimonials.forEach((t, i) => {
                    t.classList.toggle('active', i === index);
                });
            }

            showTestimonial(current);
            setInterval(() => {
                current = (current + 1) % testimonials.length;
                showTestimonial(current);
            }, 5000);

            // Smooth scrolling for anchor links
            document.querySelectorAll('a[href^="#"]').forEach(anchor => {
                anchor.addEventListener('click', function (e) {
                    e.preventDefault();
                    const target = document.querySelector(this.getAttribute('href'));
                    if (target) {
                        target.scrollIntoView({ behavior: 'smooth' });
                    }
                });
            });

            // Client-side form validation and feedback
            const form = document.querySelector('.contact-form');
            const formMessage = document.querySelector('.form-message');
            if (form) {
                form.addEventListener('submit', (e) => {
                    const name = document.getElementById('<%= txtName.ClientID %>').value.trim();
                    const email = document.getElementById('<%= txtEmail.ClientID %>').value.trim();
                    const message = document.getElementById('<%= txtMessage.ClientID %>').value.trim();
                    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

                    if (!name || !email || !message) {
                        e.preventDefault();
                        if (formMessage) {
                            formMessage.textContent = 'Please fill in all fields.';
                            formMessage.classList.remove('success');
                            formMessage.classList.add('error');
                            formMessage.style.display = 'block';
                        }
                    } else if (!emailRegex.test(email)) {
                        e.preventDefault();
                        if (formMessage) {
                            formMessage.textContent = 'Please enter a valid email address.';
                            formMessage.classList.remove('success');
                            formMessage.classList.add('error');
                            formMessage.style.display = 'block';
                        }
                    } else if (formMessage) {
                        formMessage.style.display = 'none';
                    }
                });
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Navigation Bar -->
        <div class="navbar">
            <a href="#" class="brand" aria-label="Naresh Resume Creations Home"><i class="fas fa-file-alt"></i> Naresh Resume Creations</a>
            <ul>
                <li><a href="#home" aria-label="Go to Home section">Home</a></li>
                <li><a href="#about" aria-label="Go to About section">About</a></li>
                <li><a href="#contact" aria-label="Go to Contact section">Contact</a></li>
            </ul>
        </div>

        <!-- Main Content -->
        <div class="welcome-section">
            <!-- Header -->
            <div class="header-section">
                <div class="header-image">
                    <img src="data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 100 100' fill='none' stroke='%232d1b3f' stroke-width='2'%3E%3Crect x='10' y='10' width='80' height='80' rx='5'/%3E%3Cline x1='20' y1='25' x2='80' y2='25'/%3E%3Cline x1='20' y1='35' x2='80' y2='35'/%3E%3Crect x='20' y='45' width='60' height='35'/%3E%3C/svg%3E" alt="Resume Icon" />
                </div>
                <div>
                    <h1>Welcome to Naresh Resume Creations</h1>
                    <p>Craft professional resumes that land your dream job!</p>
                </div>
            </div>

            <!-- Content -->
            <div class="content-section">
                <div class="about-resume" id="home">
                    <h2>Why Choose Us?</h2>
                    <p>
                        Create professional, ATS-friendly resumes with our intuitive platform. Tailor your resume with customizable templates and download instantly as a PDF.
                    </p>
                    <ul>
                        <li>ATS-Optimized</li>
                        <li>Custom Designs</li>
                        <li>Quick Editing</li>
                        <li>PDF Export</li>
                    </ul>
                </div>
                <div class="btn-container">
                    <asp:LinkButton ID="btnLogin" runat="server" CssClass="btn" OnClick="btnLogin_Click">
                        <i class="fas fa-sign-in-alt"></i> Log In
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnRegister" runat="server" CssClass="btn btn-register" OnClick="btnRegister_Click">
                        <i class="fas fa-user-plus"></i> Register
                    </asp:LinkButton>
                </div>
                <div class="about-section" id="about">
                    <h2>About Naresh Resume Creations</h2>
                    <p>
                        Founded in 2023, Naresh Resume Creations is dedicated to empowering job seekers worldwide. Our mission is to provide user-friendly tools and expert guidance to help you craft resumes that stand out. With a team of career experts and designers, we ensure your resume meets industry standards and ATS requirements, giving you the edge in your job search.
                    </p>
                </div>
                <div class="cta-banner">
                    <p>Create Your Resume in 5 Minutes!</p>
                </div>
                <div class="stats-section">
                    <div class="stat">
                        <h3>1000+</h3>
                        <p>Resumes Created</p>
                    </div>
                    <div class="stat">
                        <h3>500+</h3>
                        <p>Happy Users</p>
                    </div>
                </div>
                <div class="testimonial-carousel">
                    <div class="testimonial active">
                        <p>"This platform made my resume stand out and landed me an interview!"</p>
                        <span>- Priya S.</span>
                    </div>
                    <div class="testimonial">
                        <p>"Easy to use and professional templates. Highly recommend!"</p>
                        <span>- Arjun K.</span>
                    </div>
                    <div class="testimonial">
                        <p>"Downloaded my resume as a PDF in minutes. Amazing!"</p>
                        <span>- Sneha R.</span>
                    </div>
                </div>
                <div class="contact-section" id="contact">
                    <h2>Contact Us</h2>
                    <p>Have questions or need assistance? Reach out to us!</p>
                    <div class="contact-info">
                        <span><i class="fas fa-envelope"></i> <a href="mailto:nareshresumecreations@gmail.com">support@nareshresumecreatios@gmail.com</a></span>
                        <span><i class="fas fa-phone"></i> +91 630 249 9092</span>
                        <span><i class="fas fa-map-marker-alt"></i> 44 Career Street, Khammam, Telangana, India</span>
                    </div>
                    <div class="contact-form">
                        <asp:TextBox ID="txtName" runat="server" Placeholder="Your Name" aria-label="Your Name" />
                        <asp:TextBox ID="txtEmail" runat="server" Placeholder="Your Email" TextMode="Email" aria-label="Your Email" />
                        <asp:TextBox ID="txtMessage" runat="server" Placeholder="Your Message" TextMode="MultiLine" Rows="4" aria-label="Your Message" />
                        <asp:Button ID="btnSubmit" runat="server" Text="Send Message" OnClick="btnSubmit_Click" aria-label="Send Message" />
                        <div class="form-message" id="formMessage"></div>
                    </div>
                </div>
                <div class="btn-container">
                    <asp:LinkButton ID="btnGetStarted" runat="server" CssClass="btn" OnClick="btnGetStarted_Click" aria-label="Start Creating Your Resume">
                        <i class="fas fa-edit"></i> Start Creating
                    </asp:LinkButton>
                </div>
            </div>
        </div>

        <!-- Footer -->
        <div class="footer">
            <p><a href="mailto:nareshresumecreations.com">support@nareshresumecreations@gmail.com</a> | +91 630 249 9092</p>
            <div class="social-icons">
                <a href="https://linkedin.com" aria-label="LinkedIn"><i class="fab fa-linkedin"></i></a>
                <a href="https://twitter.com" aria-label="Twitter"><i class="fab fa-twitter"></i></a>
                <a href="https://instagram.com" aria-label="Instagram"><i class="fab fa-instagram"></i></a>
            </div>
            <p>© 2025 Naresh Resume Creations</p>
        </div>
    </form>
</body>
</html>
