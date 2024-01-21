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
    public partial class PrintOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {


                    string UserType = "";
                    UserType = Convert.ToString(HttpContext.Current.Session["UserType"]);
                    txtOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                    lblTotal.Attributes.Add("readonly", "readonly");
                    lblTotal.Text = "0";
                    hdTotalKgCount.Value = "0";


                    bindSale();
                    BindGrid();

                    if (Request.QueryString["q"] != null)
                    {
                        string strKey = Convert.ToString(Request.QueryString["q"]);
                        Common cmn = new Common();
                        strKey = cmn.Decrypt(strKey);
                        Int32 OrderId = Convert.ToInt32(strKey);
                        GetOrderDetails(OrderId);
                    }
                    if (Request.QueryString["View"] != null && Request.QueryString["View"] == "Y")
                    {
                        btnAddDealer.Attributes.Add("style", "display:none");
                  
                    }
                }
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
                drpsSalesExe.DataSource = dt;
                drpsSalesExe.DataTextField = "UserName";
                drpsSalesExe.DataValueField = "UserID";
                drpsSalesExe.DataBind();


                drpsSalesExe.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
            catch (Exception ex)
            {

                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }

        }

        protected void BindGrid()
        {
            try
            {
                BA_tblProductPacking Objtbl = new BA_tblProductPacking();

                DataTable dt1 = new DataTable();
                Objtbl.ProductID = "1";
                Objtbl.GET_RECORDS_FROM_tblProductById(ref dt1);
                gridEUROXTRA.DataSource = dt1;
                gridEUROXTRA.DataBind();


                DataTable dt2 = new DataTable();
                Objtbl.ProductID = "2";
                Objtbl.GET_RECORDS_FROM_tblProductById(ref dt2);
                grdEUROWP.DataSource = dt2;
                grdEUROWP.DataBind();


                DataTable dt3 = new DataTable();
                Objtbl.ProductID = "3";
                Objtbl.GET_RECORDS_FROM_tblProductById(ref dt3);
                grdeuro2in1.DataSource = dt3;
                grdeuro2in1.DataBind();


                DataTable dt4 = new DataTable();
                Objtbl.ProductID = "4";
                Objtbl.GET_RECORDS_FROM_tblProductById(ref dt4);
                grdExtreme.DataSource = dt4;
                grdExtreme.DataBind();


                DataTable dt5 = new DataTable();
                Objtbl.ProductID = "5";
                Objtbl.GET_RECORDS_FROM_tblProductById(ref dt5);
                grdEuroUltra.DataSource = dt5;
                grdEuroUltra.DataBind();


                DataTable dt6 = new DataTable();
                Objtbl.ProductID = "6";
                Objtbl.GET_RECORDS_FROM_tblProductById(ref dt6);
                GrdPvcGlue.DataSource = dt6;
                GrdPvcGlue.DataBind();

                DataTable dt7 = new DataTable();
                Objtbl.ProductID = "7";
                Objtbl.GET_RECORDS_FROM_tblProductById(ref dt7);
                grdWoodStrong.DataSource = dt7;
                grdWoodStrong.DataBind();

            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void txtDealerCodeSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (GetDealerRecord())
                {
                    lblErrorMessage.Text = "";
                }
                else
                {
                    lblErrorMessage.Text = CommMessage.DealerNotfound;
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }



  
      
        protected bool GetDealerRecord()
        {
            try
            {
                BA_tblDealer ObjDealer = new BA_tblDealer();
                DataTable dt = new DataTable();

                ObjDealer.DealerCode = txtDealerCodeSearch.Text;
                ObjDealer.GET_RECORDS_FROM_tblDealer_ByCode(ref dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    txtDealerCode.Text = Convert.ToString(dt.Rows[0]["DealerCode"]);
                    txtDealerName.Text = Convert.ToString(dt.Rows[0]["DealerName"]);
                    txtContactName.Text = Convert.ToString(dt.Rows[0]["ContactName"]);
                    txtAddress.Text = Convert.ToString(dt.Rows[0]["Address"]);
                    txtArea.Text = Convert.ToString(dt.Rows[0]["Area"]);
                    txtPhoneNo.Text = Convert.ToString(dt.Rows[0]["Phone"]);
                    txtGST.Text = Convert.ToString(dt.Rows[0]["GST"]);
                    txtpincode.Text = Convert.ToString(dt.Rows[0]["Pincode"]);
                    hdDelaerId.Value = Convert.ToString(dt.Rows[0]["DealerId"]);
                    //   btnSaveDealer.Visible = false;
                    return true;
                }
                else
                {
                    txtDealerCode.Text = "";
                    txtDealerName.Text = "";
                    txtContactName.Text = "";
                    txtAddress.Text = "";
                    txtArea.Text = "";
                    txtPhoneNo.Text = "";
                    txtGST.Text = "";
                    txtpincode.Text = "";
                    hdDelaerId.Value = "";
                    //  btnSaveDealer.Visible = false;
                    return false;
                    // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "alert('dealer not found, pls try again.');", true);
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
                return false;
            }
        }

    
        public decimal Caltotal(decimal totalkg, int ProductQty)
        {
            decimal totalkgcount = 0;
            try
            {

                totalkgcount = (totalkg * ProductQty);
            }
            catch (Exception)
            {
            }
            return totalkgcount;

        }


    

        protected void GetOrderDetails(Int32 OrderId)
        {
            try
            {
                BA_tblOrder ObjOrder = new BA_tblOrder();
                DataTable _dt = new DataTable();
                ObjOrder.OrderID = Convert.ToString(OrderId);
                ObjOrder.GET_RECORDS_FROM_tblOrderByOrderId(ref _dt);

                if (_dt != null)
                {
                    txtOrderDate.Text = Convert.ToDateTime(Convert.ToString(_dt.Rows[0]["OrderDate"])).ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                    drpOrderStatus.SelectedValue = Convert.ToString(_dt.Rows[0]["OrderStatus"]);
                    txttransport.Text = Convert.ToString(_dt.Rows[0]["Transport"]);
                    txtOther.Text = Convert.ToString(_dt.Rows[0]["Other"]);
                    txtPOP.Text = Convert.ToString(_dt.Rows[0]["POP"]);
                    lblTotal.Text = Convert.ToString(_dt.Rows[0]["TotalKgGm"]);
                    hdTotalKgCount.Value = Convert.ToString(_dt.Rows[0]["TotalKgGm"]);
                    hdEditOrderId.Value = Convert.ToString(_dt.Rows[0]["OrderId"]);

                    drpsSalesExe.SelectedValue = Convert.ToString(_dt.Rows[0]["SalesManId"]);

                    txtDealerCode.Text = Convert.ToString(_dt.Rows[0]["DealerCode"]);
                    txtDealerCodeSearch.Text = Convert.ToString(_dt.Rows[0]["DealerCode"]);
                    txtContactName.Text = Convert.ToString(_dt.Rows[0]["ContactName"]);
                    txtDealerName.Text = Convert.ToString(_dt.Rows[0]["DealerName"]);
                    txtAddress.Text = Convert.ToString(_dt.Rows[0]["Address"]);
                    txtArea.Text = Convert.ToString(_dt.Rows[0]["Area"]);
                    txtPhoneNo.Text = Convert.ToString(_dt.Rows[0]["Phone"]);
                    txtGST.Text = Convert.ToString(_dt.Rows[0]["GST"]);
                    txtpincode.Text = Convert.ToString(_dt.Rows[0]["Pincode"]);
                    hdDelaerId.Value = Convert.ToString(_dt.Rows[0]["DealerId"]);


                    DataTable _dtOrder = new DataTable();
                    ObjOrder.GET_RECORDS_FROM_tblOrderByOrderIdDetails(ref _dtOrder);

                    if (_dtOrder != null && _dtOrder.Rows.Count > 0)
                    {
                        for (int i = 0; i < _dtOrder.Rows.Count; i++)
                        {
                            for (int k = 0; k < gridEUROXTRA.Rows.Count; k++)
                            {
                                HiddenField HdnProductPckId1 = gridEUROXTRA.Rows[k].FindControl("HdnProductPckId1") as HiddenField;
                                string strProductPckId = HdnProductPckId1.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txtEUROXTRA = gridEUROXTRA.Rows[k].FindControl("txtEUROXTRA") as TextBox;
                                    txtEUROXTRA.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));

                                    TextBox txtschemeEUROXTRA = gridEUROXTRA.Rows[k].FindControl("txtschemeEUROXTRA") as TextBox;
                                    txtschemeEUROXTRA.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);

                                    HiddenField hdEUROXTRA = gridEUROXTRA.Rows[k].FindControl("hdEUROXTRA") as HiddenField;
                                    hdEUROXTRA.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));




                                }
                            }

                            for (int k = 0; k < grdEUROWP.Rows.Count; k++)
                            {
                                HiddenField HdnProductPckId2 = grdEUROWP.Rows[k].FindControl("HdnProductPckId2") as HiddenField;
                                string strProductPckId = HdnProductPckId2.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txteurowp = grdEUROWP.Rows[k].FindControl("txteurowp") as TextBox;
                                    HiddenField hdeurowp = grdEUROWP.Rows[k].FindControl("hdeurowp") as HiddenField;
                                    txteurowp.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                    hdeurowp.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));

                                    TextBox txtschemeEUROWP = grdEUROWP.Rows[k].FindControl("txtschemeEUROWP") as TextBox;
                                    txtschemeEUROWP.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                }
                            }

                            for (int k = 0; k < grdeuro2in1.Rows.Count; k++)
                            {
                                HiddenField HdnProductPckId3 = grdeuro2in1.Rows[k].FindControl("HdnProductPckId3") as HiddenField;
                                string strProductPckId = HdnProductPckId3.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txteuro2in1 = grdeuro2in1.Rows[k].FindControl("txteuro2in1") as TextBox;
                                    HiddenField hdeuro2in1 = grdeuro2in1.Rows[k].FindControl("hdeuro2in1") as HiddenField;
                                    txteuro2in1.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                    hdeuro2in1.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));

                                    TextBox txtschemeeuro2in1 = grdeuro2in1.Rows[k].FindControl("txtschemeeuro2in1") as TextBox;
                                    txtschemeeuro2in1.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                }
                            }

                            for (int k = 0; k < grdExtreme.Rows.Count; k++)
                            {
                                HiddenField HdnProductPckId4 = grdExtreme.Rows[k].FindControl("HdnProductPckId4") as HiddenField;
                                string strProductPckId = HdnProductPckId4.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txtExtreme = grdExtreme.Rows[k].FindControl("txtExtreme") as TextBox;
                                    HiddenField hdExtreme = grdExtreme.Rows[k].FindControl("hdExtreme") as HiddenField;
                                    txtExtreme.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                    hdExtreme.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));

                                    TextBox txtschemeExtreme = grdExtreme.Rows[k].FindControl("txtschemeExtreme") as TextBox;
                                    txtschemeExtreme.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                }
                            }

                            for (int k = 0; k < grdEuroUltra.Rows.Count; k++)
                            {
                                HiddenField HdnProductPckId5 = grdEuroUltra.Rows[k].FindControl("HdnProductPckId5") as HiddenField;
                                string strProductPckId = HdnProductPckId5.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txtEuroUltra = grdEuroUltra.Rows[k].FindControl("txtEuroUltra") as TextBox;
                                    HiddenField hdEuroUltra = grdEuroUltra.Rows[k].FindControl("hdEuroUltra") as HiddenField;
                                    txtEuroUltra.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                    hdEuroUltra.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));

                                    TextBox txtschemeEuroUltra = grdEuroUltra.Rows[k].FindControl("txtschemeEuroUltra") as TextBox;
                                    txtschemeEuroUltra.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                }
                            }

                            for (int k = 0; k < GrdPvcGlue.Rows.Count; k++)
                            {
                                HiddenField HdnProductPckId6 = GrdPvcGlue.Rows[k].FindControl("HdnProductPckId6") as HiddenField;
                                string strProductPckId = HdnProductPckId6.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txtPVcGlue = GrdPvcGlue.Rows[k].FindControl("txtPVcGlue") as TextBox;
                                    HiddenField hdPVcGlue = GrdPvcGlue.Rows[k].FindControl("hdPVcGlue") as HiddenField;
                                    txtPVcGlue.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                    hdPVcGlue.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));

                                    TextBox txtschemePvcGlue = GrdPvcGlue.Rows[k].FindControl("txtschemePvcGlue") as TextBox;
                                    txtschemePvcGlue.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                }
                            }

                            for (int k = 0; k < grdWoodStrong.Rows.Count; k++)
                            {
                                HiddenField HdnProductPckId7 = grdWoodStrong.Rows[k].FindControl("HdnProductPckId7") as HiddenField;
                                string strProductPckId = HdnProductPckId7.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txtWoodStrong = grdWoodStrong.Rows[k].FindControl("txtWoodStrong") as TextBox;
                                    HiddenField hdWoodStrong = grdWoodStrong.Rows[k].FindControl("hdWoodStrong") as HiddenField;
                                    txtWoodStrong.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                    hdWoodStrong.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));

                                    TextBox txtschemeWoodStrong = grdWoodStrong.Rows[k].FindControl("txtschemeWoodStrong") as TextBox;
                                    txtschemeWoodStrong.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                }
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


        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrderList.aspx", false);
        }


        protected void gridEUROXTRA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme1") as HiddenField;
                if (Convert.ToBoolean(HdnIsScheme1.Value))
                {
                    Label lblschEUROXTRA = e.Row.FindControl("lblschEUROXTRA") as Label;
                    lblschEUROXTRA.Visible = true;

                    TextBox txtschemeEUROXTRA = e.Row.FindControl("txtschemeEUROXTRA") as TextBox;
                    txtschemeEUROXTRA.Visible = true;

                }
            }
        }

        protected void grdEUROWP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme2") as HiddenField;
                if (Convert.ToBoolean(HdnIsScheme1.Value))
                {
                    Label lblschEUROWP = e.Row.FindControl("lblschEUROWP") as Label;
                    lblschEUROWP.Visible = true;

                    TextBox txtschemeEUROWP = e.Row.FindControl("txtschemeEUROWP") as TextBox;
                    txtschemeEUROWP.Visible = true;

                }
            }
        }

        protected void grdeuro2in1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme3") as HiddenField;
                if (Convert.ToBoolean(HdnIsScheme1.Value))
                {
                    Label lblscheuro2in1 = e.Row.FindControl("lblscheuro2in1") as Label;
                    lblscheuro2in1.Visible = true;

                    TextBox txtschemeeuro2in1 = e.Row.FindControl("txtschemeeuro2in1") as TextBox;
                    txtschemeeuro2in1.Visible = true;

                }
            }

        }

        protected void grdExtreme_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme4") as HiddenField;
                if (Convert.ToBoolean(HdnIsScheme1.Value))
                {
                    Label lblschExtreme = e.Row.FindControl("lblschExtreme") as Label;
                    lblschExtreme.Visible = true;

                    TextBox txtschemeExtreme = e.Row.FindControl("txtschemeExtreme") as TextBox;
                    txtschemeExtreme.Visible = true;

                }
            }

        }

        protected void grdEuroUltra_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme5") as HiddenField;
                if (Convert.ToBoolean(HdnIsScheme1.Value))
                {
                    Label lblschEuroUltra = e.Row.FindControl("lblschEuroUltra") as Label;
                    lblschEuroUltra.Visible = true;

                    TextBox txtschemeEuroUltra = e.Row.FindControl("txtschemeEuroUltra") as TextBox;
                    txtschemeEuroUltra.Visible = true;

                }
            }
        }

        protected void GrdPvcGlue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme6") as HiddenField;
                if (Convert.ToBoolean(HdnIsScheme1.Value))
                {
                    Label lblschPvcGlue = e.Row.FindControl("lblschPvcGlue") as Label;
                    lblschPvcGlue.Visible = true;

                    TextBox txtschemePvcGlue = e.Row.FindControl("txtschemePvcGlue") as TextBox;
                    txtschemePvcGlue.Visible = true;

                }
            }
        }

        protected void grdWoodStrong_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme7") as HiddenField;
                if (Convert.ToBoolean(HdnIsScheme1.Value))
                {
                    Label lblschWoodStrong = e.Row.FindControl("lblschWoodStrong") as Label;
                    lblschWoodStrong.Visible = true;

                    TextBox txtschemeWoodStrong = e.Row.FindControl("txtschemeWoodStrong") as TextBox;
                    txtschemeWoodStrong.Visible = true;

                }
            }
        }
    }
}