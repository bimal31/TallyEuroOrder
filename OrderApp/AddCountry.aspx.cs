using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class AddCountry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["q"] != null)
                    {
                        string strKey = Convert.ToString(Request.QueryString["q"]);
                        Common cmn = new Common();
                        strKey = cmn.Decrypt(strKey);

                        Int32 CountryId = Convert.ToInt32(strKey);

                        BA_Country ObjDealer = new BA_Country();
                        DataTable dt = new DataTable();

                        ObjDealer.countryId = CountryId;
                        ObjDealer.GET_RECORDS_FROM_Country(ref dt);

                        if (dt != null)
                        {
                            txtCountryName.Text = Convert.ToString(dt.Rows[0]["country_name"]);
                            hdCountryId.Value = Convert.ToString(dt.Rows[0]["countryId"]);
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
                BA_Country ObjCountry = new BA_Country();
                Common Cmn = new Common();
                ObjCountry.country_name = txtCountryName.Text;

                if (!checkCountryName())
                {
                    bool output;
                    if (hdCountryId.Value == "")
                    {
                        ObjCountry.created_by = Convert.ToInt32(Session["UserId"]);
                        ObjCountry.created_date = DateTime.Now;
                        ObjCountry.is_del = false;
                        output = ObjCountry.INSERT_Country();
                    }
                    else
                    {
                        ObjCountry.modify_by = Convert.ToInt32(Session["UserId"]);
                        ObjCountry.modify_date = DateTime.Now;
                        ObjCountry.countryId = Convert.ToInt32(hdCountryId.Value);
                        output = ObjCountry.UPDATE_Country();
                    }
                    if (output == true)
                    {
                        Response.Redirect("CountryList.aspx", false);
                    }
                    else
                    {
                        lblErrorMessage.Text = CommMessage.Recordcouldnotable;
                        lblErrorMessage.ForeColor = System.Drawing.Color.Black;
                    }
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {

                lblErrorMessage.Text = "";
                txtCountryName.Text = "";
                hdCountryId.Value = "";
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        public bool checkCountryName()
        {
            try
            {
                BA_Country ObjCountry = new BA_Country();
                DataTable dt = new DataTable();
                if (hdCountryId.Value == "")
                {
                    ObjCountry.countryId =0;
                }
                else 
                {
                    ObjCountry.countryId = Convert.ToInt32(hdCountryId.Value);
                }
                ObjCountry.country_name = txtCountryName.Text;
                ObjCountry.CHK_RECORDS_FROM_Country(ref dt);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        //txtCountryName.Text = "";
                        lblErrorMessage.Text = CommMessage.Countrynamealreadyexist;
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

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("CountryList.aspx", false);
        }
    }
}