<%@ Page language="c#" autoeventwireup="true" codefile="ContentSearch.aspx.cs" inherits="ContentSearch" %>
<body>
    <form runat="server" id="mainform">

        <div style="width: 100%">
            <b>Search:</b>
            <asp:textbox runat="server" id="txtInput" />
            &nbsp;&nbsp;&nbsp;
    <b>Page Size:</b>
            <asp:dropdownlist id="ddlPageSize" runat="server" autopostback="true" onselectedindexchanged="btnSubmit_Click">
        <asp:ListItem value="3" Text="3"></asp:ListItem>
        <asp:ListItem value="10" Text="10"></asp:ListItem>
        <asp:ListItem value="25" Text="25"></asp:ListItem>
    </asp:dropdownlist>
            <br />
            <b>Order By:</b>
            <asp:radiobuttonlist id="rbtOrder" runat="server" repeatdirection="Horizontal" autopostback="True" onselectedindexchanged="btnSubmit_Click" repeatlayout="Flow">
            <asp:ListItem Text="Name" Value="name"></asp:ListItem>
            <asp:ListItem Text="Modified Date" Value="date"></asp:ListItem>
            <asp:ListItem Text="Auto" Value="" selected></asp:ListItem>
        </asp:radiobuttonlist>
            <asp:button id="btnSubmit" text="Search" onclick="btnSubmit_Click" runat="server" />
        </div>
        <div style="float: left; width: 500px;">
            <i>
                <asp:label runat="server" id="lblResult" />
            </i>
            <asp:repeater runat="server" id="repeaterSearch">
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <b><%#Eval("Name") %></b> (<i><%#Eval("Updated") %></i>)
                    <hr />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate></table></FooterTemplate>
    </asp:repeater>

            <asp:repeater runat="server" id="repeaterPage">
        <ItemTemplate>
            <asp:LinkButton runat="server" Text='<%# Container.DataItem.ToString() %>' id="lnkPageNo" OnClick="lnkPageNo_Click" />
        </ItemTemplate>
    </asp:repeater>
        </div>

    </form>
</body>
