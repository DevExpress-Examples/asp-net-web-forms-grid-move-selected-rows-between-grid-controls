<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v13.1, Version=13.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="mainForm" runat="server">
    <div>
        <dx:ASPxGridView ID="gridView" runat="server" AutoGenerateColumns="False" DataSourceID="ds"
            KeyFieldName="CategoryID">
            <Columns>
                <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="CategoryID" ReadOnly="True" VisibleIndex="1">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CategoryName" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="3">
                </dx:GridViewDataTextColumn>
            </Columns>
        </dx:ASPxGridView>
        <asp:AccessDataSource ID="ds" runat="server" DataFile="~/App_Data/nwindNew.mdb" SelectCommand="SELECT [CategoryID], [CategoryName], [Description] FROM [Categories]">
        </asp:AccessDataSource>
    </div>
    <br />
    <dx:ASPxButton ID="btnMove" runat="server" Text="Move" OnClick="btnMove_Click">
    </dx:ASPxButton>
    <br />
    <table cellpadding="1" cellspacing="1">
        <tr>
            <td>
                <dx:ASPxLabel ID="lblUpdatedRowsCountText" runat="server" Text="UpdatedRowsCount = ">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxLabel ID="lblUpdatedRowsCount" runat="server" Text="">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxLabel ID="lblTotalByColumnCategoryIDText" runat="server" Text="TotalByColumnCategoryID = ">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxLabel ID="lblTotalByColumnCategoryID" runat="server" Text="">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxLabel ID="lblInsertedRowsCountText" runat="server" Text="InsertedRowsCount = ">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxLabel ID="lblInsertedRowsCount" runat="server" Text="">
                </dx:ASPxLabel>
            </td>
        </tr>
        <tr>
            <td>
                <dx:ASPxLabel ID="lblTotalByCategoryNameText" runat="server" Text="TotalByColumnCategoryName = ">
                </dx:ASPxLabel>
            </td>
            <td>
                <dx:ASPxLabel ID="lblTotalByCategoryName" runat="server" Text="">
                </dx:ASPxLabel>
            </td>
        </tr>        
    </table>
    <br />
    <dx:ASPxGridView ID="gridViewUpdated" runat="server" AutoGenerateColumns="False"
        DataSourceID="dsUpdated" KeyFieldName="CategoryID">
        <Columns>
            <dx:GridViewCommandColumn VisibleIndex="0">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn FieldName="CategoryID" ReadOnly="True" VisibleIndex="1">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
        </Columns>
    </dx:ASPxGridView>
    <asp:AccessDataSource ID="dsUpdated" runat="server" DataFile="~/App_Data/nwindNew.mdb"
        SelectCommand="SELECT [CategoryID], [Description] FROM [CategoriesUpdated]">
    </asp:AccessDataSource>
    <dx:ASPxButton ID="btnRestore" runat="server" Text="Restore Database" OnClick="btnRestore_Click">
    </dx:ASPxButton>
    </form>
</body>
</html>
