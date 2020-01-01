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
    public class ConfigurationController : Controller
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

        // GET: Configuration
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
                return View(db.table_configuration.Where(p => p.school == sch).ToList());
            }
        }

        // GET: Configuration/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_configuration table_configuration = db.table_configuration.Find(id);
            if (table_configuration == null)
            {
                return HttpNotFound();
            }
            return View(table_configuration);
        }

        // GET: Configuration/Create
        public ActionResult Create(table_configuration model,string s)
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
                model.Name_of_School = (string)Session["school"];
                model.Date = DateTime.Now.ToLongDateString();
                return View(model);
            }
        }

        // POST: Configuration/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,Name_of_School,Address,City,State,Country,Phone,Email,Date,Upload_Status,school,Website,L_U")] table_configuration model)
        {
            if (string.IsNullOrWhiteSpace(model.Name_of_School) || string.IsNullOrWhiteSpace(model.Address) || string.IsNullOrWhiteSpace(model.City) || string.IsNullOrWhiteSpace(model.State) || string.IsNullOrWhiteSpace(model.Country) || string.IsNullOrWhiteSpace(model.Phone))
            {
                ViewBag.report = "All Fields must be filled except Website and Email ...";
            }
            else
            {
                model.school = (string)Session["school"];
                model.Name_of_School = (string)Session["school"];
                model.Date = DateTime.Now.ToLongDateString();
                db.Database.ExecuteSqlCommand("INSERT INTO table_Configuration(Name_of_School,Address,City,State,Country,Phone,Email,Date,Upload_Status,School,Website)VALUES('" + model.Name_of_School + "','" + model.Address + "','" + model.City + "','" + model.State + "','" + model.Country + "','" + model.Phone + "','" + model.Email + "','" + DateTime.Now + "','Dormant','" + model.school + "','" + model.Website + "') ON DUPLICATE KEY UPDATE Name_of_school=values(name_of_school),address=values(address),city=values(city),state=values(state),country=values(country),email=values(email),date=values(date),upload_status=values(upload_status),school=values(school),website=values(website)");
                return RedirectToAction("Index");
            }
            model.school = (string)Session["school"];
            model.Name_of_School = (string)Session["school"];
            model.Date = DateTime.Now.ToLongDateString();
            return View(model);
        }

        // GET: Configuration/Edit/5
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
                table_configuration table_configuration = db.table_configuration.Find(id);
                if (table_configuration == null)
                {
                    return HttpNotFound();
                }
                return View(table_configuration);
            }
        }

        // POST: Configuration/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,Name_of_School,Address,City,State,Country,Phone,Email,Date,Upload_Status,school,Website,L_U")] table_configuration mode)
        {
            if (string.IsNullOrWhiteSpace(mode.Name_of_School) || string.IsNullOrWhiteSpace(mode.Address) || string.IsNullOrWhiteSpace(mode.City) || string.IsNullOrWhiteSpace(mode.State) || string.IsNullOrWhiteSpace(mode.Country) || string.IsNullOrWhiteSpace(mode.Phone))
            {
                ViewBag.report = "All Fields must be filled except Website and Email ...";
            }
            else
            {
                mode.school = (string)Session["school"];
                mode.Name_of_School = (string)Session["school"];
                mode.Date = DateTime.Now.ToLongDateString();
                db.Entry(mode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            mode.school = (string)Session["school"];
            mode.Name_of_School = (string)Session["school"];
            mode.Date = DateTime.Now.ToLongDateString();
            return View(mode);
        }

        // GET: Configuration/Delete/5
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
                table_configuration table_configuration = db.table_configuration.Find(id);
                if (table_configuration == null)
                {
                    return HttpNotFound();
                }
                return View(table_configuration);
            }
        }

        // POST: Configuration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            table_configuration table_configuration = db.table_configuration.Find(id);
            db.table_configuration.Remove(table_configuration);
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
