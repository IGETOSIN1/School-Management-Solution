using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WEB_SCONET_MANAGEMENT;

namespace WEB_SCONET_MANAGEMENT.Controllers
{
    public class UsersController : Controller
    {
        private Entities_School db = new Entities_School();
        bool v1, v2 = false;

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
            if(v1==true)
            if (Session["role"].ToString().Contains("user") || Session["cat_status"].ToString().Contains("Super Sup"))
            {
                v2 = true;
            }
            else
            {
                v2 = false;
            }
        }
        
        public List<SelectListItem> display_status()
        {
            string ses = (string)Session["role"];
            if (ses == "Super Super")
            {
                return new List<SelectListItem>() {
                new SelectListItem { Text="Super Super",Value="Super Super"},
                new SelectListItem { Text="Super User",Value="Super User"},
                new SelectListItem { Text="Secretary/ Bursar",Value="Secretary/ Bursar"},
                new SelectListItem { Text="System Admin",Value="System Admin"},
                new SelectListItem { Text="Teacher",Value="Teacher"},};
            }
            else
            {
                return new List<SelectListItem>() {
                new SelectListItem { Text="Super User",Value="Super User"},
                new SelectListItem { Text="Secretary/ Bursar",Value="Secretary/ Bursar"},
                new SelectListItem { Text="System Admin",Value="System Admin"},
                new SelectListItem { Text="Teacher",Value="Teacher"},};
            }
           // return cat_status1;
        }

        public SelectList display_staff()
        {
            string a1 = (string)Session["school"];
            return new SelectList(db.table_staff.Where(p => p.School == a1).Distinct().OrderBy(p => p.Full_Name), "Full_Name", "Full_Name");
        }

        public SelectList display_school()
        {
            string a1 = (string)Session["school"];
            string ses = (string)Session["role"];
            if (ses == "Super Super")
            {
                return new SelectList(db.table_schools.Distinct().OrderBy(p => p.School).Select(p=>p.School).ToList());
            }
            else
            {
                return new SelectList(db.table_schools.Distinct().OrderBy(p => p.School).Where(p=>p.School==a1).Select(p => p.School).ToList());
            }
        }

        // GET: Users
        public ActionResult Index()
        {
            verify();
            if (v1 == false)
            {
                return RedirectToAction("Login", "Home");
            }
            else if (v2 == false)
            {
                return RedirectToAction("Deny", "Home");
            }
            else
            {
                string est = (string)Session["school"];
                return View(db.table_user.Where(p => p.School == est).OrderBy(p => p.User_Name).ToList());
            }
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            verify();
            if (v1 == false)
            {
                return RedirectToAction("Login", "Home");
            }
            else if (v2 == false)
            {
                return RedirectToAction("Deny", "Home");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                table_user table_user = db.table_user.Find(id);
                if (table_user == null)
                {
                    return HttpNotFound();
                }
                return View(table_user);
            }
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            verify();
            if (v1==false)
            {
                return RedirectToAction("Login", "Home");
            }
            else if (v2==false)
            {
                return RedirectToAction("Deny", "Home");
            }
            else
            {
                ViewBag.Category_Status = display_status();
                ViewBag.Full_Name = display_staff();
                ViewBag.Schoo = display_school();
                return View();
            }
           
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(table_user model)
        {
            verify();
            if (v1 == false)
            {
                return RedirectToAction("Login", "Home");
            }
            else if (v2 == false)
            {
                return RedirectToAction("Deny", "Home");
            }
            else
            {

                string a = Session["school"].ToString();
                if (ModelState.IsValid)
                {
                    if (model.Full_Name == "Select")
                    {
                        ViewBag.report = "Select Name of Staff ...";
                    }
                    else if (string.IsNullOrWhiteSpace(model.Category_Status))
                    {
                        ViewBag.report = "Select Status";
                    }
                    else if (string.IsNullOrWhiteSpace(model.User_Name))
                    {
                        ViewBag.report = "Enter User Name ...";
                    }
                    else if (string.IsNullOrWhiteSpace(model.Password))
                    {
                        ViewBag.report = "Enter Password ...";
                    }
                    /* else if (string.IsNullOrWhiteSpace(model.confirmpassword))
                     {
                         ViewBag.report = "Confirm Password ...";
                     }
                     else if (model.Password != model.confirmpassword)
                     {
                         ViewBag.report = "Password Does Not Match ...";
                     }*/
                    else
                    {
                        model.Surname = model.Full_Name;
                        model.Other_Name = model.Full_Name;
                        model.School = Session["school"].ToString();
                        model.Department = Session["department"].ToString();
                        model.Registered_By = Session["user"].ToString();
                        model.Date = DateTime.Now.ToLongDateString();

                        var check = db.table_user.Where(p => p.User_Name == model.User_Name).FirstOrDefault();
                        if (check != null)
                        {
                            ViewBag.report = "User Name already Exist ...";

                            return View(model);
                        }
                        else
                        {
                            db.table_user.Add(model);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                }

                ViewBag.Category_Status = display_status();
                ViewBag.Full_Name = display_staff();
                ViewBag.Schoo = display_school();
                return View(model);
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            verify();
            if (v1 == false)
            {
                return RedirectToAction("Login", "Home");
            }
            else if (v2 == false)
            {
                return RedirectToAction("Deny", "Home");
            }
            else
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                table_user table_user = db.table_user.Find(id);
                if (table_user == null)
                {
                    return HttpNotFound();
                }
                return View(table_user);
            }
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,Surname,Other_Name,Full_Name,School,Department,Category_Status,User_Name,Password,Registered_By,Date")] table_user table_user, table_user model)
        {
            verify();
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(model.Password))
                {
                    ViewBag.report = "Enter New Password ...";
                }
                else
                {
                    int noOfRowUpdated = db.Database.ExecuteSqlCommand("Update table_user set FULL_name = '" + model.Full_Name + "',Password='" + model.Password + "' where USER_NAME = '" + model.User_Name + "'");
                    return RedirectToAction("Index");
                }
                
            }
            table_user tab = db.table_user.Find(model.p_id);
            if (table_user == null)
            {
                return HttpNotFound();
            }
            return View(tab);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            verify();
            if (v1 == false)
            {
                return RedirectToAction("Login", "Home");
            }
            else if (v2 == false)
            {
                return RedirectToAction("Deny", "Home");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                table_user table_user = db.table_user.Find(id);
                if (table_user == null)
                {
                    return HttpNotFound();
                }
                return View(table_user);
            }
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            table_user table_user = db.table_user.Find(id);
            db.table_user.Remove(table_user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
