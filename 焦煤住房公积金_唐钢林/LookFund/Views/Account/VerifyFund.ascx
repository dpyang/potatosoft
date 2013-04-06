<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList>" %>
<%@ Import Namespace="LookFund.Dao.FundInfo" %>
<div id="inright">
    <div id="biaoge">
        <div class="biaogeks">数据验证</div>
        <div id="showFund" style="text-align:center"> 
             <table width="762" cellspacing="1" bgcolor="#88C4FF">               
                <%
                    ReadXML xml = ReadXML.getInstance();
                %>       
                <%=xml.VerifyShowHead %>
                <%
                foreach (var row in Model)
                {                    
                    %>
                    <tr>
                        <%
                        object[] Cols = (object[])row;
                        int maxRows = Cols.Count() - 1;
                        if (Cols[0].Equals("合计"))
                        {
                            for (int i = 0; i < maxRows;i++ )
                            {
                                %>
                                <td bgcolor="#EEF7FE"><span style="color:Red"><%:Cols[i]%></span></td>
                                <%
                            } 
                            %>
                                <td bgcolor="#EEF7FE">&nbsp;</td>
                            <%
                        }
                        else
                        {
                            for (int i = 0; i < maxRows;i++ )
                            {
                                %>
                                <td bgcolor="#EEF7FE"><%:Cols[i]%></td>
                                <%
                            } 
                            %>
                                <td bgcolor="#EEF7FE"><a href="javascript:VerifySucced('<%:Cols[maxRows]%>')">通过</a>
                                <input type="hidden" value="<%:Cols[maxRows] %>" /></td>
                            <%
                        }
                        %>
                    </tr>
                 <% }%>
           </table>
        </div>
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="762">
                <tr>
                    <td style="width:300px"><input id="Button1" type="button" value="当前页通过" onclick="PageSucced()" />
                    <input id="Button2" type="button" value="全部通过" onclick="SuccedAll()"/></td>
                    <td style="text-align:right"><%= ViewData["htmlPage"].ToString()  %></td>
                </tr>
            </table>
        </div>
    </div>
</div>

