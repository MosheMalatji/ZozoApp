using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using ZozoApp.Models;

namespace ZozoApp.Services
{
    public class ApiServices
    {
        public static async Task<bool> RegisterUser(string name, string email, string password)
        {
            var register = new Register()
            {
                Name = name,
                Email = email,
                Password = password
            };
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(register);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("http://localhost:49887/api/Accounts/Register",content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public static async Task<bool> Login(string email,string password) 
        {
            var login = new Login()
            {
                Email = email,
                Password = password
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(ApiSettings.ApiUrl "api/Accounts/Login", content);
            if (!response.IsSuccessStatusCode) return false;
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Token>(jsonResult);
            Preferences.Set("accessToken", result.access_token);
            Preferences.Set("accessToken", result.user_id);
            Preferences.Set("accessToken", result.user_name);
            return true;
        }

        public static async Task<List<bool>> GetCategories()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
                var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "Api/Categories")
                return JsonConvert.DeserializeObject<List<Category>>(response);
        }
    }
}
