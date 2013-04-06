using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LookFund.Models.ViewModel
{
    public class ErrorViewModel
    {
        /// <summary>
        /// 所属单位
        /// </summary>
        public string strUnit
        {
            get;
            set;
        }

        /// <summary>
        /// 单位名称
        /// </summary>
        public string unitName
        {
            get;
            set;
        }

        private string _strDate = string.Empty;
        /// <summary>
        /// 时间
        /// </summary>
        public string strDate
        {
            get
            {
                if (!string.IsNullOrEmpty(_strDate))
                    return _strDate.Trim().Replace("-", "");
                return "";
            }
            set
            {
                _strDate = value;
            }
        }

        /// <summary>
        /// 错误原因
        /// </summary>
        public string errorType
        {
            get;
            set;
        }

        public string DeleteSql()
        {
            string strSql=string.Format("delete from R_2001_10_2001_{0}Error ", strUnit);
            return GetStrSql(strSql);
        }

        public string ModelSql()
        {
            string strSql = string.Format("select [A0],[A11],[A15],[A14],[A18],[A16],[A13],[A20],[A73],[A79],[A24],[A25],[A26],[A27],[A28],[A84],[A44],[A30],[A32],[A41],[A34],[A35],[A36],[A37],[A38],[A39],[A40],[C8] from R_2001_10_2001_{0}Error ", strUnit);
            return GetStrSql(strSql);
        }

        private string GetStrSql(string strSql)
        {
            switch (errorType)
            {
                case "1":
                    strSql += "where" + ModelWhere() + " and c8='职工账号或身份证号为空'";
                    break;
                case "2":
                    strSql += "where" + ModelWhere() + " and c8='身份证号错误'";
                    break;
                case "3":
                    strSql += "where" + ModelWhere() + " and c8='职工账号重复' order by A16";
                    break;
                case "4":
                    strSql += "where A16 in(select a16 from R_2001_10_2001_{0}Error where " + ModelWhere() + " and c8='身份证号重复' ";
                    strSql += "group by A16 having count(*)=1) and " + ModelWhere() + " and c8='身份证号重复' order by A15";
                    strSql = string.Format(strSql, strUnit);
                    break;
                case "5":
                    strSql += "where A16 in(select a16 from R_2001_10_2001_{0}Error where " + ModelWhere() + " and c8='身份证号重复' ";
                    strSql += "group by A16 having count(*)>1) and " + ModelWhere() + " and c8='身份证号重复' order by A15";
                    strSql = string.Format(strSql, strUnit);
                    break;
                case "6":
                    strSql += "where " + ModelWhere() + " and c8='身份证号和职工账号都没有匹配上'";
                    break;
                default:
                    strSql += "where" + ModelWhere();
                    break;
            }
            return strSql;
        }

        private string ModelWhere()
        {
            string strWhere = string.Empty;
            strWhere = " c6='" + strUnit + "'";
            if (!"0".Equals(unitName))
            {
                strWhere += " and A50='" + unitName + "'";
            }
            if (!string.IsNullOrEmpty(strDate))
            {
                strWhere += " and A0='" + strDate + "'";
            }
            return strWhere;
        }
    }
}