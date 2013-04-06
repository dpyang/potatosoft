<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LookFund.Models.Membership" %>

<div id="inright">
    <div id="biaoge">
        <div class="biaogeks">
            市中心数据导入</div>
        <form id="CanterfilePost" action="<%:Url.Action("CenterImport","Account") %>" method="post" enctype="multipart/form-data">
        <table width="762" cellspacing="1" bgcolor="#88C4FF">
            <tr>
                <td bgcolor="#CEE7FF" style="width:100px; text-align:right">
                    所属单位：
                </td>
                <td bgcolor="#CEE7FF">
                    <%: Html.DropDownList("ddlAddress", MEmploye.SelectListEnum(typeof(Company)))%>
                </td>                
            </tr>
            <tr>
                <td bgcolor="#CEE7FF" style="width:100px; text-align:right;">
                    市中心文件：
                </td>
                <td bgcolor="#CEE7FF">
                   <input id="upfile" name="upfile" type="file" /> 
                </td>
            </tr>
            <tr>
                <td bgcolor="#EEF7FE">
                    &nbsp;
                </td>
                <td bgcolor="#EEF7FE">
                    <input id="Submit1" type="button" value="上传" onclick="CanteruploadFile()" style="width: 63px"/>
                </td>                
            </tr>
        </table>
       </form>       
    </div>
</div>