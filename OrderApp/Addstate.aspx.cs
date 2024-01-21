using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class Addstate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!Page.IsPostBack)
                {
                    DataTable dt = new DataTable();
                    BA_Country ObjCountry = new BA_Country();

                    ObjCountry.SELECT_ALL_Country(ref dt);

                    drpCountryName.DataSource = dt;
                    drpCountryName.DataTextField = "country_name";
                    drpCountryName.DataValueField = "countryId";
                    drpCountryName.DataBind();

                    drpCountryName.Items.Insert(0, new ListItem("-- Select --", "0"));
                    drpCountryName.SelectedIndex =1;
                    if (Request.QueryString["q"] != null)
                    {
                        string strKey = Convert.ToString(Request.QueryString["q"]);
                        Common cmn = new Common();
                        strKey = cmn.Decrypt(strKey);

                        Int32 stateid = Convert.ToInt32(strKey);

                        BA_States ObjStates = new BA_States();
                        DataTable _dt = new DataTable();

                        ObjStates.state_id = stateid;
                        ObjStates.GET_RECORDS_FROM_States(ref _dt);

                        if (_dt != null)
                        {
                            txtstatename.Text = Convert.ToString(_dt.Rows[0]["state_name"]);
                            drpCountryName.SelectedValue = Convert.ToString(_dt.Rows[0]["country_id"]);


                            hdstateid.Value = Convert.ToString(_dt.Rows[0]["state_id"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (drpCountryName.SelectedValue != "0")
                {
                    BA_States ObjStates = new BA_States();
                    Common Cmn = new Common();

                    if (!checkStateName())
                    {
                        ObjStates.country_id = drpCountryName.SelectedIndex;
                        ObjStates.state_name = txtstatename.Text;

                        ObjStates.is_del = false;


                        bool output;
                        if (hdstateid.Value == "")
                        {
                            ObjStates.created_by = Convert.ToInt32(Session["UserId"]);
                            ObjStates.created_date = DateTime.Now;
                            output = ObjStates.INSERT_States();
                        }
                        else
                        {
                            ObjStates.modify_by = Convert.ToInt32(Session["UserId"]);
                            ObjStates.modify_date = DateTime.Now;

                            ObjStates.state_id = Convert.ToInt32(hdstateid.Value);
                            output = ObjStates.UPDATE_States();
                        }

                        if (output == true)
                        {
                            Response.Redirect("StateList.aspx", false);
                        }
                        else
                        {
                            lblErrorMessage.Text = CommMessage.Recordcouldnotable;
                        }
                    }
                    else 
                    {
                        
                    }
                    
                }
                else
                {
                    lblErrorMessage.Text = "Please select country";
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }


        public bool checkStateName()
        {
            try
            {
                BA_States ObjStates = new BA_States();
                DataTable dt = new DataTable();
                if (hdstateid.Value == "")
                {
                    ObjStates.state_id = 0;
                }
                else
                {
                    ObjStates.state_id = Convert.ToInt32(hdstateid.Value);
                }
                ObjStates.country_id = drpCountryName.SelectedIndex;
                ObjStates.state_name = txtstatename.Text;
                ObjStates.CheckCoutnryWise_States(ref dt);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        //txtCountryName.Text = "";
                        lblErrorMessage.Text = CommMessage.Statenamealreadyexist;
                        lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
                return false;
            }
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtstatename.Text = "";
                lblErrorMessage.Text   ="";
                drpCountryName.SelectedIndex = 0;
                hdstateid.Value = "";
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("StateList.aspx", false);
        }
    }
}