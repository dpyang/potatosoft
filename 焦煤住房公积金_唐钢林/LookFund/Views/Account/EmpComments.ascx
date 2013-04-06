<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LookFund.Models.ViewModel.CommentsViewModel>" %>
<%@ Import Namespace="LookFund.Models.Membership" %>
<script src="../../Scripts/Account/EmpComments.js" type="text/javascript"></script>

<div id="inright">
    <div id="biaoge">
        <div class="biaogeks">
            上报问题</div>
        <div id="showComments">            
            <table width="762" cellspacing="1" bgcolor="#88C4FF">
                <% 
                    var Commlist = ViewData["Comments"] as ICollection<MComments>;
                    foreach (var item in Commlist)
                    {
                        %>
                        <tr>
                            <td bgcolor="#CEE7FF" style="width:150px; text-align:right">问题描述：</td>
                            <td bgcolor="#CEE7FF"><%:item.Conent %></td>
                        </tr>
                        <%
                        if (!string.IsNullOrEmpty(item.Reply))
                        {
                            %>
                            <tr>
                                <td bgcolor="#eef7fe" style="text-align:right">回复：</td>
                                <td bgcolor="#eef7fe"><%:item.Reply %></td>
                            </tr>
                            <%
                        }
                    }
                %>
            </table>
            <br />
            <br />
        </div>
        <% Html.BeginForm("EmpComments", "Account", FormMethod.Post, new { ID = "FormComments" }); %>
             <table width="762" cellspacing="1" bgcolor="#88C4FF">
                <tr>
                    <td bgcolor="#eef7fe" style="width:150px; text-align:right">编辑意见：</td>
                    <td bgcolor="#eef7fe">
                        <%: Html.TextAreaFor(m => m.Conent, new { Style="width:350px; height:150px;"})%>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#eef7fe">&nbsp;</td>
                    <td bgcolor="#eef7fe">
                        <input id="Button1" type="button" value="提交" onclick="simbitComments()" /></td>
                </tr>
             </table>
        <% Html.EndForm(); %>
    </div>
</div>