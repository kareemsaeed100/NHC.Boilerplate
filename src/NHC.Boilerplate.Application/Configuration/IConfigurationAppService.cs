using NHC.Boilerplate.Configuration.Dto;
using System.Threading.Tasks;

namespace NHC.Boilerplate.Configuration;

public interface IConfigurationAppService
{
    Task ChangeUiTheme(ChangeUiThemeInput input);
}
