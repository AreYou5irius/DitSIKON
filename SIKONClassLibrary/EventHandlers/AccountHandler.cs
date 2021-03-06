﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace SIKONClassLibrary.EventHandlers
{
    public class AccountHandler : IHandle<Account>
    {
        //private const string ServerUrl = "http://localhost:53683/";
        private const string ServerUrl = "http://sikon.azurewebsites.net/";
        private const string RequestUri = "api/Accounts";

        public void Create(Account Obj)
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
                    throw new Exception("Serveren er ikke tilgængelig");
                }
            }
        }

        public List<Account> Read()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.UseDefaultCredentials = true;

            List<Account> ListObjects = new List<Account>();
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
                        var List = response.Content.ReadAsAsync<IEnumerable<Account>>().Result;
                        foreach (var ob in List)
                        {
                            ListObjects.Add(ob);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Serveren er ikke tilgængelig");
                }
            }
            return ListObjects;
        }

        public Account ReadFrom(int ID)
        {
            throw new NotImplementedException();
        }

        public Account ReadFrom(string ID)
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
                    Account ev = null;

                    var response = client.GetAsync($"{RequestUri}/{ID}").Result;
                    if (response.IsSuccessStatusCode)
                    {
                        ev = response.Content.ReadAsAsync<Account>().Result;
                    }

                    return ev;
                }
                catch (Exception e)
                {
                    throw new Exception("Serveren er ikke tilgængelig");
                }
            }
        }

        public bool Update(Account Obj, int ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Account Obj, string ID)
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
                    return client.PutAsJsonAsync<Account>($"{RequestUri}/{ID}", Obj).Result.IsSuccessStatusCode;
                }
                catch (Exception e)
                {
                    throw new Exception("Serveren er ikke tilgængelig");
                }
            }
        }

        public void Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public void Delete(string ID)
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
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        /// <summary>
        /// Tjekker om brugeren eksisterer og om det givne kodeord er korrekt.
        /// </summary>
        /// <param name="id">Email til den konto der skal logges ind</param>
        /// <param name="password">Password til konto</param>
        /// <returns>Thrower exception hvis konto ikke eksistere eller password er forkert, ellers returnerer den den indloggede bruger</returns>
        public Account LogIn(string id, string password)
        {
            try
            {
                Account account = ReadFrom(id);

                if (account == null)
                {
                    throw new Exception("Konto ikke fundet!");  
                }

                if (password == account.Password)
                {
                    return account;
                }

                Exception accountNotFoundException = new Exception("Password forkert");

                throw accountNotFoundException;
            }
            catch (Exception e)
            {

                throw e;

                //MessageDialogHelper.Show($"{e.Message}","Login Fejl");
                //return null;
            }
        }
    }
}
