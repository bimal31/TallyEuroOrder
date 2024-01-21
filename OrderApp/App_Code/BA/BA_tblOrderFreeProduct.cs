using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public class BA_tblOrderFreeProduct
{
    DA_tblOrderFreeProduct objDA_tblOrderFreeProduct = new DA_tblOrderFreeProduct();

    public BA_tblOrderFreeProduct()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private string _SrNo;
    public string SrNo
    {
        get { return _SrNo; }
        set { _SrNo = value; }
    }
    private string _OrderID;
    public string OrderID
    {
        get { return _OrderID; }
        set { _OrderID = value; }
    }
    private string _OrderSrNo;
    public string OrderSrNo
    {
        get { return _OrderSrNo; }
        set { _OrderSrNo = value; }
    }
    private string _ProductId;
    public string ProductId
    {
        get { return _ProductId; }
        set { _ProductId = value; }
    }
    private decimal _AnnualPurchasQty;
    public decimal AnnualPurchasQty
    {
        get { return _AnnualPurchasQty; }
        set { _AnnualPurchasQty = value; }
    }
    private decimal _FreeSchemeFrom;
    public decimal FreeSchemeFrom
    {
        get { return _FreeSchemeFrom; }
        set { _FreeSchemeFrom = value; }
    }
    private decimal _FreeSchemeTO;
    public decimal FreeSchemeTO
    {
        get { return _FreeSchemeTO; }
        set { _FreeSchemeTO = value; }
    }
    private Boolean _Isdeleted;
    public Boolean Isdeleted
    {
        get { return _Isdeleted; }
        set { _Isdeleted = value; }
    }
    private Int64 _CreateBy;
    public Int64 CreateBy
    {
        get { return _CreateBy; }
        set { _CreateBy = value; }
    }
    private string _CreateDate;
    public string CreateDate
    {
        get { return _CreateDate; }
        set { _CreateDate = value; }
    }
    private Int64 _UpdateBy;
    public Int64 UpdateBy
    {
        get { return _UpdateBy; }
        set { _UpdateBy = value; }
    }
    private string _UpdateDate;
    public string UpdateDate
    {
        get { return _UpdateDate; }
        set { _UpdateDate = value; }
    }
    public bool GET_RECORDS_FROM_OrderFreeProduct_to_Order(ref DataTable dt)
    {
        try
        {
            if (objDA_tblOrderFreeProduct.GET_RECORDS_FROM_OrderFreeProduct_to_Order(this, ref dt))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
