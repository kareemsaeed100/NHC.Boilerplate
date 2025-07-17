using Microsoft.AspNetCore.Mvc;
using NHC.Boilerplate.FarzCertificate.AppService.Dto;
using System.Threading.Tasks;

namespace NHC.Boilerplate.FarzCertificate.AppService;

//[AbpAuthorize]
public class FarzCertificateAppService(IFarzCertificateDataProvider provider, IFarzCertificateAppServiceMapper mapper) : BoilerplateAppServiceBase, IFarzCertificateAppService
{

    [HttpGet]
    public async Task<CertificateUrlResponseDto> GetCertificateUrl(CertificateUrlRequestDto request)
    {
        var mapRequest = mapper.MapToGetCertificateUrlDto(request);
        var result = await provider.GetCertificateUrlAsync(mapRequest);
        return mapper.MapToCertificateUrlResponseDto(result);
    }
}
