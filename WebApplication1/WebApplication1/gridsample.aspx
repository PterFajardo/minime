<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="gridsample.aspx.cs" Inherits="WebApplication1.gridsample" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        div {
            padding: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div><asp:LinkButton ID="lnkTable" Text="table" runat="server" />&nbsp;|&nbsp;<asp:LinkButton ID="lnkGrid" Text="grid" runat="server" /></div>
        <div>
            <asp:ListView ID="lstData" runat="server">
                <LayoutTemplate>
                    <table runat="server" id="table1" >
                        <tr runat="server" id="itemPlaceholder" ></tr>
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
                                    <asp:Image ID="picture" runat="server" ImageUrl='<%#Eval("Picture") %>' Height="30px" />
                                </td>
                                <td>
                                    <asp:Label ID="FirstNameLabel" runat="server" Text='<%#Eval("FirstName") %>' />
                                    <asp:Label ID="LastNameLabel" runat="server" Text='<%#Eval("LastName") %>' />
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
                    <table runat="server" id="table1" >
                        <tr runat="server" id="groupPlaceholder" ></tr>
                    </table>
                    <%--<asp:DataPager runat="server" ID="DataPager" PageSize="2">
                      <Fields>
                        <asp:NumericPagerField ButtonCount="5" PreviousPageText="<--" NextPageText="-->" />
                      </Fields>
                    </asp:DataPager>--%>
                </LayoutTemplate>
                <GroupTemplate>
                    <tr runat="server" id="personRow" style="height:80px">
                      <td runat="server" id="itemPlaceholder"></td>
                    </tr>
                  </GroupTemplate>
                  <ItemTemplate>
                      <td runat="server">
                        <table>
                            <tr>
                                <td>
                                    <asp:Image ID="picture" runat="server" ImageUrl='<%#Eval("Picture") %>' />
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
