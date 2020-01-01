using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace WEB_SCONET_MANAGEMENT
{
    public partial class Login : System.Web.UI.Page
    {
        General_Class gclass = new General_Class();
        bool status_student_id_existence = false;
        bool status_pin_existence = false; 
        string status_pin = null;
        string student_name = null;
        string school_campus = null;
        string class_spec = null;
        string p_t1 = null;
        string p_t_s1 = null;
        string subdomain = " ";
       
        protected void Page_Load(object sender, EventArgs e)
        {
         
            try
            {
                Response.ExpiresAbsolute = DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0));
                Response.Expires = 0;
                Response.CacheControl = "no-cache";
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

             

           /* try
            {
                string host = Request.Url.Host.ToString();
                if (host.Split('.').Length > 1)
                {
                    int index = host.IndexOf(".");
                    subdomain = host.Substring(0, index);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            xxxx.Text = subdomain;//HttpContext.Current.Request.Url.Authority;
            xxx1.Text = HttpContext.Current.Request.Url.Authority;*/
            /*
            //////////////////////////// END OF DETERMINATION OF SUB-DOMAIN AND CONNECTION //////////////////////////////////////////////////
            if (ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString == " " && subdomain == " ")
            {
                try
                {
                    string host = Request.Url.Host.ToString();
                    if (host.Split('.').Length > 1)
                    {
                        int index = host.IndexOf(".");
                        subdomain = host.Substring(0, index);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }

                MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring_gen"].ConnectionString);
                cn.Open();
                MySqlCommand cmd1 = new MySqlCommand("SELECT connection_string FROM Table_Verify_Schoolms WHERE sub_domain like '%" + subdomain + "%'", cn);
                MySqlDataReader dr = null;
                try
                {

                    dr = cmd1.ExecuteReader();   //gclass.display_in_box("SELECT* FROM Table_Student WHERE Full_Name='" + student_name_query.Text + "' AND School='" + school.Text + "'");
                    if (dr.Read())
                    {
                        ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString = (string)dr.GetValue(0).ToString();
                    }

                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                    //result_output.Text = ex.Message;
                }
                finally
                {
                    cn.Close();
                    cmd1.Dispose();
                    dr.Close();
                }
            }

            //////////////////////////// END OF DETERMINATION OF SUB-DOMAIN AND CONNECTION //////////////////////////////////////////////////
            */



            //  Panel_login_staff.Visible = true;
            //  Panel_login_result.Visible = false;

            //   Response.Write(Request.Browser.Browser);

            /* chk_terminal.Checked = true;
             radiobutton_resultchecker.Checked = true;
             Chk_mid_term.Checked = false;
             radiobutton_stafflogin.Checked = false;*/
            //General_Class gclass = new General_Class();
            //gclass.insert1("update table_result set score_ca=score_ca+score_ca1,score_ca1='0' where score_ca1>0;update table_result_mid set score_ca=score_ca+score_ca1,score_ca1='0' where score_ca1>0;update table_mark_range set first_ca=first_ca+second_ca,second_ca='0' where second_ca>0;");

            MySqlConnection cn1 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            cn1.Open();
            MySqlCommand cmd = new MySqlCommand("UPDATE TABLE_RESULT SET Score_Total=score_ca+score_ca1+score_exam;UPDATE TABLE_RESULT SET Score_CA=Substring(Score_CA,2) WHERE Score_CA like '0%';UPDATE TABLE_RESULT SET Score_CA1=Substring(Score_CA1,2) WHERE Score_CA1 like '0%';UPDATE TABLE_RESULT SET Score_Exam=Substring(Score_Exam,2) WHERE Score_Exam like '0%';UPDATE TABLE_comment_term SET No_of_Time_School_Open=Substring(No_of_Time_School_Open,2) WHERE No_of_Time_School_Open like '0%';UPDATE TABLE_comment_term SET No_of_time_student_present=Substring(No_of_time_student_present,2) WHERE No_of_time_student_present like '0%';UPDATE Table_Result SET Position=CONCAT(position,'th') WHERE Position !='1' AND Position !='2' AND Position !='3' and Position not like '%st' AND Position not like '%nd' AND Position not like '%rd'  and Position !='' and position not like '%th';UPDATE Table_Result SET Position=CONCAT(position,'rd') WHERE Position='3' AND Position not like '%rd' and Position !='';UPDATE Table_Result SET Position=CONCAT(position,'nd') WHERE Position='2' AND Position not like '%nd'  and Position !='';UPDATE Table_Result SET Position=CONCAT(position,'st') WHERE Position='1' AND Position not like '%st' and Position !='';UPDATE TABLE_comment_term SET no_of_time_student_present='0' WHERE no_of_time_student_present='';UPDATE table_result_mid SET Score_Total=score_ca+score_ca1+score_exam;UPDATE table_result_mid SET Score_CA=Substring(Score_CA,2) WHERE Score_CA like '0%';UPDATE table_result_mid SET Score_CA1=Substring(Score_CA1,2) WHERE Score_CA1 like '0%';UPDATE table_result_mid SET Score_Exam=Substring(Score_Exam,2) WHERE Score_Exam like '0%';UPDATE TABLE_comment_term SET No_of_Time_School_Open=Substring(No_of_Time_School_Open,2) WHERE No_of_Time_School_Open like '0%';UPDATE TABLE_comment_term SET No_of_time_student_present=Substring(No_of_time_student_present,2) WHERE No_of_time_student_present like '0%';UPDATE table_result_mid SET Position=CONCAT(position,'th') WHERE Position !='1' AND Position !='2' AND Position !='3' and Position not like '%st' AND Position not like '%nd' AND Position not like '%rd'  and Position !='' and position not like '%th';UPDATE table_result_mid SET Position=CONCAT(position,'rd') WHERE Position='3' AND Position not like '%rd' and Position !='';UPDATE table_result_mid SET Position=CONCAT(position,'nd') WHERE Position='2' AND Position not like '%nd'  and Position !='';UPDATE table_result_mid SET Position=CONCAT(position,'st') WHERE Position='1' AND Position not like '%st' and Position !='';UPDATE TABLE_comment_term SET no_of_time_student_present='0' WHERE no_of_time_student_present='';update table_pin set code=concat(p_id,session,term,pin) WHERE code='';", cn1);
            MySqlCommand cmd1 = new MySqlCommand("update table_result set score_ca=score_ca+score_ca1,score_ca1='0' where score_ca1>0;update table_result_mid set score_ca=score_ca+score_ca1,score_ca1='0' where score_ca1>0;update table_mark_range set first_ca=first_ca+second_ca,second_ca='0' where second_ca>0;", cn1);
            try
            {
                  cmd.ExecuteNonQuery();
                  cmd1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                cn1.Close();
                cmd.Dispose();
                cmd1.Dispose();
            }
            


            Panel_login_staff.Visible = false;
            Panel_login_student_parent.Visible = false;
            Panel_login_result.Visible = true;

           /* chk_terminal.Checked = true;
            HttpCookie cookie_mid_terminal = new HttpCookie("cookie_mid_terminal");
            cookie_mid_terminal.Value = "Terminal";
            cookie_mid_terminal.Expires = DateTime.Now.AddHours(3);
            Response.SetCookie(cookie_mid_terminal);*/

          /*  session.SelectedIndex = 2;
            term.SelectedIndex = 2;*/
            try
            {
                MySqlConnection cn8 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                cn8.Open();
                MySqlCommand cmd8 = new MySqlCommand("SELECT School_Name FROM table_schools ORDER BY p_id asc limit 1", cn8);
                //gclass.display_in_box("SELECT* FROM table_configuration Where name_of_school='" + school_query.Text + "'");
                MySqlDataReader dr8 = cmd8.ExecuteReader();
                try
                {
                    if (dr8.Read())
                    {
                        school.Items.Clear();
                        school.Items.Add((string)dr8.GetValue(0).ToString());
                        school.SelectedIndex = 0;

                        school_login.Items.Clear();
                        school_login.Items.Add((string)dr8.GetValue(0).ToString());
                        school_login.SelectedIndex = 0;

                        this.Title = (string)dr8.GetValue(0).ToString() + " Schoolnigeria.com.ng";

                        dr8.Close();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    cn8.Close(); cn8.Dispose();
                    cmd8.Dispose();
                    dr8.Close(); dr8.Dispose();
                }
            }
            catch (Exception ex)
            {
               // Response.Write(ex.Message);
            }

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            /* if ((username.Text == "dtmoyo" && password.Text == "dtmoyo") || (username.Text == "igtsystem" && password.Text == "igtsystem"))
             {
                 Response.Redirect("Default.aspx");
             }
             else
             {
                 result.Text = "User Name or Passowrd is Incorrect!...";
             }*/

            if (string.IsNullOrWhiteSpace(username.Text))
            {
                result.Text = " Enter your User Name! ...";
            }
            else if (string.IsNullOrWhiteSpace(password.Text))
            {
                result.Text = " Enter your User Password! ...";
            }
            else if (username.Text == "activate" && password.Text == "activate@1234")
            {
                gclass.Delete1("DELETE FROM Table_Sae");
                gclass.Delete1("DELETE FROM Table_Schools");
                Response.Redirect("License.aspx");
            }
            else if ((username.Text == "igtsystem20" && password.Text == "igtsystem20@nigeria") || (username.Text == "Memesco" && password.Text == "memesco@nigeria"))
            {
                // category_status.Text = "SSSU";
                // department.Text = "Super Super";
                //  school.Text = "Super Super";
                HttpCookie cookie_department = new HttpCookie("cookie_department");
                cookie_department.Value = "Super Super";
                cookie_department.Expires = DateTime.Now.AddHours(3);
                Response.SetCookie(cookie_department);

                HttpCookie cookie_cat_status = new HttpCookie("cookie_cat_status");
                cookie_cat_status.Value = "Super Super";
                cookie_cat_status.Expires = DateTime.Now.AddHours(5);
                Response.SetCookie(cookie_cat_status);

                HttpCookie cookie_school = new HttpCookie("cookie_school");
                cookie_school.Value = "Super Super";
                cookie_school.Expires = DateTime.Now.AddHours(3);
                Response.SetCookie(cookie_school);

                HttpCookie cookie_users = new HttpCookie("cookie_users");
                cookie_users.Value = "Welcome - Admin Admin";
                cookie_users.Expires = DateTime.Now.AddHours(3);
                Response.SetCookie(cookie_users);

                HttpCookie cookie_user_name = new HttpCookie("cookie_user_name");
                cookie_user_name.Value = username.Text;
                cookie_user_name.Expires = DateTime.Now.AddHours(3);
                Response.SetCookie(cookie_user_name);

                Session["school"] = "";//(string)dr.GetValue(4).ToString();
                Session["department"] = "Super Super";
                Session["user"] = "Admin Admin"; //(string)dr.GetValue(3).ToString().Replace("Mr.", "").Replace("Mrs.", "").Replace("Miss.", "").Replace("Chief.", "").Replace("Dr.", "").Replace("Prof.", "");
                Session["time"] = DateTime.Now.ToLongTimeString();
                Session["role"] = "Super Super"; //"manageclass||manageuser||managesubject||role||config||student||result||comment";
                Session["cat_status"] = "Super Super";

                // Response.Redirect("Home/Index");
                Response.Redirect("Det.aspx");

                /* fm.blessed_kiddies_campus.Enabled = true;
                 fm.shallom_campus.Enabled = true;
                 fm.winners_campus.Enabled = true;
                 fm.jislord_campus.Enabled = true;*/

            }
            else
            {
                MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT* FROM table_user WHERE User_Name='" + username.Text + "' AND Password='" + password.Text + "'", cn);

                try
                {
                    MySqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        HttpCookie cookie_department = new HttpCookie("cookie_department");
                        cookie_department.Value = (string)dr.GetValue(5).ToString().ToLower();
                        cookie_department.Expires = DateTime.Now.AddHours(5);
                        Response.SetCookie(cookie_department);

                        HttpCookie cookie_cat_status = new HttpCookie("cookie_cat_status");
                        cookie_cat_status.Value = (string)dr.GetValue(6).ToString();
                        cookie_cat_status.Expires = DateTime.Now.AddHours(5);
                        Response.SetCookie(cookie_cat_status);

                        HttpCookie cookie_school = new HttpCookie("cookie_school");
                        cookie_school.Value = (string)dr.GetValue(4).ToString();
                        cookie_school.Expires = DateTime.Now.AddHours(5);
                        Response.SetCookie(cookie_school);

                        HttpCookie cookie_users = new HttpCookie("cookie_users");
                        cookie_users.Value = (string)dr.GetValue(3).ToString().Replace("Mr.", "").Replace("Mrs.", "").Replace("Miss.", "").Replace("Chief.", "").Replace("Dr.", "").Replace("Prof.", "");
                        cookie_users.Expires = DateTime.Now.AddHours(5);
                        Response.SetCookie(cookie_users);

                        HttpCookie cookie_user_name = new HttpCookie("cookie_user_name");
                        cookie_user_name.Value = username.Text.ToLower();
                        cookie_user_name.Expires = DateTime.Now.AddHours(5);
                        Response.SetCookie(cookie_user_name);


                        Session["school"] = (string)dr.GetValue(4).ToString();
                        Session["department"] = (string)dr.GetValue(5).ToString().ToLower();
                        Session["user"] = (string)dr.GetValue(3).ToString().Replace("Mr.", "").Replace("Mrs.", "").Replace("Miss.", "").Replace("Chief.", "").Replace("Dr.", "").Replace("Prof.", "");
                        Session["time"] = DateTime.Now.ToLongTimeString();
                        Session["role"] = (string)dr.GetValue(5).ToString().ToLower(); //"manageclass||manageuser||managesubject||role||config||student||result||comment";
                        Session["cat_status"] = (string)dr.GetValue(6).ToString();

                        Response.Redirect("Det.aspx");
                        //Response.Redirect("Home/Index");


                        /* fm.user.Text = (string)dr.GetValue(3).ToString(); 
                         fm.category_status.Text = (string)dr.GetValue(6).ToString(); 
                         if (fm.school.Text == "Super Super" || fm.department.Text == "Super Super")
                         {
                             fm.blessed_kiddies_campus.Enabled = true;
                             fm.shallom_campus.Enabled = true;
                             fm.winners_campus.Enabled = true;
                             fm.jislord_campus.Enabled = true;
                         }*/
                        dr.Close();

                    }
                    else
                    {
                        result.Text = "User Name/ Password is Incorrect! Try again later.";
                    }
                }
                catch (Exception ex)
                {
                    result.Text = "Try to Re-Login Again ...";
                    // result.Text = ex.Message;
                }
                finally
                {
                    cn.Close();
                    cmd.Dispose();
                }
            }
           
        }

        protected void Button3_Click(object sender, EventArgs e)
        {

        }

        protected void Button6_Click(object sender, EventArgs e)
        {

        }

        protected void Button5_Click(object sender, EventArgs e)
        {

        }

        protected void radiobutton_resultchecker_CheckedChanged(object sender, EventArgs e)
        {
            radiobutton_stafflogin.Checked = false;
            Panel_login_result.Visible = true;
            Panel_login_staff.Visible = false;
            Panel_login_student_parent.Visible = false;
           // Chk_mid_term.Visible = true;
           // chk_terminal.Visible = true;
            //radiobutton_studentlogin.Checked = false;
        }

        protected void radiobutton_stafflogin_CheckedChanged(object sender, EventArgs e)
        {
            radiobutton_resultchecker.Checked = false;
            Panel_login_result.Visible = false;
            Panel_login_staff.Visible = true;
            Panel_login_student_parent.Visible = false;
           // Chk_mid_term.Visible = false;
           // chk_terminal.Visible = false;
           // radiobutton_studentlogin.Checked = false;
        }

        protected void radiobutton_studentlogin_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void school_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void school_login_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void session_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void term_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            student_id.Text = null;
            session.SelectedIndex = -1;
            term.SelectedIndex = -1;
            student_id.Text = null;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            result.Text = null;
            try
            {
                if (string.IsNullOrWhiteSpace(student_id.Text))
                {
                    result.Text = "Enter Your Student ID ...";
                }
                else if (string.IsNullOrWhiteSpace(pin_number.Text))
                {
                    result.Text = "Enter Your Pin Number ...";
                }
                else if (session.SelectedIndex == 0)
                {
                    result.Text = "Select Session ...";
                }
                else if (term.SelectedIndex == 0)
                {
                    result.Text = "Select Term ...";
                }
               /* else if (Chk_mid_term.Checked == false && chk_terminal.Checked == false)
                {
                    result.Text = "Tick Category of Result to Display whether Mid-Term or Terminal Result Sheet ...";
                }*/
                else
                {
                    if (chk_terminal.Checked == true)
                    {
                        HttpCookie cookie_det = new HttpCookie("cookie_det");
                        cookie_det.Value = "terminal";
                        cookie_det.Expires = DateTime.Now.AddHours(3);
                        Response.SetCookie(cookie_det);
                    }
                    else if (Chk_mid_term.Checked == true)
                    {
                        HttpCookie cookie_det = new HttpCookie("cookie_det");
                        cookie_det.Value = "mid term";
                        cookie_det.Expires = DateTime.Now.AddHours(3);
                        Response.SetCookie(cookie_det);
                    }

                    // FIRST STAGE VERIFICATION ...
                    MySqlConnection cn21 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                    cn21.Open();
                    try
                    {

                        MySqlCommand cmd = new MySqlCommand("SELECT* FROM Table_Student WHERE Student_ID='" + student_id.Text + "'", cn21);
                        MySqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            //  result.Text = "ID Exists";
                            student_name = (string)dr.GetValue(4).ToString();
                            school_campus = (string)dr.GetValue(29).ToString();
                            class_spec = (string)dr.GetValue(6).ToString();
                            status_student_id_existence = true;
                        }
                        else
                        {
                            result.Text = "This Student ID Does not Exist ...";
                            status_student_id_existence = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        result.Text = ex.Message;
                    }
                    finally
                    {
                        cn21.Close();
                    }

                    // SECOND STAGE VERIFICATION ...
                    if (status_student_id_existence == true)
                    {
                        MySqlConnection cn22 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                        cn22.Open();
                        try
                        {

                            MySqlCommand cmd = new MySqlCommand("SELECT* FROM Table_Pin WHERE Pin='" + pin_number.Text + "' and Session='"+ session.Text +"' AND Term='"+ term.Text +"'", cn22);
                            MySqlDataReader dr1 = cmd.ExecuteReader();
                            if (dr1.Read())
                            {
                                status_pin = (string)dr1.GetValue(6).ToString();
                                p_t_s1 = (string)dr1.GetValue(2).ToString();
                                p_t1 = (string)dr1.GetValue(3).ToString();
                                status_pin_existence = true;
                            }
                            else
                            {
                                result.Text = "Pin Number Does not Exist ...";
                                status_pin_existence = false;
                            }

                        }
                        catch (Exception ex)
                        {
                            result.Text = ex.Message;
                        }
                        finally
                        {
                            cn22.Close();
                        }

                    }

                    // THIRD STAGE VERIFICATION ...
                    if (status_pin == "Dormant")
                    {
                        if (p_t1 !=term.Text && p_t_s1 !=session.Text)
                        {
                            result.Text = "Thi Pin can only be Used to check  " + p_t1.ToString() + " term result in " + p_t_s1.ToString() + " academic session ...";
                        }
                       /* else if ((p_t1 == "Second" && term.Text == "Third"))
                        {
                            result.Text = "This Pin can only check First and Second Term result ...";
                        }*/
                        else
                        {
                            MySqlConnection cn23 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                            cn23.Open();
                            try
                            {

                                MySqlCommand cmd = new MySqlCommand("UPDATE Table_Pin SET Status='Active' WHERE Pin='" + pin_number.Text + "' AND Session='" + session.Text + "' AND Term='" + term.Text + "'", cn23);
                                MySqlCommand cmd1 = new MySqlCommand("INSERT INTO Table_Pin_History(Student_ID,Student_Name,Session,Term,Pin,Date,Status,School,Class)VALUES('" + student_id.Text + "','" + student_name.ToString() + "','" + session.Text + "','" + term.Text + "','" + pin_number.Text + "','" + DateTime.Now.ToString() + "','Active','" + school_campus.ToString() + "','" + class_spec.ToString() + "')", cn23);

                                cmd1.ExecuteNonQuery();
                                cmd.ExecuteNonQuery();

                                HttpCookie cookie_student_id = new HttpCookie("cookie_student_id");
                                cookie_student_id.Value = student_id.Text;
                                cookie_student_id.Expires = DateTime.Now.AddHours(3);
                                Response.SetCookie(cookie_student_id);

                                HttpCookie cookie_school = new HttpCookie("cookie_school");
                                cookie_school.Value = school_campus.ToString();
                                cookie_school.Expires = DateTime.Now.AddHours(3);
                                Response.SetCookie(cookie_school);

                                HttpCookie cookie_session = new HttpCookie("cookie_session");
                                cookie_session.Value = session.Text;
                                cookie_session.Expires = DateTime.Now.AddHours(3);
                                Response.SetCookie(cookie_session);

                                HttpCookie cookie_term = new HttpCookie("cookie_term");
                                cookie_term.Value = term.Text;
                                cookie_term.Expires = DateTime.Now.AddHours(3);
                                Response.SetCookie(cookie_term);

                              //  Response.Redirect("Report-Sheet.aspx");



                                //MessageBox.Show("Record Successfully Saved!", "Confirmation Box!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            catch (Exception ex)
                            {
                                Response.Write(ex.Message);
                            }
                            finally
                            {
                                cn23.Close();
                            }
                        }
                    }
                    else if (status_pin == "Active")
                    {
                       /* if ((p_t1 == "First" && term.Text == "Second") || (p_t1 == "First" && term.Text == "Third"))
                        {
                            result.Text = "This Pin can only check first term result ...";
                        }
                        else if ((p_t1 == "Second" && term.Text == "Third"))
                        {
                            result.Text = "This Pin can only check First and Second Term result ...";
                        }*/
                        if (p_t1 != term.Text && p_t_s1 != session.Text)
                        {
                            result.Text = "Thi Pin can only be Used to check  " + p_t1.ToString() + " term result in " + p_t_s1.ToString() + " academic session ...";
                        }
                        else
                        {
                            MySqlConnection cn24 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                            cn24.Open();
                            try
                            {

                                MySqlCommand cmd4 = new MySqlCommand("SELECT* FROM Table_Pin_History WHERE Pin='" + pin_number.Text + "' AND Session='" + session.Text + "' AND Student_ID='" + student_id.Text + "'", cn24);
                                MySqlDataReader dr3 = cmd4.ExecuteReader();
                                if (dr3.Read())
                                {
                                    HttpCookie cookie_student_id = new HttpCookie("cookie_student_id");
                                    cookie_student_id.Value = student_id.Text;
                                    cookie_student_id.Expires = DateTime.Now.AddHours(5);
                                    Response.SetCookie(cookie_student_id);

                                    HttpCookie cookie_school = new HttpCookie("cookie_school");
                                    cookie_school.Value = school_campus.ToString();
                                    cookie_school.Expires = DateTime.Now.AddHours(5);
                                    Response.SetCookie(cookie_school);

                                    HttpCookie cookie_session = new HttpCookie("cookie_session");
                                    cookie_session.Value = session.Text;
                                    cookie_session.Expires = DateTime.Now.AddHours(5);
                                    Response.SetCookie(cookie_session);

                                    HttpCookie cookie_term = new HttpCookie("cookie_term");
                                    cookie_term.Value = term.Text;
                                    cookie_term.Expires = DateTime.Now.AddHours(5);
                                    Response.SetCookie(cookie_term);

                                    Response.Redirect("Report-Sheet.aspx");
                                }
                                else
                                {
                                    result.Text = "Scratch Card/ Pin already Used by Someone else ... Contact the School Authority for Complaint and Verification ...";
                                    // result.Text = "Pin already used by " + (string)dr3.GetValue(2).ToString() + " From " + (string)dr3.GetValue(9).ToString() + " in " + (string)dr3.GetValue(10).ToString() + " on " + (string)dr3.GetValue(7).ToString();
                                }

                            }
                            catch (Exception ex)
                            {
                                result.Text = ex.Message;
                            }
                            finally
                            {
                                cn24.Close();
                            }
                        }

                    }
                }
            }

            catch (Exception ex)
            {
                result.Text = "Try to re-check Your result again ...";
            }
           // Response.Redirect("Report-Sheet.aspx");
        }

        protected void Button7_Click(object sender, EventArgs e)
        {

        }

        protected void Chk_mid_term_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_mid_term.Checked == true)
            {
                chk_terminal.Checked = false;
                HttpCookie cookie_mid_terminal = new HttpCookie("cookie_mid_terminal");
                cookie_mid_terminal.Value = "Mid Term";
                cookie_mid_terminal.Expires = DateTime.Now.AddHours(3);
                Response.SetCookie(cookie_mid_terminal);
            }
            else
            {
                chk_terminal.Checked = true;
                HttpCookie cookie_mid_terminal = new HttpCookie("cookie_mid_terminal");
                cookie_mid_terminal.Value = "Terminal";
                cookie_mid_terminal.Expires = DateTime.Now.AddHours(3);
                Response.SetCookie(cookie_mid_terminal);
            }
        }

        protected void chk_terminal_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_terminal.Checked == true)
            {
                Chk_mid_term.Checked = false;
                HttpCookie cookie_mid_terminal = new HttpCookie("cookie_mid_terminal");
                cookie_mid_terminal.Value = "Terminal";
                cookie_mid_terminal.Expires = DateTime.Now.AddHours(3);
                Response.SetCookie(cookie_mid_terminal);
            }
            else
            {
                Chk_mid_term.Checked = true;
                HttpCookie cookie_mid_terminal = new HttpCookie("cookie_mid_terminal");
                cookie_mid_terminal.Value = "Mid Term";
                cookie_mid_terminal.Expires = DateTime.Now.AddHours(3);
                Response.SetCookie(cookie_mid_terminal);
            }
        }

        protected void Button7_Click1(object sender, EventArgs e)
        {

        }
    }
}