// See https://aka.ms/new-console-template for more information
// Cool thing about .NET 6 & C# 10.0 - all of this runs inside a Main method after being compiled:

// Load our Email library (DLL) or Assembly(?):
using Email;

// Create an instance of the EmailService class from DLL and invoke SendEmail method:
// TODO: think about resource cleanup, either using w/ IDisposable or try-finally block
EmailService emailService = new EmailService();
emailService.SendEmail(
    "tkasparaitis@spoofed.com", // sender
    "leah@company.com", // recipient
    "Purchase Order #102832", // e-mail subject
    @"Hi Leah,

Thank you for the purchase of our product.

If you have any questions or concerns please feel free to reach out to our customer support at support@spoofed.com.

Kind Regards,
Team", // e-mail body
    "smtp.mailtrap.io", // smtpHost url from mailtrap.io
    2525, //smtpPort number - only a few ports but they vary by provider
    "eb21bf2238088a", // smtp user name/login
    "39ccdc35823c5f"); // smtp password