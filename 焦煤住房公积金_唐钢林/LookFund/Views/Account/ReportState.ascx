<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LookFund.Models.ViewModel.ReportStatViewModel>" %>

<script type="text/javascript">
    Ext.onReady(function () {
        new Ext.form.DateField({
            applyTo: "StatDate",
            format: "Y-m",
            maxValue: new Date()
        });
        new Ext.form.DateField({
            applyTo: "EndDate",
            format: "Y-m",
            maxValue:new Date(),
            value: new Date()
        });
    });
</script>
<div id="inright">
    <div id="biaoge">
        <div class="biaogeks">上报情况</div>
         <div id="LookFild">
            <% Html.BeginForm("ReportStatLook", "Account", FormMethod.Post, new { id = "FormReportStatLook" }); %>
             <table width="762" cellspacing="1" bgcolor="#88C4FF">
                <tr>
                    <td bgcolor="#CEE7FF">
                        <table>
                            <tr>
                                <td>选择统计时间段：</td>
                                <td><%:Html.TextBoxFor(m => m.StatDate, new { style="width:80px;"})%></td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp; 到 &nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td><%:Html.TextBoxFor(m => m.EndDate, new { style = "width:80px;" })%></td>
                                <td><input id="Button1" type="button" value="查询" style="width: 59px" onclick="ReportStatLook()"/></td>
                            </tr>
                         </table>
                    </td>
                </tr>
            </table>
            <% Html.EndForm(); %>
        </div>
        <br />
        <div id="LookResult" style="text-align:center">
            <% Html.RenderAction("ReportStateResult", "Account"); %>
        </div>
    </div>
</div>