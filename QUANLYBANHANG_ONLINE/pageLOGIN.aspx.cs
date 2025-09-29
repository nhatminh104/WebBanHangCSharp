using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using QUANLYBANHANG_ONLINE.App_Code;
namespace QUANLYBANHANG_ONLINE
{
    public partial class pageLOGIN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            XULYDULIEU xuly = new XULYDULIEU();
            SqlParameter[] pr = new SqlParameter[2];
            pr[0] = new SqlParameter("@USERNAME", txtUserName.Text);
            pr[1] = new SqlParameter("@PASSWORD", txtPassWord.Text);
            DataTable tbLOGIN = xuly.getTable("psGetTableLOGIN", pr);
            if (tbLOGIN.Rows.Count > 0)
            {
                Session.Timeout = 2;
                Session["LOGIN"] = tbLOGIN;
                Response.Redirect("pageGIOHANG.aspx");
            }
        }
    }
}