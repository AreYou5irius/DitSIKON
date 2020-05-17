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

        #region Create
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
                    var response = client.PostAsJsonAsync("api/Events", Obj).Result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        #endregion


        #region Read
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

        }
        #endregion

        #region ReadFromID
        public Event ReadFrom(int ID)
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
                        foreach (var ob in List.Where(ob => ob.ID == ID))
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

            return ListObjects;   // forstår ikke fejlen

        } 
        #endregion


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
                bool status;

                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = client.PutAsJsonAsync("api/Events", ID).Result;

                    List<Event> EventList = new List<Event>();

                    foreach (var e in EventList.Where(e => e.ID == ID))
                    {
                        e.Category = Obj.Category;
                        //Jeg mangler en eller anden form for SaveChanges();
                    }
                    
                    status = true;
                }
                catch (Exception )
                {
                    status = false;
                }

                return status;
            }
        }

        public void Update(Event Obj, string ID)
        {
            throw new NotImplementedException();
        }

        #region Delete
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
                    var response = client.DeleteAsync($"api/Events/{ID}").Result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        } 
        #endregion

        public void Delete(string ID)
        {
            throw new NotImplementedException();
        }
    }
}
