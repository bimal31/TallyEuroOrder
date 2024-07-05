using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlTypes;

/// <summary>
/// Summary description for BA_tblOrder
/// </summary>
public class BA_tblOrder
{
    DA_tblOrder objDA_tblOrder = new DA_tblOrder();

    public BA_tblOrder()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private string _OrderID;
    public string OrderID
    {
        get { return _OrderID; }
        set { _OrderID = value; }
    }
    private string _FreeOrderId;
    public string FreeOrderId
    {
        get { return _FreeOrderId; }
        set { _FreeOrderId = value; }
    }
    private string _OrderSrNo;
    public string OrderSrNo
    {
        get { return _OrderSrNo; }
        set { _OrderSrNo = value; }
    }
    private SqlDateTime? _OrderDate;
    public SqlDateTime? OrderDate
    {
        get { return _OrderDate; }
        set { _OrderDate = value; }
    }
    private string _OrderType;
    public string OrderType
    {
        get { return _OrderType; }
        set { _OrderType = value; }
    }
    private string _PurchaseDurationFromDate;
    public string PurchaseDurationFromDate
    {
        get { return _PurchaseDurationFromDate; }
        set { _PurchaseDurationFromDate = value; }
    }
    private string _PurchaseDurationToDate;
    public string PurchaseDurationToDate
    {
        get { return _PurchaseDurationToDate; }
        set { _PurchaseDurationToDate = value; }
    }
    private decimal _PurchaseKgs;
    public decimal PurchaseKgs
    {
        get { return _PurchaseKgs; }
        set { _PurchaseKgs = value; }
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
    private decimal _TotalKgGm;
    public decimal TotalKgGm
    {
        get { return _TotalKgGm; }
        set { _TotalKgGm = value; }
    }
    private decimal _TotalFreeKgGm;
    public decimal TotalFreeKgGm
    {
        get { return _TotalFreeKgGm; }
        set { _TotalFreeKgGm = value; }
    }
    private string _DealerId;
    public string DealerId
    {
        get { return _DealerId; }
        set { _DealerId = value; }
    }
    //private decimal _TotalKg;
    //public decimal TotalKg
    //{
    //    get { return _TotalKg; }
    //    set { _TotalKg = value; }
    //}
    private string _Other;
    public string Other
    {
        get { return _Other; }
        set { _Other = value; }
    }
    private string _POP;
    public string POP
    {
        get { return _POP; }
        set { _POP = value; }
    }

    private string _SiteDelivery;
    public string SiteDelivery 
    {
        get { return _SiteDelivery; }
        set { _SiteDelivery = value; }
    }

    
    private string _OrderStatus;
    public string OrderStatus
    {
        get { return _OrderStatus; }
        set { _OrderStatus = value; }
    }
    private string _ParentOrderId;
    public string ParentOrderId
    {
        get { return _ParentOrderId; }
        set { _ParentOrderId = value; }
    }
    private bool _IsFree;
    public bool IsFree
    {
        get { return _IsFree; }
        set { _IsFree = value; }
    }
    private string _Isdeleted;
    public string Isdeleted
    {
        get { return _Isdeleted; }
        set { _Isdeleted = value; }
    }
    private int _CreateBy;
    public int CreateBy
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
    private int _UpdateBy;
    public int UpdateBy
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
    private string _OrderFromDate;
    public string OrderFromDate
    {
        get { return _OrderFromDate; }
        set { _OrderFromDate = value; }
    }
    private string _OrderToDate;
    public string OrderToDate {
        get { return _OrderToDate; }
        set { _OrderToDate = value; }
    }

    private string _xmlProd;
    public string xmlProd
    {
        get { return _xmlProd; }
        set { _xmlProd = value; }
    }

    private string _xmlFreeProd;
    public string xmlFreeProd
    {
        get { return _xmlFreeProd; }
        set { _xmlFreeProd = value; }
    }

    private string _UserType;
    public string UserType
    {
        get { return _UserType; }
        set { _UserType = value; }
    }

    private DateTime _FromDate;
    public DateTime FromDate
    {
        get { return _FromDate; }
        set { _FromDate = value; }
    }

    private DateTime _ToDate;
    public DateTime ToDate
    {
        get { return _ToDate; }
        set { _ToDate = value; }
    }

    private int _SalesId;
    public int SalesId
    {
        get { return _SalesId; }
        set { _SalesId = value; }
    }

    private string _Transport;
    public string Transport
    {
        get { return _Transport; }
        set { _Transport = value; }
    }
  

    public bool INSERT_tblOrder(ref int ReturnId)
    {
        try
        {
            return objDA_tblOrder.INSERT_tblOrder(this, ref ReturnId);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool INSERT_tblOrderDealerScheme(ref int ReturnId)
    {
        try
        {
            return objDA_tblOrder.INSERT_tblOrderDealerScheme(this, ref ReturnId);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_tblOrder(ref DataTable dt)
    {
        try
        {
            if (objDA_tblOrder.SELECT_ALL_tblOrder(this, ref dt))
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

    public bool DELETE_RECORDS_FROM_tblOrder()
    {
        try
        {
            if (objDA_tblOrder.DELETE_RECORDS_FROM_tblOrder(this))
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

    public bool GET_RECORDS_FROM_tblOrder(ref DataTable dt)
    {
        try
        {
            if (objDA_tblOrder.GET_RECORDS_FROM_tblOrder(this, ref dt))
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

    public bool GET_RECORDS_FROM_tblOrderByDate(ref DataTable dt)
    {
        try
        {
            if (objDA_tblOrder.GET_RECORDS_FROM_tblOrderByDate(this, ref dt))
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

    public bool UPDATE_tblOrder(ref int ReturnId)
    {
        try
        {
            if (objDA_tblOrder.UPDATE_tblOrder(this, ref ReturnId))
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

    public bool UPDATE_tblOrderDealerScheme(ref int ReturnId)
    {
        try
        {
            if (objDA_tblOrder.UPDATE_tblOrderDealerScheme(this, ref ReturnId))
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

    public bool UPDATE_tblOrderStatus()
    {
        try
        {
            if (objDA_tblOrder.UPDATE_tblOrderStatus(this))
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

    public bool INSERT_RECORDS_FROM_tblOrderProductDetails(string XML)
    {
        try
        {
            if (objDA_tblOrder.INSERT_RECORDS_FROM_tblOrderProductDetails(this, XML))
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

    public bool GET_RECORDS_FROM_tblOrderByOrderId(ref DataTable dt)
    {
        try
        {
            if (objDA_tblOrder.GET_RECORDS_FROM_tblOrderByOrderId(this, ref dt))
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


    public bool GET_RECORDS_FROM_tblOrderByOrderIdNew(ref DataSet ds)
    {
        try
        {
            if (objDA_tblOrder.GET_RECORDS_FROM_tblOrderByOrderIdNew(this, ref ds))
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

    public bool GET_RECORDS_FROM_tblOrderByOrderIdDetails(ref DataTable dt)
    {
        try
        {
            if (objDA_tblOrder.GET_RECORDS_FROM_tblOrderByOrderIdDetails(this, ref dt))
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

    public bool GET_RECORDS_FROM_PrinttblOrder(ref DataSet ds)
    {
        try
        {
            if (objDA_tblOrder.GET_RECORDS_FROM_PrinttblOrder(this, ref ds))
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
    public bool GET_RECORDS_FROM_PrinttblOrder_Table(ref DataSet ds)
    {
        try
        {
            if (objDA_tblOrder.GET_RECORDS_FROM_PrinttblOrder_table(this, ref ds))
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
    public bool GET_RECORDS_FROM_tblParentOrderByOrderId(ref DataTable dt)
    {
        try
        {
            if (objDA_tblOrder.GET_RECORDS_FROM_tblParentOrderByOrderId(this, ref dt))
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


    public bool GET_Order_API(ref DataTable dt)
    {
        try
        {
            if (objDA_tblOrder.GET_Order_API(this, ref dt))
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

    public bool GET_FreeOrder_API(ref DataTable dt)
    {
        try
        {
            if (objDA_tblOrder.GET_FreeOrder_API(this, ref dt))
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


    public bool GET_OrderView_API(ref DataSet dt)
    {
        try
        {
            if (objDA_tblOrder.GET_OrderView_API(this, ref dt))
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

    public bool GET_FreeOrderView_API(ref DataSet dt)
    {
        try
        {
            if (objDA_tblOrder.GET_FreeOrderView_API(this, ref dt))
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


    public bool INSERT_tblOrder_API(ref int ReturnId)
    {
        try
        {
            return objDA_tblOrder.INSERT_tblOrder_API(this, ref ReturnId);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool INSERT_tblOrder_API_New(ref int ReturnId)
    {
        try
        {
            return objDA_tblOrder.INSERT_tblOrder_API_New(this, ref ReturnId);
        }
        catch (Exception ex)
        {
            return false;
        }
    }





    public bool UPDATE_tblOrderFree(ref int ReturnId)
    {
        try
        {
            if (objDA_tblOrder.UPDATE_tblOrderFree(this, ref ReturnId))
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

    public bool INSERT_tblOrderFree(ref int ReturnId)
    {
        try
        {
            return objDA_tblOrder.INSERT_tblOrderFree(this, ref ReturnId);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool INSERT_tblOrderFreeNew(ref int ReturnId)
    {
        try
        {
            return objDA_tblOrder.INSERT_tblOrderFreeNew(this, ref ReturnId);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool INSERT_tblOrderDealerSchemeNew(ref int ReturnId)
    {
        try
        {
            return objDA_tblOrder.INSERT_tblOrderDealerSchemeNew(this, ref ReturnId);
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    public bool SELECT_ALL_PendingBillTally(ref DataTable dt)
    {
        try
        {
            if (objDA_tblOrder.SELECT_ALL_PendingBillTally(this, ref dt))
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

    public bool INSERT_tblOrderNew(ref int ReturnId)
    {
        try
        {
            return objDA_tblOrder.INSERT_tblOrderNew(this, ref ReturnId);
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    //  *****************************************************************************
    //  You Can use following code to fillup form
    //  *****************************************************************************
    //  objBA_tblOrder.OrderID = "";
    //  objBA_tblOrder.OrderSrNo = "";
    //  objBA_tblOrder.OrderDate = "";
    //  objBA_tblOrder.OrderType = "";
    //  objBA_tblOrder.DealerId = "";
    //  objBA_tblOrder.TotalKg = "";
    //  objBA_tblOrder.Other = "";
    //  objBA_tblOrder.POP = "";
    //  objBA_tblOrder.Isdeleted = "";
    //  objBA_tblOrder.CreateBy = "";
    //  objBA_tblOrder.CreateDate = "";
    //  objBA_tblOrder.UpdateBy = "";
    //  objBA_tblOrder.UpdateDate = "";


    public bool SELECT_ALL_tblOrder_orderstatus_Factory(ref DataTable dt)
    {
        try
        {
            if (objDA_tblOrder.SELECT_ALL_tblOrder_orderstatus_Factory(this, ref dt))
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