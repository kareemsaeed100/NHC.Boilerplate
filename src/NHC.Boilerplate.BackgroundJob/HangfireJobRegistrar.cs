using Hangfire;
using NHC.Boilerplate.BackgroundJob.Jobs;
namespace NHC.Boilerplate.BackgroundJob
{
    public static class HangfireJobRegistrar
    {
        public static void Register()
        {
            Hangfire.BackgroundJob.Schedule<SampleJobWrapper>(
                job => job.Execute(),
                TimeSpan.FromMinutes(1)
            );

            RecurringJob.AddOrUpdate<SampleJobWrapper>(
                "SampleJobRecurring",
                job => job.Execute(),
                Cron.Minutely
            );
        }
    }

}
