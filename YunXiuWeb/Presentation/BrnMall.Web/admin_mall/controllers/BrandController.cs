using System;
using System.Web;
using System.Data;
using System.Text;
using System.Web.Mvc;
using System.Collections.Generic;
using System.IO;
using BrnMall.Core;
using BrnMall.Services;
using BrnMall.Web.Framework;
using BrnMall.Web.MallAdmin.Models;
using YunXiu.Model;
using YunXiu.Commom;
using Newtonsoft.Json;

namespace BrnMall.Web.MallAdmin.Controllers
{
    /// <summary>
    /// 商城后台品牌控制器类
    /// </summary>
    public partial class BrandController : BaseMallAdminController
    {
        /// <summary>
        /// 品牌列表
        /// </summary>
        /// <param name="brandName">品牌名称</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        //public ActionResult List(string brandName, int pageSize = 15, int pageNumber = 1)
        //{
        //    string condition = AdminBrands.AdminGetBrandListCondition(brandName);

        //    PageModel pageModel = new PageModel(pageSize, pageNumber, AdminBrands.AdminGetBrandCount(condition));

        //    BrandListModel model = new BrandListModel()
        //    {
        //        PageModel = pageModel,
        //        BrandList = AdminBrands.AdminGetBrandList(pageModel.PageSize, pageModel.PageNumber, condition),
        //        BrandName = brandName
        //    };
        //    MallUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&brandName={3}",
        //                                                  Url.Action("list"),
        //                                                  pageModel.PageNumber,
        //                                                  pageModel.PageSize,
        //                                                  brandName));
        //    return View(model);
        //}
        public ActionResult List()
        {
            var brandData = CommomClass.HttpPost("http://192.168.9.32:8082/Brand/GetBrand", "30");
            var brandCount = JsonConvert.DeserializeObject<List<Brand>>(brandData);
            BrandList model = new BrandList();
            model.Brands = brandCount;

            return View(model);
        }
        /// <summary>
        /// 品牌选择列表
        /// </summary>
        /// <param name="brandName">品牌名称</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        public ContentResult SelectList(string brandName, int pageNumber = 1, int pageSize = 24)
        {
            string condition = AdminBrands.AdminGetBrandListCondition(brandName);
            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminBrands.AdminGetBrandCount(condition));

            DataTable brandSelectList = AdminBrands.AdminGetBrandSelectList(pageModel.PageSize, pageModel.PageNumber, condition);

            StringBuilder result = new StringBuilder("{");
            result.AppendFormat("\"totalPages\":\"{0}\",\"pageNumber\":\"{1}\",\"items\":[", pageModel.TotalPages, pageModel.PageNumber);
            foreach (DataRow row in brandSelectList.Rows)
                result.AppendFormat("{0}\"id\":\"{1}\",\"name\":\"{2}\"{3},", "{", row["brandid"], row["name"].ToString().Trim(), "}");

            if (brandSelectList.Rows.Count > 0)
                result.Remove(result.Length - 1, 1);

            result.Append("]}");
            return Content(result.ToString());
        }


        /// <summary>
        /// 添加品牌
        /// </summary>
        [HttpGet]
        public ActionResult Add()
        {
            BrandModel model = new BrandModel();
            Load();
            return View(model);
        }
        /// <summary>
        /// 添加品牌
        /// </summary>
        [HttpPost]
        public ActionResult Add(BrandModel model)
        {

       
            if (ModelState.IsValid)
            {
                HttpPostedFileBase f = Request.Files[0];

                Brand brand = new Brand
                {
                    Sort = model.DisplayOrder,
                    Name = model.BrandName,
                    Logo = f.FileName,
                    Category=new Category
                    {
                    CateId=model.CateID
                    }
                };

                var data = CommomClass.HttpPost("http://192.168.9.32:8082/Brand/AddBrand", JsonConvert.SerializeObject(brand));
                var path = Server.MapPath("/images/brand");
                string dataname = Convert.ToString(data);
                string filename = dataname + "_" + f.FileName;
                path = Path.Combine(path, filename);
                f.SaveAs(path);


                return PromptView("");

            }
            Load();
            return View(model);

        }

        /// <summary>
        /// 编辑品牌
        /// </summary>
        [HttpGet]
        public ActionResult Edit(int BrandId)
        {
            //BrandInfo brandInfo = AdminBrands.GetBrandById(brandId);
            var data = CommomClass.HttpPost("http://192.168.9.32:8082/Brand/GetBrandByID", JsonConvert.SerializeObject(BrandId));
            var brandData = JsonConvert.DeserializeObject<Brand>(data);
            if (brandData == null)
                return PromptView("品牌不存在");

            BrandModel model = new BrandModel();
            model.BrandID = brandData.BrandID;
            model.DisplayOrder = brandData.Sort;
            model.BrandName = brandData.Name;
            model.Logo = brandData.Logo;
            Load();

            return View(model);
        }

        /// <summary>
        /// 编辑品牌
        /// </summary>
        [HttpPost]
        public ActionResult Edit(BrandModel model)
        {
            //BrandInfo brandInfo = AdminBrands.GetBrandById(brandId);
            //if (brandInfo == null)
            //    return PromptView("品牌不存在");
            var logo = model.Logo;
            if (logo != null)
            {
                var data1 = CommomClass.HttpPost("http://192.168.9.32:8082/Brand/GetBrandByID", JsonConvert.SerializeObject(model.BrandID));
                var brandData = JsonConvert.DeserializeObject<Brand>(data1);
                var oldLogo = brandData.Logo;
            }

            //if (brandData == null)
            //    return PromptView("品牌不存在");
            if (ModelState.IsValid)
            {
                
                HttpPostedFileBase f = Request.Files[0];
                BrandModel brandList = new BrandModel()
                {
                    BrandID=model.BrandID,
                    Sort = model.DisplayOrder,
                    Name = model.BrandName,
                    Logo = f.FileName,
                    Category = new Category
                    {
                        CateId = model.CateID
                    }
                };
                
                var updateBrand = CommomClass.HttpPost("http://192.168.9.32:8082/Brand/UpdateBrand", JsonConvert.SerializeObject(brandList));
                //var OleLogo = oldLogo;
                var path = Server.MapPath("/images/brand");
                string dataname = Convert.ToString(updateBrand);
                string filename = dataname + "_" + f.FileName;
                path = Path.Combine(path, filename);
                f.SaveAs(path);
                return PromptView("");
            }

            Load();
            return View(model);
        }

        /// <summary>
        /// 删除品牌
        /// </summary>
        public ActionResult Del(int brandId = -1)
        {
            int result = AdminBrands.DeleteBrandById(brandId);
            if (result == 0)
                return PromptView("删除失败,请先删除此品牌下的商品");
            AddMallAdminLog("删除品牌", "删除品牌,品牌ID为:" + brandId);
            return PromptView("品牌删除成功");
        }

        private void Load()
        {
            ViewData["allowImgType"] = BMAConfig.UploadConfig.UploadImgType.Replace(".", "");
            ViewData["maxImgSize"] = BMAConfig.UploadConfig.UploadImgSize;
            ViewData["referer"] = MallUtils.GetMallAdminRefererCookie();
        }
    }
}
