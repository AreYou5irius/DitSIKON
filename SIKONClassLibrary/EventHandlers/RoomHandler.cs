using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SIKONClassLibrary.EventHandlers
{
    public class RoomHandler : IHandle<Room>
    {
        //private const string ServerUrl = "http://localhost:53683/";
        private const string ServerUrl = "http://sikon.azurewebsites.net/";
        private const string RequestUri = "api/Rooms";

        public void Create(Room Obj)
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

        public List<Room> Read()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.UseDefaultCredentials = true;

            List<Room> ListObjects = new List<Room>();
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
                        var List = response.Content.ReadAsAsync<IEnumerable<Room>>().Result;
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

        public Room ReadFrom(int ID)
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
                    Room ro = null;

                    var response = client.GetAsync($"{RequestUri}/{ID}").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        ro = response.Content.ReadAsAsync<Room>().Result;
                    }

                    return ro;
                }
                catch (Exception e)
                {
                    MessageDialogHelper.Show($"Ingen Forbindelse til serveren", "Server Fejl");
                    return null;
                }
            }
        }

        public Room ReadFrom(string ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Room Obj, int ID)
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
                    return client.PutAsJsonAsync<Room>($"{RequestUri}/{ID}", Obj).Result.IsSuccessStatusCode;
                }
                catch (Exception)
                {
                    MessageDialogHelper.Show($"Ingen Forbindelse til serveren", "Server Fejl");
                    return false;
                }
            }
        }

        public bool Update(Room Obj, string ID)
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
