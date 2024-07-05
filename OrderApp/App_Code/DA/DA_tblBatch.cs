using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

/// <summary>
/// Summary description for DA_tblOrder
/// <summary>
public class DA_tblBatch : DALBase
{
    public DA_tblBatch()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool INSERT_tblBatch(BA_tblBatch objBA_tblBatch, ref int ReturnId)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[4];
            
            p[0] = new SqlParameter("@Batchorder", objBA_tblBatch.Batchorder);
            p[1] = new SqlParameter("@BatchRemark", objBA_tblBatch.BatchRemark);
            p[2] = new SqlParameter("@CreateBy", objBA_tblBatch.CreateBy);
            p[3] = new SqlParameter("@Totalkg", objBA_tblBatch.Totalkg);
            ReturnId = Convert.ToInt32(this.Execute_Scalar("sproc_INSERT_tblBatch", p));
            if (ReturnId > 0)
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

    public bool INSERT_tblBatchorderid(string Orderid, BA_tblBatch objBA_tblBatch, ref int ReturnId)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@Srno", objBA_tblBatch.Srno);
            p[1] = new SqlParameter("@BatchNo", objBA_tblBatch.BatchNo);
            p[2] = new SqlParameter("@Orderid", Orderid);
            ReturnId = Convert.ToInt32(this.Execute_Scalar("sproc_INSERT_tblOrderDealerScheme", p));
            if (ReturnId > 0)
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

  

    public bool UPDATE_tblBatch(BA_tblBatch objBA_tblBatch, ref int ReturnId)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@BatchNo", objBA_tblBatch.BatchNo);
            p[1] = new SqlParameter("@BatachDate", objBA_tblBatch.BatachDate);
            p[2] = new SqlParameter("@BatchRemark", objBA_tblBatch.BatchRemark);
       
            ReturnId = Convert.ToInt32(this.Execute_Scalar("sproc_UPDATE_tblOrder", p));
            if (ReturnId > 0)
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

    public bool UPDATE_tblBatchStatus(BA_tblBatch objBA_tblBatch)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@Srno", objBA_tblBatch.Srno);           
            p[1] = new SqlParameter("@BatchStatus", objBA_tblBatch.BatchStatus);           
            return this.Execute_NonQuery("sproc_UPDATE_tblBatchStatus", p);            
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool SELECT_ALL_tblBatch(string FromBatachDate, string ToBatachDate,  ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@FromDate", FromBatachDate);
            p[1] = new SqlParameter("@ToDate", ToBatachDate);
            return this.Get_Records("sproc_SELECT_ALL_tblbatch", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FROM_tblBatch(BA_tblBatch objBA_tblBatch, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@Srno", objBA_tblBatch.Srno);
            return this.Get_Records("sproc_SELECT_tblOrder", p, ref dt);
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
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@batchsrno", Batchsrno);
            return this.Get_RecordsDataset("Getuserwisedailyreport", p, ref ds);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

}