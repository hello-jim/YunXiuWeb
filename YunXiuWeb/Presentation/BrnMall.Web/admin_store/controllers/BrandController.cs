﻿using System;
using System.Web;
using System.Text;
using System.Data;
using System.Web.Mvc;
using System.Collections.Generic;
using BrnMall.Core;
using BrnMall.Services;
using BrnMall.Web.Framework;
using BrnMall.Web.StoreAdmin.Models;
using YunXiu.Model;
<<<<<<< HEAD
using YunXiu.Commom;
using Newtonsoft.Json;
using System.Configuration;
=======
using Newtonsoft.Json;
using YunXiu.Commom;
using System.Collections.Generic;
>>>>>>> remotes/origin/LIZIDEEP_MALL_V6

namespace BrnMall.Web.StoreAdmin.Controllers
{

    /// <summary>
    /// 店铺后台品牌控制器类
    /// </summary>
    public partial class BrandController : BaseStoreAdminController
    {
        string productApi = ConfigurationManager.AppSettings["productApi"];
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

            //  DataTable brandSelectList = AdminBrands.AdminGetBrandSelectList(pageModel.PageSize, pageModel.PageNumber, condition);
            var brandList = JsonConvert.DeserializeObject<List<Brand>>(CommomClass.HttpPost(string.Format("{0}/Brand/GetBrand", productApi), ""));
            StringBuilder result = new StringBuilder("{");
            result.AppendFormat("\"totalPages\":\"{0}\",\"pageNumber\":\"{1}\",\"items\":[", pageModel.TotalPages, pageModel.PageNumber);
            foreach (var b in brandList)
                result.AppendFormat("{0}\"id\":\"{1}\",\"name\":\"{2}\"{3},", "{", b.BrandID, b.Name, "}");

            if (brandList.Count > 0)
                result.Remove(result.Length - 1, 1);

            result.Append("]}");
            return Content(result.ToString());
        }
 
    }
}
