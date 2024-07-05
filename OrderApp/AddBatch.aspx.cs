using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class AddBatch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GetOrderList();
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

     

   

        protected void GetOrderList()
        {
            try
            {
                DateTime now = DateTime.Now;
                DataTable dt = new DataTable();
                BA_tblOrder objBA_tblOrder = new BA_tblOrder();
                objBA_tblOrder.OrderType = "Order";
                objBA_tblOrder.UserType = Convert.ToString(HttpContext.Current.Session["UserType"]);
                objBA_tblOrder.SELECT_ALL_tblOrder_orderstatus_Factory(ref dt);

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

        protected void btnSaveBatch_Click(object sender, EventArgs e)
        {
            try
            {
                int ReturnId = 0;
                decimal Totalkg = 0;
                string str = string.Empty;
                string strname = string.Empty;
                var strorderidXml = new StringBuilder();
                strorderidXml.AppendFormat("<root>");

                foreach (GridViewRow gvrow in grdOrder.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
                    if (chk != null & chk.Checked)
                    {
                        //str += grdOrder.DataKeys[gvrow.RowIndex].Value.ToString() + ',';
                        strorderidXml.AppendFormat("<Bathorder OrderId = ''" + grdOrder.DataKeys[gvrow.RowIndex].Value.ToString() + "''/>");

                        Totalkg = Totalkg + Convert.ToDecimal(gvrow.Cells[6].Text);
                    }

                }


                strorderidXml.AppendFormat("</root>");
                var strbatchorder = strorderidXml.ToString().Replace("''", "\"");

                BA_tblBatch ObjBA_tblBatch = new BA_tblBatch();

                ObjBA_tblBatch.BatchRemark = "";
                ObjBA_tblBatch.Batchorder = strbatchorder;
                ObjBA_tblBatch.CreateBy = Convert.ToInt32(Session["UserId"]);
                ObjBA_tblBatch.Totalkg = Totalkg;
                //ObjBA_tblBatch.CreateDate = DateTime.Now;
                //ObjBA_tblBatch.BatachDate = DateTime.Now;
                ObjBA_tblBatch.INSERT_tblBatch(ref ReturnId);


                if (ReturnId > 0)
                {
                    Response.Redirect("BatchList.aspx", false);
                }
                else
                {
                    lblErrorMessage.Text = CommMessage.Recordcouldnotable;
                }

            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e) {
            try {
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