using Newtonsoft.Json;
using StonksCasino.classes.Api.Models;
using StonksCasino.classes.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StonksCasino.classes.Api
{
    public class ApiWrapper
    {
        public static async Task<string> Login(LoginCredentials loginCredentials)
        {
            string baseUri = @"https://stonkscasino.nl/api/login.php";
            Uri request = new Uri(baseUri);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "StonksCasino");


            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(loginCredentials), Encoding.UTF8, "application/json");
            HttpResponseMessage respons = await client.PostAsync(request, httpContent);
            if (respons.IsSuccessStatusCode == false)
            {
                return null;
            }

            string content = await respons.Content.ReadAsStringAsync();
            LoginResult loginResult = JsonConvert.DeserializeObject<LoginResult>(content);

            if(loginResult.Result == "succes")
            {
                GlobalApiToken.AccessToken = loginResult.AccessToken;
                GlobalApiToken.UserId = loginResult.UserId;
            }

            return loginResult.Result;
        }

        public static async Task<bool> Logout()
        {
            string baseUri = @"https://stonkscasino.nl/api/Logout.php";
            Uri request = new Uri(baseUri);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "StonksCasino");

            ApiAccessToken apiToken = new ApiAccessToken();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(apiToken), Encoding.UTF8, "application/json");
            HttpResponseMessage respons = await client.PostAsync(request, httpContent);
            return true;
        }

        public static async Task<bool> GetUserInfo()
        {
            string baseUri = @"https://stonkscasino.nl/api/GetUserInfo.php";
            Uri request = new Uri(baseUri);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "StonksCasino");

            ApiAccessToken token = new ApiAccessToken();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(token), Encoding.UTF8, "application/json");
            HttpResponseMessage respons = await client.PostAsync(request, httpContent);
            if (respons.IsSuccessStatusCode)
            {
                string content = await respons.Content.ReadAsStringAsync();
                UserInfoResult infoResult = JsonConvert.DeserializeObject<UserInfoResult>(content);
                if (infoResult.Result)
                {
                    User.Username = infoResult.UserName;
                    User.Tokens = infoResult.Tokens;
                    return true;
                }
                else
                {
                    await Logout();
                    return false;
                }
            }
            else
            {
                User.Username = "";
                User.Tokens = 0;
                return false;
            }
        }

        public static async Task<bool> UpdateTokens(int tokens)
        {
            string baseUri = @"https://stonkscasino.nl/api/UpdateUserTokens.php";
            Uri request = new Uri(baseUri);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "StonksCasino");

            TokenUpdate tokenUpdate = new TokenUpdate(tokens);
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(tokenUpdate), Encoding.UTF8, "application/json");
            HttpResponseMessage respons = await client.PostAsync(request, httpContent);

            if (respons.IsSuccessStatusCode)
            {
                string content = await respons.Content.ReadAsStringAsync();
                ApiResult result = JsonConvert.DeserializeObject<ApiResult>(content);
                return result.Result;
            }
            else
            {
                await Logout();
                return false;
            }
        }

        public static async Task<bool> CheckLogin()
        {
            string baseUri = @"https://stonkscasino.nl/api/CheckAccesskey.php";
            Uri request = new Uri(baseUri);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "StonksCasino");

            ApiAccessToken token = new ApiAccessToken();
            HttpContent httpContent = new StringContent(JsonConvert.SerializeObject(token), Encoding.UTF8, "application/json");
            HttpResponseMessage respons = await client.PostAsync(request, httpContent);

            if (respons.IsSuccessStatusCode)
            {
                string content = await respons.Content.ReadAsStringAsync();
                ApiResult result = JsonConvert.DeserializeObject<ApiResult>(content);
                return result.Result;
            }
            else
            {
                return false;
            }
        }
    }
}
