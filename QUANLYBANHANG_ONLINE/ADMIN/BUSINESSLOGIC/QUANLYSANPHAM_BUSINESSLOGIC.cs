using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using QUANLYBANHANG_ONLINE.ADMIN.PROCESSDATA;

namespace QUANLYBANHANG_ONLINE.ADMIN.BUSINESSLOGIC
{
    public class QUANLYSANPHAM_BUSINESSLOGIC
    {
        private QUANLYSANPHAM_PROCESSDATA processdata;
        private Page pageSANPHAM;

        public QUANLYSANPHAM_BUSINESSLOGIC(Page page)
        {
            pageSANPHAM = page;
            processdata = new QUANLYSANPHAM_PROCESSDATA();
        }

        // Set dữ liệu cho dropdown danh mục
        public void SetValueDropdownlistDanhMuc()
        {
            DropDownList drp = (DropDownList)pageSANPHAM.FindControl("drpDANHMUC");
            drp.DataSource = processdata.getTableDanhmuc();
            drp.DataTextField = "TENDANHMUC";
            drp.DataValueField = "MADANHMUC";
            drp.DataBind();
            drp.Items.Insert(0, new ListItem("--Chọn danh mục--", ""));
        }

        // Set dữ liệu cho GridView sản phẩm
        public void SetValueGridViewSanPham()
        {
            GridView grv = (GridView)pageSANPHAM.FindControl("grvSANPHAM");
            grv.DataSource = processdata.getTableSanPham();
            grv.DataBind();
        }

        // Upload ảnh từ FileUpload
        public string UploadAnh()
        {
            FileUpload fileupload = (FileUpload)pageSANPHAM.FindControl("FileANHSANPHAM");
            string fileName = null;
            if (fileupload.HasFile)
            {
                fileName = System.IO.Path.GetFileName(fileupload.PostedFile.FileName);
                string path = pageSANPHAM.Server.MapPath("~/Images/");
                if (!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);
                fileupload.PostedFile.SaveAs(System.IO.Path.Combine(path, fileName));
            }
            return fileName;
        }

        // Thêm mới sản phẩm
        public int InsertRecordSanPham(string tensanpham, int dongia, int soluong, string madanhmuc, string mota, string hinhanh)
        {
            Dictionary<string, object> list = new Dictionary<string, object>();
            list.Add("@TENSANPHAM", tensanpham);
            list.Add("@DONGIA", dongia);
            list.Add("@SOLUONG", soluong);
            list.Add("@HINHANH", hinhanh);
            list.Add("@MOTA", mota);
            list.Add("@MADANHMUC", int.Parse(madanhmuc)); // parse sang int

            return processdata.InsertRecord(list); // SP trong PROCESSDATA đã gọi đúng psInsertRecordSANPHAM
        }

        // Cập nhật sản phẩm
        public int UpdateRecordSanPham(string masanpham, string tensanpham, int dongia, int soluong, string madanhmuc, string mota, string hinhanh)
        {
            Dictionary<string, object> list = new Dictionary<string, object>();
            list.Add("@MASANPHAM", int.Parse(masanpham)); // parse sang int
            list.Add("@TENSANPHAM", tensanpham);
            list.Add("@DONGIA", dongia);
            list.Add("@SOLUONG", soluong);
            list.Add("@HINHANH", hinhanh);
            list.Add("@MOTA", mota);
            list.Add("@MADANHMUC", int.Parse(madanhmuc)); // parse sang int

            return processdata.UpdateRecord(list);
        }

        // Xóa sản phẩm
        public int DeleteRecordSanPham()
        {
            string masanpham = ((TextBox)pageSANPHAM.FindControl("txtMASANPHAM")).Text.Trim();
            if (string.IsNullOrEmpty(masanpham))
                return -1;

            Dictionary<string, object> list = new Dictionary<string, object>();
            list.Add("@MASANPHAM", int.Parse(masanpham)); // parse sang int
            return processdata.DeleteRecord(list);
        }
    }
}
