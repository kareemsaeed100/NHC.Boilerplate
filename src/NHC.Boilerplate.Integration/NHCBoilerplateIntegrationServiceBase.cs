using Abp;
using Abp.Dependency;

namespace NHC.Boilerplate.Integration;

internal abstract class NHCBoilerplateIntegrationServiceBase : AbpServiceBase
{
    protected NHCBoilerplateIntegrationServiceBase(IIocManager iocManager)
    {
    }
}
