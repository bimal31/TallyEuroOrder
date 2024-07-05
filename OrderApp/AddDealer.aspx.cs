using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OrderApp
{
    public partial class AddDealer : System.Web.UI.Page
    {
        #region Class Properties Declarations
       // private static readonly string _rootPath = ConfigurationManager.AppSettings["RootPath"].ToString();
        private static readonly string _rootPath = HttpContext.Current.Server.MapPath("~/");
        #endregion 

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    GetStateList();

                    if (Request.QueryString["q"] != null)
                    {
                        string strKey = Convert.ToString(Request.QueryString["q"]);
                        Common cmn = new Common();
                        strKey = cmn.Decrypt(strKey);

                        Int32 DealerId = Convert.ToInt32(strKey);

                        BA_tblDealer ObjDealer = new BA_tblDealer();
                        DataTable dt = new DataTable();

                        ObjDealer.DealerID = Convert.ToInt32(DealerId);
                        ObjDealer.GET_RECORDS_FROM_tblDealer(ref dt);

                        if (dt != null)
                        {
                            txtDealerCode.Text = Convert.ToString(dt.Rows[0]["DealerCode"]);
                            txtDealerName.Text = Convert.ToString(dt.Rows[0]["DealerName"]);
                            txtcontactname.Text = Convert.ToString(dt.Rows[0]["ContactName"]);
                            txtAddress.Text = Convert.ToString(dt.Rows[0]["Address"]);
                            txtArea.Text = Convert.ToString(dt.Rows[0]["Area"]);
                            txtpincode.Text = Convert.ToString(dt.Rows[0]["Pincode"]);
                            txtGST.Text = Convert.ToString(dt.Rows[0]["GST"]);
                            txtPhoneNo.Text = Convert.ToString(dt.Rows[0]["Phone"]);
                            if (Convert.ToString(dt.Rows[0]["StateID"]) == "")
                            {
                                drpStateName.SelectedValue = "0";
                            }
                            else {
                                drpStateName.SelectedValue = Convert.ToInt32(dt.Rows[0]["StateID"]).ToString();
                            }
                            
                            hdnGSTPhoto.Value = Convert.ToString(dt.Rows[0]["GSTPhoto"]);
                            hdnVisitCard.Value = Convert.ToString(dt.Rows[0]["VisitCard"]);

                            hdDealerId.Value = Convert.ToString(dt.Rows[0]["DealerId"]);

                            if (hdnGSTPhoto.Value != "")
                            {
                                var sourceFilePath = Path.Combine(_rootPath, "DealerImage", "GST", hdnGSTPhoto.Value);

                                if (File.Exists(sourceFilePath))
                                    imgGSTPhoto.ImageUrl = Path.Combine("~/DealerImage", "GST", hdnGSTPhoto.Value);
                            }

                            if (hdnVisitCard.Value != "")
                            {
                                var sourceFilePath = Path.Combine(_rootPath, "DealerImage", "Card", hdnVisitCard.Value);

                                if (File.Exists(sourceFilePath))
                                    imgVisitCard.ImageUrl = Path.Combine("~/DealerImage", "Card", hdnVisitCard.Value);
                            }
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

        public void GetStateList()
        {
            DataTable dt = new DataTable();
            BA_States ObjBStates = new BA_States();

            ObjBStates.SELECT_ALL_States(ref dt);

            drpStateName.DataSource = dt;
            drpStateName.DataTextField = "state_name";
            drpStateName.DataValueField = "state_id";
            drpStateName.DataBind();
            drpStateName.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        private bool ValidateForm()
        {
            if (drpStateName.SelectedValue == "0")
            {
                lblErrorMessage.Text = CommMessage.selectstatename;

                return false;
            }

            if (FileUploadGST.HasFile)
            {
                //Validating image file type
                var fileExtension = Path.GetExtension(FileUploadGST.FileName).ToLower();

                if (!CommMessage._validImageExtensions.Contains(fileExtension))
                {
                    lblErrorMessage.Text = string.Format(CommMessage._InvalidImageFile, "GST Photo");

                    return false;
                }

                //Validating image file size
                var fileSize = FileUploadGST.FileBytes.Length;

                if (fileSize > CommMessage._validImageSize)
                {
                    lblErrorMessage.Text = string.Format(CommMessage._InvalidImageSize, "GST Photo");

                    return false;
                }
            }

            if (FileUploadVisitCard.HasFile)
            {
                //Validating image file type
                var fileExtension = Path.GetExtension(FileUploadVisitCard.FileName).ToLower();

                if (!CommMessage._validImageExtensions.Contains(fileExtension))
                {
                    lblErrorMessage.Text = string.Format(CommMessage._InvalidImageFile, "Visit Card Photo");

                    return false;
                }

                //Validating image file size
                var fileSize = FileUploadVisitCard.FileBytes.Length;

                if (fileSize > CommMessage._validImageSize)
                {
                    lblErrorMessage.Text = string.Format(CommMessage._InvalidImageSize, "Visit Card Photo");

                    return false;
                }
            }

            return true;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateForm())
                {
                    var newGSTFileName = hdnGSTPhoto.Value != "" ? hdnGSTPhoto.Value :
                        FileUploadGST.HasFile ? string.Concat(Guid.NewGuid().ToString(), ".png") : string.Empty;

                    var newCardFileName = hdnVisitCard.Value != "" ? hdnVisitCard.Value :
                        FileUploadVisitCard.HasFile ? string.Concat(Guid.NewGuid().ToString(), ".png") : string.Empty;

                    BA_tblDealer ObjDealer = new BA_tblDealer();
                    Common Cmn = new Common();
                    ObjDealer.DealerCode = txtDealerCode.Text;
                    ObjDealer.DealerName = txtDealerName.Text;
                    ObjDealer.Address = txtAddress.Text;
                    ObjDealer.Area = txtArea.Text;
                    ObjDealer.Pincode = txtpincode.Text;
                    ObjDealer.GST = txtGST.Text;
                    ObjDealer.Phone = txtPhoneNo.Text;
                    ObjDealer.ContactName = txtcontactname.Text;
                    ObjDealer.StateID = Convert.ToInt32(drpStateName.SelectedValue);
                    ObjDealer.GSTPhoto = newGSTFileName;
                    ObjDealer.VisitCard = newCardFileName;
                    ObjDealer.CreateBy = Convert.ToInt32(Session["UserId"]);
                    ObjDealer.Isdeleted = false;
                    ObjDealer.UpdateBy = Convert.ToInt32(Session["UserId"]);

                    bool output = false;

                    if (hdDealerId.Value == "")
                        output = ObjDealer.INSERT_tblDealer();
                    else
                    {
                        ObjDealer.DealerID = Convert.ToInt32(hdDealerId.Value);
                        output = ObjDealer.UPDATE_tblDealer();
                    }

                    if (output)
                    {
                        if (FileUploadGST.HasFile)
                        {
                            var destFilePath = Path.Combine(_rootPath, "DealerImage", "GST", newGSTFileName);

                            FileUploadGST.SaveAs(destFilePath);
                        }

                        if (FileUploadVisitCard.HasFile)
                        {
                            var destFilePath = Path.Combine(_rootPath, "DealerImage", "Card", newCardFileName);

                            FileUploadVisitCard.SaveAs(destFilePath);
                        }

                        Response.Redirect("DealerList.aspx", false);
                    }
                    else
                        lblErrorMessage.Text = CommMessage.Recordcouldnotable;
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
                txtDealerCode.Text = "";
                txtDealerName.Text = "";
                txtGST.Text = "";
                txtAddress.Text = "";
                txtPhoneNo.Text = "";
                txtArea.Text = "";
                txtcontactname.Text = "";
                txtpincode.Text = "";
                drpStateName.SelectedValue = "0";

                hdnGSTPhoto.Value = "";
                hdnVisitCard.Value = "";

                imgGSTPhoto.ImageUrl = Path.Combine("~/DealerImage", "no-picture-available.png");
                imgVisitCard.ImageUrl = Path.Combine("~/DealerImage", "no-picture-available.png");
            }
            catch (Exception ex)
            {
                BA_ErrorLog ObjError = new BA_ErrorLog();
                ObjError.INSERT_ErrorLog(ex);
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            Response.Redirect("DealerList.aspx", false);
        }
    }
}