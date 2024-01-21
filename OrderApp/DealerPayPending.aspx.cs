using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class DealerPayPending : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GetDealerPayPendingList();
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
                GetDealerPayPendingList();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

       

     

        protected void grdDealerPayPendingList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "EditValue")
                {
                    string DealerCode = Convert.ToString(e.CommandArgument);
                    BA_tblDealer ObjDealer = new BA_tblDealer();
                    Common Cmn = new Common();
                    ObjDealer.DealerCode = DealerCode;
                    ObjDealer.UPDATE_DealerPayPenidng();
                    GetDealerPayPendingList();
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void GetDealerPayPendingList()
        {
            try
            {
                DataTable dt = new DataTable();
                BA_tblDealer objBA_tblDealer = new BA_tblDealer();
                objBA_tblDealer.DealerCode = txtSearch.Text;
                objBA_tblDealer.SELECT_ALL_tblDealerPayPending(ref dt);



                grdDealerPayPendingList.DataSource = dt;
                grdDealerPayPendingList.DataBind();

                Session["dtDealerPayPending"] = dt;
                ViewState["sortdr"] = "Asc";
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdDealerPayPendingList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dtProductPacking = new DataTable();
                dtProductPacking = Session["dtDealerPayPending"] as DataTable;

                grdDealerPayPendingList.PageIndex = e.NewPageIndex;
                grdDealerPayPendingList.DataSource = dtProductPacking;
                grdDealerPayPendingList.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdDealerPayPendingList_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtrslt = (DataTable)Session["dtDealerPayPending"];
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
                grdDealerPayPendingList.DataSource = dtrslt;
                grdDealerPayPendingList.DataBind();


            }

        }
    }
}