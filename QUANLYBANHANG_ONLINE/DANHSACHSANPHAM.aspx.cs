using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QUANLYBANHANG_ONLINE.App_Code;


namespace QUANLYBANHANG_ONLINE
{
	public partial class DANHSACHSANPHAM : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                XULYDULIEU xuly = new XULYDULIEU();
                int madanhmuc;
                if (!int.TryParse(Request.QueryString["MADANHMUC"], out madanhmuc))
                {
                    madanhmuc = 0;
                }

                SqlParameter[] pr = new SqlParameter[1];
                pr[0] = new SqlParameter("@MASANPHAM", DBNull.Value);

                this.DataList1.RepeatColumns = 3;
                this.DataList1.DataSource = xuly.getTable("psGetTableSANPHAM", pr);
                this.DataList1.DataBind();
            }
        }

    }
}