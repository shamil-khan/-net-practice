<%@ Page Language="C#"%>

<script runat="server">

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ButtonEcho.Click += (s, e1) => TextResponse.Text = string.Format("({0:hh.mm.ss.ffffff}) - t1 - {1}", DateTime.Now, TextRequest.Text);
    }

</script>

<!DOCTYPE >
<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextRequest" Text="Hello" Width="200px" runat="server" />
        <asp:Button ID="ButtonEcho" Text="Echo 1" Width="120px" runat="server" />
        <asp:Label ID="TextResponse" Width="100%" runat="server" />
    </div>
    </form>
</body>
</html>
