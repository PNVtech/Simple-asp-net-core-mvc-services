using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServerDBClients.Models;

namespace ServerDBClients.ModuleCode
{
    /// <summary>
    /// Класс для работы с базой данных
    /// </summary>
    /// 
    public class DataActions
    {
        private ClientsDBContext DB;

        public DataActions(ClientsDBContext db)
        {
            this.DB = db;
        }

        // Получить клиента по ID
        public Client GetClientById(int? ID)
        {
            if (ID.HasValue)
            {
                Client SelectedClient;

                SelectedClient = DB.Client.FirstOrDefault(c => c.Id == ID);
                return SelectedClient;
            }
            else
            {
                return null;
            }
        }

        // Получить клиента по ID для представления
        public ClientView GetClientViewById(int? ID)
        {
            if (ID.HasValue)
            {
                ClientView SelectedClient;

                SelectedClient = GetClientsView(DB.Client).FirstOrDefault(c => c.Id == ID);
                return SelectedClient;
            }
            else
            {
                return null;
            }
        }

        // Получить всех клиентов
        public IEnumerable<ClientView> GetClients()
        {
            return GetClientsView(DB.Client);
        }

        // Получить клиентов из города
        public IEnumerable<ClientView> GetClientsFromCity(int? CityID)
        {
            return GetClientsView(DB.Client).Where(c => c.CityId == CityID);
        }

        // Сохранить изменения клиента
        public async Task SaveClient(Client SelectedClient)
        {
            if (SelectedClient.Id > 0)
            {
                Client CurrentClient = GetClientById(SelectedClient.Id);
                CurrentClient.Name = SelectedClient.Name;
                CurrentClient.Surname = SelectedClient.Surname;
                CurrentClient.CityId = SelectedClient.CityId;
                await DB.SaveChangesAsync();
            }
            else
            {
                int MaxID = DB.Client.Max(c => c.Id) + 1;
                Client NewClient = new Client
                {
                    Id = MaxID,
                    Name = SelectedClient.Name,
                    Surname = SelectedClient.Surname,
                    CityId = SelectedClient.CityId
                };
                await DB.Client.AddAsync(NewClient);
                await DB.SaveChangesAsync();
            }
        }

        // Удалить клиента
        public async Task DeleteClient(int? id)
        {
            DB.Remove(GetClientById(id));
            await DB.SaveChangesAsync();
        }

        public IEnumerable<ClientView> GetClientsView(IEnumerable<Client> CurrentClients)
        {
            var ClientsViewEnumerable = from Clients in CurrentClients
                                        join Cities in DB.City on Clients.CityId equals Cities.Id into CW
                                        from subset in CW.DefaultIfEmpty<City>(new City() { Id = 0, Name = string.Empty })
                                        select new ClientView() { Id = Clients.Id, Name = Clients.Name, Surname = Clients.Surname, CityId = Clients.CityId, City = subset.Name };
            return ClientsViewEnumerable;
        }

        public IEnumerable<City> GetCities()
        {
            IEnumerable<City> Cities;

            Cities = DB.City;
            return Cities;
        }
    }
}
