using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MachineTestModels;
using Database.Operations;

namespace MachineTest.Controllers
{
    public class HomeController : Controller
    {
        InsertRepository insertData = new InsertRepository();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public JsonResult AddUserToDB(string Fname, string Lname, string password)
        {
            string message = "";
            UserModelModel userss = new UserModelModel();
            userss.FirstName = Fname;
            userss.LastName = Lname;
            userss.Password = password;
            userss.InitCode = Fname.Substring(0, 1).ToUpper().Trim() + Lname.Substring(0, 1).ToUpper().Trim();
            message = insertData.AddUser(userss);
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Login()
        {
            return View();
        }

        public JsonResult LoginUser(string username, string password)
        {
            var message = new object[2];
            message[0] = insertData.LoginUser(username, password);
            return Json(message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DataRec()
        {
            return View();
        }

        public JsonResult AddDataRec(string code, string type, string rate, string hrs, string total)
        {
            var message = new object[2];
            DatarecordModelModel model = new DatarecordModelModel();
            model.Code = code;
            model.Hours = hrs;
            model.Rate = rate;
            model.Total = total;
            model.Type = type;
            message[0] = insertData.AddDataRec(model);
            return Json(message, JsonRequestBehavior.AllowGet);
        }
    }
}