<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/NavMenu.Master" Inherits="System.Web.Mvc.ViewPage<LookFund.Models.Membership.MEmploye>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Scripts/jquery.form.js" type="text/javascript"></script>
    <script src="../../Scripts/Account/ReportImport.js" type="text/javascript"></script>
    <script src="../../Scripts/Account/PwdEdit.js" type="text/javascript"></script>
    <script src="../../Scripts/Account/CommentsManage.js" type="text/javascript"></script>
    <script src="../../Scripts/Account/VerifyFund.js" type="text/javascript"></script>
    <script src="../../Scripts/Account/OperatorManage.js" type="text/javascript"></script>
    <script src="../../Scripts/Account/EmpFundLook.js" type="text/javascript"></script>
    <script src="../../Scripts/Account/CenterImport.js" type="text/javascript"></script>
    <link href="../../Scripts/ExtSystem/resources/css/ext-all.css" rel="stylesheet" type="text/css"/>
	<script src="../../Scripts/ExtSystem/adapter/ext/ext-base.js" type="text/javascript"></script>
	<script src="../../Scripts/ExtSystem/ext-all.js" type="text/javascript"></script>          	
    <script src="../../Scripts/ExtSystem/ext-lang-zh_CN.js" type="text/javascript" charset="utf-8"></script> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="inleft">
        <% Html.RenderAction("LeftMemu");%>
    </div>
    <div id="right">
        <%
            if ("22".Equals(Model.EmpKind))
            {
                Html.RenderAction("FundInfoData");
            }
            else if ("11".Equals(Model.EmpKind))
            {
                Html.RenderAction("EmpFundLook");
            }
            else
            {
                Html.RenderAction("BaseImport");
            }
        %>
    </div>
</asp:Content>
