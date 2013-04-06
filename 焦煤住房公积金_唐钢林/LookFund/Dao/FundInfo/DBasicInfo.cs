using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TglFirst.Core.Repository;
using LookFund.Models.FundInfo;
using System.Data;
using LookFund.Models.Membership;
using LookFund.Dao.Membership;
using TglFirst.Core;
using Spring.Transaction.Interceptor;
using NHibernate;
using NHibernate.Transform;
using System.Collections;
using System.Data.SqlClient;
using LookFund.Models.ViewModel;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.IO;

namespace LookFund.Dao.FundInfo
{
    public interface IBasicInfo
    {
        /// <summary>
        /// 将信息保存到临时表里面。
        /// </summary>
        /// <param name="path">excel路径</param>
        /// <param name="strAdderss">所属单位</param>
        /// <param name="userName">登录名</param>
        /// <returns></returns>
        string uploadTemp(string path, string strAdderss, string userName,string UpFileName);
        /// <summary>
        /// 保存导入的基本数据
        /// </summary>
        /// <returns></returns>
        string ImportBaseSave(string strAdderss,string userName);
        /// <summary>
        /// 保存导入的历史信息
        /// </summary>
        /// <param name="strAdderss"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        string ImprotHistorySave(string strAdderss, string userName);
        /// <summary>
        /// 通过身份证号获取Guid
        /// </summary>
        /// <param name="cid"></param>
        string BaseGuid(string cid);
        /// <summary>
        /// 通过身份证号获取基本信息
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        DataTable BaseInfo(string cid);

        /// <summary>
        /// 通过Guid获取基本信息
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        DataTable BaseInfoGuid(string strguid);

        /// <summary>
        /// 通过登录人员uid判断是否有公积金信息
        /// </summary>
        /// <param name="LoginUid"></param>
        /// <returns></returns>
        bool Exits(string LoginUid);
        /// <summary>
        /// 通过查询条件所回结果
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        DataTable BaseInfo(EmpLookViewModel model, int page, int size, out string HtmlPage);
        /// <summary>
        /// 中心数据导入
        /// </summary>
        /// <param name="path"></param>
        /// <param name="strAdderss"></param>
        void ImportCenter(string path, string strAdderss);
        /// <summary>
        /// 查询市中心数据
        /// </summary>
        /// <param name="idcard"></param>
        DataTable CenterLookInfo(string idcard);
        DataTable CenterLookInfo(EmpLookViewModel model, string empKind, int page, int size, out string HtmlPage);

        string GetEmpKind(string strguid);

        SelectList SelectListUnit(string company);

        SelectList SelectEorrListUnit(string company);
        /// <summary>
        /// 获取所属管理部
        /// </summary>
        /// <param name="card">登陆名(身份证号)</param>
        /// <returns></returns>
        string GetGanlibu(string card);
    }

    public class DBasicInfo : RepositoryBase<BasicInfo>, IBasicInfo
    {

        public string uploadTemp(string path, string strAdderss, string userName,string UpFileName)
        {
            ReadXML readXml = ReadXML.getInstance();
            string strSql = "select " + readXml.BaseImportName + "," + readXml.FundImportName + " from [Sheet1$]";
            ImportExcel excel = new ImportExcel();
            DataTable dt = excel.ReadFile(path, strSql);
            dt.Rows.RemoveAt(0);
            dt.Rows.RemoveAt(0);
            if (dt.Rows.Count > 0)
            {
                string strDate = dt.Rows[0]["A0"].ToString().Trim();
                if (string.IsNullOrEmpty(strDate))
                {
                    return "第一列日期为空";
                }
                if (!Regex.IsMatch(strDate, "[1-2]\\d{3}(0?[1-9]|1[0-2])"))
                {
                    return "第列日期格式不正确";
                }
                dt.Columns.Remove("A0");
                DataColumn column = new DataColumn("A0", typeof(string));
                column.DefaultValue = strDate;
                dt.Columns.Add(column);
                DataColumn column1 = new DataColumn("C6", typeof(string));
                column1.DefaultValue = strAdderss;
                dt.Columns.Add(column1);
                DataColumn column2 = new DataColumn("C7", typeof(string));
                column2.DefaultValue = userName;
                dt.Columns.Add(column2);



                string[] fs = UpFileName.Split('.')[0].Split('_');
                if (fs.Length == 2)
                {
                    DataColumn column50 = new DataColumn("A50", typeof(string));
                    column50.DefaultValue = fs[0];
                    dt.Columns.Add(column50);
                    DataColumn column51 = new DataColumn("A51", typeof(string));
                    column51.DefaultValue = fs[1];
                    dt.Columns.Add(column51);
                }

                SqlBulkCopy _bulk = new SqlBulkCopy((SqlConnection)Session.Connection);
                _bulk.DestinationTableName = "R_2001_10_2001Temp";
                _bulk.WriteToServer(dt);

                var sql = "delete R_2001_10_2001Temp where A16 is null and A15 is null";
                Session.CreateSQLQuery(sql).UniqueResult();
          
            }
            return "上传成功";
        }

        public string ImportBaseSave(string strAdderss, string userName)
        {
            var list = Session.GetNamedQuery("ImproGetMonth")
                .SetString("userName", userName)
                .List();
            string procName = string.Empty;
            switch (strAdderss)
            {
                case "110101":
                    procName = "jmDateImport";
                    break;
                case "110201":
                    procName = "xsDateImport";
                    break;
                case "110301":
                    procName = "fxDateImport";
                    break;
                case "110401":
                    procName = "hzDateImport";
                    break;
                default:
                    return "所属单位不对";
            }
            string strMsg = string.Empty;
            for (int i = 0; i < list.Count; i++)
            {
                string strDate = list[i].ToString();
                var sql = Session.GetNamedQuery("ImprotProc").QueryString
                   .Replace("#procName", procName)
                   .Replace("#procdate", strDate)
                   .Replace("#procuser", userName)
                   .Replace("#proArea", strAdderss);
                var perlist = Session.CreateSQLQuery(sql).List();
                strMsg += strDate.Insert(4, "年") + "月共导入" + perlist[0].ToString() + "人。";
            }
            return "保存成功," + strMsg;
        }

        public string ImprotHistorySave(string strAdderss, string userName)
        {
            var list = Session.GetNamedQuery("ImproGetMonth")
                .SetString("userName", userName)
                .List();
            string procName = string.Empty;
            switch (strAdderss)
            {
                case "110101":
                    procName = "jmHisDateImport";
                    break;
                case "110201":
                    procName = "xsHisDateImport";
                    break;
                case "110301":
                    procName = "fxHisDateImport";
                    break;
                case "110401":
                    procName = "hzHisDateImport";
                    break;
                default:
                    return "所属单位不对";
            }
            string strMsg = string.Empty;
            for (int i = 0; i < list.Count; i++)
            {
                string strDate = list[i].ToString();
                var sql = Session.GetNamedQuery("ImprotProc").QueryString
                   .Replace("#procName", procName)
                   .Replace("#procdate", strDate)
                   .Replace("#procuser", userName)
                   .Replace("#proArea", strAdderss);
                var perlist = Session.CreateSQLQuery(sql).List();
                strMsg += strDate.Insert(4, "年") + "月共导入" + perlist[0].ToString() + "人。";
            }
            return "保存成功," + strMsg;
        }

        public void ImportCenter(string path, string strAdderss)
        {
            string strSql = "select 单位账号,职工账号,职工姓名,性别,身份证号,本金余额,本年利息,实存总额 from [Sheet1$]";
            ImportExcel excel = new ImportExcel();
            DataTable dt = excel.ReadFile(path, strSql);
            DataColumn column = new DataColumn("C6", typeof(string), strAdderss);
            dt.Columns.Add(column);
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
            SqlBulkCopy _bulk = new SqlBulkCopy((SqlConnection)Session.Connection);
            _bulk.DestinationTableName = "R_2001_10_3001";
            _bulk.WriteToServer(dt);
        }

        public DataTable CenterLookInfo(string idcard)
        {
            string sql = "select  * from R_2001_10_3001 where A15 like '" + idcard + "%'";
            var cmd = Session.Connection.CreateCommand();
            cmd.CommandText = sql;
            SqlDataAdapter da = new SqlDataAdapter(cmd as SqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable CenterLookInfo(EmpLookViewModel model, string empKind, int page, int size, out string HtmlPage)
        {
            #region 查询条件
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(model.EmpName))
            {
                strWhere += "and A13 like '" + model.EmpName + "%'";
            }
            if (!string.IsNullOrEmpty(model.IdCard))
            {
                strWhere += " and A15 like '" + model.IdCard + "%'";
            }
            if (!model.strCompany.Equals("0"))
            {
                strWhere += " and C6='" + model.strCompany + "'";
            }
            #endregion
            var SqlCount = "select count(*) from R_2001_10_3001 where C6 like '" + empKind + "%' " + strWhere;
            int rows = Convert.ToInt32(Session.CreateSQLQuery(SqlCount).List()[0]);
            int yu = rows % size;
            int count = yu == 0 ? rows / size : rows / size + 1;
            page = page >= count ? count - 1 : page;
            int minpage = page - 5 > 0 ? page - 5 : 0;
            int maxpage = page + 6 < count ? page + 6 : count;
            HtmlPage = "<span style=\"font-size:20px;padding-right:20px;\">共" + rows + "人</span><span style='font-size:20px;'>共" + count + "页</span>&nbsp;&nbsp;&nbsp;&nbsp;";
            for (int i = minpage; i < maxpage; i++)
            {
                if (i == page)
                    HtmlPage += "<span style=\"font-size:25px; padding-right:5px;\"><b id='currentPage'>" + (i + 1) + "</b></span>";
                else
                    HtmlPage += "<a href=\"javascript:NextPageCenter(" + i + ")\"><span style=\"font-size:20px; padding-right:5px;\">" + (i + 1) + "</span></a>";
            }
            string sql = "select top " + size + " * from (select *,ROW_NUMBER() OVER(ORDER BY CURRENT_TIMESTAMP) as rowsnumber " +
            "from R_2001_10_3001 where C6 like '" + empKind + "%' " + strWhere + ") as T where T.rowsnumber>" + page * size;
            var cmd = Session.Connection.CreateCommand();
            cmd.CommandText = sql;
            SqlDataAdapter da = new SqlDataAdapter(cmd as SqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public string BaseGuid(string cid)
        {
            var sql = Session.GetNamedQuery("BaseGuid").QueryString;
            var list= Session.CreateSQLQuery(sql)
               .SetString("Cid", cid)
               .List();
            if (list.Count > 0)
                return list[0].ToString();
            return string.Empty;
        }

        public DataTable BaseInfo(string cid)
        {
            ReadXML readXml = ReadXML.getInstance();
            var sql = Session.GetNamedQuery("CidBaseInfo").QueryString
                .Replace("#ColName", readXml.BaseColumnShow)
                .Replace("#cid", cid);
            var cmd = Session.Connection.CreateCommand();
            cmd.CommandText = sql;
            SqlDataAdapter da = new SqlDataAdapter(cmd as SqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }

        public DataTable BaseInfoGuid(string strguid)
        {
            ReadXML readXml = ReadXML.getInstance();
            var sql = Session.GetNamedQuery("guidBaseInfo").QueryString
                .Replace("#ColName", readXml.BaseColumnShow)
                .Replace("#strguid", strguid);
            var cmd = Session.Connection.CreateCommand();
            cmd.CommandText = sql;
            SqlDataAdapter da = new SqlDataAdapter(cmd as SqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable BaseInfo(EmpLookViewModel model, int page, int size, out string HtmlPage)
        {
            #region 查询条件
            ReadXML readXml = ReadXML.getInstance();
            string[] cols = readXml.EmpLookColumn.Split(',');
            string strWhere = string.Empty;
            string tableName = LookTableName.GetTableName(model.strCompany);
            if (!string.IsNullOrEmpty(model.EmpName))
            {
                if (string.IsNullOrEmpty(strWhere))
                    strWhere = " where a." + cols[0] + " like '" + model.EmpName + "%'";
                else
                    strWhere += "and a." + cols[0] + " like '" + model.EmpName + "%'";
            }
            if (!string.IsNullOrEmpty(model.IdCard))
            {
                if (string.IsNullOrEmpty(strWhere))
                    strWhere = " where a." + cols[1] + " like '" + model.IdCard + "%'";
                else
                    strWhere += " and a." + cols[1] + " like '" + model.IdCard + "%'";
            }
            if (!"0".Equals(model.unitName))
            {
                if (string.IsNullOrEmpty(strWhere))
                    strWhere = " where (a." + cols[3] + "='" + model.unitName + "' or a.A17 is null)";
                else
                    strWhere += " and (a." + cols[3] + "='" + model.unitName + "' or a.A17 is null)";
            }
            if (!string.IsNullOrEmpty(model.MothDate))
            {
                string strMonth = model.MothDate.Replace("-", "");
                if (string.IsNullOrEmpty(strWhere))
                    strWhere = " where b.c7='" + strMonth + "'";
                else
                    strWhere += " and b.c7='" + strMonth + "'";
            }
            #endregion
            var SqlCount = Session.GetNamedQuery("whereBaseInfoCount").QueryString
                .Replace("#tableName", tableName)
                .Replace("#strWhere", strWhere);
            int rows = Convert.ToInt32(Session.CreateSQLQuery(SqlCount).List()[0]);
            int yu = rows % size;
            int count = yu == 0 ? rows / size : rows / size + 1;
            page = page >= count ? count - 1 : page;
            int minpage = page - 5 > 0 ? page - 5 : 0;
            int maxpage = page + 6 < count ? page + 6 : count;
            HtmlPage = "<span style=\"font-size:20px;padding-right:20px;\">共" + rows + "人</span><span style='font-size:20px;'>共" + count + "页</span>&nbsp;&nbsp;&nbsp;&nbsp;";
            for (int i = minpage; i < maxpage; i++)
            {
                if (i == page)
                    HtmlPage += "<span style=\"font-size:25px; padding-right:5px;\"><b id='currentPage'>" + (i + 1) + "</b></span>";
                else
                    HtmlPage += "<a href=\"javascript:NextPageBase(" + i + ")\"><span style=\"font-size:20px; padding-right:5px;\">" + (i + 1) + "</span></a>";
            }
            var sql = Session.GetNamedQuery("whereBaseInfo").QueryString
                .Replace("#size", size.ToString())
                .Replace("#ColName", readXml.EmpShowColumn)
                .Replace("#tableName", tableName)
                .Replace("#strWhere", strWhere)
                .Replace("#rowsNuber", (page * 20).ToString());
            var cmd = Session.Connection.CreateCommand();
            cmd.CommandText = sql;
            SqlDataAdapter da = new SqlDataAdapter(cmd as SqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public bool Exits(string LoginUid)
        {
            var sql = Session.GetNamedQuery("BaseLoginUid").QueryString
               .Replace("#loginUid", LoginUid);
            string str= Session.CreateSQLQuery(sql).List()[0].ToString();
            if (str.Equals("0"))
            {
                return false;
            }
            return true;
        }

        public string GetEmpKind(string strguid)
        {
            var sql = Session.GetNamedQuery("GetEmpKind").QueryString;
            var list = Session.CreateSQLQuery(sql)
                .SetString("strGuid", strguid)
                .List();
            if (list.Count > 0)
                return list[0].ToString();
            return "22";
        }

        public SelectList SelectListUnit(string company)
        {
            var sql = Session.GetNamedQuery("selectListUnit").QueryString
                .Replace("#company",company);
            var cmd = Session.Connection.CreateCommand();
            cmd.CommandText = sql;
            SqlDataAdapter da = new SqlDataAdapter(cmd as SqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            var dic = new Dictionary<string, string>();
            dic.Add("0", "==请选择==");
            foreach (DataRow item in dt.Rows)
            {
                if (item["sValue"] == null)
                {
                    dic.Add("","");
                }
                else
                {
                    try
                    {
                        dic.Add(item["sValue"].ToString(), item["sText"].ToString());
                    }catch
                    {
                    }
                }
            }
            return new SelectList(dic, "Key", "Value");
        }

        public SelectList SelectEorrListUnit(string company)
        {
            var sql = string.Format(@Session.GetNamedQuery("selectEorrListUnit").QueryString, company);
                ;
            var cmd = Session.Connection.CreateCommand();
            cmd.CommandText = sql;
            SqlDataAdapter da = new SqlDataAdapter(cmd as SqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            var dic = new Dictionary<string, string>();
            dic.Add("0", "==请选择==");
            foreach (DataRow item in dt.Rows)
            {
                if (item["sValue"] == null)
                {
                    dic.Add("", "");
                }
                else
                {
                    dic.Add(item["sValue"].ToString(), item["sText"].ToString());
                }
            }
            return new SelectList(dic, "Key", "Value");
        }

        public string GetGanlibu(string card)
        {
            string sql = "select c6 from R_2001_10_2001 where c8='" + card + "'";
            var list =  Session.CreateSQLQuery(sql).List();
            if (list.Count > 0)
                return list[0].ToString();
            return "";
        }
    }
}