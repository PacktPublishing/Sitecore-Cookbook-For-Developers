<%@ Page language="c#" autoeventwireup="true" inherits="MoreLikeThisSearch" CodeFile="MoreLikeThisSearch.aspx.cs" %>
<body>
    <form runat="server" id="mainform">

        <h2>Similar Results</h2>
        <div style="width: 500px; margin: 0 auto;">
            <div style="width: 100%; float: left; text-align: left">
                <table style="border: 1px solid #888; margin: 0 auto;">
                    <tr>
                        <td>
                            <asp:textbox id="txtSearch" runat="server" />
                        </td>
                        <td>
                            <asp:button id="btnSearch" text="Search" runat="server" onclick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:placeholder id="phResults" runat="server" />
                            <asp:repeater runat="server" id="repeaterResults">
                                <HeaderTemplate>
                                    <table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <a href='?relatedto=<%# Eval("ID") %>'><%#Eval("Title") %></a> - <i><a href='?relatedto=<%# Eval("ID") %>'>Similar</a></i>
                                            <br />
                                            <%#Eval("Description") %>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                </table>
                </FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

    </form>
</body>
