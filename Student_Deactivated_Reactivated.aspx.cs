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
    public partial class Student_Deactivated_Reactivated : System.Web.UI.Page
    {
        General_Class gclass = new General_Class();
        string cstring = ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
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

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            MySqlConnection cn1 = new MySqlConnection(cstring);
            try
            {
                cn1.Open();
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT DISTINCT(Full_NAME) FROM table_student WHERE School='" + school.Text + "' order by Full_name", cn1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                student_activated.DataSource = dt;
                student_activated.DataValueField = "Full_Name";
                student_activated.DataBind();
            }
            catch (Exception ex)
            {
                // Response.Write(ex.Message);
            }
            finally
            {
                cn1.Close();
            }

            MySqlConnection cn2 = new MySqlConnection(cstring);
            try
            {
                cn2.Open();
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT DISTINCT(Full_NAME) FROM table_student_old WHERE School='" + school.Text + "' order by Full_name", cn2);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                student_deactivated.DataSource = dt1;
                student_deactivated.DataValueField = "Full_Name";
                student_deactivated.DataBind();
            }
            catch (Exception ex)
            {
                // Response.Write(ex.Message);
            }
            finally
            {
                cn2.Close();
            }
            ///////////////////////////////////////////// START OF REGRESH //////////////////////////////////////////////////
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            student_id.Text = null;
            MySqlDataReader dr4 = gclass.display_in_box("SELECT student_id FROM Table_Student WHERE full_name='" + student_activated.Text + "' AND School='" + school.Text + "'");
            if (dr4.Read())
            {
                student_id.Text = (string)dr4.GetValue(0).ToString();
            }
            //////////////////////////// DE-ACTIVATE ////////////////////////////////////////
            MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            try
            {
                cn.Open();
                cn = new MySqlConnection(cstring);
                cn.Open();
                string query = "insert into table_student_old select* from table_student where student_status1!='' on duplicate key update student_id=values(student_id),full_name=values(full_name),class=values(class);delete from table_student where student_status1!='';  UPDATE Table_payment_analysis set student_status1='Dormant' Where student_id='" + student_id.Text + "';UPDATE Table_payment_fees set student_status1='Dormant' Where student_id='" + student_id.Text + "';UPDATE Table_payment_fees_detail set student_status1='Dormant' Where student_id='" + student_id.Text + "';UPDATE Table_student set student_status1='Dormant' Where student_id='" + student_id.Text + "';       insert into table_student_old select* from table_student where school='" + school.Text + "' and student_id='" + student_id.Text + "' on duplicate key update student_id=values(student_id),full_name=values(full_name),class=values(class);delete from table_student where school='" + school.Text + "' and student_id='" + student_id.Text + "';";

                MySqlCommand cmd = new MySqlCommand(query, cn);
                cmd.ExecuteNonQuery();
                result_output.Text = "De-activation was Successful ...";
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                cn.Close(); cn.Dispose();
            }

            //////////////////////////////////////////////////// START OF REFRESH /////////////////////////////////////////////////////////
            MySqlConnection cn1 = new MySqlConnection(cstring);
            try
            {
                cn1.Open();
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT DISTINCT(Full_NAME) FROM table_student WHERE School='" + school.Text + "' order by Full_name", cn1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                student_activated.DataSource = dt;
                student_activated.DataValueField = "Full_Name";
                student_activated.DataBind();
            }
            catch (Exception ex)
            {
                // Response.Write(ex.Message);
            }
            finally
            {
                cn1.Close();
            }

            MySqlConnection cn2 = new MySqlConnection(cstring);
            try
            {
                cn2.Open();
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT DISTINCT(Full_NAME) FROM table_student_old WHERE School='" + school.Text + "' order by Full_name", cn2);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                student_deactivated.DataSource = dt1;
                student_deactivated.DataValueField = "Full_Name";
                student_deactivated.DataBind();
            }
            catch (Exception ex)
            {
                // Response.Write(ex.Message);
            }
            finally
            {
                cn2.Close();
            }
            ///////////////////////////////////////////// END OF REGRESH //////////////////////////////////////////////////

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            student_id.Text = null;
            MySqlDataReader dr4 = gclass.display_in_box("SELECT student_id FROM Table_Student_old WHERE full_name='" + student_deactivated.Text + "' AND School='" + school.Text + "'");
            if (dr4.Read())
            {
                student_id.Text = (string)dr4.GetValue(0).ToString();
            }
            //////////////////////////// ACTIVATE ////////////////////////////////////////
            MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            try
            {
                cn.Open();
                cn = new MySqlConnection(cstring);
                cn.Open();
                string query = "UPDATE Table_payment_analysis set student_status1='' Where student_id='" + student_id.Text + "';UPDATE Table_payment_fees set student_status1='' Where student_id='" + student_id.Text + "';UPDATE Table_payment_fees_detail set student_status1='' Where student_id='" + student_id.Text + "';UPDATE Table_student set student_status1='' Where student_id='" + student_id.Text + "';     insert into table_student select* from table_student_old where school='" + school.Text + "' and student_id='" + student_id.Text + "' on duplicate key update student_id=values(student_id),full_name=values(full_name),class=values(class);delete from table_student_old where school='" + school.Text + "' and student_id='" + student_id.Text + "';";
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cmd.ExecuteNonQuery();
                result_output.Text = "Activation was Successful ...";
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                cn.Close(); cn.Dispose();
            }
            //////////////////////////////////////////////////// START OF REFRESH /////////////////////////////////////////////////////////
            MySqlConnection cn1 = new MySqlConnection(cstring);
            try
            {
                cn1.Open();
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT DISTINCT(Full_NAME) FROM table_student WHERE School='" + school.Text + "' order by Full_name", cn1);
                DataTable dt = new DataTable();
                da.Fill(dt);
                student_activated.DataSource = dt;
                student_activated.DataValueField = "Full_Name";
                student_activated.DataBind();
            }
            catch (Exception ex)
            {
                // Response.Write(ex.Message);
            }
            finally
            {
                cn1.Close();
            }

            MySqlConnection cn2 = new MySqlConnection(cstring);
            try
            {
                cn2.Open();
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT DISTINCT(Full_NAME) FROM table_student_old WHERE School='" + school.Text + "' order by Full_name", cn2);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                student_deactivated.DataSource = dt1;
                student_deactivated.DataValueField = "Full_Name";
                student_deactivated.DataBind();
            }
            catch (Exception ex)
            {
                // Response.Write(ex.Message);
            }
            finally
            {
                cn2.Close();
            }
            ///////////////////////////////////////////// END OF REGRESH //////////////////////////////////////////////////


        }
    }
}