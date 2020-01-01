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
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        string subdomain = " ";
        string cstring = ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            string school = null;
            try
            {
                users.Text = "Welcome- " + Request.Cookies["cookie_users"].Value.ToString();
                school = Request.Cookies["cookie_school"].Value.ToString();
            }
            catch(Exception ex)
            {
                //Response.Redirect(ex.Message);
                Response.Redirect("Login.aspx");
            }

            //////////////////////////// END OF DETERMINATION OF SUB-DOMAIN AND CONNECTION //////////////////////////////////////////////////
           /* if (cstring == " " && subdomain == " ")
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
            }*/

            //////////////////////////// END OF DETERMINATION OF SUB-DOMAIN AND CONNECTION //////////////////////////////////////////////////




            try
            {
                image_logo.ImageUrl = "images/" + school.Replace(" ", "_") + "_logo.png";
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

            try
            {
                MySqlConnection cn8 = new MySqlConnection(cstring);
                cn8.Open();
                MySqlCommand cmd8 = new MySqlCommand("SELECT School_Name FROM table_schools ORDER BY p_id asc limit 1", cn8);
                //gclass.display_in_box("SELECT* FROM table_configuration Where name_of_school='" + school_query.Text + "'");
                MySqlDataReader dr8 = cmd8.ExecuteReader();
                try
                {
                    if (dr8.Read())
                    {
                        Label1.Text = (string)dr8.GetValue(0).ToString();
                        dr8.Close();
                    }
                    else
                    {
                        Label1.Text = "School-MS Solutions";
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

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            HttpCookie cookie_department = new HttpCookie("cookie_department");
            cookie_department.Value = "Super Super";
            cookie_department.Expires = DateTime.Now.AddHours(-3);
            Response.SetCookie(cookie_department);

            HttpCookie cookie_school = new HttpCookie("cookie_school");
            cookie_school.Value = "Super Super";
            cookie_school.Expires = DateTime.Now.AddHours(-3);
            Response.SetCookie(cookie_school);

            HttpCookie cookie_users = new HttpCookie("cookie_users");
            cookie_users.Value = "Welcome - Visitor";
            cookie_users.Expires = DateTime.Now.AddHours(-3);
            Response.SetCookie(cookie_users);

            HttpCookie cookie_user_name = new HttpCookie("cookie_user_name");
            cookie_user_name.Value = "Welcome - Visitor";
            cookie_user_name.Expires = DateTime.Now.AddHours(-3);
            Response.SetCookie(cookie_user_name);

            Response.Redirect("Redirect.aspx");
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            HttpCookie cookie_department = new HttpCookie("cookie_department");
            cookie_department.Value = "Super Super";
            cookie_department.Expires = DateTime.Now.AddHours(-3);
            Response.SetCookie(cookie_department);

            HttpCookie cookie_school = new HttpCookie("cookie_school");
            cookie_school.Value = "Super Super";
            cookie_school.Expires = DateTime.Now.AddHours(-3);
            Response.SetCookie(cookie_school);

            HttpCookie cookie_users = new HttpCookie("cookie_users");
            cookie_users.Value = "Welcome - Visitor";
            cookie_users.Expires = DateTime.Now.AddHours(-3);
            Response.SetCookie(cookie_users);

            HttpCookie cookie_user_name = new HttpCookie("cookie_user_name");
            cookie_user_name.Value = "Welcome - Visitor";
            cookie_user_name.Expires = DateTime.Now.AddHours(-3);
            Response.SetCookie(cookie_user_name);

            Response.Redirect("Home/Logout");
        }
    }
}
