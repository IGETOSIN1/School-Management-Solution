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
    public class ConfigureScoreController : Controller
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
                if (Session["role"].ToString().Contains("config") || Session["cat_status"].ToString().Contains("Super Sup"))
                {
                    v2 = true;
                }
                else
                {
                    v2 = false;
                }
        }

        // GET: ConfigureScore
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
                string sch = (string)Session["school"];
                return View(db.table_mark_range.Where(p => p.School == sch).ToList());
            }
        }

        // GET: ConfigureScore/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_mark_range table_mark_range = db.table_mark_range.Find(id);
            if (table_mark_range == null)
            {
                return HttpNotFound();
            }
            return View(table_mark_range);
        }

        // GET: ConfigureScore/Create
        public ActionResult Create(table_mark_range model,string s)
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
                model.School = (string)Session["school"];
                model.Date = DateTime.Now.ToLongDateString();
                return View(model);
            }
        }

        // POST: ConfigureScore/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,First_Ca,Second_Ca,Exam,School,Code,Date,L_U")] table_mark_range model)
        {
            if (string.IsNullOrWhiteSpace(model.First_Ca.ToString()))
            {
                ViewBag.report = "Enter First CA. Test Total Mark ...";
            }
            else if (string.IsNullOrWhiteSpace(model.Second_Ca.ToString()))
            {
                ViewBag.report = "Enter Second CA. Test Total Mark ...";
            }
            else if (string.IsNullOrWhiteSpace(model.Exam.ToString()))
            {
                ViewBag.report = "Enter Examination Total Mark ...";
            }
            else
            {
                model.School = (string)Session["school"];
                model.Date = DateTime.Now.ToLongDateString();
                model.Code = model.School;
                //  db.table_mark_range.Add(model);
                // db.SaveChanges();
                db.Database.ExecuteSqlCommand("INSERT INTO table_mark_range(first_ca,second_ca,exam,code,school,Date)VALUES('" + model.First_Ca + "','" + model.Second_Ca + "','" + model.Exam + "','" + model.Code + "','" + model.School + "','" + model.Date + "') ON DUPLICATE KEY UPDATE first_ca=values(first_ca),second_ca=values(second_ca),exam=values(exam),school=values(school),code=values(code),date=values(date)");
                return RedirectToAction("Index");
            }
            model.School = (string)Session["school"];
            model.Date = DateTime.Now.ToLongDateString();
            return View(model);
        }

        // GET: ConfigureScore/Edit/5
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
                table_mark_range table_mark_range = db.table_mark_range.Find(id);
                if (table_mark_range == null)
                {
                    return HttpNotFound();
                }
                return View(table_mark_range);
            }
        }

        // POST: ConfigureScore/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,First_Ca,Second_Ca,Exam,School,Code,Date,L_U")] table_mark_range model)
        {
            if (string.IsNullOrWhiteSpace(model.First_Ca.ToString()))
            {
                ViewBag.report = "Enter First CA. Test Total Mark ...";
            }
            else if (string.IsNullOrWhiteSpace(model.Second_Ca.ToString()))
            {
                ViewBag.report = "Enter Second CA. Test Total Mark ...";
            }
            else if (string.IsNullOrWhiteSpace(model.Exam.ToString()))
            {
                ViewBag.report = "Enter Examination Total Mark ...";
            }
            else
            {
                model.School = (string)Session["school"];
                model.Date = DateTime.Now.ToLongDateString();
                model.Code = model.School;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            model.School = (string)Session["school"];
            model.Date = DateTime.Now.ToLongDateString();
            return View(model);
        }

        // GET: ConfigureScore/Delete/5
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
                table_mark_range table_mark_range = db.table_mark_range.Find(id);
                if (table_mark_range == null)
                {
                    return HttpNotFound();
                }
                return View(table_mark_range);
            }
        }

        // POST: ConfigureScore/Delete/5
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
                table_mark_range table_mark_range = db.table_mark_range.Find(id);
                db.table_mark_range.Remove(table_mark_range);
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
