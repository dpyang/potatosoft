<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="inright">
    <div id="biaoge">
        <div class="biaogeks">
            初始化密码</div>
        <table width="762px" cellspacing="1" bgcolor="#88C4FF">
            <tr>
                <td bgcolor="#CEE7FF" style="width:150px; text-align:right;">身份证号/登录名：</td>
                <td bgcolor="#CEE7FF" style="width:200px;"><%:Html.TextBox("txtIdCard")%></td>
                <td bgcolor="#CEE7FF">
                    <input id="Button1" type="button" value="初始化" onclick="UserPwdInit()"/></td>
            </tr>
        </table>
    </div>
</div>