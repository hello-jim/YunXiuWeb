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

        [HttpGet]
        public ActionResult AddRole()
        {

            return View();
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>

        public string AddRolePost()
        {

            var result = "";
            var rName = Convert.ToString(Request.Form["rName"]);
            var describe = Convert.ToString(Request.Form["describe"]);
            Role r = new YunXiu.Model.Role
            {
                RName = rName,
                Describe = describe
            };
            var isAdd = Convert.ToBoolean(CommomClass.HttpPost(string.Format("{0}/Authority/AddRole", accountApi), JsonConvert.SerializeObject(r)));
            if (isAdd)
            {
                result = "1";
            }
            else
            {
                result = "-1";
            }
            return result;
            //   return PromptView("添加成功");
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <returns></returns>
        public string DeleteRole()
        {
            var result = "";
            var rID = Convert.ToInt32(Request.Form["rID"]);
            var isDel = Convert.ToBoolean(CommomClass.HttpPost(string.Format("{0}/Authority/DeleteRole", accountApi), rID.ToString()));
            if (isDel)
            {
                result = "1";
            }
            else
            {
                result = "-1";
            }
            return result;
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RoleUpdate()
        {
            Role role = new Role
            {
                RID = Convert.ToInt32(Request.Params["rID"]),
                RName = Convert.ToString(Request.Params["rName"]),
                Describe = Convert.ToString(Request.Params["describe"])
            };
            return View(role);
        }

        public string RoleUpdatePost()
        {
            var result = "";
            Role role = new Role
            {
                RID = Convert.ToInt32(Request.Form["rID"]),
                RName = Convert.ToString(Request.Form["rName"]),
                Describe = Convert.ToString(Request.Form["Describe"])
            };
            var isEdit = Convert.ToBoolean(CommomClass.HttpPost(string.Format("{0}/Authority/UpdateRole", accountApi), JsonConvert.SerializeObject(role)));
            if (isEdit)
            {
                result = "1";
            }
            else
            {
                result = "-1";
            }
            return result;
        }

        /// <summary>
        /// 权限
        /// </summary>
        /// <returns></returns>
        public ActionResult Permission()
        {
            List<Permission> list = new List<Permission>();
            list = JsonConvert.DeserializeObject<List<Permission>>(CommomClass.HttpPost(string.Format("{0}/Authority/GetPermission", accountApi), ""));
            return View(list);
        }

        public string AddPermissionPost()
        {
            var result = "";
            Permission p = new Permission
            {
                PName = Convert.ToString(Request.Form["pName"]),
                Describe = Convert.ToString(Request.Form["describe"]),
            };
            var isAdd = Convert.ToBoolean(CommomClass.HttpPost(string.Format("{0}/Authority/AddPermission", accountApi), JsonConvert.SerializeObject(p)));
            if (isAdd)
            {
                result = "1";
            }
            else
            {
                result = "-1";
            }
            return result;
        }

        /// <summary>
        /// 添加权限
        /// </summary>
        /// <returns></returns>
        public ActionResult AddPermission()
        {
            return View();
        }


        public string DeletePermission()
        {
            var result = "";
            var pID = Convert.ToInt32(Request.Form["pID"]);
            var isDel = Convert.ToBoolean(CommomClass.HttpPost(string.Format("{0}/Authority/DeletePermission", accountApi), pID.ToString()));
            if (isDel)
            {
                result = "1";
            }
            else
            {
                result = "-1";
            }
            return result;
        }

        [HttpGet]
        public ActionResult UpdatePermission()
        {
            Permission role = new Permission
            {
                PID = Convert.ToInt32(Request.Params["pID"]),
                PName = Convert.ToString(Request.Params["pName"]),
                PKey = Convert.ToString(Request.Params["pKey"]),
                Describe = Convert.ToString(Request.Params["describe"])
            };
            return View(role);
        }

        public string UpdatePermissionPost()
        {
            var result = "";
            Permission p = new Permission
            {
                PID = Convert.ToInt32(Request.Form["pID"]),
                PName = Convert.ToString(Request.Form["pName"]),
                Describe = Convert.ToString(Request.Form["describe"])
            };
            var isEdit = Convert.ToBoolean(CommomClass.HttpPost(string.Format("{0}/Authority/UpdatePermission", accountApi), JsonConvert.SerializeObject(p)));
            if (isEdit)
            {
                result = "1";
            }
            else
            {
                result = "-1";
            }
            return result;
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

        private void Load()
        {
            ViewData["navList"] = AdminNavs.GetNavList();
            ViewData["referer"] = MallUtils.GetMallAdminRefererCookie();
        }

    }
}
