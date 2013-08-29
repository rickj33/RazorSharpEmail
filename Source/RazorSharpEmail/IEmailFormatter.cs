using System.Net.Mail;

namespace RazorSharpEmail {
    public interface IEmailFormatter {
        MailMessage BuildMailMessageFrom<TModel>(TModel model, string templateName = null);
        TemplatedEmail BuildTemplatedEmailFrom<TModel>(TModel model, string templateName = null);
        MailMessage BuildMailMessageFrom(TemplatedEmail templatedEmail);
    }
}