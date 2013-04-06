<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Data.DataTable>" %>
<%@ Import Namespace="LookFund.Models.Membership" %>

<table width="762" cellspacing="1" bgcolor="#88C4FF">
<tr>
    <td bgcolor="#CEE7FF">年月</td>
    <td bgcolor="#CEE7FF">所属单位</td>
    <td bgcolor="#CEE7FF">单位名称</td>
    <td bgcolor="#CEE7FF">人数</td>
</tr>
<%
    if (Model.Rows.Count > 0)
    {  
        var strDate=string.Empty;
        for (int i = 0; i < Model.Rows.Count;)
        {
            var currDate = Model.Rows[i][0].ToString();
            Model.DefaultView.RowFilter = "c7=" + currDate;
            var dt = Model.DefaultView.ToTable();
            var yearDate = currDate.Insert(4, "年").Insert(7, "月");
            for (int n = 0; n < dt.Rows.Count;)
            {                       
                var currunit = dt.Rows[n]["c6"].ToString();
                dt.DefaultView.RowFilter = "c6=" + currunit;
                var dtUnit = dt.DefaultView.ToTable();
                for (int y = 0; y < dtUnit.Rows.Count; y++)
                { 
                    %>
                    <tr>
                    <%
                        if (n == 0 && y==0)
                        {
                            %>                             
                            <td bgcolor="#EEF7FE" rowspan="<%= dt.Rows.Count%>"><%: yearDate%></td>  
                            <%
                        }
                        if (y == 0)
                        {
                            %>
                            <td bgcolor="#EEF7FE" rowspan="<%= dtUnit.Rows.Count %>"><%: Enum.GetName(typeof(Company),Int32.Parse(currunit))%></td>
                            <%
                        }
                        %>                            
                        <td bgcolor="#EEF7FE"><%: dtUnit.Rows[y][2].ToString()%></td>
                        <td bgcolor="#EEF7FE"><%: dtUnit.Rows[y][3].ToString()%></td>
                    </tr>
                    <%
                }
                n = n + dtUnit.Rows.Count;
            }
            i = i + dt.Rows.Count;
        }   
    }
    else
    {
        %><tr><td bgcolor="#EEF7FE" colspan="4" style="color:Red">没有此时间段的数据</td></tr><%
    }
           
%>           
</table>
<div style="text-align:center;"><%=ViewData["htmlPage"].ToString()%></div>