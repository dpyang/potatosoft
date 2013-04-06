using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace LookFund.Dao.FundInfo
{
    public class ReadXML
    {
        
        private static ReadXML instance = null;
        public static ReadXML getInstance()
        {
            if (instance == null)
            {                           //line A   
                instance = new ReadXML();          //line B   
            }
            return instance;
        }

        
        private ReadXML()
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Config\\Import.xml");
            XmlNode xn = xmlDoc.SelectSingleNode("config");
            XmlNode BaseNode = xn.ChildNodes[0];
            UserIdCard = ((XmlElement)BaseNode).GetAttribute("UserIdCard");
            UserName = ((XmlElement)BaseNode).GetAttribute("UserName");
            XmlNodeList xnl = BaseNode.ChildNodes;
            foreach (XmlNode xnf in xnl)
            {
                XmlElement xe = (XmlElement)xnf;
                if (string.IsNullOrEmpty(BaseColumnName))
                    BaseColumnName = xe.GetAttribute("column");
                else
                    BaseColumnName += "," + xe.GetAttribute("column");

                if (string.IsNullOrEmpty(BaseColumnShow))
                    BaseColumnShow = xe.GetAttribute("column") + " as " + xe.GetAttribute("showName");
                else
                    BaseColumnShow += "," + xe.GetAttribute("column") + " as " + xe.GetAttribute("showName");

                if (string.IsNullOrEmpty(BaseImportName))
                    BaseImportName = xe.GetAttribute("ImportName");
                else
                    BaseImportName += "," + xe.GetAttribute("ImportName");

                if (xe.GetAttribute("VerifyShow").Equals("Yes"))
                {
                    if (string.IsNullOrEmpty(VerifyShow))
                    {
                        VerifyColumn = "a." + xe.GetAttribute("column");
                        VerifyShow = "a." + xe.GetAttribute("column") + " as " + xe.GetAttribute("showName");
                    }
                    else
                    {
                        VerifyColumn += ",a." + xe.GetAttribute("column");
                        VerifyShow += ",a." + xe.GetAttribute("column") + " as " + xe.GetAttribute("showName");
                    }
                }
            }

            XmlNode BaseNode1 = xn.ChildNodes[1];
            FundTimeCol = ((XmlElement)BaseNode1).GetAttribute("FundTimeCol");
            XmlNodeList xnl1 = BaseNode1.ChildNodes;
            foreach (XmlNode xnf in xnl1)
            {
                XmlElement xe = (XmlElement)xnf;
                if (string.IsNullOrEmpty(FundColumnName))
                    FundColumnName = xe.GetAttribute("column");
                else
                    FundColumnName += "," + xe.GetAttribute("column");

                if (string.IsNullOrEmpty(FundImportName))
                    FundImportName = xe.GetAttribute("ImportName");
                else
                    FundImportName += "," + xe.GetAttribute("ImportName");

                if (string.IsNullOrEmpty(FundImportNameb))
                    FundImportNameb = "b." + xe.GetAttribute("ImportName");
                else
                    FundImportNameb += ",b." + xe.GetAttribute("ImportName");

                if (xe.GetAttribute("VerifyShow").Equals("Yes"))
                {
                    if (string.IsNullOrEmpty(VerifyShow))
                    {
                        VerifyColumn = "b." + xe.GetAttribute("column");
                        VerifyShow = "b." + xe.GetAttribute("column") + " as " + xe.GetAttribute("showName");
                    }
                    else
                    {
                        VerifyColumn += ",b." + xe.GetAttribute("column");
                        VerifyShow += ",b." + xe.GetAttribute("column") + " as " + xe.GetAttribute("showName");
                    }
                }

                if (xe.GetAttribute("type").Equals("take"))
                {
                    if (string.IsNullOrEmpty(TakeColumnShow))
                        TakeColumnShow = xe.GetAttribute("column") + " as " + xe.GetAttribute("showName");
                    else
                        TakeColumnShow += "," + xe.GetAttribute("column") + " as " + xe.GetAttribute("showName");
                }
            }

            VerifyShowHead = xn.ChildNodes[2].ChildNodes[0].FirstChild.Value;
            VerifyStat = xn.ChildNodes[2].ChildNodes[1].FirstChild.Value;
            IncomeColumnShow = xn.ChildNodes[3].ChildNodes[0].FirstChild.Value;
            FundLookHead = xn.ChildNodes[3].ChildNodes[1].FirstChild.Value;
            TakeShowHead = xn.ChildNodes[4].FirstChild.Value;
            EmpShowColumn = xn.ChildNodes[5].ChildNodes[0].FirstChild.Value;
            EmpLookColumn = xn.ChildNodes[5].ChildNodes[1].FirstChild.Value;
        }
        /// <summary>
        /// 基本信息列表
        /// </summary>
        public string BaseColumnName
        {
            get;
            set;
        }
        /// <summary>
        /// 基本信息显示名称
        /// </summary>
        public string BaseColumnShow
        {
            get;
            set;
        }

        /// <summary>
        /// 基本信息导入的excel列名
        /// </summary>
        public string BaseImportName
        {
            get;
            set;
        }

        /// <summary>
        /// 公积金信息表列名
        /// </summary>
        public string FundColumnName
        {
            get;
            set;
        }

        /// <summary>
        /// 导入excel文件列名
        /// </summary>
        public string FundImportName
        {
            get;
            set;
        }
        /// <summary>
        ///  导入excel文件列名带b.
        /// </summary>
        public string FundImportNameb
        {
            get;
            set;
        }
        /// <summary>
        /// 时间列
        /// </summary>
        public string FundTimeCol
        {
            get;
            set;
        }
       
        /// <summary>
        /// 支取显示列
        /// </summary>
        public string TakeColumnShow
        {
            get;
            set;
        }
         
        /// <summary>
        /// 收入显示列
        /// </summary>
        public string IncomeColumnShow
        {
            get;
            set;
        }
        
        public string UserIdCard
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// 人员查看公积金表头
        /// </summary>
        public string FundLookHead
        {
            get;
            set;
        }

        /// <summary>
        /// 验证公积金表头
        /// </summary>
        public string VerifyShowHead
        {
            get;
            set;
        }
        /// <summary>
        /// 验证统计
        /// </summary>
        public string VerifyStat
        {
            get;
            set;
        }
        public string VerifyColumn
        {
            get;
            set;
        }
        /// <summary>
        /// 验证显示列
        /// </summary>
        public string VerifyShow
        {
            get;
            set;
        }
        /// <summary>
        /// 人员查看公积金支取表头
        /// </summary>
        public string TakeShowHead
        {
            get;
            set;
        }


        #region 查看员工公积金
        /// <summary>
        /// 显示字段。
        /// </summary>
        public string EmpShowColumn
        {
            get;
            set;
        }

        /// <summary>
        /// 查询字段
        /// </summary>
        public string EmpLookColumn
        {
            get;
            set;
        }

        #endregion
    }
}