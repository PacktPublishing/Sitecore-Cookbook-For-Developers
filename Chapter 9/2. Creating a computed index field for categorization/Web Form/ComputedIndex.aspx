<%@ Page language="c#" autoeventwireup="true" inherits="ComputedIndex" Codebehind="ComputedIndex.aspx.cs" %>
<body>
    <form runat="server" id="mainform">

<h2>Search product information</h2>
<div style="width: 500px; margin: 0 auto;">
    <div style="width: 90%; float: left; text-align: left">
        <table style="border-width: 0px">
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
                    <asp:repeater runat="server" id="repeaterResults">
                        <HeaderTemplate>
                            <table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <img src='<%# Eval("Thumbnail") %>' />
                                </td>
                                <td>
                                    <a href='<%# Eval("Url") %>'><b><%# Eval("Title") %></b></a> by <i><%# Eval("Author") %></i>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:repeater>
                </td>
            </tr>
        </table>
    </div>
</div>

    </form>
</body>