using Frontend_MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Frontend_MVC.Services
{
    public class UserApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:22640/api/";  // Replace with your actual API URL

        public UserApiService()
        {
            _httpClient = new HttpClient();
        }

        // Register User
        public async Task<string> RegisterUser( UserModel user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/users/register", content);
            string responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Response StatusCode: {response.StatusCode}");
            Console.WriteLine($"Response Body: {responseBody}");
            if (response.IsSuccessStatusCode)
            {
                return "User registered successfully";
            }
            else
            {
                return "Registration failed";
            }
        }

        // Login User
        public async Task<string> LoginUser(LoginModel login)
        {
            var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/users/login", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);

                if (responseObject != null && responseObject.ContainsKey("token"))
                {
                    return $"Bearer {responseObject["token"]}";
                }
                return "Login response invalid";
            }
            else
            {
                return "Invalid credentials";
            }
        }

    }
}