using QUANLYBANHANG_ONLINE.App_Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QUANLYBANHANG_ONLINE
{
    public partial class pageGIOHANG : System.Web.UI.Page
    {
        public void LoadCart()
        {
            if (Session["CART"] != null)
            {
                CART cart = (CART)Session["CART"];
                this.grvCART.DataSource = cart.LISTCARTS.Values.ToList();
                this.grvCART.DataBind();

                // Hiển thị tổng tiền trong footer sau khi DataBind
                if (this.grvCART.FooterRow != null && cart.LISTCARTS.Count > 0)
                {
                    this.grvCART.FooterRow.Cells[0].Text = "Tổng tiền:";
                    this.grvCART.FooterRow.Cells[1].Text = cart.TotalBill().ToString("N0") + " đ";
                    this.grvCART.FooterRow.Cells[2].Text = ""; // Đơn giá
                    this.grvCART.FooterRow.Cells[3].Text = ""; // Số lượng  
                    this.grvCART.FooterRow.Cells[4].Text = cart.TotalBill().ToString("N0") + " đ"; // Thành tiền
                    this.grvCART.FooterRow.Cells[5].Text = ""; // Hình ảnh
                    this.grvCART.FooterRow.Cells[6].Text = ""; // Xóa
                }
            }
            else
            {
                // Nếu không có giỏ hàng, hiển thị thông báo
                this.grvCART.DataSource = null;
                this.grvCART.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CART"] == null)
                {
                    // 👉 Lấy sản phẩm từ database và đưa vào giỏ mặc định
                    CreateCartFromDatabase();
                }
                LoadCart();
            }
        }

        /// <summary>
        /// Lấy dữ liệu sản phẩm từ DB và đưa vào giỏ
        /// </summary>
        private void CreateCartFromDatabase()
        {
            XULYDULIEU xldl = new XULYDULIEU();
            DataTable dt = xldl.GetSanPham();

            CART cart = new CART();
            foreach (DataRow row in dt.Rows)
            {
                string masp = row["MASANPHAM"].ToString();
                string tensp = row["TENSANPHAM"].ToString();
                string hinhanh = row["HINHANH"].ToString();
                decimal dongia = Convert.ToDecimal(row["DONGIA"]);
                int soluong = Convert.ToInt32(row["SOLUONG"]);

                cart.AddCart(masp, tensp, hinhanh, soluong, (double)dongia);
            }

            Session["CART"] = cart;
        }

        protected void btnDELETE_Click(object sender, EventArgs e)
        {
            if (Session["CART"] != null)
            {
                CART cart = (CART)Session["CART"];
                foreach (GridViewRow row in grvCART.Rows)
                {
                    CheckBox ckb = (CheckBox)row.FindControl("chkREMOVEITEM");
                    if (ckb != null && ckb.Checked)
                    {
                        string masanpham = row.Cells[0].Text;
                        cart.RemoveCart(masanpham);
                    }
                }
                Session["CART"] = cart;
                LoadCart();
            }
        }

        protected void grvCART_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
