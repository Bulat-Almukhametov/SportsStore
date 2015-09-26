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

        [Required(ErrorMessage = "Пожалуйста, введите названия товара")]
        public string Name { set; get; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Пожалуйста, введите описание")]
        public string Description { set; get; }

        [Required]
        [Range(0.01, Double.MaxValue, ErrorMessage = "Пожалуйста, введите положительное число для цены")]
        public decimal Price { set; get; }

        [Required(ErrorMessage = "Пожалуйста, определите категорию")]
        public string Category { set; get; }

        public byte[] ImageData { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }
    }
}
