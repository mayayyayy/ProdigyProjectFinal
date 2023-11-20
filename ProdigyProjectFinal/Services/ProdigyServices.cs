﻿using ProdigyProjectFinal.Models;
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

        #region GetHello
        public async Task<string> GetHello()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{URL}Hello");
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                return "Something is Wrong";
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return "ooops";
        }
        #endregion


        





        #region LogInAsync

        public async Task<UserDto> LogInAsync(string userName, string password)  //  this DOes NOT WORK DUMB BITCH :(
        {
            try
            {
                //object for sending
                User user = new User() { Email = userName, UserPswd = password };
                //serialisation
                var jsonContent = JsonSerializer.Serialize(user, _serializerOptions);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                Console.WriteLine(content);
                var response = await _httpClient.PostAsync($"{URL}Login", content);

                switch (response.StatusCode)
                {
                    case (HttpStatusCode.OK):
                        {
                            jsonContent = await response.Content.ReadAsStringAsync();
                            User u = JsonSerializer.Deserialize<User>(jsonContent, _serializerOptions);
                            await Task.Delay(2000);
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
        }

        //private async Task<string> GetUserEmail(string x)
        //{
        //    try
        //    {
        //        var response = await _httpClient.GetAsync($@"{URL}GetUserEmail?nick={x}");
        //        switch (response.StatusCode)
        //        {
        //            case HttpStatusCode.OK:
        //                return await response.Content.ReadAsStringAsync();
        //            case HttpStatusCode.NotFound:
        //                return "user does not exist";
        //            default:
        //                return $"error: {response.StatusCode.ToString()}";
        //        }


        //    }
        //    catch (Exception ex) { Console.WriteLine(ex.Message); };
        //    return "error";
        //}
    } 
