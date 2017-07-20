<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Data</title>
</head>
    
<body>
    <form id="form1" runat="server">
    <asp:DropDownList ID="ddlCategories" runat="server" />
    <asp:DataGrid ID="discoveredAPI" runat="server" />


    <div id="domains" runat="server"></div>
    <div id="results" runat="server"></div>
    </form>
</body>

</html>
