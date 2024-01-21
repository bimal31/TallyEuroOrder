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
    public partial class AddDealerOrderScheme : System.Web.UI.Page
    {
        public bool Ispaymentpending = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {

                    isview.Text = "0";
                    string UserType = "";
                    UserType = Convert.ToString(HttpContext.Current.Session["UserType"]);

                    bindSale();
                    txtOrderDate.Text = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                    lblTotal.Text = "0";
                    txtTotalFreeKg.Attributes.Add("readonly", "readonly");
                    lblTotal.Attributes.Add("readonly", "readonly");
                    hdTotalKgCount.Value = "0";
                    // BindGrid();

                    lblheading.Text = CommMessage.addfreescheme;

                    if (Request.QueryString["q"] != null)
                    {
                        string strKey = Convert.ToString(Request.QueryString["q"]);
                        Common cmn = new Common();
                        strKey = cmn.Decrypt(strKey);

                        Int32 OrderId = Convert.ToInt32(strKey);
                        editorderid.Text = OrderId.ToString();
                        lblheading.Text = CommMessage.Editfreescheme;

                        GetOrderDetails(OrderId);
                    }
                    if (Request.QueryString["BackButton"] != null && Request.QueryString["BackButton"] == "N")
                        ViewState["BackButton"] = "N";
                    else
                        ViewState["BackButton"] = "Y";

                    if (Request.QueryString["View"] != null && Request.QueryString["View"] == "Y")
                    {
                        isview.Text = "1";
                        lblheading.Text = CommMessage.viewfreescheme;
                        //btnSubmitOrder.Attributes.Add("style", "display:none");
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
        
        protected void txtDealerCodeSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (GetDealerRecord())
                {
                    if (Ispaymentpending)
                    {
                        lblErrorMessage.Text = CommMessage.Dealerpayment;
                        
                    }
                    else
                    {
                        lblErrorMessage.Text = "";
                    }
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
                    Ispaymentpending = Convert.ToBoolean(dt.Rows[0]["Ispaymentpending"]);
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

        protected void ClearData()
        {
            try
            {
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
                    txtFromDate.Text = Convert.ToDateTime(_dt.Rows[0]["PurchaseDurationFromDate"]).ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                    txtToDate.Text = Convert.ToDateTime(_dt.Rows[0]["PurchaseDurationToDate"]).ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                    //txtFromScheme.Text = Convert.ToInt32(_dt.Rows[0]["FreeSchemeFrom"]).ToString();
                    //txtToScheme.Text = Convert.ToInt32(_dt.Rows[0]["FreeSchemeTO"]).ToString();
                    lblTotal.Text = Convert.ToString(Convert.ToDecimal(_dt.Rows[0]["TotalKgGm"]));
                    txtPurchaseKg.Text = Convert.ToInt32(_dt.Rows[0]["PurchaseKgs"]).ToString();
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
                    txtpincode.Text = Convert.ToString(_dt.Rows[0]["Pincode"]);
                    hdDelaerId.Value = Convert.ToString(_dt.Rows[0]["DealerId"]);
                    ViewState["StateID"] = Convert.ToInt32(_dt.Rows[0]["StateID"]);
                    hdstate.Value = Convert.ToString(_dt.Rows[0]["StateID"]);

                    txtTotalFreeKg.Text = Convert.ToString(_dt.Rows[0]["TotalKgGm"]);
                    
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
                Response.Redirect("DealerOrderSchemeList.aspx", false);
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
        public static string SaveData(long Stateid, string data, string productFreedata, string OrderProductDetails)
        {

            List<OrderProductDetailData> OrderProductDetailDatadtl = new List<OrderProductDetailData>();
            List<OrderFreeProduct> OrderFreeProductdtl = new List<OrderFreeProduct>();

            int ReturnId = 0;
            List<string> productFreedata1 = new List<string>();
            productFreedata1.Add(productFreedata);
            List<string> OrderProductDetails1 = new List<string>();
            OrderProductDetails1.Add(OrderProductDetails);

            string xmlOrderFreeCreates = "";
            string xmlOrderProductDetails = "";
            BA_tblOrder ObjOrder = new BA_tblOrder();

            ObjOrder = JsonConvert.DeserializeObject<BA_tblOrder>(data);
            ObjOrder.OrderType = CommMessage.OrderType_FreeScheme;
            ObjOrder.PurchaseDurationFromDate = DateTime.ParseExact(ObjOrder.PurchaseDurationFromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", new CultureInfo("en-GB"));

            ObjOrder.PurchaseDurationToDate = DateTime.ParseExact(ObjOrder.PurchaseDurationToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", new CultureInfo("en-GB"));


            OrderFreeProduct OrderFreeProductdtl1 = new OrderFreeProduct();
            OrderProductDetailData OrderProductDetailDatadtl1 = new OrderProductDetailData();
            dynamic Jsontradelist1 = null;
            if (Convert.ToString(productFreedata1[0]) != "" && Convert.ToString(productFreedata1[0]) != "null")
            {

                if (OrderFreeProductdtl.Count != 0)
                {
                    OrderFreeProductdtl.Clear();
                }
                Jsontradelist1 = JArray.Parse(Convert.ToString(productFreedata1[0]));
                foreach (JToken tag in Jsontradelist1)
                {
                    OrderFreeProductdtl1 = new JavaScriptSerializer().Deserialize<OrderFreeProduct>(tag.ToString());
                    OrderFreeProductdtl.Add(OrderFreeProductdtl1);

                }
            }

            dynamic Jsontradelist = null;
            if (Convert.ToString(OrderProductDetails1[0]) != "" && Convert.ToString(OrderProductDetails1[0]) != "null")
            {
                Jsontradelist = JArray.Parse(Convert.ToString(OrderProductDetails1[0]));
                foreach (JToken tag in Jsontradelist)
                {
                    try
                    {
                        OrderProductDetailDatadtl1 = new JavaScriptSerializer().Deserialize<OrderProductDetailData>(tag.ToString());
                        OrderProductDetailDatadtl1.ProductCode = GetProductCode(Stateid,
                            Convert.ToString(OrderProductDetailDatadtl1.SchemeId), Convert.ToInt32(OrderProductDetailDatadtl1.ProductPckID));
                        OrderProductDetailDatadtl.Add(OrderProductDetailDatadtl1);
                    }
                    catch (Exception ex)
                    {
                        
                    }
                        
                }
            }
            try
            {
                xmlOrderFreeCreates = xmlOrderFreeCreate(OrderFreeProductdtl);
                xmlOrderProductDetails = xmlOrderProductDetail(OrderProductDetailDatadtl);
                if (xmlOrderFreeCreates != "")
                {
                    ObjOrder.xmlProd = xmlOrderProductDetails;
                    ObjOrder.xmlFreeProd = xmlOrderFreeCreates;
                    ObjOrder.TotalKgGm = Convert.ToDecimal(ObjOrder.TotalKgGm);
                    ObjOrder.CreateBy = Convert.ToInt32(HttpContext.Current.Session["UserId"]);

                    ObjOrder.INSERT_tblOrderDealerScheme(ref ReturnId);
                    
                }

            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }

            return ReturnId.ToString();
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

        public static string xmlOrderProductDetail(List<OrderProductDetailData> OrderProductDetailDatadtl)
        {

            string XML = "";
            try
            {
                decimal totalkg = 0;
                XML = "<OrderProduct>";
                for (int i = 0; i < OrderProductDetailDatadtl.Count; i++)
                {

                    XML += "<TABLE>";
                    XML += "<ProductId>" + Convert.ToInt64(OrderProductDetailDatadtl[i].Productid) + "</ProductId>";
                    if (OrderProductDetailDatadtl[i].ProductPckID != "")
                    {
                        XML += "<ProductPckIds>" + Convert.ToInt64(OrderProductDetailDatadtl[i].ProductPckID) + "</ProductPckIds>";
                    }
                    else
                    {
                        XML += "<ProductPckIds>" + 0 + "</ProductPckIds>";

                    }

                    XML += "<ProductPck>" + OrderProductDetailDatadtl[i].PKG + "</ProductPck>";
                    XML += "<PackingNos>" + Convert.ToInt32(OrderProductDetailDatadtl[i].QTY) + "</PackingNos>";
                    XML += "<PackingType>" + OrderProductDetailDatadtl[i].PackingType + "</PackingType>";
                    XML += "<BoxORNos>NO</BoxORNos>";
                    XML += "<PckTotalKg>" + OrderProductDetailDatadtl[i].PckTotalKg + "</PckTotalKg>";
                    XML += "<ProductQty>" + Convert.ToInt32(OrderProductDetailDatadtl[i].QTY) + "</ProductQty>";
                    XML += "<IsScheme>" + OrderProductDetailDatadtl[i].isscheme + "</IsScheme>";
                    if (OrderProductDetailDatadtl[i].isscheme == "")
                    {
                        XML += "<Scheme></Scheme>";
                    }
                    else
                    {
                        XML += "<Scheme>" + OrderProductDetailDatadtl[i].Schemetext + "</Scheme>";
                    }
                    XML += "<SchemeId>" + Convert.ToInt32(OrderProductDetailDatadtl[i].SchemeId) + "</SchemeId>";
                    XML += "<ProductCode>" + Convert.ToString(OrderProductDetailDatadtl[i].ProductCode) + "</ProductCode>";
                    XML += "<PDtlSrno>" + Convert.ToInt32(OrderProductDetailDatadtl[i].srno) + "</PDtlSrno>";
                    XML += "<FreeOrderSRNO>" + Convert.ToInt32(OrderProductDetailDatadtl[i].arrOrderProductDetailscounterdata) + "</FreeOrderSRNO>";
                    XML += "</TABLE>";
                    totalkg = totalkg + staticCaltotal(Convert.ToDecimal(OrderProductDetailDatadtl[i].PckTotalKg), Convert.ToString(OrderProductDetailDatadtl[i].PackingType), Convert.ToInt32(OrderProductDetailDatadtl[i].QTY));

                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return XML += "</OrderProduct>";


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

public class OrderProductDetailData
{
    public string PKG { get; set; }
    public string QTY { get; set; }
    public string srno { get; set; }
    public string arrOrderProductDetailscounterdata { get; set; }
    public string PackingType { get; set; }
    public string Productid { get; set; }
    public string ProductPckID { get; set; }
    public string PckTotalKg { get; set; }
    public string PackingNos { get; set; }
    public string isscheme { get; set; }
    public string Schemetext { get; set; }

    public long SchemeId { get; set; }

    public string ProductCode { get; set; }




}

public class OrderFreeProduct
{
    public string item { get; set; }
    public string FromScheme { get; set; }
    public string ToScheme { get; set; }
    public string PurchaseK { get; set; }
    public string TotalFreeKg { get; set; }
    public string hdProdSrno { get; set; }
    public string PckTotalKg { get; set; }
}