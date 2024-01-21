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
    public partial class DealerOrderSchemeList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    txtFromDate.Attributes.Add("readonly", "readonly");
                    txtToDate.Attributes.Add("readonly", "readonly");
                    GetOrderList();

                    if (Convert.ToString(Session["UserType"]) == "Factory")
                    {
                        btnAdd.Visible = false;
                    }
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
                Response.Redirect("AddDealerOrderScheme.aspx", false);
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }


        public bool UpdateORderStatus(string OrderId)
        {
            BA_tblOrder ObjOrder = new BA_tblOrder();
            try
            {
                bool output;

                ObjOrder.OrderID = OrderId;
                ObjOrder.OrderStatus = "Dispatch Department";
                output = ObjOrder.UPDATE_tblOrderStatus();
                return output;

            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void grdOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "ViewValue")
                {
                   // Int32 OrderId = Convert.ToInt32(e.CommandArgument);
                    string Order = Convert.ToString(e.CommandArgument);
                    string OrderId = "", OrderType = "";

                    string[] strOrder = Order.Split('|');
                    OrderId = strOrder[0];
                    OrderType = strOrder[1];

                    Common cmn = new Common();
                    string strEncryptValue = cmn.Encrypt(Convert.ToString(OrderId));

                   // Response.Redirect("PrintOrder.aspx?q=" + strEncryptValue + "&View=Y" + "&OrderType=" + OrderType, false);
                    Response.Redirect("AddDealerOrderScheme.aspx?q=" + strEncryptValue + "&View=Y&BackButton=Y" + "&OrderType=" + OrderType, false);
                }

                if (e.CommandName == "PrintValue")
                {
                    string Order = Convert.ToString(e.CommandArgument);
                    string OrderId = "", OrderType = "";

                    string[] strOrder = Order.Split('|');
                    OrderId = strOrder[0];
                    OrderType = strOrder[1];

                    Common cmn = new Common();
                    string strEncryptValue = cmn.Encrypt(Convert.ToString(OrderId));

                    if (Convert.ToString(Session["UserType"]) == "Factory" || Convert.ToString(Session["UserType"]) == "Admin")
                    {
                        if (UpdateORderStatus(OrderId))
                        {

                            lblErrorMessage.Text = "";

                            string strUrl = "RptPrintOrder.aspx?q=" + strEncryptValue + "&Print=Y" + "&OrderType=" + OrderType;

                            Response.Write("<script>");
                            Response.Write("window.open('" + strUrl + "','_blank')");
                            Response.Write("</script>");
                            GetOrderList();
                        }
                        else
                        {
                            lblErrorMessage.Text = CommMessage.orderstatusnotupdate;
                        }
                    }
                    else
                    {
                        string strUrl = "RptPrintOrder.aspx?q=" + strEncryptValue + "&Print=Y" + "&OrderType=" + OrderType;
                        Response.Write("<script>");
                        Response.Write("window.open('" + strUrl + "','_blank')");
                        Response.Write("</script>");
                    }
                }

                if (e.CommandName == "EditValue")
                {
                    Int32 OrderId = Convert.ToInt32(e.CommandArgument);

                    Common cmn = new Common();
                    string strEncryptValue = cmn.Encrypt(Convert.ToString(OrderId));

                    Response.Redirect("AddDealerOrderScheme.aspx?q=" + strEncryptValue + "&BackButton=Y", false);
                }

                if (e.CommandName == "DeleteValue")
                {
                    Int32 OrderId = Convert.ToInt32(e.CommandArgument);

                    BA_tblOrder ObjOrder = new BA_tblOrder();
                    ObjOrder.OrderID = Convert.ToString(OrderId);

                    bool output = ObjOrder.DELETE_RECORDS_FROM_tblOrder();

                    if (output == true)
                    {
                        GetOrderList();
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

        protected void GetOrderList()
        {
            try
            {
                DateTime now = DateTime.Now;
                if (txtFromDate.Text == "")
                {
                    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-GB"));

                }

                if (txtToDate.Text == "")
                {
                    txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                }

                DataTable dt = new DataTable();
                BA_tblOrder objBA_tblOrder = new BA_tblOrder();
                objBA_tblOrder.OrderType = CommMessage.OrderType_FreeScheme;
                objBA_tblOrder.UserType = Convert.ToString(HttpContext.Current.Session["UserType"]);
                objBA_tblOrder.OrderFromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
                objBA_tblOrder.OrderToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
                
                objBA_tblOrder.SELECT_ALL_tblOrder(ref dt);

                Session["dtOrder"] = dt;

                grdOrder.DataSource = dt;
                grdOrder.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dtOrder = new DataTable();
                dtOrder = Session["dtOrder"] as DataTable;

                grdOrder.PageIndex = e.NewPageIndex;
                grdOrder.DataSource = dtOrder;
                grdOrder.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void drpOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddl.Parent.Parent;
                int idx = row.RowIndex;

                HiddenField hdOrderListId = row.Cells[idx].FindControl("hdOrderListId") as HiddenField;
                DropDownList drpOrderStatus = row.Cells[idx].FindControl("drpOrderStatus") as DropDownList;
                Int32 OrderId = Convert.ToInt32(hdOrderListId.Value);

                BA_tblOrder ObjOrder = new BA_tblOrder();
                ObjOrder.OrderID = Convert.ToString(OrderId);
                ObjOrder.OrderStatus = drpOrderStatus.SelectedValue;

                bool output;
                output = ObjOrder.UPDATE_tblOrderStatus();

                if (output == true)
                {
                    GetOrderList();
                }
                else
                {
                    lblErrorMessage.Text = CommMessage.coludnotchangestatus;
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (Convert.ToString(Session["UserType"]) == "Factory")
                {
                    if (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header)
                    {
                        e.Row.Cells[8].Visible = false;//this is your templatefield column.
                        e.Row.Cells[9].Visible = false;//this is your templatefield column.
                    }
                }

                GridViewRow gvRow = (GridViewRow)e.Row;
                HiddenField hdOrderStatus = (HiddenField)gvRow.FindControl("hdOrderStatus");
                HiddenField hdnIsTallyImport = (HiddenField)gvRow.FindControl("hdnIsTallyImport");
                if (hdOrderStatus != null)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        DropDownList drpOrderStatus = (DropDownList)gvRow.FindControl("drpOrderStatus");

                        string UserType = "";
                        UserType = Convert.ToString(HttpContext.Current.Session["UserType"]);

                        if (UserType == "Factory")
                        {
                            drpOrderStatus.Items.Clear();
                            drpOrderStatus.Items.Add(new ListItem("Factory", "Factory"));
                            drpOrderStatus.Items.Add(new ListItem("Dispatch Department", "Dispatch Department"));
                            drpOrderStatus.Items.Add(new ListItem("Dispatched", "Dispatched"));

                        }

                        if (UserType.ToLower() == "admin" || UserType.ToLower() == "clerk")
                        {
                            drpOrderStatus.Items.Clear();
                            drpOrderStatus.Items.Add(new ListItem("Pending", "Pending"));
                            drpOrderStatus.Items.Add(new ListItem("Factory", "Factory"));
                            drpOrderStatus.Items.Add(new ListItem("Dispatch Department", "Dispatch Department"));
                            drpOrderStatus.Items.Add(new ListItem("Dispatched", "Dispatched"));
                            drpOrderStatus.Items.Add(new ListItem("Cancel", "Cancel"));

                            // drpOrderStatus.SelectedValue = hdOrderStatus.Value;
                        }


                        drpOrderStatus.SelectedValue = hdOrderStatus.Value;
                        if (hdnIsTallyImport.Value == "False")
                        {
                            e.Row.BackColor = System.Drawing.Color.Tomato;
                        }
                        else
                        {
                            if ((Convert.ToString(Session["UserType"]).ToLower() == "factory" || Convert.ToString(Session["UserType"]).ToLower() == "admin") && drpOrderStatus.SelectedValue.ToLower() == "factory")
                            {
                                e.Row.BackColor = System.Drawing.Color.SlateGray;
                            }
                            if ((Convert.ToString(Session["UserType"]).ToLower() == "clerk" || Convert.ToString(Session["UserType"]).ToLower() == "admin") && drpOrderStatus.SelectedValue.ToLower() == "pending")
                            {
                                e.Row.BackColor = System.Drawing.Color.LightGray;
                            }
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetOrderList();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }
    }
}