﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SIKONClassLibrary.EventHandlers
{
   public class QuestionHandler :IHandle<Question>
    {
        //private const string ServerUrl = "http://localhost:53683/";
        private const string ServerUrl = "http://sikon.azurewebsites.net/";
        private const string RequestUri = "api/Questions";

        public void Create(Question Obj)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.UseDefaultCredentials = true;
            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = client.PostAsJsonAsync($"{RequestUri}", Obj).Result;
                }
                catch (Exception e)
                {
                    MessageDialogHelper.Show($"Ingen Forbindelse til serveren", "Server Fejl");
                }
            }
        }

        public List<Question> Read()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.UseDefaultCredentials = true;

            List<Question> ListObjects = new List<Question>();
            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = client.GetAsync($"{RequestUri}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var List = response.Content.ReadAsAsync<IEnumerable<Question>>().Result;
                        foreach (var ob in List)
                        {
                            ListObjects.Add(ob);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageDialogHelper.Show($"Ingen Forbindelse til serveren", "Server Fejl");
                    return null;
                }
            }
            return ListObjects;
        }

        public Question ReadFrom(int ID)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.UseDefaultCredentials = true;


            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    Question qu = null;

                    var response = client.GetAsync($"{RequestUri}/{ID}").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        qu = response.Content.ReadAsAsync<Question>().Result;
                    }

                    return qu;
                }
                catch (Exception e)
                {
                    MessageDialogHelper.Show($"Ingen Forbindelse til serveren", "Server Fejl");
                    return null;
                }
            }
        }

        public Question ReadFrom(string ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Question Obj, int ID)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.UseDefaultCredentials = true;
            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    return client.PutAsJsonAsync<Question>($"{RequestUri}/{ID}", Obj).Result.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    MessageDialogHelper.Show($"Ingen Forbindelse til serveren", "Server Fejl");
                    return false;
                }
            }
        }

        public bool Update(Question Obj, string ID)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.UseDefaultCredentials = true;
            using (var client = new HttpClient(clientHandler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = client.DeleteAsync($"{RequestUri}/{ID}").Result;
                }
                catch (Exception e)
                {
                    MessageDialogHelper.Show($"Ingen Forbindelse til serveren", "Server Fejl");
                }
            }
        }

        public void Delete(string ID)
        {
            throw new NotImplementedException();
        }
    }
}
