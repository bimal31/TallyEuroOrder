using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using OrderApp.App_Code;

namespace OrderApp
{
    public partial class RptPrintOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["q"] != null)
                {
                    ReportViewer ReportViewer1 = new ReportViewer();
                    LocalReport localReport = new LocalReport();

                    string strKey = Convert.ToString(Request.QueryString["q"]);
                    Common cmn = new Common();
                    strKey = cmn.Decrypt(strKey);
                    Int32 OrderId = Convert.ToInt32(strKey);

                    BA_tblOrder ObjOrder = new BA_tblOrder();
                    DataSet _ds = new DataSet();
                    ObjOrder.OrderID = Convert.ToString(OrderId);
                    ObjOrder.GET_RECORDS_FROM_PrinttblOrder_Table(ref _ds);

                    if (_ds.Tables.Count > 0)
                    {

                        ReportViewer1.ProcessingMode = ProcessingMode.Local;
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("rptPrintOrder2.rdlc");
                        ReportDataSource datasource = new ReportDataSource("dsOrderDetails", _ds.Tables[0]);

                        decimal totalProduct1 = 0, totalProduct2 = 0, totalProduct3 = 0, totalProduct4 = 0, totalProduct5 = 0, totalProduct6 = 0, totalProduct7 = 0, totalProduct8 = 0; 

                        #region Product 1
                        DataTable dtEuroXTRA = new DataTable();
                        dtEuroXTRA.Clear();
                        dtEuroXTRA.Columns.Add("productName");
                        dtEuroXTRA.Columns.Add("ProductPackType");
                        dtEuroXTRA.Columns.Add("TotalKg");
                        dtEuroXTRA.Columns.Add("TKg");
                        dtEuroXTRA.Columns.Add("IsScheme");
                        dtEuroXTRA.Columns.Add("Scheme");
                        DataRow _row; // = dtEuroXTRA.NewRow();
                        if (_ds.Tables[1] != null && _ds.Tables[1].Rows.Count > 0)
                        {
                            for (int i = 0; i < _ds.Tables[1].Rows.Count; i++)
                            {
                                
                                _row = dtEuroXTRA.NewRow();
                                _row["productName"] = _ds.Tables[1].Rows[i]["productName"];
                                _row["ProductPackType"] = _ds.Tables[1].Rows[i]["ProductPackType"];
                                _row["TotalKg"] = _ds.Tables[1].Rows[i]["TotalKg"];
                                _row["TKg"] = _ds.Tables[1].Rows[i]["TKg"];
                                _row["IsScheme"] = _ds.Tables[1].Rows[i]["IsScheme"];
                                _row["Scheme"] = _ds.Tables[1].Rows[i]["Scheme"];

                                dtEuroXTRA.Rows.Add(_row);
                                totalProduct1 += Convert.ToDecimal(_ds.Tables[1].Rows[i]["Tkg"]);
                              
                                //if (Convert.ToBoolean(_ds.Tables[1].Rows[i]["IsScheme"]))
                                //{
                                //    _row = dtEuroXTRA.NewRow();
                                //    _row["ProductPackType"] = "Scheme";
                                //    _row["TotalKg"] = _ds.Tables[1].Rows[i]["Scheme"];
                                //    dtEuroXTRA.Rows.Add(_row);
                                //}

                            }
                        }

                         _row = dtEuroXTRA.NewRow();
                         _row["productName"] = "";
                         _row["ProductPackType"] = "";
                         _row["TotalKg"] = "";
                         _row["TKg"] = totalProduct1;
                         _row["IsScheme"] = "";
                         _row["Scheme"] = "Total";
                         dtEuroXTRA.Rows.Add(_row);
                        #endregion


                         localReport.ReportPath = Server.MapPath("~/rptPrintOrder2.rdlc");
                         ReportDataSource datasource1 = new ReportDataSource("dsEuroXTRA", dtEuroXTRA);
                        //ReportDataSource datasource2 = new ReportDataSource("dsEuroWP", dtEuroWP);
                        //ReportDataSource datasource3 = new ReportDataSource("dsEuro2in1", dtEuro2in1);
                        //ReportDataSource datasource4 = new ReportDataSource("dsExtreme3", dtExtreme3);
                        //ReportDataSource datasource5 = new ReportDataSource("dsEuroULTRA", dtEuroULTRA);
                        //ReportDataSource datasource6 = new ReportDataSource("dsPVCGLUE", dtPVCGLUE);
                        //ReportDataSource datasource7 = new ReportDataSource("dsWoodStrong", dtWoodStrong);
                        //ReportDataSource datasource8 = new ReportDataSource("dsEuroEWR", dtEuroEWR);

                       
                        localReport.DataSources.Clear();
                        localReport.DataSources.Add(datasource);
                        localReport.DataSources.Add(datasource1);
                        //localReport.DataSources.Add(datasource2);
                        //localReport.DataSources.Add(datasource3);
                        //localReport.DataSources.Add(datasource4);
                        //localReport.DataSources.Add(datasource5);
                        //localReport.DataSources.Add(datasource6);
                        //localReport.DataSources.Add(datasource7);
                        //localReport.DataSources.Add(datasource8);
                        if (_ds.Tables.Count == 3)
                        {
                            foreach (DataColumn dc in _ds.Tables[2].Columns) // trim column names
                            {
                                dc.ColumnName = dc.ColumnName.Replace(" ", "");
                            }
                            ReportDataSource datasource9 = new ReportDataSource("dtFreeScheme", _ds.Tables[2]);
                            localReport.DataSources.Add(datasource9);
                        }
                        

                        


                        string reportType = "pdf";
                        string mimeType;
                        string encoding;
                        string fileNameExtension;
                        string deviceInfo = "";
                        int sal = 120511;
                        deviceInfo = "<DeviceInfo>" +
                                             "  <OutputFormat>" + sal + "</OutputFormat>" +
                                             "</DeviceInfo>";

                        Warning[] warnings;
                        string[] streams;
                        byte[] renderedBytes;
                        //Render the report       

                        renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
                        // return File(renderedBytes, mimeType);


                        //string strPrint = "var mywindow = window.open('', 'PRINT', 'height=1000,width=1000');";

                        //strPrint += "mywindow.document.write('<html><head><title>' + document.title  + '</title>');";
                        //strPrint += "mywindow.document.write('</head><body >');";
                        //strPrint += "mywindow.document.write('<h1>' + document.title  + '</h1>');";
                        //strPrint += "mywindow.document.write(renderedBytes);";
                        //strPrint += "mywindow.document.write('</body></html>');";
                        //strPrint += "mywindow.document.close();";
                        //strPrint += "mywindow.focus();";
                        //strPrint += "mywindow.print();";
                        //strPrint += "mywindow.close();";

                        string file_name = Request.QueryString["file"];
                        string path = Server.MapPath("Print_Files/" + file_name);

                        // Open PDF File in Web Browser 

                        WebClient client = new WebClient();
                        Byte[] buffer = renderedBytes;
                        if (buffer != null)
                        {
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("content-length", buffer.Length.ToString());
                            Response.BinaryWrite(buffer);
                        }

                        //  PrintReport.Export(report, true);
                        //  ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "javascript: " + strPrint, true);
                        //   Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "PrintReport(); ", true);
                    }
                }
            }
        }
    }
}