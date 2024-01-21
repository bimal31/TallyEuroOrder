using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class StateList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GetStateList();
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }


        protected void txtSearch_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GetStateList();
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
                Response.Redirect("AddState.aspx", false);
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdStateList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditValue")
                {
                    Int32 ProductPckID = Convert.ToInt32(e.CommandArgument);

                    Common cmn = new Common();
                    string strEncryptValue = cmn.Encrypt(Convert.ToString(ProductPckID));

                    Response.Redirect("AddState.aspx?q=" + strEncryptValue, false);
                }

                if (e.CommandName == "DeleteValue")
                {
                    Int32 stateid = Convert.ToInt32(e.CommandArgument);

                    BA_States ObjState = new BA_States();

                    ObjState.state_id = stateid;
                    bool output = ObjState.DELETE_RECORDS_FROM_States();

                    if (output == true)
                    {
                        GetStateList();
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

        protected void GetStateList()
        {
            try
            {
                DataTable dt = new DataTable();
                BA_States ObjState = new BA_States();
                ObjState.state_name = txtSearch.Text;
                ObjState.SELECT_ALL_States(ref dt);

               

                grdStateList.DataSource = dt;
                grdStateList.DataBind();

                Session["dtState"] = dt;
                ViewState["sortdr"] = "Asc";
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdStateList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dtState = new DataTable();
                dtState = Session["dtState"] as DataTable;

                grdStateList.PageIndex = e.NewPageIndex;
                grdStateList.DataSource = dtState;
                grdStateList.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdStateList_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtrslt = (DataTable)Session["dtState"];
            if (dtrslt.Rows.Count > 0)
            {
                if (Convert.ToString(ViewState["sortdr"]) == "Asc")
                {
                    dtrslt.DefaultView.Sort = e.SortExpression + " Desc";
                    ViewState["sortdr"] = "Desc";
                }
                else
                {
                    dtrslt.DefaultView.Sort = e.SortExpression + " Asc";
                    ViewState["sortdr"] = "Asc";
                }
                grdStateList.DataSource = dtrslt;
                grdStateList.DataBind();


            }

        }
    }
}