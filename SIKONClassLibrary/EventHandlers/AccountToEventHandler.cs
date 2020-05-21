using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SIKONClassLibrary.EventHandlers
{
    public class AccountToEventHandler : IHandle<AccountToEvent>
    {
        private const string ServerUrl = "http://localhost:53683/";
        private const string RequestUri = "api/AccountToEvents";

        public void Create(AccountToEvent Obj)
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

        public List<AccountToEvent> Read()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.UseDefaultCredentials = true;

            List<AccountToEvent> AccountToEventList = new List<AccountToEvent>();
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
                        var List = response.Content.ReadAsAsync<IEnumerable<AccountToEvent>>().Result;
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

        public AccountToEvent ReadFrom(int ID)
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
                    AccountToEvent ev = null;

                    var response = client.GetAsync($"{RequestUri}/{ID}").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        ev = response.Content.ReadAsAsync<AccountToEvent>().Result;
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

        public AccountToEvent ReadFrom(string ID)
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
                    AccountToEvent ev = null;

                    var response = client.GetAsync($"{RequestUri}/{ID}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        ev = response.Content.ReadAsAsync<AccountToEvent>().Result;

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

        public bool Update(AccountToEvent Obj, int ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(AccountToEvent Obj, string ID)
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
