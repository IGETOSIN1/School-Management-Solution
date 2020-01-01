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
    public class ClassController : Controller
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
                if (Session["role"].ToString().Contains("user") || Session["cat_status"].ToString().Contains("Super Sup"))
                {
                    v2 = true;
                }
                else
                {
                    v2 = false;
                }
        }
        // GET: Class
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
                string kid = (string)Session["school"];
                var drafts = db.table_class.Where(d => d.School == kid).OrderBy(d => d.Class).ToList();
                return View(drafts);
            }
        }

        // GET: Class/Details/5
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
                table_class table_class = db.table_class.Find(id);
                if (table_class == null)
                {
                    return HttpNotFound();
                }
                return View(table_class);
            }
        }

        // GET: Class/Create
        public ActionResult Create()
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
                return View();
            }
        }

        // POST: Class/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,Class,Date,School,Code")] table_class model)
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
                if (string.IsNullOrWhiteSpace(model.Class))
                {
                    ViewBag.report = "Enter Name for the New Class ...";
                }
                else
                {
                    var check = db.table_class.Where(p => p.Class == model.Class).FirstOrDefault();
                    if (check != null)
                    {
                        ViewBag.report = model.Class + " had already been added ...";
                        return View(model);
                    }
                    else
                    {
                        model.Code = model.Class + (string)Session["school"];
                        model.School = (string)Session["school"];
                        model.Date = DateTime.Now.ToShortDateString();
                        db.table_class.Add(model);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(model);
        }

        public string dest(table_class model)
        {
            db.table_class.Add(model);
            db.SaveChanges();
            return "Registration was successfull ...";
        }

        // GET: Class/Edit/5
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
                table_class table_class = db.table_class.Find(id);
                if (table_class == null)
                {
                    return HttpNotFound();
                }
                return View(table_class);
            }
        }

        // POST: Class/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,Class,Date,School,Code")] table_class model)
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
                if (ModelState.IsValid)
                {
                    //db.Entry(table_class).State = EntityState.Modified;
                    //db.SaveChanges();
                    db.Database.ExecuteSqlCommand("UPDATE Table_Class set class='" + model.Class + "' where p_id='" + model.p_id + "'");
                    return RedirectToAction("Index");
                }
                return View(model);
            }
        }

        // GET: Class/Delete/5
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
                table_class table_class = db.table_class.Find(id);
                if (table_class == null)
                {
                    return HttpNotFound();
                }
                return View(table_class);
            }
        }

        // POST: Class/Delete/5
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
                table_class table_class = db.table_class.Find(id);
                db.table_class.Remove(table_class);
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
