using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using FortniteClient_Win64_Shipping.Classes.HTTPEngine;

namespace FortniteClient_Win64_Shipping.Classes.HTTPEngine
{
    class Requests
    {
        private readonly HttpClient _client = new HttpClient();

        // Fortnite Build information

        public async Task<T> GetJSON<T>(string url)
        {
            try
            {
                var baseUrl = $"{Engine.OnlineSubsystemServiceProd}/{url}";
                var json = await _client.GetStringAsync(baseUrl);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<T>(json, options);
            }
            catch (Exception ex)
            {
                Logger.Log($"GetJSON failed when calling {url} | {ex.Message}");
                throw;
            }
        }

        public async Task<T> PostJSON<T>(string url, object? requestBody = null, Dictionary<string, string>? headers = null)
        {
            try
            {
                var baseUrl = $"{Engine.OnlineSubsystemServiceProd}/{url}";
                using var requestMessage = new HttpRequestMessage();

                if (requestBody == null)
                {
                    requestMessage.Method = HttpMethod.Get;
                    requestMessage.RequestUri = new Uri(baseUrl);
                }
                else
                {
                    requestMessage.Method = HttpMethod.Post;
                    requestMessage.RequestUri = new Uri(baseUrl);

                    var jsonBody = JsonSerializer.Serialize(requestBody);
                    requestMessage.Content = new StringContent(jsonBody, Encoding.UTF8, "application/x-www-form-urlencoded");
                }

                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);
                    }
                }

                using var response = await _client.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<T>(responseJson, options);
            }
            catch(Exception ex)
            {
                Logger.Log($"PostJSON failed when calling {url} | {ex}");
                throw;
            }
        }

        public async Task<T> Authenticate<T>(string url, Dictionary<string, string>? formData = null, Dictionary<string, string>? headers = null)
        {
            var baseUrl = $"{Engine.OnlineSubsystemServiceProd}/{url}";

            try
            {
                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, baseUrl);

                if (formData != null)
                {
                    requestMessage.Content = new FormUrlEncodedContent(formData);
                }

                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);
                    }
                }

                using var response = await _client.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<T>(responseJson, options);
            }
            catch (Exception ex)
            {
                Logger.Log($"HTTP Error | {ex}");
                throw;
            }
        }
    }
}
