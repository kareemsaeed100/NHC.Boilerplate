using Abp.Application.Services;

namespace NHC.Boilerplate.SampleJob;

public interface ISampleJobAppService : IApplicationService
{
    void DoWork();
}
