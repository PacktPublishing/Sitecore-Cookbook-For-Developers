<%@ Page language="c#" autoeventwireup="true" inherits="FacetedSearch" codeFile="FacetedSearch.aspx.cs" %>
<body>
    <form id="form1" runat="server">
        Search keyword:
    <asp:textbox runat="server" id="txtInput" />
        <input type="submit" id="btnSubmit" value="Search" />
        </div>

        <div style="float: left; width: 100px;">
            <b>Tags</b>

            <asp:repeater runat="server" id="repeaterFacets" visible="false">
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <input type="checkbox" name="facets" id='<%#Eval("Id") %>' value='<%#Eval("Id") %>' <%#Eval("Selected") %> onclick="document.getElementById('btnSubmit').click();" />
                    <label for='<%#Eval("Id") %>'><%#Eval("Name") %></label>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate></table></FooterTemplate>
    </asp:repeater>

        </div>
        <div style="float: left; width: 500px;">
            <h4>
                <asp:label runat="server" id="lblResult" />
            </h4>
            <asp:repeater runat="server" id="repeaterBooks">
        <HeaderTemplate>
            <table>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <b><%#Eval("Title") %></b> by <i><%#Eval("Author") %></i>
                    <hr />
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate></table></FooterTemplate>
    </asp:repeater>
        </div>
    </form>
</body>
