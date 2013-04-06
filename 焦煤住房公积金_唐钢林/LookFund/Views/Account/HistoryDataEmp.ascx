<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%@ Import Namespace="LookFund.Dao.FundInfo" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="LookFund.Models.Membership" %>

<div id="inright">
    <div id="biaoge">
        <div class="biaogeks" style="text-align:left">
            公积金数据</div>
        <div id="htmlPrint">
        <table width="762px" cellspacing="1" bgcolor="#88C4FF">
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
        <div style="width:762px;">
            <table cellspacing="1" bgcolor="#88C4FF" style="text-align:center; width:762px;">
                <tr>
                  <td rowspan="3" bgcolor="#CEE7FF">所<br />属<br />单<br />位</td>
                  <td rowspan="3" bgcolor="#CEE7FF">年月</td>
                  <td rowspan="3" bgcolor="#CEE7FF">期<br/>初<br/>缴<br/>存<br/>余<br/>额</td>
                  <td colspan="5" bgcolor="#CEE7FF">当月汇缴</td>
                  <td rowspan="3" bgcolor="#CEE7FF">调<br/>入<br/>金<br/>额</td>
                  <td rowspan="3" bgcolor="#CEE7FF">调<br/>出<br/>金<br/>额</td>
                  <td rowspan="3" bgcolor="#CEE7FF">当<br/>年<br/>结<br/>息</td>          
                  <td colspan="7" bgcolor="#CEE7FF">提取</td>
                  <td rowspan="3" bgcolor="#CEE7FF">期<br/>末<br/>缴<br/>存<br/>余<br/>额</td>
                </tr>
                <tr>
                  <td rowspan="2" bgcolor="#CEE7FF">个人</td>
                  <td rowspan="2" bgcolor="#CEE7FF">企业</td>
                  <td colspan="2" bgcolor="#CEE7FF">补缴</td>
                  <td rowspan="2" bgcolor="#CEE7FF">外<br/>部<br/>转<br/>入</td>
                  <td rowspan="2" bgcolor="#CEE7FF">购<br/>房</td>
                  <td rowspan="2" bgcolor="#CEE7FF">退<br/>休</td>
                  <td rowspan="2" bgcolor="#CEE7FF">调<br/>出<br/>集<br/>团</td>
                  <td rowspan="2" bgcolor="#CEE7FF">解<br/>聘</td>
                  <td rowspan="2" bgcolor="#CEE7FF">死<br/>亡</td>
                  <td rowspan="2" bgcolor="#CEE7FF">其<br/>它</td>
                  <td rowspan="2" bgcolor="#CEE7FF">合<br/>计</td>
                </tr>
                <tr>
                  <td bgcolor="#CEE7FF">个<br/>人</td>
                  <td bgcolor="#CEE7FF">企<br/>业</td>
                </tr>                   
                <% 
                if (ViewData["FundDate"] != null)
                {
                   
                    DataTable dt = ViewData["FundDate"] as DataTable;
                    var strCompany = "111111";
                    var y = 0;
                    for (int rows = 0; rows < dt.Rows.Count; rows++)
                    {
                        var currCompany=dt.Rows[rows]["company"].ToString();
                        %>
                        <tr>
                        <%
                            if (!strCompany.Equals(currCompany))
                            {
                                y++;
                                strCompany = currCompany;
                                int n = dt.Select("company=" + currCompany).Length;
                                %><td bgcolor="#EEF7FE" rowspan="<%=n %>" style="width:20px;"><%:Enum.GetName(typeof(Company), int.Parse(currCompany))%></td><%
                            }
                            if (y == 1)
                            {
                                for (int col = 1; col < dt.Columns.Count; col++)
                                {
                                    %><td bgcolor="#EEF7FE"><%:dt.Rows[rows][col].ToString()%></td><%
                                }
                            }
                            else
                            {
                                for (int col = 1; col < dt.Columns.Count; col++)
                                {
                                    %><td bgcolor="#d2d2d2"><%:dt.Rows[rows][col].ToString()%></td><%
                                }
                            }
                            
                        %>                                 
                        </tr>
                        <%
                    }
                }
                %>
            </table>
            <div style="padding-top:40px;"></div>
         </div>
        </div>
    </div>
</div>
