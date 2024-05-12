using ProdigyProjectFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        readonly UserService userService;
        const string URL = @"https://nghpvwqt-7112.uks1.devtunnels.ms/api/Values/";
        public const string IMAGE_URL = @"https://nghpvwqt-7112.uks1.devtunnels.ms/images/";

        private const string RootURL = $"{WwwRoot}/ProdigyWeb";
        public const string WwwRoot = "https://nghpvwqt-7112.uks1.devtunnels.ms/swagger/index.html";

        public ProdigyServices(UserService _userService)
        {
            _httpClient = new HttpClient();
            userService = _userService;
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


        public async Task<User> GetCurrentUser()
        {
            try
            {
                string st = await SecureStorage.Default.GetAsync("CurrentUser");
                if (!string.IsNullOrEmpty(st))
                    return JsonSerializer.Deserialize<User>(st, _serializerOptions);
            }
            catch (Exception) { }
            return null;
        }

        public async Task<bool> StarBook(string isbn) 
        {
            var response = await _httpClient.GetAsync($"{URL}StarBook?isbn={isbn}");
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> TBRBook(string isbn)
        {
            var response = await _httpClient.GetAsync($"{URL}TBRBook?isbn={isbn}");
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> CurrentReadBook(string isbn)
        {
            var response = await _httpClient.GetAsync($"{URL}CurrentReadBook?isbn={isbn}");
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<bool> DropBook(string isbn)
        {
            var response = await _httpClient.GetAsync($"{URL}DroppedBook?isbn={isbn}");
            return response.StatusCode == HttpStatusCode.OK;
        }

        public async Task<List<Book>> SearchAsync(string AuthorName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{URL}AuthorBooks?name={AuthorName}");
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        {
                            string content = await response.Content.ReadAsStringAsync();
                            return JsonSerializer.Deserialize<List<Book>>(content, _serializerOptions);
                        }

                        case HttpStatusCode.NotFound:
                        {
                            return null;
                        }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return null;
        }

        public async Task<StatusEnum> UploadPFP(FileResult file = null)
        {
            try
            {
                var multipartFormContent = new MultipartFormDataContent();

                if (file != null)
                {
                    byte[] bytes;
                    using (MemoryStream ms = new())
                    {
                        var stream = await file.OpenReadAsync();
                        stream.CopyTo(ms);
                        bytes = ms.ToArray();
                    }

                    var content = new ByteArrayContent(bytes);
                    multipartFormContent.Add(content, "file", file.FileName);
                }

                var response = await _httpClient.PostAsync($"{URL}UploadImage", multipartFormContent);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        var content = await response.Content.ReadAsStringAsync();
                        User user = JsonSerializer.Deserialize<User>(content, _serializerOptions);
                        if(user != null)
                            userService.User = user;
                        return StatusEnum.OK;
                    case HttpStatusCode.Unauthorized:
                        return StatusEnum.Unauthorized;
                    default:
                        return StatusEnum.Error;
                }
            }
            catch (Exception)
            {
                return StatusEnum.Error;
            }
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
                    case HttpStatusCode.OK:
                        {
                            jsonContent = await response.Content.ReadAsStringAsync();
                            User u = JsonSerializer.Deserialize<User>(jsonContent, _serializerOptions);
                            userService.User = u;
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

        public async Task<User> Register(User user)
        {
            var stringContent = new StringContent(JsonSerializer.Serialize(user, _serializerOptions), Encoding.UTF8, "application/json");
            try
            {
                var response = await _httpClient.PostAsync($"{URL}SignUp", stringContent);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    user = JsonSerializer.Deserialize<User>(content, _serializerOptions);
                    userService.User = user;
                    return user;
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    return null;
                }
                else throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        #endregion

        #region change X

        #region username
        public async Task<bool> ChangeUsername(string newUsername)
        {
            User user = JsonSerializer.Deserialize<User>(await SecureStorage.Default.GetAsync("CurrentUser"), _serializerOptions);
            if (user == null)
                return false;
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
                            return true;

                        }
                    case (HttpStatusCode.Unauthorized):
                        {
                            return false;

                        }
                }
            }
            catch (Exception) { throw new Exception(); }
            return false;
        }
        #endregion

        #region password
        public async Task<bool> ChangePassword(string newPassword)
        {
            User user = JsonSerializer.Deserialize<User>(await SecureStorage.Default.GetAsync("CurrentUser"), _serializerOptions);
            if (user == null)
                return false;
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
                            return true;

                        }
                    case (HttpStatusCode.Unauthorized):
                        {
                            return false;
                        }
                }
            }
            catch (Exception) { throw new Exception(); }
            return false;
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

    }



}
 
