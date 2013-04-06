using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LookFund.Models.FundInfo;
using TglFirst.Core.Repository;
using System.Data;
using System.Data.SqlClient;
using LookFund.Models.ViewModel;

namespace LookFund.Dao.FundInfo
{
    public interface IHistoryMonth
    {
        DataTable TakeDate(string strGuid);
        /// <summary>
        /// 所有的公积金信息
        /// </summary>
        /// <param name="strGuid"></param>
        /// <returns></returns>
        DataTable FundDate(string strCompany, string strGuid);
        /// <summary>
        /// 除去当月的公积金信息
        /// </summary>
        /// <param name="strGuid"></param>
        /// <returns></returns>
        DataTable FundHistoryDate(string strGuid);
        DataTable FundHistoryDate(FundLookTimeViewModel model, string strGuid);
        /// <summary>
        /// 导出错误数据
        /// </summary>
        /// <returns></returns>
        DataTable ExporError(ErrorViewModel model);

        /// <summary>
        /// 删除错误信息
        /// </summary>
        /// <param name="model"></param>
        void DeleteError(ErrorViewModel model);
    }

    public class DHistoryMonth : RepositoryBase<HistoryMonth>, IHistoryMonth
    {

        public DataTable TakeDate(string strGuid)
        {
            ReadXML readXml = ReadXML.getInstance();
            var sql = Session.GetNamedQuery("CidHistoryDate").QueryString
                .Replace("#ColName", readXml.TakeColumnShow)
                .Replace("#guid", strGuid);
            var cmd = Session.Connection.CreateCommand();
            cmd.CommandText = sql;
            SqlDataAdapter da = new SqlDataAdapter(cmd as SqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable FundDate(string strCompany,string strGuid)
        {
            string tableName = LookTableName.GetTableName(strCompany);
            ReadXML readXml = ReadXML.getInstance();
            var sql = Session.GetNamedQuery("CidHistoryDate").QueryString
                .Replace("#tableName", tableName)
                .Replace("#ColName", readXml.IncomeColumnShow)
                .Replace("#guid", strGuid);
            var cmd = Session.Connection.CreateCommand();
            cmd.CommandText = sql;
            SqlDataAdapter da = new SqlDataAdapter(cmd as SqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable FundHistoryDate(string strGuid)
        {
            ReadXML readXml = ReadXML.getInstance();
            var sql = Session.GetNamedQuery("NotCurrHistory").QueryString
                .Replace("#ColName", readXml.IncomeColumnShow)
                .Replace("#guid", strGuid)
                .Replace("#currYear", DateTime.Now.ToString("yyyyMM"));                
            var cmd = Session.Connection.CreateCommand();
            cmd.CommandText = sql;
            SqlDataAdapter da = new SqlDataAdapter(cmd as SqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable FundHistoryDate(FundLookTimeViewModel model, string strGuid)
        {
            var sql = HistoryDateSql(strGuid).Replace("#TimeWhere", model.GetWhere());
            var cmd = Session.Connection.CreateCommand();
            cmd.CommandText = sql;
            SqlDataAdapter da = new SqlDataAdapter(cmd as SqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        private string HistoryDateSql(string strGuid)
        {
            ReadXML readXml = ReadXML.getInstance();
            string sql = "select c6 from R_2001_10_2001 where C1='" + strGuid + "'";
            var strCompany = Session.CreateSQLQuery(sql).List()[0].ToString();
            string strSql = string.Empty;
            switch (strCompany)
            {
                case "110101":
                    strSql = "select * from (select top 500 '110101' as company," + readXml.IncomeColumnShow + " from R_2001_10_2001JM where C6='" + strGuid + "' #TimeWhere order by c7 desc) as T1 union all";
                    strSql += " select * from (select top 500 '110201' as company," + readXml.IncomeColumnShow + " from R_2001_10_2001XS where C6='" + strGuid + "' #TimeWhere order by c7 desc) as T2 union all";
                    strSql += " select * from (select top 500 '110301' as company," + readXml.IncomeColumnShow + " from R_2001_10_2001FX where C6='" + strGuid + "' #TimeWhere order by c7 desc) as T3 union all";
                    strSql += " select * from (select top 500 '110401' as company," + readXml.IncomeColumnShow + " from R_2001_10_2001HZ where C6='" + strGuid + "' #TimeWhere order by c7 desc) as T4";
                    break;
                case "110201":
                    strSql = "select * from (select top 500 '110201' as company," + readXml.IncomeColumnShow + " from R_2001_10_2001XS where C6='" + strGuid + "' #TimeWhere order by c7 desc) as T1 union all";
                    strSql += " select * from (select top 500 '110101' as company," + readXml.IncomeColumnShow + " from R_2001_10_2001JM where C6='" + strGuid + "' #TimeWhere order by c7 desc) as T2 union all";
                    strSql += " select * from (select top 500 '110301' as company," + readXml.IncomeColumnShow + " from R_2001_10_2001FX where C6='" + strGuid + "' #TimeWhere order by c7 desc) as T3 union all";
                    strSql += " select * from (select top 500 '110401' as company," + readXml.IncomeColumnShow + " from R_2001_10_2001HZ where C6='" + strGuid + "' #TimeWhere order by c7 desc) as T4";
                    break;
                case "110301":
                    strSql = "select * from (select top 500 '110301' as company," + readXml.IncomeColumnShow + " from R_2001_10_2001FX where C6='" + strGuid + "' #TimeWhere order by c7 desc) as T1 union all";
                    strSql += " select * from (select top 500 '110101' as company," + readXml.IncomeColumnShow + " from R_2001_10_2001JM where C6='" + strGuid + "' #TimeWhere order by c7 desc) as T2 union all";
                    strSql += " select * from (select top 500 '110201' as company," + readXml.IncomeColumnShow + " from R_2001_10_2001XS where C6='" + strGuid + "' #TimeWhere order by c7 desc) as T3 union all";
                    strSql += " select * from (select top 500 '110401' as company," + readXml.IncomeColumnShow + " from R_2001_10_2001HZ where C6='" + strGuid + "' #TimeWhere order by c7 desc) as T4";
                    break;
                case "110401":
                    strSql = "select * from (select top 500 '110401' as company," + readXml.IncomeColumnShow + " from R_2001_10_2001HZ where C6='" + strGuid + "' #TimeWhere order by c7 desc) as T1 union all";
                    strSql += " select * from (select top 500 '110101' as company," + readXml.IncomeColumnShow + " from R_2001_10_2001JM where C6='" + strGuid + "' #TimeWhere order by c7 desc) as T2 union all";
                    strSql += " select * from (select top 500 '110201' as company," + readXml.IncomeColumnShow + " from R_2001_10_2001XS where C6='" + strGuid + "' #TimeWhere order by c7 desc) as T3 union all";
                    strSql += " select * from (select top 500 '110301' as company," + readXml.IncomeColumnShow + " from R_2001_10_2001FX where C6='" + strGuid + "' #TimeWhere order by c7 desc) as T4";
                    break;
            }
            return strSql;
        }

        public DataTable ExporError(ErrorViewModel model)
        {
            var cmd = Session.Connection.CreateCommand();
            cmd.CommandText = model.ModelSql();
            SqlDataAdapter da = new SqlDataAdapter(cmd as SqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataRow dr = dt.NewRow();
            dr["A0"] = "年月";
            dr["A11"] = "姓名";
            dr["A15"] = "身份证号";
            dr["A14"] = "职工性别";
            dr["A18"] = "单位名称";
            dr["A16"] = "职工帐号";
            dr["A13"] = "职工状态";
            dr["A20"] = "所属部门";
            dr["A73"] = "开户日期";
            dr["A79"] = "贷款状态";
            dr["A24"] = "期初缴存余额";
            dr["A25"] = "当月汇缴#个人";
            dr["A26"] = "当月汇缴#企业";
            dr["A27"] = "当月汇缴#补缴#个人";
            dr["A28"] = "当月汇缴#补缴#企业";
            dr["A84"] = "当月汇缴#外部转入";
            dr["A44"] = "期末缴存余额";
            dr["A30"] = "调入金额";
            dr["A32"] = "调出金额";
            dr["A41"] = "当年结息";
            dr["A34"] = "购房";
            dr["A35"] = "退休";
            dr["A36"] = "调出集团";
            dr["A37"] = "解聘";
            dr["A38"] = "死亡";
            dr["A39"] = "其它";
            dr["A40"] = "合计";
            dr["C8"] = "错误分类";
            dt.Rows.InsertAt(dr,0);
            dt.Rows.InsertAt(dt.NewRow(), 1);
            return dt;
        }

        public void DeleteError(ErrorViewModel model)
        {
            var cmd = Session.Connection.CreateCommand();
            cmd.CommandText = model.DeleteSql();
            SqlCommand sqlCommand = cmd as SqlCommand;
            sqlCommand.ExecuteNonQuery();
        }
    }
}