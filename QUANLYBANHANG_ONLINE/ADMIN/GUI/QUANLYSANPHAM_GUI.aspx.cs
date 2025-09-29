using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using QUANLYBANHANG_ONLINE.ADMIN.BUSINESSLOGIC;

namespace QUANLYBANHANG_ONLINE.ADMIN.GUI
{
    public partial class QUANLYSANPHAM_GUI : System.Web.UI.Page
    {
        QUANLYSANPHAM_BUSINESSLOGIC businesslogic;

        protected void Page_Load(object sender, EventArgs e)
        {
            businesslogic = new QUANLYSANPHAM_BUSINESSLOGIC(this);

            if (!IsPostBack)
            {
                businesslogic.SetValueDropdownlistDanhMuc();
                businesslogic.SetValueGridViewSanPham();
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                // --- Validate dữ liệu ---
                if (string.IsNullOrWhiteSpace(txtTENSANPHAM.Text))
                {
                    Response.Write("<script>alert('Tên sản phẩm không được để trống!');</script>");
                    return;
                }

                if (!int.TryParse(txtDONGIA.Text.Trim(), out int dongia))
                {
                    Response.Write("<script>alert('Đơn giá phải là số nguyên!');</script>");
                    return;
                }

                if (!int.TryParse(txtSOLUONG.Text.Trim(), out int soluong))
                {
                    Response.Write("<script>alert('Số lượng phải là số nguyên!');</script>");
                    return;
                }

                if (string.IsNullOrEmpty(drpDANHMUC.SelectedValue))
                {
                    Response.Write("<script>alert('Vui lòng chọn danh mục!');</script>");
                    return;
                }

                // --- Xử lý hình ảnh ---
                string hinhanh = hfHINHANH.Value; // Lấy từ HiddenField nếu đã có
                if (FileANHSANPHAM.HasFile)
                {
                    string fileName = Path.GetFileName(FileANHSANPHAM.PostedFile.FileName);
                    string savePath = Server.MapPath("~/Images/") + fileName;
                    FileANHSANPHAM.SaveAs(savePath);
                    hinhanh = fileName;
                }

                // --- Gọi BusinessLogic để insert ---
                int k = businesslogic.InsertRecordSanPham(
                    txtTENSANPHAM.Text.Trim(),
                    dongia,
                    soluong,
                    drpDANHMUC.SelectedValue,
                    txtMOTA.Text.Trim(),
                    hinhanh
                );

                if (k > 0)
                {
                    Response.Write("<script>alert('Thêm sản phẩm thành công!');</script>");
                    ClearForm();
                }
                else
                {
                    Response.Write("<script>alert('Thêm sản phẩm thất bại!');</script>");
                }

                businesslogic.SetValueGridViewSanPham();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Lỗi: {ex.Message}');</script>");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int k = businesslogic.DeleteRecordSanPham();
            if (k > 0)
            {
                Response.Write("<script>alert('Xóa sản phẩm thành công!');</script>");
                ClearForm();
            }
            else
            {
                Response.Write("<script>alert('Xóa sản phẩm thất bại hoặc không tìm thấy!');</script>");
            }
            businesslogic.SetValueGridViewSanPham();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtMASANPHAM.Text))
                {
                    Response.Write("<script>alert('Vui lòng chọn sản phẩm để cập nhật!');</script>");
                    return;
                }

                if (!int.TryParse(txtDONGIA.Text.Trim(), out int dongia))
                {
                    Response.Write("<script>alert('Đơn giá phải là số nguyên!');</script>");
                    return;
                }

                if (!int.TryParse(txtSOLUONG.Text.Trim(), out int soluong))
                {
                    Response.Write("<script>alert('Số lượng phải là số nguyên!');</script>");
                    return;
                }

                string hinhanh = hfHINHANH.Value;
                if (FileANHSANPHAM.HasFile)
                {
                    string fileName = Path.GetFileName(FileANHSANPHAM.PostedFile.FileName);
                    string savePath = Server.MapPath("~/Images/") + fileName;
                    FileANHSANPHAM.SaveAs(savePath);
                    hinhanh = fileName;
                }

                int k = businesslogic.UpdateRecordSanPham(
                    txtMASANPHAM.Text.Trim(),
                    txtTENSANPHAM.Text.Trim(),
                    dongia,
                    soluong,
                    drpDANHMUC.SelectedValue,
                    txtMOTA.Text.Trim(),
                    hinhanh
                );

                if (k > 0)
                {
                    Response.Write("<script>alert('Cập nhật sản phẩm thành công!');</script>");
                    ClearForm();
                }
                else
                {
                    Response.Write("<script>alert('Cập nhật sản phẩm thất bại!');</script>");
                }

                businesslogic.SetValueGridViewSanPham();
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('Lỗi: {ex.Message}');</script>");
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
            businesslogic.SetValueGridViewSanPham();
            Response.Write("<script>alert('Form đã được làm mới!');</script>");
        }

        protected void grvSANPHAM_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grvSANPHAM.SelectedRow;
            if (row == null) return;

            txtMASANPHAM.Text = row.Cells[1].Text;
            txtTENSANPHAM.Text = row.Cells[2].Text;
            txtDONGIA.Text = row.Cells[3].Text.Replace(" VNĐ", "").Replace(",", "");
            txtSOLUONG.Text = row.Cells[4].Text;
            txtMOTA.Text = row.Cells[6].Text;

            try
            {
                drpDANHMUC.SelectedValue = row.Cells[7].Text;
            }
            catch { }

            HiddenField hfRow = (HiddenField)row.FindControl("HF_HINHANH_ROW");
            if (hfRow != null)
            {
                hfHINHANH.Value = hfRow.Value;
            }
        }

        private void ClearForm()
        {
            txtMASANPHAM.Text = "";
            txtTENSANPHAM.Text = "";
            txtDONGIA.Text = "";
            txtSOLUONG.Text = "";
            txtMOTA.Text = "";
            drpDANHMUC.ClearSelection();
            FileANHSANPHAM.Attributes.Clear();
            hfHINHANH.Value = "";
        }
    }
}
