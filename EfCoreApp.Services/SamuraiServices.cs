﻿using EfCoreApp.Domain;
using EfCoreApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;

namespace EfCoreApp.Services
{
    public class SamuraiServices : ISamuraiServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _client;

        public SamuraiServices(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _client = _httpClientFactory.CreateClient("myclient");
        }

        public async Task Add(Samurai samurai)
        {
            const string uri = "Samurais";
            var options = new JsonSerializerOptions { WriteIndented = true, IgnoreNullValues = true };
            var jsonString = JsonSerializer.Serialize(samurai, options);
            var data = new StringContent(jsonString, Encoding.UTF8, "application/json");

            await _client.PostAsync(uri, data);            
        }

        public async Task<IEnumerable<Samurai>> GetAll()
        {
            const string uri = "Samurais";
            var response = await _client.GetAsync(uri);

            response.EnsureSuccessStatusCode();
            var samurai = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IEnumerable<Samurai>>(samurai, new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<Samurai> GetOne(int id)
        {
            var uri = $"Samurais/{id}";
            var response = await _client.GetAsync(uri);

            response.EnsureSuccessStatusCode();
            var samurai = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Samurai>(samurai, new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true
            });
        }
    }
}