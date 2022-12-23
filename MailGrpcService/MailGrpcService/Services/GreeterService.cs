using Grpc.Core;
using MailGrpcService;
using System.Net.Mail;
using System.Net;

namespace MailGrpcService.Services
{
    public class GreeterService : SendMailService.SendMailServiceBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<MailReply> SendMail(MailRequest request, ServerCallContext context)
        {
            string fromMail = "recyclecoin.destek@gmail.com"; //mail g�nderen
            string fromPassword = "wdjninmmcssepzlr";

            MailMessage message = new MailMessage();

            message.From = new MailAddress(fromMail);
            message.Subject = "�ifre yenileme";       //mail konusu

            message.To.Add(new MailAddress(request.ToMailAddress));      //g�nderilecek mail adresi
            message.Body = "<html><body> Yeni �ifreniz </body></html>" + request.NewPassword;  // g�nderilecek mail i�eri�i
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")      //smtp aya�a kald�rd�k
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
            return Task.FromResult(new MailReply
            {
                Message = "ok"
            });
        }
    }
}