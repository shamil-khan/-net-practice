<%@ Page Language="C#" Inherits="Sky.Web.Views.DefaultView" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>default </title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:right; width:90%" >
        <asp:Label ID="LabelUser" runat="server" Width="100%" BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="X-Large" />
    </div>
    <hr />
    <div>
        <asp:GridView ID="GridUser" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="Item1" HeaderText="Name">
                    <ItemStyle Width="300px" />
                </asp:BoundField>
                <asp:BoundField DataField="Item2" HeaderText="Value">
                    <ItemStyle Width="400px" />
                </asp:BoundField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
