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
        const string IMAGE_URL = @"https://zr8z94hw-7004.euw.devtunnels.ms/";

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
            var stringContent = new StringContent(JsonSerializer.Serialize(user, _serializerOptions), Encoding.UTF8, "application/json");
            try
            {
                var response = await _httpClient.PostAsync($"{URL}SignUp", stringContent);
                return response.StatusCode;
            }
            catch (Exception)
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

        public async Task<bool> UploadPhoto(FileResult file)
        {

            try
            {
                byte[] bytes;

                using (MemoryStream ms = new MemoryStream())
                {
                    var stream = await file.OpenReadAsync();
                    stream.CopyTo(ms);
                    bytes = ms.ToArray();
                }

                var multipartFormDataContent = new MultipartFormDataContent();

                var content = new ByteArrayContent(bytes);
                multipartFormDataContent.Add(content, "file", "robot.jpg");


                var response = await _httpClient.PostAsync($@"{URL}UploadImage?Id=1", multipartFormDataContent);
                if (response.IsSuccessStatusCode) { return true; }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task<string> GetImage() { return $"{IMAGE_URL}images/"; }

        public async Task<bool> UploadFile(FileResult file)
        {

            try
            {
                byte[] bytes;

                #region המרה של הקובץ
                using (MemoryStream ms = new MemoryStream())
                {
                    var stream = await file.OpenReadAsync();
                    stream.CopyTo(ms);
                    bytes = ms.ToArray();
                }
                #endregion

                var multipartFormDataContent = new MultipartFormDataContent();

                var content = new ByteArrayContent(bytes);
                multipartFormDataContent.Add(content, "file", "robot.jpg");
               
                var userContent = JsonSerializer.Serialize(new User() { Id = 1, Email = "kuku@kuku.com", FirstName = "kuku", LastName = "kiki", UserPswd = "1234" }, _serializerOptions);
                
                multipartFormDataContent.Add(new StringContent(userContent, Encoding.UTF8, "application/json"), "user");

                var response = await _httpClient.PostAsync($@"{URL}UploadFile", multipartFormDataContent);
                if (response.IsSuccessStatusCode) { return true; }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
    }



}
 
