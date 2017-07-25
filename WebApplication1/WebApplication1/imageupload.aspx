<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="imageupload.aspx.cs" Inherits="WebApplication1.imageupload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:FileUpload id="FileUploadControl" runat="server" />
        <asp:Button runat="server" id="UploadButton" text="Upload" OnClick="UploadButton_Click"/>
        <br /><br />
        <asp:Label runat="server" id="StatusLabel" text="Upload status: " />
        <div style="padding:50px">
            <asp:Image ID="uploadedImage" runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
