using SendGrid;
using SendGrid.Helpers.Mail;

namespace DisneyApi.Services.EmailService
{
    public class EmailService
    {
        public async Task SendRegisterEmail(string newUser,string name, string password)
        {
            var client = new SendGridClient("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");
            var from = new EmailAddress("ezecaliguri@gmail.com", "Bienvenido a la api de Disney");
            var subject = $"Gracias por Registrarse en la Api: {name} ";
            var to = new EmailAddress(newUser, "Nuevo usuario creado");
            var plainTextContent = "prueba api";
            var htmlContent = $"<strong><br/>Gracias por registrarse en la Api <br><br/>  Usuario: {name} <br><br/> Contraseña: {password}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

        public async Task SendLoginEmail(string token, DateTime validTo, string name, string email)
        {
            var validToArg = validTo.AddHours(-3);
            var client = new SendGridClient("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");
            var from = new EmailAddress("ezecaliguri@gmail.com", "Bienvenido a la api de Disney");
            var subject = $"Gracias por logearse en la Api: {name} ";
            var to = new EmailAddress(email, "Login creado");
            var plainTextContent = "prueba api";
            var htmlContent = $"<strong><br/>Gracias por logearse en la Api <br><br/>  Usuario: {name} <br><br/> Token: {token} <br><br/> Validez: {validTo} Horario UTC. <br><br/> Hora Argentina: {validToArg}</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}
