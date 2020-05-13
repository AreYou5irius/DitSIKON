using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Audio;
using SIKONClassLibrary;

namespace SIKONClient.EventHandlers
{
    public class EventsHandler : IHandle<Event>
    {
        private const string ServerUrl = "http://localhost:53683/";

        public void Create(Event Obj)
        {
            throw new NotImplementedException();
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
                    var response = client.GetAsync("api/Events").Result;
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
                    Console.WriteLine(e);
                    throw;
                }
            }
            return ListObjects;

            //throw new NotImplementedException();
        }

        public Event ReadFrom(int ID)
        {
            throw new NotImplementedException();
        }

        public Event ReadFrom(string ID)
        {
            throw new NotImplementedException();
        }

        public void Update(Event Obj, int ID)
        {
            throw new NotImplementedException();
        }

        public void Update(Event Obj, string ID)
        {
            throw new NotImplementedException();
        }

        public void Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public void Delete(string ID)
        {
            throw new NotImplementedException();
        }
    }
}
