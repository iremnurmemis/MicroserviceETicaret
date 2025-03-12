using IdentityModel.Client;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.WebUI.Services.Interfaces;
using MultiShop.WebUI.Settings;

namespace MultiShop.WebUI.Services.Concrete
{
    public class ClientCredentialTokenService : IClientCredentialTokenService
    {
        private readonly ServiceApiSettings _serviceApiSettings;
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;
        private readonly ClientSettings _clientSettings;

        public ClientCredentialTokenService(IOptions<ServiceApiSettings> serviceApiSettings, HttpClient httpClient, IMemoryCache memoryCache, IOptions<ClientSettings> clientSettings)

        {
            _serviceApiSettings = serviceApiSettings.Value;
            _httpClient = httpClient;
            _memoryCache = memoryCache;
            _clientSettings = clientSettings.Value;

        }

        //değişiklik yapıldı dinamik hale gelicek
        public async Task<string> GetToken()
        {
            if (_memoryCache.TryGetValue("multishoptoken", out string cachedToken))
            {
                return cachedToken;
            }


            var discoveryEndpoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityServerUrl,
            });

          

            var passwordTokenRequest = new PasswordTokenRequest
            {
                ClientId = _clientSettings.MultiShopManagerClient.ClientId,
                ClientSecret = _clientSettings.MultiShopManagerClient.ClientSecret,
                UserName = "ali01",
                Password = "123456aA*",
                Address = discoveryEndpoint.TokenEndpoint
            };

            var tokenResponse = await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest);

            if (tokenResponse.IsError)
            {
                throw new Exception("Token alınamadı: " + tokenResponse.ErrorDescription);
            }

            _memoryCache.Set("multishoptoken", tokenResponse.AccessToken, TimeSpan.FromSeconds(tokenResponse.ExpiresIn));
            return tokenResponse.AccessToken;


        }
    }
}
