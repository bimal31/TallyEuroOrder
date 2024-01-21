using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrderApp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class FreeOrder : System.Web.UI.Page
    {
        int total = 0; int FreeScheme = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string strType = "";
                Int32 OrderId = 0;

                if (!Page.IsPostBack)
                {
                    //txtTotalKgsF.Attributes.Add("readonly", "readonly");
                    //txtFreetotalkg.Attributes.Add("readonly", "readonly");
                    txtFreeKg.Attributes.Add("readonly", "readonly");
                    drpsSalesExe.Enabled = false;
                    txtDealerCodeSearch.Attributes.Add("readonly", "readonly");
                    txtOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                    txtFreeKg.Text = "0";
                    isview.Text = "0";
                    string UserType = "";
                    UserType = Convert.ToString(HttpContext.Current.Session["UserType"]);
                    bindSale();
                    hdTotalKgCount.Value = "0";
                    lblheading.Text = CommMessage.addfreescheme;
                    if (Request.QueryString["q"] != null)
                    {
                        string strKey = Convert.ToString(Request.QueryString["q"]);
                        Common cmn = new Common();
                        strKey = cmn.Decrypt(strKey);
                        OrderId = Convert.ToInt32(strKey);

                        editorderid.Text = OrderId.ToString();
                        lblheading.Text = CommMessage.Editfreescheme;
                    }

                    if (Request.QueryString["BackButton"] != null && Request.QueryString["BackButton"] == "N")
                        ViewState["BackButton"] = "N";
                    else
                        ViewState["BackButton"] = "Y";

                    if (Request.QueryString["View"] != null && Request.QueryString["View"] == "Y")
                    {
                        isview.Text = "1";
                        lblheading.Text = CommMessage.viewWithBillFreeScheme;
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

                    txtdealernamesearch.Text = Convert.ToString(dt.Rows[0]["DealerName"]);
                    if (ViewState["StateID"] != null && Convert.ToString(ViewState["StateID"]) != "0")
                    {
                        ViewState["OldStateID"] = ViewState["StateID"];
                    }

                    ViewState["StateID"] = Convert.ToInt32(dt.Rows[0]["StateID"]);
                    hdstate.Value = Convert.ToString(dt.Rows[0]["StateID"]);

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
                    txtdealernamesearch.Text = "";
                    hdstate.Value = "";
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

        public decimal Caltotal(decimal totalkg, string PackingType, int ProductQty)
        {
            decimal totalkgcount = 0;
            try
            {
                totalkgcount = (totalkg * ProductQty);

                if (PackingType.ToLower() == "gram")
                {
                    totalkgcount = totalkgcount / 1000;
                }
            }
            catch (Exception)
            {
            }
            return totalkgcount;

        }
        public static decimal staticCaltotal(decimal totalkg, string PackingType, int ProductQty)
        {
            decimal totalkgcount = 0;
            try
            {
                totalkgcount = (totalkg * ProductQty);

                if (PackingType.ToLower() == "gram")
                {
                    totalkgcount = totalkgcount / 1000;
                }
            }
            catch (Exception)
            {
            }
            return totalkgcount;

        }
        public static decimal Caltotals(decimal totalkg, string PackingType, int ProductQty)
        {
            decimal totalkgcount = 0;
            try
            {
                totalkgcount = (totalkg * ProductQty);

                if (PackingType.ToLower() == "gram")
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
                DataSet _dt = new DataSet();
                ObjOrder.OrderID = Convert.ToString(OrderId);
                ObjOrder.GET_RECORDS_FROM_tblOrderByOrderIdNew(ref _dt);

                if (_dt != null)
                {
                    drpsSalesExe.SelectedValue = Convert.ToString(_dt.Tables[0].Rows[0]["SalesManId"]);
                    txtDealerCodeSearch.Text = Convert.ToString(_dt.Tables[0].Rows[0]["DealerCode"]);
                    txtdealernamesearch.Text = Convert.ToString(_dt.Tables[0].Rows[0]["DealerName"]);
                    ViewState["StateID"] = Convert.ToInt32(_dt.Tables[0].Rows[0]["StateID"]);
                    hdstate.Value = Convert.ToString(_dt.Tables[0].Rows[0]["StateID"]);

                    if (Convert.ToString(_dt.Tables[0].Rows[0]["DealerCode"]) == "")
                    {
                        txtDealerCodeSearch.Text = Convert.ToString(_dt.Tables[0].Rows[0]["DealerName"]);
                    }
                    hdDelaerId.Value = Convert.ToString(_dt.Tables[0].Rows[0]["DealerId"]);

                    drpOrderStatus.Text = Convert.ToString(_dt.Tables[0].Rows[0]["OrderStatus"]);
                    //txtFromScheme.Text = Convert.ToString(_dt.Tables[0].Rows[0]["FreeSchemeFrom"]);
                    //txtToScheme.Text = Convert.ToString(_dt.Tables[0].Rows[0]["FreeSchemeTO"]);

                    if (Convert.ToString(ViewState["Operation"]) == "A")
                    {
                        hdTotalKgCount.Value = Convert.ToString(_dt.Tables[0].Rows[0]["TotalKgGm"]);
                       // txtTotalKgsF.Text = hdTotalKgCount.Value;
                    }
                    else
                    {
                        hdTotalKgCount.Value = Convert.ToString(_dt.Tables[0].Rows[0]["TotalKgGm"]);
                        //txtFreetotalkg.Text = Convert.ToString(_dt.Tables[0].Rows[0]["TotalKgGm"]);
                        //txtTotalKgsF.Text = Convert.ToString(_dt.Tables[0].Rows[0]["PurchaseKgs"]);
                    }

                    hdEditOrderId.Value = Convert.ToString(_dt.Tables[0].Rows[0]["OrderId"]);

                    if (Convert.ToString(_dt.Tables[0].Rows[0]["OrderType"]).ToLower() == CommMessage.OrderType_withbillFreeScheme.ToLower())
                    {
                        txtOther.Text = Convert.ToString(_dt.Tables[0].Rows[0]["Other"]);
                        txtPOP.Text = Convert.ToString(_dt.Tables[0].Rows[0]["POP"]);
                        txtsitedelivery.Text = Convert.ToString(_dt.Tables[0].Rows[0]["SiteDelivery"]);
                        txttransport.Text = Convert.ToString(_dt.Tables[0].Rows[0]["Transport"]);

                        txtOrderDate.Text = Convert.ToDateTime(Convert.ToString(_dt.Tables[0].Rows[0]["OrderDate"])).ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                       // txtFreetotalkg.Text = Convert.ToString(_dt.Tables[0].Rows[0]["TotalKgGm"]);
                        txtFreeKg.Text = Convert.ToString(_dt.Tables[0].Rows[0]["TotalKgGm"]);

                        hdTotalFreeKgCount.Value = Convert.ToString(_dt.Tables[0].Rows[0]["TotalKgGm"]);

                        hdFreeOrderId.Value = Convert.ToString(_dt.Tables[0].Rows[0]["OrderID"]);
                        hdEditOrderId.Value = Convert.ToString(_dt.Tables[0].Rows[0]["ParentOrderId"]);

                        
                    }

                    hdOrdertype.Value = Convert.ToString(_dt.Tables[0].Rows[0]["OrderType"]).ToLower();

                    grdProductFree.DataSource = _dt.Tables[1];
                    grdProductFree.DataBind();

                  

                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }



        
        protected void grdProductFree_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                total += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Totalkg"));
                FreeScheme += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "FreeScheme"));

                GridViewRow row = e.Row;
                row.Attributes["ProductId"] = grdProductFree.DataKeys[e.Row.RowIndex].Value.ToString();
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblamount = (Label)e.Row.FindControl("lbltotal");
                lblamount.Text = total.ToString();

                Label lblTotalFree = (Label)e.Row.FindControl("lblTotalFree");
                lblTotalFree.Text = FreeScheme.ToString();
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(ViewState["BackButton"]) == "Y")
                Response.Redirect("OrderList.aspx", false);
            else
                Response.Redirect("Dashboard.aspx", false);
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetProcustlist()
        {
            WSResponseObject response = new WSResponseObject();
            try
            {
                DataTable dt = new DataTable();
                BA_tblProduct objBA_tblProduct = new BA_tblProduct();
                if (objBA_tblProduct.SELECT_ALL_tblProduct(ref dt))
                {

                }
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(dt);
                return JSONString;

            }
            catch (Exception ex)
            {
                return "Errro :" + ex.Message;
            }
            return "nodata";
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetProductPacking(string id)
        {
            WSResponseObject response = new WSResponseObject();
            try
            {
                DataTable dt = new DataTable();
                BA_tblProductPacking objBA_tblProductPacking = new BA_tblProductPacking();
                objBA_tblProductPacking.ProductID = id;
                if (objBA_tblProductPacking.GET_RECORDS_FROM_tblProductById(ref dt))
                {

                }
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(dt);
                return JSONString;

            }
            catch (Exception ex)
            {
                return "Errro :" + ex.Message;
            }
            return "nodata";
        }

      
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string SaveData(long Stateid, string data,  string OrderProductDetails,string OrderFreeProductDetails)
        {

             List<FreeOrderProductData> OrderProductDetailDatadtl = new List<FreeOrderProductData>();
            List<FreePurchaseOrderProductData> OrderFreeProductDetailDatadtl = new List<FreePurchaseOrderProductData>();

            int ReturnId = 0;

            List<string> OrderProductDetails1 = new List<string>();
            OrderProductDetails1.Add(OrderProductDetails);


            List<string> OrderFreeProductDetails1 = new List<string>();
            OrderFreeProductDetails1.Add(OrderFreeProductDetails);



            string xmlOrderProductDetails = "";

            string xmlOrderFreeProductDetails = "";
            BA_tblOrder ObjOrder = new BA_tblOrder();

            ObjOrder = JsonConvert.DeserializeObject<BA_tblOrder>(data);
            ObjOrder.OrderType = CommMessage.OrderType_withbillFreeScheme;
           // ObjOrder.ParentOrderId = hdEditOrderId.Value;

            FreeOrderProductData FreeOrderProductData1 = new FreeOrderProductData();
           

            dynamic Jsontradelist = null;
            if (Convert.ToString(OrderProductDetails1[0]) != "" && Convert.ToString(OrderProductDetails1[0]) != "null")
            {
                Jsontradelist = JArray.Parse(Convert.ToString(OrderProductDetails1[0]));
                foreach (JToken tag in Jsontradelist)
                {
                    try
                    {
                        FreeOrderProductData1 = new JavaScriptSerializer().Deserialize<FreeOrderProductData>(tag.ToString());
                        FreeOrderProductData1.ProductCode = GetProductCode(Stateid,
                            Convert.ToString(FreeOrderProductData1.SchemeId), Convert.ToInt32(FreeOrderProductData1.ProductPckID));
                        OrderProductDetailDatadtl.Add(FreeOrderProductData1);
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }

            FreePurchaseOrderProductData FreePurchaseOrderProductData1 = new FreePurchaseOrderProductData();
            dynamic JsontradeFreelist = null;
            if (Convert.ToString(OrderFreeProductDetails1[0]) != "" && Convert.ToString(OrderFreeProductDetails1[0]) != "null")
            {
                JsontradeFreelist = JArray.Parse(Convert.ToString(OrderFreeProductDetails1[0]));
                foreach (JToken Freetag in JsontradeFreelist)
                {
                    try
                    {
                        FreePurchaseOrderProductData1 = new JavaScriptSerializer().Deserialize<FreePurchaseOrderProductData>(Freetag.ToString());
                        OrderFreeProductDetailDatadtl.Add(FreePurchaseOrderProductData1);
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }


            try
            {
                xmlOrderProductDetails = xmlOrderProductDetail(OrderProductDetailDatadtl);
                xmlOrderFreeProductDetails = xmlOrderFreeProductDetail(OrderFreeProductDetailDatadtl);
                if (xmlOrderProductDetails != "")
                {
                    ObjOrder.xmlProd = xmlOrderProductDetails;
                    ObjOrder.xmlFreeProd = xmlOrderFreeProductDetails;
                    ObjOrder.TotalKgGm = Convert.ToDecimal(ObjOrder.TotalKgGm);
                    ObjOrder.CreateBy = Convert.ToInt32(HttpContext.Current.Session["UserId"]);

                    ObjOrder.INSERT_tblOrderFreeNew(ref ReturnId);

                }
                    
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }

            return ReturnId.ToString();
        }


        public static string xmlOrderProductDetail(List<FreeOrderProductData> FreeOrderProductDatadtl)
        {

            string XML = "";
            try
            {
                decimal totalkg = 0;
                XML = "<OrderProduct>";
                for (int i = 0; i < FreeOrderProductDatadtl.Count; i++)
                {

                    XML += "<TABLE>";
                    XML += "<ProductId>" + Convert.ToInt64(FreeOrderProductDatadtl[i].ProductID) + "</ProductId>";
                    if (FreeOrderProductDatadtl[i].ProductPckID != "")
                    {
                        XML += "<ProductPckIds>" + Convert.ToInt64(FreeOrderProductDatadtl[i].ProductPckID) + "</ProductPckIds>";
                    }
                    else
                    {
                        XML += "<ProductPckIds>" + 0 + "</ProductPckIds>";

                    }

                    XML += "<ProductPck>" + FreeOrderProductDatadtl[i].ProductPck + "</ProductPck>";
                    XML += "<PackingNos>" + Convert.ToInt32(FreeOrderProductDatadtl[i].PackingNos) + "</PackingNos>";
                    XML += "<PackingType>" + FreeOrderProductDatadtl[i].PackingType + "</PackingType>";
                    XML += "<BoxORNos>NO</BoxORNos>";
                    XML += "<PckTotalKg>" + FreeOrderProductDatadtl[i].PckTotalKg + "</PckTotalKg>";
                    XML += "<ProductQty>" + Convert.ToInt32(FreeOrderProductDatadtl[i].QTY) + "</ProductQty>";
                    XML += "<IsScheme>" + FreeOrderProductDatadtl[i].isscheme + "</IsScheme>";
                    if (FreeOrderProductDatadtl[i].isscheme == "")
                    {
                        XML += "<Scheme></Scheme>";
                    }
                    else
                    {
                        XML += "<Scheme>" + FreeOrderProductDatadtl[i].Scheme + "</Scheme>";
                    }
                    XML += "<SchemeId>" + Convert.ToInt32(FreeOrderProductDatadtl[i].SchemeId) + "</SchemeId>";
                    XML += "<ProductCode>" + Convert.ToString(FreeOrderProductDatadtl[i].ProductCode) + "</ProductCode>";
                    XML += "<PDtlSrno>" + 0 + "</PDtlSrno>";
                    XML += "</TABLE>";
                    totalkg = totalkg + staticCaltotal(Convert.ToDecimal(FreeOrderProductDatadtl[i].PckTotalKg), Convert.ToString(FreeOrderProductDatadtl[i].PackingType), Convert.ToInt32(FreeOrderProductDatadtl[i].QTY));

                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return XML += "</OrderProduct>";


        }


        public static string xmlOrderFreeProductDetail(List<FreePurchaseOrderProductData> FreePurchaseOrderProductDatadtl)
        {

            string XML = "";
            try
            {
                decimal totalkg = 0;
                XML = "<FreeOrderProduct>";
                for (int i = 0; i < FreePurchaseOrderProductDatadtl.Count; i++)
                {

                    XML += "<TABLE>";
                    XML += "<OrderSrNo>" + Convert.ToInt64(FreePurchaseOrderProductDatadtl[i].OrderSrNo) + "</OrderSrNo>";
                    XML += "<ProductId>" + Convert.ToInt64(FreePurchaseOrderProductDatadtl[i].ProductID) + "</ProductId>";
                    XML += "<Fromscheme>" + Convert.ToInt64(FreePurchaseOrderProductDatadtl[i].Fromscheme) + "</Fromscheme>";
                    XML += "<Toscheme>" + Convert.ToInt64(FreePurchaseOrderProductDatadtl[i].Toscheme) + "</Toscheme>";
                    XML += "<Totalkg>" + Convert.ToDecimal(FreePurchaseOrderProductDatadtl[i].Totalkg) + "</Totalkg>";
                    XML += "<FreeSchemeTotalkg>" + Convert.ToDecimal(FreePurchaseOrderProductDatadtl[i].FreeSchemeTotalkg) + "</FreeSchemeTotalkg>";
                    XML += "</TABLE>";
              
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return XML += "</FreeOrderProduct>";


        }


        static string GetProductCode(long StateId, string SchemeID, long ProductPckID)
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

        public static string xmlOrderFreeCreate(List<OrderFreeProduct> OrderFreeProductdtl)
        {
            string XML = "";
            try
            {

                XML = "<OrderFree>";
                for (int i = 0; i < OrderFreeProductdtl.Count; i++)
                {

                    XML += "<TABLE>";
                    XML += "<OrderSrNo>" + OrderFreeProductdtl[i].hdProdSrno + "</OrderSrNo>";
                    XML += "<ProductId>" + Convert.ToInt64(OrderFreeProductdtl[i].item) + "</ProductId>";
                    XML += "<AnnualPurchasQty>" + Convert.ToDecimal(OrderFreeProductdtl[i].PurchaseK) + "</AnnualPurchasQty>";
                    XML += "<FreeSchemeFrom>" + Convert.ToDecimal(OrderFreeProductdtl[i].FromScheme) + "</FreeSchemeFrom>";
                    XML += "<FreeSchemeTO>" + Convert.ToInt64(OrderFreeProductdtl[i].ToScheme) + "</FreeSchemeTO>";

                    XML += "</TABLE>";


                }
            }
            catch (Exception)
            { }
            return XML += "</OrderFree>";


        }

    

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetSelectOrderFreeProduct(string orderid)
        {
            WSResponseObject response = new WSResponseObject();
            try
            {
                DataTable dt = new DataTable();
                BA_tblOrderFreeProduct objBA_tblOrderFreeProduct = new BA_tblOrderFreeProduct();
                objBA_tblOrderFreeProduct.OrderID = orderid;
                if (objBA_tblOrderFreeProduct.GET_RECORDS_FROM_OrderFreeProduct_to_Order(ref dt))
                {
                }
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(dt);
                return JSONString;

            }
            catch (Exception ex)
            {
                return "Errro :" + ex.Message;
            }
            return "nodata";
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetOrderProductDetails(string orderid)
        {
            WSResponseObject response = new WSResponseObject();
            try
            {
                DataTable dt = new DataTable();
                BA_tblOrderProductDetails objBA_tblOrderProductDetails = new BA_tblOrderProductDetails();
                objBA_tblOrderProductDetails.OrderID = Convert.ToInt32(orderid);
                if (objBA_tblOrderProductDetails.GET_RECORDS_FROM_OrderProductDetails_to_Order(ref dt))
                {
                }
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(dt);
                return JSONString;

            }
            catch (Exception ex)
            {
                return "Errro :" + ex.Message;
            }
            return "nodata";
        }


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string GetProductPackingScheme(long ProductPckId, long Stateid)
        {
            WSResponseObject response = new WSResponseObject();
            try
            {
                BA_tblScheme ObjBA_tblScheme = new BA_tblScheme();
                DataTable dt = new DataTable();

                if (ObjBA_tblScheme.SELECT_ALL_tblSchemeAPI(ProductPckId, Stateid, ref dt))
                {
                }

                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(dt);
                return JSONString;


            }
            catch (Exception ex)
            {
                return "Errro :" + ex.Message;
            }
            return "nodata";
        }
    }
}


public class FreeOrderProductData
{
    public string ProductID { get; set; }
    public string ProductPckID { get; set; }

    public string ProductPck { get; set; }

    public string PackingNos { get; set; }
    public string PackingType { get; set; }
    public string PckTotalKg { get; set; }

    public string QTY { get; set; }

    public string Scheme { get; set; }
    public string ProductCode { get; set; }
    public string TotalKg { get; set; }

    public long SchemeId { get; set; }

    public string isscheme { get; set; }
    public  string BoxORNos { get; set; }

}


public class FreePurchaseOrderProductData
{
    public long OrderSrNo { get; set; }
    public long ProductID { get; set; }
  
    public string Fromscheme { get; set; }

    public string Toscheme { get; set; }
    public string Totalkg { get; set; }
    public string FreeSchemeTotalkg { get; set; }


}