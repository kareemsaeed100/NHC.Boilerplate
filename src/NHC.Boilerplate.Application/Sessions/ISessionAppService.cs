using Abp.Application.Services;
using NHC.Boilerplate.Sessions.Dto;
using System.Threading.Tasks;

namespace NHC.Boilerplate.Sessions;

public interface ISessionAppService : IApplicationService
{
    Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
}
