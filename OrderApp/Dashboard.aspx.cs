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
    public partial class Dashboard : System.Web.UI.Page
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


                    bindSale();
                    GetRecords();
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
                GetRecords();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        public void bindSale()
        {
            BA_tblUser ObjBA_tblUser = new BA_tblUser();
            DataTable dt = new DataTable();
            try
            {
                ObjBA_tblUser.SELECT_ALL_tblUserSalesman(ref dt);
                drpSalesExecutive.DataSource = dt;
                drpSalesExecutive.DataTextField = "UserName";
                drpSalesExecutive.DataValueField = "UserID";
                drpSalesExecutive.DataBind();


                drpSalesExecutive.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
            catch (Exception ex)
            {

                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }

        }



        protected void GetRecords()
        {
            try
            {
                

                BA_tblDashboard objBA_tblDashboard = new BA_tblDashboard();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                objBA_tblDashboard.FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
                objBA_tblDashboard.ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
                objBA_tblDashboard.UserId = Convert.ToInt32(drpSalesExecutive.SelectedValue);
                objBA_tblDashboard.UserType = Convert.ToString(Session["UserType"]);

                objBA_tblDashboard.GET_RECORDS_FOR_Dashboard(ref ds);

                if (ds != null)
                {
                    decimal MainTotalKg = 0, FreeTotalKg = 0, DealerTotalKg = 0;
                    int MainTotalCount = 0, FreeTotalCount = 0, DealerTotalCount = 0;
                    int MainFactoryCnt = 0, MainPendingCnt = 0, MainDispatchCnt = 0, MainDispatchdeptCnt = 0, FreeFactoryCnt = 0, FreePendingCnt = 0,
                        FreeDispatchCnt = 0, FreeDispatchdeptCNT = 0, DealerFactoryCnt = 0, DealerPendingCnt = 0, DealerDispatchCnt = 0, DealerDispatchdeptCnt= 0;

                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (Convert.ToString(dr["OrderType"]).ToLower() == CommMessage.OrderType_Order.ToLower() && Convert.ToString(dr["OrderStatus"]) == "Factory")
                            {
                                MainFactoryCnt = Convert.ToInt32(dr["TotalOrderCount"]);
                                MainTotalCount += Convert.ToInt32(dr["TotalOrderCount"]);
                                MainTotalKg += Convert.ToDecimal(dr["TotalKg"]);
                            }

                            if (Convert.ToString(dr["OrderType"]).ToLower() == CommMessage.OrderType_Order.ToLower() && Convert.ToString(dr["OrderStatus"]) == "Pending")
                            {
                                MainPendingCnt = Convert.ToInt32(dr["TotalOrderCount"]);
                                MainTotalCount += Convert.ToInt32(dr["TotalOrderCount"]);
                                MainTotalKg += Convert.ToDecimal(dr["TotalKg"]);
                            }

                            if (Convert.ToString(dr["OrderType"]).ToLower() == CommMessage.OrderType_Order.ToLower() && Convert.ToString(dr["OrderStatus"]) == "Dispatch Department")
                            {
                                MainDispatchdeptCnt = Convert.ToInt32(dr["TotalOrderCount"]);
                                MainTotalCount += Convert.ToInt32(dr["TotalOrderCount"]);
                                MainTotalKg += Convert.ToDecimal(dr["TotalKg"]);
                            }

                            if (Convert.ToString(dr["OrderType"]).ToLower() == CommMessage.OrderType_Order.ToLower() && Convert.ToString(dr["OrderStatus"]) == "Dispatched")
                            {
                                MainDispatchCnt = Convert.ToInt32(dr["TotalOrderCount"]);
                                MainTotalCount += Convert.ToInt32(dr["TotalOrderCount"]);
                                MainTotalKg += Convert.ToDecimal(dr["TotalKg"]);
                            }

                            if (Convert.ToString(dr["OrderType"]).ToLower() == CommMessage.OrderType_withbillFreeScheme.ToLower() && Convert.ToString(dr["OrderStatus"]) == "Factory")
                            {
                                FreeFactoryCnt = Convert.ToInt32(dr["TotalOrderCount"]);
                                FreeTotalCount += Convert.ToInt32(dr["TotalOrderCount"]);
                                FreeTotalKg += Convert.ToDecimal(dr["TotalKg"]);
                            }

                            if (Convert.ToString(dr["OrderType"]).ToLower() == CommMessage.OrderType_withbillFreeScheme.ToLower() && Convert.ToString(dr["OrderStatus"]) == "Pending")
                            {
                                FreePendingCnt = Convert.ToInt32(dr["TotalOrderCount"]);
                                FreeTotalCount += Convert.ToInt32(dr["TotalOrderCount"]);
                                FreeTotalKg += Convert.ToDecimal(dr["TotalKg"]);
                            }
                            if (Convert.ToString(dr["OrderType"]).ToLower() == CommMessage.OrderType_withbillFreeScheme.ToLower() && Convert.ToString(dr["OrderStatus"]) == "Dispatch Department")
                            {
                                FreeDispatchdeptCNT = Convert.ToInt32(dr["TotalOrderCount"]);
                                FreeTotalCount += Convert.ToInt32(dr["TotalOrderCount"]);
                                FreeTotalKg += Convert.ToDecimal(dr["TotalKg"]);
                            }

                            if (Convert.ToString(dr["OrderType"]).ToLower() == CommMessage.OrderType_withbillFreeScheme.ToLower() && Convert.ToString(dr["OrderStatus"]) == "Dispatched")
                            {
                                FreeDispatchCnt = Convert.ToInt32(dr["TotalOrderCount"]);
                                FreeTotalCount += Convert.ToInt32(dr["TotalOrderCount"]);
                                FreeTotalKg += Convert.ToDecimal(dr["TotalKg"]);
                            }

                            if (Convert.ToString(dr["OrderType"]).ToLower() == CommMessage.OrderType_FreeScheme.ToLower() && Convert.ToString(dr["OrderStatus"]) == "Factory")
                            {
                                DealerFactoryCnt = Convert.ToInt32(dr["TotalOrderCount"]);
                                DealerTotalCount += Convert.ToInt32(dr["TotalOrderCount"]);
                                DealerTotalKg += Convert.ToDecimal(dr["TotalKg"]);
                            }

                            if (Convert.ToString(dr["OrderType"]).ToLower() == CommMessage.OrderType_FreeScheme.ToLower() && Convert.ToString(dr["OrderStatus"]) == "Pending")
                            {
                                DealerPendingCnt = Convert.ToInt32(dr["TotalOrderCount"]);
                                DealerTotalCount += Convert.ToInt32(dr["TotalOrderCount"]);
                                DealerTotalKg += Convert.ToDecimal(dr["TotalKg"]);
                            }
                            if (Convert.ToString(dr["OrderType"]).ToLower() == CommMessage.OrderType_FreeScheme.ToLower() && Convert.ToString(dr["OrderStatus"]) == "Dispatch Department")
                            {
                                DealerDispatchdeptCnt = Convert.ToInt32(dr["TotalOrderCount"]);
                                DealerTotalCount += Convert.ToInt32(dr["TotalOrderCount"]);
                                DealerTotalKg += Convert.ToDecimal(dr["TotalKg"]);
                            }

                            if (Convert.ToString(dr["OrderType"]).ToLower() == CommMessage.OrderType_FreeScheme.ToLower() && Convert.ToString(dr["OrderStatus"]) == "Dispatched")
                            {
                                DealerDispatchCnt = Convert.ToInt32(dr["TotalOrderCount"]);
                                DealerTotalCount += Convert.ToInt32(dr["TotalOrderCount"]);
                                DealerTotalKg += Convert.ToDecimal(dr["TotalKg"]);
                            }
                        }

                        DataTable dt1 = new DataTable();
                        if (ds.Tables.Count > 1)
                        {
                            dt1 = ds.Tables[1];
                        }
                        else
                        {
                            dt1 = null;
                        }

                        Session["dtMainOrder"] = dt1;

                        grdMainOrder.DataSource = dt1;
                        grdMainOrder.DataBind();

                        DataTable dt2 = new DataTable();
                        if (ds.Tables.Count > 2)
                        {
                            dt2 = ds.Tables[2];
                        }
                        else
                        {
                            dt2 = null;
                        }

                        Session["dtFreeOrder"] = dt2;

                        grdFreeOrder.DataSource = dt2;
                        grdFreeOrder.DataBind();

                        DataTable dt3 = new DataTable();
                        if (ds.Tables.Count > 3)
                        {
                            dt3 = ds.Tables[3];
                        }
                        else
                        {
                            dt3 = null;
                        }

                        Session["dtDealerOrder"] = dt3;

                        grdDealerOrder.DataSource = dt3;
                        grdDealerOrder.DataBind();
                    }

                    MainTotalOrder.InnerText = Convert.ToString(MainTotalCount);
                    MainTotalKgs.InnerText = Convert.ToString(MainTotalKg);
                    MainFactory.InnerText = Convert.ToString(MainFactoryCnt);
                    MainPending.InnerText = Convert.ToString(MainPendingCnt);
                    MainDispatch.InnerText = Convert.ToString(MainDispatchCnt);
                    MainDispatchdept.InnerText = Convert.ToString(MainDispatchdeptCnt);

                    FreeTotalOrder.InnerText = Convert.ToString(FreeTotalCount);
                    FreeTotalKgs.InnerText = Convert.ToString(FreeTotalKg);
                    FreeFactory.InnerText = Convert.ToString(FreeFactoryCnt);
                    FreePending.InnerText = Convert.ToString(FreePendingCnt);
                    FreeDispatch.InnerText = Convert.ToString(FreeDispatchCnt);
                    FreeDispatchdept.InnerText = Convert.ToString(FreeDispatchdeptCNT);

                    DealerTotalOrder.InnerText = Convert.ToString(DealerTotalCount);
                    DealerTotalKgs.InnerText = Convert.ToString(DealerTotalKg);
                    DealerFactory.InnerText = Convert.ToString(DealerFactoryCnt);
                    DealerPending.InnerText = Convert.ToString(DealerPendingCnt);
                    DealerDispatch.InnerText = Convert.ToString(DealerDispatchCnt);
                    DealerDispatchdept.InnerText = Convert.ToString(DealerDispatchdeptCnt);

                }
                else
                {
                    MainTotalOrder.InnerText = "0";
                    MainTotalKgs.InnerText = "0";
                    MainPending.InnerText = "0";
                    MainDispatch.InnerText = "0";
                    MainFactory.InnerText = "0";

                    FreeTotalOrder.InnerText = "0";
                    FreeTotalKgs.InnerText = "0";
                    FreePending.InnerText = "0";
                    FreeDispatch.InnerText = "0";
                    FreeFactory.InnerText = "0";

                    DealerTotalOrder.InnerText = "0";
                    DealerTotalKgs.InnerText = "0";
                    DealerPending.InnerText = "0";
                    DealerDispatch.InnerText = "0";
                    DealerFactory.InnerText = "0";
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdMainOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string Order = Convert.ToString(e.CommandArgument);
                string OrderId = "", OrderType = "";

                string[] strOrder = Order.Split('|');
                OrderId = strOrder[0];
                OrderType = strOrder[1];

                Common cmn = new Common();
                string strEncryptValue = cmn.Encrypt(Convert.ToString(OrderId));


                if (e.CommandName == "ViewValue")
                {
                    // Response.Redirect("PrintOrder.aspx?q=" + strEncryptValue + "&View=Y" + "&OrderType=" + OrderType, false);
                    Response.Redirect("AddOrder.aspx?q=" + strEncryptValue + "&View=Y&BackButton=N" + "&OrderType=" + OrderType, false);
                }

                if (e.CommandName == "PrintValue")
                {
                    PrintData(OrderId, strEncryptValue, OrderType);
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdMainOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dtOrder = new DataTable();
                dtOrder = Session["dtMainOrder"] as DataTable;

                grdMainOrder.PageIndex = e.NewPageIndex;
                grdMainOrder.DataSource = dtOrder;
                grdMainOrder.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdFreeOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string Order = Convert.ToString(e.CommandArgument);
                string OrderId = "", OrderType = "";

                string[] strOrder = Order.Split('|');
                OrderId = strOrder[0];
                OrderType = strOrder[1];

                Common cmn = new Common();
                string strEncryptValue = cmn.Encrypt(Convert.ToString(OrderId));

                if (e.CommandName == "ViewValue")
                {
                    // Response.Redirect("PrintOrder.aspx?q=" + strEncryptValue + "&View=Y" + "&OrderType=" + OrderType, false);
                    Response.Redirect("FreeOrder.aspx?q=" + strEncryptValue + "&View=Y&BackButton=N" + "&OrderType=" + OrderType, false);
                }

                if (e.CommandName == "PrintValue")
                {
                    PrintData(OrderId, strEncryptValue, OrderType);
                }
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

        protected void grdFreeOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dtOrder = new DataTable();
                dtOrder = Session["dtFreeOrder"] as DataTable;

                grdFreeOrder.PageIndex = e.NewPageIndex;
                grdFreeOrder.DataSource = dtOrder;
                grdFreeOrder.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdDealerOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dtOrder = new DataTable();
                dtOrder = Session["dtDealerOrder"] as DataTable;

                grdDealerOrder.PageIndex = e.NewPageIndex;
                grdDealerOrder.DataSource = dtOrder;
                grdDealerOrder.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

             
        protected void grdDealerOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string Order = Convert.ToString(e.CommandArgument);
                string OrderId = "", OrderType = "";

                string[] strOrder = Order.Split('|');
                OrderId = strOrder[0];
                OrderType = strOrder[1];

                Common cmn = new Common();
                string strEncryptValue = cmn.Encrypt(Convert.ToString(OrderId));

                if (e.CommandName == "ViewValue")
                {
                    // Response.Redirect("PrintOrder.aspx?q=" + strEncryptValue + "&View=Y" + "&OrderType=" + OrderType, false);
                    Response.Redirect("AddDealerOrderScheme.aspx?q=" + strEncryptValue + "&View=Y&BackButton=N" + "&OrderType=" + OrderType, false);
                }

                if (e.CommandName == "PrintValue")
                {
                    PrintData(OrderId, strEncryptValue, OrderType);
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        public void PrintData(string OrderId, string strEncryptValue, string OrderType)
        {
            try
            {
                if (Convert.ToString(Session["UserType"]) == "Factory" || Convert.ToString(Session["UserType"]) == "Admin")
                {
                    if (UpdateORderStatus(OrderId))
                    {

                        lblerror.Text = "";

                        string strUrl = "RptPrintOrder.aspx?q=" + strEncryptValue + "&Print=Y" + "&OrderType=" + OrderType;
                        Response.Write("<script>");
                        Response.Write("window.open('" + strUrl + "','_blank')");
                        Response.Write("</script>");
                        GetRecords();
                    }
                    else
                    {
                        lblerror.Text = CommMessage.orderstatusnotupdate;
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
            catch (Exception)
            {

                throw;
            }
        }

        protected void grdMainOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)e.Row;
            HiddenField hdnIsTallyImport = (HiddenField)gvRow.FindControl("hdnIsTallyImport");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string order = e.Row.Cells[5].Text;
                if (hdnIsTallyImport.Value == "False")
                {
                    e.Row.BackColor = System.Drawing.Color.Tomato;
                }
                else
                {
                    if ((Convert.ToString(Session["UserType"]).ToLower() == "factory" || Convert.ToString(Session["UserType"]).ToLower() == "admin") && order.ToLower() == "factory")
                    {
                        e.Row.BackColor = System.Drawing.Color.LightGray;
                    }
                    if ((Convert.ToString(Session["UserType"]).ToLower() == "clerk" || Convert.ToString(Session["UserType"]).ToLower() == "admin") && order.ToLower() == "pending")
                    {
                        e.Row.BackColor = System.Drawing.Color.LightGray;
                    }
                }
            }

           
        }

        protected void grdFreeOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
             GridViewRow gvRow = (GridViewRow)e.Row;
             HiddenField hdnIsTallyImport = (HiddenField)gvRow.FindControl("hdnIsTallyImport");
             if (e.Row.RowType == DataControlRowType.DataRow)
             {
                 string order = e.Row.Cells[5].Text;
                 if (hdnIsTallyImport.Value == "False")
                 {
                     e.Row.BackColor = System.Drawing.Color.Tomato;
                 }
                 else
                 {
                     if ((Convert.ToString(Session["UserType"]).ToLower() == "factory" || Convert.ToString(Session["UserType"]).ToLower() == "admin") && order.ToLower() == "factory")
                     {
                         e.Row.BackColor = System.Drawing.Color.LightGray;
                     }
                     if ((Convert.ToString(Session["UserType"]).ToLower() == "clerk" || Convert.ToString(Session["UserType"]).ToLower() == "admin") && order.ToLower() == "pending")
                     {
                         e.Row.BackColor = System.Drawing.Color.LightGray;
                     }
                 }
             }
        }

        protected void grdDealerOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
             GridViewRow gvRow = (GridViewRow)e.Row;
             HiddenField hdnIsTallyImport = (HiddenField)gvRow.FindControl("hdnIsTallyImport");
             if (e.Row.RowType == DataControlRowType.DataRow)
             {
                 string order = e.Row.Cells[5].Text;
                 if (hdnIsTallyImport.Value == "False")
                 {
                     e.Row.BackColor = System.Drawing.Color.Tomato;
                 }
                 else
                 {
                     if ((Convert.ToString(Session["UserType"]).ToLower() == "factory" || Convert.ToString(Session["UserType"]).ToLower() == "admin") && order.ToLower() == "factory")
                     {
                         e.Row.BackColor = System.Drawing.Color.LightGray;
                     }
                     if ((Convert.ToString(Session["UserType"]).ToLower() == "clerk" || Convert.ToString(Session["UserType"]).ToLower() == "admin") && order.ToLower() == "pending")
                     {
                         e.Row.BackColor = System.Drawing.Color.LightGray;
                     }
                 }
             }
        }

    }
}