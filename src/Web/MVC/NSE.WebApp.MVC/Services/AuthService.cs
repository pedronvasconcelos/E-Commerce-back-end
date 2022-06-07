using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NSE.Core.Comunication;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services
{
    public class AuthService : Service, IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient, 
                                   IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.AuthUrl);

            _httpClient = httpClient;
        }

        public async Task<UserResponseLogin> Login(UserLogin userLogin)
        {
            var loginContent = GetContent(userLogin);

            var response = await _httpClient.PostAsync("/api/identity/login-auth", loginContent);

            if (!HandleErrorsResponse(response))
            {
                return new UserResponseLogin
                {
                    ResponseResult = await DeserializeResponse<ResponseResult>(response)
                };
            }

            return await DeserializeResponse<UserResponseLogin>(response);
        }

        public async Task<UserResponseLogin> Register(UserRegister userRegister)
        {
            var registerContent = GetContent(userRegister);

            var response = await _httpClient.PostAsync("/api/identity/new-account", registerContent);

            if (!HandleErrorsResponse(response))
            {
                return new UserResponseLogin
                {
                    ResponseResult = await DeserializeResponse<ResponseResult>(response)
                };
            }

            return await DeserializeResponse<UserResponseLogin>(response);
        }
    }
}