using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class OrderReport : System.Web.UI.Page
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



                    string UserType = "";
                    UserType = Convert.ToString(HttpContext.Current.Session["UserType"]);

                    if (UserType == "Factory")
                    {
                        drpOrderStatus.Items.Clear();
                        drpOrderStatus.Items.Add(new ListItem("All", "All"));
                        drpOrderStatus.Items.Add(new ListItem("Factory", "Factory"));
                        drpOrderStatus.Items.Add(new ListItem("Dispatch Department", "Dispatch Department"));
                        drpOrderStatus.Items.Add(new ListItem("Dispatched", "Dispatched"));

                        // drpOrderStatus.SelectedValue = hdOrderStatus.Value;


                    }
                    if (UserType.ToLower() == "admin" || UserType.ToLower() == "clerk")
                    {
                        drpOrderStatus.Items.Clear();
                        drpOrderStatus.Items.Add(new ListItem("All", "All"));
                        drpOrderStatus.Items.Add(new ListItem("Pending", "Pending"));
                        drpOrderStatus.Items.Add(new ListItem("Factory", "Factory"));
                        drpOrderStatus.Items.Add(new ListItem("Dispatch Department", "Dispatch Department"));
                        drpOrderStatus.Items.Add(new ListItem("Dispatched", "Dispatched"));
                        drpOrderStatus.Items.Add(new ListItem("Cancel", "Cancel"));

                        // drpOrderStatus.SelectedValue = hdOrderStatus.Value;
                    }


                    //  bindSale();
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
                //drpSalesExecutive.DataSource = dt;
                //drpSalesExecutive.DataTextField = "UserName";
                //drpSalesExecutive.DataValueField = "UserID";
                //drpSalesExecutive.DataBind();


                //drpSalesExecutive.Items.Insert(0, new ListItem("-- Select --", "0"));
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
                objBA_tblDashboard.UserId = 0;// Convert.ToInt32(drpSalesExecutive.SelectedValue);
                objBA_tblDashboard.UserType = Convert.ToString(Session["UserType"]);
                string OrderStatus = drpOrderStatus.SelectedValue;
                objBA_tblDashboard.GET_RECORDS_FOR_OrderReport(OrderStatus,ref ds);

                if (ds != null)
                {


                    if (ds.Tables.Count > 0)
                    {
                        grdMainOrder.DataSource = ds.Tables[0];
                        grdMainOrder.DataBind();
                        Session["dtMainOrder"] = ds.Tables[0];

                    }
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



        protected void ExportToExcel(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=OrderReport"+ DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                grdMainOrder.AllowPaging = false;
                this.GetRecords();
                grdMainOrder.RenderControl(hw);
                string style = @"<style> .textmode { mso-number-format:\@; } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }


    }
  
}