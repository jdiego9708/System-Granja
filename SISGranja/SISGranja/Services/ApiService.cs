namespace SISGranja.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Common.Models;
    using Newtonsoft.Json;
    using System.Text;
    using SISGranja.Common;

    public class ApiService
    {
        public async Task<Response> GetList<T>(string urlBase, string prefix, string controller)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                string url = $"{prefix}{controller}";
                //HttpResponseMessage response = await client.GetAsync(url);
                string jsonString = "";
                var response = client.GetAsync(url).Result;  // Blocking call!

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    jsonString = response.Content.ReadAsStringAsync().Result;
                    List<T> list = JsonConvert.DeserializeObject<List<T>>(jsonString);
                    return new Response
                    {
                        IsSuccess = true,
                        Result = list,
                    };
                }
                else
                {
                    jsonString = $"{(int)response.StatusCode} ({response.ReasonPhrase})";
                    return new Response
                    {
                        IsSuccess = false,
                        Message = jsonString,
                    };
                }

                //string jsonString = await response.Content.ReadAsStringAsync();
                //if (!response.IsSuccessStatusCode)
                //{
                //    return new Response
                //    {
                //        IsSuccess = false,
                //        Message = jsonString,
                //    };
                //}               
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Post<T>(string urlBase, string prefix, string controller, T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                string url = $"{prefix}{controller}";
                HttpResponseMessage response = await client.PostAsync(url, content);
                string jsonString = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = jsonString,
                    };
                }

                var obj = JsonConvert.DeserializeObject<T>(jsonString);
                return new Response
                {
                    IsSuccess = true,
                    Result = obj,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Delete(string urlBase, string prefix, string controller, int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                string url = $"{prefix}{controller}/{id}";
                HttpResponseMessage response = await client.DeleteAsync(url);
                string jsonString = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = jsonString,
                    };
                }
                return new Response
                {
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> Put<T>(string urlBase, string prefix, string controller, T model, int id)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                string url = $"{prefix}{controller}/{id}";
                HttpResponseMessage response = await client.PutAsync(url, content);
                string jsonString = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = jsonString,
                    };
                }

                var obj = JsonConvert.DeserializeObject<T>(jsonString);
                return new Response
                {
                    IsSuccess = true,
                    Result = obj,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    }
}