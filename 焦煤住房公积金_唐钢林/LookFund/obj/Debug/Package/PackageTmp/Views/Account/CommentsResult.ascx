<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<table width="762" cellspacing="1" bgcolor="#88C4FF">
<% 
    var list = ViewData["InfoList"] as IList<LookFund.Models.ViewModel.CommentsManageViewModel>;          
    foreach (var item in list)
    {
        %>
        <tr>
            <td bgcolor="#CEE7FF" style="width:50px; text-align:right">意见：</td>
            <td bgcolor="#CEE7FF"><%:item.Conent %></td>
            <td bgcolor="#CEE7FF" style="width:100px;">发表人：<%:item.EmpName %></td>
            <td bgcolor="#CEE7FF" style="width:50px;"><a href="javascript:CommentsReply('<%:item.Uid %>')">回复</a></td>
        </tr>
        <%
        if (!string.IsNullOrEmpty(item.Reply))
        {
            %>
            <tr>
                <td bgcolor="#eef7fe" style="text-align:right;">回复：</td>
                <td bgcolor="#eef7fe" colspan="3"><%:item.Reply %></td>
            </tr>
            <%
        }
    }
%>
</table>
<div style="text-align:center">
<%= ViewData["pageHtml"].ToString() %>
</div>
