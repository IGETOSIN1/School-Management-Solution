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
    public class CommentController : Controller
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

        // GET: Comment
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
                return View(db.table_comment_term.Where(p=>p.School==sch).ToList());
            }
        }

        // GET: Comment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_comment_term table_comment_term = db.table_comment_term.Find(id);
            if (table_comment_term == null)
            {
                return HttpNotFound();
            }
            return View(table_comment_term);
        }

        // GET: Comment/Create
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

        // POST: Comment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,Student_ID,Student_Name,Session,Term,Class,Code,Remark_Teacher,Remark_Principal,No_of_time_school_open,No_of_time_student_present,Comment_Punctuality,General_Remark,Note_to_Parent,Concentration,Neatness,Comportment,Relation_with_Peer,Obedience,Punctuality,Remark_Psycomotor,Position,Date_Vacation,Date_Resumption,Date,School,L_U")] table_comment_term table_comment_term)
        {
            if (ModelState.IsValid)
            {
                db.table_comment_term.Add(table_comment_term);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(table_comment_term);
        }

        // GET: Comment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_comment_term table_comment_term = db.table_comment_term.Find(id);
            if (table_comment_term == null)
            {
                return HttpNotFound();
            }
            return View(table_comment_term);
        }

        // POST: Comment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,Student_ID,Student_Name,Session,Term,Class,Code,Remark_Teacher,Remark_Principal,No_of_time_school_open,No_of_time_student_present,Comment_Punctuality,General_Remark,Note_to_Parent,Concentration,Neatness,Comportment,Relation_with_Peer,Obedience,Punctuality,Remark_Psycomotor,Position,Date_Vacation,Date_Resumption,Date,School,L_U")] table_comment_term table_comment_term)
        {
            if (ModelState.IsValid)
            {
                db.Entry(table_comment_term).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(table_comment_term);
        }

        // GET: Comment/Delete/5
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
                table_comment_term table_comment_term = db.table_comment_term.Find(id);
                if (table_comment_term == null)
                {
                    return HttpNotFound();
                }
                return View(table_comment_term);
            }
        }

        // POST: Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            table_comment_term table_comment_term = db.table_comment_term.Find(id);
            db.table_comment_term.Remove(table_comment_term);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult fetchClass()
        {
            string sch = (string)Session["school"];
            var des = db.table_class.Where(p => p.School == sch).OrderBy(p => p.Class).Distinct().ToList();
            return Json(des, JsonRequestBehavior.AllowGet);
        }

        public string find_id(string student_name)
        {
            string asp = db.table_student.Where(p => p.Full_Name == student_name).Select(p => p.Student_ID).FirstOrDefault();
            return asp;
        }

        [HttpPost]
        public JsonResult fetchStudent(string id)
        {
            string sch = (string)Session["school"];
            var rec = db.table_student.Where(P => P.School == sch && P.Class == id).ToList();
            return Json(rec);
        }

        public string Insert_Comment(string student_name, string student_id, string comment, string school_open, string student_present, string student_absent, string category, string sessio, string term, string clas,table_comment_term model)
        {
            string resp = "";
            string reg_by = "";
            string sch = (string)Session["school"];
            string user = (string)Session["user"];
            if (category == "undefined" || sessio == "undefined" || term == "undefined" || clas == "undefined")
            {
                resp = "Category, Session, Term, and Class must be Selected ...";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(comment))
                {
                    string code = student_id + sessio + term;
                   
                    if (category.Contains("Mid"))
                    {
                        db.Database.ExecuteSqlCommand("INSERT INTO table_comment_term_mid(Student_ID,Student_Name,Session,Term,Code,Remark_Teacher,Remark_Principal,Comment_Punctuality,General_Remark,Note_To_Parent,Concentration,Neatness,Comportment,Relation_with_peer,Obedience,Punctuality,Remark_psycomotor,Position,Date_Vacation,Date_Resumption,Date,School,Class,No_of_Time_School_Open,No_of_Time_Student_Present)VALUES('" + student_id + "','" + student_name + "','" + sessio + "','" + term + "','" + code.ToString() + "','" + comment + "','" + model.Remark_Principal + "','" + model.Punctuality + "','" + model.Punctuality + "','" + model.Note_to_Parent + "','" + model.Concentration + "','" + model.Neatness + "','" + model.Comportment + "','" + model.Relation_with_Peer + "','" + model.Obedience + "','" + model.Punctuality + "','" + model.Remark_Psycomotor + "','" + student_absent + "','" + model.Date_Vacation + "','" + model.Date_Resumption + "','" + DateTime.Now + "','" + sch + "','" + clas + "','" + school_open + "','" + student_present + "') ON DUPLICATE KEY UPDATE student_id=values(student_id),student_name=values(student_name),session=values(session),term=values(term),code=values(code),remark_teacher=values(remark_teacher),remark_principal=values(remark_principal),comment_punctuality=values(comment_punctuality),general_remark=values(general_remark),note_to_parent=values(note_to_parent),concentration=values(concentration),neatness=values(neatness),comportment=values(comportment),relation_with_peer=values(relation_with_peer),obedience=values(obedience),punctuality=values(punctuality),remark_psycomotor=values(remark_psycomotor),position=values(position),date_vacation=values(date_vacation),date_resumption=values(date_resumption),date=values(date),school=values(school),class=values(class),No_of_Time_School_Open=values(No_of_Time_School_Open),No_of_Time_Student_Present=values(No_of_Time_Student_Present)");
                    }
                    else
                    {
                        db.Database.ExecuteSqlCommand("INSERT INTO table_comment_term(Student_ID,Student_Name,Session,Term,Code,Remark_Teacher,Remark_Principal,Comment_Punctuality,General_Remark,Note_To_Parent,Concentration,Neatness,Comportment,Relation_with_peer,Obedience,Punctuality,Remark_psycomotor,Position,Date_Vacation,Date_Resumption,Date,School,Class,No_of_Time_School_Open,No_of_Time_Student_Present)VALUES('" + student_id + "','" + student_name + "','" + sessio + "','" + term + "','" + code.ToString() + "','" + comment + "','" + model.Remark_Principal + "','" + model.Punctuality + "','" + model.Punctuality + "','" + model.Note_to_Parent + "','" + model.Concentration + "','" + model.Neatness + "','" + model.Comportment + "','" + model.Relation_with_Peer + "','" + model.Obedience + "','" + model.Punctuality + "','" + model.Remark_Psycomotor + "','" + student_absent + "','" + model.Date_Vacation + "','" + model.Date_Resumption + "','" + DateTime.Now + "','" + sch + "','" + clas + "','" + school_open + "','" + student_present + "') ON DUPLICATE KEY UPDATE student_id=values(student_id),student_name=values(student_name),session=values(session),term=values(term),code=values(code),remark_teacher=values(remark_teacher),remark_principal=values(remark_principal),comment_punctuality=values(comment_punctuality),general_remark=values(general_remark),note_to_parent=values(note_to_parent),concentration=values(concentration),neatness=values(neatness),comportment=values(comportment),relation_with_peer=values(relation_with_peer),obedience=values(obedience),punctuality=values(punctuality),remark_psycomotor=values(remark_psycomotor),position=values(position),date_vacation=values(date_vacation),date_resumption=values(date_resumption),date=values(date),school=values(school),class=values(class),No_of_Time_School_Open=values(No_of_Time_School_Open),No_of_Time_Student_Present=values(No_of_Time_Student_Present)");
                    }
                    resp = "Comment Successfully Saved for " + student_name + " with Student ID:" + student_id;
                }
                else
                {
                    resp = "You Must Select Student's Name, and enter comment to save ...";
                }

            }
            return resp;
        }

        public string insert_trait(string student_name, string student_id, string trait, string remark, string category, string sessio, string term, string clas, table_trait model)
        {
            string resp = "";
            string reg_by = "";
            string sch = (string)Session["school"];
            string user = (string)Session["user"];
            if (category == "undefined" || sessio == "undefined" || term == "undefined" || clas == "undefined")
            {
                resp = "Category, Session, Term, and Class must be Selected ...";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(trait) && !string.IsNullOrWhiteSpace(remark))
                {
                    string code = student_id + sessio + term + trait;
                    string table = "";
                    if (category.Contains("Mid"))
                    {
                        table = "table_trait_mid";
                    }
                    else
                    {
                        table = "table_trait";
                    }

                    db.Database.ExecuteSqlCommand("DELETE FROM " + table + " WHERE Code = '" + code.ToString() + "'");

                    if (remark == "Excellent")
                    {
                        string query = "INSERT INTO " + table + "(Student_ID,Student_Name,Qualities,Excellent,Session,Term,School,Date,Code,Class)VALUES('" + student_id + "','" + student_name + "','" + trait + "','~/images/click.png','" + sessio + "','" + term + "','" + sch + "','" + DateTime.Now + "','" + code.ToString() + "','" + clas + "')";
                        db.Database.ExecuteSqlCommand(query);
                    }
                    else if (remark == "Good")
                    {
                        string query = "INSERT INTO " + table + "(Student_ID,Student_Name,Qualities,Good,Session,Term,School,Date,Code,Class)VALUES('" + student_id + "','" + student_name + "','" + trait + "','~/images/click.png','" + sessio + "','" + term + "','" + sch + "','" + DateTime.Now + "','" + code.ToString() + "','" + clas + "')";
                        db.Database.ExecuteSqlCommand(query);
                    }
                    else if (remark == "Fair")
                    {
                        string query = "INSERT INTO " + table + "(Student_ID,Student_Name,Qualities,Fair,Session,Term,School,Date,Code,Class)VALUES('" + student_id + "','" + student_name + "','" + trait + "','~/images/click.png','" + sessio + "','" + term + "','" + sch + "','" + DateTime.Now + "','" + code.ToString() + "','" + clas + "')";
                        db.Database.ExecuteSqlCommand(query);
                    }
                    else if (remark == "Poor")
                    {
                        string query = "INSERT INTO " + table + "(Student_ID,Student_Name,Qualities,Poor,Session,Term,School,Date,Code,Class)VALUES('" + student_id + "','" + student_name + "','" + trait + "','~/images/click.png','" + sessio + "','" + term + "','" + sch + "','" + DateTime.Now + "','" + code.ToString() + "','" + clas + "')";
                        db.Database.ExecuteSqlCommand(query);
                    }
                    // resp = remark;
                    resp = "Psychomotor Successfully Saved for " + student_name + " with Student ID:" + student_id;
                }
                else
                {
                    resp = "You Must Select Student's Name,Select Psychomotor, as well as Rating to save ...";
                }

            }
            return resp;
        }

        [HttpPost]
        public JsonResult search_comment(string category, string sessio, string term, string student_name)
        {
            string sch = (string)Session["school"];
            string table = "";
            if (category.Contains("Mid"))
            {
                table = "table_comment_term_mid";
            }
            else
            {
                table = "table_comment_term";
            }
            var rec = db.table_comment_term.SqlQuery("SELECT * FROM " + table + " where session='" + sessio + "' and term='" + term + "' and student_name='" + student_name + "'").FirstOrDefault<table_comment_term>();
            return Json(rec);
        }

        [HttpPost]
        public JsonResult searchComment(string category, string sessio, string term, string clas)
        {
            string sch = (string)Session["school"];
            string table = "";
            if (category.Contains("Mid"))
            {
                table = "table_comment_term_mid";
            }
            else
            {
                table = "table_comment_term";
            }
            var rec = db.table_comment_term.SqlQuery("SELECT * FROM " + table + " where session='" + sessio + "' and term='" + term + "' and class='" + clas + "'").ToList<table_comment_term>();
            return Json(rec);
        }

        [HttpPost]
        public JsonResult searchComment_Name(string category, string sessio, string term, string clas, string student_name)
        {
            string sch = (string)Session["school"];
            string table = "";
            if (category.Contains("Mid"))
            {
                table = "table_comment_term_mid";
            }
            else
            {
                table = "table_comment_term";
            }
            var rec = db.table_comment_term.SqlQuery("SELECT * FROM " + table + " where session='" + sessio + "' and term='" + term + "' and class='" + clas + "' and student_name='" + student_name + "'").ToList<table_comment_term>();
            return Json(rec);
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
