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
    public class Default_CommentController : Controller
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

        // GET: Default_Comment
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
                return View(db.table_default_comment.Where(p=>p.school==sch).ToList());
            }
        }

        // GET: Default_Comment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_default_comment table_default_comment = db.table_default_comment.Find(id);
            if (table_default_comment == null)
            {
                return HttpNotFound();
            }
            return View(table_default_comment);
        }

        // GET: Default_Comment/Create
        public ActionResult Create(table_default_comment model,string s)
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
                model.school = (string)Session["school"];
                model.Reg_By = (string)Session["user"];
                model.Date = DateTime.Now.ToLongDateString();
                return View(model);
            }
        }

        // POST: Default_Comment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,From1,To1,Remark,Code,school,Reg_By,Date,L_U")] table_default_comment model)
        {
            if(string.IsNullOrWhiteSpace(model.From1.ToString()) || string.IsNullOrWhiteSpace(model.To1.ToString()) || string.IsNullOrWhiteSpace(model.school))
            {
                ViewBag.report = "To, From, and Default Comment Fields must be entered ...";
            }
            else
            {
                model.school = (string)Session["school"];
                model.Reg_By = (string)Session["user"];
                model.Date = DateTime.Now.ToLongDateString();
                model.Code = model.To1 + model.From1 + model.school;
                db.Database.ExecuteSqlCommand("INSERT INTO Table_default_comment(from1,to1,remark,code,School,Date)VALUES('" + model.From1 + "','" + model.To1 + "','" + model.Remark + "','" + model.Code + "','" + model.school + "','" + model.Date + "') ON DUPLICATE KEY UPDATE from1=values(from1),to1=values(to1),remark=values(remark),school=values(school),code=values(code),date=values(date)");
                //db.table_default_comment.Add(model);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Default_Comment/Edit/5
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
                table_default_comment table_default_comment = db.table_default_comment.Find(id);
                if (table_default_comment == null)
                {
                    return HttpNotFound();
                }
                return View(table_default_comment);
            }
        }

        // POST: Default_Comment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,From1,To1,Remark,Code,school,Reg_By,Date,L_U")] table_default_comment model)
        {
            if (string.IsNullOrWhiteSpace(model.From1.ToString()) || string.IsNullOrWhiteSpace(model.To1.ToString()) || string.IsNullOrWhiteSpace(model.school))
            {
                ViewBag.report = "To, From, and Default Comment Fields must be entered ...";
            }
            else
            {
                model.school = (string)Session["school"];
                model.Reg_By = (string)Session["user"];
                model.Date = DateTime.Now.ToLongDateString();
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            model.school = (string)Session["school"];
            model.Reg_By = (string)Session["user"];
            model.Date = DateTime.Now.ToLongDateString();
            return View(model);
        }

        // GET: Default_Comment/Delete/5
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
                table_default_comment table_default_comment = db.table_default_comment.Find(id);
                if (table_default_comment == null)
                {
                    return HttpNotFound();
                }
                return View(table_default_comment);
            }
        }

        // POST: Default_Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            table_default_comment table_default_comment = db.table_default_comment.Find(id);
            db.table_default_comment.Remove(table_default_comment);
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
