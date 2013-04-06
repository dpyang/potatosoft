<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LookFund.Models.ViewModel.ErrorViewModel>" %>
<%@ Import Namespace="LookFund.Models.Membership" %>
<script type="text/javascript">
    Ext.onReady(function () {
        new Ext.form.DateField({
            applyTo: "strDate",
            format: "Y-m",
            maxValue:new Date(),
            value: new Date()
        });
    });
    function DeleteError() {
        if (confirm("确定要删除满足条件的错误信息")) {
            $.ajax({
                type: "GET",
                async: false,
                url: "/Account.html/DeleteError",
                data: { teamname: $("#strUnit").val(), errorteam: $("#unitName").val(), errortime: $("#strDate").val(), erroritem: $("#errorType").val() },
                success: function (msg) {
                    if (msg == "OK") {
                        alert("删除成功！");
                    }
                },
                error: function (e) {
                    alert("发生错误请重试！");
                }
            });
        }
    }
</script>
<div id="inright">
    <div id="biaoge">
        <div class="biaogeks">错误数据导出</div>
        <div id="LookFild">
            <% Html.BeginForm("ExportErrorDate", "Account", FormMethod.Post, new { id = "formExportError" }); %>
             <table width="762" cellspacing="1" bgcolor="#88C4FF">
                <tr>
                    <td bgcolor="#CEE7FF" style="text-align:right">所属单位：</td>
                    <td bgcolor="#CEE7FF"><%:Html.DropDownListFor(m=>m.strUnit, MEmploye.SelectListEnum(typeof(Company)), new { onchange = "getUnitList()" })%></td>
                    <td bgcolor="#CEE7FF" style="text-align:right">有错误数据单位名称：</td>
                    <td bgcolor="#CEE7FF"> <% Html.RenderAction("SelectEorrListUnit", "Account"); %></td>
                </tr>
                <tr>
                    <td bgcolor="#CEE7FF" style="text-align:right">月份：</td>
                    <td bgcolor="#CEE7FF"><%:Html.TextBoxFor(m => m.strDate, new { style = "width:80px;" })%></td>
                    <td bgcolor="#CEE7FF" style="text-align:right">错误分类：</td>
                    <td bgcolor="#CEE7FF">
                        <select id="errorType" name="errorType">
                            <option value="0">＝请选择＝</option>
                            <option value="1">职工账号或身份证号为空</option>
                            <option value="2">身份证号错误</option>
                            <option value="3">职工账号重复</option>
                            <option value="4">一个人多个账号</option>
                            <option value="5">一个月里一个人多次出现</option>
                            <option value="6">身份证号和职工账号都没有匹配上的数据</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#CEE7FF" colspan="4">
                        <input id="Submit1" type="submit" value="导出" style="width:80px;" />
                        <input id="DeleteErrorBTN" type="button" onclick="javascript:DeleteError();" value="清除错误" style=" margin-left:10px; width:80px;" />
                        </td>
                </tr>
            </table>
          <% Html.EndForm(); %>
        </div>
    </div>
</div>