using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TglFirst.Core.Repository;
using LookFund.Models.FundInfo;
using System.Data;
using NHibernate.Transform;
using System.Collections;
using System.Data.SqlClient;
using LookFund.Models.ViewModel;

namespace LookFund.Dao.FundInfo
{
    public interface ICurrentMonth
    {
        /// <summary>
        /// 判断是否已有本月数据
        /// </summary>
        /// <param name="cid">身份证号</param>
        /// <param name="time">年月</param>
        /// <returns>有反回True</returns>
        bool Exits(string cid, string time);

        IList DateMonth(int page, int size,out string HtmlPage,string empKind);
        /// <summary>
        /// 验证通过
        /// </summary>
        /// <param name="cid"></param>
        void VerifySucced(string strGuid);
        /// <summary>
        /// 整页验证通过
        /// </summary>
        /// <param name="arrGuid"></param>
        void PageSucced(string[] arrGuid);
        /// <summary>
        /// 全部验证通过
        /// </summary>
        void SuccedAll(string empKind);
        /// <summary>
        /// 通过身份证号获取公积金数据
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        DataTable DateMonth(string strGuid);

        /// <summary>
        /// 统计上报情况
        /// </summary>
        /// <returns></returns>
        DataTable ReportState(ReportStatViewModel model, string empKind, int page, int size, out string HtmlPage);
    }

    public class DCurrentMonth : RepositoryBase<CurrentMonth>, ICurrentMonth
    {
        public bool Exits(string cid, string time)
        {
            var Sql = Session.GetNamedQuery("fundExits").QueryString;
            var list = Session.CreateSQLQuery(Sql)
                .SetString("empcid", cid)
                .SetString("time", time)
                .List();
            return list[0].ToString().Equals("0") ? false : true;
            
        }

        public IList DateMonth(int page, int size, out string HtmlPage, string empKind)
        {
            ReadXML readXml = ReadXML.getInstance();
            var Sql = Session.GetNamedQuery("DateMonth").QueryString
                .Replace("#ColName", readXml.VerifyShow)
                .Replace("#empKind", empKind);

            var SqlCount = Session.GetNamedQuery("MonthCount").QueryString.Replace("#empKind", empKind);
            int rows = Convert.ToInt32(Session.CreateSQLQuery(SqlCount).List()[0]);
            int yu = rows % size;
            int count = yu == 0 ? rows / size : rows / size + 1;

            page = page >= count ? count - 1 : page;
            int minpage = page - 5 > 0 ? page - 5 : 0;
            int maxpage = page + 6 < count ? page + 6 : count;
            HtmlPage = "<span style=\"font-size:25px; padding-right:20px;\">共" + rows + "行</span><span style='font-size:20px;'>共" + count + "页</span>&nbsp;&nbsp;&nbsp;&nbsp;";
            for (int i = minpage; i < maxpage; i++)
            {
                if (i == page)
                    HtmlPage += "<span style=\"font-size:25px; padding-right:5px;\"><b id='currentPage'>" + (i + 1) + "</b></span>";
                else
                    HtmlPage += "<a href=\"javascript:NextPage(" + i + ")\"><span style=\"font-size:20px; padding-right:5px;\">" + (i + 1) + "</span></a>";
            }
            var list = Session.CreateSQLQuery(Sql)
                .SetFirstResult(page * size)
                .SetMaxResults(size)
                .List();

            string sumColumn = string.Empty;
            string[] StatArr = readXml.VerifyStat.Split(',');
            foreach (var item in StatArr)
            {
                sumColumn += ",sum(convert(float," + item + ")) as " + item;
            }
            var statSql = Session.GetNamedQuery("VerifyStat").QueryString
                .Replace("#StatColumn", sumColumn);
            DataTable dt = GetTable(statSql);
            object[] objArr = null;
            var verifyCol = readXml.VerifyColumn.Split(',');
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                objArr = new object[verifyCol.Length + 1];
                objArr[0] = "合计";
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    for (int n = 0; n < verifyCol.Length; n++)
                    {
                        if (("b." + dt.Columns[col].ColumnName).Equals(verifyCol[n]))
                        {
                            objArr[n] = dt.Rows[i][col].ToString();
                            break;
                        }
                    }
                }
                objArr[12] = dt.Rows[i]["c7"].ToString();
                list.Insert(0, objArr);
            }
            return list;
        }

        public void VerifySucced(string strGuid)
        {
            var sql = Session.GetNamedQuery("VerifySucced").QueryString;
            Session.CreateSQLQuery(sql)
                .SetString("guid", strGuid)
                .UniqueResult();
        }

        public void PageSucced(string[] arrGuid)
        {
            var sql = Session.GetNamedQuery("VerifySucced").QueryString;
            foreach (var strGuid in arrGuid)
            {                
                Session.CreateSQLQuery(sql)
                    .SetString("guid", strGuid)
                    .UniqueResult();
            }
        }

        public void SuccedAll(string empKind)
        {
            var sql = Session.GetNamedQuery("SuccedAll").QueryString.Replace("#empKind", empKind);
            Session.CreateSQLQuery(sql)
                .UniqueResult();
        }

        public DataTable DateMonth(string strGuid)
        {
            string tableName = LookTableName.GetTableName(GetCompany(strGuid));
            ReadXML readXml = ReadXML.getInstance();
            var sql = Session.GetNamedQuery("CidDateMonth").QueryString
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

        public DataTable ReportState(ReportStatViewModel model,string empKind, int page, int size, out string HtmlPage)
        {
            var SqlCount = ReportMonthNumber(empKind)
                .Replace("#sDate", model.StatDate)
                .Replace("#eDate", model.EndDate)
                .Replace("#empKind", empKind);
            var SizeList = Session.CreateSQLQuery(SqlCount).List();
            if (SizeList.Count > 0)
            {
                var rows = SizeList.Count;
                int yu = rows % size;
                int count = yu == 0 ? rows / size : rows / size + 1;

                page = page >= count ? count - 1 : page;
                int minpage = page - 5 > 0 ? page - 5 : 0;
                int maxpage = page + 6 < count ? page + 6 : count;

                HtmlPage = "<span style='font-size:20px;'>共" + count + "页</span>&nbsp;&nbsp;&nbsp;&nbsp;";
                for (int i = minpage; i < maxpage; i++)
                {
                    if (i == page)
                        HtmlPage += "<span style=\"font-size:25px; padding-right:5px;\"><b id='currentPage'>" + (i + 1) + "</b></span>";
                    else
                        HtmlPage += "<a href=\"javascript:NextPageStat(" + i + ")\"><span style=\"font-size:20px; padding-right:5px;\">" + (i + 1) + "</span></a>";
                }
                var edate = (SizeList[page * size] as IList)[0].ToString();
                int s = page * size + size - 1;
                if (s >= SizeList.Count)
                    s = SizeList.Count - 1;
                var sdate = (SizeList[s] as IList)[0].ToString();
                var sql = ReportState(empKind)
                    .Replace("#sDate", sdate)
                    .Replace("#eDate", edate)
                    .Replace("#empKind", empKind);
                var cmd = Session.Connection.CreateCommand();
                cmd.CommandText = sql;
                SqlDataAdapter da = new SqlDataAdapter(cmd as SqlCommand);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            HtmlPage = string.Empty;
            return new DataTable();
        }

        /// <summary>
        /// 要统计时间
        /// </summary>
        /// <param name="empKind"></param>
        /// <returns></returns>
        private string ReportMonthNumber(string empKind)
        {
            string sql = string.Empty;
            switch (empKind)
            {
                case"11":
                    sql = @"select *,count(*) from (select b.c7 from R_2001_10_2001 a inner join R_2001_10_2001JM b on a.c1=b.c6 
                            where b.c7 BETWEEN '#sDate' and '#eDate' union all
                            select b.c7 from R_2001_10_2001 a inner join R_2001_10_2001XS b on a.c1=b.c6 
                            where b.c7 BETWEEN '#sDate' and '#eDate' union all
                            select b.c7 from R_2001_10_2001 a inner join R_2001_10_2001HZ b on a.c1=b.c6 
                            where b.c7 BETWEEN '#sDate' and '#eDate' union all
                            select b.c7 from R_2001_10_2001 a inner join R_2001_10_2001Fx b on a.c1=b.c6 
                            where b.c7 BETWEEN '#sDate' and '#eDate') as T group by T.c7 order by T.c7 desc";
                    break;
                case "1101":
                    sql = @"select b.c7,count(*) from R_2001_10_2001 a inner join R_2001_10_2001JM b on a.c1=b.c6 
                        where a.c6 like '#empKind%' and b.c7 BETWEEN '#sDate' and '#eDate' group by b.c7 order by b.c7 desc";
                    break;
                case "1102":
                    sql = @"select b.c7,count(*) from R_2001_10_2001 a inner join R_2001_10_2001XS b on a.c1=b.c6 
                        where a.c6 like '#empKind%' and b.c7 BETWEEN '#sDate' and '#eDate' group by b.c7 order by b.c7 desc";
                    break;
                case "1103":
                    sql = @"select b.c7,count(*) from R_2001_10_2001 a inner join R_2001_10_2001FX b on a.c1=b.c6 
                        where a.c6 like '#empKind%' and b.c7 BETWEEN '#sDate' and '#eDate' group by b.c7 order by b.c7 desc";
                    break;
                case "1104":
                    sql = @"select b.c7,count(*) from R_2001_10_2001 a inner join R_2001_10_2001HZ b on a.c1=b.c6 
                        where a.c6 like '#empKind%' and b.c7 BETWEEN '#sDate' and '#eDate' group by b.c7 order by b.c7 desc";
                    break;
                default:            
                    break;
            }
            return sql;
        }

        private string ReportState(string empKind)
        {
            string sql = string.Empty;
            switch (empKind)
            {
                case "11":
                    sql = @"select *,count(*) as number from (
                        select b.c7,a.c6,a.A17  from R_2001_10_2001 a inner join R_2001_10_2001JM b on a.c1=b.c6 
                        where a.c6='110101' and b.c7 BETWEEN '#sDate' and '#eDate' union all
                        select b.c7,a.c6,a.A17  from R_2001_10_2001 a inner join R_2001_10_2001XS b on a.c1=b.c6 
                        where a.c6='110201' and b.c7 BETWEEN '#sDate' and '#eDate' union all
                        select b.c7,a.c6,a.A17  from R_2001_10_2001 a inner join R_2001_10_2001FX b on a.c1=b.c6 
                        where a.c6='110301' and b.c7 BETWEEN '#sDate' and '#eDate' union all
                        select b.c7,a.c6,a.A17  from R_2001_10_2001 a inner join R_2001_10_2001HZ b on a.c1=b.c6 
                        where a.c6='110401' and b.c7 BETWEEN '#sDate' and '#eDate') as T group by T.c7,T.c6,T.A17 order by T.c7 desc,T.c6";
                    break;
                case "1101":
                    sql = @"select b.c7,a.c6,a.A17, count(*) as number from R_2001_10_2001 a inner join R_2001_10_2001JM b on a.c1=b.c6 
                        where a.c6 like '#empKind%' and b.c7 BETWEEN '#sDate' and '#eDate' group by b.c7,a.c6,a.A17  order by b.c7 desc,a.c6";
                    break;
                case "1102":
                    sql = @"select b.c7,a.c6,a.A17, count(*) as number from R_2001_10_2001 a inner join R_2001_10_2001XS b on a.c1=b.c6 
                        where a.c6 like '#empKind%' and b.c7 BETWEEN '#sDate' and '#eDate' group by b.c7,a.c6,a.A17  order by b.c7 desc,a.c6";
                    break;
                case "1103":
                    sql = @"select b.c7,a.c6,a.A17, count(*) as number from R_2001_10_2001 a inner join R_2001_10_2001FX b on a.c1=b.c6 
                        where a.c6 like '#empKind%' and b.c7 BETWEEN '#sDate' and '#eDate' group by b.c7,a.c6,a.A17  order by b.c7 desc,a.c6";
                    break;
                case "1104":
                    sql = @"select b.c7,a.c6,a.A17, count(*) as number from R_2001_10_2001 a inner join R_2001_10_2001HZ b on a.c1=b.c6
                         where a.c6 like '#empKind%' and b.c7 BETWEEN '#sDate' and '#eDate' group by b.c7,a.c6,a.A17  order by b.c7 desc,a.c6";
                    break;
                default:
                    break;
            }
            return sql;
        }

        private DataTable GetTable(string sql)
        {
            var cmd = Session.Connection.CreateCommand();
            cmd.CommandText = sql;
            SqlDataAdapter da = new SqlDataAdapter(cmd as SqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        private string GetCompany(string strGuid)
        {
            string sql = "select c6 from R_2001_10_2001 where C1='" + strGuid + "'";
            return Session.CreateSQLQuery(sql).List()[0].ToString();
        }
    }
}