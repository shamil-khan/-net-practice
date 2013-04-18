<%@ Page Language="C#" Inherits="Sky.Web.Views.LoginView" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>login</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="ButtonLoginGoogle" Text="Login From Google" runat="server" Width="200px" />
    </div>
    <hr />
    <asp:Label ID="LabelStatus" runat="server" />
    </form>
</body>
</html>
