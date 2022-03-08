using ChatApp.Application.Services;
using ChatApp.DataContracts;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ChatApp.Web.Controllers
{
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpPost]
        [Route("message")]
        public async Task<HttpResponseMessage> SendAsync([FromBody] CommandMessage message)
        {
            try
            {
                await messageService.SendAsync(message);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (System.Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}