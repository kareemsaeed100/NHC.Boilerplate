using NHC.Boilerplate.FarzCertificate.AppService.Dto;
using System.Threading.Tasks;

namespace NHC.Boilerplate.FarzCertificate.AppService;

public interface IFarzCertificateAppService
{
    Task<CertificateUrlResponseDto> GetCertificateUrl(CertificateUrlRequestDto request);
}
