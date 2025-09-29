using QUANLYBANHANG_ONLINE.App_Code;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace QUANLYBANHANG_ONLINE
{
	public partial class MasterPageUser : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			XULYDULIEU xuly = new XULYDULIEU();
				SqlParameter[] pr = new SqlParameter[1];
			pr[0] = new SqlParameter("@MADANHMUC", DBNull.Value);
			this.Repeater1.DataSource = xuly.getTable("psGetTableDANHMUC", pr);
			this.Repeater1.DataBind();
		}
	}
}