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
    public partial class AddOrderold : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    ViewState["StateID"] = "0";
                    GetStateList();
                    string UserType = "";
                    UserType = Convert.ToString(HttpContext.Current.Session["UserType"]);
                    txtOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                    lblTotal.Attributes.Add("readonly", "readonly");
                    lblTotal.Text = "0";
                    hdTotalKgCount.Value = "0";

                    BindSaleuser();
                    BindProductGrid();

                    lblheading.Text = CommMessage.addOrder;

                    if (Request.QueryString["q"] != null)
                    {
                        string strKey = Convert.ToString(Request.QueryString["q"]);
                        Common cmn = new Common();
                        strKey = cmn.Decrypt(strKey);
                        Int32 OrderId = Convert.ToInt32(strKey);
                        GetOrderDetails(OrderId);
                        lblheading.Text = CommMessage.EditOrder;
                    }
                    if (Request.QueryString["BackButton"] != null && Request.QueryString["BackButton"] == "N")
                        ViewState["BackButton"] = "N";
                    else
                        ViewState["BackButton"] = "Y";

                    if (Request.QueryString["View"] != null && Request.QueryString["View"] == "Y")
                    {
                        lblheading.Text = CommMessage.viewOrder;
                        btnSubmitOrder.Attributes.Add("style", "display:none");
                    }
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        public void GetStateList()
        {
            DataTable dt = new DataTable();
            BA_States ObjBStates = new BA_States();

            ObjBStates.SELECT_ALL_States(ref dt);

            drpStateName.DataSource = dt;
            drpStateName.DataTextField = "state_name";
            drpStateName.DataValueField = "state_id";
            drpStateName.DataBind();
            drpStateName.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        public void BindSaleuser()
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

        protected void BindProductGrid()
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
                Objtbl.ProductID = "9";
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

                DataTable dt8 = new DataTable();
                Objtbl.ProductID = "8";
                Objtbl.GET_RECORDS_FROM_tblProductById(ref dt8);
                grdEuroEWR.DataSource = dt8;
                grdEuroEWR.DataBind();

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
                   
                    if(Convert.ToInt32(ViewState["StateID"]) != Convert.ToInt32(ViewState["OldStateID"]))
                        BindProductGrid(); 
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

        protected void btnSaveDealer_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateForm())
                {
                    BA_tblDealer ObjDealer = new BA_tblDealer();
                    Common Cmn = new Common();
                    ObjDealer.DealerName = txtDealerName.Text;
                    ObjDealer.ContactName = txtContactName.Text;
                    ObjDealer.Address = txtAddress.Text;
                    ObjDealer.Area = txtArea.Text;
                    ObjDealer.GST = txtGST.Text;
                    ObjDealer.Phone = txtPhoneNo.Text;
                    ObjDealer.Pincode = txtpincode.Text;
                    ObjDealer.CreateBy = Convert.ToInt32(Session["UserId"]);
                    ObjDealer.Isdeleted = false;
                    ObjDealer.UpdateBy = Convert.ToInt32(Session["UserId"]);

                    bool output;
                    output = ObjDealer.INSERT_tblDealer();

                    if (output == true)
                    {

                        divAddOrder.Visible = true;
                        btnSubmitOrder.Visible = true;
                        //btnClear.Visible = true;
                        divOtherDetails.Visible = true;
                        GetDealerRecord();

                        if (txtDealerCode.Text == "")
                        {
                            txtDealerCodeSearch.Text = txtDealerName.Text;
                            lbldealercode.Text = "Dealer Name";
                        }
                        else
                        {
                            txtDealerCodeSearch.Text = txtDealerCode.Text;
                            lbldealercode.Text = "Dealer Code";
                        }
                    }
                    else
                    {
                        lblErrorMessage.Text = CommMessage.Recordcouldnotable;
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        private bool ValidateForm()
        {


            if (drpStateName.SelectedValue == "0")
            {
                lblerr.Text = CommMessage.selectstatename;

                return false;
            }



            return true;
        }

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        divAddOrder.Visible = true;
        //        btnSubmitOrder.Visible = true;
        //        //btnClear.Visible = true;
        //        divOtherDetails.Visible = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        BA_ErrorLog ObjError = new BA_ErrorLog();
        //        ObjError.INSERT_ErrorLog(ex);
        //    }
        //}

        protected bool GetDealerRecord()
        {
            try
            {
                lblTotal.Text = "0";
                hdTotalKgCount.Value = "0";
                BA_tblDealer ObjDealer = new BA_tblDealer();
                DataTable dt = new DataTable();

                ObjDealer.DealerCode = txtDealerCodeSearch.Text;
                ObjDealer.GET_RECORDS_FROM_tblDealer_ByCode(ref dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    txtDealerCode.Text = Convert.ToString(dt.Rows[0]["DealerCode"]);
                    txtDealerName.Text = Convert.ToString(dt.Rows[0]["DealerName"]);
                    txtdealernamesearch.Text = Convert.ToString(dt.Rows[0]["DealerName"]);
                    txtContactName.Text = Convert.ToString(dt.Rows[0]["ContactName"]);
                    txtAddress.Text = Convert.ToString(dt.Rows[0]["Address"]);
                    txtArea.Text = Convert.ToString(dt.Rows[0]["Area"]);
                    txtPhoneNo.Text = Convert.ToString(dt.Rows[0]["Phone"]);
                    txtGST.Text = Convert.ToString(dt.Rows[0]["GST"]);
                    txtpincode.Text = Convert.ToString(dt.Rows[0]["Pincode"]);
                    hdDelaerId.Value = Convert.ToString(dt.Rows[0]["DealerId"]);
                    if (ViewState["StateID"] != null && Convert.ToString(ViewState["StateID"]) != "0" )
                    {
                        ViewState["OldStateID"] = ViewState["StateID"];
                    }
                   
                    ViewState["StateID"] = Convert.ToInt32(dt.Rows[0]["StateID"]);
                    return true;
                }
                else
                {
                    txtDealerCode.Text = "";
                    txtdealernamesearch.Text = "";
                    txtDealerName.Text = "";
                    txtContactName.Text = "";
                    txtAddress.Text = "";
                    txtArea.Text = "";
                    txtPhoneNo.Text = "";
                    txtGST.Text = "";
                    txtpincode.Text = "";
                    hdDelaerId.Value = "";
                    ViewState["StateID"] = "0";
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

        protected void btnSubmitOrder_Click(object sender, EventArgs e)
        {
            try
            {
                int ReturnId = 0;
                string XMLData = "";

                BA_tblOrder ObjOrder = new BA_tblOrder();
                if (txtOrderDate.Text == "")
                {
                    lblErrorMessage.Text = CommMessage.enterorderdate;
                }
                else if (drpsSalesExe.SelectedIndex == 0)
                {
                    lblErrorMessage.Text = CommMessage.SelectSalesExecutive;
                }
                else if (hdDelaerId.Value == "")
                {
                    lblErrorMessage.Text = CommMessage.DealerNotfound;
                }
                else
                {

                    ObjOrder.OrderType = CommMessage.OrderType_Order;
                    ObjOrder.DealerId = hdDelaerId.Value;
                    if (hdOrderId.Value == "")
                    {
                        ObjOrder.ParentOrderId = "0";
                    }
                    else
                    {
                        ObjOrder.ParentOrderId = hdOrderId.Value;
                    }
                    ObjOrder.Transport = txttransport.Text;
                    ObjOrder.Other = txtOther.Text;
                    ObjOrder.POP = txtPOP.Text;
                    ObjOrder.SiteDelivery = txtsitedelivery.Text;



                    ObjOrder.CreateBy = Convert.ToInt32(Session["UserId"]);
                    ObjOrder.UpdateBy = Convert.ToInt32(Session["UserId"]);
                    ObjOrder.OrderStatus = drpOrderStatus.SelectedValue;

                    ObjOrder.SalesId = Convert.ToInt32(drpsSalesExe.SelectedValue);


                    try
                    {
                        XMLData = xmlCreate();
                    }
                    catch (Exception ex)
                    {
                        BA_ErrorLog ObjError = new BA_ErrorLog();
                        ObjError.INSERT_ErrorLog(ex);
                    }
                    if (XMLData != "")
                    {

                        ObjOrder.xmlProd = XMLData;
                        ObjOrder.TotalKgGm = Convert.ToDecimal(lblTotal.Text);
                        if (hdEditOrderId.Value != "")
                        {
                            ObjOrder.OrderID = hdEditOrderId.Value;
                            ObjOrder.UPDATE_tblOrder(ref ReturnId);
                        }
                        else
                        {
                            ObjOrder.INSERT_tblOrder(ref ReturnId);
                        }
                        hdOrderId.Value = Convert.ToString(ReturnId);
                        if (ReturnId > 0)
                        {
                            lblErrorMessage.Text = CommMessage.OrderSave;
                            Response.Redirect("OrderList.aspx", false);
                        }
                        else
                        {
                            lblErrorMessage.Text = CommMessage.somethingwrong;
                        }
                    }
                    else
                    {
                        lblErrorMessage.Text = CommMessage.enterprodqty;
                    }
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        public string xmlCreate()
        {
            try
            {
                int kk = 0;
                decimal totalkg = 0;
                #region Product Order Details
                string XML = "";
                XML = "<OrderProduct>";
                for (int i = 0; i < gridEUROXTRA.Rows.Count; i++)
                {
                    HiddenField HdnProductId1 = gridEUROXTRA.Rows[i].FindControl("HdnProductId1") as HiddenField;
                    HiddenField HdnProductPckId1 = gridEUROXTRA.Rows[i].FindControl("HdnProductPckId1") as HiddenField;
                    HiddenField HdnProductPck1 = gridEUROXTRA.Rows[i].FindControl("HdnProductPck1") as HiddenField;
                    HiddenField HdnPackingNos1 = gridEUROXTRA.Rows[i].FindControl("HdnPackingNos1") as HiddenField;
                    HiddenField HdnPackingType1 = gridEUROXTRA.Rows[i].FindControl("HdnPackingType1") as HiddenField;
                    HiddenField HdnIsScheme1 = gridEUROXTRA.Rows[i].FindControl("HdnIsScheme1") as HiddenField;
                    HiddenField HdnTotalProductPckKG1 = gridEUROXTRA.Rows[i].FindControl("HdnTotalProductPckKG1") as HiddenField;

                    TextBox txtEUROXTRA = gridEUROXTRA.Rows[i].FindControl("txtEUROXTRA") as TextBox;
                    DropDownList dropschemeEUROXTRA = gridEUROXTRA.Rows[i].FindControl("dropschemeEUROXTRA") as DropDownList;
                    //TextBox txtschemeEUROXTRA = gridEUROXTRA.Rows[i].FindControl("txtschemeEUROXTRA") as TextBox;/
                    HiddenField hdeuroxtrapcode = gridEUROXTRA.Rows[i].FindControl("hdeuroxtrapcode") as HiddenField;

                    if (Convert.ToString(txtEUROXTRA.Text).Trim() != "")
                    {
                        
                        string pcodeEUROXTRA = GetProductCode(Convert.ToInt32(ViewState["StateID"]), Convert.ToString(dropschemeEUROXTRA.SelectedValue), Convert.ToInt32(HdnProductPckId1.Value));

                        XML += "<TABLE>";
                        XML += "<ProductId>" + HdnProductId1.Value + "</ProductId>";
                        XML += "<ProductPckIds>" + HdnProductPckId1.Value + "</ProductPckIds>";
                        XML += "<ProductPck>" + HdnProductPck1.Value + "</ProductPck>";
                        XML += "<PackingNos>" + HdnPackingNos1.Value + "</PackingNos>";
                        XML += "<PackingType>" + HdnPackingType1.Value + "</PackingType>";
                        XML += "<BoxORNos>Box</BoxORNos>";
                        XML += "<PckTotalKg>" + HdnTotalProductPckKG1.Value + "</PckTotalKg>";
                        XML += "<ProductQty>" + txtEUROXTRA.Text + "</ProductQty>";
                        XML += "<IsScheme>1</IsScheme>";

                        //if (Convert.ToString(txtschemeEUROXTRA.Text) == "")
                        //    XML += "<Scheme>" + Convert.ToString(dropschemeEUROXTRA.SelectedItem) + "</Scheme>";
                        //else
                        //    XML += "<Scheme>" + Convert.ToString(txtschemeEUROXTRA.Text) + "</Scheme>";

                        XML += "<SchemeId>" + Convert.ToString(dropschemeEUROXTRA.SelectedValue) + "</SchemeId>";
                        XML += "<ProductCode>" + pcodeEUROXTRA + "</ProductCode>";

                        XML += "</TABLE>";

                        kk = kk + 1;
                        totalkg = totalkg + Caltotal(Convert.ToDecimal(HdnTotalProductPckKG1.Value), Convert.ToInt32(txtEUROXTRA.Text));
                    }
                }

                for (int i = 0; i < grdEUROWP.Rows.Count; i++)
                {

                    HiddenField HdnProductId2 = grdEUROWP.Rows[i].FindControl("HdnProductId2") as HiddenField;
                    HiddenField HdnProductPckId2 = grdEUROWP.Rows[i].FindControl("HdnProductPckId2") as HiddenField;
                    HiddenField HdnProductPck2 = grdEUROWP.Rows[i].FindControl("HdnProductPck2") as HiddenField;
                    HiddenField HdnPackingNos2 = grdEUROWP.Rows[i].FindControl("HdnPackingNos2") as HiddenField;
                    HiddenField HdnPackingType2 = grdEUROWP.Rows[i].FindControl("HdnPackingType2") as HiddenField;
                    HiddenField HdnIsScheme2 = grdEUROWP.Rows[i].FindControl("HdnIsScheme2") as HiddenField;
                    HiddenField HdnTotalProductPckKG2 = grdEUROWP.Rows[i].FindControl("HdnTotalProductPckKG2") as HiddenField;


                    TextBox txteurowp = grdEUROWP.Rows[i].FindControl("txteurowp") as TextBox;
                    DropDownList dropschemeeurowp = grdEUROWP.Rows[i].FindControl("dropschemeeurowp") as DropDownList;
                    //TextBox txtschemeEUROWP = grdEUROWP.Rows[i].FindControl("txtschemeEUROWP") as TextBox;
                    HiddenField hdeurowppcode = gridEUROXTRA.Rows[i].FindControl("hdeurowppcode") as HiddenField;


                    if (Convert.ToString(txteurowp.Text).Trim() != "")
                    {
                        string pcodeEUROXTRA = GetProductCode(Convert.ToInt32(ViewState["StateID"]), Convert.ToString(dropschemeeurowp.SelectedValue), Convert.ToInt32(HdnProductPckId2.Value));

                        XML += "<TABLE>";
                        XML += "<ProductId>" + HdnProductId2.Value + "</ProductId>";
                        XML += "<ProductPckIds>" + HdnProductPckId2.Value + "</ProductPckIds>";
                        XML += "<ProductPck>" + HdnProductPck2.Value + "</ProductPck>";
                        XML += "<PackingNos>" + HdnPackingNos2.Value + "</PackingNos>";
                        XML += "<PackingType>" + HdnPackingType2.Value + "</PackingType>";
                        XML += "<BoxORNos>Box</BoxORNos>";
                        XML += "<PckTotalKg>" + HdnTotalProductPckKG2.Value + "</PckTotalKg>";
                        XML += "<ProductQty>" + txteurowp.Text + "</ProductQty>";
                        XML += "<IsScheme>1</IsScheme>";

                        //if (Convert.ToString(txtschemeEUROWP.Text) == "")
                        //    XML += "<Scheme>" + Convert.ToString(dropschemeeurowp.SelectedItem) + "</Scheme>";
                        //else
                        //    XML += "<Scheme>" + Convert.ToString(txtschemeEUROWP.Text) + "</Scheme>";

                        XML += "<SchemeId>" + Convert.ToString(dropschemeeurowp.SelectedValue) + "</SchemeId>";
                        XML += "<ProductCode>" + pcodeEUROXTRA + "</ProductCode>";

                        XML += "</TABLE>";
                        kk = kk + 1;
                        totalkg = totalkg + Caltotal(Convert.ToDecimal(HdnTotalProductPckKG2.Value), Convert.ToInt32(txteurowp.Text));
                    }
                }

                for (int i = 0; i < grdeuro2in1.Rows.Count; i++)
                {

                    HiddenField HdnProductId3 = grdeuro2in1.Rows[i].FindControl("HdnProductId3") as HiddenField;
                    HiddenField HdnProductPckId3 = grdeuro2in1.Rows[i].FindControl("HdnProductPckId3") as HiddenField;
                    HiddenField HdnProductPck3 = grdeuro2in1.Rows[i].FindControl("HdnProductPck3") as HiddenField;
                    HiddenField HdnPackingNos3 = grdeuro2in1.Rows[i].FindControl("HdnPackingNos3") as HiddenField;
                    HiddenField HdnPackingType3 = grdeuro2in1.Rows[i].FindControl("HdnPackingType3") as HiddenField;
                    HiddenField HdnIsScheme3 = grdeuro2in1.Rows[i].FindControl("HdnIsScheme3") as HiddenField;
                    HiddenField HdnTotalProductPckKG3 = grdeuro2in1.Rows[i].FindControl("HdnTotalProductPckKG3") as HiddenField;

                    TextBox txteuro2in1 = grdeuro2in1.Rows[i].FindControl("txteuro2in1") as TextBox;
                    DropDownList dropschemeeuro2in1 = grdeuro2in1.Rows[i].FindControl("dropschemeeuro2in1") as DropDownList;
                    //TextBox txtschemeeuro2in1 = grdeuro2in1.Rows[i].FindControl("txtschemeeuro2in1") as TextBox;
                    HiddenField hdeuro2in1pcode = gridEUROXTRA.Rows[i].FindControl("hdeuro2in1pcode") as HiddenField;



                    if (Convert.ToString(txteuro2in1.Text).Trim() != "")
                    {
                        string pcodeeuro2in1 = GetProductCode(Convert.ToInt32(ViewState["StateID"]), Convert.ToString(dropschemeeuro2in1.SelectedValue), Convert.ToInt32(HdnProductPckId3.Value));


                        XML += "<TABLE>";
                        XML += "<ProductId>" + HdnProductId3.Value + "</ProductId>";
                        XML += "<ProductPckIds>" + HdnProductPckId3.Value + "</ProductPckIds>";
                        XML += "<ProductPck>" + HdnProductPck3.Value + "</ProductPck>";
                        XML += "<PackingNos>" + HdnPackingNos3.Value + "</PackingNos>";
                        XML += "<PackingType>" + HdnPackingType3.Value + "</PackingType>";
                        XML += "<BoxORNos>Box</BoxORNos>";
                        XML += "<PckTotalKg>" + HdnTotalProductPckKG3.Value + "</PckTotalKg>";
                        XML += "<ProductQty>" + txteuro2in1.Text + "</ProductQty>";
                        XML += "<IsScheme>1</IsScheme>";

                        //if (Convert.ToString(txtschemeeuro2in1.Text) == "")
                        //    XML += "<Scheme>" + Convert.ToString(dropschemeeuro2in1.SelectedItem) + "</Scheme>";
                        //else
                        //    XML += "<Scheme>" + Convert.ToString(txtschemeeuro2in1.Text) + "</Scheme>";

                        XML += "<SchemeId>" + Convert.ToString(dropschemeeuro2in1.SelectedValue) + "</SchemeId>";
                        XML += "<ProductCode>" + pcodeeuro2in1 + "</ProductCode>";


                        XML += "</TABLE>";
                        kk = kk + 1;
                        totalkg = totalkg + Caltotal(Convert.ToDecimal(HdnTotalProductPckKG3.Value), Convert.ToInt32(txteuro2in1.Text));
                    }
                }

                for (int i = 0; i < grdExtreme.Rows.Count; i++)
                {
                    HiddenField HdnProductId4 = grdExtreme.Rows[i].FindControl("HdnProductId4") as HiddenField;
                    HiddenField HdnProductPckId4 = grdExtreme.Rows[i].FindControl("HdnProductPckId4") as HiddenField;
                    HiddenField HdnProductPck4 = grdExtreme.Rows[i].FindControl("HdnProductPck4") as HiddenField;
                    HiddenField HdnPackingNos4 = grdExtreme.Rows[i].FindControl("HdnPackingNos4") as HiddenField;
                    HiddenField HdnPackingType4 = grdExtreme.Rows[i].FindControl("HdnPackingType4") as HiddenField;
                    HiddenField HdnIsScheme4 = grdExtreme.Rows[i].FindControl("HdnIsScheme4") as HiddenField;
                    HiddenField HdnTotalProductPckKG4 = grdExtreme.Rows[i].FindControl("HdnTotalProductPckKG4") as HiddenField;



                    TextBox txtExtreme = grdExtreme.Rows[i].FindControl("txtExtreme") as TextBox;
                    DropDownList dropschemeExtreme = grdExtreme.Rows[i].FindControl("dropschemeExtreme") as DropDownList;
                    //TextBox txtschemeExtreme = grdExtreme.Rows[i].FindControl("txtschemeExtreme") as TextBox;
                    HiddenField hdExtremepcode = gridEUROXTRA.Rows[i].FindControl("hdExtremepcode") as HiddenField;

                    if (Convert.ToString(txtExtreme.Text).Trim() != "")
                    {
                        string pcodeExtreme = GetProductCode(Convert.ToInt32(ViewState["StateID"]), Convert.ToString(dropschemeExtreme.SelectedValue), Convert.ToInt32(HdnProductPckId4.Value));


                        XML += "<TABLE>";
                        XML += "<ProductId>" + HdnProductId4.Value + "</ProductId>";
                        XML += "<ProductPckIds>" + HdnProductPckId4.Value + "</ProductPckIds>";
                        XML += "<ProductPck>" + HdnProductPck4.Value + "</ProductPck>";
                        XML += "<PackingNos>" + HdnPackingNos4.Value + "</PackingNos>";
                        XML += "<PackingType>" + HdnPackingType4.Value + "</PackingType>";
                        XML += "<BoxORNos>Box</BoxORNos>";
                        XML += "<PckTotalKg>" + HdnTotalProductPckKG4.Value + "</PckTotalKg>";
                        XML += "<ProductQty>" + txtExtreme.Text + "</ProductQty>";
                        XML += "<IsScheme>1</IsScheme>";
                        //if (Convert.ToString(txtschemeExtreme.Text) == "")
                        //    XML += "<Scheme>" + Convert.ToString(dropschemeExtreme.SelectedItem) + "</Scheme>";
                        //else
                        //    XML += "<Scheme>" + Convert.ToString(txtschemeExtreme.Text) + "</Scheme>";

                        XML += "<SchemeId>" + Convert.ToString(dropschemeExtreme.SelectedValue) + "</SchemeId>";
                        XML += "<ProductCode>" + pcodeExtreme + "</ProductCode>";


                        XML += "</TABLE>";
                        kk = kk + 1;
                        totalkg = totalkg + Caltotal(Convert.ToDecimal(HdnTotalProductPckKG4.Value), Convert.ToInt32(txtExtreme.Text));
                    }
                }

                for (int i = 0; i < grdEuroUltra.Rows.Count; i++)
                {
                    HiddenField HdnProductId5 = grdEuroUltra.Rows[i].FindControl("HdnProductId5") as HiddenField;
                    HiddenField HdnProductPckId5 = grdEuroUltra.Rows[i].FindControl("HdnProductPckId5") as HiddenField;
                    HiddenField HdnProductPck5 = grdEuroUltra.Rows[i].FindControl("HdnProductPck5") as HiddenField;
                    HiddenField HdnPackingNos5 = grdEuroUltra.Rows[i].FindControl("HdnPackingNos5") as HiddenField;
                    HiddenField HdnPackingType5 = grdEuroUltra.Rows[i].FindControl("HdnPackingType5") as HiddenField;
                    HiddenField HdnIsScheme5 = grdEuroUltra.Rows[i].FindControl("HdnIsScheme5") as HiddenField;
                    HiddenField HdnTotalProductPckKG5 = grdEuroUltra.Rows[i].FindControl("HdnTotalProductPckKG5") as HiddenField;



                    TextBox txtEuroUltra = grdEuroUltra.Rows[i].FindControl("txtEuroUltra") as TextBox;
                    DropDownList dropschemeEuroUltra = grdEuroUltra.Rows[i].FindControl("dropschemeEuroUltra") as DropDownList;
                    //TextBox txtschemeEuroUltra = grdEuroUltra.Rows[i].FindControl("txtschemeEuroUltra") as TextBox;
                    HiddenField hdEuroUltrapcode = gridEUROXTRA.Rows[i].FindControl("hdEuroUltrapcode") as HiddenField;


                    if (Convert.ToString(txtEuroUltra.Text).Trim() != "")
                    {
                        string pcodeEuroUltra = GetProductCode(Convert.ToInt32(ViewState["StateID"]), Convert.ToString(dropschemeEuroUltra.SelectedValue), Convert.ToInt32(HdnProductPckId5.Value));


                        XML += "<TABLE>";
                        XML += "<ProductId>" + HdnProductId5.Value + "</ProductId>";
                        XML += "<ProductPckIds>" + HdnProductPckId5.Value + "</ProductPckIds>";
                        XML += "<ProductPck>" + HdnProductPck5.Value + "</ProductPck>";
                        XML += "<PackingNos>" + HdnPackingNos5.Value + "</PackingNos>";
                        XML += "<PackingType>" + HdnPackingType5.Value + "</PackingType>";
                        XML += "<BoxORNos>Box</BoxORNos>";
                        XML += "<PckTotalKg>" + HdnTotalProductPckKG5.Value + "</PckTotalKg>";
                        XML += "<ProductQty>" + txtEuroUltra.Text + "</ProductQty>";
                        XML += "<IsScheme>1</IsScheme>";

                        //if (Convert.ToString(txtschemeEuroUltra.Text) == "")
                        //    XML += "<Scheme>" + Convert.ToString(dropschemeEuroUltra.SelectedItem) + "</Scheme>";
                        //else
                        //    XML += "<Scheme>" + Convert.ToString(txtschemeEuroUltra.Text) + "</Scheme>";

                        XML += "<SchemeId>" + Convert.ToString(dropschemeEuroUltra.SelectedValue) + "</SchemeId>";
                        XML += "<ProductCode>" + pcodeEuroUltra + "</ProductCode>";

                        XML += "</TABLE>";
                        kk = kk + 1;
                        totalkg = totalkg + Caltotal(Convert.ToDecimal(HdnTotalProductPckKG5.Value), Convert.ToInt32(txtEuroUltra.Text));
                    }
                }

                for (int i = 0; i < GrdPvcGlue.Rows.Count; i++)
                {
                    HiddenField HdnProductId6 = GrdPvcGlue.Rows[i].FindControl("HdnProductId6") as HiddenField;
                    HiddenField HdnProductPckId6 = GrdPvcGlue.Rows[i].FindControl("HdnProductPckId6") as HiddenField;
                    HiddenField HdnProductPck6 = GrdPvcGlue.Rows[i].FindControl("HdnProductPck6") as HiddenField;
                    HiddenField HdnPackingNos6 = GrdPvcGlue.Rows[i].FindControl("HdnPackingNos6") as HiddenField;
                    HiddenField HdnPackingType6 = GrdPvcGlue.Rows[i].FindControl("HdnPackingType6") as HiddenField;
                    HiddenField HdnIsScheme6 = GrdPvcGlue.Rows[i].FindControl("HdnIsScheme6") as HiddenField;
                    HiddenField HdnTotalProductPckKG6 = GrdPvcGlue.Rows[i].FindControl("HdnTotalProductPckKG6") as HiddenField;



                    TextBox txtPVcGlue = GrdPvcGlue.Rows[i].FindControl("txtPVcGlue") as TextBox;
                    DropDownList dropschemePvcGlue = GrdPvcGlue.Rows[i].FindControl("dropschemePvcGlue") as DropDownList;
                    //TextBox txtschemePvcGlue = GrdPvcGlue.Rows[i].FindControl("txtschemePvcGlue") as TextBox;
                    HiddenField hdPVcGluepcode = gridEUROXTRA.Rows[i].FindControl("hdPVcGluepcode") as HiddenField;

                    if (Convert.ToString(txtPVcGlue.Text).Trim() != "")
                    {
                        string pcodePVcGlue = GetProductCode(Convert.ToInt32(ViewState["StateID"]), Convert.ToString(dropschemePvcGlue.SelectedValue), Convert.ToInt32(HdnProductPckId6.Value));


                        XML += "<TABLE>";
                        XML += "<ProductId>" + HdnProductId6.Value + "</ProductId>";
                        XML += "<ProductPckIds>" + HdnProductPckId6.Value + "</ProductPckIds>";
                        XML += "<ProductPck>" + HdnProductPck6.Value + "</ProductPck>";
                        XML += "<PackingNos>" + HdnPackingNos6.Value + "</PackingNos>";
                        XML += "<PackingType>" + HdnPackingType6.Value + "</PackingType>";
                        XML += "<BoxORNos>Box</BoxORNos>";
                        XML += "<PckTotalKg>" + HdnTotalProductPckKG6.Value + "</PckTotalKg>";
                        XML += "<ProductQty>" + txtPVcGlue.Text + "</ProductQty>";
                        XML += "<IsScheme>1</IsScheme>";

                        //if (Convert.ToString(txtschemePvcGlue.Text) == "")
                        //    XML += "<Scheme>" + Convert.ToString(dropschemePvcGlue.SelectedItem) + "</Scheme>";
                        //else
                        //    XML += "<Scheme>" + Convert.ToString(txtschemePvcGlue.Text) + "</Scheme>";

                        XML += "<SchemeId>" + Convert.ToString(dropschemePvcGlue.SelectedValue) + "</SchemeId>";
                        XML += "<ProductCode>" + pcodePVcGlue + "</ProductCode>";

                        XML += "</TABLE>";
                        kk = kk + 1;
                        totalkg = totalkg + Caltotal(Convert.ToDecimal(HdnTotalProductPckKG6.Value), Convert.ToInt32(txtPVcGlue.Text));
                    }
                }

                for (int i = 0; i < grdWoodStrong.Rows.Count; i++)
                {
                    HiddenField HdnProductId7 = grdWoodStrong.Rows[i].FindControl("HdnProductId7") as HiddenField;
                    HiddenField HdnProductPckId7 = grdWoodStrong.Rows[i].FindControl("HdnProductPckId7") as HiddenField;
                    HiddenField HdnProductPck7 = grdWoodStrong.Rows[i].FindControl("HdnProductPck7") as HiddenField;
                    HiddenField HdnPackingNos7 = grdWoodStrong.Rows[i].FindControl("HdnPackingNos7") as HiddenField;
                    HiddenField HdnPackingType7 = grdWoodStrong.Rows[i].FindControl("HdnPackingType7") as HiddenField;
                    HiddenField HdnIsScheme7 = grdWoodStrong.Rows[i].FindControl("HdnIsScheme7") as HiddenField;
                    HiddenField HdnTotalProductPckKG7 = grdWoodStrong.Rows[i].FindControl("HdnTotalProductPckKG7") as HiddenField;



                    TextBox txtWoodStrong = grdWoodStrong.Rows[i].FindControl("txtWoodStrong") as TextBox;
                    DropDownList dropschemeWoodStrong = grdWoodStrong.Rows[i].FindControl("dropschemeWoodStrong") as DropDownList;
                    //TextBox txtschemeWoodStrong = grdWoodStrong.Rows[i].FindControl("txtschemeWoodStrong") as TextBox;
                    HiddenField hdWoodStrongpcode = gridEUROXTRA.Rows[i].FindControl("hdWoodStrongpcode") as HiddenField;


                    if (Convert.ToString(txtWoodStrong.Text).Trim() != "")
                    {

                        string pcodeWoodStrong = GetProductCode(Convert.ToInt32(ViewState["StateID"]), Convert.ToString(dropschemeWoodStrong.SelectedValue), Convert.ToInt32(HdnProductPckId7.Value));


                        XML += "<TABLE>";
                        XML += "<ProductId>" + HdnProductId7.Value + "</ProductId>";
                        XML += "<ProductPckIds>" + HdnProductPckId7.Value + "</ProductPckIds>";
                        XML += "<ProductPck>" + HdnProductPck7.Value + "</ProductPck>";
                        XML += "<PackingNos>" + HdnPackingNos7.Value + "</PackingNos>";
                        XML += "<PackingType>" + HdnPackingType7.Value + "</PackingType>";
                        XML += "<BoxORNos>Box</BoxORNos>";
                        XML += "<PckTotalKg>" + HdnTotalProductPckKG7.Value + "</PckTotalKg>";
                        XML += "<ProductQty>" + txtWoodStrong.Text + "</ProductQty>";
                        XML += "<IsScheme>1</IsScheme>";
                        //if (Convert.ToString(txtschemeWoodStrong.Text) == "")
                        //    XML += "<Scheme>" + Convert.ToString(dropschemeWoodStrong.SelectedItem) + "</Scheme>";
                        //else
                        //    XML += "<Scheme>" + Convert.ToString(txtschemeWoodStrong.Text) + "</Scheme>";

                        XML += "<SchemeId>" + Convert.ToString(dropschemeWoodStrong.SelectedValue) + "</SchemeId>";
                        XML += "<ProductCode>" + pcodeWoodStrong + "</ProductCode>";

                        XML += "</TABLE>";
                        kk = kk + 1;
                        totalkg = totalkg + Caltotal(Convert.ToDecimal(HdnTotalProductPckKG7.Value), Convert.ToInt32(txtWoodStrong.Text));
                    }
                }



                for (int i = 0; i < grdEuroEWR.Rows.Count; i++)
                {
                    HiddenField HdnProductId8 = grdEuroEWR.Rows[i].FindControl("HdnProductId8") as HiddenField;
                    HiddenField HdnProductPckId8 = grdEuroEWR.Rows[i].FindControl("HdnProductPckId8") as HiddenField;
                    HiddenField HdnProductPck8 = grdEuroEWR.Rows[i].FindControl("HdnProductPck8") as HiddenField;
                    HiddenField HdnPackingNos8 = grdEuroEWR.Rows[i].FindControl("HdnPackingNos8") as HiddenField;
                    HiddenField HdnPackingType8 = grdEuroEWR.Rows[i].FindControl("HdnPackingType8") as HiddenField;
                    HiddenField HdnIsScheme8 = grdEuroEWR.Rows[i].FindControl("HdnIsScheme8") as HiddenField;
                    HiddenField HdnTotalProductPckKG8 = grdEuroEWR.Rows[i].FindControl("HdnTotalProductPckKG8") as HiddenField;



                    TextBox txtEuroEWR = grdEuroEWR.Rows[i].FindControl("txtEuroEWR") as TextBox;
                    DropDownList dropschemeEuroEWR = grdEuroEWR.Rows[i].FindControl("dropschemeEuroEWR") as DropDownList;
                    //TextBox txtschemeEuroEWR = grdEuroEWR.Rows[i].FindControl("txtschemeEuroEWR") as TextBox;
                    HiddenField hdEuroEWRpcode = gridEUROXTRA.Rows[i].FindControl("hdEuroEWRpcode") as HiddenField;

                    if (Convert.ToString(txtEuroEWR.Text).Trim() != "")
                    {
                        string pcodeEuroEWR = GetProductCode(Convert.ToInt32(ViewState["StateID"]), Convert.ToString(dropschemeEuroEWR.SelectedValue), Convert.ToInt32(HdnProductPckId8.Value));

                        XML += "<TABLE>";
                        XML += "<ProductId>" + HdnProductId8.Value + "</ProductId>";
                        XML += "<ProductPckIds>" + HdnProductPckId8.Value + "</ProductPckIds>";
                        XML += "<ProductPck>" + HdnProductPck8.Value + "</ProductPck>";
                        XML += "<PackingNos>" + HdnPackingNos8.Value + "</PackingNos>";
                        XML += "<PackingType>" + HdnPackingType8.Value + "</PackingType>";
                        XML += "<BoxORNos>Box</BoxORNos>";
                        XML += "<PckTotalKg>" + HdnTotalProductPckKG8.Value + "</PckTotalKg>";
                        XML += "<ProductQty>" + txtEuroEWR.Text + "</ProductQty>";
                        XML += "<IsScheme>1</IsScheme>";
                        //if (Convert.ToString(txtschemeEuroEWR.Text) == "")
                        //    XML += "<Scheme>" + Convert.ToString(dropschemeEuroEWR.SelectedItem) + "</Scheme>";
                        //else
                        //    XML += "<Scheme>" + Convert.ToString(txtschemeEuroEWR.Text) + "</Scheme>";

                        XML += "<SchemeId>" + Convert.ToString(dropschemeEuroEWR.SelectedValue) + "</SchemeId>";
                        XML += "<ProductCode>" + pcodeEuroEWR + "</ProductCode>";

                        XML += "</TABLE>";
                        kk = kk + 1;
                        totalkg = totalkg + Caltotal(Convert.ToDecimal(HdnTotalProductPckKG8.Value), Convert.ToInt32(txtEuroEWR.Text));
                    }
                }







                if (kk == 0)
                {
                    return "";
                }
                ViewState["TotalKg"] = totalkg;
                return XML += "</OrderProduct>";

                #endregion
            }
            catch (Exception ex)
            {

                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
                return "";
            }
        }

        public string GetProductCode(long StateId, string SchemeID, long ProductPckID)
        {
            BA_tblProductPackingStateScheme ObjBA_tblProductPackingStateScheme = new BA_tblProductPackingStateScheme();
            DataTable dt = new DataTable();
            string productcode = "";
            long SchemeIDINT = 0;
            if (SchemeID == "")
                SchemeIDINT = 0;
            else
                SchemeIDINT = Convert.ToInt32(SchemeID);


            try
            {

                ObjBA_tblProductPackingStateScheme.state_id = StateId;
                ObjBA_tblProductPackingStateScheme.SchemeIdData = SchemeIDINT;
                ObjBA_tblProductPackingStateScheme.ProductPckID = ProductPckID;

                ObjBA_tblProductPackingStateScheme.GetProductPackingStateScheme(ref dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    productcode = Convert.ToString(dt.Rows[0]["SchemeProductCode"]);
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
            return productcode;
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

        protected void ClearData()
        {
            try
            {
                for (int i = 0; i < gridEUROXTRA.Rows.Count; i++)
                {
                    TextBox txtEUROXTRA = gridEUROXTRA.Rows[i].FindControl("txtEUROXTRA") as TextBox;
                    txtEUROXTRA.Text = "";

                    //TextBox txtschemeEUROXTRA = grdWoodStrong.Rows[i].FindControl("txtschemeEUROXTRA") as TextBox;
                    //txtschemeEUROXTRA.Text = "";
                }

                for (int i = 0; i < grdEUROWP.Rows.Count; i++)
                {
                    TextBox txteurowp = grdEUROWP.Rows[i].FindControl("txteurowp") as TextBox;
                    txteurowp.Text = "";

                    //TextBox txtschemeEUROWP = grdWoodStrong.Rows[i].FindControl("txtschemeEUROWP") as TextBox;
                    //txtschemeEUROWP.Text = "";
                }

                for (int i = 0; i < grdeuro2in1.Rows.Count; i++)
                {
                    TextBox txteuro2in1 = grdeuro2in1.Rows[i].FindControl("txteuro2in1") as TextBox;
                    txteuro2in1.Text = "";

                    //TextBox txtschemeeuro2in1 = grdWoodStrong.Rows[i].FindControl("txtschemeeuro2in1") as TextBox;
                    //txtschemeeuro2in1.Text = "";
                }

                for (int i = 0; i < grdExtreme.Rows.Count; i++)
                {
                    TextBox txtExtreme = grdExtreme.Rows[i].FindControl("txtExtreme") as TextBox;
                    txtExtreme.Text = "";

                    //TextBox txtschemeExtreme = grdWoodStrong.Rows[i].FindControl("txtschemeExtreme") as TextBox;
                    //txtschemeExtreme.Text = "";
                }

                for (int i = 0; i < grdEuroUltra.Rows.Count; i++)
                {
                    TextBox txtEuroUltra = grdEuroUltra.Rows[i].FindControl("txtEuroUltra") as TextBox;
                    txtEuroUltra.Text = "";

                    //TextBox txtschemeEuroUltra = grdEuroEWR.Rows[i].FindControl("txtschemeEuroUltra") as TextBox;
                    //txtschemeEuroUltra.Text = "";

                }

                for (int i = 0; i < grdEuroEWR.Rows.Count; i++)
                {
                    TextBox txtEuroEWR = grdEuroEWR.Rows[i].FindControl("txtEuroEWR") as TextBox;
                    txtEuroEWR.Text = "";

                    //TextBox txtschemeEuroEWR = grdEuroEWR.Rows[i].FindControl("txtschemeEuroEWR") as TextBox;
                    //txtschemeEuroEWR.Text = "";
                }


                for (int i = 0; i < GrdPvcGlue.Rows.Count; i++)
                {
                    TextBox txtPVcGlue = GrdPvcGlue.Rows[i].FindControl("txtPVcGlue") as TextBox;
                    txtPVcGlue.Text = "";

                    //TextBox txtschemePvcGlue = grdWoodStrong.Rows[i].FindControl("txtschemePvcGlue") as TextBox;
                    //txtschemePvcGlue.Text = "";
                }

                for (int i = 0; i < grdWoodStrong.Rows.Count; i++)
                {
                    TextBox txtWoodStrong = grdWoodStrong.Rows[i].FindControl("txtWoodStrong") as TextBox;
                    txtWoodStrong.Text = "";

                    //TextBox txtschemeWoodStrong = grdWoodStrong.Rows[i].FindControl("txtschemeWoodStrong") as TextBox;
                    //txtschemeWoodStrong.Text = "";
                }
                ViewState["StateID"] = "0";
                hdOrderId.Value = "";
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
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
                    txtsitedelivery.Text = Convert.ToString(_dt.Rows[0]["SiteDelivery"]);
                    lblTotal.Text = Convert.ToString(_dt.Rows[0]["TotalKgGm"]);
                    hdTotalKgCount.Value = Convert.ToString(_dt.Rows[0]["TotalKgGm"]);
                    hdEditOrderId.Value = Convert.ToString(_dt.Rows[0]["OrderId"]);

                    drpsSalesExe.SelectedValue = Convert.ToString(_dt.Rows[0]["SalesManId"]);

                    txtDealerCode.Text = Convert.ToString(_dt.Rows[0]["DealerCode"]);
                    txtdealernamesearch.Text = Convert.ToString(_dt.Rows[0]["DealerName"]);
                    txtDealerCodeSearch.Text = Convert.ToString(_dt.Rows[0]["DealerCode"]);
                    txtContactName.Text = Convert.ToString(_dt.Rows[0]["ContactName"]);
                    txtDealerName.Text = Convert.ToString(_dt.Rows[0]["DealerName"]);
                    txtAddress.Text = Convert.ToString(_dt.Rows[0]["Address"]);
                    txtArea.Text = Convert.ToString(_dt.Rows[0]["Area"]);
                    txtPhoneNo.Text = Convert.ToString(_dt.Rows[0]["Phone"]);
                    txtGST.Text = Convert.ToString(_dt.Rows[0]["GST"]);

                    drpStateName.SelectedIndex = Convert.ToInt32(_dt.Rows[0]["StateID"]);
                    ViewState["StateID"] = Convert.ToInt32(_dt.Rows[0]["StateID"]);

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
                                DropDownList dropschemeEUROXTRA = gridEUROXTRA.Rows[k].FindControl("dropschemeEUROXTRA") as DropDownList;
                                Dropdownfill(dropschemeEUROXTRA, Convert.ToInt32(HdnProductPckId1.Value), Convert.ToInt32(ViewState["StateID"]));


                                string strProductPckId = HdnProductPckId1.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txtEUROXTRA = gridEUROXTRA.Rows[k].FindControl("txtEUROXTRA") as TextBox;
                                    txtEUROXTRA.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                    dropschemeEUROXTRA.SelectedIndex = Convert.ToInt32(_dtOrder.Rows[i]["SchemeId"]);

                                    HiddenField hdEUROXTRA = gridEUROXTRA.Rows[k].FindControl("hdEUROXTRA") as HiddenField;
                                    hdEUROXTRA.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));

                                    //HiddenField myhidden = gridEUROXTRA.Rows[k].FindControl("myhidden") as HiddenField;
                                    //myhidden.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));


                                    //TextBox txtschemeEUROXTRA = gridEUROXTRA.Rows[k].FindControl("txtschemeEUROXTRA") as TextBox;
                                    //txtschemeEUROXTRA.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);



                                }
                            }

                            for (int k = 0; k < grdEUROWP.Rows.Count; k++)
                            {
                                HiddenField HdnProductPckId2 = grdEUROWP.Rows[k].FindControl("HdnProductPckId2") as HiddenField;
                                DropDownList dropschemeeurowp = grdEUROWP.Rows[k].FindControl("dropschemeeurowp") as DropDownList;
                                Dropdownfill(dropschemeeurowp, Convert.ToInt32(HdnProductPckId2.Value), Convert.ToInt32(ViewState["StateID"]));
                                string strProductPckId = HdnProductPckId2.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txteurowp = grdEUROWP.Rows[k].FindControl("txteurowp") as TextBox;
                                    txteurowp.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));

                                    HiddenField hdeurowp = grdEUROWP.Rows[k].FindControl("hdeurowp") as HiddenField;
                                    hdeurowp.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                    dropschemeeurowp.SelectedIndex = Convert.ToInt32(_dtOrder.Rows[i]["SchemeId"]);

                                    //TextBox txtschemeEUROWP = grdEUROWP.Rows[k].FindControl("txtschemeEUROWP") as TextBox;
                                    //txtschemeEUROWP.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);

                                }
                            }

                            for (int k = 0; k < grdeuro2in1.Rows.Count; k++)
                            {
                                HiddenField HdnProductPckId3 = grdeuro2in1.Rows[k].FindControl("HdnProductPckId3") as HiddenField;
                                DropDownList dropschemeeuro2in1 = grdeuro2in1.Rows[k].FindControl("dropschemeeuro2in1") as DropDownList;
                                Dropdownfill(dropschemeeuro2in1, Convert.ToInt32(HdnProductPckId3.Value), Convert.ToInt32(ViewState["StateID"]));

                                string strProductPckId = HdnProductPckId3.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txteuro2in1 = grdeuro2in1.Rows[k].FindControl("txteuro2in1") as TextBox;
                                    HiddenField hdeuro2in1 = grdeuro2in1.Rows[k].FindControl("hdeuro2in1") as HiddenField;
                                    txteuro2in1.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                    hdeuro2in1.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));


                                    dropschemeeuro2in1.SelectedIndex = Convert.ToInt32(_dtOrder.Rows[i]["SchemeId"]);

                                    //TextBox txtschemeeuro2in1 = grdeuro2in1.Rows[k].FindControl("txtschemeeuro2in1") as TextBox;
                                    //txtschemeeuro2in1.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                }
                            }

                            for (int k = 0; k < grdExtreme.Rows.Count; k++)
                            {
                                HiddenField HdnProductPckId4 = grdExtreme.Rows[k].FindControl("HdnProductPckId4") as HiddenField;
                                DropDownList dropschemeExtreme = grdExtreme.Rows[k].FindControl("dropschemeExtreme") as DropDownList;
                                Dropdownfill(dropschemeExtreme, Convert.ToInt32(HdnProductPckId4.Value), Convert.ToInt32(ViewState["StateID"]));

                                string strProductPckId = HdnProductPckId4.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txtExtreme = grdExtreme.Rows[k].FindControl("txtExtreme") as TextBox;
                                    HiddenField hdExtreme = grdExtreme.Rows[k].FindControl("hdExtreme") as HiddenField;
                                    txtExtreme.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                    hdExtreme.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));

                                    dropschemeExtreme.SelectedIndex = Convert.ToInt32(_dtOrder.Rows[i]["SchemeId"]);

                                    //TextBox txtschemeExtreme = grdExtreme.Rows[k].FindControl("txtschemeExtreme") as TextBox;
                                    //txtschemeExtreme.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                }
                            }

                            for (int k = 0; k < grdEuroUltra.Rows.Count; k++)
                            {
                                HiddenField HdnProductPckId5 = grdEuroUltra.Rows[k].FindControl("HdnProductPckId5") as HiddenField;
                                DropDownList dropschemeEuroUltra = grdEuroUltra.Rows[k].FindControl("dropschemeEuroUltra") as DropDownList;
                                Dropdownfill(dropschemeEuroUltra, Convert.ToInt32(HdnProductPckId5.Value), Convert.ToInt32(ViewState["StateID"]));

                                string strProductPckId = HdnProductPckId5.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txtEuroUltra = grdEuroUltra.Rows[k].FindControl("txtEuroUltra") as TextBox;
                                    txtEuroUltra.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));

                                    HiddenField hdEuroUltra = grdEuroUltra.Rows[k].FindControl("hdEuroUltra") as HiddenField;
                                    hdEuroUltra.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));

                                    dropschemeEuroUltra.SelectedIndex = Convert.ToInt32(_dtOrder.Rows[i]["SchemeId"]);

                                    //TextBox txtschemeEuroUltra = grdEuroUltra.Rows[k].FindControl("txtschemeEuroUltra") as TextBox;
                                    //txtschemeEuroUltra.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                }
                            }

                            for (int k = 0; k < GrdPvcGlue.Rows.Count; k++)
                            {
                                HiddenField HdnProductPckId6 = GrdPvcGlue.Rows[k].FindControl("HdnProductPckId6") as HiddenField;
                                DropDownList dropschemePvcGlue = GrdPvcGlue.Rows[k].FindControl("dropschemePvcGlue") as DropDownList;
                                Dropdownfill(dropschemePvcGlue, Convert.ToInt32(HdnProductPckId6.Value), Convert.ToInt32(ViewState["StateID"]));

                                string strProductPckId = HdnProductPckId6.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txtPVcGlue = GrdPvcGlue.Rows[k].FindControl("txtPVcGlue") as TextBox;
                                    HiddenField hdPVcGlue = GrdPvcGlue.Rows[k].FindControl("hdPVcGlue") as HiddenField;
                                    txtPVcGlue.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                    hdPVcGlue.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));


                                    dropschemePvcGlue.SelectedIndex = Convert.ToInt32(_dtOrder.Rows[i]["SchemeId"]);

                                    //TextBox txtschemePvcGlue = GrdPvcGlue.Rows[k].FindControl("txtschemePvcGlue") as TextBox;
                                    //txtschemePvcGlue.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                }
                            }

                            for (int k = 0; k < grdWoodStrong.Rows.Count; k++)
                            {
                                HiddenField HdnProductPckId7 = grdWoodStrong.Rows[k].FindControl("HdnProductPckId7") as HiddenField;
                                DropDownList dropschemeWoodStrong = grdWoodStrong.Rows[k].FindControl("dropschemeWoodStrong") as DropDownList;
                                Dropdownfill(dropschemeWoodStrong, Convert.ToInt32(HdnProductPckId7.Value), Convert.ToInt32(ViewState["StateID"]));

                                string strProductPckId = HdnProductPckId7.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txtWoodStrong = grdWoodStrong.Rows[k].FindControl("txtWoodStrong") as TextBox;
                                    HiddenField hdWoodStrong = grdWoodStrong.Rows[k].FindControl("hdWoodStrong") as HiddenField;
                                    txtWoodStrong.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                    hdWoodStrong.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));

                                    dropschemeWoodStrong.SelectedIndex = Convert.ToInt32(_dtOrder.Rows[i]["SchemeId"]);

                                    //TextBox txtschemeWoodStrong = grdWoodStrong.Rows[k].FindControl("txtschemeWoodStrong") as TextBox;
                                    //txtschemeWoodStrong.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                }
                            }

                            for (int k = 0; k < grdEuroEWR.Rows.Count; k++)
                            {
                                HiddenField HdnProductPckId8 = grdEuroEWR.Rows[k].FindControl("HdnProductPckId8") as HiddenField;
                                DropDownList dropschemeEuroEWR = grdEuroEWR.Rows[k].FindControl("dropschemeEuroEWR") as DropDownList;
                                Dropdownfill(dropschemeEuroEWR, Convert.ToInt32(HdnProductPckId8.Value), Convert.ToInt32(ViewState["StateID"]));

                                string strProductPckId = HdnProductPckId8.Value;
                                if (Convert.ToString(_dtOrder.Rows[i]["ProductPckId"]) == strProductPckId)
                                {
                                    TextBox txtEuroEWR = grdEuroEWR.Rows[k].FindControl("txtEuroEWR") as TextBox;
                                    HiddenField hdEuroEWR = grdEuroEWR.Rows[k].FindControl("hdEuroEWR") as HiddenField;
                                    txtEuroEWR.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                    hdEuroEWR.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));

                                    dropschemeEuroEWR.SelectedIndex = Convert.ToInt32(_dtOrder.Rows[i]["SchemeId"]);

                                    //TextBox txtschemeEuroEWR = grdEuroEWR.Rows[k].FindControl("txtschemeEuroEWR") as TextBox;
                                    //txtschemeEuroEWR.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
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
            if (Convert.ToString(ViewState["BackButton"]) == "Y")
                Response.Redirect("OrderList.aspx", false);
            else
                Response.Redirect("Dashboard.aspx", false);

        }

        public void Dropdownfill(DropDownList dropschemeEUROXTRA, int ProductPckId, int stateid)
        {
            BA_tblScheme ObjBA_tblScheme = new BA_tblScheme();
            DataTable dt = new DataTable();
            try
            {
                ObjBA_tblScheme.SELECT_ALL_tblSchemeAPI(ProductPckId, stateid, ref dt);
                dropschemeEUROXTRA.DataSource = dt;
                dropschemeEUROXTRA.DataTextField = "SchemeName";
                dropschemeEUROXTRA.DataValueField = "SchemeId";
                dropschemeEUROXTRA.DataBind();


                //dropschemeEUROXTRA.Items.Insert(0, new ListItem("-- Select --", "0"));
            }
            catch (Exception ex)
            {

                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void gridEUROXTRA_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList dropschemeEUROXTRA = e.Row.FindControl("dropschemeEUROXTRA") as DropDownList;
                HiddenField HdnProductPckId1 = e.Row.FindControl("HdnProductPckId1") as HiddenField;

                Dropdownfill(dropschemeEUROXTRA, Convert.ToInt32(HdnProductPckId1.Value), Convert.ToInt32(ViewState["StateID"]));

                HiddenField HdnProductPck1 = e.Row.FindControl("HdnProductPck1") as HiddenField;
                Label lblpkgboxnoEUROXTRA = e.Row.FindControl("lblpkgboxnoEUROXTRA") as Label;

                HiddenField HdnPackingType1 = e.Row.FindControl("HdnPackingType1") as HiddenField;
                if (Convert.ToString(HdnPackingType1.Value) == "kg")
                {
                    if (Convert.ToDecimal(HdnProductPck1.Value) == 20)
                    {
                        lblpkgboxnoEUROXTRA.Text = "BKT";
                    }
                    if (Convert.ToDecimal(HdnProductPck1.Value) > 20)
                    {
                        lblpkgboxnoEUROXTRA.Text = "CRB";
                    }
                }
            }
        }

        protected void grdEUROWP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme2") as HiddenField;

                DropDownList dropschemeEUROWP = e.Row.FindControl("dropschemeEUROWP") as DropDownList;
                HiddenField HdnProductPckId2 = e.Row.FindControl("HdnProductPckId2") as HiddenField;

                Dropdownfill(dropschemeEUROWP, Convert.ToInt32(HdnProductPckId2.Value), Convert.ToInt32(ViewState["StateID"]));

                HiddenField HdnProductPck2 = e.Row.FindControl("HdnProductPck2") as HiddenField;
                Label lblpkgboxnoeurowp = e.Row.FindControl("lblpkgboxnoeurowp") as Label;
                HiddenField HdnPackingType2 = e.Row.FindControl("HdnPackingType2") as HiddenField;
                if (Convert.ToString(HdnPackingType2.Value) == "kg")
                {
                    if (Convert.ToDecimal(HdnProductPck2.Value) == 20)
                    {
                        lblpkgboxnoeurowp.Text = "BKT";
                    }
                    if (Convert.ToDecimal(HdnProductPck2.Value) > 20)
                    {
                        lblpkgboxnoeurowp.Text = "CRB";
                    }
                }
            }
        }

        protected void grdeuro2in1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList dropschemeeuro2in1 = e.Row.FindControl("dropschemeeuro2in1") as DropDownList;
                HiddenField HdnProductPckId3 = e.Row.FindControl("HdnProductPckId3") as HiddenField;

                Dropdownfill(dropschemeeuro2in1, Convert.ToInt32(HdnProductPckId3.Value), Convert.ToInt32(ViewState["StateID"]));

                HiddenField HdnProductPck3 = e.Row.FindControl("HdnProductPck3") as HiddenField;
                Label lblpkgboxnoeuro2in1 = e.Row.FindControl("lblpkgboxnoeuro2in1") as Label;
                HiddenField HdnPackingType3 = e.Row.FindControl("HdnPackingType3") as HiddenField;
                if (Convert.ToString(HdnPackingType3.Value) == "kg")
                {
                    if (Convert.ToDecimal(HdnProductPck3.Value) == 20)
                    {
                        lblpkgboxnoeuro2in1.Text = "BKT";
                    }
                    if (Convert.ToDecimal(HdnProductPck3.Value) > 20)
                    {
                        lblpkgboxnoeuro2in1.Text = "CRB";
                    }
                }
            }

        }

        protected void grdExtreme_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme4") as HiddenField;

                DropDownList dropschemeExtreme = e.Row.FindControl("dropschemeExtreme") as DropDownList;
                HiddenField HdnProductPckId4 = e.Row.FindControl("HdnProductPckId4") as HiddenField;

                Dropdownfill(dropschemeExtreme, Convert.ToInt32(HdnProductPckId4.Value), Convert.ToInt32(ViewState["StateID"]));

                HiddenField HdnProductPck4 = e.Row.FindControl("HdnProductPck4") as HiddenField;
                Label lblpkgboxnoExtreme = e.Row.FindControl("lblpkgboxnoExtreme") as Label;
                HiddenField HdnPackingType4 = e.Row.FindControl("HdnPackingType4") as HiddenField;
                if (Convert.ToString(HdnPackingType4.Value) == "kg")
                {
                    if (Convert.ToDecimal(HdnProductPck4.Value) == 20)
                    {
                        lblpkgboxnoExtreme.Text = "BKT";
                    }
                    if (Convert.ToDecimal(HdnProductPck4.Value) > 20)
                    {
                        lblpkgboxnoExtreme.Text = "CRB";
                    }
                }
            }

        }

        protected void grdEuroUltra_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme5") as HiddenField;

                DropDownList dropschemeEuroUltra = e.Row.FindControl("dropschemeEuroUltra") as DropDownList;
                HiddenField HdnProductPckId5 = e.Row.FindControl("HdnProductPckId5") as HiddenField;

                Dropdownfill(dropschemeEuroUltra, Convert.ToInt32(HdnProductPckId5.Value), Convert.ToInt32(ViewState["StateID"]));

                HiddenField HdnProductPck5 = e.Row.FindControl("HdnProductPck5") as HiddenField;
                Label llblpkgboxnoEuroUltra = e.Row.FindControl("llblpkgboxnoEuroUltra") as Label;
                HiddenField HdnPackingType5 = e.Row.FindControl("HdnPackingType5") as HiddenField;
                if (Convert.ToString(HdnPackingType5.Value) == "kg")
                {

                    if (Convert.ToDecimal(HdnProductPck5.Value) == 20)
                    {
                        llblpkgboxnoEuroUltra.Text = "BKT";
                    }
                    if (Convert.ToDecimal(HdnProductPck5.Value) > 20)
                    {
                        llblpkgboxnoEuroUltra.Text = "CRB";
                    }
                }
            }
        }

        protected void GrdPvcGlue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme6") as HiddenField;

                DropDownList dropschemePvcGlue = e.Row.FindControl("dropschemePvcGlue") as DropDownList;
                HiddenField HdnProductPckId6 = e.Row.FindControl("HdnProductPckId6") as HiddenField;

                Dropdownfill(dropschemePvcGlue, Convert.ToInt32(HdnProductPckId6.Value), Convert.ToInt32(ViewState["StateID"]));

                HiddenField HdnProductPck6 = e.Row.FindControl("HdnProductPck6") as HiddenField;
                Label lblpkgboxnoPVcGlue = e.Row.FindControl("lblpkgboxnoPVcGlue") as Label;
                HiddenField HdnPackingType6 = e.Row.FindControl("HdnPackingType6") as HiddenField;
                if (Convert.ToString(HdnPackingType6.Value) == "kg")
                {

                    if (Convert.ToDecimal(HdnProductPck6.Value) == 20)
                    {
                        lblpkgboxnoPVcGlue.Text = "BKT";
                    }
                    if (Convert.ToDecimal(HdnProductPck6.Value) > 20)
                    {
                        lblpkgboxnoPVcGlue.Text = "CRB";
                    }
                }
            }
        }

        protected void grdWoodStrong_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme7") as HiddenField;

                DropDownList dropschemeWoodStrong = e.Row.FindControl("dropschemeWoodStrong") as DropDownList;
                HiddenField HdnProductPckId7 = e.Row.FindControl("HdnProductPckId7") as HiddenField;

                    Dropdownfill(dropschemeWoodStrong, Convert.ToInt32(HdnProductPckId7.Value), Convert.ToInt32(ViewState["StateID"]));

                HiddenField HdnProductPck7 = e.Row.FindControl("HdnProductPck7") as HiddenField;
                Label lblpkgboxnoWoodStrong = e.Row.FindControl("lblpkgboxnoWoodStrong") as Label;
                HiddenField HdnPackingType7 = e.Row.FindControl("HdnPackingType7") as HiddenField;
                if (Convert.ToString(HdnPackingType7.Value) == "kg")
                {
                    if (Convert.ToDecimal(HdnProductPck7.Value) == 20)
                    {
                        lblpkgboxnoWoodStrong.Text = "BKT";
                    }
                    if (Convert.ToDecimal(HdnProductPck7.Value) > 20)
                    {
                        lblpkgboxnoWoodStrong.Text = "CRB";
                    }
                }
            }
        }

        protected void grdEuroEWR_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme8") as HiddenField;

                DropDownList dropschemeEuroEWR = e.Row.FindControl("dropschemeEuroEWR") as DropDownList;
                HiddenField HdnProductPckId8 = e.Row.FindControl("HdnProductPckId8") as HiddenField;

                Dropdownfill(dropschemeEuroEWR, Convert.ToInt32(HdnProductPckId8.Value), Convert.ToInt32(ViewState["StateID"]));

                HiddenField HdnProductPck1 = e.Row.FindControl("HdnProductPck8") as HiddenField;
                Label lblpkgboxnoEUROEWR = e.Row.FindControl("llblpkgboxnoEuroEWR") as Label;

                HiddenField HdnPackingType1 = e.Row.FindControl("HdnPackingType8") as HiddenField;
                if (Convert.ToString(HdnPackingType1.Value) == "kg")
                {
                    if (Convert.ToDecimal(HdnProductPck1.Value) == 20)
                    {
                        lblpkgboxnoEUROEWR.Text = "BKT";
                    }
                    if (Convert.ToDecimal(HdnProductPck1.Value) > 20)
                    {
                        lblpkgboxnoEUROEWR.Text = "CRB";
                    }
                }
            }
        }


        //protected void dropschemeEUROXTRA_OnSelectedIndexChanged(object sender, EventArgs e)
        //{

        //    var row = (sender as DropDownList).NamingContainer as GridViewRow; //instead of Gridview1.SelectedRow;
        //    HiddenField HdnProductPckId1 = row.FindControl("HdnProductPckId1") as HiddenField;
        //    DropDownList dropschemeEUROXTRA = row.FindControl("dropschemeEUROXTRA") as DropDownList;
        //    GetProductCode(Convert.ToInt32(ViewState["StateID"]), dropschemeEUROXTRA.SelectedIndex, Convert.ToInt32(HdnProductPckId1.Value));


        //}
    }
}