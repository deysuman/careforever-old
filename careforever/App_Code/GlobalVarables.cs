using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Globalization;
using System.Collections;
using System.Text;

namespace careforever.App_Code
{
    public class GlobalVarables
    {
        Cart cart = new Cart();
        public GlobalVarables()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        #region Image Urls


        public static string Image_Url()
        {
            String originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
            String parentDirectory = originalPath.Substring(0, originalPath.LastIndexOf("/"));
            if (parentDirectory.Contains("blulife.in"))
            {
                return "https://blulife.in/Admin/ProductImages/";
            }
            else
            {
                return "https://blulife.in/Admin/ProductImages/";
            }
            //return "http://graylogic.org/Blulife_Admin/ProductImages/";
        }

        public string Current_Url()
        {
            String originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
            String parentDirectory = originalPath.Substring(0, originalPath.LastIndexOf("/"));
            if (parentDirectory.Contains("blulife.in"))
            {
                return "https://blulife.in/Admin/";
            }
            else
            {
                return "https://blulife.in/Admin/";
            }
            //return "http://graylogic.org/Blulife_Admin/";
        }
        public string CurrentClient_Url()
        {
            String originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
            String parentDirectory = originalPath.Substring(0, originalPath.LastIndexOf("/"));
            if (parentDirectory.Contains("blulife.in"))
            {
                return "https://blulife.in/";
            }
            else
            {
                return "http://192.168.10.97/BluClient/";
            }
        }

        public string Current_Url_Client()
        {
            String originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
            String parentDirectory = originalPath.Substring(0, originalPath.LastIndexOf("/"));
            if (parentDirectory.Contains("blulife.in"))
            {
                return "http://blulife.in/";
            }
            else
            {
                return "http://192.168.10.95/blulife_client/";
            }
            //return "http://graylogic.org/blulife/";
        }
        public static string AttachmentURL()
        {
            string str = HttpContext.Current.Request.Url.AbsoluteUri;
            String originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
            String parentDirectory = originalPath.Substring(0, originalPath.LastIndexOf("/"));
            if (parentDirectory.Contains("blulife.in"))
            {
                return "http://blulife.in/Documents/";
            }
            else
            {
                // return "http://blulife.in/Documents/";
                return "http://192.168.10.95/Blulife_Client/Documents/";
            }

        }
        public static string AttachmentURLreplace()
        {
            string str = HttpContext.Current.Request.Url.AbsoluteUri;
            String originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
            String parentDirectory = originalPath.Substring(0, originalPath.LastIndexOf("/"));
            if (parentDirectory.Contains("blulife.in"))
            {
                return "http://blulife.in/admin/Documents/";
            }
            else
            {
                //return "http://blulife.in/admin/Documents/";
                return "http://192.168.10.95/Blulife_Admin/ProductImages/";

            }

        }
        #endregion


        public static bool isAuthenticated(System.Web.SessionState.HttpSessionState session)
        {
            return session["member"] != null;
        }

        public static void logout(System.Web.SessionState.HttpSessionState session)
        {
            session.Clear();
        }

        public static bool login(System.Web.SessionState.HttpSessionState session, string accno, string pwd)
        {
            DataSet loginds = new DataSet();
            BLL.BLL balobj = new BLL.BLL();
            loginds = balobj.Account_Login(accno, pwd);
            if (GlobalVarables.chkdataset(loginds))
            {
                session["member"] = loginds.Tables[0].Rows[0]["AccountNo"].ToString();
                return true;
            }
            else
                return false;
        }

        public static bool chkdataset(DataSet chkds)
        {
            bool returnvalue = false;
            if (chkds.Tables.Count > 0)
            {
                for (int i = 0; i < chkds.Tables.Count; i++)
                {
                    if (chkds.Tables[i].Rows.Count > 0)
                    {
                        if (chkds.Tables[i].Rows[0][0].ToString().Trim().Length > 0)
                        {
                            returnvalue = true;
                            break;
                        }
                    }
                }
            }
            return returnvalue;
        }

        public string TitleCaseString(string str)
        {
            string title = string.Empty;
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            title = textInfo.ToTitleCase(str); // 
            return title;
        }


        public string Update_Cart(string prodID, string Qty)
        {
            string Output = "";
            try
            {
                DataTable dt = cart.itemTable();
                bool prodStatus = false;
                int prod_Index = 0;
                if (HttpContext.Current.Session["Cart"] != null)
                {
                    dt = HttpContext.Current.Session["Cart"] as DataTable;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Productid"].ToString() == prodID)
                        {
                            prodStatus = true;
                            prod_Index = i;
                            break;
                        }
                    }
                    if (prodStatus)
                    {
                        dt.Rows[prod_Index]["Quantity"] = Convert.ToString(Convert.ToInt32(dt.Rows[prod_Index]["Quantity"].ToString()) + Convert.ToInt32(Qty));
                        Output = "Updated";
                    }
                    else
                    {
                        dt.Rows.Add(prodID, Qty);
                        Output = "Inserted";
                    }
                }
                else
                {
                    dt.Rows.Add(prodID, Qty);
                    Output = "Inserted";
                }
                HttpContext.Current.Session["Cart"] = dt;
            }
            catch
            {
                Output = "Error";
            }
            return Output;
        }

        public string Update_Cart1(string prodID, string Qty, string size, string color)
        {
            string Output = "";
            try
            {
                DataTable dt = cart.itemTable1();
                bool prodStatus = false;
                int prod_Index = 0;
                if (HttpContext.Current.Session["Cart"] != null)
                {
                    dt = HttpContext.Current.Session["Cart"] as DataTable;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["Productid"].ToString() == prodID &&
                            dt.Rows[i]["Color"].ToString() == color && dt.Rows[i]["Size"].ToString() == size)
                        {
                            prodStatus = true;
                            prod_Index = i;
                            break;
                        }
                    }
                    if (prodStatus)
                    {
                        dt.Rows[prod_Index]["Quantity"] = Convert.ToString(Convert.ToInt32(dt.Rows[prod_Index]["Quantity"].ToString()) + Convert.ToInt32(Qty));
                        Output = "Updated";
                    }
                    else
                    {
                        dt.Rows.Add(prodID, Qty, size, color);
                        Output = "Inserted";
                    }
                }
                else
                {
                    dt.Rows.Add(prodID, Qty, size, color);
                    Output = "Inserted";
                }
                HttpContext.Current.Session["Cart"] = dt;
            }
            catch
            {
                Output = "Error";
            }
            return Output;
        }

        //public DataTable CartProducts()
        //{
        //    BLL.BLL balobj = new BLL.BLL();
        //    DataSet ds = new DataSet();
        //    Cart cart = new Cart();

        //    DataTable dt = cart.itemTable();
        //    DataTable dtt = cart.ViewCartTable();
        //    DataTable dtt1 = cart.ViewCartTable();

        //    ds = balobj.Get_Products("");
        //    dt = HttpContext.Current.Session["Cart"] as DataTable;

        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        dtt.Rows.Add(ds.Tables[0].Rows[i]["ProductID"].ToString(),
        //                   ds.Tables[0].Rows[i]["Productname"].ToString(),
        //                   GlobalVarables.Image_Url() + ds.Tables[0].Rows[i]["Product_ImgUrl"].ToString(),
        //                   "0",
        //                   ds.Tables[0].Rows[i]["DistributorPrice"].ToString(),
        //                   ds.Tables[0].Rows[i]["DistributorPrice"].ToString());
        //    }
        //    if (dt != null & dtt != null)
        //    {
        //        if (dt.Rows.Count > 0 & dtt.Rows.Count > 0)
        //        {
        //            var result = from dataRows1 in dtt.AsEnumerable()
        //                         join dataRows2 in dt.AsEnumerable()
        //                         on dataRows1.Field<string>("ProductID") equals dataRows2.Field<string>("Productid")
        //                         select dtt1.LoadDataRow(new object[]
        //                    {
        //                        dataRows1.Field<string>("ProductID"),
        //                        dataRows1.Field<string>("Productname"),
        //                        dataRows1.Field<string>("Product_ImgUrl"),
        //                        dataRows2.Field<string>("Quantity"),
        //                        dataRows1.Field<string>("Unitmrpprice"),
        //                        (Convert.ToDouble(dataRows2.Field<string>("Quantity")) * Convert.ToDouble(dataRows1.Field<string>("Unitmrpprice"))).ToString(),
        //                        dataRows2.Field<string>("Size"),
        //                        dataRows2.Field<string>("Color")
        //                    }, false);
        //            return result.CopyToDataTable();
        //        }
        //        else
        //            return dtt1;
        //    }
        //    else
        //        return dtt1;
        //}
        public DataTable CartProducts()
        {
            BLL.BLL balobj = new BLL.BLL();
            DataSet ds = new DataSet();
            Cart cart = new Cart();

            DataTable dt = cart.itemTable();
            DataTable dtt = cart.ViewCartTable();
            DataTable dtt1 = cart.ViewCartTable();

            ds = balobj.Get_Products("");
            dt = HttpContext.Current.Session["Cart"] as DataTable;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dtt.Rows.Add(ds.Tables[0].Rows[i]["ProductID"].ToString(),
                           ds.Tables[0].Rows[i]["Productname"].ToString(),
                           GlobalVarables.Image_Url() + ds.Tables[0].Rows[i]["Product_ImgUrl"].ToString(),
                           "0",
                           ds.Tables[0].Rows[i]["DistributorPrice"].ToString(),
                           ds.Tables[0].Rows[i]["DistributorPrice"].ToString());
            }
            if (dt != null & dtt != null)
            {
                if (dt.Rows.Count > 0 & dtt.Rows.Count > 0)
                {
                    var result = from dataRows1 in dtt.AsEnumerable()
                                 join dataRows2 in dt.AsEnumerable()
                                 on dataRows1.Field<string>("ProductID") equals dataRows2.Field<string>("Productid")
                                 select dtt1.LoadDataRow(new object[]
                            {
                            dataRows1.Field<string>("ProductID"),
                            dataRows1.Field<string>("Productname"),
                            dataRows1.Field<string>("Product_ImgUrl"),
                            dataRows2.Field<string>("Quantity"),
                            dataRows1.Field<string>("Unitmrpprice"),
                            (Convert.ToDouble(dataRows2.Field<string>("Quantity")) * Convert.ToDouble(dataRows1.Field<string>("Unitmrpprice"))).ToString()
                            }, false);
                    return result.CopyToDataTable();
                }
                else
                    return dtt1;
            }
            else
                return dtt1;
        }

        public void cler_CartProducts()
        {
            HttpContext.Current.Session["Cart"] = null;
        }

        public DataTable Update_SessionCart(string prodID, string Qty)
        {
            DataTable dt_cart = new DataTable();
            DataTable dt = new DataTable();
            dt = HttpContext.Current.Session["Cart"] as DataTable;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Productid"].ToString() == prodID)
                {
                    dr["Quantity"] = Qty;
                }
            }
            HttpContext.Current.Session["Cart"] = dt;
            /*
            var result = dt.AsEnumerable().Where(r => r.Field<string>("Productid") == prodID);
            foreach (var row in result)
                row.SetField("Quantity", Qty);
            */

            dt_cart = new GlobalVarables().CartProducts();
            return dt_cart;
        }

        public DataTable RemoveProd_Cart(string prodID)
        {
            DataTable dt_cart = new DataTable();
            DataTable dt = HttpContext.Current.Session["Cart"] as DataTable;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Productid"].ToString() == prodID)
                {
                    dt.Rows[i].Delete();
                }
            }
            HttpContext.Current.Session["Cart"] = dt;
            /*
            var result = dt.AsEnumerable().Where(r => r.Field<string>("Productid") == prodID);
            foreach (var row in result.ToList())
                row.Delete();
             */

            dt_cart = new GlobalVarables().CartProducts();
            return dt_cart;
        }

        public static DateTime? GetDate(string datetime)
        {
            if (datetime == "")
            {
                return null;
            }
            else
            {
                return DateTime.ParseExact(datetime, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
        }

        public static DateTime GetDate()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        }

        /// <summary>
        /// NetBanking Signature
        /// </summary>
        /// <param name="RawData"></param>
        /// <returns></returns>
        public string CreateMD5Signature(string RawData)
        {
            /*
             <summary>Creates a MD5 Signature</summary>
             <param name="RawData">The string used to create the MD5 signautre.</param>
             <returns>A string containing the MD5 signature.</returns>
             */

            System.Security.Cryptography.MD5 hasher = System.Security.Cryptography.MD5CryptoServiceProvider.Create();
            byte[] HashValue = hasher.ComputeHash(Encoding.ASCII.GetBytes(RawData));

            string strHex = "";
            foreach (byte b in HashValue)
            {
                strHex += b.ToString("x2");
            }
            return strHex.ToUpper();
        }

        /// <summary>
        /// Response Description
        /// </summary>
        /// <param name="vResponseCode"></param>
        /// <returns></returns>
        public string getResponseDescription(string vResponseCode)
        {
            /* 
             <summary>Maps the vpc_TxnResponseCode to a relevant description</summary>
             <param name="vResponseCode">The vpc_TxnResponseCode returned by the transaction.</param>
             <returns>The corresponding description for the vpc_TxnResponseCode.</returns>
             */
            string result = "Unknown";

            if (vResponseCode.Length > 0)
            {
                switch (vResponseCode)
                {
                    case "0": result = "Transaction Successful"; break;
                    case "1": result = "Transaction Declined"; break;
                    case "2": result = "Bank Declined Transaction"; break;
                    case "3": result = "No Reply from Bank"; break;
                    case "4": result = "Expired Card"; break;
                    case "5": result = "Insufficient Funds"; break;
                    case "6": result = "Error Communicating with Bank"; break;
                    case "7": result = "Payment Server detected an error"; break;
                    case "8": result = "Transaction Type Not Supported"; break;
                    case "9": result = "Bank declined transaction (Do not contact Bank)"; break;
                    case "A": result = "Transaction Aborted"; break;
                    case "B": result = "Transaction Declined - Contact the Bank"; break;
                    case "C": result = "Transaction Cancelled"; break;
                    case "D": result = "Deferred transaction has been received and is awaiting processing"; break;
                    case "F": result = "3-D Secure Authentication failed"; break;
                    case "I": result = "Card Security Code verification failed"; break;
                    case "L": result = "Shopping Transaction Locked (Please try the transaction again later)"; break;
                    case "N": result = "Cardholder is not enrolled in Authentication scheme"; break;
                    case "P": result = "Transaction has been received by the Payment Adaptor and is being processed"; break;
                    case "R": result = "Transaction was not processed - Reached limit of retry attempts allowed"; break;
                    case "S": result = "Duplicate SessionID"; break;
                    case "T": result = "Address Verification Failed"; break;
                    case "U": result = "Card Security Code Failed"; break;
                    case "V": result = "Address Verification and Card Security Code Failed"; break;
                    default: result = "Unable to be determined"; break;
                }
            }
            return result;
        }


        public string displayAVSResponse(string vAVSResultCode)
        {
            /*
             <summary>Maps the vpc_AVSResultCode to a relevant description</summary>
             <param name="vAVSResultCode">The vpc_AVSResultCode returned by the transaction.</param>
             <returns>The corresponding description for the vpc_AVSResultCode.</returns>
             */
            string result = "Unknown";

            if (vAVSResultCode.Length > 0)
            {
                if (vAVSResultCode.Equals("Unsupported"))
                {
                    result = "AVS not supported or there was no AVS data provided";
                }
                else
                {
                    switch (vAVSResultCode)
                    {
                        case "X": result = "Exact match - address and 9 digit ZIP/postal code"; break;
                        case "Y": result = "Exact match - address and 5 digit ZIP/postal code"; break;
                        case "S": result = "Service not supported or address not verified (international transaction)"; break;
                        case "G": result = "Issuer does not participate in AVS (international transaction)"; break;
                        case "A": result = "Address match only"; break;
                        case "W": result = "9 digit ZIP/postal code matched, Address not Matched"; break;
                        case "Z": result = "5 digit ZIP/postal code matched, Address not Matched"; break;
                        case "R": result = "Issuer system is unavailable"; break;
                        case "U": result = "Address unavailable or not verified"; break;
                        case "E": result = "Address and ZIP/postal code not provided"; break;
                        case "N": result = "Address and ZIP/postal code not matched"; break;
                        case "0": result = "AVS not requested"; break;
                        default: result = "Unable to be determined"; break;
                    }
                }
            }
            return result;
        }


        public string displayCSCResponse(string vCSCResultCode)
        {
            /*
             <summary>Maps the vpc_CSCResultCode to a relevant description</summary>
             <param name="vCSCResultCode">The vpc_CSCResultCode returned by the transaction.</param>
             <returns>The corresponding description for the vpc_CSCResultCode.</returns>
             */
            string result = "Unknown";
            if (vCSCResultCode.Length > 0)
            {
                if (vCSCResultCode.Equals("Unsupported"))
                {
                    result = "CSC not supported or there was no CSC data provided";
                }
                else
                {

                    switch (vCSCResultCode)
                    {
                        case "M": result = "Exact code match"; break;
                        case "S": result = "Merchant has indicated that CSC is not present on the card (MOTO situation)"; break;
                        case "P": result = "Code not processed"; break;
                        case "U": result = "Card issuer is not registered and/or certified"; break;
                        case "N": result = "Code invalid or not matched"; break;
                        default: result = "Unable to be determined"; break;
                    }
                }
            }
            return result;
        }

        private System.Collections.Hashtable splitResponse(string rawData)
        {
            /*
             * <summary>This function parses the content of the VPC response
             * <para>This function parses the content of the VPC response to extract the
             * individual parameter names and values. These names and values are then
             * returned as a Hashtable.</para>
             *
             * <para>The content returned by the VPC is a HTTP POST, so the content will
             * be in the format "parameter1=value&parameter2=value&parameter3=value".
             * i.e. key/value pairs separated by ampersands "&".</para>
             *
             * <param name="RawData"> data string containing the raw VPC response content
             * <returns> responseData - Hashtable containing the response data
             */
            System.Collections.Hashtable responseData = new System.Collections.Hashtable();
            try
            {
                // Check if there was a response containing parameters
                if (rawData.IndexOf("=") > 0)
                {
                    // Extract the key/value pairs for each parameter
                    foreach (string pair in rawData.Split('&'))
                    {
                        int equalsIndex = pair.IndexOf("=");
                        if (equalsIndex > 1 && pair.Length > equalsIndex)
                        {
                            string paramKey = System.Web.HttpUtility.UrlDecode(pair.Substring(0, equalsIndex));
                            string paramValue = System.Web.HttpUtility.UrlDecode(pair.Substring(equalsIndex + 1));
                            responseData.Add(paramKey, paramValue);
                        }
                    }
                }
                else
                {
                    // There were no parameters so create an error
                    responseData.Add("vpc_Message", "The data contained in the response was not a valid receipt.<br/>\nThe data was: <pre>" + rawData + "</pre><br/>\n");
                }
                return responseData;
            }
            catch (Exception ex)
            {
                // There was an exception so create an error
                responseData.Add("vpc_Message", "\nThe was an exception parsing the response data.<br/>\nThe data was: <pre>" + rawData + "</pre><br/>\n<br/>\nException: " + ex.ToString() + "<br/>\n");
                return responseData;
            }
        }

        static string str = HttpContext.Current.Request.Url.AbsoluteUri;
        static String originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
        static String parentDirectory = originalPath.Substring(0, originalPath.LastIndexOf("/"));


        public static string SECURE_SECRET()
        {
            return parentDirectory.ToLower().Trim().Contains("blulife.in") ? "5547D223D9E6224040E8B9BCC71FB2B6" : "5547D223D9E6224040E8B9BCC71FB2B6";
        }

        public static string vpc_Merchant()
        {
            return parentDirectory.ToLower().Trim().Contains("blulife.in") ? "TESTBLULIFE" : "TESTBLULIFE";
        }

        public static string vpc_AccessCode()
        {
            return parentDirectory.ToLower().Trim().Contains("blulife.in") ? "1D6C40EE" : "1D6C40EE";
        }

        public static string vpc_ReturnURL()
        {
            return parentDirectory.ToLower().Trim().Contains("blulife.in") ? "http://blulife.in/MemberPanel/Payment_Response.aspx" : "http://graylogic.net/blife/MemberPanel/Payment_Response.aspx";
        }

    }

    public class VPCStringComparer : IComparer
    {

        public VPCStringComparer()
        {

        }

        /*
         <summary>Customised Compare Class</summary>
         <remarks>
         <para>
         The Virtual Payment Client need to use an Ordinal comparison to Sort on 
         the field names to create the MD5 Signature for validation of the message. 
         This class provides a Compare method that is used to allow the sorted list 
         to be ordered using an Ordinal comparison.
         </para>
         </remarks>
         */

        public int Compare(Object a, Object b)
        {
            /*
             <summary>Compare method using Ordinal comparison</summary>
             <param name="a">The first string in the comparison.</param>
             <param name="b">The second string in the comparison.</param>
             <returns>An int containing the result of the comparison.</returns>
             */

            // Return if we are comparing the same object or one of the 
            // objects is null, since we don't need to go any further.
            if (a == b) return 0;
            if (a == null) return -1;
            if (b == null) return 1;

            // Ensure we have string to compare
            string sa = a as string;
            string sb = b as string;

            // Get the CompareInfo object to use for comparing
            System.Globalization.CompareInfo myComparer = System.Globalization.CompareInfo.GetCompareInfo("en-US");
            if (sa != null && sb != null)
            {
                // Compare using an Ordinal Comparison.
                return myComparer.Compare(sa, sb, System.Globalization.CompareOptions.Ordinal);
            }
            throw new ArgumentException("a and b should be strings.");
        }
    }

    public class ShippingAddress
    {
        public string ShippingName { get; set; }
        public string ShipAddress { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string StateID { get; set; }
        public string StateName { get; set; }
        public string Emailid { get; set; }
        public string Mobilbie { get; set; }
        public string Pincode { get; set; }
    }


    public class ViewCart
    {
        public string productid = "";
        public string productname = "";
        public string product_imgurl = "";
        public string Quantity = "";
        public string Unitmrpprice = "";
        public string Totalprice = "";
        public string TshirtSize = "";
        public string TshirtColor = "";

        public ViewCart()
        {

        }

        public ViewCart(string proID, string proName, string pro_Img, string Quan, string unit_Price, string total_Price, string Size, string Color)
        {
            productid = proID;
            productname = proName;
            product_imgurl = pro_Img;
            Quantity = Quan;
            Unitmrpprice = unit_Price;
            Totalprice = total_Price;
            TshirtSize = Size;
            TshirtColor = Color;
        }

    }
}
