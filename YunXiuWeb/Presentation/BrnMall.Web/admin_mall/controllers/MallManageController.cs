using BrnMall.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using BrnMall.Core;
using BrnMall.Services;
using BrnMall.Web.MallAdmin.Models;
using YunXiu.Model;
using YunXiu.Commom;
using Newtonsoft.Json;

namespace BrnMall.Web.MallAdmin.Controllers
{
    public partial class MallManageController : BaseMallAdminController
    {
        //
        // GET: /MallAdmin/
        string accountApi = ConfigurationManager.AppSettings["accountApi"];
        /// <summary>
        /// 角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Role()
        {
            List<Role> list = JsonConvert.DeserializeObject<List<Role>>(CommomClass.HttpPost(string.Format("{0}/Authority/GetRole", accountApi), ""));
            return View(list);
        }

        public ActionResult AddRole() 
        {
            
            return View();
        }

        
       
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        public ActionResult AddRole(Role role)
        {
            return View();
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateRole()
        {
            return View();
        }

        /// <summary>
        /// 权限
        /// </summary>
        /// <returns></returns>
        public ActionResult Permission()
        {
            return View();
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <returns></returns>
        public ActionResult AddPermission()
        {
            return View();
        }

        public ActionResult UpdatePermission()
        {
            return View();
        }

        /// <summary>
        /// 角色权限
        /// </summary>
        /// <returns></returns>
        public ActionResult RolePermission()
        {
            return View();
        }

        public ActionResult UserRole() 
        {
            return View();
        }

        public ActionResult UserPermission() 
        {
            return View();
        }

        /// <summary>
        /// 添加角色权限
        /// </summary>
        /// <returns></returns>
        public string AddRolePermission() 
        {
            var result = "";
            return result;
        }

    }
}
