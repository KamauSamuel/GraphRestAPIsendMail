using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using MSGraph.SendEmail.Authentication;
using MSGraph.SendEmail.File_Attachment;
using MSGraph.SendEmail.Model;
using System.Collections.Generic;
using System.Net.Mail;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSGraph.SendEmail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {
        private readonly AppRegistration _options;
        public SendMailController(IOptions<AppRegistration> options)
        {
            _options = options.Value;
        }      
        // POST api/<SendMailController>
        [HttpPost]
        public async Task SendEmail(RequestBody request)
        {
            AuthenticationProvider auth = new AuthenticationProvider(_options.tenantId, _options.clientId, _options.clientSecret);
            FormFileUpload fileup = new FormFileUpload();
            List<FileAttachment> fileprops = await fileup.UploadAttachment(request.fileAttachment);
            MessageCollection collection = new MessageCollection();          
            
            Message message = new() 
            {
                Subject = request.Subject,
                Body = new ItemBody
                {
                    ContentType = BodyType.Text,
                    Content = request.emailBody,
                },
                ToRecipients = new List<Recipient>
                {
                    new Recipient
                    {
                        EmailAddress = new EmailAddress
                        {
                            Address = request.recipient,
                        },
                    },
                },               
                Attachments = collection.AddAttachment(fileprops)
        };
            
            try
            {
                await auth.getGraphClient().Users[_options.userPrincipalName].SendMail(message, true).Request().PostAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }


    }
}
