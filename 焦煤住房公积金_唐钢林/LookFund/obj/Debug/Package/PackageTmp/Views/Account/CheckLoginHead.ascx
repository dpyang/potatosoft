<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>
<%@ Import Namespace="LookFund.Models.Membership" %>

<% 
    if (!string.Equals("false", Model))
    {
        %>
          <div><a href="<%:Url.Action("AdminMain","Account") %>" style="color:Blue"><%:Model%></a>你好，欢迎光临住房公积金查询系统！&nbsp;&nbsp;&nbsp;&nbsp;
            <a href="javascript:bookmark()" style="color:Blue">加入收藏</a>&nbsp;&nbsp;<a href="javascript:CancelLogin()" style="color:Blue">退出</a></div> 
        <%
    }
%>

