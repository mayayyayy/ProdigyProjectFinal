using ProdigyProjectFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProdigyProjectFinal.Services
{
    public class ProdigyServices
    {
        readonly HttpClient _httpClient;
        readonly JsonSerializerOptions _serializerOptions;
        const string URL = @"https://2c7rkmj3-7112.euw.devtunnels.ms/api/Values/";


        public ProdigyServices()
        {
            _httpClient = new HttpClient();
            _serializerOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            };

        }
        private async Task<string> GetUserEmail(string x)
        {
            try
            {
                var response = await _httpClient.GetAsync($@"{URL}GetUserEmail?nick={x}");
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return await response.Content.ReadAsStringAsync();
                    case HttpStatusCode.NotFound:
                        return "user does not exist";
                    default:
                        return $"error: {response.StatusCode.ToString()}";
                }


            }
            catch (Exception ex) { Console.WriteLine(ex.Message); };
            return "error";
        }


        #region LogInAsync

        public async Task<UserDto> LogInAsync(string userName, string password)  
        {
            try
            {
                //object for sending
                User user = new User() { Username = userName, UserPswd = password };
                var jsonContent = JsonSerializer.Serialize(user, _serializerOptions);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{URL}Login", content);

                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            jsonContent = await response.Content.ReadAsStringAsync();
                            User u = JsonSerializer.Deserialize<User>(jsonContent, _serializerOptions);
                            return new UserDto() { Success = true, Message = string.Empty, User = u };

                        }
                    case (HttpStatusCode.Unauthorized):
                        {
                            return new UserDto() { Success = false, User = null, Message = ErrorMsgs.invalidLogin };

                        }

                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return new UserDto() { Success = false, User = null, Message = ErrorMsgs.invalidLogin };

        }
        #endregion


        #region RegisterAsync

        public async Task<HttpStatusCode> Register(User user)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(user,_serializerOptions), Encoding.UTF8, "application/json");
            try
            {
                var response = await _httpClient.PostAsync($"{URL}SignUp", stringContent);
                return response.StatusCode;
            }
            catch(Exception)
            {
                throw new Exception();
            }
        }

        #endregion

        #region change X

        #region username
        public async Task<UserDto> ChangeUsername(string newUsername)
        {
            User user = JsonSerializer.Deserialize<User>(await SecureStorage.Default.GetAsync("CurrentUser"), _serializerOptions);
            if (user == null)
                return null;
            try
            {
                //object for sending
                var jsonContent = JsonSerializer.Serialize(user, _serializerOptions);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{URL}ChangeUsername?newUsername={newUsername}", content);

                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            jsonContent = await response.Content.ReadAsStringAsync();
                            User u = JsonSerializer.Deserialize<User>(jsonContent, _serializerOptions);
                            return new UserDto() { Success = true, Message = string.Empty, User = u };

                        }
                    case (HttpStatusCode.Unauthorized):
                        {
                            return new UserDto() { Success = false, User = null, Message = ErrorMsgs.invalidUsernameChange };

                        }
                }    
            }
            catch (Exception) { throw new Exception(); }
            return null;
        }
        #endregion

        #region password
        public async Task<UserDto> ChangePassword(string newPassword)
        {
            User user = JsonSerializer.Deserialize<User>(await SecureStorage.Default.GetAsync("CurrentUser"), _serializerOptions);
            if (user == null)
                return null;
            try
            {
                //object for sending
                var jsonContent = JsonSerializer.Serialize(user, _serializerOptions);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{URL}ChangePassword?newPass={newPassword}", content);

                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            jsonContent = await response.Content.ReadAsStringAsync();
                            User u = JsonSerializer.Deserialize<User>(jsonContent, _serializerOptions);
                            return new UserDto() { Success = true, Message = string.Empty, User = u };

                        }
                    case (HttpStatusCode.Unauthorized):
                        {
                            return new UserDto() { Success = false, User = null, Message = ErrorMsgs.invalidPassChange };

                        }
                }
            }
            catch (Exception) { throw new Exception(); }
            return null;
        }
        #endregion


        #endregion


        #region Authentication 
        private const string AuthStateKey = "AuthState";
        public async Task<bool> IsAuthenticatedAsync()
        {
            await Task.Delay(2000);

            var authState = Preferences.Default.Get<bool>(AuthStateKey, false);
            return authState;
        }

        public void Login()
        {
            Preferences.Default.Set<bool>("AuthState", true);
        }
        public void Logout()
        {
            Preferences.Default.Remove("AuthState");
        }
        #endregion
    }


} 
