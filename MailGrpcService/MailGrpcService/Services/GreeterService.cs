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
            string fromMail = "recyclecoin.destek@gmail.com"; //mail gönderen
            string fromPassword = "wdjninmmcssepzlr";

            MailMessage message = new MailMessage();

            message.From = new MailAddress(fromMail);
            message.Subject = "þifre yenileme";       //mail konusu

            message.To.Add(new MailAddress(request.ToMailAddress));      //gönderilecek mail adresi
            message.Body = "<html><body> Yeni þifreniz </body></html>" + request.NewPassword;  // gönderilecek mail içeriði
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")      //smtp ayaða kaldýrdýk
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