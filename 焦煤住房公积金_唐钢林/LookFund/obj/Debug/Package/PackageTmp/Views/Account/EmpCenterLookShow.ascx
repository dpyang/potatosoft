<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Data.DataTable>" %>
<div id="inright">
    <div id="biaoge">
        <div class="biaogeks">查看市中心数据</div>
        <div id="htmlPrint">
        <table width="762" cellspacing="1" bgcolor="#88C4FF">
        <tr>
            <td bgcolor="#CEE7FF">单位账号</td>
            <td bgcolor="#CEE7FF">职工账号</td>
            <td bgcolor="#CEE7FF">职工姓名</td>
            <td bgcolor="#CEE7FF">性别</td>
            <td bgcolor="#CEE7FF">身份证号</td>
            <td bgcolor="#CEE7FF">本金余额</td>
            <td bgcolor="#CEE7FF">本年利息</td>
            <td bgcolor="#CEE7FF">实存总额</td>
        </tr>
        <% 
            if (Model.Rows.Count > 0)
            {
                for (int i = 0; i < Model.Rows.Count; i++)
                { 
                %>
                    <tr>
                        <td bgcolor="#EEF7FE"><%:Model.Rows[i][0].ToString()%></td>
                        <td bgcolor="#EEF7FE"><%:Model.Rows[i][1].ToString()%></td>
                        <td bgcolor="#EEF7FE"><%:Model.Rows[i][2].ToString()%></td>
                        <td bgcolor="#EEF7FE"><%:Model.Rows[i][3].ToString()%></td>
                        <td bgcolor="#EEF7FE"><%:Model.Rows[i][4].ToString()%></td>
                        <td bgcolor="#EEF7FE"><%:Model.Rows[i][5].ToString()%></td>
                        <td bgcolor="#EEF7FE"><%:Model.Rows[i][6].ToString()%></td>
                        <td bgcolor="#EEF7FE"><%:Model.Rows[i][7].ToString()%></td>
                    </tr>
                <%
                }
            }
            else
            {
                %>
                <tr><td bgcolor="#EEF7FE" colspan="8" style="color:Red; text-align:center;">没有查到数据</td></tr>
                <%
            }            
            %>
    </table>
        </div>
    </div>
</div>