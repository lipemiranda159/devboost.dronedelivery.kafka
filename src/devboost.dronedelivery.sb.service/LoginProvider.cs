using devboost.dronedelivery.felipe.Security.Entities;
using devboost.dronedelivery.sb.domain.DTO;
using devboost.dronedelivery.sb.domain.Interfaces;
using devboost.dronedelivery.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace devboost.dronedelivery.sb.service
{
    public class LoginProvider : HttpServiceBase, ILoginProvider
    {
        public LoginProvider(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<string> GetTokenAsync()
        {
            var user = new LoginDTO()
            {
                UserId = "admin_drone",
                Password = "AdminAPIDrone01!"
            };

            var request = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            client.BaseAddress = new Uri(_urlPedidos);
            var tokenJson = await client.PostAsync("api/login", request);
            var token = JSONHelper.DeserializeJsonToObject<Token>(await tokenJson.Content.ReadAsStringAsync()) as Token;
            return token.AccessToken;
        }

        public bool Equals(LoginProvider other)
        {
            throw new NotImplementedException();
        }
    }
}
