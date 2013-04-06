<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LookFund.Models.Membership.MComments>" %>

<% Html.BeginForm("CommentsManage", "Account", FormMethod.Post, new { ID = "formCommentsReply" }); %>
<table width="762" cellspacing="1" bgcolor="#88C4FF">
    <tr>
        <td bgcolor="#eef7fe">&nbsp;</td>
        <td bgcolor="#eef7fe">回复：<%: Html.Label(Model.Conent) %></td>
    </tr>
    <tr>
        <td bgcolor="#eef7fe" style="width:50px">回复:</td>
        <td bgcolor="#eef7fe">
            <%: Html.TextArea("Reply", new { Style = "width:350px; height:150px;" })%></td>
    </tr>
        <tr>
        <td bgcolor="#eef7fe">
             <%:Html.Hidden("Uid",Model.Uid) %>
        </td>
        <td bgcolor="#eef7fe">
            <input id="Button1" type="button" value="提交" onclick="simbitReply()" /></td>
    </tr>
</table>
<% Html.EndForm(); %>