using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB_SCONET_MANAGEMENT.Controllers
{
    public class HomeController : Controller
    {
        private Entities_School db = new Entities_School();
        bool v1, v2 = false;
        decimal f_ca_1, f_ca_2, exam;
        public void verify()
        {
            if (Session["school"] != null)
            {
                v1 = true;
            }
            else
            {
                v1 = false;
            }
            ////////////////////
            if (v1 == true)
                if (Session["role"].ToString().Contains("comment") || Session["cat_status"].ToString().Contains("Super Sup"))
                {
                    v2 = true;
                }
                else
                {
                    v2 = false;
                }
        }
        public ActionResult Index()
        {
            verify();
            if (v1 == false)
            {
                return RedirectToAction("Login", "Home");
            }
           /* else if (v2 == false)
            {
                return RedirectToAction("Deny", "Home");
            }*/
            else
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            return Redirect("~/Login.aspx");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Deny()
        {
            ViewBag.Message = "Access Deny.";
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            HttpCookie cookie_department = new HttpCookie("cookie_department");
            cookie_department.Value = "Super Super";
            cookie_department.Expires = DateTime.Now.AddHours(-3);
            Response.SetCookie(cookie_department);

            HttpCookie cookie_school = new HttpCookie("cookie_school");
            cookie_school.Value = "Super Super";
            cookie_school.Expires = DateTime.Now.AddHours(-3);
            Response.SetCookie(cookie_school);

            HttpCookie cookie_users = new HttpCookie("cookie_users");
            cookie_users.Value = "Welcome - Visitor";
            cookie_users.Expires = DateTime.Now.AddHours(-3);
            Response.SetCookie(cookie_users);

            HttpCookie cookie_user_name = new HttpCookie("cookie_user_name");
            cookie_user_name.Value = "Welcome - Visitor";
            cookie_user_name.Expires = DateTime.Now.AddHours(-3);
            Response.SetCookie(cookie_user_name);

            Session.Clear(); Session.Abandon();

            /* Response.Cookies["det"]["school"] = "Republic Academy. Ghana";
             Response.Cookies["det"]["department"] = "Department xx";
             Response.Cookies["det"]["user"] = "Users xx";
             Response.Cookies["det"]["role"] = "manageclass";
             Response.Cookies["det"].Expires = DateTime.Now.AddDays(3);*/

            /* Session["school"] = "Republic Academy. Ghana";
             Session["department"] = "Department xx";
             Session["user"] = "Mr. Sola Adebayo";
             Session["time"] = DateTime.Now.ToLongTimeString();
             Session["role"] = "manageclass||manageuser||managesubject||role||config||student||result||comment";

             ViewBag.message = Session["school"];*/ //"Login.";
            return Redirect("~/Login.aspx");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}