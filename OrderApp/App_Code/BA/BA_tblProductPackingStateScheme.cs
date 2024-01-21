using System;
using System.Data;

public class BA_tblProductPackingStateScheme
{
    DA_tblProductPackingStateScheme objDA_tblProductPackingStateScheme = new DA_tblProductPackingStateScheme();

    public BA_tblProductPackingStateScheme() { }

    private long _SrNo;
    public long SrNo
    {
        get { return _SrNo; }
        set { _SrNo = value; }
    }
    private long _ProductPckID;
    public long ProductPckID
    {
        get { return _ProductPckID; }
        set { _ProductPckID = value; }
    }
    private long _state_id;
    public long state_id
    {
        get { return _state_id; }
        set { _state_id = value; }
    }
    private long _SchemeIdData;
    public long SchemeIdData
    {
        get { return _SchemeIdData; }
        set { _SchemeIdData = value; }
    }
    private string _SchemeProductCode;
    public string SchemeProductCode
    {
        get { return _SchemeProductCode; }
        set { _SchemeProductCode = value; }
    }
    private DateTime _created_date;
    public DateTime created_date
    {
        get { return _created_date; }
        set { _created_date = value; }
    }
    private DateTime _modify_date;
    public DateTime modify_date
    {
        get { return _modify_date; }
        set { _modify_date = value; }
    }
    private long _created_by;
    public long created_by
    {
        get { return _created_by; }
        set { _created_by = value; }
    }
    private long _modify_by;
    public long modify_by
    {
        get { return _modify_by; }
        set { _modify_by = value; }
    }
    private bool _is_del;
    public bool is_del
    {
        get { return _is_del; }
        set { _is_del = value; }
    }

    public bool INSERT_tblProductPackingStateScheme()
    {
        try
        {
            return objDA_tblProductPackingStateScheme.INSERT_tblProductPackingStateScheme(this);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_tblProductPackingStateScheme(long StateId,ref DataTable dt)
    {
        try
        {
            if (objDA_tblProductPackingStateScheme.SELECT_ALL_tblProductPackingStateScheme(StateId,ref dt))
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

    public bool DELETE_RECORDS_FROM_tblProductPackingStateScheme()
    {
        try
        {
            if (objDA_tblProductPackingStateScheme.DELETE_RECORDS_FROM_tblProductPackingStateScheme(this))
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

    public bool GET_RECORDS_FROM_tblProductPackingStateScheme(ref DataTable dt)
    {
        try
        {
            if (objDA_tblProductPackingStateScheme.GET_RECORDS_FROM_tblProductPackingStateScheme(this, ref dt))
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

    public bool GET_RECORDS_FROM_tblProductPackingStateScheme_By_ProdPackId(ref DataTable dt)
    {
        try
        {
            if (objDA_tblProductPackingStateScheme.GET_RECORDS_FROM_tblProductPackingStateScheme_By_ProdPackId(this, ref dt))
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool CHECK_EXISTING_tblProductPackingStateScheme(ref DataTable dt)
    {
        try
        {
            if (objDA_tblProductPackingStateScheme.CHECK_EXISTING_tblProductPackingStateScheme(this, ref dt))
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool UPDATE_tblProductPackingStateScheme()
    {
        try
        {
            if (objDA_tblProductPackingStateScheme.UPDATE_tblProductPackingStateScheme(this))
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


    public bool GetProductPackingStateScheme(ref DataTable dt)
    {
        try
        {
            if (objDA_tblProductPackingStateScheme.GetProductPackingStateScheme(this, ref dt))
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
    //  objBA_tblProductPackingStateScheme.SrNo = "";
    //  objBA_tblProductPackingStateScheme.ProductPckID = "";
    //  objBA_tblProductPackingStateScheme.state_id = "";
    //  objBA_tblProductPackingStateScheme.SchemeIdData = "";
    //  objBA_tblProductPackingStateScheme.SchemeProductCode = "";
    //  objBA_tblProductPackingStateScheme.created_date = "";
    //  objBA_tblProductPackingStateScheme.modify_date = "";
    //  objBA_tblProductPackingStateScheme.created_by = "";
    //  objBA_tblProductPackingStateScheme.modify_by = "";
    //  objBA_tblProductPackingStateScheme.is_del = "";

}