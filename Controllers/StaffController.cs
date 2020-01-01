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
    public class StaffController : Controller
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
            if (v1 == true)
                if (Session["role"].ToString().Contains("staff") || Session["cat_status"].ToString().Contains("Super Sup"))
                {
                    v2 = true;
                }
                else
                {
                    v2 = false;
                }
        }

        public List<SelectListItem> display_title()
        {
            return new List<SelectListItem>() {
                new SelectListItem { Text="Mr. ",Value="Mr. "},
                new SelectListItem { Text="Mrs. ",Value="Mr. "},
                new SelectListItem { Text="Miss. ",Value="Miss. "},
                new SelectListItem { Text="Dr. ",Value="Dr. "},
                new SelectListItem { Text="Prof. ",Value="Prof. "},};
            // return cat_status1;
        }
        public List<SelectListItem> display_position()
        {
            List<SelectListItem> lis = new List<SelectListItem>()
            {
                new SelectListItem {Text="Teaching Staff",Value="Teaching Staff" },
                new SelectListItem {Text="Non-Teaching Staff",Value="Non-Teaching Staff" },
                new SelectListItem {Text="Guest/ External Supervisor",Value="Guest/ External Supervisor" },
            };
            return lis;
        }
        // GET: Staff
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
                string asd = (string)Session["school"];
                return View(db.table_staff.Where(p => p.School == asd).OrderBy(p => p.School).ToList());
            }
        }

        // GET: Staff/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_staff table_staff = db.table_staff.Find(id);
            if (table_staff == null)
            {
                return HttpNotFound();
            }
            return View(table_staff);
        }

        // GET: Staff/Create
        public ActionResult Create()
        {
            ViewBag.tit = display_title();
            ViewBag.posit = display_position();
            return View();
        }

        // POST: Staff/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,Title,Surname,Other_Name,Full_Name,Date,School,Position,Code,Phone,Email")] table_staff model)
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
                if (model.Title == "Select")
                {
                    ViewBag.report = "Select Title ...";
                }
                else if (model.Position == "Select")
                {
                    ViewBag.report = "Select Position ...";
                }
                else if (string.IsNullOrWhiteSpace(model.Surname))
                {
                    ViewBag.report = "Enter Surname/ Last Name ...";
                }
                else if (string.IsNullOrWhiteSpace(model.Other_Name))
                {
                    ViewBag.report = "Enter Other Name/ First Name ...";
                }
                else
                {
                    model.Full_Name = model.Title + " " + model.Other_Name + " " + model.Surname;
                    model.School = (string)Session["school"];
                    model.Date = DateTime.Now.ToLongDateString();
                    model.Code = model.Title + " " + model.Other_Name + " " + model.Surname + (string)Session["school"];
                    var check = db.table_staff.Where(p => p.Code == model.Code).FirstOrDefault();
                    if (check != null)
                    {
                        ViewBag.report = model.Full_Name + " had already been added/ registered for " + model.School;
                    }
                    else
                    {
                        db.table_staff.Add(model);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                ViewBag.tit = display_title();
                ViewBag.posit = display_position();
                return View(model);
            }
        }

        // GET: Staff/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_staff table_staff = db.table_staff.Find(id);
            if (table_staff == null)
            {
                return HttpNotFound();
            }
            return View(table_staff);
        }

        // POST: Staff/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,Title,Surname,Other_Name,Full_Name,Date,School,Position,Code,Phone,Email")] table_staff model)
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
                if (string.IsNullOrWhiteSpace(model.Title))
                {
                    ViewBag.report = "Enter Title ...";
                }
                else if (string.IsNullOrWhiteSpace(model.Position))
                {
                    ViewBag.report = "Enter Position ...";
                }
                else if (string.IsNullOrWhiteSpace(model.Surname))
                {
                    ViewBag.report = "Enter Surname/ Last Name ...";
                }
                else if (string.IsNullOrWhiteSpace(model.Other_Name))
                {
                    ViewBag.report = "Enter Other Name/ First Name ...";
                }
                else
                {
                    model.Full_Name = model.Title + " " + model.Other_Name + " " + model.Surname;
                    model.School = (string)Session["school"];
                    model.Date = DateTime.Now.ToLongDateString();
                    model.Code = model.Title + " " + model.Other_Name + " " + model.Surname + (string)Session["school"];
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    // model.Full_Name = model.Title + " " + model.Other_Name + " " + model.Surname;
                    // db.Database.ExecuteSqlCommand("UPDATE table_staff set full_name='" + model.Full_Name + "',title='" + model.Title + "',surname='" + model.Surname + "',other_name='" + model.Other_Name + "',email='" + model.Email + "',phone='" + model.Phone + "' where p_id='" + model.p_id + "'");
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }

        // GET: Staff/Delete/5
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
                table_staff table_staff = db.table_staff.Find(id);
                if (table_staff == null)
                {
                    return HttpNotFound();
                }
                return View(table_staff);
            }
        }

        // POST: Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
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
                table_staff table_staff = db.table_staff.Find(id);
                db.table_staff.Remove(table_staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
