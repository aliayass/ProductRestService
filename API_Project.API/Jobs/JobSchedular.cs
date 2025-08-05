using Hangfire;

namespace API_Project.API.Jobs
{
    public class JobScheduler
    {
        private readonly IRecurringJobManager _jobManager;

        public JobScheduler(IRecurringJobManager jobManager)
        {
            _jobManager = jobManager;
        }

        public void Schedule()
        {
            _jobManager.AddOrUpdate<ProductJsonJob>(
                "save-products-json",
                job => job.ExecuteAsync(),
                "*/2 * * * *"
            );
        }
    }
}
