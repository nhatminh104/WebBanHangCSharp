<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QUANLYSANPHAM_GUI.aspx.cs" Inherits="QUANLYBANHANG_ONLINE.ADMIN.GUI.QUANLYSANPHAM_GUI" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quản lý sản phẩm</title>
    <style type="text/css">
        body { font-family: Arial; margin: 20px; background: #f9f9f9; }
        h2 { text-align: center; margin-bottom: 20px; }
        .form-container {
            width: 700px;
            margin: 0 auto;
            text-align: left;
            padding: 20px;
            border: 1px solid #ddd;
            border-radius: 10px;
            background: #fff;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }
        table { margin-bottom: 10px; width: 100%; }
        .auto-style1 { width: 200px; }
        .gridview { border-collapse: collapse; width: 100%; margin-top: 15px; }
        .gridview th, .gridview td { border: 1px solid #ccc; padding: 6px; text-align: center; }
        .gridview th { background-color: #f2f2f2; }
        img { max-width: 120px; height: auto; }
        .btn { margin-right: 5px; padding: 6px 12px; border-radius: 5px; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>QUẢN LÝ SẢN PHẨM</h2>
        <div class="form-container">
            <table>
                <tr>
                    <td class="auto-style1">Mã danh mục</td>
                    <td><asp:DropDownList ID="drpDANHMUC" runat="server"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="auto-style1">Mã sản phẩm</td>
                    <td><asp:TextBox ID="txtMASANPHAM" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="auto-style1">Tên sản phẩm</td>
                    <td><asp:TextBox ID="txtTENSANPHAM" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="auto-style1">Đơn giá</td>
                    <td><asp:TextBox ID="txtDONGIA" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="auto-style1">Số lượng</td>
                    <td><asp:TextBox ID="txtSOLUONG" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="auto-style1">Upload ảnh</td>
                    <td>
                        <asp:FileUpload ID="FileANHSANPHAM" runat="server" />
                        <asp:HiddenField ID="hfHINHANH" runat="server" />
                    </td>
                </tr>
            </table>
            <asp:TextBox ID="txtMOTA" runat="server" Height="83px" Width="100%" TextMode="MultiLine"></asp:TextBox>
            <br /><br />
            <asp:Button ID="btnInsert" runat="server" Text="Thêm" CssClass="btn" OnClick="btnInsert_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="Xóa" CssClass="btn" OnClick="btnDelete_Click" />
            <asp:Button ID="btnUpdate" runat="server" Text="Sửa" CssClass="btn" OnClick="btnUpdate_Click" />
            <asp:Button ID="btnReset" runat="server" Text="Làm mới" CssClass="btn" OnClick="btnReset_Click" />
        </div>
        <br />
        <asp:GridView ID="grvSANPHAM" runat="server" AutoGenerateColumns="False"
            DataKeyNames="MASANPHAM" CssClass="gridview" Width="100%"
            OnSelectedIndexChanged="grvSANPHAM_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True" SelectText="Chọn" />
                <asp:BoundField DataField="MASANPHAM" HeaderText="Mã SP" ReadOnly="True" />
                <asp:BoundField DataField="TENSANPHAM" HeaderText="Tên SP" />
                <asp:BoundField DataField="DONGIA" HeaderText="Đơn Giá" DataFormatString="{0:N0} VNĐ" />
                <asp:BoundField DataField="SOLUONG" HeaderText="Số Lượng" />
                <asp:TemplateField HeaderText="Hình Ảnh">
                    <ItemTemplate>
                        <asp:Image ID="imgSP" runat="server"
                                   ImageUrl='<%# "~/images/" + Eval("HINHANH") %>'
                                   Width="100px" Height="100px" AlternateText="Ảnh SP" />
                        <asp:HiddenField ID="HF_HINHANH_ROW" runat="server" Value='<%# Eval("HINHANH") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="MOTA" HeaderText="Mô Tả" />
                <asp:BoundField DataField="MADANHMUC" HeaderText="Mã DM" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
