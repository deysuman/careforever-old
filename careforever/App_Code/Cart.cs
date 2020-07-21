using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace careforever.App_Code
{
    public class Cart
    {
        public DataTable itemTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Productid");
            dt.Columns.Add("Quantity");
            return dt;
        }
        public DataTable itemTable1()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Productid");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Size");
            dt.Columns.Add("Color");
            return dt;
        }

        public DataTable ViewCartTable()
        {
            DataTable Dt_Cart = new DataTable();
            Dt_Cart.Columns.Add("productid");
            Dt_Cart.Columns.Add("productname");
            Dt_Cart.Columns.Add("product_imgurl");
            Dt_Cart.Columns.Add("Quantity");
            Dt_Cart.Columns.Add("Unitmrpprice");
            Dt_Cart.Columns.Add("Totalprice");
            Dt_Cart.Columns.Add("Size");
            Dt_Cart.Columns.Add("Color");
            return Dt_Cart;
        }
    }
}
