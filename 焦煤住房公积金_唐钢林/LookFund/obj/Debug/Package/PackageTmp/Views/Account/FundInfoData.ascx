<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Data.DataTable>" %>
<%@ Import Namespace="LookFund.Dao.FundInfo" %>
<%@ Import Namespace="System.Data" %>
<div id="inright">
    <div id="biaoge">
        <div class="biaogeks" style="text-align:left">
            <% if (ViewData["FundDate"] != null)
               {
                   DataTable dt = ViewData["FundDate"] as DataTable;
                   if (dt.Rows.Count > 0)
                   {
                       var yearm = dt.Rows[0]["C7"].ToString();
                       if (yearm.Length >= 6)
                       {
                            %><%: yearm.Substring(0,4)%>年<%: yearm.Substring(4,2) %>月公积金缴存信息<%
                       }
                       else
                       {
                           %>当月公积金缴存信息<%
                       }
                   }
                   else
                   {
                       %>当月公积金缴存信息<%
                   }
               }
               else
               {
                   %>当月公积金缴存信息<%
               }
            %>            
         </div>
        <div id="htmlPrint">
        <table width="762px" cellspacing="1px">
            <%
                if (Model.Rows.Count > 0)
                {
                    var countcol = Model.Columns.Count - 1;
                    var rows = countcol % 3 == 0 ? countcol / 3 : countcol / 3 + 1;
                    for (int i = 0; i < rows; i++)
                    {
                        int cols = countcol - i * 3 < 3 ? countcol - i * 3 : 3;
                        %>
                        <tr>
                            <% 
                                for (int n = 0; n < cols; n++)
                                {
                                    int colIndex = i * 3 + n;
                                    if (cols < 3 && n+1==cols)
                                    {
                                        int rowspan = 7 - 2 * cols;
                                        %>
                                        <td bgcolor="#EEF7FE" style="text-align:right">
                                            <%:Model.Columns[colIndex].ColumnName%>：
                                        </td>
                                        <td bgcolor="#EEF7FE" style="text-align:left" colspan="<%:rowspan %>">
                                            <%:Model.Rows[0][colIndex].ToString()%>
                                        </td> 
                                        <%
                                    }
                                    else
                                    {
                                        %>
                                        <td bgcolor="#EEF7FE" style="text-align:right">
                                            <%:Model.Columns[colIndex].ColumnName%>：
                                        </td>
                                        <td bgcolor="#EEF7FE" style="text-align:left">
                                            <%:Model.Rows[0][colIndex].ToString()%>
                                        </td> 
                                        <%
                                    }
                                } %>                      
                        </tr>    
                        <%
                    }
                }  
            %>
        </table>
        <div style="width:762px;overflow:auto;">
            <table cellspacing="1" bgcolor="#88C4FF" style="text-align:center; width:762px;">
                <% ReadXML xml=ReadXML.getInstance(); %>
                <%=xml.FundLookHead %>                        
                <% 
                if (ViewData["FundDate"] != null)
                {
                    DataTable dt = ViewData["FundDate"] as DataTable;
                    for (int rows = 0; rows < dt.Rows.Count; rows++)
                    {
                    %>
                    <tr>
                        <% 
                        for (int col = 0; col < dt.Columns.Count; col++)
                        {
                            %>
                            <td  bgcolor="#EEF7FE"><%:dt.Rows[rows][col].ToString()%></td>
                            <%
                        }
                    %>                                 
                    </tr>
                    <%
                    }
                }
                %>
            </table>
            <div style="padding-top:30px;"></div>
         </div>
        </div>
    </div>
</div>
