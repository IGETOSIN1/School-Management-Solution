using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace WEB_SCONET_MANAGEMENT
{
    public partial class Det : System.Web.UI.Page
    {
        string cstring = ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_det.Text))
            {
                try
                {
                    schl.Text = Request.Cookies["cookie_school"].Value.ToString();
                    schl1.Text = Request.Cookies["cookie_school"].Value.ToString();
                    cat.Text = Request.Cookies["cookie_cat_status"].Value.ToString();
                }
                catch
                {
                    Response.Redirect("Login.aspx");
                }

                if (cat.Text == "Super Super")
                {
                    HttpCookie cookie_school = new HttpCookie("cookie_school");
                    cookie_school.Value = "Super Super";
                    cookie_school.Expires = DateTime.Now.AddHours(-3);
                    Response.SetCookie(cookie_school);
                }
                else
                {
                    Response.Redirect("Home/Index");
                }

                MySqlConnection cn1 = new MySqlConnection(cstring);
                try
                {
                    cn1.Open();
                    MySqlDataAdapter da = new MySqlDataAdapter("SELECT DISTINCT(School) FROM table_Schools order by School", cn1);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    school_query.DataSource = dt;
                    school_query.DataValueField = "School";
                    school_query.DataBind();
                }
                catch (Exception ex)
                {
                    // Response.Write(ex.Message);
                }
                finally
                {
                    cn1.Close();
                }
                txt_det.Text = "Performed ...";
            }

        }

        protected void Button3_Click(object sender)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            
            HttpCookie cookie_school = new HttpCookie("cookie_school");
            cookie_school.Value = school_query.Text;
            cookie_school.Expires = DateTime.Now.AddHours(5);
            Response.SetCookie(cookie_school);

            Session["school"] = school_query.Text;

            Response.Redirect("Home/Index");
        }

        protected void school_query_SelectedIndexChanged()
        {
            HttpCookie cookie_school = new HttpCookie("cookie_school");
            cookie_school.Value = school_query.Text;
            cookie_school.Expires = DateTime.Now.AddHours(5);
            Response.SetCookie(cookie_school);

            Session["school"] = school_query.Text;
        }

        protected void school_query_SelectedIndexChanged(object sender, EventArgs e)
        {
            HttpCookie cookie_school = new HttpCookie("cookie_school");
            cookie_school.Value = school_query.Text;
            cookie_school.Expires = DateTime.Now.AddHours(5);
            Response.SetCookie(cookie_school);

            Session["school"] = school_query.Text;
        }
    }
}