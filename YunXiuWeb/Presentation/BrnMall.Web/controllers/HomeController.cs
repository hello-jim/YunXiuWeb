using System;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json;
using BrnMall.Core;
using BrnMall.Services;
using BrnMall.Web.Framework;
using YunXiu.Model;
using YunXiu.Commom;
using BrnMall.Web.Models;
using System.Collections.Generic;
using BrnMall.Web.Models;

namespace BrnMall.Web.Controllers
{
    /// <summary>
    /// 首页控制器类
    /// </summary>
    public partial class HomeController : BaseWebController
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //判断请求是否来自移动设备，如果是则重定向到移动主题
            if (WebHelper.GetQueryInt("m") != 1 && WebHelper.IsMobile())
                return RedirectToAction("index", "home", new RouteValueDictionary { { "area", "mob" } });

            //商品
            
            //首页的数据需要在其视图文件中直接调用，所以此处不再需要视图模型
            HomeModel model = new HomeModel();
         
           
            var count = CommomClass.HttpPost("http://192.168.9.32:8082/Product/GetHotProduct", "6");
            var hotProduct = JsonConvert.DeserializeObject<List<Product>>(count);
            model.HotProducts = hotProduct;

            //获取类目
            var getCategory = CommomClass.HttpPost("http://192.168.9.32:8082/Category/GetCategory", "20");
            var gateCoryCount = JsonConvert.DeserializeObject<List<Category>>(getCategory);
            model.GetCategorys = gateCoryCount;
            //获取类目36
            var getCategoryByID = CommomClass.HttpPost("http://192.168.9.32:8082/Category/GetCategoryByID", "36");
            var getCategorydata = JsonConvert.DeserializeObject<Category>(getCategoryByID);
            model.CategoryName = getCategorydata.Name;
            //获取类目38
            var getCategoryOne = CommomClass.HttpPost("http://192.168.9.32:8082/Category/GetCategoryByID", "38");
            var getCategoryDataOne = JsonConvert.DeserializeObject<Category>(getCategoryOne);
            model.CategoryNameOne = getCategoryDataOne.Name;
            //根据类目13名获取产品
            var getProductByCategoryData = CommomClass.HttpPost("http://192.168.9.32:8082/Product/GetProductByCategory", "36,6");
            var categoryProduct = JsonConvert.DeserializeObject<List<Product>>(getProductByCategoryData);
            model.GetProductByCategory = categoryProduct;
            //根据类目19名获取产品、
            var getProductByCategoryDataOne = CommomClass.HttpPost("http://192.168.9.32:8082/Product/GetProductByCategory", "38,6");
            var categoryProductOn = JsonConvert.DeserializeObject<List<Product>>(getProductByCategoryDataOne);
            model.GetProductByCategoryOne = categoryProductOn;

            var cateCoryData = CommomClass.HttpPost("http://192.168.9.32:8082/Category/GetCategoryByID", "2");
            var Category = JsonConvert.DeserializeObject<Category>(cateCoryData);
            //品牌
            var brandData = CommomClass.HttpPost("http://192.168.9.32:8082/Brand/GetBrandByID", "36");
            var brand = JsonConvert.DeserializeObject<Brand>(brandData);
            model.Logo = brand.Logo;
            //获取品牌
            var getbrand = CommomClass.HttpPost("http://192.168.9.32:8082/Brand/GetBrand", "18");
            var getBrandData = JsonConvert.DeserializeObject<List<Brand>>(getbrand);
            model.BrandImg = getBrandData;
            //动态品牌
            var brandCount = CommomClass.HttpPost("http://192.168.9.32:8082/Brand/GetShowDynamicBrand", "3");
            var showBrand = JsonConvert.DeserializeObject<List<Brand>>(brandCount);
            model.ShowDynamicBran = showBrand;
            //热门品牌
            var hotBrandCount = CommomClass.HttpPost("http://192.168.9.32:8082/Brand/GetHotBrand", "7");
            var hotBrand = JsonConvert.DeserializeObject<List<Brand>>(hotBrandCount);
            model.HotBrand = hotBrand;
            //根据类目38获取品牌
            var brandByCategoryDataOne = CommomClass.HttpPost("http://192.168.9.32:8082/Brand/GetBrandByCategory", "38");
            var brandByCategoryOne = JsonConvert.DeserializeObject<List<Brand>>(brandByCategoryDataOne);
            model.GetBrandByCategoryOne = brandByCategoryOne;
            //根据类目36获取品牌
            var brandByCategoryData = CommomClass.HttpPost("http://192.168.9.32:8082/Brand/GetBrandByCategory", "36");
            var brandByCategory = JsonConvert.DeserializeObject<List<Brand>>(brandByCategoryData);
            model.GetBrandByCategory = brandByCategory;

            //获取banner数量
            var bannerCount = CommomClass.HttpPost("http://192.168.9.32:8082/Banner/GetBanner", "3");
            var banner = JsonConvert.DeserializeObject<List<Banner>>(bannerCount);
            model.GetBanner = banner;
       
            return View(model);
        }
    }
}
