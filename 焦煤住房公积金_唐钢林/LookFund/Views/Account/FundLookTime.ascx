<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LookFund.Models.ViewModel.FundLookTimeViewModel>" %>
<script type="text/javascript">
    Ext.onReady(function () {
        new Ext.form.DateField({
            applyTo: "BegionTime",
            format: "Ym"
        });
        new Ext.form.DateField({
            applyTo: "EndTime",
            format: "Ym"
        });
    });
</script>
<div id="inright">
    <div id="biaoge">
        <div class="biaogeks">历史数据查询</div>
        <div id="LookFild">
            <% Html.BeginForm("HistoryFundLook", "Account", FormMethod.Post, new { id = "FormHistFundLook" }); %>
             <table width="762" cellspacing="1" bgcolor="#88C4FF">
                <tr>
                    <td bgcolor="#CEE7FF" style="text-align:right">查询开始日期：</td>
                    <td bgcolor="#CEE7FF"><%:Html.TextBoxFor(m=>m.BegionTime)%>输入格式如：200901</td>
                    <td bgcolor="#CEE7FF" style="text-align:right">查询结束日期：</td>
                    <td bgcolor="#CEE7FF"><%:Html.TextBoxFor(m=>m.EndTime) %>输入格式如：201001</td>
                    <td bgcolor="#CEE7Ff"><input id="Button1" type="button" value="查询" style="width: 59px" onclick="EmpHistoryFundLook()"/></td>
                </tr>
            </table>
            <% Html.EndForm(); %>
        </div>        
        <div id="LookResult" style="text-align:center">
        
        </div>
    </div>
</div>