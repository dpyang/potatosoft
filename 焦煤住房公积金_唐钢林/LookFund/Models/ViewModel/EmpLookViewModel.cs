using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LookFund.Models.ViewModel
{
    public class EmpLookViewModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string EmpName
        {
            get;
            set;
        }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard
        {
            get;
            set;
        }

        /// <summary>
        /// 所属矿物局
        /// </summary>
        public string strCompany
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

        /// <summary>
        /// 月份
        /// </summary>
        public string MothDate
        {
            get;
            set;
        }
    }

    public static class LookTableName
    {
        public static string GetTableName(string strcode)
        {
            switch (strcode)
            {
                case "110101":
                    return "R_2001_10_2001JM";
                case "110201":
                    return "R_2001_10_2001XS";
                case "110301":
                    return "R_2001_10_2001FX";
                case "110401":
                    return "R_2001_10_2001HZ";
                default:
                    return string.Empty;
                  
            }
        }
        
    }
}