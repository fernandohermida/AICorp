using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Collections.Specialized;

namespace AiCorporationApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SubmitButton_OnClick(Object sender, EventArgs e)
        {
            var valuesCollectionAll = new Dictionary<string, string>
            {
                ["PreSharedKey"] = "5XNm04UE0/EaXDSeKa4Fw29hTzieAl32uO4=",
                ["MerchantID"] = "Progra-7702818",
                ["Password"] = "G6CH6Z90I2",
                ["Amount"] = Amount.Text,
                ["OrderID"] = OrderID.Text,
                ["TransactionType"] = TransactionType.Text,
                ["CurrencyCode"] = "826",
                ["TransactionDateTime"] = "2019-05-09 21:20:29 +00:00",
                ["CV2Mandatory"] = "false",
                ["Address1Mandatory"] = "false",
                ["CityMandatory"] = "false",
                ["PostCodeMandatory"] = "false",
                ["StateMandatory"] = "false",
                ["CountryMandatory"] = "false",
                ["ResultDeliveryMethod"] = "POST"
            };

            string stringForhashDigest = string.Join("&", valuesCollectionAll.Select(x => x.Key + "=" + x.Value).ToArray());
            string hashDigest = SHA1HashStringForUTF8String(stringForhashDigest);

            NameValueCollection valuesCollection = new NameValueCollection()
            {
                { "HashDigest" , hashDigest.ToString()},
                { "MerchantID", "Progra-7702818" },
                { "Amount", Amount.Text },
                { "OrderID", OrderID.Text },
                { "TransactionType", TransactionType.Text },
                { "CurrencyCode", "863" },
                { "TransactionDateTime", "2019-05-09 21:20:29 +00:00" },
                { "ResultDeliveryMethod", "POST" },
                { "CallbackURL", "http://www.somedomain.com/callback.php" }
            };
            
            string URI = "https://mms.payvector.net/Pages/PublicPages/PaymentForm.aspx";

            HttpPostwebClient(URI, valuesCollection);
        }

        public void SubmitButton_OnClick2(Object sender, EventArgs e)
        {
            var valuesCollectionHash = new Dictionary<string, string>
            {
                ["PreSharedKey"] = "5XNm04UE0/EaXDSeKa4Fw29hTzieAl32uO4=",
                ["MerchantID"] = "Progra-7702818",
                ["Password"] = "G6CH6Z90I2",
                ["Amount"] = "100",
                ["OrderID"] = "Order-1234",
                ["TransactionType"] = "SALE",
                ["CurrencyCode"] = "826",
                ["TransactionDateTime"] = "2019-05-09 21:20:29 +00:00",
                ["CV2Mandatory"] = "false",
                ["Address1Mandatory"] = "false",
                ["CityMandatory"] = "false",
                ["PostCodeMandatory"] = "false",
                ["StateMandatory"] = "false",
                ["CountryMandatory"] = "false",
                ["ResultDeliveryMethod"] = "POST"

            };
          
            string stringForhashDigest = string.Join("&", valuesCollectionHash.Select(x => x.Key + "=" + x.Value).ToArray());
            string hashDigest = SHA1HashStringForUTF8String(stringForhashDigest);

            var valuesCollection = new Dictionary<string, string>
            {
                ["HashDigest"] = hashDigest,
                ["MerchantID"] = "Progra-7702818",
                ["Amount"] = "100",
                ["OrderID"] = "Order-1234",
                ["TransactionType"] = "SALE",
                ["CurrencyCode"] = "826",
                ["TransactionDateTime"] = "2019-05-09 21:20:29 +00:00",
                ["ResultDeliveryMethod"] = "POST",
                ["CallbackURL"] = "http://www.somedomain.com/callback.php"

            };

            string stingParameters = string.Join("&", valuesCollection.Select(x => x.Key + "=" + x.Value).ToArray());

            string URI = "https://mms.payvector.net/Pages/PublicPages/PaymentForm.aspx";


            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string HtmlResult = wc.UploadString(URI, stingParameters);
            }
        }

        public static string HttpPostWebRequest(string URI, string Parameters)
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            //req.Proxy = new System.Net.WebProxy(ProxyString, true);
            //Add these, as we're doing a POST
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            //We need to count how many bytes we're sending. Post'ed Faked Forms should be name=value&
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(Parameters);
            req.ContentLength = bytes.Length;
            System.IO.Stream os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            System.Net.WebResponse resp = req.GetResponse();
            if (resp == null) return null;
            System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }

        public static void HttpPostwebClient(string URI, NameValueCollection valuesCollection)
        {          
            WebClient webClient = new WebClient();
            byte[] responseBytes = webClient.UploadValues(URI, "POST", valuesCollection);
            string Result = Encoding.UTF8.GetString(responseBytes);
        }

        public static string SHA1HashStringForUTF8String(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);

            var sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(bytes);

            return HexStringFromBytes(hashBytes);
        }

        public static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }

       
    }
}