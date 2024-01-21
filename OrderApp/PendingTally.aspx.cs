using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class PendingTally : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    txtFromDate.Attributes.Add("readonly", "readonly");
                    txtToDate.Attributes.Add("readonly", "readonly");

                    if (txtFromDate.Text == "")
                    {
                        txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-GB"));

                    }

                    if (txtToDate.Text == "")
                    {
                        txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                    }

                    GetPendingBillList();

                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }


        protected void grdPendingBill_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dtOrder = new DataTable();
                dtOrder = Session["PendingBill"] as DataTable;

                grdPendingBill.PageIndex = e.NewPageIndex;
                grdPendingBill.DataSource = dtOrder;
                grdPendingBill.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void GetPendingBillList()
        {
            try
            {
                DateTime now = DateTime.Now;
                DataTable dt = new DataTable();
                BA_tblOrder objBA_tblOrder = new BA_tblOrder();
                objBA_tblOrder.OrderFromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
                objBA_tblOrder.OrderToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");

                objBA_tblOrder.SELECT_ALL_PendingBillTally(ref dt);

                Session["PendingBill"] = dt;

                grdPendingBill.DataSource = dt;
                grdPendingBill.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetPendingBillList();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }



        protected void grdPendingBill_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ViewValue")
                {
                    string Order = Convert.ToString(e.CommandArgument);
                    string OrderId = "", OrderType = "";

                    string[] strOrder = Order.Split('|');
                    OrderId = strOrder[0];
                    OrderType = strOrder[1];

                    Common cmn = new Common();
                    string strEncryptValue = cmn.Encrypt(Convert.ToString(OrderId));
                    if (OrderType == CommMessage.OrderType_Order)
                        Response.Redirect("AddOrder.aspx?q=" + strEncryptValue + "&View=Y&BackButton=Y" + "&OrderType=" + OrderType, false);
                    else
                        Response.Redirect("FreeOrder.aspx?q=" + strEncryptValue + "&View=Y&BackButton=Y" + "&OrderType=" + OrderType, false);
                }

                
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

    }
}