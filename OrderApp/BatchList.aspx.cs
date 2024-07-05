using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class BatchList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    txtFromDate.Attributes.Add("readonly", "readonly");
                    txtToDate.Attributes.Add("readonly", "readonly");

                    if (txtFromDate.Text == "")
                    {
                        txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-GB"));

                    }

                    if (txtToDate.Text == "")
                    {
                        txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en-GB"));
                    }

                    GetBatchList();
                    if (Convert.ToString(Session["UserType"]) == "Factory")
                    {
                        btnAdd.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("AddBatch.aspx", false);
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }
        protected void GetBatchList()
        {
            try
            {
                DateTime now = DateTime.Now;

                DataTable dt = new DataTable();
                BA_tblBatch objBA_tblBatch = new BA_tblBatch();
                var BatchFromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");
                var BatchToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture).ToString("MM/dd/yyyy");

                objBA_tblBatch.SELECT_ALL_tblBatch(BatchFromDate, BatchToDate, ref dt);

                Session["dtOrder"] = dt;

                grdbatch.DataSource = dt;
                grdbatch.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdbatch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                DataTable dtOrder = new DataTable();
                dtOrder = Session["dtOrder"] as DataTable;

                grdbatch.PageIndex = e.NewPageIndex;
                grdbatch.DataSource = dtOrder;
                grdbatch.DataBind();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetBatchList();
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }


        protected void grdbatch_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                GridViewRow gvRow = (GridViewRow)e.Row;

                HiddenField hdBatchStatus = (HiddenField)gvRow.FindControl("hdBatchStatus");
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList drpbatchStatus = (DropDownList)gvRow.FindControl("drpbatchStatus");
                    string UserType = "";
                    UserType = Convert.ToString(HttpContext.Current.Session["UserType"]);

                    drpbatchStatus.Items.Clear();
                    drpbatchStatus.Items.Add(new ListItem("Pending", "Pending"));
                    drpbatchStatus.Items.Add(new ListItem("Close", "Close"));
                    drpbatchStatus.Items.Add(new ListItem("Cancel", "Cancel"));

                    drpbatchStatus.SelectedValue = hdBatchStatus.Value;

                    if (drpbatchStatus.SelectedValue == "Cancel")
                    {
                        drpbatchStatus.Enabled = false;
                    }

                }


            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }


        protected void drpbatchStatuss_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DropDownList ddl = (DropDownList)sender;
                GridViewRow row = (GridViewRow)ddl.Parent.Parent;
                int idx = row.RowIndex;

                HiddenField hdbatchsrnoListId = row.Cells[idx].FindControl("hdbatchsrnoListId") as HiddenField;
                DropDownList drpbatchStatus = row.Cells[idx].FindControl("drpbatchStatus") as DropDownList;
                Int32 batchsrno = Convert.ToInt32(hdbatchsrnoListId.Value);

                BA_tblBatch ObjBA_tblBatch = new BA_tblBatch();
                ObjBA_tblBatch.Srno = batchsrno;
                ObjBA_tblBatch.BatchStatus = drpbatchStatus.SelectedValue;

                bool output;
                output = ObjBA_tblBatch.UPDATE_tblBatchStatus();

                if (output == true)
                {
                    GetBatchList();
                }
                else
                {
                    lblErrorMessage.Text = CommMessage.coludnotchangestatus;
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void grdbatchr_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "PrintValue")
                {
                    long batchsrno = Convert.ToInt32(e.CommandArgument);
                    DataSet ds = new DataSet();
                    BA_tblBatch ObjBA_tblBatch = new BA_tblBatch();
                    ObjBA_tblBatch.GET_RECORDS_FROM_tblBatchOrderPrint(batchsrno, ref ds);
                    calldataprintBothSheet(ds);
                }

            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        public void calldataprintBothSheet(DataSet ds)
        {
            Decimal TotalKgDealer = 0;

            decimal TotalKgXTRABox = 0;
            decimal TotalKgXTRANos = 0;

            decimal TotalKgWPBox = 0;
            decimal TotalKgWPNos = 0;

            decimal TotalKgE3Box = 0;
            decimal TotalKgE3Nos = 0;

            decimal TotalKgULTRABox = 0;
            decimal TotalKgULTRANos = 0;

            decimal TotalKgPVCBox = 0;
            decimal TotalKgPVCNos = 0;

            decimal TotalKgWoodBox = 0;
            decimal TotalKgWoodNos = 0;

            decimal TotalKgEWRBox = 0;
            decimal TotalKgEWRNos = 0;

            decimal TotalKgJHIBox = 0;
            decimal TotalKgJHINos = 0;

            decimal TotalKgJECOGRADEBox = 0;
            decimal TotalKgJECOGRADENos = 0;

            decimal TotalKgJEWRMAXBox = 0;
            decimal TotalKgJEWRMAXNos = 0;

            decimal TotalKgJEWRE3Box = 0;
            decimal TotalKgJEWRE3Nos = 0;


            int countrow = 4;
            try
            {
                var workbook = new XLWorkbook();
                foreach (var wsNum in Enumerable.Range(1, 1))
                {
                    #region Dealer data
                    var ws = workbook.Worksheets.Add("DealerSheet");
                    ws.Cell("A1").Value = " ";
                    var rangespaceupper = ws.Range("A1", "I1");
                    rangespaceupper.Merge().Style.Font.SetBold().Font.FontSize = 10;

                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        ws.Cell("A2").Value = "ORDER SUMMARY";
                        var range = ws.Range("A2:C2");
                        range.Merge().Style.Font.SetBold().Font.FontSize = 12;
                        range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                        DataTable dtbatch = ds.Tables[0];
                        if (dtbatch != null && dtbatch.Rows.Count > 0)
                        {
                            ws.Cell("D2").Value = "TOTAL KG - " + Convert.ToString(dtbatch.Rows[0]["Totalkg"]);
                            var range1 = ws.Range("D2:F2");
                            range1.Merge().Style.Font.SetBold().Font.FontSize = 12;
                            range1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            range1.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                            ws.Cell("G2").Value = "DATE - " + Convert.ToString(dtbatch.Rows[0]["BatachDate"]);
                            var range2 = ws.Range("G2:I2");
                            range2.Merge().Style.Font.SetBold().Font.FontSize = 12;
                            range2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            range2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        }
                        DataTable dtdealerList = ds.Tables[1];
                        int widhtvalue = 10;
                        var colA = ws.Column("A");
                        colA.Width = widhtvalue;
                        var colB = ws.Column("B");
                        colB.Width = widhtvalue;
                        var colC = ws.Column("C");
                        colC.Width = widhtvalue + 8;
                        var colD = ws.Column("D");
                        colD.Width = widhtvalue + 8;
                        var colE = ws.Column("E");
                        colE.Width = widhtvalue;
                        var colF = ws.Column("F");
                        colF.Width = widhtvalue;
                        var colG = ws.Column("G");
                        colG.Width = widhtvalue;
                        var colH = ws.Column("H");
                        colH.Width = widhtvalue;
                        var colI = ws.Column("I");
                        colI.Width = widhtvalue;

                        ws.Cell("A3").Value = "SR.NO";
                        ws.Cell("A3").Style.Font.SetBold();
                        ws.Range("A3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                        ws.Cell("B3").Value = "DATE";
                        ws.Cell("B3").Style.Font.SetBold();
                        ws.Range("B3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                        ws.Cell("C3").Value = "AREA";
                        ws.Cell("C3").Style.Font.SetBold();
                        ws.Range("C3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws.Cell("D3").Value = "PARTY CODE";
                        ws.Cell("D3").Style.Font.SetBold();
                        ws.Range("D3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                        ws.Cell("E3").Value = "PARTY NAME";
                        var rangep1 = ws.Range("E3:H3");
                        rangep1.Merge().Style.Font.SetBold().Font.FontSize = 12;
                        rangep1.Merge().Style.Alignment.WrapText = true;
                        rangep1.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        rangep1.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;



                        ws.Cell("I3").Value = "KG";
                        ws.Cell("I3").Style.Font.SetBold();
                        ws.Range("I3").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;



                        if (dtdealerList != null && dtdealerList.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtdealerList.Rows.Count; i++)
                            {
                                ws.Cell("A" + countrow).Value = Convert.ToString(dtdealerList.Rows[i]["Rank_no"]);
                                ws.Cell("A" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                ws.Cell("B" + countrow).Value = Convert.ToString(dtdealerList.Rows[i]["orderdate"]);
                                ws.Cell("B" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                ws.Cell("C" + countrow).Value = Convert.ToString(dtdealerList.Rows[i]["Area"]);
                                ws.Cell("C" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                ws.Cell("D" + countrow).Value = Convert.ToString(dtdealerList.Rows[i]["DealerCode"]);
                                ws.Cell("D" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                                ws.Cell("E" + countrow).Value = Convert.ToString(dtdealerList.Rows[i]["DealerName"]);
                                var rangeE = ws.Range("E" + countrow + ":H" + countrow);
                                rangeE.Merge().Style.Font.FontSize = 12;
                                rangeE.Merge().Style.Alignment.WrapText = true;
                                rangeE.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                                rangeE.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                                ws.Cell("I" + countrow).Value = Convert.ToString(dtdealerList.Rows[i]["TotalKgGm"]);
                                ws.Cell("I" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                TotalKgDealer = TotalKgDealer + Convert.ToDecimal(dtdealerList.Rows[i]["TotalKgGm"]);
                                countrow = countrow + 1;
                            }
                        }

                        ws.Cell("A" + countrow).Value = "Total";
                        ws.Cell("A" + countrow).Style.Font.SetBold();
                        ws.Cell("A" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws.Cell("B" + countrow).Value = "";
                        ws.Cell("B" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws.Cell("C" + countrow).Value = "";
                        ws.Cell("C" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws.Cell("D" + countrow).Value = "";
                        ws.Cell("D" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws.Cell("E" + countrow).Value = "";
                        var rangeD = ws.Range("E" + countrow + ":H" + countrow);
                        rangeD.Merge().Style.Font.FontSize = 12;
                        rangeD.Merge().Style.Alignment.WrapText = true;
                        rangeD.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        rangeD.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws.Cell("I" + countrow).Value = Convert.ToString(TotalKgDealer);
                        ws.Cell("I" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        ws.Cell("I" + countrow).Style.Font.SetBold();
                    }
                    #endregion

                    #region qty shhet 2 
                    var ws2 = workbook.Worksheets.Add("OrderSheet");
                    countrow = 1;
                    if (ds != null)
                    {

                        ws2.Cell("A1").Value = " ";
                        var rangespaceupper1 = ws2.Range("A1", "U1");
                        rangespaceupper.Merge().Style.Font.SetBold().Font.FontSize = 10;
                        DataTable product = ds.Tables[2];
                        ws2.Cell("A" + countrow).Value = " ";
                        string rangextraspace = "A" + countrow;
                        string rangextraspace1 = "U" + countrow;
                        var rangespace = ws2.Range(rangextraspace, rangextraspace1);
                        rangespace.Merge().Style.Font.SetBold().Font.FontSize = 10;
                        rangespace.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        countrow = countrow + 1;

                        ws2.Cell("A" + countrow).Value = "PRODUCT &";
                        ws2.Cell("A" + countrow).Style.Font.SetBold();
                        ws2.Cell("A" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("B" + countrow).Value = "XTRA";
                        string rangextraB = "B" + countrow;
                        string rangextraC = "C" + countrow;
                        var rangextra = ws2.Range(rangextraB, rangextraC);
                        rangextra.Merge().Style.Font.SetBold().Font.FontSize = 10;
                        rangextra.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        rangextra.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("D" + countrow).Value = "WP";
                        string rangextraD = "D" + countrow;
                        string rangextraE = "E" + countrow;
                        var rangextraWP = ws2.Range(rangextraD, rangextraE);
                        rangextraWP.Merge().Style.Font.SetBold().Font.FontSize = 10;
                        rangextraWP.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        rangextraWP.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("F" + countrow).Value = "E3";
                        string rangextraF = "F" + countrow;
                        string rangextraG = "G" + countrow;
                        var rangextraE3 = ws2.Range(rangextraF, rangextraG);
                        rangextraE3.Merge().Style.Font.SetBold().Font.FontSize = 10;
                        rangextraE3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        rangextraE3.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                        ws2.Cell("H" + countrow).Value = "ULTRA";
                        string rangextraH = "H" + countrow;
                        string rangextraI = "I" + countrow;
                        var rangextraULTRA3 = ws2.Range(rangextraH, rangextraI);
                        rangextraULTRA3.Merge().Style.Font.SetBold().Font.FontSize = 10;
                        rangextraULTRA3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        rangextraULTRA3.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("J" + countrow).Value = "PVC GLUE";
                        string rangextraJ = "J" + countrow;
                        string rangextraK = "K" + countrow;
                        var rangextraPVCGLU = ws2.Range(rangextraJ, rangextraK);
                        rangextraPVCGLU.Merge().Style.Font.SetBold().Font.FontSize = 10;
                        rangextraPVCGLU.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        rangextraPVCGLU.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("L" + countrow).Value = "Wood STRONG";
                        string rangextraL = "L" + countrow;
                        string rangextraM = "M" + countrow;
                        var rangextrawoodSTRONG = ws2.Range(rangextraL, rangextraM);
                        rangextrawoodSTRONG.Merge().Style.Font.SetBold().Font.FontSize = 10;
                        rangextrawoodSTRONG.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        rangextrawoodSTRONG.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        //ws2.Cell("N" + countrow).Value = "EWR";
                        //string rangextraN = "N" + countrow;
                        //string rangextraO = "O" + countrow;
                        //var rangextraEWR = ws2.Range(rangextraN, rangextraO);
                        //rangextraEWR.Merge().Style.Font.SetBold().Font.FontSize = 10;
                        //rangextraEWR.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        //rangextraEWR.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("N" + countrow).Value = "HI Strong";
                        string rangextraP = "N" + countrow;
                        string rangextraQ = "O" + countrow;
                        var rangextraHistrong = ws2.Range(rangextraP, rangextraQ);
                        rangextraHistrong.Merge().Style.Font.SetBold().Font.FontSize = 10;
                        rangextraHistrong.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        rangextraHistrong.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("P" + countrow).Value = "EWR ECO GRADE";
                        string rangextraR = "P" + countrow;
                        string rangextraS = "Q" + countrow;
                        var rangextraEWRecograde = ws2.Range(rangextraR, rangextraS);
                        rangextraEWRecograde.Merge().Style.Font.SetBold().Font.FontSize = 10;
                        rangextraEWRecograde.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        rangextraEWRecograde.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("R" + countrow).Value = "EWR MAX";
                        string rangextraT = "R" + countrow;
                        string rangextraU = "S" + countrow;
                        var rangextraEWRmax = ws2.Range(rangextraT, rangextraU);
                        rangextraEWRmax.Merge().Style.Font.SetBold().Font.FontSize = 10;
                        rangextraEWRmax.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        rangextraEWRmax.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                        ws2.Cell("T" + countrow).Value = "EWR MAX";
                        string rangextraV = "T" + countrow;
                        string rangextraW = "U" + countrow;
                        var rangextraEWRE3 = ws2.Range(rangextraV, rangextraW);
                        rangextraEWRE3.Merge().Style.Font.SetBold().Font.FontSize = 10;
                        rangextraEWRE3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        rangextraEWRE3.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                        countrow = countrow + 1;
                        ws2.Cell("A" + countrow).Value = "PACKING";
                        ws2.Cell("A" + countrow).Style.Font.SetBold();
                        ws2.Cell("A" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("B" + countrow).Value = "BOX";
                        ws2.Cell("B" + countrow).Style.Font.SetBold();
                        ws2.Cell("B" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("C" + countrow).Value = "NOS";
                        ws2.Cell("C" + countrow).Style.Font.SetBold();
                        ws2.Cell("C" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("D" + countrow).Value = "BOX";
                        ws2.Cell("D" + countrow).Style.Font.SetBold();
                        ws2.Cell("D" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("E" + countrow).Value = "NOS";
                        ws2.Cell("E" + countrow).Style.Font.SetBold();
                        ws2.Cell("E" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("F" + countrow).Value = "BOX";
                        ws2.Cell("F" + countrow).Style.Font.SetBold();
                        ws2.Cell("F" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("G" + countrow).Value = "NOS";
                        ws2.Cell("G" + countrow).Style.Font.SetBold();
                        ws2.Cell("G" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("H" + countrow).Value = "BOX";
                        ws2.Cell("H" + countrow).Style.Font.SetBold();
                        ws2.Cell("H" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("I" + countrow).Value = "NOS";
                        ws2.Cell("I" + countrow).Style.Font.SetBold();
                        ws2.Cell("I" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("J" + countrow).Value = "BOX";
                        ws2.Cell("J" + countrow).Style.Font.SetBold();
                        ws2.Cell("J" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("K" + countrow).Value = "NOS";
                        ws2.Cell("K" + countrow).Style.Font.SetBold();
                        ws2.Cell("K" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("L" + countrow).Value = "BOX";
                        ws2.Cell("L" + countrow).Style.Font.SetBold();
                        ws2.Cell("L" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("M" + countrow).Value = "NOS";
                        ws2.Cell("M" + countrow).Style.Font.SetBold();
                        ws2.Cell("M" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("N" + countrow).Value = "BOX";
                        ws2.Cell("N" + countrow).Style.Font.SetBold();
                        ws2.Cell("N" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("O" + countrow).Value = "NOS";
                        ws2.Cell("O" + countrow).Style.Font.SetBold();
                        ws2.Cell("O" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("P" + countrow).Value = "BOX";
                        ws2.Cell("P" + countrow).Style.Font.SetBold();
                        ws2.Cell("P" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("Q" + countrow).Value = "NOS";
                        ws2.Cell("Q" + countrow).Style.Font.SetBold();
                        ws2.Cell("Q" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                        ws2.Cell("R" + countrow).Value = "BOX";
                        ws2.Cell("R" + countrow).Style.Font.SetBold();
                        ws2.Cell("R" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("S" + countrow).Value = "NOS";
                        ws2.Cell("S" + countrow).Style.Font.SetBold();
                        ws2.Cell("S" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                        ws2.Cell("T" + countrow).Value = "BOX";
                        ws2.Cell("T" + countrow).Style.Font.SetBold();
                        ws2.Cell("T" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("U" + countrow).Value = "NOS";
                        ws2.Cell("U" + countrow).Style.Font.SetBold();
                        ws2.Cell("U" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                        //ws2.Cell("V" + countrow).Value = "BOX";
                        //ws2.Cell("V" + countrow).Style.Font.SetBold();
                        //ws2.Cell("V" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        //ws2.Cell("W" + countrow).Value = "NOS";
                        //ws2.Cell("W" + countrow).Style.Font.SetBold();
                        //ws2.Cell("W" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        countrow = countrow + 1;

                        if (product != null && product.Rows.Count > 0)
                        {
                            for (int j = 0; j < product.Rows.Count; j++)
                            {

                                ws2.Cell("A" + countrow).Value = Convert.ToString(product.Rows[j]["Packing"]);
                                ws2.Cell("A" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                                ws2.Cell("B" + countrow).Value = Convert.ToString(product.Rows[j]["Euro XTRA - Box"]);
                                TotalKgXTRABox = TotalKgXTRABox + (Convert.IsDBNull(product.Rows[j]["Euro XTRA - Box"]) ? 0 : (decimal)product.Rows[j]["Euro XTRA - Box"]);
                                ws2.Cell("B" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                                ws2.Cell("C" + countrow).Value = Convert.ToString(product.Rows[j]["Euro XTRA - NO"]);
                                TotalKgXTRANos = TotalKgXTRANos + (Convert.IsDBNull(product.Rows[j]["Euro XTRA - No"]) ? 0 : (decimal)product.Rows[j]["Euro XTRA - No"]);
                                ws2.Cell("C" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                                ws2.Cell("D" + countrow).Value = Convert.ToString(product.Rows[j]["Euro WP - Box"]);
                                TotalKgWPBox = TotalKgWPBox + (Convert.IsDBNull(product.Rows[j]["Euro WP - Box"]) ? 0 : (decimal)product.Rows[j]["Euro WP - Box"]);
                                ws2.Cell("D" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                                ws2.Cell("E" + countrow).Value = Convert.ToString(product.Rows[j]["Euro WP - NO"]);
                                TotalKgWPNos = TotalKgWPNos + (Convert.IsDBNull(product.Rows[j]["Euro WP - NO"]) ? 0 : (decimal)product.Rows[j]["Euro WP - NO"]);
                                ws2.Cell("E" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                                ws2.Cell("F" + countrow).Value = Convert.ToString(product.Rows[j]["Extreme 3 - Box"]);
                                TotalKgE3Box = TotalKgE3Box + (Convert.IsDBNull(product.Rows[j]["Extreme 3 - Box"]) ? 0 : (decimal)product.Rows[j]["Extreme 3 - Box"]);
                                ws2.Cell("F" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                                ws2.Cell("G" + countrow).Value = Convert.ToString(product.Rows[j]["Extreme 3 - NO"]);
                                ws2.Cell("G" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                TotalKgE3Nos = TotalKgE3Nos + (Convert.IsDBNull(product.Rows[j]["Extreme 3 - NO"]) ? 0 : (decimal)product.Rows[j]["Extreme 3 - NO"]);


                                ws2.Cell("H" + countrow).Value = Convert.ToString(product.Rows[j]["Euro ULTRA - Box"]);
                                ws2.Cell("H" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                TotalKgULTRABox = TotalKgULTRABox + (Convert.IsDBNull(product.Rows[j]["Euro ULTRA - Box"]) ? 0 : (decimal)product.Rows[j]["Euro ULTRA - Box"]);


                                ws2.Cell("I" + countrow).Value = Convert.ToString(product.Rows[j]["Euro ULTRA - NO"]);
                                ws2.Cell("I" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                TotalKgULTRANos = TotalKgULTRANos + (Convert.IsDBNull(product.Rows[j]["Euro ULTRA - NO"]) ? 0 : (decimal)product.Rows[j]["Euro ULTRA - NO"]);


                                ws2.Cell("J" + countrow).Value = Convert.ToString(product.Rows[j]["PVC GLUE - Box"]);
                                ws2.Cell("J" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                TotalKgPVCBox = TotalKgPVCBox + (Convert.IsDBNull(product.Rows[j]["PVC GLUE - Box"]) ? 0 : (decimal)product.Rows[j]["PVC GLUE - Box"]);


                                ws2.Cell("K" + countrow).Value = Convert.ToString(product.Rows[j]["PVC GLUE - NO"]);
                                ws2.Cell("K" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                TotalKgPVCNos = TotalKgPVCNos + (Convert.IsDBNull(product.Rows[j]["PVC GLUE - NO"]) ? 0 : (decimal)product.Rows[j]["PVC GLUE - NO"]);


                                ws2.Cell("L" + countrow).Value = Convert.ToString(product.Rows[j]["Wood Strong - Box"]);
                                ws2.Cell("L" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                TotalKgWoodBox = TotalKgWoodBox + (Convert.IsDBNull(product.Rows[j]["Wood Strong - Box"]) ? 0 : (decimal)product.Rows[j]["Wood Strong - Box"]);

                                ws2.Cell("M" + countrow).Value = Convert.ToString(product.Rows[j]["Wood Strong - NO"]);
                                ws2.Cell("M" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                TotalKgXTRANos = TotalKgXTRANos + (Convert.IsDBNull(product.Rows[j]["Wood Strong - NO"]) ? 0 : (decimal)product.Rows[j]["Wood Strong - NO"]);

                                //ws2.Cell("N" + countrow).Value = Convert.ToString(product.Rows[j]["EWR - Box"]);
                                //ws2.Cell("N" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                //TotalKgEWRBox = TotalKgEWRBox + (Convert.IsDBNull(product.Rows[j]["EWR - Box"]) ? 0 : (decimal)product.Rows[j]["EWR - Box"]);


                                //ws2.Cell("O" + countrow).Value = Convert.ToString(product.Rows[j]["EWR - NO"]);
                                //ws2.Cell("O" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                //TotalKgEWRNos = TotalKgEWRNos + (Convert.IsDBNull(product.Rows[j]["EWR - NO"]) ? 0 : (decimal)product.Rows[j]["EWR - NO"]);

                                ws2.Cell("N" + countrow).Value = Convert.ToString(product.Rows[j]["EURO HI STRONG - Box"]);
                                ws2.Cell("N" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                TotalKgJHIBox = TotalKgJHIBox + (Convert.IsDBNull(product.Rows[j]["EURO HI STRONG - Box"]) ? 0 : (decimal)product.Rows[j]["EURO HI STRONG - Box"]);


                                ws2.Cell("O" + countrow).Value = Convert.ToString(product.Rows[j]["EURO HI STRONG - NO"]);
                                ws2.Cell("O" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                TotalKgJHINos = TotalKgJHINos + (Convert.IsDBNull(product.Rows[j]["EURO HI STRONG - NO"]) ? 0 : (decimal)product.Rows[j]["EURO HI STRONG - NO"]);



                                ws2.Cell("P" + countrow).Value = Convert.ToString(product.Rows[j]["EWR ECO GRADE - Box"]);
                                ws2.Cell("P" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                TotalKgJECOGRADEBox = TotalKgJECOGRADEBox + (Convert.IsDBNull(product.Rows[j]["EWR ECO GRADE - Box"]) ? 0 : (decimal)product.Rows[j]["EWR ECO GRADE - Box"]);


                                ws2.Cell("Q" + countrow).Value = Convert.ToString(product.Rows[j]["EWR ECO GRADE - NO"]);
                                ws2.Cell("Q" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                TotalKgJECOGRADENos = TotalKgJECOGRADENos + (Convert.IsDBNull(product.Rows[j]["EWR ECO GRADE - NO"]) ? 0 : (decimal)product.Rows[j]["EWR ECO GRADE - NO"]);

                                ws2.Cell("R" + countrow).Value = Convert.ToString(product.Rows[j]["EWR MAX - Box"]);
                                ws2.Cell("R" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                TotalKgJEWRMAXBox = TotalKgJEWRMAXBox + (Convert.IsDBNull(product.Rows[j]["EWR MAX - Box"]) ? 0 : (decimal)product.Rows[j]["EWR MAX - Box"]);


                                ws2.Cell("S" + countrow).Value = Convert.ToString(product.Rows[j]["EWR MAX - NO"]);
                                ws2.Cell("S" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                TotalKgJEWRMAXNos = TotalKgJEWRMAXNos + (Convert.IsDBNull(product.Rows[j]["EWR MAX - NO"]) ? 0 : (decimal)product.Rows[j]["EWR MAX - NO"]);


                                ws2.Cell("T" + countrow).Value = Convert.ToString(product.Rows[j]["EWR E3+ - Box"]);
                                ws2.Cell("T" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                TotalKgJEWRE3Box = TotalKgJEWRE3Box + (Convert.IsDBNull(product.Rows[j]["EWR E3+ - Box"]) ? 0 : (decimal)product.Rows[j]["EWR E3+ - Box"]);

                                ws2.Cell("U" + countrow).Value = Convert.ToString(product.Rows[j]["EWR E3+ - NO"]);
                                ws2.Cell("U" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                TotalKgJEWRE3Nos = TotalKgJEWRE3Nos + (Convert.IsDBNull(product.Rows[j]["EWR E3+ - NO"]) ? 0 : (decimal)product.Rows[j]["EWR E3+ - NO"]);

                                countrow = countrow + 1;
                            }
                        }


                        countrow = countrow + 1;
                        ws2.Cell("A" + countrow).Value = "Total";
                        ws2.Cell("A" + countrow).Style.Font.SetBold();
                        ws2.Cell("A" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("B" + countrow).Value = TotalKgXTRABox;
                        ws2.Cell("B" + countrow).Style.Font.SetBold();
                        ws2.Cell("B" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("C" + countrow).Value = TotalKgXTRANos;
                        ws2.Cell("C" + countrow).Style.Font.SetBold();
                        ws2.Cell("C" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("D" + countrow).Value = TotalKgWPBox;
                        ws2.Cell("D" + countrow).Style.Font.SetBold();
                        ws2.Cell("D" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("E" + countrow).Value = TotalKgWPNos;
                        ws2.Cell("E" + countrow).Style.Font.SetBold();
                        ws2.Cell("E" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("F" + countrow).Value = TotalKgE3Box;
                        ws2.Cell("F" + countrow).Style.Font.SetBold();
                        ws2.Cell("F" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("G" + countrow).Value = TotalKgE3Nos;
                        ws2.Cell("G" + countrow).Style.Font.SetBold();
                        ws2.Cell("G" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("H" + countrow).Value = TotalKgULTRABox;
                        ws2.Cell("H" + countrow).Style.Font.SetBold();
                        ws2.Cell("H" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("I" + countrow).Value = TotalKgULTRANos;
                        ws2.Cell("I" + countrow).Style.Font.SetBold();
                        ws2.Cell("I" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("J" + countrow).Value = TotalKgPVCBox;
                        ws2.Cell("J" + countrow).Style.Font.SetBold();
                        ws2.Cell("J" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("K" + countrow).Value = TotalKgPVCNos;
                        ws2.Cell("K" + countrow).Style.Font.SetBold();
                        ws2.Cell("K" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("L" + countrow).Value = TotalKgWoodBox;
                        ws2.Cell("L" + countrow).Style.Font.SetBold();
                        ws2.Cell("L" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("M" + countrow).Value = TotalKgWoodNos;
                        ws2.Cell("M" + countrow).Style.Font.SetBold();
                        ws2.Cell("M" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("N" + countrow).Value = TotalKgJHIBox;
                        ws2.Cell("N" + countrow).Style.Font.SetBold();
                        ws2.Cell("N" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("O" + countrow).Value = TotalKgJHINos;
                        ws2.Cell("O" + countrow).Style.Font.SetBold();
                        ws2.Cell("O" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("P" + countrow).Value = TotalKgJECOGRADEBox;
                        ws2.Cell("P" + countrow).Style.Font.SetBold();
                        ws2.Cell("P" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("Q" + countrow).Value = TotalKgJECOGRADENos;
                        ws2.Cell("Q" + countrow).Style.Font.SetBold();
                        ws2.Cell("Q" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                        ws2.Cell("R" + countrow).Value = TotalKgJEWRMAXBox;
                        ws2.Cell("R" + countrow).Style.Font.SetBold();
                        ws2.Cell("R" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("S" + countrow).Value = TotalKgJEWRMAXNos;
                        ws2.Cell("S" + countrow).Style.Font.SetBold();
                        ws2.Cell("S" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("T" + countrow).Value = TotalKgJEWRE3Box;
                        ws2.Cell("T" + countrow).Style.Font.SetBold();
                        ws2.Cell("T" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        ws2.Cell("U" + countrow).Value = TotalKgJEWRE3Nos;
                        ws2.Cell("U" + countrow).Style.Font.SetBold();
                        ws2.Cell("U" + countrow).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        #endregion
                    }

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                    Response.AddHeader("content-disposition",
                        "attachment;filename=Batchlist" + DateTime.Now.ToString("ddMMMyyyyhhmmss") + ".xlsx");
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        workbook.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        //Response.End();

                        HttpContext.Current.Response.SuppressContent = true;
                        //Directs the thread to finish, bypassing additional processing
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                        Thread.Sleep(1);
                    }
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }

        }

    }
}
