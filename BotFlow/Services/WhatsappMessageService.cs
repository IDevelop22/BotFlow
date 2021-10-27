using BotFlow.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BotFlow.Services
{
    public class WhatsappMessageService
    {
        private readonly IConfiguration _config;
        public WhatsappMessageService(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendLocation(string chatId, string location,string addressName = "Location")
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"{_config["ChatApi:BaseAddress"]}");
            var smt = new
            {
                chatId = "27843745215@c.us",
                lat = location.Split(';')[0],
                lng = location.Split(';')[1],
                address = addressName
            };
            var content = new StringContent(JsonSerializer.Serialize(smt), Encoding.UTF8, "application/json");
            var result = await client.PostAsync($"/{_config["ChatAPI:InstanceID"]}/sendLocation?token={_config["ChatApi:Token"]}", content);
        }

        public async Task SendMessage(string chatId, string message)
            {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri($"{_config["ChatApi:BaseAddress"]}");
            SendMessageTemplate smt = new SendMessageTemplate
            {
                phone = "27843745215",
                body = message
            };
            var content = new StringContent(JsonSerializer.Serialize(smt), Encoding.UTF8, "application/json");
            var result = await client.PostAsync($"/{_config["ChatApi:InstanceID"]}/sendMessage?token={_config["ChatApi:Token"]}", content);
        }

    }
}
