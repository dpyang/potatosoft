<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%: Html.DropDownList("unitName",ViewData["selectList"] as SelectList) %>