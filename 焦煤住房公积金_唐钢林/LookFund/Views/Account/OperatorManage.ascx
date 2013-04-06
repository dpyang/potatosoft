<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<LookFund.Models.Membership.MEmploye>>" %>
<div id="inright">
    <div id="biaoge">
        <div class="biaogeks">操作员管理</div>
        <div id="empList">
            <table width="762" cellspacing="1" bgcolor="#88C4FF">
                <tr>
                    <td bgcolor="#CEE7FF">登录名</td>
                    <td bgcolor="#CEE7FF">姓名</td>
                    <td bgcolor="#CEE7F">联系电话</td>
                    <td bgcolor="#CEE7F">电子信箱</td>
                    <td bgcolor="#CEE7FF">操作员级管</td>
                    <td bgcolor="#CEE7FF">修改</td>
                    <td bgcolor="#CEE7FF">删除</td>
                    <td bgcolor="#CEE7FF">权限</td>
                </tr>
                <%
                    foreach (var item in Model)
                    {
                       %>
                       <tr>
                        <td bgcolor="#EEF7FE"><%:item.LoginName%></td>
                        <td bgcolor="#EEF7FE"><%:item.ShowName%></td>
                        <td bgcolor="#EEF7FE"><%:item.EmpPhone%></td>
                        <td bgcolor="#EEF7FE"><%:item.EmpEmail %></td>
                        <td bgcolor="#EEF7FE"><%: Enum.GetName(typeof(LookFund.Models.Membership.EmpKind),int.Parse(item.EmpKind))%></td>
                        <td bgcolor="#EEF7FE"><a href="javascript:EmployeUpdate('<%:item.Uid %>')" style="color:Blue">修改</a></td>
                        <% 
                            if ("Administrator".Equals(item.LoginName))
                            { 
                               %>
                               <td bgcolor="#EEF7FE" style="color:Gray">删除</td>
                               
                               <%                          
                            }
                            else
                            {
                                %>
                                <td bgcolor="#EEF7FE"><a href="javascript:EmployeDel('<%:item.Uid %>')" style="color:Blue">删除</a></td>
                                
                                <%
                            }  
                            var username= ViewData["currentUser"].ToString();
                            if ("Administrator".Equals(username))
                            {
                                %>
                                <td bgcolor="#EEF7FE"><a href="javascript:EmployeFunction('<%:item.Uid %>')" style="color:Blue">权限</a></td>
                                <%
                            }
                            else
                            {
                                %>
                                <td bgcolor="#EEF7FE" style="color:Gray">权限</td>
                                <%
                            }
                        %>                       
                       </tr>
                       <%                            
                    }
                %>
            </table>
        </div>
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="762">
                <tr>
                    <td><input id="Button1" type="button" value="新增" onclick="EmployeAdd()" 
                            style="width: 70px" /></td>
                    <td style="text-align:right"><%=ViewData["pageHtml"]%></td>
                </tr>
            </table>
        </div>
     </div>   
 </div> 