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

        public ActionResult List()
        {
            var brandData = CommomClass.HttpPost("http://192.168.9.32:8082/Brand/GetBrand", "");
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
                    Category = new Category
                    {
                        CateId = model.CateID
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
            model.Category = new Category
            {
                CateId = brandData.Category.CateId,
                Name = brandData.Category.Name
            };

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
            HttpPostedFileBase f = Request.Files[0];
            var logo = f.FileName;
            if (logo != "")
            {
                var data1 = CommomClass.HttpPost("http://192.168.9.32:8082/Brand/GetBrandByID", JsonConvert.SerializeObject(model.BrandID));
                var brandData = JsonConvert.DeserializeObject<Brand>(data1);

                Brand brandList = new Brand()
                {

                    BrandID = model.BrandID,
                    Sort = model.DisplayOrder,
                    Name = model.BrandName,
                    Logo = f.FileName,
                    Category = new Category
                    {
                        CateId = model.CateID,
                    }
                };

                var updateBrand = CommomClass.HttpPost("http://192.168.9.32:8082/Brand/UpdateBrand", JsonConvert.SerializeObject(brandList));
                //var OleLogo = oldLogo;
                var path = Server.MapPath("/images/brand");
                var delpath = Server.MapPath("/images/brand");
                var oldLogo = brandData.Logo;
                string oldLogoName = model.BrandID + "_" + oldLogo;
                delpath = Path.Combine(path, oldLogoName);
                if (System.IO.File.Exists(delpath))
                {
                    System.IO.File.Delete(delpath);
                }
                string dataname = Convert.ToString(model.BrandID);
                string filename = dataname + "_" + f.FileName;
                path = Path.Combine(path, filename);
                f.SaveAs(path);
                return PromptView("更改成功");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var data1 = CommomClass.HttpPost("http://192.168.9.32:8082/Brand/GetBrandByID", JsonConvert.SerializeObject(model.BrandID));
                    var brandData = JsonConvert.DeserializeObject<Brand>(data1);
                    string str = brandData.Logo.TrimEnd();
                    Brand brandList = new Brand()
                    {
                        BrandID = model.BrandID,
                        Sort = model.DisplayOrder,
                        Name = model.BrandName,
                        Logo = str,
                        Category = new Category
                        {
                            CateId = model.CateID
                        }

                    };

                    var updateBrand = CommomClass.HttpPost("http://192.168.9.32:8082/Brand/UpdateBrand", JsonConvert.SerializeObject(brandList));
                    //var OleLogo = oldLogo;
                    return PromptView("更改成功");
                }
            }
            //if (brandData == null)
            //    return PromptView("品牌不存在");


            Load();
            return View(model);
        }



        /// <summary>
        /// 删除品牌
        /// </summary>
        public ActionResult Del(int BrandId)
        {
            var delBrand = CommomClass.HttpPost("http://192.168.9.32:8082/Brand/DeleteBrand", JsonConvert.SerializeObject(BrandId));
            return PromptView("删除成功");
        }

        private void Load()
        {
            ViewData["allowImgType"] = BMAConfig.UploadConfig.UploadImgType.Replace(".", "");
            ViewData["maxImgSize"] = BMAConfig.UploadConfig.UploadImgSize;
            ViewData["referer"] = MallUtils.GetMallAdminRefererCookie();
        }
    }
}
