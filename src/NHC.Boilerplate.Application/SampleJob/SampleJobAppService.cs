using Abp.Application.Services;
using Castle.Core.Logging;
using System;

namespace NHC.Boilerplate.SampleJob;

public class SampleJobAppService : ApplicationService, ISampleJobAppService
{
    private readonly ILogger _logger;

    public SampleJobAppService(ILogger logger)
    {
        _logger = logger;
    }

    public void DoWork()
    {
        _logger.Info("Sample Hangfire job executed at: " + DateTime.Now);
    }
}
