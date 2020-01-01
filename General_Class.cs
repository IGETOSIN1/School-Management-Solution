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
    public class General_Class
    {
        MySqlDataReader dr;

        public MySqlDataReader display_in_box(string query)
        {

            try
            {
                MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                cn.Open();
                MySqlCommand cmd = new MySqlCommand(query, cn);
                dr = cmd.ExecuteReader();
                //MessageBox.Show("Record Successfully Saved!", "Confirmation Box!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message);
            }
            return dr;
        }

        public MySqlDataReader display_in_box1(string query, MySqlConnection cn, MySqlCommand cmd)
        {
            try
            {
                cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                cn.Open();
                cmd = new MySqlCommand(query, cn);
                dr = cmd.ExecuteReader();
                //MessageBox.Show("Record Successfully Saved!", "Confirmation Box!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            return dr;
        }

        public string display_in_combobox(string query, DropDownList cbobox, string vmember)
        {
            try
            {
                MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                cn.Open();
                MySqlCommand cmd = new MySqlCommand(query, cn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                cbobox.DataSource = dt;
                cbobox.DataValueField = vmember;
                cbobox.DataBind();
                //MessageBox.Show("Record Successfully Saved!", "Confirmation Box!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
            return "Sola";
        }

        public string insert(string query)
        {
                try
                {
                    MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                    cn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, cn);
                    cmd.ExecuteNonQuery();
                  //  MessageBox.Show("Record Successfully Saved!", "Confirmation Box!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cn.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                   // MessageBox.Show(ex.Message);
                }
          
            return "Sola";
        }

        public string insert_online(string query)
        {
            try
            {
                MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring_online"].ConnectionString);
                cn.Open();
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cmd.ExecuteNonQuery();
               // MessageBox.Show("Record Successfully Saved!", "Confirmation Box!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cn.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
            return "Sola";
        }

        /*  public string insert_online_Multiple_Insert_from_datagridview(string query, DataGridView dgv)
          {
              for (int i = 0; i < dgv.Rows.Count; i++)
              {
                  try
                  {
                      MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring_online"].ConnectionString);
                      cn.Open();
                      MySqlCommand cmd = new MySqlCommand(query, cn);
                      cmd.ExecuteNonQuery();
                    //  MessageBox.Show("Record Successfully Saved!", "Confirmation Box!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                      cn.Close();
                  }
                  catch (Exception ex)
                  {
                      MessageBox.Show(ex.Message);
                  }
              }
              return "Sola";
          }*/

        public string insert1(string query)
        {
            try
            {
                MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                cn.Open();
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cmd.ExecuteNonQuery();
                // MessageBox.Show("Record Successfully Saved!", "Confirmation Box!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cn.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
              //  MessageBox.Show(ex.Message);
            }
            return "Sola";
        }

        public string Update1(string query)
        {
            try
            {
                MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                cn.Open();
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cmd.ExecuteNonQuery();
                //  MessageBox.Show("Update was Successfull!", "Confirmation Box!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cn.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
            return "Sola";
        }

        public string Delete1(string query)
        {
            try
            {
                MySqlConnection cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["cnstring"].ConnectionString);
                cn.Open();
                MySqlCommand cmd = new MySqlCommand(query, cn);
                cmd.ExecuteNonQuery();
                // MessageBox.Show("Record Successfully Deleted! \n \n You might need to Re-login to see the Effect!", "Confirmation Box!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cn.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(" Delete Not Permitted for the Selected Record! ", " Message Center ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return "Sola";
        }

        public string panel_format(Panel panel1)
        {
            // panel3 = new Panel();
            foreach (Control control in panel1.Controls)
            {
                if (control is TextBox)
                {
                    (control as TextBox).Text = null;
                  //  control.Text = null;
                }

                if (control is DropDownList)
                {
                    (control as DropDownList).SelectedIndex = -1;
                }
            }
            return "Sola";
        }
    }
}