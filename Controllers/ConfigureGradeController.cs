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
    public class ConfigureGradeController : Controller
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

        public List<SelectListItem> display_remark()
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem { Text="Excellent",Value="Excellent"},
                new SelectListItem { Text="Very Good",Value="Very Good"},
                new SelectListItem { Text="Good",Value="Good"},
                new SelectListItem { Text="Credit",Value="Credit"},
                new SelectListItem { Text="Fair",Value="Fair"},
                new SelectListItem { Text="Weak",Value="Weak"},
            };
            return list;
        }
        // GET: ConfigureGrade
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
                return View(db.table_grade_range.Where(p => p.school == sch).ToList());
            }
        }

        // GET: ConfigureGrade/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_grade_range table_grade_range = db.table_grade_range.Find(id);
            if (table_grade_range == null)
            {
                return HttpNotFound();
            }
            return View(table_grade_range);
        }

        // GET: ConfigureGrade/Create
        public ActionResult Create(table_grade_range model,string s)
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
                ViewBag.remar = display_remark();
                model.school = (string)Session["school"];
                model.To1 = 0;
                model.From1 = 0;
                model.Date = DateTime.Now.ToLongDateString();
                return View(model);
            }
            
        }

        // POST: ConfigureGrade/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,From1,To1,Grade,Remark,Code,school,Reg_By,Date,L_U")] table_grade_range model)
        {
            if (string.IsNullOrWhiteSpace(model.From1.ToString()) || model.From1 > 100 || model.From1 < 0)
            {
                ViewBag.report = "From Field is Required and must be between 0 and 100 ...";
            }
            else if (string.IsNullOrWhiteSpace(model.To1.ToString()) || model.To1 > 100 || model.To1 < 0)
            {
                ViewBag.report = "To Field is Required and must be between 0 and 100 ...";
            }
            else if (string.IsNullOrWhiteSpace(model.Grade))
            {
                ViewBag.report = "Grade is Required ...";
            }
            else if (model.Remark == "Select" || string.IsNullOrWhiteSpace(model.Remark))
            {
                ViewBag.report = "Select remark that will be equivalent to the grade ...";
            }
            else
            {
               // ViewBag.remar = display_remark();
                model.school = (string)Session["school"];
                model.Date = DateTime.Now.ToLongDateString();
                model.Code = model.From1 + model.To1 + model.school;
                db.Database.ExecuteSqlCommand("INSERT INTO table_grade_range(from1,to1,grade,remark,code,School,Date)VALUES('" + model.From1 + "','" + model.To1 + "','" + model.Grade + "','" + model.Remark + "','" + model.Code + "','" + model.school + "','" + model.Date + "') ON DUPLICATE KEY UPDATE from1=values(from1),to1=values(to1),grade=values(grade),remark=values(remark),school=values(school),code=values(code),date=values(date)");
               // db.table_grade_range.Add(model);
               // db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.remar = display_remark();
            model.school = (string)Session["school"];
            model.Date = DateTime.Now.ToLongDateString();
            db.table_grade_range.Add(model);
            return View(model);
        }

        // GET: ConfigureGrade/Edit/5
        public ActionResult Edit(int? id,table_grade_range model)
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
                ViewBag.remar = display_remark();
                model.school = (string)Session["school"];
                model.Date = DateTime.Now.ToLongDateString();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                table_grade_range table_grade_range = db.table_grade_range.Find(id);
                if (table_grade_range == null)
                {
                    return HttpNotFound();
                }
                return View(table_grade_range);
            }
        }

        // POST: ConfigureGrade/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,From1,To1,Grade,Remark,Code,school,Reg_By,Date,L_U")] table_grade_range model)
        {
            if (string.IsNullOrWhiteSpace(model.From1.ToString()) || model.From1 > 100 || model.From1 < 0)
            {
                ViewBag.report = "From Field is Required and must be between 0 and 100 ...";
            }
            else if (string.IsNullOrWhiteSpace(model.To1.ToString()) || model.To1 > 100 || model.To1 < 0)
            {
                ViewBag.report = "To Field is Required and must be between 0 and 100 ...";
            }
            else if (string.IsNullOrWhiteSpace(model.Grade))
            {
                ViewBag.report = "Grade is Required ...";
            }
            else if (model.Remark == "Select" || string.IsNullOrWhiteSpace(model.Remark))
            {
                ViewBag.report = "Select remark that will be equivalent to the grade ...";
            }
            else
            {
               // ViewBag.remar = display_remark();
                model.school = (string)Session["school"];
                model.Date = DateTime.Now.ToLongDateString();
                model.Code = model.From1 + model.To1 + model.school;
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.remar = display_remark();
            model.school = (string)Session["school"];
            model.Date = DateTime.Now.ToLongDateString();
            return View(model);
        }

        // GET: ConfigureGrade/Delete/5
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
                table_grade_range table_grade_range = db.table_grade_range.Find(id);
                if (table_grade_range == null)
                {
                    return HttpNotFound();
                }
                return View(table_grade_range);
            }
        }

        // POST: ConfigureGrade/Delete/5
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
                table_grade_range table_grade_range = db.table_grade_range.Find(id);
                db.table_grade_range.Remove(table_grade_range);
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
