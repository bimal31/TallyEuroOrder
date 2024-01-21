using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace OrderApp
{
    public partial class TallyImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            string _xmlFilePath = Server.MapPath("~/assets/tallyImport/final.xml");

            XmlDocument _xmlDoc = new XmlDocument();
            _xmlDoc.Load(_xmlFilePath);

            var _xmlData = _xmlDoc.InnerXml;

            var _response = SendTallyRequest(_xmlData);
        }

        public ResponseModel SendTallyRequest(string _requestString)
        {
            ResponseModel _responseModel = new ResponseModel();

            try
            {
                //string _tallyHost = "http://localhost:9000";
                string _tallyHost = "http://122.169.58.251:9000";

                HttpWebRequest _webRequest = (HttpWebRequest)WebRequest.Create(_tallyHost);
                _webRequest.Method = "POST";
                _webRequest.ContentLength = (long)_requestString.Length;
                _webRequest.ContentType = "application/x-www-form-urlencoded";

                StreamWriter _sWriter = new StreamWriter(_webRequest.GetRequestStream());
                _sWriter.Write(_requestString);
                _sWriter.Close();

                HttpWebResponse _webResponse = (HttpWebResponse)_webRequest.GetResponse();
                Stream _responseStream = _webResponse.GetResponseStream();

                StreamReader _sReader = new StreamReader(_responseStream, Encoding.UTF8);

                string _responseString = _sReader.ReadToEnd();

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(_responseString);
                doc.Save(Server.MapPath("~/assets/tallyImport/response.xml"));

                DataSet ds = new DataSet();
                ds.ReadXml(Server.MapPath("~/assets/tallyImport/response.xml"));

                _responseModel.dsResponse = ds;
                _responseModel.StrResponse = _responseString;

                _webResponse.Close();
                _sReader.Close();

                return _responseModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public class ResponseModel
        {
            public DataSet dsResponse;

            public string StrResponse;
            public string Company;
            public string strVchType;
            public string strVchNumber;
            public string strDate;
            public string strNarration;
            public string strVoucherEntryName1;
            public string strISDEEMEDPOSITIVE1;
            public string strAmount1;
            public string strVoucherEntryName2;
            public string strISDEEMEDPOSITIVE2;
            public string strAmount2;

            public int intMasterId;
        }
    }
}