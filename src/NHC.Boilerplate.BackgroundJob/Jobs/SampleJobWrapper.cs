using Abp.Dependency;
using NHC.Boilerplate.SampleJob;

namespace NHC.Boilerplate.BackgroundJob.Jobs;

public class SampleJobWrapper : ITransientDependency
{
    private readonly ISampleJobAppService _sampleJobAppService;

    public SampleJobWrapper(ISampleJobAppService sampleJobAppService)
    {
        _sampleJobAppService = sampleJobAppService;
    }

    public void Execute()
    {
        _sampleJobAppService.DoWork();
    }
}
