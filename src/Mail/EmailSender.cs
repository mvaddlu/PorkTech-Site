using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailSender
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUsername;
    private readonly string _smtpPassword;

    public EmailSender(SmtpData smtpData) : this(smtpData.SmtpServer, smtpData.SmtpPort, smtpData.SmtpUsername, smtpData.SmtpPassword)
    {
    }

    public EmailSender(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
    {
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _smtpUsername = smtpUsername;
        _smtpPassword = smtpPassword;
    }

    public async Task SendEmailAsync(string fromAddress, string toAddress, string subject, string body)
    {
        using MailMessage mail = new MailMessage();
        mail.From = new MailAddress(fromAddress);
        mail.To.Add(new MailAddress(toAddress));
        mail.Subject = subject;
        mail.Body = body;

        using SmtpClient smtp = new SmtpClient(_smtpServer, _smtpPort);
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
        smtp.EnableSsl = true;

        await smtp.SendMailAsync(mail);
    }

    public async Task CreateLead(BusinessForm businessForm)
    {
        await SendEmailAsync(
            _smtpUsername, 
            _smtpUsername,
            "New lead(PorkTech)",
            $"Business Name: {businessForm.BusinessName}\nBusiness Type: {businessForm.BusinessType}\nContact Email: {businessForm.ContactEmail}\nContact Phone: {businessForm.ContactPhone}"
        );
    }
}
