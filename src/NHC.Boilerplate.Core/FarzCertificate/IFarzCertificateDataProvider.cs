using NHC.Boilerplate.FarzCertificate.Dto;
using System.Threading.Tasks;

namespace NHC.Boilerplate.FarzCertificate;
public interface IFarzCertificateDataProvider
{
    Task<CertificateUrlDto> GetCertificateUrlAsync(CertificateUrlRequest request);
}


