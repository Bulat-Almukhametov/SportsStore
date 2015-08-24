using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportsStore.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductID { set; get; }
        public string Name { set; get; }

        [DataType(DataType.MultilineText)]
        public string Description { set; get; }
        public decimal Price { set; get; }
        public string Category { set; get; }
    }
}
