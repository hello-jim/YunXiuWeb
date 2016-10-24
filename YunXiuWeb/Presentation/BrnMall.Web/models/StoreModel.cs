using System;
using System.Collections.Generic;
using BrnMall.Core;
using BrnMall.Services;
using BrnMall.Web.Framework;
using YunXiu.Model;

namespace BrnMall.Web.Models
{
    /// <summary>
    /// 店铺分类模型类
    /// </summary>
    public class StoreClassModel
    {
        /// <summary>
        /// 店铺分类id
        /// </summary>
        public int StoreCid { get; set; }
        /// <summary>
        /// 排序列
        /// </summary>
        public int SortColumn { get; set; }
        /// <summary>
        /// 排序方向
        /// </summary>
        public int SortDirection { get; set; }
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<PartProductInfo> ProductList { get; set; }
        /// <summary>
        /// 店铺分类信息
        /// </summary>
        public StoreClassInfo StoreClassInfo { get; set; }
    }

    /// <summary>
    /// 店铺搜索模型类
    /// </summary>
    public class StoreSearchModel
    {
        /// <summary>
        /// 搜索词
        /// </summary>
        public string Word { get; set; }
        /// <summary>
        /// 店铺分类id
        /// </summary>
        public int StoreCid { get; set; }
        /// <summary>
        /// 开始价格
        /// </summary>
        public int StartPrice { get; set; }
        /// <summary>
        /// 结束价格
        /// </summary>
        public int EndPrice { get; set; }
        /// <summary>
        /// 排序列
        /// </summary>
        public int SortColumn { get; set; }
        /// <summary>
        /// 排序方向
        /// </summary>
        public int SortDirection { get; set; }
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<PartProductInfo> ProductList { get; set; }
        /// <summary>
        /// 店铺分类信息
        /// </summary>
        public StoreClassInfo StoreClassInfo { get; set; }
    }

    public class StoreHomeModel
    {
        /// <summary>
        /// 主页信息
        /// </summary>
        public StoreHome StoreHome { get; set; }

        /// <summary>
        /// 店铺信息
        /// </summary>
        public Store StoreInfo { get; set; }
    }

    public class StoreIntroductionModel
    {

        /// <summary>
        /// 店铺信息
        /// </summary>
        public Store StoreInfo { get; set; }

        /// <summary>
        /// 公司证件
        /// </summary>
        public List<Certificate> CertificateList { get; set; }
    }

    public class SupplyProductModel
    {
        /// <summary>
        /// 产品分类
        /// </summary>
        public List<Categories> CateList { get; set; }

        /// <summary>
        /// 供应商品
        /// </summary>
        public List<Product> Products { get; set; }

        /// <summary>
        /// 店铺信息
        /// </summary>
        public Store StoreInfo { get; set; }
    }

    public class StoreAlbumModel 
    {
        /// <summary>
        /// 店铺信息
        /// </summary>
        public Store StoreInfo { get; set; }

        /// <summary>
        /// 店铺相册
        /// </summary>
        public List<StoreImg> StoreImages { get; set; }

        public List<Product> Products { get; set; }

    }
    /// <summary>
    /// 店铺动态信息类
    /// </summary>
    public class StoreNewsModel 
    {
        public Store StoreInfo { get; set; }
        public List<StoreDynamics> StoreDynamics { get; set; }

        public List<Product> Products { get; set; }
    }
}