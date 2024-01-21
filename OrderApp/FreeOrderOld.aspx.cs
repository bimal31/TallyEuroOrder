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
    public partial class FreeOrderOld : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string strType = "";
                Int32 OrderId = 0;
                if (!Page.IsPostBack)
                {
                    txtTotalKgsF.Attributes.Add("readonly", "readonly");
                    txtFreetotalkg.Attributes.Add("readonly", "readonly");
                    txtFreeKg.Attributes.Add("readonly", "readonly");
                    drpsSalesExe.Enabled = false;
                    txtDealerCodeSearch.Attributes.Add("readonly", "readonly");
                    txtOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                    txtFreeKg.Text = "0";
                    BindSaleuser();
                    // BindProductGrid();
                    if (Request.QueryString["q"] != null)
                    {

                        string strKey = Convert.ToString(Request.QueryString["q"]);
                        Common cmn = new Common();
                        strKey = cmn.Decrypt(strKey);
                        OrderId = Convert.ToInt32(strKey);

                        if (Request.QueryString["BackButton"] != null && Request.QueryString["BackButton"] == "N")
                            ViewState["BackButton"] = "N";
                        else
                            ViewState["BackButton"] = "Y";
                        if (Request.QueryString["View"] != null && Request.QueryString["View"] == "Y")
                        {
                            lblheading.Text = CommMessage.viewWithBillFreeScheme;
                            btnSubmitOrder.Attributes.Add("style", "display:none");
                            //btnClear.Attributes.Add("style", "display:none");
                        }
                        else if (Request.QueryString["View"] == "N" && Request.QueryString["Action"] == "A")
                        {
                            lblheading.Text = CommMessage.addWithBillFreeScheme;
                            ViewState["Operation"] = "A";
                        }
                        else
                        {
                            lblheading.Text = CommMessage.EditWithBillFreeScheme;
                            ViewState["Operation"] = "U";
                        }

                        GetOrderDetails(OrderId, strType);
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

                //DataTable dt8 = new DataTable();
                //Objtbl.ProductID = "8";
                //Objtbl.GET_RECORDS_FROM_tblProductById(ref dt8);
                //grdEuroEWR.DataSource = dt8;
                //grdEuroEWR.DataBind();

            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }
        protected void btnSubmitOrder_Click(object sender, EventArgs e)
        {
            try
            {
                int ReturnId = 0;
                string XMLFreeData = "";
                BA_tblOrder ObjOrder = new BA_tblOrder();
                if (txtFromScheme.Text == "")
                {
                    lblErrorMessage.Text = CommMessage.Enterfromscheme;
                }
                else if (txtToScheme.Text == "")
                {
                    lblErrorMessage.Text = CommMessage.Entertoscheme;
                }
                else if (Convert.ToDecimal(txtFreetotalkg.Text) != Convert.ToDecimal(txtFreeKg.Text))
                {
                    lblErrorMessage.Text = CommMessage.freetotalkgnotmatch;
                }
                //else if (Convert.ToDecimal(txtTotalKgsF.Text) != Convert.ToDecimal(hdTotalKgCount.Value))
                //{
                //    lblErrorMessage.Text = CommMessage.totalkgandtotalvalue;
                //}
                else
                {
                    ObjOrder.OrderType = CommMessage.OrderType_withbillFreeScheme;
                    ObjOrder.DealerId = hdDelaerId.Value;

                    ObjOrder.FreeSchemeFrom = Convert.ToDecimal(txtFromScheme.Text);
                    ObjOrder.FreeSchemeTO = Convert.ToDecimal(txtToScheme.Text);
                    ObjOrder.TotalKgGm = Convert.ToDecimal(txtFreetotalkg.Text);

                    ObjOrder.Transport = txttransport.Text;
                    ObjOrder.Other = txtOther.Text;
                    ObjOrder.POP = txtPOP.Text;
                    ObjOrder.SiteDelivery = txtsitedelivery.Text;

                    ObjOrder.OrderStatus = drpOrderStatus.Text;

                    ObjOrder.SalesId = Convert.ToInt32(drpsSalesExe.SelectedValue);


                    ObjOrder.ParentOrderId = hdEditOrderId.Value;


                    ObjOrder.CreateBy = Convert.ToInt32(Session["UserId"]);



                    try
                    {
                        XMLFreeData = xmlCreate();
                    }
                    catch (Exception ex)
                    {
                        BA_ErrorLog ObjError = new BA_ErrorLog();
                        ObjError.INSERT_ErrorLog(ex);
                    }
                    if (XMLFreeData != "")
                    {
                        ObjOrder.xmlFreeProd = XMLFreeData;
                        ObjOrder.TotalFreeKgGm = Convert.ToDecimal(txtFreeKg.Text);

                        if (hdFreeOrderId.Value != "")
                        {
                            //   ObjOrder.FreeOrderId = hdFreeOrderId.Value;
                            ObjOrder.OrderID = hdFreeOrderId.Value;
                            ObjOrder.UpdateBy = Convert.ToInt32(Session["UserId"]);
                            ObjOrder.UPDATE_tblOrderFree(ref ReturnId);
                        }
                        else
                        {
                            ObjOrder.INSERT_tblOrderFree(ref ReturnId);
                        }

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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearData();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void ClearData()
        {
            try
            {
                for (int i = 0; i < gridEUROXTRA.Rows.Count; i++)
                {
                    TextBox txtEUROXTRA = gridEUROXTRA.Rows[i].FindControl("txtFreeEUROXTRA") as TextBox;
                    txtEUROXTRA.Text = "";
                }

                for (int i = 0; i < grdEUROWP.Rows.Count; i++)
                {
                    TextBox txteurowp = grdEUROWP.Rows[i].FindControl("txtFreeeurowp") as TextBox;
                    txteurowp.Text = "";
                }

                for (int i = 0; i < grdeuro2in1.Rows.Count; i++)
                {
                    TextBox txteuro2in1 = grdeuro2in1.Rows[i].FindControl("txtFreeeuro2in1") as TextBox;
                    txteuro2in1.Text = "";
                }

                for (int i = 0; i < grdExtreme.Rows.Count; i++)
                {
                    TextBox txtExtreme = grdExtreme.Rows[i].FindControl("txtFreeExtreme") as TextBox;
                    txtExtreme.Text = "";
                }

                for (int i = 0; i < grdEuroUltra.Rows.Count; i++)
                {
                    TextBox txtEuroUltra = grdEuroUltra.Rows[i].FindControl("txtFreeEuroUltra") as TextBox;
                    txtEuroUltra.Text = "";
                }

                for (int i = 0; i < GrdPvcGlue.Rows.Count; i++)
                {
                    TextBox txtPVcGlue = GrdPvcGlue.Rows[i].FindControl("txtFreePVcGlue") as TextBox;
                    txtPVcGlue.Text = "";
                }

                for (int i = 0; i < grdWoodStrong.Rows.Count; i++)
                {
                    TextBox txtWoodStrong = grdWoodStrong.Rows[i].FindControl("txtFreeWoodStrongs") as TextBox;
                    txtWoodStrong.Text = "";
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
                    //TextBox txtschemeEUROXTRA = gridEUROXTRA.Rows[i].FindControl("txtschemeEUROXTRA") as TextBox;
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
                        XML += "<BoxORNos>No</BoxORNos>";
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
                        totalkg = totalkg + CalFreetotal(Convert.ToDecimal(HdnProductPck1.Value), Convert.ToString(HdnPackingType1.Value), Convert.ToInt32(txtEUROXTRA.Text));
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
                        XML += "<BoxORNos>No</BoxORNos>";
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
                        totalkg = totalkg + CalFreetotal(Convert.ToDecimal(HdnProductPck2.Value), Convert.ToString(HdnPackingType2.Value), Convert.ToInt32(txteurowp.Text));
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
                        XML += "<BoxORNos>No</BoxORNos>";
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
                        totalkg = totalkg + CalFreetotal(Convert.ToDecimal(HdnProductPck3.Value), Convert.ToString(HdnPackingType3.Value), Convert.ToInt32(txteuro2in1.Text));
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
                        XML += "<BoxORNos>No</BoxORNos>";
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
                        //totalkg = totalkg + CalFreetotal(Convert.ToDecimal(HdnTotalProductPckKG4.Value), Convert.ToInt32(txtExtreme.Text));
                        totalkg = totalkg + CalFreetotal(Convert.ToDecimal(HdnProductPck4.Value), Convert.ToString(HdnPackingType4.Value), Convert.ToInt32(txtExtreme.Text));
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
                        XML += "<BoxORNos>No</BoxORNos>";
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
                        //totalkg = totalkg + CalFreetotal(Convert.ToDecimal(HdnTotalProductPckKG5.Value), Convert.ToInt32(txtEuroUltra.Text));
                        totalkg = totalkg + CalFreetotal(Convert.ToDecimal(HdnProductPck5.Value), Convert.ToString(HdnPackingType5.Value), Convert.ToInt32(txtEuroUltra.Text));
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
                        XML += "<BoxORNos>No</BoxORNos>";
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
                        //totalkg = totalkg + CalFreetotal(Convert.ToDecimal(HdnTotalProductPckKG6.Value), Convert.ToInt32(txtPVcGlue.Text));
                        totalkg = totalkg + CalFreetotal(Convert.ToDecimal(HdnProductPck6.Value), Convert.ToString(HdnPackingType6.Value), Convert.ToInt32(txtPVcGlue.Text));
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
                        XML += "<BoxORNos>No</BoxORNos>";
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
                        //totalkg = totalkg + CalFreetotal(Convert.ToDecimal(HdnTotalProductPckKG7.Value), Convert.ToInt32(txtWoodStrong.Text));
                        totalkg = totalkg + CalFreetotal(Convert.ToDecimal(HdnProductPck7.Value), Convert.ToString(HdnPackingType7.Value), Convert.ToInt32(txtWoodStrong.Text));
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

        public decimal CalFreetotal(decimal totalkg, string PackingType, int ProductQty)
        {
            decimal totalkgcount = 0;
            try
            {
                totalkgcount = (totalkg * ProductQty);

                if (PackingType.ToLower() == "gram" || PackingType.ToLower() == "gm")
                {
                    totalkgcount = totalkgcount / 1000;
                }
            }
            catch (Exception)
            {
            }
            return totalkgcount;

        }

        protected void GetOrderDetails(Int32 OrderId, string ordertype)
        {
            try
            {
                BA_tblOrder ObjOrder = new BA_tblOrder();
                DataTable _dt = new DataTable();
                ObjOrder.OrderID = Convert.ToString(OrderId);
                ObjOrder.GET_RECORDS_FROM_tblOrderByOrderId(ref _dt);

                if (_dt != null)
                {
                    drpsSalesExe.SelectedValue = Convert.ToString(_dt.Rows[0]["SalesManId"]);
                    txtDealerCodeSearch.Text = Convert.ToString(_dt.Rows[0]["DealerCode"]);
                    txtdealernamesearch.Text = Convert.ToString(_dt.Rows[0]["DealerName"]);
                    ViewState["StateID"] = Convert.ToInt32(_dt.Rows[0]["StateID"]);
                    BindProductGrid();
                    if (Convert.ToString(_dt.Rows[0]["DealerCode"]) == "")
                    {
                        txtDealerCodeSearch.Text = Convert.ToString(_dt.Rows[0]["DealerName"]);
                    }

                    hdDelaerId.Value = Convert.ToString(_dt.Rows[0]["DealerId"]);

                    drpOrderStatus.Text = Convert.ToString(_dt.Rows[0]["OrderStatus"]);
                    txtFromScheme.Text = Convert.ToString(_dt.Rows[0]["FreeSchemeFrom"]);
                    txtToScheme.Text = Convert.ToString(_dt.Rows[0]["FreeSchemeTO"]);

                    if (Convert.ToString(ViewState["Operation"]) == "A")
                    {
                        hdTotalKgCount.Value = Convert.ToString(_dt.Rows[0]["TotalKgGm"]);
                        txtTotalKgsF.Text = hdTotalKgCount.Value;
                    }
                    else
                    {
                        hdTotalKgCount.Value = Convert.ToString(_dt.Rows[0]["TotalKgGm"]);
                        txtFreetotalkg.Text = Convert.ToString(_dt.Rows[0]["TotalKgGm"]);

                        txtTotalKgsF.Text = Convert.ToString(_dt.Rows[0]["PurchaseKgs"]);


                    }
                    hdEditOrderId.Value = Convert.ToString(_dt.Rows[0]["OrderId"]);

                    //
                    if (Convert.ToString(_dt.Rows[0]["OrderType"]).ToLower() == CommMessage.OrderType_withbillFreeScheme.ToLower())
                    {

                        txtOther.Text = Convert.ToString(_dt.Rows[0]["Other"]);
                        txtPOP.Text = Convert.ToString(_dt.Rows[0]["POP"]);
                        txtsitedelivery.Text = Convert.ToString(_dt.Rows[0]["SiteDelivery"]);
                        txttransport.Text = Convert.ToString(_dt.Rows[0]["Transport"]);

                        txtOrderDate.Text = Convert.ToDateTime(Convert.ToString(_dt.Rows[0]["OrderDate"])).ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                        txtFreetotalkg.Text = Convert.ToString(_dt.Rows[0]["TotalKgGm"]);
                        txtFreeKg.Text = Convert.ToString(_dt.Rows[0]["TotalKgGm"]);


                        hdTotalFreeKgCount.Value = Convert.ToString(_dt.Rows[0]["TotalKgGm"]);

                        hdFreeOrderId.Value = Convert.ToString(_dt.Rows[0]["OrderID"]);
                        hdEditOrderId.Value = Convert.ToString(_dt.Rows[0]["ParentOrderId"]);


                        //Decimal Freetotalkg = (Convert.ToDecimal(txtToScheme.Text) * Convert.ToDecimal(txtTotalKgsF.Text)) / Convert.ToDecimal(txtFromScheme.Text);
                        //Freetotalkg = Math.Ceiling(Freetotalkg);
                        //txtFreetotalkg.Text = Convert.ToString(Freetotalkg);
                    }



                    if (Convert.ToString(_dt.Rows[0]["OrderType"]).ToLower() == CommMessage.OrderType_withbillFreeScheme.ToLower())
                    {

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
                                        //TextBox txtschemeEUROXTRA = gridEUROXTRA.Rows[k].FindControl("txtschemeEUROXTRA") as TextBox;
                                        HiddenField hdEUROXTRA = gridEUROXTRA.Rows[k].FindControl("hdEUROXTRA") as HiddenField;
                                        txtEUROXTRA.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                        hdEUROXTRA.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                        //txtschemeEUROXTRA.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                        dropschemeEUROXTRA.SelectedIndex = Convert.ToInt32(_dtOrder.Rows[i]["SchemeId"]);
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
                                        //TextBox txtschemeEUROWP = grdEUROWP.Rows[k].FindControl("txtschemeEUROWP") as TextBox;
                                        HiddenField hdeurowp = grdEUROWP.Rows[k].FindControl("hdeurowp") as HiddenField;
                                        txteurowp.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                        hdeurowp.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                        //txtschemeEUROWP.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                        dropschemeeurowp.SelectedIndex = Convert.ToInt32(_dtOrder.Rows[i]["SchemeId"]);
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
                                        //TextBox txtschemeeuro2in1 = grdeuro2in1.Rows[k].FindControl("txtschemeeuro2in1") as TextBox;
                                        HiddenField hdeuro2in1 = grdeuro2in1.Rows[k].FindControl("hdeuro2in1") as HiddenField;
                                        txteuro2in1.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                        hdeuro2in1.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                        //txtschemeeuro2in1.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                        dropschemeeuro2in1.SelectedIndex = Convert.ToInt32(_dtOrder.Rows[i]["SchemeId"]);

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
                                        //TextBox txtschemeExtreme = grdExtreme.Rows[k].FindControl("txtschemeExtreme") as TextBox;
                                        HiddenField hdExtreme = grdExtreme.Rows[k].FindControl("hdExtreme") as HiddenField;
                                        txtExtreme.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                        hdExtreme.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                        //txtschemeExtreme.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);

                                        dropschemeExtreme.SelectedIndex = Convert.ToInt32(_dtOrder.Rows[i]["SchemeId"]);

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
                                        //TextBox txtschemeEuroUltra = grdEuroUltra.Rows[k].FindControl("txtschemeEuroUltra") as TextBox;
                                        HiddenField hdEuroUltra = grdEuroUltra.Rows[k].FindControl("hdEuroUltra") as HiddenField;
                                        txtEuroUltra.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                        hdEuroUltra.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                        //txtschemeEuroUltra.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                        dropschemeEuroUltra.SelectedIndex = Convert.ToInt32(_dtOrder.Rows[i]["SchemeId"]);

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
                                        //TextBox txtschemePvcGlue = GrdPvcGlue.Rows[k].FindControl("txtschemePvcGlue") as TextBox;
                                        HiddenField hdPVcGlue = GrdPvcGlue.Rows[k].FindControl("hdPVcGlue") as HiddenField;
                                        txtPVcGlue.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                        hdPVcGlue.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                        //txtschemePvcGlue.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);

                                        dropschemePvcGlue.SelectedIndex = Convert.ToInt32(_dtOrder.Rows[i]["SchemeId"]);

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
                                        //TextBox txtschemeWoodStrong = grdWoodStrong.Rows[k].FindControl("txtschemeWoodStrong") as TextBox;
                                        HiddenField hdWoodStrong = grdWoodStrong.Rows[k].FindControl("hdWoodStrong") as HiddenField;
                                        txtWoodStrong.Text = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                        hdWoodStrong.Value = Convert.ToString(Convert.ToInt32(_dtOrder.Rows[i]["ProductQty"]));
                                        //txtschemeWoodStrong.Text = Convert.ToString(_dtOrder.Rows[i]["Scheme"]);
                                        dropschemeWoodStrong.SelectedIndex = Convert.ToInt32(_dtOrder.Rows[i]["SchemeId"]);

                                    }
                                }
                                //}
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
                HiddenField HdnProductPckId1 = e.Row.FindControl("HdnProductPckId1") as HiddenField;

                DropDownList dropschemeEUROXTRA = e.Row.FindControl("dropschemeEUROXTRA") as DropDownList;

                HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme1") as HiddenField;

                Dropdownfill(dropschemeEUROXTRA, Convert.ToInt32(HdnProductPckId1.Value), Convert.ToInt32(ViewState["StateID"]));

                //if (Convert.ToBoolean(HdnIsScheme1.Value))
                //{
                //    Label lblschEUROXTRA = e.Row.FindControl("lblschEUROXTRA") as Label;
                //    lblschEUROXTRA.Visible = true;

                //    TextBox txtschemeEUROXTRA = e.Row.FindControl("txtschemeEUROXTRA") as TextBox;
                //    txtschemeEUROXTRA.Visible = true;

                //}

            }
        }

        protected void grdEUROWP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField HdnProductPckId2 = e.Row.FindControl("HdnProductPckId2") as HiddenField;

                DropDownList dropschemeEUROWP = e.Row.FindControl("dropschemeEUROWP") as DropDownList;

                HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme2") as HiddenField;
                Dropdownfill(dropschemeEUROWP, Convert.ToInt32(HdnProductPckId2.Value), Convert.ToInt32(ViewState["StateID"]));

                //if (Convert.ToBoolean(HdnIsScheme1.Value))
                //{
                //    Label lblschEUROWP = e.Row.FindControl("lblschEUROWP") as Label;
                //    lblschEUROWP.Visible = true;

                //    TextBox txtschemeEUROWP = e.Row.FindControl("txtschemeEUROWP") as TextBox;
                //    txtschemeEUROWP.Visible = true;

                //}

            }
        }

        protected void grdeuro2in1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList dropschemeeuro2in1 = e.Row.FindControl("dropschemeeuro2in1") as DropDownList;
                HiddenField HdnProductPckId3 = e.Row.FindControl("HdnProductPckId3") as HiddenField;

                HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme3") as HiddenField;
                Dropdownfill(dropschemeeuro2in1, Convert.ToInt32(HdnProductPckId3.Value), Convert.ToInt32(ViewState["StateID"]));

                //if (Convert.ToBoolean(HdnIsScheme1.Value))
                //{
                //    Label lblscheuro2in1 = e.Row.FindControl("lblscheuro2in1") as Label;
                //    lblscheuro2in1.Visible = true;

                //    TextBox txtschemeeuro2in1 = e.Row.FindControl("txtschemeeuro2in1") as TextBox;
                //    txtschemeeuro2in1.Visible = true;

                //}


            }

        }

        protected void grdExtreme_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList dropschemeExtreme = e.Row.FindControl("dropschemeExtreme") as DropDownList;

                HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme4") as HiddenField;
                HiddenField HdnProductPckId4 = e.Row.FindControl("HdnProductPckId4") as HiddenField;

                Dropdownfill(dropschemeExtreme, Convert.ToInt32(HdnProductPckId4.Value), Convert.ToInt32(ViewState["StateID"]));

                //if (Convert.ToBoolean(HdnIsScheme1.Value))
                //{
                //    Label lblschExtreme = e.Row.FindControl("lblschExtreme") as Label;
                //    lblschExtreme.Visible = true;

                //    TextBox txtschemeExtreme = e.Row.FindControl("txtschemeExtreme") as TextBox;
                //    txtschemeExtreme.Visible = true;

                //}

            }

        }

        protected void grdEuroUltra_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList dropschemeEuroUltra = e.Row.FindControl("dropschemeEuroUltra") as DropDownList;

                HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme5") as HiddenField;
                HiddenField HdnProductPckId5 = e.Row.FindControl("HdnProductPckId5") as HiddenField;


                Dropdownfill(dropschemeEuroUltra, Convert.ToInt32(HdnProductPckId5.Value), Convert.ToInt32(ViewState["StateID"]));

                //if (Convert.ToBoolean(HdnIsScheme1.Value))
                //{
                //    Label lblschEuroUltra = e.Row.FindControl("lblschEuroUltra") as Label;
                //    lblschEuroUltra.Visible = true;

                //    TextBox txtschemeEuroUltra = e.Row.FindControl("txtschemeEuroUltra") as TextBox;
                //    txtschemeEuroUltra.Visible = true;

                //}

            }
        }

        protected void GrdPvcGlue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList dropschemePvcGlue = e.Row.FindControl("dropschemePvcGlue") as DropDownList;
                HiddenField HdnProductPckId6 = e.Row.FindControl("HdnProductPckId6") as HiddenField;

                HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme6") as HiddenField;
                Dropdownfill(dropschemePvcGlue, Convert.ToInt32(HdnProductPckId6.Value), Convert.ToInt32(ViewState["StateID"]));

                //if (Convert.ToBoolean(HdnIsScheme1.Value))
                //{
                //    Label lblschPvcGlue = e.Row.FindControl("lblschPvcGlue") as Label;
                //    lblschPvcGlue.Visible = true;

                //    TextBox txtschemePvcGlue = e.Row.FindControl("txtschemePvcGlue") as TextBox;
                //    txtschemePvcGlue.Visible = true;

                //}

            }
        }

        protected void grdWoodStrong_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList dropschemeWoodStrong = e.Row.FindControl("dropschemeWoodStrong") as DropDownList;
                HiddenField HdnProductPckId7 = e.Row.FindControl("HdnProductPckId7") as HiddenField;

                HiddenField HdnIsScheme1 = e.Row.FindControl("HdnIsScheme7") as HiddenField;

                Dropdownfill(dropschemeWoodStrong, Convert.ToInt32(HdnProductPckId7.Value), Convert.ToInt32(ViewState["StateID"]));

                //if (Convert.ToBoolean(HdnIsScheme1.Value))
                //{
                //    Label lblschWoodStrong = e.Row.FindControl("lblschWoodStrong") as Label;
                //    lblschWoodStrong.Visible = true;

                //    TextBox txtschemeWoodStrong = e.Row.FindControl("txtschemeWoodStrong") as TextBox;
                //    txtschemeWoodStrong.Visible = true;

                //}

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

    }
}