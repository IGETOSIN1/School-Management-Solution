using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace WEB_SCONET_MANAGEMENT
{
    public partial class Upload_Image : System.Web.UI.Page
    {
        General_Class gclass = new General_Class();
        string subdomain = " ";
        string cstring = " ";
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                school.Text = Request.Cookies["cookie_school"].Value.ToString();
                department.Text = Request.Cookies["cookie_department"].Value.ToString();
                users.Text = Request.Cookies["cookie_users"].Value.ToString();
                user_name.Text = Request.Cookies["cookie_user_name"].Value.ToString();

                string[] word = users.Text.Split(' ');
                string surname = word[word.Length - 1].ToString();
                string initial = " ." + word[word.Length - 2].First() + ".";
                TextBox1.Text = surname.Substring(0, 1).ToUpper() + surname.Substring(1).ToLower() + initial.ToUpper();

                //  TextBox2.Text = Request.Cookies["reg_cat"].Value.ToString();
            }
            catch (Exception ex)
            {
                // Response.Write(ex.Message);
                Response.Redirect("Login.aspx");
            }

            string dept = Request.Cookies["cookie_department"].Value.ToString();
            string statu = Request.Cookies["cookie_cat_status"].Value.ToString();
            if (!dept.Contains("schoo") && !statu.Contains("Super Sup"))
            {
                Response.Redirect("Home/Deny");
            }

            //////////////////////////// END OF DETERMINATION OF SUB-DOMAIN AND CONNECTION //////////////////////////////////////////////////
            if (cstring == " " && subdomain == " ")
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

                MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring_v"].ConnectionString);
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT connection_string FROM Table_Verify_Schoolms WHERE sub_domain like '%" + subdomain + "%'", cn);
                MySqlDataReader dr = null;
                try
                {

                    dr = cmd.ExecuteReader();   //gclass.display_in_box("SELECT* FROM Table_Student WHERE Full_Name='" + student_name_query.Text + "' AND School='" + school.Text + "'");
                    if (dr.Read())
                    {
                        cstring = (string)dr.GetValue(0).ToString();
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
            }

            //////////////////////////// END OF DETERMINATION OF SUB-DOMAIN AND CONNECTION //////////////////////////////////////////////////




            if (string.IsNullOrWhiteSpace(display_student.Text) || string.IsNullOrWhiteSpace(display_class.Text))
            {
                gclass.display_in_combobox("SELECT DISTINCT CLASS FROM Table_Class WHERE School='" + school.Text + "' ORDER BY Class", CLASS, "Class");
               
                CLASS.SelectedIndex = -1;
                display_class.Text = "Displayed";

                gclass.display_in_combobox("SELECT DISTINCT Full_Name FROM Table_Student WHERE School='" + school.Text + "' ORDER BY Full_Name ASC", student_name_query, "Full_Name");

                student_name_query.SelectedIndex = -1;
                display_student.Text = "Displayed";
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            student_id.Text = null;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (!FileUpload1.HasFile)
            {
                result_output.Text = " Select Student's Image/ Picture to Upload ... ";
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script> alert(' Select Student's Image/ Picture to Upload ... ')</script>");
               
            }
            else if (System.IO.Path.GetExtension(FileUpload1.FileName).ToLower() != ".jpg"
                && Path.GetExtension(FileUpload1.FileName).ToLower() != ".png"
                && Path.GetExtension(FileUpload1.FileName).ToLower() != ".gif"
                && Path.GetExtension(FileUpload1.FileName).ToLower() != ".jpeg")
            {
                result_output.Text = "Uploaded File must be an Image ...";
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script> alert('Uploaded File must be an Image ...');</script>");
               
            }
          
            else
            {
                MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                cn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT* FROM Table_Student WHERE Full_Name='" + student_name_query.Text + "' AND School='" + school.Text + "'", cn);
                MySqlDataReader dr = null;
                try
                {
                    student_id_query.Text = null;
                    dr = cmd.ExecuteReader();   //gclass.display_in_box("SELECT* FROM Table_Student WHERE Full_Name='" + student_name_query.Text + "' AND School='" + school.Text + "'");
                    if (dr.Read())
                    {
                        student_id_query.Text = (string)dr.GetValue(1).ToString();
                        student_id.Text = (string)dr.GetValue(1).ToString();
                        CLASS.Text = (string)dr.GetValue(6).ToString();
                        dr.Close();
                    }
                    string abs = student_id.Text.Replace("/", "-");
                    FileUpload1.PostedFile.SaveAs(Server.MapPath("~/images/students/" + abs.ToString() + ".jpg"));
                    result_output.Text = "Image Successfully Uploaded ...";
                    student_id.Text = null;
                    ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script> alert('Image Successfully Uploaded ...');</script>");

                }
                catch (Exception ex)
                {
                    result_output.Text = ex.Message;
                }
                finally
                {
                    cn.Close();
                    cmd.Dispose();
                    dr.Close();
                }




            }
        }

        protected void CLASS_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = "SELECT Full_Name FROM Table_Student WHERE School='" + school.Text + "' AND Class='" + CLASS.Text + "' ORDER BY Full_Name ASC";
            gclass.display_in_combobox(str, student_name_query, "Full_Name");
            student_name_query.SelectedIndex = -1;
        }

        protected void CLASS_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string str = "SELECT Full_Name FROM Table_Student WHERE School='" + school.Text + "' AND Class='" + CLASS.Text + "' ORDER BY Full_Name ASC";
            gclass.display_in_combobox(str, student_name_query, "Full_Name");
            student_name_query.SelectedIndex = -1;
        }

        protected void student_name_query_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (!FileUpload2.HasFile)
            {
                result_output.Text = " Select Logo to Upload ... ";
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script> alert(' Select Logo to Upload ... ')</script>");

            }
            else if (System.IO.Path.GetExtension(FileUpload2.FileName).ToLower() != ".jpg"
                && Path.GetExtension(FileUpload2.FileName).ToLower() != ".png"
                && Path.GetExtension(FileUpload2.FileName).ToLower() != ".gif"
                && Path.GetExtension(FileUpload2.FileName).ToLower() != ".jpeg")
            {
                result_output.Text = "Uploaded File must be in Image Format ...";
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script> alert('Uploaded File must be in Image Format ...');</script>");

            }

            else
            {
                MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                cn.Open();

                try
                {
                    //FileUpload2.PostedFile.SaveAs(Server.MapPath("~/images/students/" + school.Text.Replace(" ", "_").Replace("/", "") + "_logo.jpg")); //"images/" + school_query.Text.Replace(" ", "_") + 
                    FileUpload2.PostedFile.SaveAs(Server.MapPath("~/images/students/logo.jpg"));
                    result_output.Text = "School Logo Successfully Uploaded ...";
                    ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script> alert('Logo Successfully Uploaded ...');</script>");
                }
                catch (Exception ex)
                {
                    result_output.Text = ex.Message;
                }  
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (!FileUpload3.HasFile)
            {
                result_output.Text = " Select Signature to Upload ... ";
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script> alert(' Select Signature to Upload ... ')</script>");

            }
            else if (System.IO.Path.GetExtension(FileUpload3.FileName).ToLower() != ".jpg"
                && Path.GetExtension(FileUpload3.FileName).ToLower() != ".png"
                && Path.GetExtension(FileUpload3.FileName).ToLower() != ".gif"
                && Path.GetExtension(FileUpload3.FileName).ToLower() != ".jpeg")
            {
                result_output.Text = "Uploaded File must be in Image Format ...";
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script> alert('Uploaded File must be in Image Format ...');</script>");

            }

            else
            {
                MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                cn.Open();

                try
                {
                    FileUpload3.PostedFile.SaveAs(Server.MapPath("~/images/" + school.Text.Replace(" ", "_").Replace("/","") + "_signature.png")); //"images/" + school_query.Text.Replace(" ", "_") + 
                    result_output.Text = "Signature Successfully Uploaded ...";
                    ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script> alert('Signature Successfully Uploaded ...');</script>");
                }
                catch (Exception ex)
                {
                    result_output.Text = ex.Message;
                }
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            if (!FileUpload4.HasFile)
            {
                result_output.Text = " Select Login Background to Upload ... ";
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script> alert(' Select Login Background to Upload ... ')</script>");

            }
            else if (System.IO.Path.GetExtension(FileUpload4.FileName).ToLower() != ".jpg"
                && Path.GetExtension(FileUpload4.FileName).ToLower() != ".png"
                && Path.GetExtension(FileUpload4.FileName).ToLower() != ".gif"
                && Path.GetExtension(FileUpload4.FileName).ToLower() != ".jpeg")
            {
                result_output.Text = "Uploaded File must be in Image Format ...";
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script> alert('Uploaded File must be in Image Format ...');</script>");

            }

            else
            {
                MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                cn.Open();

                try
                {
                    FileUpload4.PostedFile.SaveAs(Server.MapPath("~/images/background1.jpg")); //"images/" + school_query.Text.Replace(" ", "_") + 
                    result_output.Text = "Login Background Successfully Uploaded ...";
                    ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script> alert('Login Background Successfully Uploaded ...');</script>");
                }
                catch (Exception ex)
                {
                    result_output.Text = ex.Message;
                }
            }
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            if (!FileUpload5.HasFile)
            {
                result_output.Text = " Select Portal Background to Upload ... ";
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script> alert(' Select Portal Background to Upload ... ')</script>");

            }
            else if (System.IO.Path.GetExtension(FileUpload5.FileName).ToLower() != ".jpg"
                && Path.GetExtension(FileUpload5.FileName).ToLower() != ".png"
                && Path.GetExtension(FileUpload5.FileName).ToLower() != ".gif"
                && Path.GetExtension(FileUpload5.FileName).ToLower() != ".jpeg")
            {
                result_output.Text = "Uploaded File must be in Image Format ...";
                ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script> alert('Uploaded File must be in Image Format ...');</script>");

            }

            else
            {
                MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                cn.Open();

                try
                {
                    FileUpload5.PostedFile.SaveAs(Server.MapPath("~/images/background2.jpg")); //"images/" + school_query.Text.Replace(" ", "_") + 
                    result_output.Text = "Portal Background Successfully Uploaded ...";
                    ClientScript.RegisterStartupScript(this.GetType(), "msg", "<script> alert('Portal Background Successfully Uploaded ...');</script>");
                }
                catch (Exception ex)
                {
                    result_output.Text = ex.Message;
                }
            }
        }
    }
}