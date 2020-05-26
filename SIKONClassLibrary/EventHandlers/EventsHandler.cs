using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SIKONClassLibrary.EventHandlers
{
    public class EventsHandler : IHandle<Event>
    {
        //private const string ServerUrl = "http://localhost:53683/";
        private const string ServerUrl = "http://sikon.azurewebsites.net/";
        private const string RequestUri = "api/Events";

        public void Create(Event Obj)
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
        
        public List<Event> Read()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.UseDefaultCredentials = true;

            List<Event> ListObjects = new List<Event>();
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
                        var List = response.Content.ReadAsAsync<IEnumerable<Event>>().Result;
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
        
        public Event ReadFrom(int ID)
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
                    Event ev = null;

                    var response = client.GetAsync($"{RequestUri}/{ID}").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        ev = response.Content.ReadAsAsync<Event>().Result;
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
        
        public Event ReadFrom(string ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Event Obj, int ID)
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
                   return client.PutAsJsonAsync<Event>($"{RequestUri}/{ID}",  Obj).Result.IsSuccessStatusCode;
                }
                catch (Exception )
                {
                    MessageDialogHelper.Show($"Ingen Forbindelse til serveren", "Server Fejl");
                    return false;
                }
            }
        }

        public bool Update(Event Obj, string ID)
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
