using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace WEB_SCONET_MANAGEMENT
{
    public partial class License : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           /* string cstring = ConfigurationManager.ConnectionStrings["cnstring_v"].ConnectionString;
            //////////////////////////////////// START OF SESSION //////////////////////////////////////
            try
            {
                MySqlConnection cn = new MySqlConnection(cstring);
                try
                {
                    cn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT Session from table_Session", cn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GridView_session.DataSource = dt;
                    GridView_session.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    cn.Close(); cn.Dispose();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////// START OF TERM //////////////////////////////////////
            try
            {
                MySqlConnection cn = new MySqlConnection(cstring);
                try
                {
                    cn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT Term from table_term", cn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GridView_term.DataSource = dt;
                    GridView_term.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    cn.Close(); cn.Dispose();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////// START OF YEAR //////////////////////////////////////
            try
            {
                MySqlConnection cn = new MySqlConnection(cstring);
                try
                {
                    cn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT Year from table_Year", cn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GridView_year.DataSource = dt;
                    GridView_year.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    cn.Close(); cn.Dispose();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////// START OF ACTIVATION //////////////////////////////////////
            try
            {
                MySqlConnection cn = new MySqlConnection(cstring);
                try
                {
                    cn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT Campus, Pre_id, School_Name, s_t, s_status, sub_domain, root, root_p from table_verify_schoolms Where Key1 = '" + license_key.Text + "'", cn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GridView_session.DataSource = dt;
                    GridView_session.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    cn.Close(); cn.Dispose();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////
            */



        }

        protected void Search_Click(object sender, EventArgs e)
        {
            string cstring = ConfigurationManager.ConnectionStrings["cnstring_v"].ConnectionString;
            //////////////////////////////////// START OF SESSION //////////////////////////////////////
            try
            {
                MySqlConnection cn = new MySqlConnection(cstring);
                try
                {
                    cn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT Session from table_Session", cn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GridView_session.DataSource = dt;
                    GridView_session.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    cn.Close(); cn.Dispose();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////// START OF TERM //////////////////////////////////////
            try
            {
                MySqlConnection cn = new MySqlConnection(cstring);
                try
                {
                    cn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT Term from table_term", cn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GridView_term.DataSource = dt;
                    GridView_term.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    cn.Close(); cn.Dispose();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////// START OF YEAR //////////////////////////////////////
            try
            {
                MySqlConnection cn = new MySqlConnection(cstring);
                try
                {
                    cn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT Year from table_Year", cn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GridView_year.DataSource = dt;
                    GridView_year.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    cn.Close(); cn.Dispose();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////
            //////////////////////////////////// START OF SCHOOL ACTIVATION //////////////////////////////////////
            try
            {
                MySqlConnection cn = new MySqlConnection(cstring);
                try
                {
                    cn.Open();
                    MySqlCommand cmd = new MySqlCommand("SELECT Campus, Pre_id, School_Name, s_t, s_status, sub_domain, root, root_p from table_verify_schoolms Where Key1 = '" + license_key.Text + "'", cn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GridView_school.DataSource = dt;
                    GridView_school.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    cn.Close(); cn.Dispose();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            //////////////////////////////////////////////////////////////////////////////////////////////////

        }

        protected void Activate_Click(object sender, EventArgs e)
        {
            ////////////////////////// START OF ACTIVATE SCHOOL ///////////////////////////////////////
            try
            {
                MySqlConnection cn1 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                try
                {
                    cn1.Open();
                    for (int i = 0; i < GridView_school.Rows.Count; i++)
                    {
                        MySqlCommand cmd = new MySqlCommand("INSERT INTO Table_schools(school,pre_id,School_Name,s_t,s_status,sub_domain,root,root_p)values('" + GridView_school.Rows[i].Cells[0].Text.ToString() + "','" + GridView_school.Rows[i].Cells[1].Text.ToString() + "','" + GridView_school.Rows[i].Cells[2].Text.ToString() + "','" + GridView_school.Rows[i].Cells[3].Text.ToString() + "','" + GridView_school.Rows[i].Cells[4].Text.ToString() + "','" + GridView_school.Rows[i].Cells[5].Text.ToString() + "','" + GridView_school.Rows[i].Cells[6].Text.ToString() + "','" + GridView_school.Rows[i].Cells[7].Text.ToString() + "') ON DUPLICATE KEY UPDATE school=values(school),pre_id=values(pre_id),School_Name=values(school_name),s_t=values(s_t),s_status=values(s_status),sub_domain=values(sub_domain),root=values(root),root_p=values(root_p)", cn1);
                        cmd.ExecuteNonQuery();
                       // Response.Write("Success ...");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    cn1.Close();
                }
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
            ////////////////////////// START OF SESSION ///////////////////////////////////////
            try
            {
                MySqlConnection cn1 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                try
                {
                    cn1.Open();
                    for (int i = 0; i < GridView_session.Rows.Count; i++)
                    {
                        MySqlCommand cmd = new MySqlCommand("INSERT INTO Table_session(Session)values('" + GridView_session.Rows[i].Cells[0].Text.ToString() + "') ON DUPLICATE KEY UPDATE Session=values(session)", cn1);
                        cmd.ExecuteNonQuery();
                        // Response.Write("Success ...");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    cn1.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            ////////////////////////// START OF TERM ///////////////////////////////////////
            try
            {
                MySqlConnection cn1 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                try
                {
                    cn1.Open();
                    for (int i = 0; i < GridView_term.Rows.Count; i++)
                    {
                        MySqlCommand cmd = new MySqlCommand("INSERT INTO Table_Term(Term)values('" + GridView_term.Rows[i].Cells[0].Text.ToString() + "') ON DUPLICATE KEY UPDATE Term=values(Term)", cn1);
                        cmd.ExecuteNonQuery();
                        // Response.Write("Success ...");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    cn1.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            ////////////////////////// START OF YEAR ///////////////////////////////////////
            try
            {
                MySqlConnection cn1 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                try
                {
                    cn1.Open();
                    for (int i = 0; i < GridView_year.Rows.Count; i++)
                    {
                        MySqlCommand cmd = new MySqlCommand("INSERT INTO Table_Year(Year)values('" + GridView_year.Rows[i].Cells[0].Text.ToString() + "') ON DUPLICATE KEY UPDATE Year=values(Year)", cn1);
                        cmd.ExecuteNonQuery();
                        // Response.Write("Success ...");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
                finally
                {
                    cn1.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }

            General_Class gclass = new General_Class();

            gclass.insert1("INSERT INTO Table_Sae(sae)values('abcdefghijklmnopqrstuvwxyz') ON DUPLICATE KEY UPDATE sae=values(sae)");
            //////////////////////////////// END OF ALL OPERATIONS ///////////////////////////////////////////////////////////



        }
    }
}