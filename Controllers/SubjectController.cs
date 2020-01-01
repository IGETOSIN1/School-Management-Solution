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
    public class SubjectController : Controller
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
                if (Session["role"].ToString().Contains("subject") || Session["cat_status"].ToString().Contains("Super Sup"))
                {
                    v2 = true;
                }
                else
                {
                    v2 = false;
                }
        }

        // GET: Subject
        public ActionResult Index()
        {
            return View(db.table_subject.ToList());
        }

        // GET: Subject/Details/5
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
                table_subject table_subject = db.table_subject.Find(id);
                if (table_subject == null)
                {
                    return HttpNotFound();
                }
                return View(table_subject);
            }
            
        }

        // GET: Subject/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subject/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,Subject,Date,School,Code")] table_subject model)
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
                    if (string.IsNullOrWhiteSpace(model.Subject))
                    {
                        ViewBag.report = "Enter Name of Subject to Add/ Register";
                    }
                    else
                    {
                        model.Code = model.Subject + (string)Session["school"];
                        var a = db.table_subject.Where(p => p.Code == model.Code).FirstOrDefault();
                        if (a != null)
                        {
                            ViewBag.report = "Subject already added/ registered";
                        }
                        else
                        {
                            model.Date = DateTime.Now.ToLongDateString();
                            model.School = (string)Session["school"];
                            string code1 = model.Subject + (string)Session["school"];
                            db.table_subject.Add(model);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }

                    }
                }
            }

            return View(model);
        }

        // GET: Subject/Edit/5
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
                table_subject table_subject = db.table_subject.Find(id);
                if (table_subject == null)
                {
                    return HttpNotFound();
                }
                return View(table_subject);
            }
        }

        // POST: Subject/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,Subject,Date,School,Code")] table_subject model)
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
            else if (string.IsNullOrWhiteSpace(model.Subject))
            {
                ViewBag.report = "Enter the name of Subject to add";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    // db.Entry(model).State = EntityState.Modified;
                    // db.SaveChanges();
                    db.Database.ExecuteSqlCommand("UPDATE Table_subject set subject='" + model.Subject + "' where p_id='" + model.p_id + "'");
                    return RedirectToAction("Index");
                }     
            }
            table_subject table_subject = db.table_subject.Find(model.p_id);
            if (table_subject == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        

        // GET: Subject/Delete/5
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
                table_subject table_subject = db.table_subject.Find(id);
                if (table_subject == null)
                {
                    return HttpNotFound();
                }
                return View(table_subject);
            }
        }

        // POST: Subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            table_subject table_subject = db.table_subject.Find(id);
            db.table_subject.Remove(table_subject);
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
