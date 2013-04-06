<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="LookFund.Models.Membership" %>

<div id="inright">
    <div id="biaoge">
        <div class="biaogeks">
            公积金历史数据导入</div>
        <form id="fileHistoryPost" action="<%:Url.Action("HistoryDateImport","Account") %>" method="post" enctype="multipart/form-data">
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
                <td bgcolor="#CEE7FF" style="width:100px; text-align:right">
                    上传文件：
                </td>
                <td bgcolor="#CEE7FF">
                   <input id="upfile" name="upfile" type="file" size="35" />&nbsp;&nbsp;<font color="red">*</font> 文件格式:山西焦煤_201106.xls
                </td>
            </tr>
            <tr>
                <td bgcolor="#EEF7FE">
                    &nbsp;
                </td>
                <td bgcolor="#EEF7FE">
                    <input id="Submit1" type="button" value="上传" onclick="uploadHistoryFile()" 
                        style="width: 63px" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="submit2" type="button" value="保存" onclick="uploadHistorySvae()" style="width:63px"  />&nbsp;&nbsp;&nbsp;&nbsp;
                    <%--<input id="submit3" type="button" value="删除重复导入的" />--%>
                </td>                
            </tr>
        </table>
       </form>       
    </div>
</div>