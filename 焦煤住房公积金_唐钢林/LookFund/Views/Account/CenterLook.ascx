<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LookFund.Models.ViewModel.EmpLookViewModel>" %>
<%@ Import Namespace="LookFund.Models.Membership" %>

<div id="inright">
    <div id="biaoge">
        <div class="biaogeks">查询太原市中心数据</div>
        <div id="LookFild">
            <% Html.BeginForm("EmpCenterLook", "Account", FormMethod.Post, new { id = "FormCenterLook" }); %>
             <table width="762" cellspacing="1" bgcolor="#88C4FF">
                <tr>
                    <td bgcolor="#CEE7FF" style="text-align:right">姓名：</td>
                    <td bgcolor="#CEE7FF"><%:Html.TextBoxFor(m=>m.EmpName)%></td>
                    <td bgcolor="#CEE7FF" style="text-align:right">身份证号：</td>
                    <td bgcolor="#CEE7FF"><%:Html.TextBoxFor(m=>m.IdCard) %></td>
                </tr>
                <tr>
                    <td bgcolor="#CEE7FF" style="text-align:right">所属单位：</td>
                    <td bgcolor="#CEE7FF"><%:Html.DropDownListFor(m => m.strCompany, MEmploye.SelectListEnum(typeof(Company),true))%></td>
                    <td bgcolor="#CEE7FF"><input id="Button1" type="button" value="查询" style="width: 59px" onclick="SumbitCenterLook()"/></td>                    
                    <td bgcolor="#CEE7FF">&nbsp;</td>
                </tr>
            </table>
            <% Html.EndForm(); %>
        </div>
        <br />
        <div id="LookResult" style="text-align:center">
        
        </div>
    </div>
</div>