<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Data.DataTable>" %>
<%@ Import Namespace="LookFund.Models.Membership" %>

<div id="htmlPrint">
<table width="762" cellspacing="1" bgcolor="#88C4FF">
    <tr>
    <% 
        for (int col = 0; col < Model.Columns.Count-2; col++)
        {
            %>
               <td bgcolor="#CEE7FF"><%:Model.Columns[col].ColumnName %></td>
            <%
        }    
    %>
    <td bgcolor="#CEE7FF">详细</td>
    </tr>
    <%
        for (int row = 0; row < Model.Rows.Count; row++)
        {
            %>
            <tr>
               <% 
                for (int col = 0; col < Model.Columns.Count-2; col++)
                {
                    if (col == Model.Columns.Count - 3)
                    {
                        %>
                        <td bgcolor="#EEF7FE"><%: Enum.GetName(typeof(Company), Convert.ToInt32(Model.Rows[row][col])) %></td>
                        <%
                    }
                    else
                    {
                        %>
                        <td bgcolor="#EEF7FE"><%:Model.Rows[row][col].ToString() %></td>
                        <%
                    }                    
                }    
                %>
                <td bgcolor="#EEF7FE"><a style="color: blue;" href="javascript:EmpFundLookInfo('<%:Model.Rows[row][Model.Columns.Count - 2].ToString() %>')">详细</a></td>
            </tr>
            <%
        }
    %>
</table>
<div>
<%= ViewData["htmlPage"].ToString() %>
</div>
</div>

