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
    public partial class set_grade_remark : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool v1, v2 = false;
            if (Session["school"] != null)
            {
                v1 = true;
            }
            else
            {
                v1 = false;
            }
            /////////////////////////////////////////////////////////////////////////////////
            if (v1 == false)
            {
                Response.Redirect("Home/Login");
            }
            //////////////////////////////////////////////////////////////////////////////////////
            string sch = (string)Session["school"];
            MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            try
            {
                cn.Open();
               
                MySqlCommand cmd = new MySqlCommand("select from1,to1,grade,remark from table_grade_range where school='" + sch+"'", cn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                cn.Close();cn.Dispose();
            }
            ///////////////////////////////////////////////////////////////////////////////////
            MySqlConnection cn1 = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
            try
            {
                cn1.Open();
                for(int i=0;i<GridView1.Rows.Count;i++)
                {
                    MySqlCommand cmd = new MySqlCommand("update table_result set grade='" + GridView1.Rows[i].Cells[2].Text + "',remark='" + GridView1.Rows[i].Cells[3].Text + "' where (score_ca+score_ca1+score_exam)>='" + GridView1.Rows[i].Cells[0].Text + "' and (score_ca+score_ca1+score_exam)<='" + GridView1.Rows[i].Cells[1].Text + "' and school='" + sch + "';update table_result_mid set grade='" + GridView1.Rows[i].Cells[2].Text + "',remark='" + GridView1.Rows[i].Cells[3].Text + "' where (score_ca+score_ca1+score_exam)>='" + GridView1.Rows[i].Cells[0].Text + "' and (score_ca+score_ca1+score_exam)<='" + GridView1.Rows[i].Cells[1].Text + "' and school='" + sch + "'", cn1);
                    cmd.ExecuteNonQuery();
                    Response.Write("Success ...");
                }
            }
            catch(Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                cn1.Close();
            }

        }
    }
}