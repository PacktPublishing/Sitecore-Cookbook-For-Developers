<%@ page language="c#" autoeventwireup="true" inherits="DidYouMean" CodeFile="DidYouMean.aspx.cs" %>

<body>
    <form runat="server" id="mainform">

        <h2>Did You Mean example</h2>
        <div style="width: 500px; margin: 0 auto;">
            <div style="width: 100%; float: left; text-align: left">
                <table style="border: 1px solid #888; margin: 0 auto;">
                    <tr>
                        <td>
                            <asp:textbox id="txtSearchText" runat="server" />
                        </td>
                        <td>
                            <asp:button id="btnSearch" text="Search" runat="server" onclick="btnSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:label id="lblDidYouMean" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="border: 1px solid #888; padding: 10px 5px;">
                            <asp:repeater runat="server" id="repeaterResults">
                                <HeaderTemplate>
                                    <table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <a href='<%# Eval("Title") %>'><%#Eval("Title") %></a>
                                            <br />
                                            <%#Eval("Description") %><hr />
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
