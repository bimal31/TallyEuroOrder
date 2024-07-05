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
public class BA_tblBatch
{
    DA_tblBatch ObjDA_tblBatch = new DA_tblBatch();

    public BA_tblBatch()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private long _Srno;
    public long Srno
    {
        get { return _Srno; }
        set { _Srno = value; }
    }

    private long _BatchNo;
    public long BatchNo
    {
        get { return _BatchNo; }
        set { _BatchNo = value; }
    }

    private SqlDateTime? _BatachDate;
    public SqlDateTime? BatachDate
    {
        get { return _BatachDate; }
        set { _BatachDate = value; }
    }
   
    private string _BatchRemark;
    public string BatchRemark
    {
        get { return _BatchRemark; }
        set { _BatchRemark = value; }
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

    private string _BatchStatus;
    public string BatchStatus
    {
        get { return _BatchStatus; }
        set { _BatchStatus = value; }
    }
    private string _Batchorder;
    public string Batchorder
    {
        get { return _Batchorder; }
        set { _Batchorder = value; }
    }

    private decimal _Totalkg;
    public decimal Totalkg
    {
        get { return _Totalkg; }
        set { _Totalkg = value; }
    }

    public bool INSERT_tblBatch(ref int ReturnId)
    {
        try
        {
            return ObjDA_tblBatch.INSERT_tblBatch(this, ref ReturnId);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool INSERT_tblBatchorderid(string orderid, ref int ReturnId)
    {
        try
        {
            return ObjDA_tblBatch.INSERT_tblBatchorderid(orderid, this, ref ReturnId);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_tblBatch(string FromBatachDate , string ToBatachDate, ref DataTable dt)
    {
        try
        {
            if (ObjDA_tblBatch.SELECT_ALL_tblBatch(FromBatachDate, ToBatachDate, ref dt))
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

   

    public bool GET_RECORDS_FROM_tblBatch(ref DataTable dt)
    {
        try
        {
            if (ObjDA_tblBatch.GET_RECORDS_FROM_tblBatch(this, ref dt))
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

   

    public bool UPDATE_tblBatch(ref int ReturnId)
    {
        try
        {
            if (ObjDA_tblBatch.UPDATE_tblBatch(this, ref ReturnId))
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

   

    public bool UPDATE_tblBatchStatus()
    {
        try
        {
            if (ObjDA_tblBatch.UPDATE_tblBatchStatus(this))
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


    public bool GET_RECORDS_FROM_tblBatchOrderPrint(long Batchsrno, ref DataSet ds)
    {
        try
        {
            if (ObjDA_tblBatch.GET_RECORDS_FROM_tblBatchOrderPrint(Batchsrno, ref ds))
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