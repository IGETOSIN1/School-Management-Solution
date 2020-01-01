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
    public class ResultController : Controller
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
                if (Session["role"].ToString().Contains("result") || Session["cat_status"].ToString().Contains("Super Sup"))
                {
                    v2 = true;
                }
                else
                {
                    v2 = false;
                }
        }

        public SelectList display_student_name()
        {
            string sch = (string)Session["school"];
            SelectList lis = new SelectList(db.table_student.OrderBy(m=>m.Full_Name).Where(p=>p.School==sch).Select(p => p.Full_Name).Distinct().ToList());
            return lis;
        }
        public SelectList display_subject()
        {
            string sch = (string)Session["school"];
            SelectList lis = new SelectList(db.table_subject.OrderBy(m => m.Subject).Where(p => p.School == sch).Select(p => p.Subject).Distinct().ToList());
            return lis;
        }

        public SelectList display_class()
        {
            string sch = (string)Session["school"];
            SelectList lis = new SelectList(db.table_class.OrderBy(m => m.Class).Where(p => p.School == sch).Select(p => p.Class).Distinct().ToList());
            return lis;
        }

        public List<SelectListItem> display_session()
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem {Text="2018/2019",Value="2018/2019" },
                new SelectListItem {Text="2019/2020",Value="2019/2020" },
                new SelectListItem {Text="2020/2021",Value="2020/2021" },
                new SelectListItem {Text="2021/2022",Value="2021/2022" },
            };
            return list;
        }

        public List<SelectListItem> display_term()
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem {Text="First",Value="First" },
                new SelectListItem {Text="Second",Value="Second" },
                new SelectListItem {Text="Third",Value="Third" },
            };
            return list;
        }

        public List<SelectListItem> display_category()
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem {Text="Terminal",Value="Terminal" },
                new SelectListItem {Text="Mid-Term",Value="Mid-Term" },
            };
            return list;
        }

        // GET: Result
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
                return View(db.table_result.Where(p=>p.School==sch).ToList());
            }
        }

        // GET: Result/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_result table_result = db.table_result.Find(id);
            if (table_result == null)
            {
                return HttpNotFound();
            }
            return View(table_result);
        }

        // GET: Result/Create
        public ActionResult Create(table_result model)
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
                
                ViewBag.clas = display_class();
                ViewBag.categor = display_category();
                ViewBag.student_nam = display_student_name();
                ViewBag.sessio = display_session();
                ViewBag.ter = display_term();
                ViewBag.subjec = display_subject();
                string sch = (string)Session["school"];
                string rb = (string)Session["user"];
                model.School = sch;
                model.Registered_By = rb;
                table_mark_range mode = db.table_mark_range.Where(p => p.School == sch).FirstOrDefault();
                f_ca_1 = Convert.ToDecimal(mode.First_Ca);
                f_ca_2 = Convert.ToDecimal(mode.Second_Ca);
                exam = Convert.ToDecimal(mode.Exam);
                return View(model);
            }
        }

        // POST: Result/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,Student_ID,Student_Name,Session,Term,Subject,Code,Score_CA,Score_Exam,Score_Total,Grade,Remark,Date,Registered_By,Class,School,Teacher_Signature,Score_CA1,Position,Status,Pre_Comment,L_U")] table_result model,FormCollection fmc)
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
                ViewBag.sessio = display_session();
                ViewBag.ter = display_term();
                ViewBag.clas = display_class();
                ViewBag.categor = display_category();
                ViewBag.subjec = display_subject();
                ViewBag.student_nam = display_student_name();
                string sch = (string)Session["school"];
                string rb = (string)Session["user"];
                model.School = sch;
                model.Registered_By = rb;
                table_mark_range mode = db.table_mark_range.Where(p => p.School == sch).FirstOrDefault();
                model.Status = mode.First_Ca.ToString();
                model.Pre_Comment = mode.Second_Ca.ToString();
                model.Code = mode.Exam.ToString();
                f_ca_1 = Convert.ToDecimal(mode.First_Ca);
                f_ca_2 = Convert.ToDecimal(mode.Second_Ca);
                exam = Convert.ToDecimal(mode.Exam);
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult Input_Result_Group(table_result model)
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
                ViewBag.sessio = display_session();
                ViewBag.ter = display_term();
                ViewBag.clas = display_class();
                ViewBag.categor = display_category();
                ViewBag.subjec = display_subject();
                ViewBag.student_nam = display_student_name();
                string sch = (string)Session["school"];
                string rb = (string)Session["user"];
                model.School = sch;
                model.Registered_By = rb;
                table_mark_range mode = db.table_mark_range.Where(p => p.School == sch).FirstOrDefault();
                f_ca_1 = Convert.ToDecimal(mode.First_Ca);
                f_ca_2 = Convert.ToDecimal(mode.Second_Ca);
                exam = Convert.ToDecimal(mode.Exam);
                return View(model);
            }
        }

        public JsonResult fetchClass()
        {
            string sch = (string)Session["school"];
            var des = db.table_class.Where(p => p.School == sch).OrderBy(p=>p.Class).Distinct().ToList();
            return Json(des, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Input_Result_Group(table_result model,FormCollection fmc)
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
                ViewBag.sessio = display_session();
                ViewBag.ter = display_term();
                ViewBag.clas = display_class();
                ViewBag.categor = display_category();
                ViewBag.subjec = display_subject();
                ViewBag.student_nam = display_student_name();
                string sch = (string)Session["school"];
                string rb = (string)Session["user"];
                model.School = sch;
                model.Registered_By = rb;
                table_mark_range mode = db.table_mark_range.Where(p => p.School == sch).FirstOrDefault();
                model.Status = mode.First_Ca.ToString();
                model.Pre_Comment = mode.Second_Ca.ToString();
                model.Code = mode.Exam.ToString();
                f_ca_1 = Convert.ToDecimal(mode.First_Ca);
                f_ca_2 = Convert.ToDecimal(mode.Second_Ca);
                exam = Convert.ToDecimal(mode.Exam);
                return View(model);
            }
        }

        public JsonResult marks()
        {
            string sch = (string)Session["school"];
            var mode = db.table_mark_range.Where(p => p.School == sch).FirstOrDefault();
            return Json(mode, JsonRequestBehavior.AllowGet);
           // model.Status = mode.First_Ca.ToString();
            //model.Pre_Comment = mode.Second_Ca.ToString();
           // model.Code = mode.Exam.ToString();
        }

        // GET: Result/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_result model = db.table_result.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        // POST: Result/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,Student_ID,Student_Name,Session,Term,Subject,Code,Score_CA,Score_Exam,Score_Total,Grade,Remark,Date,Registered_By,Class,School,Teacher_Signature,Score_CA1,Position,Status,Pre_Comment,L_U")] table_result table_result)
        {
            if (ModelState.IsValid)
            {
                db.Entry(table_result).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(table_result);
        }

        // GET: Result/Delete/5
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
                table_result table_result = db.table_result.Find(id);
                if (table_result == null)
                {
                    return HttpNotFound();
                }
                return View(table_result);
            }
        }

        // POST: Result/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            table_result table_result = db.table_result.Find(id);
            db.table_result.Remove(table_result);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult fetchStudent(string id)
        {
             string sch = (string)Session["school"];
            var rec = db.table_student.Where(P => P.School == sch && P.Class == id).ToList();
            return Json(rec);
        }

        [HttpPost]
        public JsonResult searchResult(string category,string sessio,string ter,string clas)
        {
            string sch = (string)Session["school"];
            string table = "";
            if (category.Contains("Mid"))
            {
                table = "table_result_mid";
            }
            else
            {
                table = "table_result";
            }
            var rec = db.table_result.SqlQuery("SELECT * FROM " + table + " where session='" + sessio + "' and term='" + ter + "' and class='" + clas + "'").ToList<table_result>();
            return Json(rec);
        }

        [HttpPost]
        public JsonResult searchResult_Name(string category, string sessio, string ter, string clas,string student_name)
        {
            string sch = (string)Session["school"];
            string table = "";
            if (category.Contains("Mid"))
            {
                table = "table_result_mid";
            }
            else
            {
                table = "table_result";
            }
            var rec = db.table_result.SqlQuery("SELECT * FROM " + table + " where session='" + sessio + "' and term='" + ter + "' and class='" + clas + "' and student_name='" + student_name + "'").ToList<table_result>();
            return Json(rec);
        }


        public string find_id(string student_name)
        {
            string asp = db.table_student.Where(p => p.Full_Name == student_name).Select(p => p.Student_ID).FirstOrDefault();
            return asp;
        }

        public string Insert(string student_name, string student_id, string subject, string score_ca, string score_exam, string category, string sessio, string term, string clas)
        {
            string resp = "";
            string reg_by = "";
            string sch = (string)Session["school"];
            string user = (string)Session["user"];
            var mode = db.table_mark_range.Where(p => p.School == sch).FirstOrDefault();
            f_ca_1 = Convert.ToDecimal(mode.First_Ca);
           // f_ca_2 = Convert.ToDecimal(mode.Second_Ca);
            exam = Convert.ToDecimal(mode.Exam);
            if (category=="undefined" || sessio == "undefined" || term == "undefined" || clas == "undefined")
            {
                resp = "Category, Session, Term, and Class must be Selected ...";
            }
            else if(Convert.ToInt32(score_ca)>Convert.ToInt32(f_ca_1) /*|| Convert.ToInt32(score_ca1) > Convert.ToInt32(f_ca_2)*/ || Convert.ToInt32(score_exam) > Convert.ToInt32(exam))
            {
                resp = "CA. Score must not be greater than " + f_ca_1 + ", and Exam. Score must not be greater than " + exam;
            }
            else
            {
                //////////////////////////////////////////////////////////////
                try
                {

                    string[] word = user.Split(' ');

                    try
                    {
                        string surname = word[word.Length - 1].ToString();
                        string initial = " ." + word[word.Length - 2].First() + ".";
                        reg_by = surname.Substring(0, 1).ToUpper() + surname.Substring(1).ToLower() + initial.ToUpper();
                    }
                    catch
                    {
                        string surname = word[word.Length - 1].ToString();
                        string initial = " ." + word[word.Length - 2].First() + ".";
                        reg_by = surname.Substring(0, 1).ToUpper() + surname.Substring(1).ToLower() + initial.ToUpper();
                    }
                }
                catch (Exception ex)
                {
                    resp = ex.Message;
                }
                ////////////////////////////////////////////////////////////////
                if (!string.IsNullOrWhiteSpace(score_ca) && !string.IsNullOrWhiteSpace(score_exam) && !string.IsNullOrWhiteSpace(student_id) && !string.IsNullOrWhiteSpace(subject))
                {
                    string code = student_id + sessio + term + subject;
                    int score_total = Convert.ToInt32(score_ca) + Convert.ToInt32(score_exam);

                    if (category.Contains("Mid"))
                    {
                        db.Database.ExecuteSqlCommand("INSERT INTO table_result_mid(Student_ID,Student_Name,Session,Term,Subject,Code,Score_CA,Score_Exam,Score_Total,Date,Registered_By,Class,School,Teacher_Signature,Status)VALUES('" + student_id + "','" + student_name + "','" + sessio + "','" + term + "','" + subject + "','" + code.ToString() + "','" + score_ca + "','" + score_exam + "','" + score_total + "','" + DateTime.Now + "','" + user + "','" + clas + "','" + sch + "','" + reg_by + "','Confirmed ...') ON DUPLICATE KEY UPDATE student_id=values(student_id),student_name=values(student_name),session=values(session),term=values(term),subject=values(subject),code=values(code),score_ca=values(score_ca),score_exam=values(score_exam),score_total=values(score_total),date=values(date),registered_by=values(registered_by),grade=values(grade),remark=values(remark),Class=values(Class),School=values(School),Teacher_Signature=values(Teacher_Signature),Status=values(Status)");
                    }
                    else
                    {
                        db.Database.ExecuteSqlCommand("INSERT INTO table_result(Student_ID,Student_Name,Session,Term,Subject,Code,Score_CA,Score_Exam,Score_Total,Date,Registered_By,Class,School,Teacher_Signature,Status)VALUES('" + student_id + "','" + student_name + "','" + sessio + "','" + term + "','" + subject + "','" + code.ToString() + "','" + score_ca + "','" + score_exam + "','" + score_total + "','" + DateTime.Now + "','" + user + "','" + clas + "','" + sch + "','" + reg_by + "','Confirmed ...') ON DUPLICATE KEY UPDATE student_id=values(student_id),student_name=values(student_name),session=values(session),term=values(term),subject=values(subject),code=values(code),score_ca=values(score_ca),score_exam=values(score_exam),score_total=values(score_total),date=values(date),registered_by=values(registered_by),grade=values(grade),remark=values(remark),Class=values(Class),School=values(School),Teacher_Signature=values(Teacher_Signature),Status=values(Status)");
                    }
                    resp = "Summary of Record Saved: Name: " + student_name + " \n\n SUBJECT: \t\t\t " + subject + "\n\n CA. Score:" + score_ca + "\n\n CA. Score:" + score_ca + "\n\n Exam. Score:" + score_exam;
                }
                else
                {
                    resp = "You Must Select Student's Name, Subject, Enter CA. Score, and Exam Score ...";
                }
                
            }
            return resp;
        }

       /* public string Insert(string category)
        {
            string resp = category;//"";
            if (category == "undefined")
            {
                resp = "Category, Session, Term, and Class must be Selected ... con";
            }
            return resp;
        }*/

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
