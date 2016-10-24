﻿using System;
using System.Data;
using System.Web.Mvc;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using BrnMall.Core;
using BrnMall.Services;
using BrnMall.Web.Framework;
using YunXiu.Model;

namespace BrnMall.Web.StoreAdmin.Models
{
    /// <summary>
    /// 店铺模型类
    /// </summary>
    public class StoreModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(30, ErrorMessage = "名称长度不能大于30")]
        public string StoreName { get; set; }
        /// <summary>
        /// 区域id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "请选择区域")]
        [DisplayName("区域")]
        public int RegionId { get; set; }
        /// <summary>
        /// 行业id
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "请选择行业")]
        [DisplayName("行业")]
        public int StoreIid { get; set; }
        /// <summary>
        /// logo
        /// </summary>
        [StringLength(50, ErrorMessage = "logo长度不能大于50")]
        public string Logo { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        [Mobile]
        public string Mobile { get; set; }
        /// <summary>
        /// 固定电话
        /// </summary>
        [BrnMall.Web.Framework.Phone]
        public string Phone { get; set; }
        /// <summary>
        /// qq
        /// </summary>
        [StringLength(11, ErrorMessage = "qq长度不能大于11")]
        public string QQ { get; set; }
        /// <summary>
        /// 阿里旺旺
        /// </summary>
        [StringLength(50, ErrorMessage = "阿里旺旺长度不能大于50")]
        public string WW { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Theme { get; set; }
        /// <summary>
        /// Banner
        /// </summary>
        [StringLength(50, ErrorMessage = "banner长度不能大于50")]
        public string Banner { get; set; }
        /// <summary>
        /// 公告
        /// </summary>
        [StringLength(100, ErrorMessage = "详细地址长度不能大于100")]
        public string Announcement { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [StringLength(150, ErrorMessage = "描述长度不能大于150")]
        public string Description { get; set; }
    }

    /// <summary>
    /// 店铺分类列表模型类
    /// </summary>
    public class StoreClassListModel
    {
        /// <summary>
        /// 店铺分类列表
        /// </summary>
        public List<StoreClassInfo> StoreClassList { get; set; }
    }

    /// <summary>
    /// 店铺分类模型类
    /// </summary>
    public class StoreClassModel
    {
        /// <summary>
        /// 店铺分类名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(30, ErrorMessage = "名称长度不能大于30")]
        public string StoreClassName { get; set; }

        /// <summary>
        /// 父店铺分类id
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "请选择分类")]
        public int ParentId { get; set; }

        /// <summary>
        /// 店铺分类排序
        /// </summary>
        [Required(ErrorMessage = "排序不能为空")]
        [DisplayName("排序")]
        public int DisplayOrder { get; set; }
    }

    /// <summary>
    /// 店铺配送模板列表模型类
    /// </summary>
    public class StoreShipTemplateListModel
    {
        /// <summary>
        /// 店铺配送模板列表
        /// </summary>
        public List<StoreShipTemplateInfo> StoreShipTemplateList { get; set; }
    }

    /// <summary>
    /// 添加店铺配送模板模型类
    /// </summary>
    public class AddStoreShipTemplateModel
    {
        /// <summary>
        /// 模板标题
        /// </summary>
        [Required(ErrorMessage = "模板标题不能为空")]
        [StringLength(50, ErrorMessage = "模板标题长度不能大于50")]
        public string TemplateTitle { get; set; }
        /// <summary>
        /// 是否免运费
        /// </summary>
        public int Free { get; set; }
        /// <summary>
        /// 计费类型(0代表按件数计算,1代表按重量计算)
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 起步值
        /// </summary>
        [Required(ErrorMessage = "起步值不能为空")]
        [Range(0, int.MaxValue, ErrorMessage = "起步值不能为负数")]
        [DisplayName("起步值")]
        public int StartValue { get; set; }
        /// <summary>
        /// 起步价
        /// </summary>
        [Required(ErrorMessage = "起步价不能为空")]
        [Range(0, int.MaxValue, ErrorMessage = "起步价不能为负数")]
        [DisplayName("起步价")]
        public int StartFee { get; set; }
        /// <summary>
        /// 加值
        /// </summary>
        [Required(ErrorMessage = "加值不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "加值必须大于0")]
        [DisplayName("加值")]
        public int AddValue { get; set; }
        /// <summary>
        /// 加价
        /// </summary>
        [Required(ErrorMessage = "加价不能为空")]
        [Range(0, int.MaxValue, ErrorMessage = "加价不能为负数")]
        [DisplayName("加价")]
        public int AddFee { get; set; }
    }

    /// <summary>
    /// 编辑店铺配送模板模型类
    /// </summary>
    public class EditStoreShipTemplateModel
    {
        /// <summary>
        /// 模板标题
        /// </summary>
        [Required(ErrorMessage = "模板标题不能为空")]
        [StringLength(50, ErrorMessage = "模板标题长度不能大于50")]
        public string TemplateTitle { get; set; }
        /// <summary>
        /// 是否免运费
        /// </summary>
        public int Free { get; set; }
        /// <summary>
        /// 计费类型(0代表按件数计算,1代表按重量计算)
        /// </summary>
        public int Type { get; set; }
    }

    /// <summary>
    /// 店铺配送费用列表模型类
    /// </summary>
    public class StoreShipFeeListModel
    {
        /// <summary>
        /// 店铺配送模板id
        /// </summary>
        public int StoreSTid { get; set; }
        /// <summary>
        /// 店铺配送费用列表
        /// </summary>
        public DataTable StoreShipFeeList { get; set; }
    }

    /// <summary>
    /// 店铺配送费用模型类
    /// </summary>
    public class StoreShipFeeModel
    {
        /// <summary>
        /// 区域
        /// </summary>
        [Required(ErrorMessage = "请选择区域")]
        [Range(-1, int.MaxValue, ErrorMessage = "请选择区域")]
        [DisplayName("区域")]
        public int RegionId { get; set; }
        /// <summary>
        /// 起步值
        /// </summary>
        [Required(ErrorMessage = "起步值不能为空")]
        [Range(0, int.MaxValue, ErrorMessage = "起步值不能为负数")]
        [DisplayName("起步值")]
        public int StartValue { get; set; }
        /// <summary>
        /// 起步价
        /// </summary>
        [Required(ErrorMessage = "起步价不能为空")]
        [Range(0, int.MaxValue, ErrorMessage = "起步价不能为负数")]
        [DisplayName("起步价")]
        public int StartFee { get; set; }
        /// <summary>
        /// 加值
        /// </summary>
        [Required(ErrorMessage = "加值不能为空")]
        [Range(1, int.MaxValue, ErrorMessage = "加值必须大于0")]
        [DisplayName("加值")]
        public int AddValue { get; set; }
        /// <summary>
        /// 加价
        /// </summary>
        [Required(ErrorMessage = "加价不能为空")]
        [Range(0, int.MaxValue, ErrorMessage = "加价不能为负数")]
        [DisplayName("加价")]
        public int AddFee { get; set; }
    }

    public class DynamicsModel 
    {
        public List<StoreDynamics> Dynamics { get; set; }

        public string Title { get; set; }

        public Store Store { get; set; }

        public DateTime CreatTime { get; set; }
        public string DContent { get; set; }

        public int DID { get; set; }

        public User User { get; set; }

        public int CreateUserID { get; set; }

        public int LastUpdateUserID { get; set; }

        public User CreateUser { get; set; }
    }
}
