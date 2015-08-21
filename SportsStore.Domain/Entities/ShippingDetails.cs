using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Пожалуйста введите имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите первую строку для адреса")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите название населенного пункта")]
        public string City { get; set; }

    public string Zip { get; set; }

    [Required(ErrorMessage = "Пожалуйста, введите страну")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
