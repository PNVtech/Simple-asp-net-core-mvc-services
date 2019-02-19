using System;
using System.Collections.Generic;

namespace MVCSiteClients.Models
{
    public partial class City
    {
        public City()
        {
            Client = new HashSet<Client>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Client> Client { get; set; }
    }
}
