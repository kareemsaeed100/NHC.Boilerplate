using Abp.Configuration;
using Abp.Dependency;
using Abp.UI;
using NHC.Boilerplate.FarzCertificate;
using NHC.Boilerplate.FarzCertificate.Dto;
using NHC.Boilerplate.Integration.Exceptions;
using NHC.Boilerplate.Integration.NHCIntegrationClient;
using NHC.Boilerplate.Integration.Services.FarzCertificate.Dto;

namespace NHC.Boilerplate.Integration.Services.FarzCertificate;

internal class FarzCertificateIntegrationProvider(INHClient nhcClient, ISettingManager settingManager, IIocManager iocManager, IFarzCertificateDataProviderMapper factory) : NHCBoilerplateIntegrationServiceBase(iocManager), IFarzCertificateDataProvider
{
    public async Task<CertificateUrlDto> GetCertificateUrlAsync(CertificateUrlRequest request)
    {
        try
        {
            var uri = await settingManager.GetSettingValueAsync(FarzConstants.SettingNames.GetCertificateUrl);
            var queryParam = factory.BuildCertificateUrlQueryParams(request.Id);

            var response = await nhcClient.GetAsync<CertificateResponse>(uri, urlParam: queryParam);

            if (response.Body != null && !response.Body.Success)
                throw new UserFriendlyException("CertificateFailed");

            return await factory.MapToCertificateUrlDtoAsync(response.Body);
        }
        catch (ClientCommunionException ex)
        {
            Logger.Error("NHClientCommunionException While Executing FarzContractsProvider.GetCertificateUrl", ex);
            throw new UserFriendlyException("CertificateFailed");
        }
    }
}
