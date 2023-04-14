using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HC.Core
{
    public class BaseApi
    {
        public BaseApi()
        {
        }

        public async Task<string> Get(string requestUri)
        {
            var users = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Globle.DomainName);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    //Post Method
                    var response = await client.GetAsync(requestUri);
                    if (response.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api
                        users = response.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        Console.WriteLine("Internal server Error");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return await Task.FromResult(users);
            }
        }

        public async Task<string> Post(string requestUri, object parameter)
        {
            var users = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Globle.DomainName);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(parameter, Formatting.Indented);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    //Post Method
                    var response = await client.PostAsync(requestUri, stringContent);
                    if (response.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api
                        users = response.Content.ReadAsStringAsync().Result;
                    }
                    else
                    {
                        Console.WriteLine("Internal server Error");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return await Task.FromResult(users);
            }
        }
    }
}
