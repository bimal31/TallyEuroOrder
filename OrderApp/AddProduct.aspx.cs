using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class AddProduct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Request.QueryString["q"] != null)
                    {
                        string strKey = Convert.ToString(Request.QueryString["q"]);
                        Common cmn = new Common();
                        strKey = cmn.Decrypt(strKey);

                        Int32 ProductId = Convert.ToInt32(strKey);

                        BA_tblProduct ObjDealer = new BA_tblProduct();
                        DataTable dt = new DataTable();

                        ObjDealer.ProductId = ProductId;
                        ObjDealer.GET_RECORDS_FROM_tblProduct(ref dt);

                        if (dt != null)
                        {
                            txtProductName.Text = Convert.ToString(dt.Rows[0]["ProductName"]);
                            txtProductDesc.Text = Convert.ToString(dt.Rows[0]["ProductDesc"]);

                            txtFromScheme.Text = Convert.ToString(dt.Rows[0]["Fromscheme"]);
                            txtToScheme.Text = Convert.ToString(dt.Rows[0]["Toscheme"]);

                            hdProductId.Value = Convert.ToString(dt.Rows[0]["ProductId"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                BA_tblProduct ObjProduct = new BA_tblProduct();
                Common Cmn = new Common();
                if (hdProductId.Value == "")
                {
                    ObjProduct.ProductId = 0;
                }
                else
                {
                    ObjProduct.ProductId = Convert.ToInt32(hdProductId.Value);
                }

                if (!checkproductname(ObjProduct.ProductId))
                {

                    ObjProduct.ProductName = txtProductName.Text;
                    ObjProduct.ProductDesc = txtProductDesc.Text;

                    ObjProduct.FromScheme = Convert.ToInt32(txtFromScheme.Text);
                    ObjProduct.ToScheme = Convert.ToInt32(txtToScheme.Text);


                    ObjProduct.CreateBy = Convert.ToInt32(Session["UserId"]);
                    ObjProduct.Isdeleted = false;
                    ObjProduct.UpdateBy = Convert.ToInt32(Session["UserId"]);

                    bool output;
                    if (hdProductId.Value == "")
                    {
                        output = ObjProduct.INSERT_tblProduct();
                    }
                    else
                    {
                        ObjProduct.ProductId = Convert.ToInt32(hdProductId.Value);
                        output = ObjProduct.UPDATE_tblProduct();
                    }


                    if (output == true)
                    {
                        Response.Redirect("ProductList.aspx", false);
                    }
                    else
                    {
                        lblErrorMessage.Text = CommMessage.Recordcouldnotable;
                        lblErrorMessage.ForeColor = System.Drawing.Color.Black;
                    }
                }
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtProductName.Text = "";
                txtProductDesc.Text = "";
                txtFromScheme.Text = "";
                txtToScheme.Text = "";
                hdProductId.Value = "";
                lblErrorMessage.Text = "";

            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }


        public bool checkproductname(int ProductId)
        {
            try
            {
                BA_tblProduct ObjProduct = new BA_tblProduct();
                DataTable dt = new DataTable();
                ObjProduct.ProductId = ProductId;
                ObjProduct.ProductName = txtProductName.Text;
                ObjProduct.CHK_RECORDS_FROM_tblProduct(ref dt);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        lblErrorMessage.Text = CommMessage.productnamealreadyexist;
                        lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                        return true;
                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
                return true;
            }
        }


        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductList.aspx", false);
        }
    }
}