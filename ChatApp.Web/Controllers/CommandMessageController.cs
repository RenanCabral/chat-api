using ChatApp.Application.Services;
using ChatApp.DataContracts;
using ChatApp.Web.Jobs;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using Quartz.Impl;
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

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
