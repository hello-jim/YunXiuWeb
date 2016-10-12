﻿using System;
using System.Text;
using System.Data;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using BrnMall.Core;
using BrnMall.Services;
using BrnMall.Web.Framework;
using BrnMall.Web.Models;
using YunXiu.Commom;
using YunXiu.Model;
using System.Configuration;

namespace BrnMall.Web.Controllers
{
    /// <summary>
    /// 商城目录控制器类
    /// </summary>
    public partial class CatalogController : BaseWebController
    {
        string productApi = ConfigurationManager.AppSettings["productApi"].ToString();
        string accountApi = ConfigurationManager.AppSettings["accountApi"].ToString();
        /// <summary>
        /// 商品
        /// </summary>
        /// <returns></returns>
        public ActionResult Product()
        {
            //商品id
            int pid = GetRouteInt("pid");
            if (pid == 0)
                pid = WebHelper.GetQueryInt("pid");

            //判断商品是否存在
            //ProductInfo productInfo = Products.GetProductById(pid);

            //if (productInfo == null)
            //    return PromptView("/", "你访问的商品不存在");

            var productJson = CommomClass.HttpPost(string.Format("{0}/Product/GetProductByID", productApi), pid.ToString());
            var product = JsonConvert.DeserializeObject<Product>(productJson);

            //获取商品库存
            var stockNumber = Convert.ToInt32(CommomClass.HttpPost(string.Format("{0}/Product/GetProductStock", productApi), pid.ToString()));
            var consultationResult = JsonConvert.DeserializeObject<ConsultationResult>(CommomClass.HttpPost(string.Format("{0}/Product/GetConsultation", productApi), pid.ToString()));

            //商品存在时
            ProductModel model = new ProductModel();
            model.ProductInfo = product;
            model.StockNumber = stockNumber;
            model.ConsultationResult = consultationResult;

            //   //商品id
            //   model.Pid = pid;
            //   //商品信息
            ////   model.ProductInfo = productInfo;
            //   //商品分类
            //   model.CategoryInfo = Categories.GetCategoryById(productInfo.CateId);
            //   //商品品牌
            //   model.BrandInfo = Brands.GetBrandById(productInfo.BrandId);
            //   //店铺信息
            //   model.StoreInfo = storeInfo;
            //   //店长信息
            //   model.StoreKeeperInfo = Stores.GetStoreKeeperById(storeInfo.StoreId);
            //   //店铺区域
            //   model.StoreRegion = BrnMall.Services.Regions.GetRegionById(storeInfo.RegionId);
            //   //店铺等级信息
            //   model.StoreRankInfo = StoreRanks.GetStoreRankById(storeInfo.StoreRid);
            //   //商品图片列表
            //   model.ProductImageList = Products.GetProductImageList(pid);
            //   //扩展商品属性列表
            //   model.ExtProductAttributeList = Products.GetExtProductAttributeList(pid);
            //   //商品SKU列表
            //   model.ProductSKUList = Products.GetProductSKUListBySKUGid(productInfo.SKUGid);
            //   //商品库存数量
            //   model.StockNumber = Products.GetProductStockNumberByPid(pid);


            //单品促销
            //model.SinglePromotionInfo = Promotions.GetSinglePromotionByPidAndTime(pid, DateTime.Now);
            ////买送促销活动列表
            //model.BuySendPromotionList = Promotions.GetBuySendPromotionList(productInfo.StoreId, pid, DateTime.Now);
            ////赠品促销活动
            //model.GiftPromotionInfo = Promotions.GetGiftPromotionByPidAndTime(pid, DateTime.Now);
            ////赠品列表
            //if (model.GiftPromotionInfo != null)
            //    model.ExtGiftList = Promotions.GetExtGiftList(model.GiftPromotionInfo.PmId);
            ////套装商品列表
            //model.SuitProductList = Promotions.GetProductAllSuitPromotion(pid, DateTime.Now);
            ////满赠促销活动
            //model.FullSendPromotionInfo = Promotions.GetFullSendPromotionByStoreIdAndPidAndTime(productInfo.StoreId, pid, DateTime.Now);
            ////满减促销活动
            //model.FullCutPromotionInfo = Promotions.GetFullCutPromotionByStoreIdAndPidAndTime(productInfo.StoreId, pid, DateTime.Now);

            ////广告语
            //model.Slogan = model.SinglePromotionInfo == null ? "" : model.SinglePromotionInfo.Slogan;
            ////商品促销信息
            //model.PromotionMsg = Promotions.GeneratePromotionMsg(model.SinglePromotionInfo, model.BuySendPromotionList, model.FullSendPromotionInfo, model.FullCutPromotionInfo);
            ////商品折扣价格
            //model.DiscountPrice = Promotions.ComputeDiscountPrice(model.ProductInfo.ShopPrice, model.SinglePromotionInfo);

            ////关联商品列表
            //model.RelateProductList = Products.GetRelateProductList(pid);

            ////用户浏览历史
            //model.UserBrowseHistory = BrowseHistories.GetUserBrowseHistory(WorkContext.Uid, pid);

            ////商品咨询类型列表
            //model.ProductConsultTypeList = BrnMall.Services.ProductConsults.GetProductConsultTypeList();

            ////更新浏览历史
            //if (WorkContext.Uid > 0)
            //    Asyn.UpdateBrowseHistory(WorkContext.Uid, pid);
            ////更新商品统计
            //Asyn.UpdateProductStat(pid, WorkContext.RegionId);

            return View(model);
        }

        /// <summary>
        /// 套装
        /// </summary>
        public ActionResult Suit()
        {
            //套装id
            int pmId = GetRouteInt("pmId");
            if (pmId == 0)
                pmId = WebHelper.GetQueryInt("pmId");

            //判断套装是否存在或过期
            SuitPromotionInfo suitPromotionInfo = Promotions.GetSuitPromotionByPmIdAndTime(pmId, DateTime.Now);
            if (suitPromotionInfo == null)
                return PromptView("/", "你访问的套装不存在或过期");

            //店铺信息
            StoreInfo storeInfo = Stores.GetStoreById(suitPromotionInfo.StoreId);
            if (storeInfo.State != (int)StoreState.Open)
                return PromptView("/", "你访问的套装不存在");

            //扩展套装商品列表
            List<ExtSuitProductInfo> extSuitProductList = Promotions.GetExtSuitProductList(pmId);

            SuitModel model = new SuitModel();
            model.SuitPromotionInfo = suitPromotionInfo;
            model.SuitProductList = extSuitProductList;
            model.StoreInfo = storeInfo;
            model.StoreKeeperInfo = Stores.GetStoreKeeperById(storeInfo.StoreId);
            model.StoreRegion = BrnMall.Services.Regions.GetRegionById(storeInfo.RegionId);
            model.StoreRankInfo = StoreRanks.GetStoreRankById(storeInfo.StoreRid);

            foreach (ExtSuitProductInfo extSuitProductInfo in extSuitProductList)
            {
                model.SuitDiscount += extSuitProductInfo.Number * extSuitProductInfo.Discount;
                model.ProductAmount += extSuitProductInfo.Number * extSuitProductInfo.ShopPrice;
            }
            model.SuitAmount = model.ProductAmount - model.SuitDiscount;

            return View(model);
        }

        /// <summary>
        /// 分类
        /// </summary>
        public ActionResult Category()
        {
            //分类id
            int cateId = GetRouteInt("cateId");
            if (cateId == 0)
                cateId = WebHelper.GetQueryInt("cateId");
            //品牌id
            int brandId = GetRouteInt("brandId");
            if (brandId == 0)
                brandId = WebHelper.GetQueryInt("brandId");
            //筛选价格
            int filterPrice = GetRouteInt("filterPrice");
            if (filterPrice == 0)
                filterPrice = WebHelper.GetQueryInt("filterPrice");
            //筛选属性
            string filterAttr = GetRouteString("filterAttr");
            if (filterAttr.Length == 0)
                filterAttr = WebHelper.GetQueryString("filterAttr");
            //是否只显示有货
            int onlyStock = GetRouteInt("onlyStock");
            if (onlyStock == 0)
                onlyStock = WebHelper.GetQueryInt("onlyStock");
            //排序列
            int sortColumn = GetRouteInt("sortColumn");
            if (sortColumn == 0)
                sortColumn = WebHelper.GetQueryInt("sortColumn");
            //排序方向
            int sortDirection = GetRouteInt("sortDirection");
            if (sortDirection == 0)
                sortDirection = WebHelper.GetQueryInt("sortDirection");
            //当前页数
            int page = GetRouteInt("page");
            if (page == 0)
                page = WebHelper.GetQueryInt("page");

            //分类信息
            CategoryInfo categoryInfo = Categories.GetCategoryById(cateId);
            if (categoryInfo == null)
                return PromptView("/", "此分类不存在");

            //分类关联品牌列表
            List<BrandInfo> brandList = Categories.GetCategoryBrandList(cateId);
            //分类筛选属性及其值列表
            List<KeyValuePair<AttributeInfo, List<AttributeValueInfo>>> cateAAndVList = Categories.GetCategoryFilterAAndVList(cateId);
            //分类价格范围列表
            string[] catePriceRangeList = StringHelper.SplitString(categoryInfo.PriceRange, "\r\n");

            //筛选属性处理
            List<int> attrValueIdList = new List<int>();
            string[] filterAttrValueIdList = StringHelper.SplitString(filterAttr, "-");
            if (filterAttrValueIdList.Length != cateAAndVList.Count)//当筛选属性和分类的筛选属性数目不对应时，重置筛选属性
            {
                if (cateAAndVList.Count == 0)
                {
                    filterAttr = "0";
                }
                else
                {
                    int count = cateAAndVList.Count;
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < count; i++)
                        sb.Append("0-");
                    filterAttr = sb.Remove(sb.Length - 1, 1).ToString();
                }
            }
            else
            {
                foreach (string attrValueId in filterAttrValueIdList)
                {
                    int temp = TypeHelper.StringToInt(attrValueId);
                    if (temp > 0) attrValueIdList.Add(temp);
                }
            }

            //分页对象
            PageModel pageModel = new PageModel(20, page, Products.GetCategoryProductCount(cateId, brandId, filterPrice, catePriceRangeList, attrValueIdList, onlyStock));
            //视图对象
            CategoryModel model = new CategoryModel()
            {
                CateId = cateId,
                BrandId = brandId,
                FilterPrice = filterPrice,
                FilterAttr = filterAttr,
                OnlyStock = onlyStock,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                CategoryInfo = categoryInfo,
                BrandList = brandList,
                CatePriceRangeList = catePriceRangeList,
                AAndVList = cateAAndVList,
                PageModel = pageModel,
                //  ProductList = Products.GetCategoryProductList(pageModel.PageSize, pageModel.PageNumber, cateId, brandId, filterPrice, catePriceRangeList, attrValueIdList, onlyStock, sortColumn, sortDirection)
            };

            return View(model);
        }

        /// <summary>
        /// 搜索
        /// </summary>
        public ActionResult Search()
        {
            //搜索词
            string word = WebHelper.GetQueryString("word");
            //分类id
            int cateId = WebHelper.GetQueryInt("cateId");
            //品牌id
            int brandId = WebHelper.GetQueryInt("brandId");
            //筛选价格
            int filterPrice = WebHelper.GetQueryInt("filterPrice");
            //筛选属性
            string filterAttr = WebHelper.GetQueryString("filterAttr");
            //是否只显示有货
            int onlyStock = WebHelper.GetQueryInt("onlyStock");
            //排序列
            int sortColumn = WebHelper.GetQueryInt("sortColumn");
            //排序方向
            int sortDirection = WebHelper.GetQueryInt("sortDirection");
            //当前页数
            int page = WebHelper.GetQueryInt("page");

            //搜索词处理
            WorkContext.SearchWord = word;
            if (word.Length == 0)
                return PromptView(WorkContext.UrlReferrer, "请输入搜索词");

            //检查当前页数
            if (page < 1) page = 1;

            //筛选属性处理
            List<int> attrValueIdList = new List<int>();
            string[] filterAttrValueIdList = StringHelper.SplitString(filterAttr, "-");
            foreach (string attrValueId in filterAttrValueIdList)
            {
                int temp = TypeHelper.StringToInt(attrValueId);
                if (temp > 0) attrValueIdList.Add(temp);
            }

            //分类信息
            CategoryInfo categoryInfo = null;
            //分类价格范围列表
            string[] catePriceRangeList = null;
            //分类筛选属性及其值列表
            List<KeyValuePair<AttributeInfo, List<AttributeValueInfo>>> cateAAndVList = null;
            //分类列表
            List<CategoryInfo> categoryList = null;
            //品牌信息
            BrandInfo brandInfo = null;
            //品牌列表
            List<BrandInfo> brandList = null;
            //商品总数量
            int totalCount = 0;
            //商品列表
            List<StoreProductInfo> productList = null;
            //搜索
            //Searches.SearchMallProducts(20, page, word, cateId, brandId, filterPrice, attrValueIdList, onlyStock, sortColumn, sortDirection, ref categoryInfo, ref catePriceRangeList, ref cateAAndVList, ref categoryList, ref brandInfo, ref brandList, ref totalCount, ref productList);
            string[] arr = new string[] { word, page.ToString(), "20" };
            var data = CommomClass.HttpPost(string.Format("{0}/Product/SearchProduct", productApi), JsonConvert.SerializeObject(arr));
            PageResult<Product> pageResult = JsonConvert.DeserializeObject<PageResult<Product>>(data);
            if (pageResult == null)
                return PromptView(WorkContext.UrlReferrer, "您搜索的商品不存在");
            //找出是否是同个类型的产品
            // List<int> sameCate = new List<int>();
            //foreach (var info in pageResult.ResultList) 
            //{
            //    sameCate.Add(info.Category.CateID);
            //}
            //List<CateAttribute> cateAttrList = null;
            //List<AttributeValue> attrValList = null;
            //if (sameCate.Count == 1) //如果都是相同的则找出相同类型的
            //{
            //    cateAttrList = JsonConvert.DeserializeObject<List<CateAttribute>>(CommomClass.HttpPost(string.Format("{0}/Category/GetCateAttr"), sameCate[0].ToString()));
            //    var attrIDList=new List<int>();
            //    cateAttrList.ForEach(a => attrIDList.Add(a.AttrID));
            //    attrValList = JsonConvert.DeserializeObject<List<AttributeValue>>(CommomClass.HttpPost(string.Format("{0}/Category/GetAttrVal"), JsonConvert.SerializeObject(attrIDList)));
            //}
            //else 
            //{
            //    //不是的话就按当前指定规则找出渲染分类

            //}

            //当筛选属性和分类的筛选属性数目不对应时，重置筛选属性
            if (cateAAndVList == null)
            {
                filterAttr = "0";
            }
            else
            {
                if (filterAttrValueIdList.Length != cateAAndVList.Count)
                {
                    if (cateAAndVList.Count == 0)
                    {
                        filterAttr = "0";
                    }
                    else
                    {
                        int count = cateAAndVList.Count;
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < count; i++)
                            sb.Append("0-");
                        filterAttr = sb.Remove(sb.Length - 1, 1).ToString();
                    }
                }
            }

            //用户浏览历史
            //List<PartProductInfo> userBrowseHistory = BrowseHistories.GetUserBrowseHistory(WorkContext.Uid, 0);

            //分页对象
            // PageModel pageModel = new PageModel(20, page, totalCount);
            //视图对象
            MallSearchModel model = new MallSearchModel()
            {
                Word = word,
                CateId = cateId,
                BrandId = brandId,
                FilterPrice = filterPrice,
                FilterAttr = filterAttr,
                OnlyStock = onlyStock,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                FilterAttrValueIdList = attrValueIdList,
                CategoryInfo = categoryInfo,
                CatePriceRangeList = catePriceRangeList,
                AAndVList = cateAAndVList,
                CategoryList = categoryList,
                BrandInfo = brandInfo,
                BrandList = brandList,
                //PageModel = pageModel,
                //ProductList = productList,
                //  UserBrowseHistory = userBrowseHistory
                PageResult = pageResult
            };

            //异步保存搜索历史
            Asyn.UpdateSearchHistory(WorkContext.Uid, word);

            return View(model);
        }

        /// <summary>
        /// 活动专题
        /// </summary>
        public ActionResult Topic()
        {
            //专题id
            int topicId = GetRouteInt("topicId");
            if (topicId == 0)
                topicId = WebHelper.GetQueryInt("topicId");

            TopicInfo topicInfo = BrnMall.Services.Topics.GetTopicById(topicId);
            if (topicInfo == null)
                return PromptView("此活动专题不存在");

            return View(topicInfo);
        }

        /// <summary>
        /// 商品评价列表
        /// </summary>
        public ActionResult ProductReviewList()
        {
            int pid = WebHelper.GetQueryInt("pid");
            int reviewType = WebHelper.GetQueryInt("reviewType");
            int page = WebHelper.GetQueryInt("page");

            //判断商品是否存在
            PartProductInfo productInfo = Products.GetPartProductById(pid);
            if (productInfo == null)
                return PromptView("/", "你访问的商品不存在");

            //店铺信息
            StoreInfo storeInfo = Stores.GetStoreById(productInfo.StoreId);
            if (storeInfo.State != (int)StoreState.Open)
                return PromptView("/", "你访问的商品不存在");

            if (reviewType < 0 || reviewType > 3) reviewType = 0;

            PageModel pageModel = new PageModel(10, page, ProductReviews.GetProductReviewCount(pid, reviewType));
            ProductReviewListModel model = new ProductReviewListModel()
            {
                ProductInfo = productInfo,
                CategoryInfo = Categories.GetCategoryById(productInfo.CateId),
                BrandInfo = Brands.GetBrandById(productInfo.BrandId),
                StoreInfo = storeInfo,
                StoreKeeperInfo = Stores.GetStoreKeeperById(storeInfo.StoreId),
                StoreRegion = BrnMall.Services.Regions.GetRegionById(storeInfo.RegionId),
                StoreRankInfo = StoreRanks.GetStoreRankById(storeInfo.StoreRid),
                ReviewType = reviewType,
                PageModel = pageModel,
                ProductReviewList = ProductReviews.GetProductReviewList(pid, reviewType, pageModel.PageSize, pageModel.PageNumber)
            };

            return View(model);
        }

        /// <summary>
        /// 商品评价列表
        /// </summary>
        public ActionResult AjaxProductReviewList()
        {
            int pid = WebHelper.GetQueryInt("pid");
            int reviewType = WebHelper.GetQueryInt("reviewType");
            int page = WebHelper.GetQueryInt("page");

            if (reviewType < 0 || reviewType > 3) reviewType = 0;

            PageModel pageModel = new PageModel(10, page, ProductReviews.GetProductReviewCount(pid, reviewType));
            AjaxProductReviewListModel model = new AjaxProductReviewListModel()
            {
                Pid = pid,
                ReviewType = reviewType,
                PageModel = pageModel,
                ProductReviewList = ProductReviews.GetProductReviewList(pid, reviewType, pageModel.PageSize, pageModel.PageNumber)
            };

            return View(model);
        }

        /// <summary>
        /// 商品咨询列表
        /// </summary>
        public ActionResult ProductConsultList()
        {
            int pid = WebHelper.GetQueryInt("pid");
            int consultTypeId = WebHelper.GetQueryInt("consultTypeId");
            string consultMessage = WebHelper.GetQueryString("consultMessage");
            int page = WebHelper.GetQueryInt("page");

            //判断商品是否存在
            PartProductInfo productInfo = Products.GetPartProductById(pid);
            var productJson = CommomClass.HttpPost(string.Format("{0}/Product/GetProductByID", productApi), pid.ToString());
            var product = JsonConvert.DeserializeObject<Product>(productJson);
            if (product == null)
                return PromptView("/", "你访问的商品不存在");

            //获取产品咨询类型
            var consultationTypeListJson = CommomClass.HttpPost(string.Format("{0}/Product/GetConsultationType", productApi), "");
            var consultationTypeList = JsonConvert.DeserializeObject<List<ConsultationType>>(consultationTypeListJson);
            ProductConsultListModel model = new ProductConsultListModel()
            {
                ProductInfo = product,
                ConsultationTypeList = consultationTypeList
            };

            return View(model);
        }

        /// <summary>
        /// 商品咨询列表
        /// </summary>
        public ActionResult AjaxProductConsultList()
        {
            int pid = WebHelper.GetQueryInt("pid");
            int consultTypeId = WebHelper.GetQueryInt("consultTypeId");
            string consultMessage = WebHelper.GetQueryString("consultMessage");
            int page = WebHelper.GetQueryInt("page");

            PageModel pageModel = new PageModel(10, page, BrnMall.Services.ProductConsults.GetProductConsultCount(pid, consultTypeId, consultMessage));
            AjaxProductConsultListModel model = new AjaxProductConsultListModel()
            {
                Pid = pid,
                ConsultTypeId = consultTypeId,
                ConsultMessage = consultMessage,
                PageModel = pageModel,
                ProductConsultList = BrnMall.Services.ProductConsults.GetProductConsultList(pageModel.PageSize, pageModel.PageNumber, pid, consultTypeId, consultMessage),
                ProductConsultTypeList = BrnMall.Services.ProductConsults.GetProductConsultTypeList(),
                IsVerifyCode = CommonHelper.IsInArray(WorkContext.PageKey, WorkContext.MallConfig.VerifyPages)
            };

            return View(model);
        }

        /// <summary>
        /// 咨询商品
        /// </summary>
        public ActionResult ConsultProduct()
        {
            //不允许游客访问
            if (WorkContext.Uid < 1)
                return AjaxResult("nologin", "请先登录");

            //验证验证码
            if (CommonHelper.IsInArray(WorkContext.PageKey, WorkContext.MallConfig.VerifyPages))
            {
                string verifyCode = WebHelper.GetFormString("verifyCode");//验证码
                if (string.IsNullOrWhiteSpace(verifyCode))
                {
                    return AjaxResult("emptyverifycode", "验证码不能为空"); ;
                }
                else if (verifyCode.ToLower() != Sessions.GetValueString(WorkContext.Sid, "verifyCode"))
                {
                    return AjaxResult("wrongverifycode", "验证码错误"); ;
                }
            }

            int pid = WebHelper.GetFormInt("pid");
            int consultTypeId = WebHelper.GetFormInt("consultTypeId");
            string consultMessage = WebHelper.GetFormString("consultMessage");

            PartProductInfo partProductInfo = Products.GetPartProductById(pid);
            if (partProductInfo == null)
                return AjaxResult("noproduct", "请选择商品");

            StoreInfo storeInfo = Stores.GetStoreById(partProductInfo.StoreId);
            if (storeInfo.State != (int)StoreState.Open)
                return AjaxResult("noproduct", "请选择商品");

            if (consultTypeId < 1 || BrnMall.Services.ProductConsults.GetProductConsultTypeById(consultTypeId) == null)
                return AjaxResult("noproductconsulttype", "请选择咨询类型"); ;

            if (string.IsNullOrWhiteSpace(consultMessage))
                return AjaxResult("noconsultmessage", "请填写咨询内容"); ;
            if (consultMessage.Length > 100)
                return AjaxResult("muchconsultmessage", "咨询内容内容太长"); ;

            BrnMall.Services.ProductConsults.ConsultProduct(pid, consultTypeId, WorkContext.Uid, partProductInfo.StoreId, DateTime.Now, WebHelper.HtmlEncode(consultMessage), WorkContext.NickName, partProductInfo.Name, partProductInfo.ShowImg, WorkContext.IP);
            return AjaxResult("success", Url.Action("product", new RouteValueDictionary { { "pid", pid } })); ;
        }

        /// <summary>
        /// 添加商品咨询
        /// </summary>
        /// <returns></returns>
        public string AddConsultation()
        {
            var result = "";
            try
            {
                var cTypeID = Convert.ToInt32(Request.Form["typeID"]);
                var content = Request.Form["content"].ToString();
                var pID = Convert.ToInt32(Request.Form["pID"]);
             
                if (SUserInfo != null)
                {            
                    Consultation consultation = new Consultation
                    {
                        CProduct = new Product
                        {
                            PID = pID
                        },
                        CreateUser = new User
                        {
                            UID = SUserInfo.UID
                        },
                        CType = new ConsultationType
                        {
                            ID = cTypeID
                        },
                        CContent = content
                    };
                    var isAdd = Convert.ToBoolean(CommomClass.HttpPost(string.Format("{0}/Product/AddConsultation"), JsonConvert.SerializeObject(consultation)));//是否添加成功
                    if (isAdd)
                    {
                        result = "1";
                    }
                }
                else
                {
                    result = "-1";//用户不存在
                }

            }
            catch (Exception ex)
            {

            }
            return result;
        }

        /// <summary>
        /// 添加商品评价
        /// </summary>
        /// <returns></returns>
        public string AddProductReview()
        {
            var result = "0";
            try
            {
                var content = Request.Form["content"];
                var pID = Convert.ToInt32(Request.Form["pID"]);

                var oID = Convert.ToInt32(Request.Form["oID"]);
                var parent = Request.Form["parent"] != null ? Convert.ToInt32(Request.Form["parent"]) : 0;
                if (SUserInfo != null)//登录后才能评论
                {
                    ProductReview review = new ProductReview
                    {
                        RContent = content,
                        RProduct = new Product
                        {
                            PID = pID
                        },
                        RUser = new User
                        {
                            UID =  SUserInfo.UID
                        },
                        ROrder = new Order
                        {
                            OID = oID
                        },
                        ReviewTime = DateTime.Now,
                        Parent = parent
                    };
                    var isAdd = Convert.ToBoolean(CommomClass.HttpPost(string.Format("{0}/Product/AddProductReview", productApi), JsonConvert.SerializeObject(review)));
                    if (isAdd)
                    {
                        result = "1";//评论成功
                    }
                }
                else
                {
                    result = "-1";//
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        /// <summary>
        /// 获取商品评论
        /// </summary>
        /// <returns></returns>
        public string GetProductReview()
        {
            var reviewJson = "";
            try
            {
                var pID = Convert.ToInt32(Request.Form["pID"]);//商品ID
                reviewJson = CommomClass.HttpPost(string.Format("{0}/Product/GetProductReviewByProductID", productApi), pID.ToString());
            }
            catch (Exception ex)
            {

            }
            return reviewJson;
        }
    }
}
