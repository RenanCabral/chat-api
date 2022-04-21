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
            StdSchedulerFactory factory = new StdSchedulerFactory();

            IScheduler scheduler = await factory.GetScheduler();

            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<StockQuoteJob>()
                                            .WithIdentity("stock-quote-job", "Job")
                                            .Build();

            ITrigger trigger = TriggerBuilder.Create()
                                .WithIdentity("stock-quote-job-trigger", "Job")
                                .StartNow()
                                .WithSimpleSchedule(x => x.WithIntervalInSeconds(1).RepeatForever())
                                .Build();

            await scheduler.ScheduleJob(job, trigger);

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
