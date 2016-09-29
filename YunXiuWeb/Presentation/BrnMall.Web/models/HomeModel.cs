using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YunXiu.Model;

namespace BrnMall.Web.Models
{
    public class HomeModel
    {
        public int Pid { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public string CategoryNameOne { get; set; }
        public decimal ShopPrice { get; set; }
        public decimal SaleCount { get; set; }

        public decimal CategoryShopPrice { get; set; }
        public decimal CategorySaleCount { get; set; }

        public List<Product> Products { get; set; }
        public List<Product> HotProducts { get; set; }
        public List<Product> GetProductByCategory { get; set; }
        public List<Product> GetProductByCategoryOne { get; set; }
        public string Logo { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Brand> ShowDynamicBran { get; set; }
        public List<Brand> HotBrand { get; set; }
        public List<Brand> GetBrandByCategory { get; set; }
        public List<Brand> GetBrand { get; set; }
        public List<Brand> BrandImg { get; set; }
        public List<Brand> GetBrandByCategoryOne { get; set; }
        public List<Category> GetCategorys { get; set; }
        public List<Category> GetCategoryByID { get; set; }
        public List<Banner> GetBanner { get; set; }

        
    }
}