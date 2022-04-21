using Quartz;
using Quartz.Impl;

namespace ChatApp.Web.Jobs
{
    public static class SchedulerExtensions
    {
        public static void UseScheduler(this IServiceCollection services)
        {
            services.AddScoped<StockQuoteJob>();

            var serviceProvider = services.BuildServiceProvider();

            StdSchedulerFactory factory = new StdSchedulerFactory();

            IScheduler scheduler = factory.GetScheduler().Result;

            scheduler.JobFactory = new JobFactory(serviceProvider);
            scheduler.Start().Wait();

            ScheduleJob(scheduler);
        }

        private static void ScheduleJob(IScheduler scheduler)
        {
            IJobDetail job = JobBuilder.Create<StockQuoteJob>()
                                .WithIdentity("stock-quote-job", "Job")
                                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                                .WithIdentity("stock-quote-job-trigger", "Job")
                                .StartNow()
                                .WithSimpleSchedule(x => x.WithIntervalInSeconds(1).RepeatForever())
                                .Build();

            scheduler.ScheduleJob(job, trigger).Wait();
        }
    }
}