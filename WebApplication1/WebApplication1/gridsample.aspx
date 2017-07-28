<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gridsample.aspx.cs" Inherits="WebApplication1.gridsample" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        div {
            padding: 10px;
        }

        input {
            padding: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="1">
                <tr>
                    <td colspan="2" style="text-align: center;">
                        <h2>Create Person</h2>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            <div>
                                <asp:Image ID="personImage" ImageUrl="~/images/persons/blue-noimage.png" runat="server" Height="100px" Width="95px" /></div>
                            <asp:FileUpload ID="imageUploader" runat="server" Width="80px" onchange="this.form.submit()" />
                        </div>
                    </td>
                    <td>
                        <div>
                            First Name:&nbsp;<asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
                        </div>
                        <div>
                            Last Name:&nbsp;<asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right">
                        <div>
                            <asp:Button ID="btnCreate" Text="Create" runat="server" OnClick="btnCreate_Click" /></div>
                    </td>
                </tr>
            </table>
        </div>



        <div>
            <asp:LinkButton ID="lnkTable" Text="table" runat="server" />&nbsp;|&nbsp;<asp:LinkButton ID="lnkGrid" Text="grid" runat="server" /></div>
        <div>
            <asp:ListView ID="lstData" runat="server" OnItemEditing="lstData_ItemEditing" OnItemDeleting="lstData_ItemDeleting" DataKeyNames="Id">
                <LayoutTemplate>
                    <table runat="server" id="table1">
                        <tr runat="server" id="itemPlaceholder"></tr>
                    </table>
                    <%-- <asp:DataPager runat="server" ID="lstDataPager" PageSize="2">
                        <Fields>
                            <asp:TemplatePagerField>
                              <PagerTemplate>
                                &nbsp;
                                <asp:TextBox ID="CurrentRowTextBox" runat="server" AutoPostBack="true" Text="<%# Container.StartRowIndex + 1%>" Columns="1" style="text-align:right" 
                                     OnTextChanged="CurrentRowTextBox_OnTextChanged" />
                                to
                                <asp:Label ID="PageSizeLabel" runat="server" Font-Bold="true"
                                     Text="<%# Container.StartRowIndex + Container.PageSize > Container.TotalRowCount ? Container.TotalRowCount : Container.StartRowIndex + Container.PageSize %>" />
                                of
                                <asp:Label ID="TotalRowsLabel" runat="server" Font-Bold="true" Text="<%# Container.TotalRowCount %>" />
                              </PagerTemplate>
                            </asp:TemplatePagerField>
                            <asp:NextPreviousPagerField 
                                 ShowFirstPageButton="true" ShowLastPageButton="true"
                                 FirstPageText="|<< " LastPageText=" >>|"
                                 NextPageText=" > " PreviousPageText=" < " />
                          </Fields>
                    </asp:DataPager>--%>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr id="Tr1" runat="server">
                        <td id="Td1" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Image ID="picture" runat="server" ImageUrl='<%#Eval("Picture") %>' Height="30px" Width="30px" />
                                    </td>
                                    <td>
                                        <asp:Label ID="FirstNameLabel" runat="server" Text='<%#Eval("FirstName") %>' />
                                        <asp:Label ID="LastNameLabel" runat="server" Text='<%#Eval("LastName") %>' />
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lnkEditBtn" runat="server" Text="Edit" CommandName="Edit"></asp:LinkButton>
                                        &nbsp;|&nbsp;
                                    <asp:LinkButton ID="lnkDeleteBtn" runat="server" Text="Delete" CommandName="Delete"></asp:LinkButton>
                                    </td>
                                </tr>
                            </table>

                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div>
            <asp:ListView ID="grpData" runat="server" GroupItemCount="3" DataKeyNames="Id">
                <LayoutTemplate>
                    <table runat="server" id="table1">
                        <tr runat="server" id="groupPlaceholder"></tr>
                    </table>
                    <%--<asp:DataPager runat="server" ID="DataPager" PageSize="2">
                      <Fields>
                        <asp:NumericPagerField ButtonCount="5" PreviousPageText="<--" NextPageText="-->" />
                      </Fields>
                    </asp:DataPager>--%>
                </LayoutTemplate>
                <GroupTemplate>
                    <tr runat="server" id="personRow" style="height: 80px">
                        <td runat="server" id="itemPlaceholder"></td>
                    </tr>
                </GroupTemplate>
                <ItemTemplate>
                    <td runat="server">
                        <table>
                            <tr>
                                <td>
                                    <asp:Image ID="picture" runat="server" ImageUrl='<%#Eval("Picture") %>' Height="100px" Width="95px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="FirstNameLabel" runat="server" Text='<%#Eval("FirstName") %>' />
                                    <asp:Label ID="LastNameLabel" runat="server" Text='<%#Eval("LastName") %>' />
                                </td>
                            </tr>
                        </table>
                    </td>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </form>
</body>
</html>
