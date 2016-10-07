using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using BrnMall.Web.Models;
using BrnMall.Web.Models;
using BrnMall.Core;
using BrnMall.Services;
using BrnMall.Web.Framework;

namespace BrnMall.Web.Controllers
{
    public class CommonProblemController : BaseWebController
    {
        //
        // GET: /CommonProblem/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TermsTrade() 
        {
            return View();
        }

        public ActionResult ShoppingProcess()
        {
            return View();
        }
        public ActionResult Fraud()
        {
            return View();
        }
        public ActionResult ReceiptProblems()
        {
            return View();
        }
        public ActionResult OrderViolation()
        {
            return View();
        }
        public ActionResult ProductConsultation()
        {
            return View();
        }
        public ActionResult OrderSearch()
        {
            return View();
        }

    }
}
