<%@ page language="c#" autoeventwireup="true" inherits="BoostResults" CodeFile="BoostResults.aspx.cs" %>

<body>
    <form runat="server" id="mainform">

        <h2>Search product information</h2>
        <div style="width: 500px; margin: 0 auto;">
            <div style="width: 90%; float: left; text-align: left">
                <table style="border-width: 0px">
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
                            <asp:placeholder id="phResults" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>


    </form>
</body>
