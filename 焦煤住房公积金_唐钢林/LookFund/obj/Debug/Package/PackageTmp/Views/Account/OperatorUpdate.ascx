<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LookFund.Models.ViewModel.OperatorEditViewModel>" %>
<%@ Import Namespace="LookFund.Models.Membership" %>
<div id="inright">
    <div id="biaoge">
        <div class="biaogeks">
            操作员管理</div>
        <div id="opedit">
            <% Html.BeginForm("OperatorUpdate", "Account", FormMethod.Post, new { id = "OperatorUpdateForm" }); %>
            <table width="762" cellspacing="1" bgcolor="#88C4FF">
                <tr>
                    <td style="width: 150px;" bgcolor="#EEF7FE">
                        登录名/身份证号：
                    </td>
                    <% 
                        if ("Administrator".Equals(Model.LoginName))
                        {
                            %>
                            <td bgcolor="#EEF7FE">
                                <%:Html.TextBoxFor(m => m.LoginName, new { disabled=false })%>
                                
                            </td>
                            <%                                              
                        }
                        else
                        {
                            %>
                            <td bgcolor="#EEF7FE">
                                <%:Html.TextBoxFor(m => m.LoginName) %>
                                <%:Html.ValidationMessageFor(m=>m.LoginName) %>
                            </td>
                            <%
                         }                
                        %>
                </tr>
                <tr>
                    <td bgcolor="#EEF7FE">
                        姓名：
                    </td>
                    <td bgcolor="#EEF7FE">
                        <%:Html.TextBoxFor(m=>m.ShowName) %>
                        <%:Html.ValidationMessageFor(m=>m.ShowName) %>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#EEF7FE">
                        操作员级别：
                    </td>
                    <% 
                        if ("Administrator".Equals(Model.LoginName))
                        {
                    %>
                    <td bgcolor="#EEF7FE">
                        <%:Html.DropDownListFor(m => m.EmpKind, MEmploye.SelectListEnum(typeof(EmpKind)), new { disabled = false })%>
                    </td>
                    <%                        
                        }
                    else
                    {
                    %>
                    <td bgcolor="#EEF7FE">
                        <%:Html.DropDownListFor(m => m.EmpKind, MEmploye.SelectListEnum(typeof(EmpKind)))%>
                    </td>
                    <%
                        }                
                    %>
                </tr>
                <tr>
                    <td bgcolor="#EEF7FE">
                        联系电话：
                    </td>
                    <td bgcolor="#EEF7FE">
                        <%:Html.TextBoxFor(m=>m.EmpPhone) %>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#EEF7FE">
                        电子信箱：
                    </td>
                    <td bgcolor="#EEF7FE">
                        <%:Html.TextBoxFor(m=>m.EmpEmail) %>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#EEF7FE">
                        &nbsp;<%: Html.HiddenFor(m=>m.Uid) %>
                    </td>
                    <td bgcolor="#EEF7FE">
                        <input id="Button1" type="button" value="保存" style="width: 64px" onclick="EmployeUpdateSave()" />&nbsp;&nbsp;&nbsp;
                        <input id="Button2" type="button" value="返回" style="width: 63px" onclick="EmployeReturn()" />
                    </td>
                </tr>
            </table>
            <% Html.EndForm(); %>
        </div>
    </div>
</div>
