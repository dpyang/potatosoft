<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Data.DataTable>" %>
<%@ Import Namespace="LookFund.Models.Membership" %>

<div id="inright">
    <div id="biaoge">
        <div class="biaogeks">处理问题统计</div> 
        <table width="762" cellspacing="1" bgcolor="#88C4FF">
        <tr>
            <td bgcolor="#CEE7FF">所属单位</td>
            <td bgcolor="#CEE7FF">问题总数</td>
            <td bgcolor="#CEE7FF">已经处理</td>
            <td bgcolor="#CEE7FF">待处理</td>
        </tr>
        <%
            if (Model.Rows.Count > 0)
            {
                for (int i = 0; i < Model.Rows.Count; i++)
                {
                %>
                <tr>
                    <td bgcolor="#EEF7FE"><%: Enum.GetName(typeof(Company), Convert.ToInt32(Model.Rows[i]["empUnit"]))%></td>
                    <td bgcolor="#EEF7FE"><%:Model.Rows[i]["CommentCount"].ToString()%></td>
                    <td bgcolor="#EEF7FE"><%:Model.Rows[i]["ProcessOk"].ToString()%></td>
                    <td bgcolor="#EEF7FE"><%:Model.Rows[i]["ProcessNo"].ToString()%></td>
                </tr>
                <%
            }
            }
            else
            {
                %>
                    <tr><td bgcolor="#EEF7FE" colspan="4" style="text-align:center; color:Red;">没有问题</td></tr>
                <%
            }
        %>
       </table>
    </div>
</div>