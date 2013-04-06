<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LookFund.Models.ViewModel.EmpLookViewModel>" %>
<%@ Import Namespace="LookFund.Models.Membership" %>

<script type="text/javascript">
    Ext.onReady(function () {
        new Ext.form.DateField({
            applyTo: "MothDate",
            format: "Y-m"
        });
    });
</script>

<div id="inright">
    <div id="biaoge">
        <div class="biaogeks">查询职工公积金信息</div>
        <div id="LookFild">
            <% Html.BeginForm("EmpFundLook", "Account", FormMethod.Post, new { id = "FormFundLook" }); %>
             <table width="762" cellspacing="1" bgcolor="#88C4FF">
                <tr>
                    <td bgcolor="#CEE7FF" style="text-align:right">职工姓名：</td>
                    <td bgcolor="#CEE7FF"><%:Html.TextBoxFor(m => m.EmpName, new { style="width:120px;"})%></td>
                    <td bgcolor="#CEE7FF" style="text-align:right">身份证号：</td>
                    <td bgcolor="#CEE7FF"><%:Html.TextBoxFor(m=>m.IdCard) %></td>
                    <td bgcolor="#CEE7FF">月份：</td>
                    <td bgcolor="#CEE7FF"><%:Html.TextBoxFor(m => m.MothDate, new { style = "width:80px;" })%></td>
                </tr>
                <tr>
                    <td bgcolor="#CEE7FF" style="text-align:right">所属单位：</td>
                    <td bgcolor="#CEE7FF"><%:Html.DropDownListFor(m => m.strCompany, MEmploye.SelectListEnum(typeof(Company)), new { onchange = "getUnitList()" })%></td>
                    <td bgcolor="#CEE7FF" style="text-align:right">单位名称：</td>                    
                    <td bgcolor="#CEE7FF" colspan="3">
                        <span id="changeUnit">
                        <% Html.RenderAction("FirstSelectListUnit", "Account"); %>
                        </span>
                    <input id="Button1" type="button" value="查询" style="width: 59px" onclick="SumbitFundLook()"/></td>
                </tr>
            </table>
            <% Html.EndForm(); %>
        </div>
        <br />
        <div id="LookResult" style="text-align:center">
        
        </div>
    </div>
</div>