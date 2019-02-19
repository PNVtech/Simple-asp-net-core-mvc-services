using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MVCSiteClients.Models
{
    public class ClientView
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name обязательное поле.")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Surname обязательное поле.")]
        [StringLength(50)]
        public string Surname { get; set; }
        public int? CityId { get; set; }
        public string City { get; set; }
    }
}
