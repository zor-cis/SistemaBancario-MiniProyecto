using Application.DTOs;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Application.Services
{
    public class EmailService : IEmailService
    {
        public EmailConfiguration _emailConf;

        public EmailService(IConfiguration confi)
        {
            _emailConf = new EmailConfiguration
            { 
                SmtpServer = confi["Email:SmtpServer"] ?? "smtp.gmail.com",
                SmtpPort = int.Parse(confi["Email:SmtpPort"] ?? "587"),
                SenderEmail = confi["Email:SenderEmail"] ?? "",
                SenderName = confi["Email:SenderName"] ?? "Sistema Bancario",
                Username = confi["Email:Username"] ?? "",
                Password = confi["Email:Password"] ?? "",
                EnableSsl = bool.Parse(confi["Email:EnableSsl"] ?? "true")

            };
        }

        public async Task SendWelcomeEmail(EmailRequest dto)
        {
            var subject = "Bienvenido al Sistema Bancario";
            var body = getEmailTemplate(dto.FullName);
            await SendEmail(new EmailRequest
            {
                ToEmail = dto.ToEmail,
                Subject = subject,
                HtmlBody = body,
            });
        }

        public async Task SendEmail(EmailRequest dto)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_emailConf.SenderName, _emailConf.SenderEmail));
                message.To.Add(new MailboxAddress("", dto.ToEmail));
                message.Subject = dto.Subject;

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = dto.HtmlBody
                };
                message.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient();

                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(_emailConf.SmtpServer, _emailConf.SmtpPort,
                    _emailConf.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto);

                await client.AuthenticateAsync(_emailConf.Username, _emailConf.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al enviar mensaje: {ex.Message}");
            }
        }
        private string getEmailTemplate (string fullname) 
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='utf-8'>
    <title>Bienvenido</title>
    <style>
        body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
        .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
        .header {{ background-color: #007bff; color: white; padding: 20px; text-align: center; }}
        .content {{ padding: 30px; background-color: #f8f9fa; }}
        .footer {{ background-color: #6c757d; color: white; padding: 15px; text-align: center; font-size: 12px; }}
        .btn {{ display: inline-block; padding: 12px 24px; background-color: #28a745; color: white; text-decoration: none; border-radius: 5px; margin-top: 20px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Sistema Bancario</h1>
        </div>
        <div class='content'>
            <h2>Hola {fullname},</h2>
            <p>¡Gracias por registrarte en nuestro sistema bancario! Tu cuenta ha sido creada exitosamente.</p>
            <p>Ahora puedes acceder a todos nuestros servicios:</p>
            <ul>
                <li>Crear cuentas de ahorro y corrientes</li>
                <li>Realizar depósitos y retiros</li>
                <li>Consultar tu saldo</li>
            </ul>
            <p>Para comenzar, inicia sesión en tu cuenta con las credenciales que proporcionaste durante el registro.</p>
            <p>¡Gracias por confiar en nosotros!</p>
        </div>
        <div class='footer'>
            <p>&copy; 2025 Sistema Bancario. Todos los derechos reservados.</p>
        </div>
    </div>
</body>
</html>";
        }
    

    }
}
