<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LookFund.Models.Membership.MEmploye>" %>
<%@ Import Namespace="LookFund.Models.Membership" %>
<script type="text/javascript">
    function loadright(str) {
        $('#right').load(str);
    }
</script>
<%
    if ("22".Equals(Model.EmpKind))
    {
        %>
            <div class="inleft2">
                <div class="inleft21">数据查询</div>
                <div class="inleft22">
                    <ul>
                        <li><img src="../../Content/UiStyle/images/inlanmu1.gif" width="4" height="7" alt="" />
                        <a href="javascript://" onclick="$('#right').load('FundInfoData')">个人账户当月信息查询</a></li>
                        <li><img src="../../Content/UiStyle/images/inlanmu1.gif" width="4" height="7" alt="" />
                        <a href="javascript://" onclick="$('#right').load('FundLookTime')">个人账户历史信息查询</a></li>
                        <li><img src="../../Content/UiStyle/images/inlanmu1.gif" width="4" height="7" alt="" />
                        <a href="javascript://" onclick="$('#right').load('EmpCenterLookShow')">太原市中心数据查询</a></li>
                        <li><img src="../../Content/UiStyle/images/inlanmu1.gif" width="4" height="7" alt="" />
                        <a href="javascript:PrintPage()">打印</a></li>
                    </ul>
                </div>
            </div>
            <div class="inleft2">
                <div class="inleft21">密码管理</div>
                <div class="inleft22">
                    <ul>
                         <li><img src="../../Content/UiStyle/images/inlanmu1.gif" width="4" height="7" alt="" />
                        <a href="javascript://" onclick="$('#right').load('PwdEdit')">修改密码</a></li>
                    </ul>
                </div>
            </div>
            <div class="inleft2">
                <div class="inleft21">上报问题</div>
                <div class="inleft22">
                    <ul>
                        <li><img src="../../Content/UiStyle/images/inlanmu1.gif" width="4" height="7" alt="" />
                        <a href="javascript://" onclick="$('#right').load('EmpComments')">上报问题</a></li>
                    </ul>
                </div>
            </div>
        <%
    }
    else
    {
        var list = Model.FunctionList;
        foreach (var item in list)
        {
            if (item.LevelCode.Length.Equals(4))
            {
            %>
            <div class="inleft2">
            <div class="inleft21"><%:item.FunctionName%></div>
            <div class="inleft22">
                <ul>
                    <%
                        foreach (var li in list)
                        {
                            if (li.LevelCode.Length > 4 && li.LevelCode.Contains(item.LevelCode))
                            {
                                if (li.FunctionName.Equals("打印"))
                                {
                                    %>
                                    <li>
                                    <img src="../../Content/UiStyle/images/inlanmu1.gif" width="4" height="7" alt="" />
                                    <a href="javascript:PrintPage()">打印</a></li>
                                    <%
                                }
                                else
                                {
                                    %><li>
                                    <img src="../../Content/UiStyle/images/inlanmu1.gif" width="4" height="7" alt="" />
                                    <a href="javascript:loadright('<%:li.ActionName %>')"><%:li.FunctionName%></a></li>
                                    <%
                                }
                            }
                        }
                    %>
                </ul>
            </div>
            </div>
            <%
            }
        }
    }
 %>
