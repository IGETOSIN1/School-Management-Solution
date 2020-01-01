using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_SCONET_MANAGEMENT.Models;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace WEB_SCONET_MANAGEMENT.Controllers
{
    public class MiscController : Controller
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
                if (Session["role"].ToString().Contains("role") || Session["cat_status"].ToString().Contains("Super Sup"))
                {
                    v2 = true;
                }
                else
                {
                    v2 = false;
                }
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

        public List<SelectListItem> display_promotion_comment()
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem {Text="Promoted on Merit",Value="Promoted on Merit" },
                new SelectListItem {Text="Promoted",Value="Promoted" },
                new SelectListItem {Text="Promoted on Trial",Value="Promoted on Trial" },
                new SelectListItem {Text="Not Promoted",Value="Not Promoted" },
            };
            return list;
        }

        public List<SelectListItem> display_status()
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem {Text="Full Scholarship",Value="Full Scholarship" },
                new SelectListItem {Text="Part Scholarship",Value="Part Scholarship" },
                new SelectListItem {Text="Not Applicable",Value="Not Applicable" },
            };
            return list;
        }
        private SelectList display_list()
        {
            string vst = (string)Session["school"];
            return new SelectList(db.table_user.Where(p => p.School == vst).OrderBy(m => m.Full_Name).Select(m => m.Full_Name).Distinct().ToList());
        }

        private SelectList display_student()
        {
            string vst = (string)Session["school"];
            return new SelectList(db.table_student.Where(p => p.School == vst).OrderBy(m => m.Full_Name).Select(m => m.Full_Name).Distinct().ToList());
        }

        private SelectList display_student_old()
        {
            string vst = (string)Session["school"];
            return new SelectList(db.table_student_old.Where(p => p.School == vst).OrderBy(m => m.Full_Name).Select(m => m.Full_Name).Distinct().ToList());
        }

        // GET: Misc
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Misc_Resumption(table_resumption model,string s)
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
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult edit_name_id(table_student model, string s)
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
                ViewBag.Full_Nam = display_student();
                ViewBag.Full_Nam_stat = display_student();
                ViewBag.Full_Nam_act = display_student();
                ViewBag.Full_Nam_deact = display_student_old();
                ViewBag.statu = display_status();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult edit_name_id(table_student model)
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
                ViewBag.Full_Nam = display_student();

                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Misc_Promotion_Range(table_promotion_range model,string s)
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
                ViewBag.promotion_commen = display_promotion_comment();
                ViewBag.sessio = display_session();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Misc_Resumption(table_resumption model,FormCollection fmc)
        {
            if(string.IsNullOrWhiteSpace(model.Session) || string.IsNullOrWhiteSpace(model.Term) || string.IsNullOrWhiteSpace(model.Resumption_Statement))
            {
                ViewBag.report = "All Fields are required ...";
            }
            else
            {
                string table = "";
                if (fmc["category"] == "Mid-Term")
                {
                    table = "Table_Resumption_mid";
                }
                else if (fmc["category"] == "Terminal")
                {
                    table = "Table_Resumption";
                }
                string sch = (string)Session["school"];
                model.Code = model.Session + "/" + model.Term + "/" + sch;
                model.School = (string)Session["school"];
                model.Registered_By = (string)Session["user"];
                string query = "INSERT INTO " + table + "(Resumption_Statement,Session,Term,School,Code,Date,Registered_By)VALUES('" + model.Resumption_Statement + "','" + model.Session + "','" + model.Term + "','" + model.School + "','" + model.Code + "','" + DateTime.Now.ToString() + "','"+ model.Registered_By +"') ON DUPLICATE KEY UPDATE resumption_statement=values(resumption_statement),session=values(session),term=values(term),school=values(school),code=values(code),registered_by=values(registered_by),date=values(date)";
                db.Database.ExecuteSqlCommand(query);
                ViewBag.report = "Resumption Date/ Comment Successfully Saved ...";
                ViewBag.sessio = display_session();
                ViewBag.ter = display_term();
                return View(model);
            }
            ViewBag.sessio = display_session();
            ViewBag.ter = display_term();
            return View(model);
        }
        [HttpPost]
        public ActionResult Misc_Promotion_Range(table_promotion_range model)
        {
            if (string.IsNullOrWhiteSpace(model.Session) || string.IsNullOrWhiteSpace(model.From1.ToString()) || string.IsNullOrWhiteSpace(model.To1.ToString()) || string.IsNullOrWhiteSpace(model.Promotion_Comment))
            {
                ViewBag.report = "All Fields are required ...";
            }
            else if(model.From1<0)
            {
                ViewBag.report = "The From Field Must not be less than zero ...";
            }
            else if (model.To1 <= 0)
            {
                ViewBag.report = "The To Field Must be greater than zero ...";
            }
            else
            {
                string sch = (string)Session["school"];
                model.Code = model.Session + "/" + model.Promotion_Comment + "/" + sch;
                model.Registered_By = (string)Session["user"];
                string query = "INSERT INTO Table_promotion_range(from1,to1,promotion_comment,session,school,code,date,registered_by)VALUES('" + model.From1 + "','" + model.To1 + "','" + model.Promotion_Comment + "','" + model.Session + "','" + model.School + "','" + model.Code + "','" + DateTime.Now.ToString() + "','"+ model.Registered_By +"') ON DUPLICATE KEY UPDATE to1=values(to1),from1=values(from1),promotion_comment=values(promotion_comment),school=values(school),session=values(session),code=values(code),registered_by=values(registered_by),date=values(date)";
                db.Database.ExecuteSqlCommand(query);
                ViewBag.report = "Promotion Range Successfully Saved ...";
                ViewBag.promotion_commen = display_promotion_comment();
                ViewBag.sessio = display_session();
                return View(model);
            }
            ViewBag.promotion_commen = display_promotion_comment();
            ViewBag.sessio = display_session();
            return View(model);
        }

        public string MILIK(string st)
        {
            return st + " had been processed";
        }

        // GET: Misc/ManageRole
        public ActionResult ManageRole()
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
                ViewBag.list = display_list();
            }
            return View();
        }

        public string find(string full_name, table_user model)
        {
            model.Department = full_name;
            string sch = (string)Session["school"];
            string st = db.table_user.Where(p => p.Full_Name == model.Full_Name && p.School == sch).Select(p => p.Department).First();
            return st.Replace(" ","_").Replace("-","_").ToLower();  //full_name.Replace("||", "-");
        }


        [HttpPost]
        public ActionResult ManageRole(MISC model,FormCollection fmc)
        {
            string s1 = null;
            //  COLUMN 1
            if (model.manage_staff == true)
            {
                s1 = "Manage Staff||";
            }
            if (model.configuration == true)
            {
                s1 = s1 + "Configuration||";
            }
            if (model.set_resumption_date == true)
            {
                s1 = s1 + "Manage Resumption||";
            }
            if (model.manage_student_record == true)
            {
                s1 = s1 + "Manage Student||";
            }
            if (model.inventory == true)
            {
                s1 = s1 + "Manage Inventory||";
            }
            if (model.manage_user == true)
            {
                s1 = s1 + "Manage User||";
            }
            if (model.send_email == true)
            {
                s1 = s1 + "Manage Email||";
            }
            // COLUMN 2
            if (model.manage_class == true)
            {
                s1 = s1 + "Manage Class||";
            }
            if (model.input_result == true)
            {
                s1 = s1 + "Input Result||";
            }
            if (model.set_promotion_quota == true)
            {
                s1 = s1 + "Manage Promotion||";
            }
            if (model.manage_fee == true)
            {
                s1 = s1 + "Manage Fees||";
            }
            if (model.expenditure == true)
            {
                s1 = s1 + "Expenditure||";
            }
            if (model.manage_role == true)
            {
                s1 = s1 + "Manage Role||";
            }
            if (model.send_sms == true)
            {
                s1 = s1 + "Manage SMS||";
            }
            //  THIRD COLUMN
            if (model.manage_subject == true)
            {
                s1 = s1 + "Manage Subject||";
            }
            if (model.input_comment == true)
            {
                s1 = s1 + "Input Comment||";
            }
            if (model.default_principal_comment == true)
            {
                s1 = s1 + "Default Comment||";
            }
            if (model.make_payment == true)
            {
                s1 = s1 + "Log Payment||";
            }
            if (model.manage_payroll == true)
            {
                s1 = s1 + "Manage Payroll||";
            }
            if (model.report_account == true)
            {
                s1 = s1 + "Manage Account||";
            }
            if (model.manage_time_table == true)
            {
                s1 = s1 + "Manage Time-Table||";
            }

            if(model.list_of_staff=="Select" || string.IsNullOrWhiteSpace(model.list_of_staff))
            {
                ViewBag.report = "Select Name of Staff/ User ...";
            }
            else if(s1==null)
            {
                ViewBag.report = "A role must be selected to update ...";
            }
            else
            {
                // ViewBag.report = s1;
                db.Database.ExecuteSqlCommand("UPDATE TABLE_USER SET DEPARTMENT='" + s1 + "' WHERE FULL_NAME='" + model.list_of_staff + "'");
                ViewBag.report = "Role(s) Successfully Added ...";
            }
            ViewBag.list = display_list();
            return View(model);
        }

        // GET: Misc/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Misc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Misc/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Misc/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Misc/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public string update_student_name(string student_name_old, string student_name)
        {
            string resp = "";
            string sch = (string)Session["school"];
            string user = (string)Session["user"];
            if (student_name_old == "undefined" || student_name == "undefined")
            {
                resp = "Select and Enter the Updated Name/ New Name for the Student ...";
            }
            else
            {
                string query = "UPDATE Table_Comment_Term SET Student_Name='" + student_name + "' WHERE Student_Name='" + student_name_old + "' AND School='" + sch + "';UPDATE Table_payment_fees SET Student_Name='" + student_name + "' WHERE Student_Name='" + student_name_old + "' AND School='" + sch + "';UPDATE Table_payment_fees_detail SET Student_Name='" + student_name + "' WHERE Student_Name='" + student_name_old + "' AND School='" + sch + "';UPDATE Table_Punishment SET Student_Name='" + student_name + "' WHERE Student_Name='" + student_name_old + "' AND School='" + sch + "';UPDATE Table_Punishment SET Student_Name='" + student_name + "' WHERE Student_Name='" + student_name_old + "' AND School='" + sch + "';UPDATE Table_Result SET Student_Name='" + student_name + "' WHERE Student_Name='" + student_name_old + "' AND School='" + sch + "';UPDATE Table_Result_mid SET Student_Name='" + student_name + "' WHERE Student_Name='" + student_name_old + "' AND School='" + sch + "';UPDATE Table_Commendation SET Student_Name='" + student_name + "' WHERE Student_Name='" + student_name_old + "' AND School='" + sch + "';UPDATE Table_Reward SET Student_Name='" + student_name + "' WHERE Student_Name='" + student_name_old + "' AND School='" + sch + "';UPDATE Table_Student SET Full_Name='" + student_name + "' WHERE Full_Name='" + student_name_old + "' AND School='" + sch + "';UPDATE Table_Subject_Registration SET Student_Name='" + student_name + "' WHERE Student_Name='" + student_name_old + "' AND School='" + sch + "';UPDATE Table_Student_User SET Student_Name='" + student_name + "' WHERE Student_Name='" + student_name_old + "';UPDATE Table_payment_analysis SET Student_Name='" + student_name + "' WHERE Student_Name='" + student_name_old + "' AND School='" + sch + "';UPDATE Table_trait SET Student_Name='" + student_name + "' WHERE Student_Name='" + student_name_old + "' AND School='" + sch + "';UPDATE Table_Student_User SET Student_Name='" + student_name + "' WHERE Student_Name='" + student_name_old + "';UPDATE Table_exam_student SET Student_Name='" + student_name + "' WHERE Student_Name='" + student_name_old + "';UPDATE Table_payment_alert SET Student_Name='" + student_name + "' WHERE Student_Name='" + student_name_old + "';UPDATE Table_result_cbt SET Student_Name='" + student_name + "' WHERE Student_Name='" + student_name_old + "';UPDATE Table_result_session SET Student_Name='" + student_name + "' WHERE Student_Name='" + student_name_old + "';UPDATE Table_trait SET Student_Name='" + student_name + "' WHERE Student_Name='" + student_name_old + "';";
                db.Database.ExecuteSqlCommand(query);
                resp = "Student's Name Successfully Updated to " + student_name;
            }
            return resp;
        }

        public string find_id(string student_name)
        {
            string asp = db.table_student.Where(p => p.Full_Name == student_name).Select(p => p.Student_ID).FirstOrDefault();
            return asp;
        }
        [HttpPost]
        public string update_student_id(string student_name, string student_id_new,string student_id_old)
        {
            string resp = "";
            string sch = (string)Session["school"];
            string user = (string)Session["user"];
            if (student_name == "undefined" || student_id_new == "undefined")
            {
                resp = "Select Student's Name and Enter the Updated Name/ New Student's ID ...";
            }
            else
            {
                string query = "UPDATE Table_Comment_Term SET Student_ID='" + student_id_new + "' WHERE Student_Name='" + student_name + "' AND School='" + sch + "';UPDATE Table_trait SET Student_ID='" + student_id_new + "' WHERE Student_Name='" + student_name + "' AND School='" + sch + "';UPDATE Table_Student SET Student_ID='" + student_id_new + "' WHERE Full_Name='" + student_name + "' AND School='" + sch + "';UPDATE Table_payment_analysis SET Student_ID='" + student_id_new + "' WHERE Student_Name='" + student_name + "' AND School='" + sch + "';UPDATE Table_Result_mid SET Student_ID='" + student_id_new + "' WHERE Student_Name='" + student_name + "' AND School='" + sch + "';UPDATE Table_Result SET Student_ID='" + student_id_new + "' WHERE Student_Name='" + student_name + "' AND School='" + sch + "';UPDATE Table_payment_fees_detail SET Student_ID='" + student_id_new + "' WHERE Student_Name='" + student_name + "' AND School='" + sch + "';UPDATE Table_payment_fees SET Student_ID='" + student_id_new + "' WHERE Student_Name='" + student_name + "' AND School='" + sch + "';UPDATE Table_exam_student SET Student_ID='" + student_id_new + "' WHERE Student_Name='" + student_name + "' AND School='" + sch + "';UPDATE Table_Payment_Alert SET Student_ID='" + student_id_new + "' WHERE Student_Name='" + student_name + "' AND School='" + sch + "';UPDATE Table_Result_CBT SET Student_ID='" + student_id_new + "' WHERE Student_Name='" + student_name + "' AND School='" + sch + "';UPDATE Table_Result_mid SET Student_ID='" + student_id_new + "' WHERE Student_Name='" + student_name + "' AND School='" + sch + "';UPDATE Table_Result_Session SET Student_ID='" + student_id_new + "' WHERE Student_Name='" + student_name + "' AND School='" + sch + "';UPDATE Table_Trait SET Student_ID='" + student_id_new + "' WHERE Student_Name='" + student_name + "' AND School='" + sch + "';   update table_comment_term set code=replace(code,'" + student_id_old + "','" + student_id_new + "') WHERE Student_Name='" + student_name + "' AND School='" + sch + "';update table_trait set code=replace(code,'" + student_id_old + "','" + student_id_new + "') WHERE Student_Name='" + student_name + "' AND School='" + sch + "';update table_payment_analysis set code=replace(code,'" + student_id_old + "','" + student_id_new + "') WHERE Student_Name='" + student_name + "' AND School='" + sch + "';update table_payment_fees_detail set code=replace(code,'" + student_id_old + "','" + student_id_new + "') WHERE Student_Name='" + student_name + "' AND School='" + sch + "';update table_payment_fees set code=replace(code,'" + student_id_old + "','" + student_id_new + "') WHERE Student_Name='" + student_name + "' AND School='" + sch + "';update table_result set code=replace(code,'" + student_id_old + "','" + student_id_new + "') WHERE Student_Name='" + student_name + "' AND School='" + sch + "';update table_result_mid set code=replace(code,'" + student_id_old + "','" + student_id_new + "') WHERE Student_Name='" + student_name + "' AND School='" + sch + "';update table_exam_student set code=replace(code,'" + student_id_old + "','" + student_id_new + "') WHERE Student_Name='" + student_name + "' AND School='" + sch + "';update table_result_cbt set code=replace(code,'" + student_id_old + "','" + student_id_new + "') WHERE Student_Name='" + student_name + "' AND School='" + sch + "';update table_result_session set code=replace(code,'" + student_id_old + "','" + student_id_new + "') WHERE Student_Name='" + student_name + "' AND School='" + sch + "';update table_payment_alert set code=replace(code,'" + student_id_old + "','" + student_id_new + "') WHERE Student_Name='" + student_name + "' AND School='" + sch + "';update table_trait set code=replace(code,'" + student_id_old + "','" + student_id_new + "') WHERE Student_Name='" + student_name + "' AND School='" + sch + "';";
                db.Database.ExecuteSqlCommand(query);
                resp = "Student ID Successfully Updated to " + student_id_new;
            }
            return resp;
        }
        [HttpPost]
        public string activate(string student_id)
        {
            string resp = "";
            string sch = (string)Session["school"];
            string user = (string)Session["user"];
            if (student_id == "undefined")
            {
                resp = "Select Name of Student ...";
            }
            else
            {
                string query = "UPDATE Table_payment_analysis set student_status1='' Where student_id='" + student_id + "';UPDATE Table_payment_fees set student_status1='' Where student_id='" + student_id + "';UPDATE Table_payment_fees_detail set student_status1='' Where student_id='" + student_id + "';UPDATE Table_student set student_status1='' Where student_id='" + student_id + "';     insert into table_student select* from table_student_old where school='" + sch + "' and student_id='" + student_id + "' on duplicate key update student_id=values(student_id),full_name=values(full_name),class=values(class);delete from table_student_old where school='" + sch + "' and student_id='" + student_id + "';";
                db.Database.ExecuteSqlCommand(query);
                resp = "Selected Student Record Successfully Activated";
            }
            return resp;
        }
        [HttpPost]
        public string deactivate(string student_id)
        {
            string resp = "";
            string sch = (string)Session["school"];
            string user = (string)Session["user"];
            if (student_id == "undefined")
            {
                resp = "Select Name of Student ...";
            }
            else
            {
                string query = " UPDATE Table_payment_analysis set student_status1='Dormant' Where student_id='" + student_id + "';UPDATE Table_payment_fees set student_status1='Dormant' Where student_id='" + student_id + "';UPDATE Table_payment_fees_detail set student_status1='Dormant' Where student_id='" + student_id + "';UPDATE Table_student set student_status1='Dormant' Where student_id='" + student_id + "'; ";
                db.Database.ExecuteSqlCommand("      insert into table_student_old select* from table_student where school='" + sch + "' and student_id='" + student_id + "' on duplicate key update student_id=values(student_id),full_name=values(full_name),class=values(class);delete from table_student where school='" + sch + "' and student_id='" + student_id + "';      ");
                resp = "Selected Student Record Successfully De-activated ...";
            }
            return resp;
        }
        [HttpPost]
        public string set_scholarship_status(string student_id,string student_status)
        {
            string resp = "";
            string sch = (string)Session["school"];
            string user = (string)Session["user"];
            if (student_id == "undefined" || student_status == "undefined")
            {
                resp = "Select Name of Student and Status ...";
            }
            else
            {
                string query = "UPDATE Table_student set scholarship_status='" + student_status + "' Where student_id='" + student_id + "' and school='" + sch + "';UPDATE Table_Student set scholarship_status='Not Applicable' Where scholarship_status='';";
                db.Database.ExecuteSqlCommand(query);
                resp = "Selected Student Status Successfully Updated ...";
            }
            return resp;
        }

        // GET: Misc/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Misc/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
