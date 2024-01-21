using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for BA_tblProduct
/// </summary>
public class BA_tblProduct
{
    DA_tblProduct objDA_tblProduct = new DA_tblProduct();

    public BA_tblProduct()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private int _ProductId;
    public int ProductId
    {
        get { return _ProductId; }
        set { _ProductId = value; }
    }
    private string _ProductName;
    public string ProductName
    {
        get { return _ProductName; }
        set { _ProductName = value; }
    }
    private string _ProductDesc;
    public string ProductDesc
    {
        get { return _ProductDesc; }
        set { _ProductDesc = value; }
    }

    private long _FromScheme;
    public long FromScheme
    {
        get { return _FromScheme; }
        set { _FromScheme = value; }
    }


    private long _ToScheme;
    public long ToScheme
    {
        get { return _ToScheme; }
        set { _ToScheme = value; }
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

    public bool INSERT_tblProduct()
    {
        try
        {
            return objDA_tblProduct.INSERT_tblProduct(this);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_tblProduct(ref DataTable dt)
    {
        try
        {
            if (objDA_tblProduct.SELECT_ALL_tblProduct(this, ref dt))
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

    public bool DELETE_RECORDS_FROM_tblProduct()
    {
        try
        {
            if (objDA_tblProduct.DELETE_RECORDS_FROM_tblProduct(this))
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

    public bool GET_RECORDS_FROM_tblProduct(ref DataTable dt)
    {
        try
        {
            if (objDA_tblProduct.GET_RECORDS_FROM_tblProduct(this, ref dt))
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

    public bool CHK_RECORDS_FROM_tblProduct(ref DataTable dt)
    {
        try
        {
            if (objDA_tblProduct.CHK_RECORDS_FROM_tblProduct(this, ref dt))
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

    public bool UPDATE_tblProduct()
    {
        try
        {
            if (objDA_tblProduct.UPDATE_tblProduct(this))
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


    public bool SELECT_ALL_tblProduct_API(ref DataTable dt)
    {
        try
        {
            if (objDA_tblProduct.SELECT_ALL_tblProduct_API(ref dt))
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

    public bool SELECT_ALL_tblProduct_APINew(long Stateid, ref DataTable dt)
    {
        try
        {
            if (objDA_tblProduct.SELECT_ALL_tblProduct_APINew(Stateid, ref dt))
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


    //  *****************************************************************************
    //  You Can use following code to fillup form
    //  *****************************************************************************
    //  objBA_tblProduct.ProductId = "";
    //  objBA_tblProduct.ProductName = "";
    //  objBA_tblProduct.ProductDesc = "";
    //  objBA_tblProduct.Isdeleted = "";
    //  objBA_tblProduct.CreateBy = "";
    //  objBA_tblProduct.CreateDate = "";
    //  objBA_tblProduct.UpdateBy = "";
    //  objBA_tblProduct.UpdateDate = "";

}