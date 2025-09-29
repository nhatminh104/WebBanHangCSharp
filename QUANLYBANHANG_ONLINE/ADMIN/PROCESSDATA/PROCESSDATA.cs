using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QUANLYBANHANG_ONLINE.App_Code;

namespace QUANLYBANHANG_ONLINE.ADMIN.PROCESSDATA
{
    public class QUANLYSANPHAM_PROCESSDATA
    {
        XULYDULIEU xulydulieu;

        public QUANLYSANPHAM_PROCESSDATA()
        {
            xulydulieu = new XULYDULIEU();
        }

        public DataTable getTableDanhmuc()
        {
            SqlParameter[] pr = new SqlParameter[1];
            pr[0] = new SqlParameter("@MADANHMUC", DBNull.Value);
            return xulydulieu.getTable("psGetTableDANHMUC", pr);
        }

        public DataTable getTableSanPham()
        {
            SqlParameter[] pr = new SqlParameter[1];
            pr[0] = new SqlParameter("@MASANPHAM", DBNull.Value);
            return xulydulieu.getTable("psGetTableSANPHAM", pr);
        }

        public DataTable getTableSanPhamByID(int masanpham)
        {
            SqlParameter[] pr = new SqlParameter[1];
            pr[0] = new SqlParameter("@MASANPHAM", masanpham);
            return xulydulieu.getTable("psGetTableSANPHAM", pr);
        }

        public int InsertRecord(Dictionary<string, object> parameters)
        {
            try
            {
                SqlParameter[] pr = ConvertDictionaryToSqlParameters(parameters);
                return xulydulieu.ExeCute("psInsertRecordSANPHAM", pr);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm sản phẩm: " + ex.Message);
            }
        }

        public int UpdateRecord(Dictionary<string, object> parameters)
        {
            try
            {
                SqlParameter[] pr = ConvertDictionaryToSqlParameters(parameters);
                return xulydulieu.ExeCute("psUpdateRecordSANPHAM", pr);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật sản phẩm: " + ex.Message);
            }
        }

        public int DeleteRecord(Dictionary<string, object> parameters)
        {
            try
            {
                SqlParameter[] pr = ConvertDictionaryToSqlParameters(parameters);
                return xulydulieu.ExeCute("psDeleteRecordSANPHAM", pr);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa sản phẩm: " + ex.Message);
            }
        }

        private SqlParameter[] ConvertDictionaryToSqlParameters(Dictionary<string, object> dict)
        {
            SqlParameter[] pr = new SqlParameter[dict.Count];
            int i = 0;
            foreach (var kv in dict)
            {
                pr[i++] = kv.Value != null ? new SqlParameter(kv.Key, kv.Value) : new SqlParameter(kv.Key, DBNull.Value);
            }
            return pr;
        }


    }
}
