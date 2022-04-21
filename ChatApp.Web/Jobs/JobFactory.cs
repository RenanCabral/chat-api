using Quartz;
using Quartz.Spi;

namespace ChatApp.Web.Jobs
{
    public class JobFactory : IJobFactory
    {
        private readonly IServiceProvider serviceProvider;

        public JobFactory(IServiceProvider serviceProvider) => (this.serviceProvider) = serviceProvider;

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return serviceProvider.GetService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            IDisposable disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }
}
