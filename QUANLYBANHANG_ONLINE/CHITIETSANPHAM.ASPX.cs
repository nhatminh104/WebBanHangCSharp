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
                Repeater1.DataSource = tbSANPHAM;
                Repeater1.DataBind();
            }
        }

        protected void Imagecart_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            string masanpham = btn.CommandArgument; // đây là MASANPHAM từ CommandArgument

            XULYDULIEU xuly = new XULYDULIEU();
            SqlParameter[] pr = new SqlParameter[1];
            pr[0] = new SqlParameter("@MASANPHAM", masanpham);
            DataTable tbSANPHAM = xuly.getTable("psGetTableSANPHAM", pr);

            if (tbSANPHAM != null && tbSANPHAM.Rows.Count > 0)
            {
                string tensanpham = tbSANPHAM.Rows[0]["TENSANPHAM"].ToString();
                double dongia = Convert.ToDouble(tbSANPHAM.Rows[0]["DONGIA"]);
                string hinhanh = tbSANPHAM.Rows[0]["HINHANH"].ToString();

                // Lấy giỏ hàng từ session hoặc tạo mới
                CART cart = Session["CART"] as CART ?? new CART();
                cart.AddCart(masanpham, tensanpham, hinhanh, 1, dongia);
                Session["CART"] = cart;

                // Chuyển sang trang giỏ hàng
                Response.Redirect("pageGIOHANG.aspx");
            }
        }
    }
}
