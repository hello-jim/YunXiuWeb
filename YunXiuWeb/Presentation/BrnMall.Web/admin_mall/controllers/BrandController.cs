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
            var brandData = CommomClass.HttpPost("http://192.168.9.32:8082/Brand/GetBrand","");
            var brandCount = JsonConvert.DeserializeObject<List<Brand>>(brandData);
            BrandList model = new BrandList();
            model.Brands = brandCount;
<<<<<<< HEAD

=======
>>>>>>> remotes/origin/LIZIDEEP_MALL_V4
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
<<<<<<< HEAD
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

=======
>>>>>>> remotes/origin/LIZIDEEP_MALL_V4
        /// <summary>
        /// 添加品牌
        /// </summary>
        [HttpPost]
        public ActionResult Add(BrandModel model)
        {

       
            if (ModelState.IsValid)
            {
<<<<<<< HEAD
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
=======
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
>>>>>>> remotes/origin/LIZIDEEP_MALL_V4
                var path = Server.MapPath("/images/brand");
                string dataname = Convert.ToString(data);
                string filename = dataname + "_" + f.FileName;
                path = Path.Combine(path, filename);
<<<<<<< HEAD

=======
>>>>>>> remotes/origin/LIZIDEEP_MALL_V4
                f.SaveAs(path);


                return PromptView("");

<<<<<<< HEAD


                //brandInfo.Category = info.CateID;

=======
>>>>>>> remotes/origin/LIZIDEEP_MALL_V4
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
                Name=brandData.Category.Name
            };
            
            Load();

            return View(model);
        }

        /// <summary>
        /// 编辑品牌
        /// </summary>
<<<<<<< HEAD
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
=======
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
            else {
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
    

>>>>>>> remotes/origin/LIZIDEEP_MALL_V4

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
