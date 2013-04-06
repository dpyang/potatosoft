<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LookFund.Models.ViewModel.OperatorEditViewModel>" %>
<%@ Import Namespace="LookFund.Models.Membership" %>

<div id="inright">
    <div id="biaoge">
        <div class="biaogeks">操作员管理</div>
        <div id="opedit">
            <% Html.BeginForm("OperatorAdd", "Account", FormMethod.Post, new { id = "OperatorAddForm" }); %>
            <table width="762" cellspacing="1" bgcolor="#88C4FF">
             <tr>
                <td style="width:150px;" bgcolor="#EEF7FE">登录名/身份证号：</td>
                <td bgcolor="#EEF7FE"><%:Html.TextBoxFor(m => m.LoginName, new { style="width:150px;" })%></td>
                <%:Html.ValidationMessageFor(m=>m.LoginName) %>
             </tr>
             <tr>
                <td bgcolor="#EEF7FE">姓名：</td>
                <td bgcolor="#EEF7FE"><%:Html.TextBoxFor(m => m.ShowName, new { style = "width:150px;" })%></td>
                <%:Html.ValidationMessageFor(m=>m.ShowName) %>
             </tr>
             <tr>
                <td bgcolor="#EEF7FE">密码：</td>
                <td bgcolor="#EEF7FE"><%:Html.PasswordFor(m => m.LoginPwk, new { style = "width:150px;" })%></td>
                <%:Html.ValidationMessageFor(m=>m.LoginPwk) %>
             </tr>
             <tr>
                <td bgcolor="#EEF7FE">确定密码：</td>
                <td bgcolor="#EEF7FE"><%:Html.PasswordFor(m => m.ConfirmPwd, new { style = "width:150px;" })%>
                  <%:Html.ValidationMessageFor(m=>m.ConfirmPwd) %>
                </td>
             </tr>
             <tr>
                <td bgcolor="#EEF7FE">管理员级别：</td>
                <td bgcolor="#EEF7FE">
                    <%: Html.DropDownListFor(m => m.EmpKind, MEmploye.SelectListEnum(typeof(EmpKind)))%>
                </td>
             </tr>
             <tr>
                <td bgcolor="#EEF7FE">联系电话：</td>
                <td bgcolor="#EEF7FE"><%:Html.TextBoxFor(m=>m.EmpPhone) %></td>
             </tr>
             <tr>
                <td bgcolor="#EEF7FE">电子邮箱：</td>
                <td bgcolor="#EEF7FE"><%:Html.TextBoxFor(m=>m.EmpEmail) %></td>
             </tr>
             <tr>
                <td bgcolor="#EEF7FE">&nbsp;</td>
                <td bgcolor="#EEF7FE">
                    <input id="Button1" type="button" value="保存" style="width: 64px" onclick="EmployeSaveAdd()" />&nbsp;&nbsp;&nbsp;
                    <input id="Button2" type="button" value="取消" style="width: 63px" onclick="EmployeReturn()" /></td>
             </tr>
            </table> 
            <% Html.EndForm(); %> 
        </div>
    </div>
</div>