using QUANLYBANHANG_ONLINE.App_Code;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QUANLYBANHANG_ONLINE
{
    public partial class CHITIETSANPHAM : System.Web.UI.Page
    {
        DataTable tbSANPHAM;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSanPham();
            }
        }

        private void LoadSanPham()
        {
            XULYDULIEU xuly = new XULYDULIEU();
            string masanpham = Request.QueryString.Get("MASANPHAM");
            SqlParameter[] pr = new SqlParameter[1];
            pr[0] = new SqlParameter("@MASANPHAM", masanpham);
            tbSANPHAM = xuly.getTable("psGetTableSANPHAM", pr);

            if (tbSANPHAM != null && tbSANPHAM.Rows.Count > 0)
            {
                this.Repeater1.DataSource = tbSANPHAM;
                this.Repeater1.DataBind();
            }
        }

        protected void Imagecart_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            string masanpham = btn.CommandArgument;

            XULYDULIEU xuly = new XULYDULIEU();
            SqlParameter[] pr = new SqlParameter[1];
            pr[0] = new SqlParameter("@MASANPHAM", masanpham);
            DataTable tbSANPHAM = xuly.getTable("psGetTableSANPHAM", pr);

            if (tbSANPHAM != null && tbSANPHAM.Rows.Count > 0)
            {
                string tensanpham = tbSANPHAM.Rows[0]["TENSANPHAM"].ToString();
                double dongia = Convert.ToDouble(tbSANPHAM.Rows[0]["DONGIA"]);
                string hinhanh = tbSANPHAM.Rows[0]["HINHANH"].ToString();

                CART cart = Session["CART"] as CART ?? new CART();
                cart.AddCart(masanpham, tensanpham, hinhanh, 1, dongia);
                Session["CART"] = cart;

                Response.Redirect("pageGIOHANG.aspx");
            }
        }
    }
}
