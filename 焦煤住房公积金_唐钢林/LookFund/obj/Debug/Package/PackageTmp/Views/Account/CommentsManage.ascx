<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div id="inright">
    <div id="biaoge">
        <div class="biaogeks">
           处理问题</div>
        <div id="Search">
             <table width="762" cellspacing="1" bgcolor="#88C4FF">
                <tr>
                    <td bgcolor="#CEE7FF"><%: Html.TextBox("txtSearch")%>&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="btnSearch" type="button" value="搜索" onclick="CommentSearch()" style="width: 67px" /></td>
                </tr>
             </table>
        </div>
        <br />
        <div id="showComments">            
            <% Html.RenderAction("CommentsResult", "Account"); %>
        </div>
        <div id="CommentsReply"> </div>
    </div>
</div>