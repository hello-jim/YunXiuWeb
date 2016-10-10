﻿using System;
using System.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using BrnMall.Core;
using BrnMall.Services;
using BrnMall.Web.Framework;
using System.Collections.Generic;
using YunXiu.Model;
namespace BrnMall.Web.MallAdmin.Models
{
    /// <summary>
    /// 品牌列表模型类
    /// </summary>
    public class BrandListModel
    {
        /// <summary>
        /// 分页对象
        /// </summary>
        public PageModel PageModel { get; set; }
        /// <summary>
        /// 品牌列表
        /// </summary>
        public DataTable BrandList { get; set; }
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }

        public int BrandID { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }
        public List<Brand> Brands { get; set; }
    }

    /// <summary>
    /// 品牌模型类
    ///// </summary>
    public class BrandModel
    {
        /// <summary>
        /// 品牌名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(10, ErrorMessage = "名称长度不能大于10")]
        public string BrandName { get; set; }
        /// <summary>
        /// 品牌logo
        /// </summary>
        [StringLength(50, ErrorMessage = "Logo文件名长度不能大于50")]
        public string Logo { get; set; }
        /// <summary>
        /// 品牌排序
        /// </summary>
        [DisplayName("排序")]
        public int DisplayOrder { get; set; }

        public Category Category { get; set; }

        public int CateID { get; set; }

        public int BrandID { get; set; }
        
        public int Sort
        {
            get;
            set;
        }
        public List<Brand> Brands { get; set; }

        public string Name { get; set; }

        public bool IsHotBrand { get; set; }
    }
    public class BrandList {
        public int BrandID
        {
            get;
            set;
        }
        public int Sort
        {
            get;
            set;
        }
        public List<Brand> Brands { get; set; }

        public List<Category> Categorys { get; set; }

        public int CateID { get; set; }

        public string Logo { get; set; }

        public string Name { get; set; }

    }
 
}
