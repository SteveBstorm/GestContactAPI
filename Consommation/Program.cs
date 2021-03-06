using Consommation.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;

namespace Consommation
{
    class Program
    {
        public static List<Contact> GetContact(string token = null)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:31101/api/");

            if(!(token is null))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (HttpResponseMessage message = client.GetAsync("contact/UserContact").Result)
            {
                if (!message.IsSuccessStatusCode)
                    Console.WriteLine(message.StatusCode);

                string json = message.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Réponse en json");
                Console.WriteLine(json);
                Console.WriteLine();

                return JsonConvert.DeserializeObject<List<Contact>>(json);
            }
        }

        public static User Login(string email, string password)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:31101/api/");

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "monToken");

            string jsonBody = JsonConvert.SerializeObject(new { email = email, password = password });
            HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using(HttpResponseMessage message = client.PostAsync("user/login", content).Result)
            {
                if (!message.IsSuccessStatusCode)
                {
                    Console.WriteLine(message.StatusCode);
                    Console.WriteLine(message.ToString());
                    return null;
                }

                string json = message.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<User>(json);
            }
        }

        static void Main(string[] args)
        {
            

            User connectedUser = Login("steve.lorent@bstorm.be", "Test1234=");

            List<Contact> listeContact = GetContact(connectedUser.Token);


            foreach (Contact item in listeContact)
            {
                Console.WriteLine(item.FirstName + " - " + item.LastName);
                Console.WriteLine(item.Email + " - " + item.Phone);
            }

            //Console.WriteLine(connectedUser.Email);
            //Console.WriteLine(connectedUser.FirstName + " " + connectedUser.LastName);

            
        }
    }
}
