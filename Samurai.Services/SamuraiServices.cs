using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Samurai.Services.Interfaces;

namespace Samurai.Services
{
    public class SamuraiServices : ISamuraiServices
    {
        private const string MyClient = "myclient";
        private readonly HttpClient _client;

        public SamuraiServices(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient(MyClient);
        }

        public async Task Add(Domain.Samurai samurai)
        {
            const string uri = "Samurais";
            var options = new JsonSerializerOptions { WriteIndented = true, IgnoreNullValues = true };
            var jsonString = JsonSerializer.Serialize(samurai, options);
            var data = new StringContent(jsonString, Encoding.UTF8, "application/json");

            await _client.PostAsync(uri, data);            
        }

        public async Task<IEnumerable<Domain.Samurai>> GetAll()
        {
            const string uri = "Samurais";
            var response = await _client.GetAsync(uri);

            response.EnsureSuccessStatusCode();
            var samurai = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IEnumerable<Domain.Samurai>>(samurai, new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<Domain.Samurai> GetOne(int id)
        {
            var uri = $"Samurais/{id}";
            var response = await _client.GetAsync(uri);

            response.EnsureSuccessStatusCode();
            var samurai = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Domain.Samurai>(samurai, new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true
            });
        }
    }
}