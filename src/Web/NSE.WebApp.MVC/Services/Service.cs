using NSE.WebApp.MVC.Extensions;
using System.Text;
using System.Text.Json;

namespace NSE.WebApp.MVC.Services
{
    public abstract class Service
    {
        protected StringContent SerializeContent(object obj)
        {
            return new StringContent(
                JsonSerializer.Serialize(obj),
                Encoding.UTF8, "application/json");
        }

        protected async Task<T> DeserializeResponseObject<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions

            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }


        protected bool HandleResponseErrors(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 401:
                case 403:
                case 404:
                case 500:
                    throw new CustomHttpRequestException(response.StatusCode);

                case 400:
                    return false;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }


    }
}
