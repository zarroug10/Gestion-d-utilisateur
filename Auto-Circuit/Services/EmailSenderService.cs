using System.Net;
using System.Net.Mail;

namespace Auto_Circuit.Services;

public class EmailSenderService(IConfiguration configuration)
{
    public Task SendEmailAync(string receivingEmail, string Subject, string content
        , bool IsBodyHtml = false)
    {
        //Retrieve the SMTP configurations 
        string? MailServer = configuration["EmailSettings:MailServer"];

        int Port = Convert.ToInt32(configuration["EmailSettings:MailPort"]);

        string? sender = configuration["EmailSettings:SenderName"];

        string? email = configuration["EmailSettings:FromEmail"];

        string? Password = configuration["EmailSettings:Password"];

        //Create the SMTP client using the mail server and port number

        var client = new SmtpClient(MailServer, Port)
        {
            //Set he credentials(email and password)^for the SMTP server
            Credentials = new NetworkCredential(email, Password),

            //Enable SSL for secure email communication
            EnableSsl = true
        };

        //Create a MailAddress Object with the sender email and display name .

        MailAddress fromAddress = new MailAddress(email, sender); //(sender,receiver)

        //create a MailMessage object to define  the email properties.
        MailMessage mailMessage = new MailMessage
        {
            From = fromAddress,
            Subject = Subject,
            Body = content,
            IsBodyHtml = IsBodyHtml
        };

        //Add the recipient's email address to the mail message
        mailMessage.To.Add(receivingEmail);

        //Send the email using the SMTP client
        return client.SendMailAsync(mailMessage);
    }
}
