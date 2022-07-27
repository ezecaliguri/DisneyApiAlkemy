using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace DisneyApi.Services.EmailService
{
    public class EmailService 
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            
        }
        public async Task SendRegisterEmail(string newUser,string name, string password)
        {
            var ApiKey = _configuration.GetSection("Key_SendGrid").Value;
            var emailFrom = _configuration.GetSection("Email_from_SendGrid").Value;


            var client = new SendGridClient(ApiKey);
            var from = new EmailAddress(emailFrom, "Bienvenido a la api de Disney");
            var subject = $"Gracias por Registrarse en la Api: {name} ";
            var to = new EmailAddress(newUser, "Nuevo usuario creado");
            var plainTextContent = "prueba api";
            var htmlContent = $"<strong><br/>Gracias por registrarse en la Api <br><br/>  Usuario: {name} <br><br/> Contraseña: {password}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

        public async Task SendLoginEmail(string token, DateTime validTo, string name, string email)
        {
            var ApiKey = _configuration.GetSection("Key_SendGrid").Value;
            var emailFrom = _configuration.GetSection("Email_from_SendGrid").Value;

            var validToArg = validTo.AddHours(-3);
            var client = new SendGridClient(ApiKey);
            var from = new EmailAddress(emailFrom, "Bienvenido a la api de Disney");
            var subject = $"Gracias por logearse en la Api: {name} ";
            var to = new EmailAddress(email, "Login creado");
            var plainTextContent = "prueba api";
            var htmlContent = $"<strong><br/>Gracias por logearse en la Api <br><br/>  Usuario: {name} <br><br/> Token: {token} <br><br/> Validez: {validTo} Horario UTC. <br><br/> Hora Argentina: {validToArg}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}
