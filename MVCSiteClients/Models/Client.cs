using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVCSiteClients.Models
{
    public partial class Client
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name обязательное поле.")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Surname обязательное поле.")]
        [StringLength(50)]
        public string Surname { get; set; }
        public int? CityId { get; set; }

        public City City { get; set; }
    }
}
