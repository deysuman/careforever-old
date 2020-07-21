using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.UI;
using careforever.App_Code;
namespace careforever
{
    public partial class Login : Page
    {
        BLL.BLL balobj = new BLL.BLL();
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            
            if (!IsPostBack)
            {
                //Response.Redirect("Maintenance.aspx");

                if (Request.QueryString.Count > 0 && Request.QueryString["email"] != null && Request.QueryString["pwd"] != null)
                {
                    Session.Clear();
                    string uname = DataSecurity.Decrypt(Request.QueryString["email"].ToString());
                    string pwd = "ABC"; //DataSecurity.Decrypt(Request.QueryString["pwd"].ToString());
                    userlogin(uname, pwd);
                }

                if (GlobalVarables.isAuthenticated(Session))
                {
                    if (Request.QueryString.Count > 0 && Request.QueryString["From"] != null && Request.QueryString["From"].ToString() == "Cart")
                    {
                        Response.Redirect("~/MemberPanel/Repurchase_1.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/MemberPanel/DashBoard.aspx");
                        //Response.Redirect("~/MemberPanel/PromotrackerNov.aspx");
                        //Response.Redirect("~/MemberPanel/PromotionFeb.aspx");
                        //Response.Redirect("~/MemberPanel/AprilPromoTracker.aspx");
                        //Response.Redirect("~/MemberPanel/AnualBonanza.aspx");

                    }

                }

            }
        }

        protected void btnsignin_Click(object sender, EventArgs e)
        {
            try
            {    
                Debug.Write("Hello2");
                userlogin(txtemail.Text, txtpassword.Text);
            }
            catch (Exception ex)
            {

            }
        }

        private void userlogin(string uid, string pwd)
        {    
            Debug.Write("Here");

            //GlobalVarables.logout(Session);
            ds = balobj.Account_Login(uid, pwd);
            Debug.Write(ds.Tables.Count);
            if (GlobalVarables.chkdataset(ds))
            {
                lblError.Visible = false;
                GlobalVarables.login(Session, uid, pwd);
                Session["AccountNo"] = ds.Tables[0].Rows[0]["AccountNo"].ToString();
                Session["SpnosorID"] = ds.Tables[0].Rows[0]["SponserId"].ToString();
                Session["Email"] = ds.Tables[0].Rows[0]["Email"].ToString();
                Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();
                Session["Jointype"] = ds.Tables[0].Rows[0]["Jointype"].ToString();
                Session["Mobile"] = ds.Tables[0].Rows[0]["Mobile"].ToString();
                Session["StateCode"] = ds.Tables[0].Rows[0]["StateCode"].ToString();
                Session["State"] = ds.Tables[0].Rows[0]["State"].ToString();
                Session["RetailPack"] = ds.Tables[0].Rows[0]["RetailPack"].ToString();


                if (Session["Jointype"] != null && Session["Jointype"].ToString().ToLower() == "customer")
                {
                    if (Request.QueryString.Count > 0 && Request.QueryString["From"] != null && Request.QueryString["From"].ToString() == "Cart")
                    {
                        Response.Redirect("~/MemberPanel/Repurchase_1.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/MemberPanel/MyProfile.aspx");

                    }
                }
                else
                {
                    if (Request.QueryString.Count > 0 && Request.QueryString["From"] != null && Request.QueryString["From"].ToString() == "Cart")
                    {
                        Response.Redirect("~/MemberPanel/Repurchase_1.aspx");
                    }
                    else
                    {
                        getdata();


                        //  Response.Redirect("~/MemberPanel/DashBoard.aspx");
                        //Response.Redirect("~/MemberPanel/PromotrackerNov.aspx");
                        //Response.Redirect("~/MemberPanel/PromotionFeb.aspx");
                        //Response.Redirect("~/MemberPanel/AnualBonanza.aspx");
                    }
                }
            }
            else
            {    
                Debug.Write("login error");
                lblError.Visible = true;
            }
        }


        public void getdata()
        {

            DataSet ds1 = new DataSet();
            ds1 = balobj.Get_Data_ById(txtemail.Text, "", "", "Checklogin_status");
            if (ds1.Tables[0].Rows[0]["result"].ToString() == "True")
            {
                textname.Text = ds1.Tables[0].Rows[0]["Name"].ToString();
                txtages.Text = ds1.Tables[0].Rows[0]["dob1"].ToString();
                textaddress.Text = ds1.Tables[0].Rows[0]["Address"].ToString();
                lbldate.Text = ds1.Tables[0].Rows[0]["joindate1"].ToString();
                //this.Orders_ModelPopUp.Show();
            }
            else
            {
                //Response.Redirect("~/MemberPanel/DashBoard.aspx");
                Response.Redirect("~/MemberPanel/MBFMay2020.aspx");
            }

        }

        protected void Btnok_Click(object sender, EventArgs e)
        {

            //this.Orders_ModelPopUp.Hide();
            Response.Redirect("~/MemberPanel/DashBoard.aspx");

        }

        protected void lnkforgotpwd_Click(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString.Count > 0 && Request.QueryString["From"] != null && Request.QueryString["From"].ToString() == "Cart")
                {
                    Response.Redirect("~/Register.aspx?From=CheckOut");
                }
                else
                {
                    Response.Redirect("~/Register.aspx?From=Direct");
                }
            }
            catch { }
        }
    }
}