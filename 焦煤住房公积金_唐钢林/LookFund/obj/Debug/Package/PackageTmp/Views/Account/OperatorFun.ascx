<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="inright">
    <div id="biaoge">
        <div class="biaogeks">操作员管理</div>
        <div id="functionTree">
            <%=ViewData["FunTree"].ToString()%>
        </div>
        <div style="margin-top:10px">
            <input id="Button1" type="button" value="保存" onclick="EmployeFunSave()" 
                style="width: 66px" />&nbsp;&nbsp;&nbsp;
            <input id="Button2" type="button" value="取消" style="width: 72px" onclick="EmployeReturn()" />
        </div>
    </div>
</div>