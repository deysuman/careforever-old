using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace careforever
{
    public partial class MasterPageNew : System.Web.UI.MasterPage
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        BLL.BLL objbll = new BLL.BLL();
        //string Url = new GlobalVarables().Current_Url_Client();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (isAuthenticated(Session))
                {
                    lnkbtnRegister.InnerText = "My Account [ " + Session["Name"] + " ]";
                    if (Session["Jointype"] != null && Session["Jointype"].ToString().ToLower() == "customer")
                    {
                        lnkbtnRegister.HRef = "~/MemberPanel/MyProfile.aspx";
                    }
                    else
                    {
                        lnkbtnRegister.HRef = "~/MemberPanel/DashBoard.aspx";
                    }
                    lnkbtnLogin.InnerText = "Logout";
                    lnkbtnLogin.HRef = "~/MemberPanel/Logout.aspx";
                }
                else
                {
                    lnkbtnRegister.InnerText = "Register";
                    lnkbtnRegister.HRef = "Register.aspx?From=Direct";

		    //lnkbtnRegister.HRef = "Maintenance.aspx";

                    lnkbtnLogin.InnerText = "Login";
                    lnkbtnLogin.HRef = "Login.aspx";

		    //lnkbtnLogin.HRef = "Maintenance.aspx";

                }
            }
            //BindCart();
        }
        
        public static bool isAuthenticated(System.Web.SessionState.HttpSessionState session)
        {
            return session["member"] != null;
        }
        
    }
}
