using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace WEB_SCONET_MANAGEMENT
{
    public partial class Report_Internal1 : System.Web.UI.Page
    {
        General_Class gclass = new General_Class();
        string subdomain = " ";
       // string cstring = ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
               // student_id_query.Text = Request.Cookies["cookie_student_id"].Value.ToString();
                school_query.Text = Request.Cookies["cookie_school"].Value.ToString();
                school.Text = Request.Cookies["cookie_school"].Value.ToString();
               // session1.Text = Request.Cookies["cookie_session"].Value.ToString();
              //  term1.Text = Request.Cookies["cookie_term"].Value.ToString();
              //  term.Text = term1.Text; //Request.Cookies["cookie_term"].Value.ToString() + " TERM ";
              //  session.Text = session1.Text;//Request.Cookies["cookie_session"].Value.ToString();
                //  mid_terminal.Text = Request.Cookies["cookie_mid_terminal"].Value.ToString();
               // det.Text = Request.Cookies["cookie_det"].Value.ToString();
            }
            catch
            {
                Response.Redirect("Login.aspx");
            }

            string dept = Request.Cookies["cookie_department"].Value.ToString();
            string statu = Request.Cookies["cookie_cat_status"].Value.ToString();
            if (!dept.Contains("schoo") && !statu.Contains("Super Sup"))
            {
                Response.Redirect("Home/Deny");
            }
            //////////////////////////// END OF DETERMINATION OF SUB-DOMAIN AND CONNECTION //////////////////////////////////////////////////
            /*if (ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString == " " && subdomain == " ")
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
                MySqlCommand cmd = new MySqlCommand("SELECT connection_string FROM Table_Verify_Schoolms WHERE sub_domain like '%" + subdomain + "%'", cn);
                MySqlDataReader dr = null;
                try
                {

                    dr = cmd.ExecuteReader();   //gclass.display_in_box("SELECT* FROM Table_Student WHERE Full_Name='" + student_name_query.Text + "' AND School='" + school.Text + "'");
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
                    cmd.Dispose();
                    dr.Close();
                }
            }*/

            //////////////////////////// END OF DETERMINATION OF SUB-DOMAIN AND CONNECTION //////////////////////////////////////////////////




            chk_terminal.Visible = false;
            Chk_mid_term.Visible = false;

            exam_con.Text = "0"; first_con.Text = "0"; second_con.Text = "0"; mid_total_con.Text = "0"; total_con.Text = "0";
            MySqlCommand cmd100 = new MySqlCommand();
            MySqlConnection cn100 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            MySqlDataReader dr15 = gclass.display_in_box1("SELECT* FROM Table_mark_range", cn100, cmd100);
            try
            {
                if (dr15.Read())
                {
                    first_con.Text = (string)dr15.GetValue(1).ToString();
                    second_con.Text = (string)dr15.GetValue(2).ToString();
                    exam_con.Text = (string)dr15.GetValue(3).ToString();
                    mid_total_con.Text = (Convert.ToDecimal(first_con.Text) + Convert.ToDecimal(second_con.Text)).ToString();
                    total_con.Text = (Convert.ToDecimal(first_con.Text) + Convert.ToDecimal(second_con.Text) + Convert.ToDecimal(exam_con.Text)).ToString();
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                cn100.Close(); cn100.Dispose();
                cmd100.Dispose();
                dr15.Close(); dr15.Dispose();
            }

            div_first_second_mid.Visible = false;
            div_third_only.Visible = false;
            p_promotion_status.Visible = false;

            try
            {
                school.Text = Request.Cookies["cookie_school"].Value.ToString();
                school_query.Text = Request.Cookies["cookie_school"].Value.ToString();
                // det.Text = Request.Cookies["cookie_det"].Value.ToString();
            }
            catch (Exception ex)
            {
                Response.Redirect("Login.aspx");
            }

            if (string.IsNullOrWhiteSpace(display_class.Text) || string.IsNullOrWhiteSpace(display_student.Text))
            {

                // gclass.display_in_combobox("SELECT* FROM table_student WHERE School='" + school.Text + "'", student_name_query, "Full_Name");
                // gclass.display_in_combobox("SELECT* FROM table_class WHERE School='" + school.Text + "'", CLASS, "CLASS");

                MySqlConnection cn1 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                try
                {
                    cn1.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter("SELECT DISTINCT(STUDENT_NAME) FROM table_Result WHERE School='" + school.Text + "' order by Student_name", cn1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    student_name_query.DataSource = dt;
                    student_name_query.DataValueField = "Student_Name";
                    student_name_query.DataBind();
                }
                catch (Exception ex)
                {
                    // Response.Write(ex.Message);
                }
                finally
                {
                    cn1.Close();
                }

                MySqlConnection cn2 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                try
                {
                    cn2.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter("SELECT* FROM table_class WHERE School='" + school.Text + "' order by class", cn2);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    CLASS.DataSource = dt;
                    CLASS.DataValueField = "CLASS";
                    CLASS.DataBind();
                }
                catch (Exception ex)
                {
                    // Response.Write(ex.Message);
                }
                finally
                {
                    cn2.Close();
                }



                // CLASS.SelectedIndex = -1;

                // student_name_query.SelectedIndex = -1;
                // student_id_query.Text = null;

                display_class.Text = "Display";
                display_student.Text = "Display";
                Panel1.Visible = false;
            }
            chk_terminal.Visible = true;
            Chk_mid_term.Visible = true;
        }

        protected void session1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel1.Visible = false;
        }

        protected void term1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel1.Visible = false;
        }

        protected void CLASS_SelectedIndexChanged(object sender, EventArgs e)
        {
            MySqlConnection cn1 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            try
            {
                cn1.Open();
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT DISTINCT(student_name) FROM Table_Result WHERE Class='" + CLASS.Text + "' AND School='" + school.Text + "' ORDER BY Student_Name ASC", cn1);
               // MySqlDataAdapter da = new MySqlDataAdapter("SELECT DISTINCT(full_name) FROM Table_Result WHERE Class='" + CLASS.Text + "' AND School='" + school.Text + "' ORDER BY full_Name ASC", cn1);
              
                DataTable dt = new DataTable();
                da.Fill(dt);
                student_name_query.DataSource = dt;
                student_name_query.DataValueField = "student_Name";
                student_name_query.DataBind();
            }
            catch (Exception ex)
            {
                result.Text = ex.Message; // Response.Write(ex.Message);
            }
            finally
            {
                cn1.Close();
            }

            Panel1.Visible = false;

        }

        protected void Chk_mid_term_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_mid_term.Checked == true)
            {
                det.Text = "mid term";
                chk_terminal.Checked = false;
                /* HttpCookie cookie_mid_terminal = new HttpCookie("cookie_mid_terminal");
                 cookie_mid_terminal.Value = "Mid Term";
                 cookie_mid_terminal.Expires = DateTime.Now.AddHours(3);
                 Response.SetCookie(cookie_mid_terminal);*/
            }
            else
            {
                det.Text = "terminal";
                chk_terminal.Checked = true;
              /* HttpCookie cookie_mid_terminal = new HttpCookie("cookie_mid_terminal");
                 cookie_mid_terminal.Value = "Terminal";
                 cookie_mid_terminal.Expires = DateTime.Now.AddHours(3);
                 Response.SetCookie(cookie_mid_terminal);*/
            }
        }

        protected void chk_terminal_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_terminal.Checked == true)
            {
                det.Text = "terminal";
                Chk_mid_term.Checked = false;
                /* HttpCookie cookie_det = new HttpCookie("cookie_det");
                 cookie_det.Value = "Terminal";
                 cookie_det.Expires = DateTime.Now.AddHours(3);
                 Response.SetCookie(cookie_det);*/
            }
            else
            {
                det.Text = "mid term";
                Chk_mid_term.Checked = true;
                /*HttpCookie cookie_det = new HttpCookie("cookie_det");
                cookie_det.Value = "Mid Term";
                cookie_det.Expires = DateTime.Now.AddHours(3);
                Response.SetCookie(cookie_det);*/
            }
        }

        protected void student_name_query_SelectedIndexChanged(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            MySqlConnection cn21 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            cn21.Open();
            MySqlCommand cmd21 = new MySqlCommand("SELECT Student_ID,Student_Name FROM Table_Result WHERE Student_Name='" + student_name_query.Text + "' AND School='" + school_query.Text + "' order by p_id desc limit 1", cn21);
           // MySqlCommand cmd21 = new MySqlCommand("SELECT Student_ID,full_Name FROM Table_stude WHERE full_Name='" + student_name_query.Text + "' AND School='" + school_query.Text + "' order by p_id desc limit 1", cn21);

            try
            {
                MySqlDataReader dr21 = cmd21.ExecuteReader();
                if (dr21.Read())
                {
                    //  result.Text = "ID Exists";
                    student_id_query.Text = (string)dr21.GetValue(0).ToString();
                    student_name_q1.Text = (string)dr21.GetValue(1).ToString();
                    dr21.Close();
                }
            }
            catch (Exception ex)
            {
                result.Text = ex.Message;
            }
            finally
            {
                cn21.Close();
                cmd21.Dispose();
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {

            if (Chk_mid_term.Checked == true)
            {
                det.Text = "mid term";
                chk_terminal.Checked = false;
                /* HttpCookie cookie_mid_terminal = new HttpCookie("cookie_mid_terminal");
                 cookie_mid_terminal.Value = "Mid Term";
                 cookie_mid_terminal.Expires = DateTime.Now.AddHours(3);
                 Response.SetCookie(cookie_mid_terminal);*/
            }
            else
            {
                det.Text = "terminal";
                chk_terminal.Checked = true;
                /* HttpCookie cookie_mid_terminal = new HttpCookie("cookie_mid_terminal");
                   cookie_mid_terminal.Value = "Terminal";
                   cookie_mid_terminal.Expires = DateTime.Now.AddHours(3);
                   Response.SetCookie(cookie_mid_terminal);*/
            }

           

          

            MySqlConnection cn2 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
           // string str2 = "SELECT* FROM Table_Result WHERE Student_name='" + student_name_query.Text + "' AND Session='" + session1.Text + "' AND Term='" + term1.Text + "' ORDER BY p_id DESC LIMIT 1";
            string str2;
            if (det.Text.Contains("mid"))
            {
                str2 = "SELECT* FROM Table_Result_mid WHERE Student_name='" + student_name_query.Text + "' AND Session='" + session1.Text + "' AND Term='" + term1.Text + "' AND School='" + school_query.Text + "' ORDER BY p_id DESC LIMIT 1";
            }
            else
            {
                str2 = "SELECT* FROM Table_Result WHERE Student_name='" + student_name_query.Text + "' AND Session='" + session1.Text + "' AND Term='" + term1.Text + "' AND School='" + school_query.Text + "' ORDER BY p_id DESC LIMIT 1";
            }
            cn2.Open();
            MySqlCommand cmd2 = new MySqlCommand(str2, cn2);
            try
            {

                MySqlDataReader dr = cmd2.ExecuteReader();
                if (dr.Read())
                {
                    //  result.Text = "ID Exists";
                    //string std_name = (string)dr.GetValue(2).ToString();
                   // string[] word = std_name.Split(' ');

                    class_d.Text = (string)dr.GetValue(14).ToString().ToUpper();
                    session_d.Text = (string)dr.GetValue(3).ToString().ToUpper();
                    // term_d.Text = term1.Text; 
                    try
                    {
                        student_of_name.Text = (string)dr.GetValue(2).ToString().Substring(0, 25) + ".";
                    }
                    catch
                    {
                        student_of_name.Text = (string)dr.GetValue(2).ToString();
                    }
                    student_id.Text = (string)dr.GetValue(1).ToString().ToUpper();
                    dr.Close();
                    if (det.Text.Contains("mid"))
                    {
                        term_d.Text = term1.Text + " Term (Mid)";
                    }
                    else
                    {
                        term_d.Text = term1.Text + " Term";
                    }
                    // school_campus_query.Text = (string)dr.GetValue(4).ToString() + " ";
                    //  class_student.Text = (string)dr.GetValue(14).ToString() + " ";
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                cn2.Close();
                cmd2.Dispose();
            }

            cummulative_grade1.Text = "N/A";
            cummulative_remark1.Text = "N/A";
            //  ################################# MID TERM TEST EXECUTE CODE FOR ALL TERM #####################################################
            if (det.Text.Contains("mid"))
            {
                div_first_second_mid.Visible = true;
                MySqlConnection cn1 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                try
                {
                    cn1.Open();
                    // MySqlDataAdapter da = new MySqlDataAdapter("SELECT UPPER(Subject) AS Subject, Score_CA AS '1st CA Score',Score_CA1 AS '2nd CA Score',Score_Exam AS 'Exam Score',(Score_CA+Score_CA1+Score_Exam) AS 'Total Score',Grade,Remark,Teacher_Signature AS 'Subject Teacher Signature' FROM Table_Result WHERE Student_ID='" + student_id_query.Text + "' AND Session='" + session1.Text + "' AND Term='" + term1.Text + "'", cn1);
                   // MySqlDataAdapter da = new MySqlDataAdapter("SELECT Upper(Subject) AS 'Subject',Score_CA AS 'First CA Score(20)',Score_CA1 AS 'Second CA Score(20)',(Score_CA+Score_CA1) AS 'Total Score(40)',CASE when round((Score_CA+Score_CA1)/40*100,1) between '75' and '100' then 'A1' when round((Score_CA+Score_CA1)/40*100,1) between '70' AND '74.9' then 'B2' when round((Score_CA+Score_CA1)/40*100,1) between '65' AND '70.9' then 'B3' when round((Score_CA+Score_CA1)/40*100,1) between '60' AND '64.9' Then 'C4' when round((Score_CA+Score_CA1)/40*100,1) between '55' AND '60.9' then 'C5' when round((Score_CA+Score_CA1)/40*100,1) between '50' AND '54.9' then 'C6' when round((Score_CA+Score_CA1)/40*100,1) between '45' AND '49.9' then 'D7' when round((Score_CA+Score_CA1)/40*100,1) between '40' AND '44.9' then 'E8' ELSE 'F' END AS 'Grade',CASE when round((Score_CA+Score_CA1)/40*100,1) between '75' and '100' then 'Excellent' when round((Score_CA+Score_CA1)/40*100,1) between '70' AND '74.9' then 'Good' when round((Score_CA+Score_CA1)/40*100,1) between '65' AND '70.9' then 'Good' when round((Score_CA+Score_CA1)/40*100,1) between '60' AND '64.9' Then 'Credit' when round((Score_CA+Score_CA1)/40*100,1) between '55' AND '60.9' then 'Credit' when round((Score_CA+Score_CA1)/40*100,1) between '50' AND '54.9' then 'Credit' when round((Score_CA+Score_CA1)/40*100,1) between '45' AND '49.9' then 'Pass' when round((Score_CA+Score_CA1)/40*100,1) between '40' AND '44.9' then 'Pass' ELSE 'Weak' END AS 'Remark',Position,Teacher_Signature AS 'Signature' FROM Table_Result WHERE Student_ID='" + student_id_query.Text + "' AND Session='" + session1.Text + "' AND Term='" + term1.Text + "' ORDER BY Subject Asc", cn1);
                      MySqlDataAdapter da = new MySqlDataAdapter();
                      if (class_d.Text.Contains("Monte") || class_d.Text.Contains("monte") || class_d.Text.Contains("MONTE") || class_d.Text.Contains("Nursery One") || class_d.Text.Contains("NURSERY ONE") || (class_d.Text.Contains("NUR") && class_d.Text.Contains("ONE")) || class_d.Text.Contains("PRE") || class_d.Text.Contains("PLAY"))
                      {
                          da = new MySqlDataAdapter("SELECT Upper(Subject) AS 'Subject',Pre_Comment AS 'Comment/Remark',Teacher_Signature AS 'Signature' FROM Table_Result_mid WHERE Student_Name='" + student_name_query.Text + "' AND Session='" + session1.Text + "' AND Term='" + term1.Text + "' AND School='" + school_query.Text + "' ORDER BY Subject", cn1);
                      }
                      else
                      {
                          da = new MySqlDataAdapter("SELECT Upper(Subject) AS 'Subject',Score_CA AS 'CA Score(" + first_con.Text + ")',SCore_Exam AS 'Exam Score(" + exam_con.Text + ")',(Score_CA+Score_CA1+Score_Exam) AS 'Total Score(" + total_con.Text + ")',Grade,Remark,Teacher_Signature AS 'Signature' FROM Table_Result_mid WHERE Student_Name='" + student_name_query.Text + "' AND Session='" + session1.Text + "' AND Term='" + term1.Text + "' AND School='" + school_query.Text + "' ORDER BY Subject", cn1);
                      }
                    
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    cn1.Close();
                }



                MySqlConnection cn3 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                cn3.Open();
                MySqlCommand cmd3 = new MySqlCommand("SELECT Class,SUM(Score_CA+Score_CA1+Score_Exam),COUNT(p_id),SUM(Score_CA1),SUM(Score_Exam),SUM(Score_CA+Score_CA1+Score_Exam) FROM Table_Result_mid WHERE Student_Name='" + student_name_query.Text + "' AND Session='" + session1.Text + "' AND Term='" + term1.Text + "'", cn3);

                try
                {
                    string cum_percent = "0";
                    MySqlDataReader dr1 = cmd3.ExecuteReader();
                    if (dr1.Read())
                    {
                        //  result.Text = "ID Exists";
                        class_student.Text = (string)dr1.GetValue(0).ToString();
                        total_mark_obtainable1.Text = (string)dr1.GetValue(2).ToString();
                        total_mark_obtainable1.Text = (Convert.ToDecimal(total_mark_obtainable1.Text) * Convert.ToDecimal(100)).ToString(); //(Convert.ToDecimal(total_mark_obtainable1.Text) * Convert.ToDecimal(mid_total_con.Text)).ToString();
                        total_mark_obtained1.Text = (string)dr1.GetValue(1).ToString();
                        aggregate.Text = (string)dr1.GetValue(5).ToString();
                        cummulative_percent1.Text = (Math.Round(((Convert.ToDecimal(total_mark_obtained1.Text) / Convert.ToDecimal(total_mark_obtainable1.Text)) * 100), 1) + "%").ToString();
                        cum_percent = ((Convert.ToDecimal(total_mark_obtained1.Text) / Convert.ToDecimal(total_mark_obtainable1.Text)) * 100).ToString();
                        // }
                        txt_com_det.Text = cum_percent.ToString();
                        dr1.Close();

                        MySqlConnection cn_a3 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                        cn_a3.Open();
                        MySqlCommand cmd_a3 = new MySqlCommand("SELECT* FROM Table_Grade_Range WHERE School='" + school_query.Text + "' AND '" + Convert.ToDecimal(cum_percent) + "' BETWEEN from1 and to1", cn_a3);
                        MySqlDataReader dr_a3 = cmd_a3.ExecuteReader();
                        try
                        {
                            if (dr_a3.Read())
                            {
                               // cumm_grade2.Text = (string)dr_a3.GetValue(3).ToString();
                               // cumm_remark2.Text = (string)dr_a3.GetValue(4).ToString();
                                cumm_grade2.Text = (string)dr_a3.GetValue(3).ToString();
                                cumm_remark2.Text = (string)dr_a3.GetValue(4).ToString();
                                cummulative_grade1.Text = (string)dr_a3.GetValue(3).ToString();
                                cummulative_remark1.Text = (string)dr_a3.GetValue(4).ToString();
                                dr_a3.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
                        finally
                        {
                            cn_a3.Close();
                            cmd_a3.Dispose();
                            dr_a3.Close();
                        }

                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    cn3.Close();
                    cmd3.Dispose();
                }

            }


            //  ################################# EXAMINAMTION EXECUTE CODE FOR FIRST AND SECOND TERM #####################################################
            else if ((det.Text == "terminal" && term1.Text == "First") || (det.Text == "terminal" && term1.Text == "Second"))
            {
                div_first_second_mid.Visible = true;
                line_first_second.Visible = true;
                line_third.Visible = false;
                MySqlConnection cn1 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                try
                {
                    cn1.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    // if (class_d.Text.Contains("Mon") || class_d.Text.Contains("mon") || class_d.Text.Contains("MON") || class_d.Text.Contains("Nursery One") || class_d.Text.Contains("NURSERY ONE") || (class_d.Text.Contains("NUR") && class_d.Text.Contains("ONE")) || class_d.Text.Contains("PRE") || class_d.Text.Contains("PLAY"))
                    if (class_d.Text.Contains("Monte") || class_d.Text.Contains("monte") || class_d.Text.Contains("MONTE") || class_d.Text.Contains("Nursery One") || class_d.Text.Contains("NURSERY ONE") || (class_d.Text.Contains("NUR") && class_d.Text.Contains("ONE")) || class_d.Text.Contains("PLAY"))
                    {
                        da = new MySqlDataAdapter("SELECT Upper(Subject) AS 'Subject',Pre_Comment AS 'Comment/Remark',Teacher_Signature AS 'Signature' FROM Table_Result WHERE Student_Name='" + student_name_query.Text + "' AND Session='" + session1.Text + "' AND Term='" + term1.Text + "' ORDER BY Subject", cn1);
                    }
                    else
                    {
                        da = new MySqlDataAdapter("SELECT Upper(Subject) AS 'Subject',Score_CA AS 'First CA Score(" + first_con.Text + ")',SCore_Exam AS 'Exam Score(" + exam_con.Text + ")',(Score_CA+Score_CA1+Score_Exam) AS 'Total Score(" + total_con.Text + ")',Grade,Remark,Teacher_Signature AS 'Signature' FROM Table_Result WHERE Student_Name='" + student_name_query.Text + "' AND Session='" + session1.Text + "' AND Term='" + term1.Text + "' ORDER BY Subject", cn1);
                    }
                       
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                catch (Exception ex)
                {
                    // Response.Write(ex.Message);
                }
                finally
                {
                    cn1.Close();
                }



                MySqlConnection cn3 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                cn3.Open();
                MySqlCommand cmd3 = new MySqlCommand("SELECT Class,SUM(Score_CA),COUNT(p_id),SUM(Score_CA1),SUM(Score_Exam),SUM(Score_CA+Score_CA1+Score_Exam) FROM Table_Result WHERE Student_Name='" + student_name_query.Text + "' AND Session='" + session1.Text + "' AND Term='" + term1.Text + "'", cn3);

                try
                {
                    string cum_percent = "0";
                    MySqlDataReader dr1 = cmd3.ExecuteReader();
                    if (dr1.Read())
                    {
                        //  result.Text = "ID Exists";
                        class_student.Text = (string)dr1.GetValue(0).ToString();
                        total_mark_obtainable1.Text = (string)dr1.GetValue(2).ToString();
                        total_mark_obtainable1.Text = (Convert.ToDecimal(total_mark_obtainable1.Text) * Convert.ToDecimal(total_con.Text)).ToString();
                        // total_mark_obtainable.Text = (Convert.ToInt32(total_mark_obtainable.Text) * 100).ToString();
                        total_mark_obtained1.Text = (string)dr1.GetValue(5).ToString();//(Convert.ToDecimal((decimal)dr1.GetValue(1)) + Convert.ToDecimal((decimal)dr1.GetValue(3)) + Convert.ToDecimal((decimal)dr1.GetValue(4))).ToString();
                        aggregate.Text = (string)dr1.GetValue(5).ToString(); //(Convert.ToDecimal((decimal)dr1.GetValue(1)) + Convert.ToDecimal((decimal)dr1.GetValue(3)) + Convert.ToDecimal((decimal)dr1.GetValue(5))).ToString();
                        cummulative_percent1.Text = (Math.Round(((Convert.ToDecimal(total_mark_obtained1.Text) / Convert.ToDecimal(total_mark_obtainable1.Text)) * 100), 1) + "%").ToString();
                        cum_percent = ((Convert.ToDecimal(total_mark_obtained1.Text) / Convert.ToDecimal(total_mark_obtainable1.Text)) * 100).ToString();
                        // }

                        txt_com_det.Text = cum_percent.ToString();
                        dr1.Close();
                        MySqlConnection cn_a2 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                        cn_a2.Open();
                        MySqlCommand cmd_a2 = new MySqlCommand("SELECT* FROM Table_Grade_Range WHERE School='" + school_query.Text + "' AND '" + Convert.ToDecimal(cum_percent) + "' BETWEEN from1 and to1", cn_a2);
                        MySqlDataReader dr_a2 = cmd_a2.ExecuteReader();
                        try
                        {
                            if (dr_a2.Read())
                            {
                                cumm_grade2.Text = (string)dr_a2.GetValue(3).ToString();
                                cumm_remark2.Text = (string)dr_a2.GetValue(4).ToString();
                                cummulative_grade1.Text = (string)dr_a2.GetValue(3).ToString();
                                cummulative_remark1.Text = (string)dr_a2.GetValue(4).ToString();
                                dr_a2.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
                        finally
                        {
                            cn_a2.Close();
                            cmd_a2.Dispose();
                            dr_a2.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    cn3.Close();
                    cmd3.Dispose();
                }
            }








             //  ################################# EXAMINAMTION EXECUTE CODE FOR THIRD TERM #####################################################
            else //if((det.Text == "terminal" && term1.Text == "Third"))
            {
                div_third_only.Visible = true;
                line_first_second.Visible = false;
                line_third.Visible = true;
                MySqlConnection cn1 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                try
                {
                    cn1.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    //if (class_d.Text.Contains("Mon") || class_d.Text.Contains("mon") || class_d.Text.Contains("MON") || class_d.Text.Contains("Nursery One") || class_d.Text.Contains("NURSERY ONE") || (class_d.Text.Contains("NUR") && class_d.Text.Contains("ONE")) || class_d.Text.Contains("PRE") || class_d.Text.Contains("PLAY"))
                    if (class_d.Text.Contains("Monte") || class_d.Text.Contains("monte") || class_d.Text.Contains("MONTE") || class_d.Text.Contains("Nursery One") || class_d.Text.Contains("NURSERY ONE") || (class_d.Text.Contains("NUR") && class_d.Text.Contains("ONE")) || class_d.Text.Contains("PLAY"))
                    {
                        da = new MySqlDataAdapter("SELECT Upper(Subject) AS 'Subject',Pre_Comment AS 'Comment/Remark',Teacher_Signature AS 'Signature' FROM Table_Result WHERE Student_Name='" + student_name_query.Text + "' AND Session='" + session1.Text + "' AND Term='" + term1.Text + "' ORDER BY Subject", cn1);
                    }
                    else
                    {
                        da = new MySqlDataAdapter("SELECT Upper(Subject) AS 'Subject',Score_CA AS 'First CA Score(" + first_con.Text + ")',SCore_Exam AS 'Exam Score(" + exam_con.Text + ")',(Score_CA+Score_CA1+Score_Exam) AS 'Total Score(" + total_con.Text + ")',Grade,Remark,Teacher_Signature AS 'Signature' FROM Table_Result WHERE Student_Name='" + student_name_query.Text + "' AND Session='" + session1.Text + "' AND Term='" + term1.Text + "' ORDER BY Subject", cn1);
                    }
                      
                     DataTable dt = new DataTable();
                    da.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                catch (Exception ex)
                {
                    // Response.Write(ex.Message);
                }
                finally
                {
                    cn1.Close();
                }



                MySqlConnection cn3 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                cn3.Open();
                // MySqlCommand cmd3 = new MySqlCommand("SELECT Class,SUM(Score_CA),COUNT(p_id),SUM(Score_CA1),SUM(Score_Exam),SUM(Score_CA+Score_CA1+Score_Exam) FROM Table_Result WHERE Student_Name='" + student_name_query.Text + "' AND Session='" + session1.Text + "' AND Term='" + term1.Text + "'", cn3);
                MySqlCommand cmd3 = new MySqlCommand("SELECT count(p_id),Class,student_name,Student_id,(select round((sum(score_ca+score_ca1+score_exam)/(count(p_id)*" + Convert.ToDecimal(total_con.Text) + "))*100,1) from table_result where Student_Name='" + student_name_query.Text + "' and term='First' and session='" + session1.Text + "') as 'First term score',(select round((sum(score_ca+score_ca1+score_exam)/(count(p_id)*" + Convert.ToDecimal(total_con.Text) + "))*100,1) from table_result where Student_Name='" + student_name_query.Text + "' and term='second' and session='" + session1.Text + "') as 'second term score',(select round((sum(score_ca+score_ca1+score_ca1+score_exam)/(count(p_id)*" + Convert.ToDecimal(total_con.Text) + "))*100,1) from table_result where Student_Name='" + student_name_query.Text + "' and term='third' and session='" + session1.Text + "') as 'Third term score',sum(score_ca+score_ca1+score_exam) as 'third term score(specific)' FROM `table_result` WHERE Student_Name='" + student_name_query.Text + "' and session='" + session1.Text + "' and Term='third'", cn3);

                try
                {
                    string cum_percent = "0";
                    MySqlDataReader dr1 = cmd3.ExecuteReader();
                    if (dr1.Read())
                    {
                        class_student.Text = (string)dr1.GetValue(1).ToString();
                        total_mark_obtainable2.Text = (string)dr1.GetValue(0).ToString();
                        total_mark_obtainable2.Text = (Convert.ToDecimal(total_mark_obtainable2.Text) * Convert.ToDecimal(total_con.Text)).ToString();
                        total_mark_obtained2.Text = (string)dr1.GetValue(7).ToString();
                        a.Text = (string)dr1.GetValue(4).ToString();
                        b.Text = (string)dr1.GetValue(5).ToString();
                        c.Text = (string)dr1.GetValue(6).ToString();
                        if (string.IsNullOrWhiteSpace(a.Text))
                        {
                            cummulative_first.Text = "N/A";
                            a.Text = "0";
                        }
                        else
                        {
                            cummulative_first.Text = a.Text + "%";
                        }
                        if (string.IsNullOrWhiteSpace(b.Text))
                        {
                            cummulative_second.Text = " N/A";
                            b.Text = "0";
                        }
                        else
                        {
                            cummulative_second.Text = b.Text + "%";
                        }
                        if (string.IsNullOrWhiteSpace(c.Text))
                        {
                            cummulative_third.Text = "N/A";
                            c.Text = "0";
                        }
                        else
                        {
                            cummulative_third.Text = c.Text + "%";
                        }
                        total_mark_obtained2.Text = (string)dr1.GetValue(7).ToString(); //(Convert.ToDecimal(a.Text) + Convert.ToDecimal(b.Text) + Convert.ToDecimal(c.Text)).ToString();
                        aggregate2.Text = (Math.Round(((Convert.ToDecimal(total_mark_obtained2.Text) / Convert.ToDecimal(total_mark_obtainable2.Text)) * 100), 1) + "%").ToString();// (string)dr1.GetValue(7).ToString();   // total_mark_obtained.Text = total_mark_obtained.Text;//(string)dr1.GetValue(5).ToString();
                        //cummulative_percent.Text = (Math.Round(((Convert.ToDecimal(total_mark_obtained.Text) / Convert.ToDecimal(total_mark_obtainable.Text)) * 100), 1) + "%").ToString();
                       // cum_percent = Math.Round(((Convert.ToDecimal(a.Text) + Convert.ToDecimal(b.Text) + Convert.ToDecimal(c.Text)) / 3), 1).ToString();  // ((Convert.ToDecimal(total_mark_obtained.Text) / Convert.ToDecimal(total_mark_obtainable.Text)) * 100).ToString();
                        if (cummulative_first.Text.Contains("N/") && !cummulative_second.Text.Contains("N/") && !cummulative_third.Text.Contains("N/"))
                        {
                            cum_percent = Math.Round(((Convert.ToDecimal(a.Text) + Convert.ToDecimal(b.Text) + Convert.ToDecimal(c.Text)) / 2), 1).ToString();  // ((Convert.ToDecimal(total_mark_obtained.Text) / Convert.ToDecimal(total_mark_obtainable.Text)) * 100).ToString();
                        }
                        else if (!cummulative_first.Text.Contains("N/") && cummulative_second.Text.Contains("N/") && !cummulative_third.Text.Contains("N/"))
                        {
                            cum_percent = Math.Round(((Convert.ToDecimal(a.Text) + Convert.ToDecimal(b.Text) + Convert.ToDecimal(c.Text)) / 2), 1).ToString();  // ((Convert.ToDecimal(total_mark_obtained.Text) / Convert.ToDecimal(total_mark_obtainable.Text)) * 100).ToString();
                        }
                        else if (cummulative_first.Text.Contains("N/") && cummulative_second.Text.Contains("N/") && !cummulative_third.Text.Contains("N/"))
                        {
                            cum_percent = Math.Round(((Convert.ToDecimal(a.Text) + Convert.ToDecimal(b.Text) + Convert.ToDecimal(c.Text)) / 1), 1).ToString();  // ((Convert.ToDecimal(total_mark_obtained.Text) / Convert.ToDecimal(total_mark_obtainable.Text)) * 100).ToString();
                        }


                        else if (!cummulative_first.Text.Contains("N/") && !cummulative_second.Text.Contains("N/") && cummulative_third.Text.Contains("N/"))
                        {
                            cum_percent = Math.Round(((Convert.ToDecimal(a.Text) + Convert.ToDecimal(b.Text) + Convert.ToDecimal(c.Text)) / 2), 1).ToString();  // ((Convert.ToDecimal(total_mark_obtained.Text) / Convert.ToDecimal(total_mark_obtainable.Text)) * 100).ToString();
                        }
                        else if (cummulative_first.Text.Contains("N/") && !cummulative_second.Text.Contains("N/") && !cummulative_third.Text.Contains("N/"))
                        {
                            cum_percent = Math.Round(((Convert.ToDecimal(a.Text) + Convert.ToDecimal(b.Text) + Convert.ToDecimal(c.Text)) / 2), 1).ToString();  // ((Convert.ToDecimal(total_mark_obtained.Text) / Convert.ToDecimal(total_mark_obtainable.Text)) * 100).ToString();
                        }
                        else if (cummulative_first.Text.Contains("N/") && !cummulative_second.Text.Contains("N/") && cummulative_third.Text.Contains("N/"))
                        {
                            cum_percent = Math.Round(((Convert.ToDecimal(a.Text) + Convert.ToDecimal(b.Text) + Convert.ToDecimal(c.Text)) / 1), 1).ToString();  // ((Convert.ToDecimal(total_mark_obtained.Text) / Convert.ToDecimal(total_mark_obtainable.Text)) * 100).ToString();
                        }

                        else if (!cummulative_first.Text.Contains("N/") && !cummulative_second.Text.Contains("N/") && cummulative_third.Text.Contains("N/"))
                        {
                            cum_percent = Math.Round(((Convert.ToDecimal(a.Text) + Convert.ToDecimal(b.Text) + Convert.ToDecimal(c.Text)) / 2), 1).ToString();  // ((Convert.ToDecimal(total_mark_obtained.Text) / Convert.ToDecimal(total_mark_obtainable.Text)) * 100).ToString();
                        }
                        else if (!cummulative_first.Text.Contains("N/") && cummulative_second.Text.Contains("N/") && !cummulative_third.Text.Contains("N/"))
                        {
                            cum_percent = Math.Round(((Convert.ToDecimal(a.Text) + Convert.ToDecimal(b.Text) + Convert.ToDecimal(c.Text)) / 2), 1).ToString();  // ((Convert.ToDecimal(total_mark_obtained.Text) / Convert.ToDecimal(total_mark_obtainable.Text)) * 100).ToString();
                        }
                        else if (!cummulative_first.Text.Contains("N/") && cummulative_second.Text.Contains("N/") && cummulative_third.Text.Contains("N/"))
                        {
                            cum_percent = Math.Round(((Convert.ToDecimal(a.Text) + Convert.ToDecimal(b.Text) + Convert.ToDecimal(c.Text)) / 1), 1).ToString();  // ((Convert.ToDecimal(total_mark_obtained.Text) / Convert.ToDecimal(total_mark_obtainable.Text)) * 100).ToString();
                        }

                        else if (!cummulative_first.Text.Contains("N/") && !cummulative_second.Text.Contains("N/") && !cummulative_third.Text.Contains("N/"))
                        {
                            cum_percent = Math.Round(((Convert.ToDecimal(a.Text) + Convert.ToDecimal(b.Text) + Convert.ToDecimal(c.Text)) / 3), 1).ToString();  // ((Convert.ToDecimal(total_mark_obtained.Text) / Convert.ToDecimal(total_mark_obtainable.Text)) * 100).ToString();
                        }
                        session_average2.Text = cum_percent + "%";
                        /* class_student.Text = (string)dr1.GetValue(0).ToString();
                         total_mark_obtainable2.Text = (string)dr1.GetValue(2).ToString();
                         total_mark_obtainable2.Text = (Convert.ToDecimal(total_mark_obtainable2.Text) * 100).ToString();
                         total_mark_obtained2.Text = (string)dr1.GetValue(5).ToString();
                         aggregate2.Text = (string)dr1.GetValue(5).ToString();
                         cummulative_percent1.Text = (Math.Round(((Convert.ToDecimal(total_mark_obtained2.Text) / Convert.ToDecimal(total_mark_obtainable2.Text)) * 100), 1) + "%").ToString();
                         cum_percent = ((Convert.ToDecimal(total_mark_obtained2.Text) / Convert.ToDecimal(total_mark_obtainable2.Text)) * 100).ToString();
                         // }*/

                        txt_com_det.Text = cum_percent.ToString();
                        dr1.Close();

                        MySqlConnection cn_a1 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                        cn_a1.Open();
                        MySqlCommand cmd_a1 = new MySqlCommand("SELECT* FROM Table_Grade_Range WHERE School='" + school_query.Text + "' AND '" + Convert.ToDecimal(cum_percent) + "' BETWEEN from1 and to1", cn_a1);
                        MySqlDataReader dr_a1 = cmd_a1.ExecuteReader();
                        try
                        {
                            if (dr_a1.Read())
                            {
                                cumm_grade2.Text = (string)dr_a1.GetValue(3).ToString();
                                cumm_remark2.Text = (string)dr_a1.GetValue(4).ToString();
                                dr_a1.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
                        finally
                        {
                            cn_a1.Close();
                            cmd_a1.Dispose();
                            dr_a1.Close();
                        }

                     
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    cn3.Close();
                    cmd3.Dispose();
                }



                p_promotion_status.Visible = true;
                string ps = ((Convert.ToDecimal(a.Text) + Convert.ToDecimal(b.Text) + Convert.ToDecimal(c.Text)) / 3).ToString();  // ((Convert.ToDecimal(total_mark_obtained.Text) / Convert.ToDecimal(total_mark_obtainable.Text)) * 100).ToString();
                MySqlConnection cn18 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                cn18.Open();
                MySqlCommand cmd18 = new MySqlCommand("SELECT* FROM Table_promotion_Range WHERE Session='" + session1.Text + "' AND School='" + school_query.Text + "' AND '" + Convert.ToDecimal(ps) + "' BETWEEN from1 and to1", cn18);
                MySqlDataReader dr18 = cmd18.ExecuteReader();
                try
                {
                    if (dr18.Read())
                    {
                        //  result.Text = "ID Exists";
                        promotion_status.Text = " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " + (string)dr18.GetValue(3).ToString() + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ";
                        dr18.Close();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    cn18.Close();
                    cmd18.Dispose();
                    dr18.Close();
                }





                MySqlConnection cn17 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                cn17.Open();
                MySqlCommand cmd17 = new MySqlCommand("SELECT* FROM Table_promotion_status WHERE Student_Name='" + student_name_query.Text + "' AND School='" + school_query.Text + "' and session='" + session1.Text + "' and term='" + term1.Text + "' and Promotion_comment!=''", cn17);
                MySqlDataReader dr17 = cmd17.ExecuteReader();
                try
                {
                    if (dr17.Read())
                    {
                        //  result.Text = "ID Exists";
                        promotion_status.Text = " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " + (string)dr17.GetValue(5).ToString() + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ";
                        dr17.Close();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    cn17.Close();
                    cmd17.Dispose();
                    dr17.Close();
                }

            }
















            // ###################################### INDEPENDENT GENERAL ISSUES CODE ######################################################3
            MySqlConnection cn11 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            cn11.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(p_id) FROM Table_Student WHERE Class='" + class_student.Text + "' AND School='" + school_query.Text + "'", cn11);

            try
            {
                MySqlDataReader dr2 = cmd.ExecuteReader();
                if (dr2.Read())
                {
                    //  result.Text = "ID Exists";
                    no_of_student.Text = (string)dr2.GetValue(0).ToString();
                    dr2.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                cn11.Close();
                cmd.Dispose();
            }


            MySqlConnection cn22 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            cn22.Open();
            string aw = null;

            if (det.Text.Contains("mid"))
            {
                aw = "SELECT remark_teacher,remark_principal FROM Table_Comment_Term_mid WHERE Student_Name='" + student_name_query.Text + "' AND Session='" + session1.Text + "' AND Term='" + term1.Text + "' AND School='" + school_query.Text + "'";
            }
            else
            {
                aw = "SELECT remark_teacher,remark_principal FROM Table_Comment_Term WHERE Student_Name='" + student_name_query.Text + "' AND Session='" + session1.Text + "' AND Term='" + term1.Text + "' AND School='" + school_query.Text + "'";
            }
                
            MySqlCommand cmd22 = new MySqlCommand(aw, cn22);
            comment_teacher.Text = null;
            comment_principal1.Text = null;
            try
            {
                MySqlDataReader dr22 = cmd22.ExecuteReader();
                if (dr22.Read())
                {

                    //  result.Text = "ID Exists";
                    comment_teacher.Text = (string)dr22.GetValue(0).ToString();
                    comment_principal1.Text = (string)dr22.GetValue(1).ToString();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                cn22.Close();
                cmd22.Dispose();
            }

            MySqlConnection cn8 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            cn8.Open();
            MySqlCommand cmd8 = new MySqlCommand("SELECT* FROM table_configuration Where name_of_school='" + school_query.Text + "'", cn8);
            //gclass.display_in_box("SELECT* FROM table_configuration Where name_of_school='" + school_query.Text + "'");
            MySqlDataReader dr8 = cmd8.ExecuteReader();
            try
            {
                if (dr8.Read())
                {
                    school.Text = school_query.Text; //(string)dr8.GetValue(1).ToString();
                    label_school_to_display.Text = school_query.Text;
                    address.Text = (string)dr8.GetValue(2).ToString() + ", " + (string)dr8.GetValue(3).ToString() + ", " + (string)dr8.GetValue(4).ToString() + ". " + (string)dr8.GetValue(5).ToString();
                    //city.Text = (string)dr8.GetValue(3).ToString();
                    // state.Text = (string)dr8.GetValue(4).ToString();
                    // country.Text = (string)dr8.GetValue(5).ToString();
                    email_website.Text = "Email: " + (string)dr8.GetValue(7).ToString() + " || WEBSITE: " + (string)dr8.GetValue(11).ToString();
                    phone.Text = "Phone: " + (string)dr8.GetValue(6).ToString();
                    // email.Text = (string)dr8.GetValue(7).ToString();
                    // website.Text = (string)dr8.GetValue(11).ToString();
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

            Image1.ImageUrl = "images/" + school_query.Text.Replace(" ", "_") + "_signature.png"; //"images/High.jpg";

            Image_logo.ImageUrl = "images/students/logo.jpg";
            if (school_query.Text.Contains("High") || school_query.Text.Contains("College") || school_query.Text.Contains("Secondary") || school_query.Text.Contains("Gramm") || school_query.Text.Contains("Advance"))
            {
                label_under_signature.Text = "Principal's Signature";
                label_comment_head.Text = "Principal's Comment"; 
            }
            else
            {
                label_under_signature.Text = "Head Master's Signature";
                label_comment_head.Text = "Head Master's Comment";
            }
  



            MySqlConnection cn13 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            cn13.Open();
            try
            {
                string ay=null;
                if (det.Text.Contains("mid"))
                {
                    ay = "SELECT* FROM Table_Trait_mid WHERE Student_Name='" + student_name_query.Text + "' AND Session='" + session1.Text + "' AND Term='" + term1.Text + "' AND School='" + school_query.Text + "'";
                }
                else
                {
                    ay = "SELECT* FROM Table_Trait WHERE Student_Name='" + student_name_query.Text + "' AND Session='" + session1.Text + "' AND Term='" + term1.Text + "' AND School='" + school_query.Text + "'";
                }
                
                MySqlDataAdapter da = new MySqlDataAdapter(ay, cn13);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView_trait.DataSource = dt;
                GridView_trait.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                cn13.Close();
            }



            try
            {
                 MySqlConnection cn14 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                 cn14.Open();
                 string ax = null; ;
                 if (det.Text.Contains("mid"))
                 {
                     ax = "SELECT* FROM Table_Comment_Term_mid WHERE Student_Name='" + student_name_query.Text + "' AND Session='" + session1.Text + "' AND Term='" + term1.Text + "' AND School='" + school_query.Text + "' and no_of_time_school_open>0";
                 }
                 else
                 {
                     ax = "SELECT* FROM Table_Comment_Term WHERE Student_Name='" + student_name_query.Text + "' AND Session='" + session1.Text + "' AND Term='" + term1.Text + "' AND School='" + school_query.Text + "' and no_of_time_school_open>0";
                 }
                 MySqlCommand cmd14 = new MySqlCommand(ax, cn14);
                // MySqlCommand cmd14 = new MySqlCommand("SELECT* FROM Table_Comment_Term WHERE Student_Name='" + student_name_query.Text + "' AND Session='" + session1.Text + "' AND Term='" + term1.Text + "' and no_of_time_school_open>0", cn14);
           
                 try
                 {
                     txt_open.Text = "0"; txt_present.Text = "0";
                     MySqlDataReader dr5 = cmd14.ExecuteReader();
                     if (dr5.Read())
                     {

                         DataTable dt1 = new DataTable();
                         dt1.Columns.Add("School Count", typeof(string));
                         dt1.Columns.Add("Frequency", typeof(string));

                         txt_open.Text = (string)dr5.GetValue(9).ToString();
                         txt_present.Text = (string)dr5.GetValue(10).ToString();

                         dt1.Rows.Add("No. of Time School Open", txt_open.Text);
                         dt1.Rows.Add("No. of Time Present", txt_present.Text);
                         dt1.Rows.Add("No. of Time Absent", Convert.ToString(Convert.ToInt32(txt_open.Text)-Convert.ToInt32(txt_present.Text)));

                         GridView_time_open.DataSource = dt1;
                         GridView_time_open.DataBind();
                     }
                     else
                     {
                         DataTable dt1 = new DataTable();
                         dt1.Columns.Add("School Count", typeof(string));
                         dt1.Columns.Add("Frequency", typeof(string));

                         dt1.Rows.Add("No. of Time School Open", ("NIL"));
                         dt1.Rows.Add("No. of Time Present", ("NIL"));
                         dt1.Rows.Add("No. of Time Absent", ("NIL"));

                         GridView_time_open.DataSource = dt1;
                         GridView_time_open.DataBind();
                     }
                 }
                 catch (Exception ex)
                 {
                     result.Text = ex.Message;
                 }
                 finally
                 {
                     cn14.Close(); cn14.Dispose();
                     cmd14.Dispose();
                 }
            }
            catch (Exception ex)
            {
                result.Text = ex.Message;
            }

            




      
            try
            {
                Image3.ImageUrl = "http://"+HttpContext.Current.Request.Url.Authority + "/images/students/" + student_id.Text.Replace("/", "-") + ".jpg";  //"http://bsgs.schoolnigeria.com.ng/images/students/" + student_id.Text.Replace("/", "-") + ".jpg";
            }
            catch
            {
                Image3.ImageUrl = "http://" + HttpContext.Current.Request.Url.Authority + "/images/students/PORTAL.jpg";      //Server.MapPath(@"~/images/students/LOGO.jpg"); //"http://bsgs.schoolnigeria.com.ng/images/students/LOGO.jpg";
            }
            Panel2.Visible = false;


            if (det.Text.Contains("mid"))
            {
                label12.Visible = false;
                next_term_begins.Visible = false;
            }
            else
            {
                label12.Visible = false;
                next_term_begins.Visible = false;
            }

            MySqlConnection cn19 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            cn19.Open();
            string az=null;
            if (det.Text.Contains("mid"))
            {
                az = "SELECT* FROM Table_Resumption_mid WHERE Session='" + session1.Text + "' AND Term='" + term1.Text + "' AND School='" + school_query.Text + "'";
            }
            else
            {
                az = "SELECT* FROM Table_Resumption WHERE Session='" + session1.Text + "' AND Term='" + term1.Text + "' AND School='" + school_query.Text + "'";
            }
            MySqlCommand cmd19 = new MySqlCommand(az, cn19);
           // MySqlCommand cmd19 = new MySqlCommand("SELECT* FROM Table_Resumption WHERE Session='" + session1.Text + "' AND Term='" + term1.Text + "' AND School='" + school_query.Text + "'", cn19);
            MySqlDataReader dr19 = cmd19.ExecuteReader();
            try
            {
                if (dr19.Read())
                {
                    //  result.Text = "ID Exists";
                    next_term_begins.Text = " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " + (string)dr19.GetValue(1).ToString() + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ";
                    dr19.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                cn19.Close();
                cmd19.Dispose();
                dr19.Close();
            }



            if (string.IsNullOrWhiteSpace(comment_principal1.Text)) //txt_com_det.Text = cum_percent.ToString();
            {
                MySqlConnection cn_a1 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                cn_a1.Open();
                MySqlCommand cmd_a1 = new MySqlCommand("SELECT* FROM Table_Default_Comment WHERE School='" + school_query.Text + "' AND '" + Convert.ToDecimal(txt_com_det.Text) + "' BETWEEN from1 and to1 ORDER BY p_id Desc", cn_a1);
                MySqlDataReader dr_a1 = cmd_a1.ExecuteReader();
                try
                {
                    if (dr_a1.Read())
                    {
                        comment_principal1.Text = (string)dr_a1.GetValue(3).ToString();
                        dr_a1.Close();
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    cn_a1.Close();
                    cmd_a1.Dispose();
                    dr_a1.Close();
                }
            }




            next_term_begins.Visible = true;
            label12.Visible = true;
            p_resumption_date.Visible = true;

            Panel2.Visible = true;
            Panel1.Visible = true;

            ///////////////////////////////////////////////////////////////////////////////// START OF DET TO DISPLAY OR NOT ////////////////
            if (class_d.Text.Contains("Monte") || class_d.Text.Contains("monte") || class_d.Text.Contains("MONTE") || class_d.Text.Contains("Nursery One") || class_d.Text.Contains("NURSERY ONE") || (class_d.Text.Contains("NUR") && class_d.Text.Contains("ONE")) || class_d.Text.Contains("PRE") || class_d.Text.Contains("PLAY"))
            {
                div_first_second_mid.Visible = false;
                div_third_only.Visible = false;
            }
            else
            {
                if ((det.Text == "terminal" && term1.Text == "First") || (det.Text == "terminal" && term1.Text == "Second"))
                {
                    div_first_second_mid.Visible = true;
                    line_first_second.Visible = true;
                    line_third.Visible = false;
                }
                else if (det.Text.Contains("mid"))
                {
                    div_first_second_mid.Visible = true;
                    line_first_second.Visible = true;
                    line_third.Visible = false;
                }
                else 
                {
                    div_third_only.Visible = true;
                    line_first_second.Visible = false;
                    line_third.Visible = true;
                }
            }
            ///////////////////////////////////////////////////////////////////////////////// END OF DET TO DISPLAY OR NOT ////////////////
          
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            HttpCookie member_id = new HttpCookie("cookie_session");
            member_id.Value = "0000";
            member_id.Expires = DateTime.Now.AddHours(-1);
            Response.SetCookie(member_id);
            Response.Redirect("Home/Index");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void school_campus_query_TextChanged(object sender, EventArgs e)
        {

        }
    }
}