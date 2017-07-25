<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qrcode.aspx.cs" Inherits="WebApplication1.qrcode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        textarea {
            padding: 10px;
        }
        input {
            padding: 10px;
            margin-left: 60px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="padding:10px">
            Encode:&nbsp;&nbsp;<asp:TextBox ID="qrText" runat="server" TextMode="MultiLine" />
        </div>
         <div style="padding:10px">
            <asp:Button ID="updateButton" runat="server" Text="Update" OnClick="updateButton_Click" />
        </div>
        <div style="padding:10px">
            Decode:&nbsp;&nbsp;<asp:TextBox ID="qrDecode" runat="server" TextMode="MultiLine" />
        </div>
        <div style="padding:100px">
            <asp:Image ID="qrImage" runat="server" />
        </div>
	</div>
    </form>
</body>
</html>
