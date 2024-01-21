using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace OrderApp
{
    public partial class Country : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GetCountryList();
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("AddCountry.aspx", false);
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdCountryList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditValue")
                {
                    Int32 CountryId = Convert.ToInt32(e.CommandArgument);

                    Common cmn = new Common();
                    string strEncryptValue = cmn.Encrypt(Convert.ToString(CountryId));

                    Response.Redirect("AddCountry.aspx?q=" + strEncryptValue, false);
                    // Response.Redirect("AddCountry.aspx?CountryId=" + CountryId, false);
                }

                if (e.CommandName == "DeleteValue")
                {
                    Int32 CountryId = Convert.ToInt32(e.CommandArgument);

                    BA_Country ObjCountry = new BA_Country();

                    ObjCountry.countryId = CountryId;
                    bool output = ObjCountry.DELETE_RECORDS_FROM_Country();

                    if (output == true)
                    {
                        GetCountryList();
                    }
                    else
                    {
                        lblErrorMessage.Text = CommMessage.CouldnotabletoDelete;
                    }
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void GetCountryList()
        {
            try
            {
                DataTable dt = new DataTable();
                BA_Country objUser = new BA_Country();
                objUser.SELECT_ALL_Country(ref dt);

                Session["dtCountry"] = dt;

                grdCountryList.DataSource = dt;
                grdCountryList.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdCountryList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dtCountry = new DataTable();
                dtCountry = Session["dtCountry"] as DataTable;

                grdCountryList.PageIndex = e.NewPageIndex;
                grdCountryList.DataSource = dtCountry;
                grdCountryList.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }
    }
}