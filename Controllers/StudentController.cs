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
    public class StudentController : Controller
    {
        private Entities_School db = new Entities_School();
        bool v1, v2 = false;
        public SelectList display_class()
        {
            string sch = (string)Session["school"];
            SelectList lis = new SelectList(db.table_class.Where(p => p.School == sch).OrderBy(p => p.Class).Select(p => p.Class).Distinct());
            return lis;
        }
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
                if (Session["role"].ToString().Contains("student") || Session["cat_status"].ToString().Contains("Super Sup"))
                {
                    v2 = true;
                }
                else
                {
                    v2 = false;
                }
        }

        public List<SelectListItem> display_gender()
        {
            List<SelectListItem> lis = new List<SelectListItem>()
            {
                new SelectListItem {Text="Male",Value="Male" },
                new SelectListItem {Text="Female", Value="Female" },
            };
            return lis;
        }

        public List<SelectListItem> display_student_status()
        {
            List<SelectListItem> lis = new List<SelectListItem>()
            {
                new SelectListItem {Text="Day Student",Value="Day Student" },
                new SelectListItem {Text="Boarding Student", Value="Boarding Student" },
            };
            return lis;
        }

        public List<SelectListItem> display_religion()
        {
            List<SelectListItem> lis = new List<SelectListItem>()
            {
                new SelectListItem {Text="Christianity",Value="Christianity" },
                new SelectListItem {Text="Islamic", Value="Islamic" },
                 new SelectListItem {Text="Atheist", Value="Atheist" },
                new SelectListItem {Text="Others", Value="Others" },
            };
            return lis;
        }

       

        // GET: Student
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
                ViewBag.clas = display_class();
                string sch = (string)Session["school"];
                return View(db.table_student.Where(m=>m.School==sch && !m.Class.StartsWith("grad") && !m.Class.StartsWith("Withdr") && m.Student_Status1=="").OrderBy(p=>p.Full_Name).ToList());
            }
        }
        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            table_student table_student = db.table_student.Find(id);
            if (table_student == null)
            {
                return HttpNotFound();
            }
            return View(table_student);
        }

        // GET: Student/Create
        public ActionResult Create(table_student model,string s)
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
                model.Date = DateTime.Now.ToLongDateString();
                ViewBag.gende = display_gender();
                ViewBag.clas = display_class();
                ViewBag.religio = display_religion();
                ViewBag.student_statu = display_student_status();
                return View(model);
            }
        }

        // POST: Student/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "p_id,Student_ID,Surname,Other_Name,Full_Name,Date_of_Birth,Class,Age,Gender,Student_Tel,Department,Year_of_Admission,Home_Town,State_of_Origin,Home_Address,Father_Name,Father_Phone,Father_Occupation,Father_Email,Mother_Name,Mother_Phone,Mother_Occupation,Contact_Address,Date,Day,Month,Year,Religion,Class_Teacher,School,Student_Status,Session,Term,g_name,g_phone,g_occupation,g_email,mother_email,Student_Status1,Scholarship_Status,L_U")] table_student model)
        {
            if (string.IsNullOrWhiteSpace(model.Surname))
            {
                ViewBag.report = "Enter the Student's Surname/ Last Name";
            }
            else if (string.IsNullOrWhiteSpace(model.Other_Name))
            {
                ViewBag.report = "Enter the Student's Other Name/ Fisrt Name";
            }
            else if (string.IsNullOrWhiteSpace(model.Other_Name))
            {
                ViewBag.report = "Enter the Student's Other Name/ Fisrt Name";
            }
            else if (model.Student_Status == "Select")
            {
                ViewBag.report = "Select Student's Status(Boarding or Day Status)";
            }
            else if (model.Gender == "Select")
            {
                ViewBag.report = "Select Student's Gender/ Sex";
            }
            else if (model.Student_Status == "Select")
            {
                ViewBag.report = "Select Class";
            }
            else
            {
                string sch = (string)Session["school"];
                var ast = db.table_student.OrderByDescending(p=>p.p_id).Select(p=>p.p_id).FirstOrDefault();
                string id = (Convert.ToInt32(ast) + 1).ToString();

                var ke = db.table_schools.Where(m => m.School_Name == sch).Select(m => m.Pre_id).FirstOrDefault();

                model.Full_Name = model.Surname + " " + model.Other_Name;
                model.Student_ID = ke + "/" + DateTime.Now.Year + "/" + id;
                model.School = (string)Session["school"];
                model.Date = DateTime.Now.ToLongDateString();

                db.table_student.Add(model);
                db.SaveChanges();

                model.Date = DateTime.Now.ToLongDateString();
                ViewBag.student_statu = display_student_status();
                ViewBag.gende = display_gender();
                ViewBag.clas = display_class();
                ViewBag.religio = display_religion();
                 return RedirectToAction("Index"); //return View(model);
            }
            model.Date = DateTime.Now.ToLongDateString();
            ViewBag.student_statu = display_student_status();
            ViewBag.gende = display_gender();
            ViewBag.clas = display_class();
            ViewBag.religio = display_religion();
            return View(model);
        }

        // GET: Student/Edit/5
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
                ViewBag.student_statu = display_student_status();
                ViewBag.gende = display_gender();
                ViewBag.clas = display_class();
                ViewBag.religio = display_religion();
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                table_student table_student = db.table_student.Find(id);
                if (table_student == null)
                {
                    return HttpNotFound();
                }

                return View(table_student);
            }
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "p_id,Student_ID,Surname,Other_Name,Full_Name,Date_of_Birth,Class,Age,Gender,Student_Tel,Department,Year_of_Admission,Home_Town,State_of_Origin,Home_Address,Father_Name,Father_Phone,Father_Occupation,Father_Email,Mother_Name,Mother_Phone,Mother_Occupation,Contact_Address,Date,Day,Month,Year,Religion,Class_Teacher,School,Student_Status,Session,Term,g_name,g_phone,g_occupation,g_email,mother_email,Student_Status1,Scholarship_Status,L_U")] table_student model)
        {
            if (ModelState.IsValid)
            {
                db.Database.ExecuteSqlCommand("UPDATE table_Student SET student_id='" + model.Student_ID + "',surname='" + model.Surname + "',other_name='" + model.Other_Name + "',full_name='" + model.Full_Name + "',date_of_birth='" + model.Day + "',class='" + model.Class + "',age='" + model.Age + "',gender='" + model.Gender + "',student_tel='" + model.Student_Tel + "',department='" + model.Department + "',year_of_admission='" + model.Year_of_Admission + "',home_town='" + model.Home_Town + "',state_of_origin='" + model.State_of_Origin + "',home_address='" + model.Home_Address + "',father_name='" + model.Father_Name + "',father_phone='" + model.Father_Phone + "',father_occupation='" + model.Father_Occupation + "',father_email='" + model.Father_Email + "',mother_name='" + model.Mother_Name + "',mother_phone='" + model.Mother_Phone + "',mother_occupation='" + model.Mother_Occupation + "',mother_email='" + model.mother_email + "',contact_address='" + model.Contact_Address + "',religion='" + model.Religion + "',student_status='" + model.Student_Status + "',g_name='" + model.g_name + "',g_phone='" + model.g_phone + "',g_occupation='" + model.g_occupation + "',g_email='" + model.g_email + "',scholarship_status='" + model.Scholarship_Status + "' WHERE student_id='" + model.Student_ID + "'");
              //  db.Entry(table_student).State = EntityState.Modified;
               // db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Student/Delete/5
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
                table_student table_student = db.table_student.Find(id);
                if (table_student == null)
                {
                    return HttpNotFound();
                }
                return View(table_student);
            }
        }

        // POST: Student/Delete/5
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
                table_student table_student = db.table_student.Find(id);
                db.table_student.Remove(table_student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public JsonResult display_class1()
        {
            string sch = (string)Session["school"];
            var rec = db.table_class.Where(p => p.School == sch).OrderBy(p => p.Class).Distinct().ToList();
            return Json(rec);
        }
        public JsonResult display_student_all()
        {
            string sch = (string)Session["school"];
            var rec = db.table_student.Where(m => m.School == sch && !m.Class.StartsWith("grad") && !m.Class.StartsWith("Withdr") && m.Student_Status1 == "").OrderBy(p => p.Full_Name).ToList();
            return Json(rec);
        }
        public JsonResult display_student_class(string clas)
        {
            string sch = (string)Session["school"];
            var rec = db.table_student.Where(m => m.School == sch && m.Class == clas && !m.Class.StartsWith("grad") && !m.Class.StartsWith("Withdr") && m.Student_Status1 == "").OrderBy(p => p.Full_Name).ToList();
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
