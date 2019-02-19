using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using MVCSiteClients.Models;

namespace MVCSiteClients.ModuleCode
{
    /// <summary>
    /// Класс для работы с web сервисами
    /// </summary>

    public class WebActions
    {
        // Базовый адрес сервиса получения данных
        private static Uri BaseURI = new Uri($"http://localhost:50079/");

        // Получить всех клиентов
        public static async Task<IEnumerable<ClientView>> GetClients()
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = BaseURI;
                HttpResponseMessage HTTPResult = await http.GetAsync($"api/Clients");
                HTTPResult.EnsureSuccessStatusCode();
                var Result = await HTTPResult.Content.ReadAsAsync<IEnumerable<ClientView>>();
                return Result;
            }
        }

        // Получить клиентов их города по ID
        public static async Task<IEnumerable<ClientView>> GetClientsFromCity(int? CityID)
        {
            if (CityID.HasValue)
            {
                using (var http = new HttpClient())
                {
                    http.BaseAddress = BaseURI;
                    HttpResponseMessage HTTPResult = await http.GetAsync($"/api/ClientsFromCity/{CityID}");
                    HTTPResult.EnsureSuccessStatusCode();
                    var Result = await HTTPResult.Content.ReadAsAsync<IEnumerable<ClientView>>();
                    return Result;
                }
            }
            else
            { return null; }
        }

        // Получить определённого клиента
        public static async Task<ClientView> GetClient(int? ClientID)
        {
            if (ClientID.HasValue)
            {
                using (var http = new HttpClient())
                {
                    http.BaseAddress = BaseURI;
                    HttpResponseMessage HTTPResult = await http.GetAsync($"api/Clients/{ClientID}");
                    HTTPResult.EnsureSuccessStatusCode();
                    ClientView Result = await HTTPResult.Content.ReadAsAsync<ClientView>();
                    return Result;
                }
            }
            else
            {
                return null;
            }
        }

        // Добавить клиента
        public static async void AddClient(ClientView NewClientView)
        {
            // POST api/Clients ([FromBody] Client NewClient)
            using (var http = new HttpClient())
            {
                Client NewClient = new Client() { Id = NewClientView.Id, Name = NewClientView.Name, Surname = NewClientView.Surname, CityId = NewClientView.CityId };
                http.BaseAddress = BaseURI;
                await http.PostAsJsonAsync<Client>("api/Clients", NewClient);
            }
        }

        // Редактировать клиента
        public static async void EditClient(ClientView NewClientView)
        {
            // PUT api/Clients/5 ([FromRoute] int id, [FromBody] Client SelectedClient)
            using (var http = new HttpClient())
            {
                Client NewClient = new Client() { Id = NewClientView.Id, Name = NewClientView.Name, Surname = NewClientView.Surname, CityId = NewClientView.CityId };
                http.BaseAddress = BaseURI;
                await http.PutAsJsonAsync<Client>($"api/Clients/{NewClientView.Id}", NewClient);
            }
        }

        // Удалить клиента
        public static async void DeleteClient(int? ClientID)
        {
            // DELETE api/Clients/5
            using (var http = new HttpClient())
            {
                http.BaseAddress = BaseURI;
                await http.DeleteAsync($"api/Clients/{ClientID}");
            }
        }

        // Получить все города
        public static async Task<IEnumerable<City>> GetCities()
        {
            using (var http = new HttpClient())
            {
                http.BaseAddress = BaseURI;
                HttpResponseMessage HttpResult = await http.GetAsync("api/cities");
                HttpResult.EnsureSuccessStatusCode();
                var Result = await HttpResult.Content.ReadAsAsync<IEnumerable<City>>();
                return Result.ToArray();
            }
        }
    }
}
