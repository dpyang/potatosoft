using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using TglFirst.Core.Repository;
using LookFund.Models.Membership;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace LookFund.Dao.Membership
{
    public interface IFunctionMenu : IRepository<FunctionMenu>
    {
        /// <summary>
        /// 管理员默认权限
        /// </summary>
        /// <returns></returns>
        List<FunctionMenu> AdminMenuList();

        /// <summary>
        /// 通过层次编码数组返回菜单
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        List<FunctionMenu> GetMenuList(string[] arr);

        /// <summary>
        /// 返回菜单树结构字符串
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string TableTree(MEmploye model);

    }

    public class DFunctionMenu : RepositoryBase<FunctionMenu>, IFunctionMenu
    {
        public List<FunctionMenu> AdminMenuList()
        {
            var list = new List<FunctionMenu>();
            list.Add(Query("LevelCode", "1101").First());
            list.Add(Query("LevelCode", "110101").First());
            list.Add(Query("LevelCode", "110103").First());            
            list.Add(Query("LevelCode", "1103").First());
            list.Add(Query("LevelCode", "110302").First());
            list.Add(Query("LevelCode", "110303").First());
            list.Add(Query("LevelCode", "1105").First());
            list.Add(Query("LevelCode", "110501").First());
            list.Add(Query("LevelCode", "1106").First());
            list.Add(Query("LevelCode", "110602").First());
            return list;
        }

        public List<FunctionMenu> GetMenuList(string[] arr)
        {
            var list = new List<FunctionMenu>();
            for (int i = 0; i < arr.Length; i++)
            {
                list.Add(Query("LevelCode", arr[i]).First());
            }
            return list;
        }

        private IList<FunctionMenu> GetChildrenNode(string NodeCode,string len)
        {
            string sql = Session.GetNamedQuery("GetChildrenNode").QueryString
                .Replace("#code", NodeCode)
                .Replace("#lencode", len);

            return Session.CreateSQLQuery(sql)
                .SetResultTransformer(Transformers.AliasToBean(typeof(FunctionMenu)))
                .List<FunctionMenu>();
        }

        private void StrNode(StringBuilder sb, string root, string empfun)
        {
            var list = GetChildrenNode(root, (root.Length + 2).ToString());
            foreach (var item in list)
            {
                sb.Append("<ul style=\"list-style:none; margin-left:20px;\">");
                if (empfun.Contains(item.Uid.ToString()))
                {
                    sb.Append("<li><input type=\"checkbox\" checked=\"checked\" value='" + item.LevelCode + "' onclick='SelectTreeNode(this)' />" + item.FunctionName);
                }
                else
                {
                    sb.Append("<li><input type=\"checkbox\" value='" + item.LevelCode + "' onclick='SelectTreeNode(this)'/>" + item.FunctionName);
                }
                StrNode(sb, item.LevelCode, empfun);
                sb.Append("</li>");
                sb.Append("</ul>");
            }
        }

        public string TableTree(MEmploye model)
        {
            var listEmp = model.FunctionList;
            string empfun = string.Empty;
            foreach (var item in listEmp)
            {
                empfun += item.Uid + ",";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul style=\"list-style:none\"><li id='rootNode'><input type=\"checkbox\" checked=\"true\" value='" + model.Uid + "' onclick='SelectTreeNode(this)'/>住房公积金查询系统");
            StrNode(sb, "11", empfun);
            sb.Append("</li></ul>");
            return sb.ToString();
        }

    }
}