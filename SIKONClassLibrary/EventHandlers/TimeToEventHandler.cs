using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SIKONClassLibrary.EventHandlers
{
    public class TimeToEventHandler : IHandle<TimeToEvent>
    {
        //private const string ServerUrl = "http://localhost:53683/";
        private const string ServerUrl = "http://sikon.azurewebsites.net/";
        private const string RequestUri = "api/TimeToEvents";

        public void Create(TimeToEvent Obj)
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

        public List<TimeToEvent> Read()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.UseDefaultCredentials = true;

            List<TimeToEvent> AccountToEventList = new List<TimeToEvent>();
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
                        var List = response.Content.ReadAsAsync<IEnumerable<TimeToEvent>>().Result;
                        foreach (var ate in List)
                        {
                            AccountToEventList.Add(ate);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageDialogHelper.Show($"Ingen Forbindelse til serveren", "Server Fejl");
                    return null;
                }
            }

            return AccountToEventList;
        }

        public TimeToEvent ReadFrom(int ID)
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
                    TimeToEvent ev = null;

                    var response = client.GetAsync($"{RequestUri}/{ID}").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        ev = response.Content.ReadAsAsync<TimeToEvent>().Result;
                    }

                    return ev;
                }
                catch (Exception e)
                {
                    MessageDialogHelper.Show($"Ingen Forbindelse til serveren", "Server Fejl");
                    return null;
                }
            }
        }

        public TimeToEvent ReadFrom(string ID)
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
                    TimeToEvent ev = null;

                    var response = client.GetAsync($"{RequestUri}/{ID}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        ev = response.Content.ReadAsAsync<TimeToEvent>().Result;

                    }

                    return ev;

                }
                catch (Exception e)
                {
                    MessageDialogHelper.Show($"Ingen Forbindelse til serveren", "Server Fejl");
                    return null;
                }
            }
        }

        public bool Update(TimeToEvent Obj, int ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(TimeToEvent Obj, string ID)
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
