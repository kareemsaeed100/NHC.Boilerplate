using Abp.Dependency;
using Newtonsoft.Json;
using NHC.Boilerplate.Integration.Exceptions;
namespace NHC.Boilerplate.Integration.NHCIntegrationClient;

internal interface INHClient
{
    Task<NHCBaseResponse<TResponse>> GetAsync<TResponse>(string methodUri, Dictionary<string, string>? urlParam = null, Dictionary<string, string>? headers = null);
}
internal class NHCClient(IHttpClientFactory httpClientFactory, IIocManager iocManager) : NHCBoilerplateIntegrationServiceBase(iocManager), INHClient
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    public async Task<NHCBaseResponse<TResponse>> GetAsync<TResponse>(string methodUri, Dictionary<string, string>? urlParam = null, Dictionary<string, string>? headers = null)
    {
        string BaseUrl = SettingManager.GetSettingValue(NhcClientSetting.BaseUrl);
        methodUri = AddUrlParams(methodUri, urlParam);
        string fullUrl = BaseUrl + methodUri;
        try
        {

            var client = _httpClientFactory.CreateClient();

            AddHeaders(client, headers);

            Logger.Info($"Requesting to {fullUrl} succeeded.  Headers: {JsonConvert.SerializeObject(client.DefaultRequestHeaders)}.");
            var httpResponse = await client.GetAsync(new Uri(fullUrl));
            var content = await httpResponse.Content.ReadAsStringAsync();
            if (httpResponse.IsSuccessStatusCode)
            {


                Logger.Info($"Request to {fullUrl} succeeded. StatusCode:" +
                   $" {httpResponse.StatusCode}. Headers: {JsonConvert.SerializeObject(client.DefaultRequestHeaders)}. Response: {content}.");
                var response = JsonConvert.DeserializeObject<NHCBaseResponse<TResponse>>(content);
                return response!;
            }

            Logger.Error(
                $"Request to {fullUrl} failed. StatusCode: {httpResponse.StatusCode}. Headers: {JsonConvert.SerializeObject(client.DefaultRequestHeaders)}. Response: {content}.");
            throw new ClientCommunionException($"Request to {fullUrl} failed.");
        }
        catch (Exception ex)
        {
            Logger.Error($"Request to {fullUrl} failed.", ex);
            throw new ClientCommunionException($"Request to {fullUrl} failed.");
        }
    }
    private static string AddUrlParams(string url, Dictionary<string, string>? urlParam)
    {
        if (urlParam is null || urlParam.Count == 0) return url;
        if (urlParam != null && urlParam.Count > 0)
        {
            url += "?" + string.Join("&", urlParam.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));
        }

        return url;
    }
    private void AddHeaders(HttpClient client, Dictionary<string, string>? headers)
    {

        var xxx = SettingManager.GetSettingValueForApplication(NhcClientSetting.ClientSecret);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
        client.DefaultRequestHeaders.Add("X-IBM-Client-Id", SettingManager.GetSettingValue(NhcClientSetting.ClientId));
        client.DefaultRequestHeaders.Add("X-IBM-Client-Secret", SettingManager.GetSettingValueForApplication(NhcClientSetting.ClientSecret));
        client.DefaultRequestHeaders.Add("RefId", SettingManager.GetSettingValue(NhcClientSetting.RefId));
        if (headers is null || headers.Count == 0) return;
        foreach (var key in headers)
            client.DefaultRequestHeaders.Add(key.Key, key.Value);
    }
}