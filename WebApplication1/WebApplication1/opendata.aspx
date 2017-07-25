<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="opendata.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Data</title>
</head>
    
<body>
    <form id="form1" runat="server">
    <div>
        Search&nbsp;&nbsp;<asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
        <asp:Button ID="btnSeach" OnClick="btnSeach_Click" Text="Search" runat="server" />
        &nbsp;&nbsp;<asp:Button ID="btnReset" OnClick="btnReset_Click" Text="Reset" runat="server" />
        &nbsp;&nbsp;<asp:Literal ID="litResultCount" runat="server"></asp:Literal>
    </div>
    <div>
        Categories&nbsp;&nbsp;<asp:DropDownList ID="ddlCategories" runat="server" OnSelectedIndexChanged="ddlCategories_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    </div>
    <div>
        <asp:DataGrid ID="discoveredAPI" runat="server" OnItemDataBound="discoveredAPI_ItemDataBound"
                BorderColor="Gray"
                BorderWidth="1"
                CellPadding="3"
                AutoGenerateColumns="False"
                UseAccessibleHeader="true"
                PageSize="5"
                AllowPaging="True"
                OnPageIndexChanged="Grid_Change"
                ItemStyle-Font-Size="Medium" 
                AllowSorting="true" 
                OnSortCommand="discoveredAPI_SortCommand" OnItemCommand="discoveredAPI_ItemCommand">
            <HeaderStyle BackColor="LightBlue"  /> 
            <Columns>
                <asp:TemplateColumn>
	                <ItemTemplate>
		                <asp:LinkButton ID="btnGetData" runat="server" Text="Get Data" ></asp:LinkButton>
	                </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="Id" 
                     SortExpression="Id"
                     HeaderText="Id"
                     ItemStyle-Width="50px" />

                <asp:BoundColumn DataField="UpdateDate" 
                    HeaderText="Update Date" 
                    SortExpression="UpdateDate" />

                <asp:BoundColumn DataField="Name" 
                     HeaderText="Name"
                     SortExpression="Name"/>
                
                <asp:BoundColumn DataField="Description" 
                     HeaderText="Description"
                     SortExpression="Description"/>

                <asp:BoundColumn DataField="Categories" 
                     HeaderText="Categories"
                     SortExpression="Categories"/>

                <asp:BoundColumn DataField="Link" 
                     HeaderText="Link"
                     SortExpression="Link"/>

                <asp:BoundColumn DataField="Domain" 
                     HeaderText="Domain"
                     SortExpression="Domain"/>

                <asp:BoundColumn DataField="ResourceId" 
                     HeaderText="ResourceId"
                     SortExpression="ResourceId"/>

            </Columns>
            
            <ItemStyle HorizontalAlign="Right" />
        </asp:DataGrid>
    </div>

    <div id="domains" runat="server"></div>
    <div id="results" runat="server"></div>
    </form>
</body>

</html>
