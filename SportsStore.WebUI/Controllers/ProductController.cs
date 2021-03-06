﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 10;

        public ProductController(IProductRepository productRepository)
        {
            repository = productRepository;
        }
        
        public ViewResult List(string category, int page = 1)
        {
            var productsOfSelectedCategory = repository.Products.Where(p => 
                category == null || p.Category.Replace(" ", "") == category);

            ProductsListViewModel model = new ProductsListViewModel
            {
                Products =  productsOfSelectedCategory
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = productsOfSelectedCategory.Count()
                    },
                    CurrentCategory = category
            };

            return View(model);
        }

       
        public FileContentResult GetImage(int productId)
        {
            Product prod = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public ActionResult Message()
        {
            var myStringBuilder = new StringBuilder();


            return Content(myStringBuilder.ToString(), "text/event-stream");
        }
    }
}
