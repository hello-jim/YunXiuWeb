﻿using System;
using System.Web;
using System.Data;
using System.Text;
using System.Web.Mvc;
using System.Collections.Generic;

using BrnMall.Core;
using BrnMall.Services;
using BrnMall.Web.Framework;
using BrnMall.Web.MallAdmin.Models;
using YunXiu.Model;
using YunXiu.Commom;
using Newtonsoft.Json;
using System.IO;

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
            var brandData = CommomClass.HttpPost("http://192.168.9.32:8082/Brand/GetBrand", "20");
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
        //[HttpGet]
        //public ActionResult brandList()
        //{
        //    var brandData = CommomClass.HttpPost("http://192.168.9.32:8082/Brand/GetBrand", "20");
        //    var getBrand = JsonConvert.DeserializeObject<List<Brand>>(brandData);
        //    BrandListModel model = new BrandListModel();
        //    model.Brands = getBrand;
        //    Load();
        //    return View(model);
        //}

        /// <summary>
        /// 添加品牌
        /// </summary>
        [HttpPost]
        public ActionResult Add(BrandModel model)
        {

            //if (AdminBrands.GetBrandIdByName(model.BrandName) > 0)
            //    ModelState.AddModelError("BrandName", "名称已经存在");

            if (ModelState.IsValid)
            {
                Brand brandInfo = new Brand()
                {

                    Sort = model.DisplayOrder,
                    Name = model.BrandName,
                    Category = new Category
                    {
                        CateId = 1
                    }

                };
                var caregoryData = CommomClass.HttpPost("http://192.168.9.32:8082/Category/GetCategory", "20");
                var caregoryCount = JsonConvert.DeserializeObject<List<Category>>(caregoryData);


                HttpPostedFileBase f = Request.Files[0];
                brandInfo.Logo = f.FileName;
                var data = CommomClass.HttpPost("http://192.168.9.32:8082/Brand/AddBrand", JsonConvert.SerializeObject(brandInfo));
                var path = Server.MapPath("/images/brand");
                string dataname = Convert.ToString(data);
                string filename = dataname + "_" + f.FileName;
                path = Path.Combine(path, filename);

                f.SaveAs(path);


                return PromptView("");



                //brandInfo.Category = info.CateID;

            }
            Load();
            return View(model);

        }

        /// <summary>
        /// 编辑品牌
        /// </summary>
        //[HttpGet]
        //public ActionResult Edit(int brandId = -1)
        //{
        //    BrandInfo brandInfo = AdminBrands.GetBrandById(brandId);
        //    if (brandInfo == null)
        //        return PromptView("品牌不存在");

        //    BrandModel model = new BrandModel();
        //    model.DisplayOrder = brandInfo.DisplayOrder;
        //    model.BrandName = brandInfo.Name;
        //    model.Logo = brandInfo.Logo;
        //    Load();

        //    return View(model);
        //}

        /// <summary>
        /// 编辑品牌
        /// </summary>
        //[HttpPost]
        //public ActionResult Edit(BrandModel model, int brandId = -1)
        //{
        //BrandInfo brandInfo = AdminBrands.GetBrandById(brandId);
        //if (brandInfo == null)
        //    return PromptView("品牌不存在");

        //int brandId2 = AdminBrands.GetBrandIdByName(model.BrandName);
        //if (brandId2 > 0 && brandId2 != brandId)
        //    ModelState.AddModelError("BrandName", "名称已经存在");

        //if (ModelState.IsValid)
        //{
        //brandInfo.DisplayOrder = model.DisplayOrder;
        //brandInfo.Name = model.BrandName;
        //brandInfo.Logo = model.Logo ?? "";

        //AdminBrands.UpdateBrand(brandInfo);
        //AddMallAdminLog("修改品牌", "修改品牌,品牌ID为:" + brandId);
        //return PromptView("品牌修改成功");
        //}

        //Load();
        //    return View(model);
        //}

        /// <summary>
        /// 删除品牌
        /// </summary>
        //public ActionResult Del(int brandId = -1)
        //{
        //    int result = AdminBrands.DeleteBrandById(brandId);
        //    if (result == 0)
        //        return PromptView("删除失败,请先删除此品牌下的商品");
        //    AddMallAdminLog("删除品牌", "删除品牌,品牌ID为:" + brandId);
        //    return PromptView("品牌删除成功");
        //}

        private void Load()
        {
            ViewData["allowImgType"] = BMAConfig.UploadConfig.UploadImgType.Replace(".", "");
            ViewData["maxImgSize"] = BMAConfig.UploadConfig.UploadImgSize;
            ViewData["referer"] = MallUtils.GetMallAdminRefererCookie();
        }
    }
}
