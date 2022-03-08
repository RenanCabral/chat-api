using ChatApp.Application.Services;
using ChatApp.DataContracts;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ChatApp.Web.Controllers
{
    public class CommandMessageController : ControllerBase
    {
        private IMessageService messageService;

        public CommandMessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpPost]
        [Route("commandMessage")]
        public async Task<HttpResponseMessage> SendAsync([FromBody] CommandMessage message)
        {
            try
            {
                await messageService.SendAsync(message);

                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent("APPL.US quote is $93.42 per share.");
                
                return response;
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
