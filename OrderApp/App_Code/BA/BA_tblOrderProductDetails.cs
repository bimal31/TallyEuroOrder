using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public class BA_tblOrderProductDetails
{
    DA_tblOrderProductDetails objDA_tblOrderProductDetails = new DA_tblOrderProductDetails();
 
    public BA_tblOrderProductDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private int _OrderID;
    public int OrderID
    {
        get { return _OrderID; }
        set { _OrderID = value; }
    }

    private int _ProductId;
    public int ProductId
    {
        get { return _ProductId; }
        set { _ProductId = value; }
    }


    private int _ProductPck;
    public int ProductPck
    {
        get { return _ProductPck; }
        set { _ProductPck = value; }
    }

    private int _PackingNos;
    public int PackingNos
    {
        get { return _PackingNos; }
        set { _PackingNos = value; }
    }

    private string _PackingType;
    public string PackingType
    {
        get { return _PackingType; }
        set { _PackingType = value; }
    }

    private string _BoxORNos;
    public string BoxORNos
    {
        get { return _BoxORNos; }
        set { _BoxORNos = value; }
    }


    private decimal _ProductQty;
    public decimal ProductQty
    {
        get { return _ProductQty; }
        set { _ProductQty = value; }
    }

    private bool _IsScheme;
    public bool IsScheme
    {
        get { return _IsScheme; }
        set { _IsScheme = value; }
    }

    private decimal _PckTotalKg;
    public decimal PckTotalKg
    {
        get { return _PckTotalKg; }
        set { _PckTotalKg = value; }
    }


    private decimal _TotalKg;
    public decimal TotalKg
    {
        get { return _TotalKg; }
        set { _TotalKg = value; }
    }



    private string _Scheme;
    public string Scheme
    {
        get { return _Scheme; }
        set { _Scheme = value; }
    }

    private int _ProductPckID;
    public int ProductPckID
    {
        get { return _ProductPckID; }
        set { _ProductPckID = value; }
    }

    private int _FreeOrderSRNO;
    public int FreeOrderSRNO
    {
        get { return _FreeOrderSRNO; }
        set { _FreeOrderSRNO = value; }
    }
    private int _PDtlSrno;
    public int PDtlSrno
    {
        get { return _PDtlSrno; }
        set { _PDtlSrno = value; }
    }

    private int _CreateBy;
    public int CreateBy
    {
        get { return _CreateBy; }
        set { _CreateBy = value; }
    }

    private DateTime _CreateDate;
    public DateTime CreateDate
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


    private DateTime _UpdateDate;
    public DateTime UpdateDate
    {
        get { return _UpdateDate; }
        set { _UpdateDate = value; }
    }

    private bool _Isdeleted;
    public bool Isdeleted
    {
        get { return _Isdeleted; }
        set { _Isdeleted = value; }
    }

    private long _SchemeId;
    public long SchemeId
    {
        get { return _SchemeId; }
        set { _SchemeId = value; }
    }



    private string _ProductCode;
    public string ProductCode
    {
        get { return _ProductCode; }
        set { _ProductCode = value; }
    }


    public bool GET_RECORDS_FROM_OrderProductDetails_to_Order(ref DataTable dt)
    {
        try
        {
            if (objDA_tblOrderProductDetails.GET_RECORDS_FROM_OrderProductDetails_to_Order(this, ref dt))
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
