using NHC.Boilerplate.FarzCertificate.AppService.Dto;
using NHC.Boilerplate.FarzCertificate.Dto;

namespace NHC.Boilerplate.FarzCertificate.AppService;

public interface IFarzCertificateAppServiceMapper
{
    public CertificateUrlRequest MapToGetCertificateUrlDto(CertificateUrlRequestDto request);
    public CertificateUrlResponseDto MapToCertificateUrlResponseDto(CertificateUrlDto request);
}
internal class FarzCertificateAppServiceMapper : IFarzCertificateAppServiceMapper
{
    public CertificateUrlRequest MapToGetCertificateUrlDto(CertificateUrlRequestDto request)
    {
        return new CertificateUrlRequest { Id = request.Id };
    }

    public CertificateUrlResponseDto MapToCertificateUrlResponseDto(CertificateUrlDto request)
    {
        return new CertificateUrlResponseDto
        {
            CertificateUrl = request.CertificateUrl
        };
    }
}
