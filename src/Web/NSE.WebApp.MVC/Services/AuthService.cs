using NSE.WebApp.MVC.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NSE.WebApp.MVC.Services
{
    public class AuthService : Service, IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<UserLoginResponse> Login(UserLogin userLogin)
        {
            var loginContent =  SerializeContent(userLogin);
            var response = await _httpClient.PostAsync("https://localhost:44396/api/identity/login-auth", loginContent);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            return await DeserializeResponseObject<UserLoginResponse>(response);
        }

        public async Task<UserLoginResponse> Register(UserRegister userRegistrer)
        {
            var registerContent = SerializeContent(userRegistrer);
            var response = await _httpClient.PostAsync("https://localhost:44396/api/identity/new-account", registerContent);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            return await DeserializeResponseObject<UserLoginResponse>(response);
        }
    }
}
