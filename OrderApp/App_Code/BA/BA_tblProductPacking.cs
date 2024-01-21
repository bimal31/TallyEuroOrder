using System;
using System.Data;

public class BA_tblProductPacking
{
    DA_tblProductPacking objDA_tblProductPacking = new DA_tblProductPacking();

    public BA_tblProductPacking() { }

    private string _ProductPckID;
    public string ProductPckID
    {
        get { return _ProductPckID; }
        set { _ProductPckID = value; }
    }
    private string _ProductID;
    public string ProductID
    {
        get { return _ProductID; }
        set { _ProductID = value; }
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
    private string _ProductPckDetails;
    public string ProductPckDetails
    {
        get { return _ProductPckDetails; }
        set { _ProductPckDetails = value; }
    }
    private string _PackingType;
    public string PackingType
    {
        get { return _PackingType; }
        set { _PackingType = value; }
    }
    private decimal _TotalKG;
    public decimal TotalKG
    {
        get { return _TotalKG; }
        set { _TotalKG = value; }
    }
    private Boolean _IsScheme;
    public Boolean IsScheme
    {
        get { return _IsScheme; }
        set { _IsScheme = value; }
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

    private string _UOM;
    public string UOM
    {
        get { return _UOM; }
        set { _UOM = value; }
    }

    private string _xmlData;
    public string XmlData { get { return _xmlData; } set { _xmlData = value; } }

    public bool INSERT_tblProductPacking()
    {
        try
        {
            return objDA_tblProductPacking.INSERT_tblProductPacking(this);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_tblProductPacking(ref DataTable dt)
    {
        try
        {
            if (objDA_tblProductPacking.SELECT_ALL_tblProductPacking(this,ref dt))
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

    public bool DELETE_RECORDS_FROM_tblProductPacking()
    {
        try
        {
            if (objDA_tblProductPacking.DELETE_RECORDS_FROM_tblProductPacking(this))
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

    public bool GET_RECORDS_FROM_tblProductPacking(ref DataTable dt)
    {
        try
        {
            if (objDA_tblProductPacking.GET_RECORDS_FROM_tblProductPacking(this, ref dt))
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

    public bool GET_RECORDS_FROM_tblProductById(ref DataTable dt)
    {
        try
        {
            if (objDA_tblProductPacking.GET_RECORDS_FROM_tblProductById(this, ref dt))
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

    public bool UPDATE_tblProductPacking()
    {
        try
        {
            if (objDA_tblProductPacking.UPDATE_tblProductPacking(this))
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


    public bool GET_RECORDS_FROM_tblProductPacking_API(ref DataTable dt)
    {
        try
        {
            if (objDA_tblProductPacking.GET_RECORDS_FROM_tblProductPacking_API(this, ref dt))
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

    public bool GET_RECORDS_FROM_tblProductPacking_APINew(long ProductID,long StateId, ref DataTable dt)
    {
        try
        {
            if (objDA_tblProductPacking.GET_RECORDS_FROM_tblProductPacking_APINew(ProductID, StateId, ref dt))
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
    //  objBA_tblProductPacking.ProductPckID = "";
    //  objBA_tblProductPacking.ProductID = "";
    //  objBA_tblProductPacking.ProductPck = "";
    //  objBA_tblProductPacking.PackingNos = "";
    //  objBA_tblProductPacking.ProductPckDetails = "";
    //  objBA_tblProductPacking.PackingType = "";
    //  objBA_tblProductPacking.IsScheme = "";
    //  objBA_tblProductPacking.Isdeleted = "";
    //  objBA_tblProductPacking.CreateBy = "";
    //  objBA_tblProductPacking.CreateDate = "";
    //  objBA_tblProductPacking.UpdateBy = "";
    //  objBA_tblProductPacking.UpdateDate = "";

}