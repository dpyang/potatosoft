<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<LookFund.Models.ViewModel.LoginViewModel>" %>

<%@ Import Namespace="LookFund.Models.Membership" %>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Scripts/Home/Login.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="tlogin">
        <div id="tloginbk">
            <div id="tloginz">
                <div id="tlogin1" style="font-size: 18px">
                    住房公积金查询登录</div>
                <% Html.BeginForm("Login", "Home", FormMethod.Post, new { onsubmit = "submit()" }); %>
                <div id="tlogin2">
                    <ul>
                        <li class="tlogingai"><span style="margin-left: 11px;"></span>所属单位：
                            <%: Html.DropDownListFor(m=>m.strAddress, MEmploye.SelectListEnum(typeof(Company),true,false))%>
                        </li>
                        <li class="tlogingai">身份证号码：
                            <%: Html.TextBoxFor(m=>m.LoginName)%>
                            <%-- <%: Html.ValidationMessageFor(m => m.LoginName) %>--%>
                        </li>
                        <li class="tlogingai"><span class="konggek">密</span>码：
                            <%: Html.PasswordFor(m => m.LoginPwk) %>
                            <%--  <%: Html.ValidationMessageFor(m => m.LoginPwk) %>--%>
                        </li>
                        <li class="tlogingai1">
                            <table border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="border: 0px;">
                                        <span class="konggek1">验</span><span class="konggek2">证</span>码：<%: Html.TextBoxFor(m => m.ValidCode, new { maxlength = "5" })%>
                                    </td>
                                    <td style="border: 0px">
                                        &nbsp;&nbsp;&nbsp;&nbsp;<img id="valiCode" style="cursor: pointer;" src="<%:Url.Action("GetValidateCode","Home") %>"
                                            alt="验证码" />
                                    </td>
                                </tr>
                            </table>
                        </li>
                    </ul>
                </div>
                <div id="tlogin3">
                    <a href="javascript:submitLogin()">
                        <img src="../../Content/UiStyle/images/login1.jpg" width="58" height="22" alt="" /></a>
                    <a href="javascript://">
                        <img src="../../Content/UiStyle/images/login2.jpg" width="58" height="22" alt="" /></a>
                </div>
                <% Html.EndForm(); %>


                <div id="tlogin4">
                <div id="tlogin6" style="text-align: left; vertical-align: top">
                    <%:Html.ValidationSummary() %></div>
                   <%--<a href="javascript://">忘记密码</a>--%>
                 </div>
                
            </div>
        </div>
    </div>
</asp:Content>
