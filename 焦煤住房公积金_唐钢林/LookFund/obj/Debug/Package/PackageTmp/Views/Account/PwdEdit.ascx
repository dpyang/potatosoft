<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LookFund.Models.ViewModel.PwdEditViewModel>" %>


<div id="inright">
    <div id="biaoge">
        <div class="biaogeks">
            修改密码</div>
        <% Html.BeginForm("PwdEdit", "Account", FormMethod.Post, new { id = "formEditRwd" }); %>
        <table width="762px" cellspacing="1" bgcolor="#88C4FF">
            <tr>
                <td bgcolor="#CEE7FF" style=" text-align:right">
                    原密码：
                </td>
                <td bgcolor="#CEE7FF">
                  <%: Html.PasswordFor(m=>m.OldPwd) %>
                  <%: Html.ValidationMessageFor(m => m.OldPwd) %>
                </td>
            </tr>
            <tr>
                <td bgcolor="#EEF7FE"  style=" text-align:right">
                    新密码：
                </td>
                <td bgcolor="#EEF7FE">
                    <%: Html.PasswordFor(m=>m.NewPwd) %>
                    <%: Html.ValidationMessageFor(m => m.NewPwd)%>
                </td>                
            </tr>
            <tr>
                <td bgcolor="#CEE7FF" style=" text-align:right">
                    确定密码：
                </td>
                <td bgcolor="#CEE7FF">
                    <%: Html.PasswordFor(m => m.ConfirmPwd)%>
                    <%: Html.ValidationMessageFor(m => m.ConfirmPwd)%>
                </td>                
            </tr>
            <tr>
                <td  bgcolor="#EEF7FE">&nbsp;</td>
                <td  bgcolor="#EEF7FE">
                    <input id="Button1" type="button" value="确定" onclick="UpeatePwd()" /></td>
            </tr>
        </table>
       <% Html.EndForm(); %>      
    </div>
</div>
