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
public class DA_tblOrder : DALBase
{
    public DA_tblOrder()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool INSERT_tblOrder(BA_tblOrder objBA_tblOrder, ref int ReturnId)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[13];
            p[0] = new SqlParameter("@OrderType", objBA_tblOrder.OrderType);
            p[1] = new SqlParameter("@DealerId", objBA_tblOrder.DealerId);
            p[2] = new SqlParameter("@FreeSchemeFrom", objBA_tblOrder.FreeSchemeFrom);
            p[3] = new SqlParameter("@FreeSchemeTO", objBA_tblOrder.FreeSchemeTO);
            p[4] = new SqlParameter("@TotalKgGm", objBA_tblOrder.TotalKgGm);
            p[5] = new SqlParameter("@Transport", objBA_tblOrder.Transport);
            p[6] = new SqlParameter("@Other", objBA_tblOrder.Other);
            p[7] = new SqlParameter("@POP", objBA_tblOrder.POP);
            p[8] = new SqlParameter("@OrderStatus", objBA_tblOrder.OrderStatus);
            p[9] = new SqlParameter("@CreateBy", objBA_tblOrder.CreateBy);            
            p[10] = new SqlParameter("@xmlProd", objBA_tblOrder.xmlProd);
            p[11] = new SqlParameter("@SalesId", objBA_tblOrder.SalesId);
            p[12] = new SqlParameter("@SiteDelivery", objBA_tblOrder.SiteDelivery);
            ReturnId = Convert.ToInt32(this.Execute_Scalar("sproc_INSERT_tblOrder", p));
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

    public bool INSERT_tblOrderDealerScheme(BA_tblOrder objBA_tblOrder, ref int ReturnId)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[18];
            p[0] = new SqlParameter("@OrderType", objBA_tblOrder.OrderType);
            p[1] = new SqlParameter("@DealerId", objBA_tblOrder.DealerId);
            p[2] = new SqlParameter("@PurchaseDurationFromDate", DateTime.ParseExact(objBA_tblOrder.PurchaseDurationFromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            p[3] = new SqlParameter("@PurchaseDurationToDate", DateTime.ParseExact(objBA_tblOrder.PurchaseDurationToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            p[4] = new SqlParameter("@FreeSchemeFrom", objBA_tblOrder.FreeSchemeFrom);
            p[5] = new SqlParameter("@FreeSchemeTO", objBA_tblOrder.FreeSchemeTO);
            p[6] = new SqlParameter("@PurchaseKgs", objBA_tblOrder.PurchaseKgs);
            p[7] = new SqlParameter("@TotalKgGm", objBA_tblOrder.TotalKgGm);
            p[8] = new SqlParameter("@Transport", objBA_tblOrder.Transport);
            p[9] = new SqlParameter("@Other", objBA_tblOrder.Other);
            p[10] = new SqlParameter("@POP", objBA_tblOrder.POP);
            p[11] = new SqlParameter("@OrderStatus", objBA_tblOrder.OrderStatus);
            p[12] = new SqlParameter("@CreateBy", objBA_tblOrder.CreateBy);
            p[13] = new SqlParameter("@xmlProd", objBA_tblOrder.xmlProd);
            p[14] = new SqlParameter("@xmlFreeProd", objBA_tblOrder.xmlFreeProd);
            p[15] = new SqlParameter("@SalesId", objBA_tblOrder.SalesId);
            p[16] = new SqlParameter("@SiteDelivery", objBA_tblOrder.SiteDelivery);
            p[17] = new SqlParameter("@orderid", objBA_tblOrder.OrderID);
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

    public bool INSERT_RECORDS_FROM_tblOrderProductDetails(BA_tblOrder objBA_tblOrder, string XML)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@MyXML", XML);  
            bool returns;
            returns = Convert.ToBoolean(this.Execute_NonQuery("sproc_INSERT_tblOrderProductDetails", p));

            return returns;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool UPDATE_tblOrder(BA_tblOrder objBA_tblOrder, ref int ReturnId)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[11];
            p[0] = new SqlParameter("@OrderID", objBA_tblOrder.OrderID);        
            p[1] = new SqlParameter("@DealerId", objBA_tblOrder.DealerId);
            p[2] = new SqlParameter("@TotalKgGm", objBA_tblOrder.TotalKgGm);
            p[5] = new SqlParameter("@Transport", objBA_tblOrder.Transport);
            p[3] = new SqlParameter("@Other", objBA_tblOrder.Other);
            p[4] = new SqlParameter("@POP", objBA_tblOrder.POP);
            p[6] = new SqlParameter("@OrderStatus", objBA_tblOrder.OrderStatus);
            p[7] = new SqlParameter("@UpdateBy", objBA_tblOrder.UpdateBy);         
            p[8] = new SqlParameter("@xmlProd", objBA_tblOrder.xmlProd);
            p[9] = new SqlParameter("@SalesId", objBA_tblOrder.SalesId);
            p[10] = new SqlParameter("@SiteDelivery", objBA_tblOrder.SiteDelivery);
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

    public bool UPDATE_tblOrderDealerScheme(BA_tblOrder objBA_tblOrder, ref int ReturnId)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[16];
            p[0] = new SqlParameter("@OrderID", objBA_tblOrder.OrderID);
            p[1] = new SqlParameter("@DealerId", objBA_tblOrder.DealerId);
            p[2] = new SqlParameter("@PurchaseDurationFromDate", DateTime.ParseExact(objBA_tblOrder.PurchaseDurationFromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            p[3] = new SqlParameter("@PurchaseDurationToDate", DateTime.ParseExact(objBA_tblOrder.PurchaseDurationToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
            p[4] = new SqlParameter("@FreeSchemeFrom", objBA_tblOrder.FreeSchemeFrom);
            p[5] = new SqlParameter("@FreeSchemeTO", objBA_tblOrder.FreeSchemeTO);
            p[6] = new SqlParameter("@PurchaseKgs", objBA_tblOrder.PurchaseKgs);
            p[7] = new SqlParameter("@TotalKgGm", objBA_tblOrder.TotalKgGm);
            p[8] = new SqlParameter("@Transport", objBA_tblOrder.Transport);
            p[9] = new SqlParameter("@Other", objBA_tblOrder.Other);
            p[10] = new SqlParameter("@POP", objBA_tblOrder.POP);
            p[11] = new SqlParameter("@OrderStatus", objBA_tblOrder.OrderStatus);
            p[12] = new SqlParameter("@UpdateBy", objBA_tblOrder.UpdateBy);
            p[13] = new SqlParameter("@xmlProd", objBA_tblOrder.xmlProd);
            p[14] = new SqlParameter("@SalesId", objBA_tblOrder.SalesId);
            p[15] = new SqlParameter("@SiteDelivery", objBA_tblOrder.SiteDelivery);
            ReturnId = Convert.ToInt32(this.Execute_Scalar("sproc_UPDATE_tblOrderDealerScheme", p));
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

    public bool UPDATE_tblOrderStatus(BA_tblOrder objBA_tblOrder)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@OrderID", objBA_tblOrder.OrderID);           
            p[1] = new SqlParameter("@OrderStatus", objBA_tblOrder.OrderStatus);           
            return this.Execute_NonQuery("sproc_UPDATE_tblOrderStatus", p);            
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    
    


    public bool SELECT_ALL_tblOrder(BA_tblOrder objBA_tblOrder, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[4];
            p[0] = new SqlParameter("@OrderType", objBA_tblOrder.OrderType);
            p[1] = new SqlParameter("@UserType", objBA_tblOrder.UserType);
            p[2] = new SqlParameter("@FromDate", objBA_tblOrder.OrderFromDate);
            p[3] = new SqlParameter("@ToDate", objBA_tblOrder.OrderToDate);
            return this.Get_Records("sproc_SELECT_ALL_tblOrder", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FROM_tblOrder(BA_tblOrder objBA_tblOrder, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@OrderID", objBA_tblOrder.OrderID);
            return this.Get_Records("sproc_SELECT_tblOrder", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FROM_tblOrderByDate(BA_tblOrder objBA_tblOrder, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@OrderDate", objBA_tblOrder.OrderDate);
            return this.Get_Records("sproc_SELECT_tblOrderByDate", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool DELETE_RECORDS_FROM_tblOrder(BA_tblOrder objBA_tblOrder)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@OrderID", objBA_tblOrder.OrderID);     
            return this.Execute_NonQuery("sproc_DELETE_tblOrder", p);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_RECORDS_FROM_tblOrderByOrderId(BA_tblOrder objBA_tblOrder, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@OrderID", objBA_tblOrder.OrderID);
            return this.Get_Records("sproc_SELECT_tblOrderByOrderId", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    public bool GET_RECORDS_FROM_tblOrderByOrderIdNew(BA_tblOrder objBA_tblOrder, ref DataSet ds)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@OrderID", objBA_tblOrder.OrderID);
            return this.Get_RecordsDataset("sproc_SELECT_tblOrderByOrderId_New", p, ref ds);
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    public bool GET_RECORDS_FROM_tblOrderByOrderIdDetails(BA_tblOrder objBA_tblOrder, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@OrderID", objBA_tblOrder.OrderID);
            return this.Get_Records("sproc_SELECT_tblOrderByOrderIdDetails", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    

    public bool GET_RECORDS_FROM_PrinttblOrder(BA_tblOrder objBA_tblOrder, ref DataSet ds)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@OrderID", objBA_tblOrder.OrderID);
            return this.Get_RecordsDataset("sproc_SELECT_PrinttblOrder", p, ref ds);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public bool GET_RECORDS_FROM_PrinttblOrder_table(BA_tblOrder objBA_tblOrder, ref DataSet ds)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@OrderID", objBA_tblOrder.OrderID);
            return this.Get_RecordsDataset("sproc_SELECT_PrinttblOrderNew", p, ref ds);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public bool GET_RECORDS_FROM_tblParentOrderByOrderId(BA_tblOrder objBA_tblOrder, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@OrderID", objBA_tblOrder.OrderID);
            return this.Get_Records("sproc_SELECT_tblParentOrderByOrderId", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    public bool GET_Order_API(BA_tblOrder objBA_tblOrder, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@FromDate", objBA_tblOrder.FromDate);
            p[1] = new SqlParameter("@ToDate", objBA_tblOrder.ToDate);
            p[2] = new SqlParameter("@UserID", objBA_tblOrder.CreateBy);
                return this.Get_Records("sproc_SELECT_tblOrder_API", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_FreeOrder_API(BA_tblOrder objBA_tblOrder, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[3];
            p[0] = new SqlParameter("@FromDate", objBA_tblOrder.FromDate);
            p[1] = new SqlParameter("@ToDate", objBA_tblOrder.ToDate);
            p[2] = new SqlParameter("@UserID", objBA_tblOrder.CreateBy);
            return this.Get_Records("sproc_Free_SELECT_tblOrder_API", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    public bool GET_OrderView_API(BA_tblOrder objBA_tblOrder, ref DataSet ds)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@OrderID", objBA_tblOrder.OrderID);
            return this.Get_RecordsDataset("sproc_SELECT_tblOrderByOrderId_API", p, ref ds);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool GET_FreeOrderView_API(BA_tblOrder objBA_tblOrder, ref DataSet ds)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[1];
            p[0] = new SqlParameter("@OrderID", objBA_tblOrder.OrderID);
            return this.Get_RecordsDataset("sproc_Free_SELECT_tblOrderByOrderId_API", p, ref ds);
        }
        catch (Exception ex)
        {
            return false;
        }
    }




     public bool INSERT_tblOrder_API(BA_tblOrder objBA_tblOrder, ref int ReturnId)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[12];
            p[0] = new SqlParameter("@OrderType", objBA_tblOrder.OrderType);
            p[1] = new SqlParameter("@DealerId", objBA_tblOrder.DealerId);
            p[2] = new SqlParameter("@FreeSchemeFrom", objBA_tblOrder.FreeSchemeFrom);
            p[3] = new SqlParameter("@FreeSchemeTO", objBA_tblOrder.FreeSchemeTO);
            p[4] = new SqlParameter("@TotalKgGm", objBA_tblOrder.TotalKgGm);
            p[5] = new SqlParameter("@Transport", objBA_tblOrder.Transport);
            p[6] = new SqlParameter("@Other", objBA_tblOrder.Other);
            p[7] = new SqlParameter("@POP", objBA_tblOrder.POP);
            p[8] = new SqlParameter("@SiteDelivery", objBA_tblOrder.SiteDelivery);
            p[9] = new SqlParameter("@ParentOrderId", objBA_tblOrder.ParentOrderId);
            p[10] = new SqlParameter("@CreateBy", objBA_tblOrder.CreateBy);            
            p[11] = new SqlParameter("@xmlProd", objBA_tblOrder.xmlProd);
            
            ReturnId = Convert.ToInt32(this.Execute_Scalar("sproc_INSERT_tblOrder_API", p));
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


     public bool INSERT_tblOrder_API_New(BA_tblOrder objBA_tblOrder, ref int ReturnId)
     {
         try
         {
             SqlParameter[] p = new SqlParameter[12];
             p[0] = new SqlParameter("@OrderType", objBA_tblOrder.OrderType);
             p[1] = new SqlParameter("@DealerId", objBA_tblOrder.DealerId);
             p[2] = new SqlParameter("@FreeSchemeFrom", objBA_tblOrder.FreeSchemeFrom);
             p[3] = new SqlParameter("@FreeSchemeTO", objBA_tblOrder.FreeSchemeTO);
             p[4] = new SqlParameter("@TotalKgGm", objBA_tblOrder.TotalKgGm);
             p[5] = new SqlParameter("@Transport", objBA_tblOrder.Transport);
             p[6] = new SqlParameter("@Other", objBA_tblOrder.Other);
             p[7] = new SqlParameter("@POP", objBA_tblOrder.POP);
             p[8] = new SqlParameter("@SiteDelivery", objBA_tblOrder.SiteDelivery);
             p[9] = new SqlParameter("@ParentOrderId", objBA_tblOrder.ParentOrderId);
             p[10] = new SqlParameter("@CreateBy", objBA_tblOrder.CreateBy);
             p[11] = new SqlParameter("@xmlProd", objBA_tblOrder.xmlProd);

             ReturnId = Convert.ToInt32(this.Execute_Scalar("sproc_INSERT_tblOrder_API_New", p));
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

     public bool UPDATE_tblOrderFree(BA_tblOrder objBA_tblOrder, ref int ReturnId)
     {
         try
         {
             SqlParameter[] p = new SqlParameter[15];
             p[0] = new SqlParameter("@OrderID", objBA_tblOrder.OrderID);
             p[1] = new SqlParameter("@DealerId", objBA_tblOrder.DealerId);
             p[2] = new SqlParameter("@FreeSchemeFrom", objBA_tblOrder.FreeSchemeFrom);
             p[3] = new SqlParameter("@FreeSchemeTO", objBA_tblOrder.FreeSchemeTO);
             p[4] = new SqlParameter("@TotalKgGm", objBA_tblOrder.TotalKgGm);
             p[5] = new SqlParameter("@Transport", objBA_tblOrder.Transport);
             p[6] = new SqlParameter("@Other", objBA_tblOrder.Other);
             p[7] = new SqlParameter("@POP", objBA_tblOrder.POP);
             p[8] = new SqlParameter("@OrderStatus", objBA_tblOrder.OrderStatus);
             p[9] = new SqlParameter("@ParentOrderId", objBA_tblOrder.ParentOrderId);            
             p[10] = new SqlParameter("@UpdateBy", objBA_tblOrder.UpdateBy);
             p[11] = new SqlParameter("@xmxFreeProd", objBA_tblOrder.xmlFreeProd);
             p[12] = new SqlParameter("@TotalFreeKgGm", objBA_tblOrder.TotalFreeKgGm);
             p[13] = new SqlParameter("@SalesId", objBA_tblOrder.SalesId);
             p[14] = new SqlParameter("@SiteDelivery", objBA_tblOrder.SiteDelivery);
             ReturnId = Convert.ToInt32(this.Execute_Scalar("sproc_UPDATE_tblOrderFree", p));
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

     public bool INSERT_tblOrderFree(BA_tblOrder objBA_tblOrder, ref int ReturnId)
     {
         try
         {
             SqlParameter[] p = new SqlParameter[15];
             p[0] = new SqlParameter("@OrderType", objBA_tblOrder.OrderType);
             p[1] = new SqlParameter("@DealerId", objBA_tblOrder.DealerId);
             p[2] = new SqlParameter("@FreeSchemeFrom", objBA_tblOrder.FreeSchemeFrom);
             p[3] = new SqlParameter("@FreeSchemeTO", objBA_tblOrder.FreeSchemeTO);
             p[4] = new SqlParameter("@TotalKgGm", objBA_tblOrder.TotalKgGm);
             p[5] = new SqlParameter("@Transport", objBA_tblOrder.Transport);
             p[6] = new SqlParameter("@Other", objBA_tblOrder.Other);
             p[7] = new SqlParameter("@POP", objBA_tblOrder.POP);
             p[8] = new SqlParameter("@OrderStatus", objBA_tblOrder.OrderStatus);
             p[9] = new SqlParameter("@ParentOrderId", objBA_tblOrder.ParentOrderId);
             p[10] = new SqlParameter("@CreateBy", objBA_tblOrder.CreateBy);
             p[11] = new SqlParameter("@xmxFreeProd", objBA_tblOrder.xmlProd);
             p[12] = new SqlParameter("@TotalFreeKgGm", objBA_tblOrder.TotalFreeKgGm);
             p[13] = new SqlParameter("@SalesId", objBA_tblOrder.SalesId);
             p[14] = new SqlParameter("@SiteDelivery", objBA_tblOrder.SiteDelivery);
             ReturnId = Convert.ToInt32(this.Execute_Scalar("sproc_INSERT_tblOrderFree", p));
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


    public bool INSERT_tblOrderFreeNew(BA_tblOrder objBA_tblOrder, ref int ReturnId)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[16];
            p[0] = new SqlParameter("@OrderType", objBA_tblOrder.OrderType);
            p[1] = new SqlParameter("@DealerId", objBA_tblOrder.DealerId);
            p[2] = new SqlParameter("@FreeSchemeFrom", objBA_tblOrder.FreeSchemeFrom);
            p[3] = new SqlParameter("@FreeSchemeTO", objBA_tblOrder.FreeSchemeTO);
            p[4] = new SqlParameter("@TotalKgGm", objBA_tblOrder.TotalKgGm);
            p[5] = new SqlParameter("@Transport", objBA_tblOrder.Transport);
            p[6] = new SqlParameter("@Other", objBA_tblOrder.Other);
            p[7] = new SqlParameter("@POP", objBA_tblOrder.POP);
            p[8] = new SqlParameter("@SiteDelivery", objBA_tblOrder.SiteDelivery);
            p[9] = new SqlParameter("@OrderStatus", objBA_tblOrder.OrderStatus);
            p[10] = new SqlParameter("@ParentOrderId", objBA_tblOrder.ParentOrderId);
            p[11] = new SqlParameter("@CreateBy", objBA_tblOrder.CreateBy);
            p[12] = new SqlParameter("@xmlProd", objBA_tblOrder.xmlProd);
            p[13] = new SqlParameter("@xmlFreeProd", objBA_tblOrder.xmlFreeProd);
            p[14] = new SqlParameter("@SalesId", objBA_tblOrder.SalesId);
            p[15] = new SqlParameter("@OrderID", objBA_tblOrder.OrderID);
            
            ReturnId = Convert.ToInt32(this.Execute_Scalar("sproc_INSERT_tblOrderFreeNew", p));
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



    public bool INSERT_tblOrderDealerSchemeNew(BA_tblOrder objBA_tblOrder, ref int ReturnId)
     {
         try
         {
             SqlParameter[] p = new SqlParameter[18];
             p[0] = new SqlParameter("@OrderType", objBA_tblOrder.OrderType);
             p[1] = new SqlParameter("@DealerId", objBA_tblOrder.DealerId);
             p[2] = new SqlParameter("@PurchaseDurationFromDate", DateTime.ParseExact(objBA_tblOrder.PurchaseDurationFromDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
             p[3] = new SqlParameter("@PurchaseDurationToDate", DateTime.ParseExact(objBA_tblOrder.PurchaseDurationToDate, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
             p[4] = new SqlParameter("@FreeSchemeFrom", objBA_tblOrder.FreeSchemeFrom);
             p[5] = new SqlParameter("@FreeSchemeTO", objBA_tblOrder.FreeSchemeTO);
             p[6] = new SqlParameter("@PurchaseKgs", objBA_tblOrder.PurchaseKgs);
             p[7] = new SqlParameter("@TotalKgGm", objBA_tblOrder.TotalKgGm);
             p[8] = new SqlParameter("@Transport", objBA_tblOrder.Transport);
             p[9] = new SqlParameter("@Other", objBA_tblOrder.Other);
             p[10] = new SqlParameter("@POP", objBA_tblOrder.POP);
             p[11] = new SqlParameter("@OrderStatus", objBA_tblOrder.OrderStatus);
             p[12] = new SqlParameter("@CreateBy", objBA_tblOrder.CreateBy);
             p[13] = new SqlParameter("@xmlProd", objBA_tblOrder.xmlProd);
             p[14] = new SqlParameter("@xmlFreeProd", objBA_tblOrder.xmlFreeProd);
             p[15] = new SqlParameter("@SalesId", objBA_tblOrder.SalesId);
             p[16] = new SqlParameter("@SiteDelivery", objBA_tblOrder.SiteDelivery);
             p[17] = new SqlParameter("@orderid", objBA_tblOrder.OrderID);
             ReturnId = Convert.ToInt32(this.Execute_Scalar("sproc_INSERT_tblOrderDealerSchemeNew", p));
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


    public bool SELECT_ALL_PendingBillTally(BA_tblOrder objBA_tblOrder, ref DataTable dt)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@FromDate", objBA_tblOrder.OrderFromDate);
            p[1] = new SqlParameter("@ToDate", objBA_tblOrder.OrderToDate);
            return this.Get_Records("sproc_SELECT_ALL_PendingBillTally", p, ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }


    public bool INSERT_tblOrderNew(BA_tblOrder objBA_tblOrder, ref int ReturnId)
    {
        try
        {
            SqlParameter[] p = new SqlParameter[12];
            p[0] = new SqlParameter("@OrderType", objBA_tblOrder.OrderType);
            p[1] = new SqlParameter("@DealerId", objBA_tblOrder.DealerId);
            p[2] = new SqlParameter("@Transport", objBA_tblOrder.Transport);
            p[3] = new SqlParameter("@Other", objBA_tblOrder.Other);
            p[4] = new SqlParameter("@POP", objBA_tblOrder.POP);
            p[5] = new SqlParameter("@SiteDelivery", objBA_tblOrder.SiteDelivery);
            p[6] = new SqlParameter("@OrderStatus", objBA_tblOrder.OrderStatus);
            p[7] = new SqlParameter("@TotalKgGm", objBA_tblOrder.TotalKgGm);
            p[8] = new SqlParameter("@CreateBy", objBA_tblOrder.CreateBy);
            p[9] = new SqlParameter("@xmlProd", objBA_tblOrder.xmlProd);
            p[10] = new SqlParameter("@SalesId", objBA_tblOrder.SalesId);
            p[11] = new SqlParameter("@orderid", objBA_tblOrder.OrderID);
            ReturnId = Convert.ToInt32(this.Execute_Scalar("sproc_INSERT_tblOrderNew", p));
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

    public bool SELECT_ALL_tblOrder_orderstatus_Factory(BA_tblOrder objBA_tblOrder, ref DataTable dt)
    {
        try
        {

            return this.Get_Records("sproc_SELECT_ALL_tblOrder_orderstatus_Factory", ref dt);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}