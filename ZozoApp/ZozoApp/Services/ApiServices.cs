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
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/Accounts/Register",content);
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
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/Accounts/Login", content);
            if (!response.IsSuccessStatusCode) return false;
            var jsonResult = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Token>(jsonResult);
            Preferences.Set("accessToken", result.access_token);
            Preferences.Set("accessToken", result.user_id);
            Preferences.Set("accessToken", result.user_name);
            return true;
        }

        public static async Task<List<Category>> GetCategories( )
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "Api/Categories");
            return JsonConvert.DeserializeObject<List<Category>>(response);
        }

        public static async Task<List<Product>> GetProductById(int productId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "Api/Products/" + productId);
            return JsonConvert.DeserializeObject<List<Product>>(response);
        }

        public static async Task<List<ProductByCategory>> GetProductByCategory(int categoryId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "Api/ProductsByCategory/" + categoryId);
            return JsonConvert.DeserializeObject<List<ProductByCategory>>(response);
        }
        public static async Task<List<PopularProduct>> GetPopularProduct(int categoryId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "Api/PopularProducts/" + categoryId);
            return JsonConvert.DeserializeObject<List<PopularProduct>>(response);
        }

        public static async Task<bool> AddItemsInCart(AddToCart addToCart)
        {
            
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(addToCart);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/ShoppingCartItems", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public static async Task<CartSubTotal> GetCartSubTotal(int userid)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/ShoppingCartItems/SubTotal/" + userid);
            return JsonConvert.DeserializeObject<CartSubTotal>(response);
        }

        public static async Task<List<ShoppingCartItem>> GetShoppingCartItems(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/ShoppingCartItems/" + userId);
            return JsonConvert.DeserializeObject<List<ShoppingCartItem>>(response);
        }

        public static async Task<List<TotalCartItems>> GetTotalCartItems(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/ShoppingCartItems/TotalItems/" + userId);
            return JsonConvert.DeserializeObject<List<TotalCartItems>>(response);
        }


        public static async Task<bool> ClearShoppingCart(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.DeleteAsync (AppSettings.ApiUrl + "api/ShoppingCartItems/" + userId);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public static async Task<OrderResponse> PlaceOrder(Order order)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/Orders", content);
            var jsonResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OrderResponse>(jsonResult);

        }

        public static async Task<List<OrderByUser>> GetOrderByUser(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Orders/OrdersByUser/" + userId);
            return JsonConvert.DeserializeObject<List<OrderByUser>>(response);
        }

        public static async Task<List<Order>> GetOrderDetails(int userId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Orders/OrderDetails/" + userId);
            return JsonConvert.DeserializeObject<List<Order>>(response);
        }





    }
}
