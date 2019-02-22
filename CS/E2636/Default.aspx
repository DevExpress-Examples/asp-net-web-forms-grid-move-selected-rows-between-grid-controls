<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="E2636.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .container {
            display: table;
        }
        .contentButtons {
            padding-top:20px; 
            padding-bottom:10px;
        }
        .button {
                width:100% !important;
        }
        @media(min-width:790px) {
            .contentEditors, .contentButtons {
                display: table-cell;
                width: 33.33333333%;
            }
            .button {
                width:170px !important;
            }
            .contentEditors {
                vertical-align: top;
            }
            .contentButtons {
                vertical-align: middle;
                text-align: center;
            }
        }
    </style>
    <script type="text/javascript">
        var command = null;
        function AddSelectedRows() {
            command = 'addSelectedRows';
            UpdateTargetGrid();
        }
        function AddAllRows() {
            command = 'addAllRows';
            UpdateTargetGrid();
        }
        function RemoveSelectedRows() {
            command = 'removeSelectedRows';
            UpdateSourceGrid();
        }
        function RemoveAllRows() {
            command = 'removeAllRows';
            UpdateSourceGrid();
        }
        function UpdateSourceGrid() {
            if (command != null)
                sourceGrid.PerformCallback(command);
            else
                sourceGrid.Refresh();
        }
        function UpdateTargetGrid() {
            if (command != null)
                targetGrid.PerformCallback(command);
            else {
                targetGrid.Refresh();
            }
        }
        function UpdateButtonState() {
            btnMoveAllItemsToRight.SetEnabled(sourceGrid.GetVisibleRowsOnPage() > 0);
            btnMoveAllItemsToLeft.SetEnabled(targetGrid.GetVisibleRowsOnPage() > 0);
            btnMoveSelectedItemsToRight.SetEnabled(sourceGrid.GetSelectedRowCount() > 0);
            btnMoveSelectedItemsToLeft.SetEnabled(targetGrid.GetSelectedRowCount() > 0);
        }
        function onControlsInitialized(s, e) {
            switch (command) {
                case 'addSelectedRows':
                case 'addAllRows':
                    command = null;
                    UpdateSourceGrid();
                    break;
                case 'removeSelectedRows':
                case 'removeAllRows':
                    command = null;
                    UpdateTargetGrid();
                    break;
                default:
                    UpdateButtonState();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <dx:ASPxGlobalEvents ID="GlobalEvents" runat="server">
        <ClientSideEvents ControlsInitialized="onControlsInitialized" />
    </dx:ASPxGlobalEvents>
    <div class="container">
        <div class="contentEditors">
            <dx:ASPxGridView ID="SourceGrid" ClientInstanceName="sourceGrid" runat="server" AutoGenerateColumns="false" KeyFieldName="CategoryID"
                DataSourceID="SourceDS" OnCustomCallback="SourceGrid_CustomCallback">
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" />
                    <dx:GridViewDataTextColumn FieldName="CategoryName" />
                    <dx:GridViewDataTextColumn FieldName="Description" />
                </Columns>
                <SettingsBehavior AllowSelectByRowClick="true" />
                <ClientSideEvents SelectionChanged="function(s, e) { UpdateButtonState(); }" />
            </dx:ASPxGridView>
            <asp:AccessDataSource ID="SourceDS" runat="server" DataFile="~/App_Data/nwindNew.mdb" 
                SelectCommand="SELECT [CategoryID], [CategoryName], [Description] FROM [Categories]">
            </asp:AccessDataSource>
        </div>
        <div class="contentButtons">
            <div>
                <dx:ASPxButton ID="btnMoveSelectedItemsToRight" runat="server" ClientInstanceName="btnMoveSelectedItemsToRight" CssClass="button"
                    AutoPostBack="False" Text="Add >" ClientEnabled="False"
                    ToolTip="Add selected items">
                    <ClientSideEvents Click="function(s, e) { AddSelectedRows(); }" />
                </dx:ASPxButton>
            </div>
            <div>
                <dx:ASPxButton ID="btnMoveAllItemsToRight" runat="server" ClientInstanceName="btnMoveAllItemsToRight" CssClass="button"
                    AutoPostBack="False" Text="Add All >>" ToolTip="Add all items">
                    <ClientSideEvents Click="function(s, e) { AddAllRows(); }" />
                </dx:ASPxButton>
            </div>
            <div style="height: 32px">
            </div>
            <div>
                <dx:ASPxButton ID="btnMoveSelectedItemsToLeft" runat="server" ClientInstanceName="btnMoveSelectedItemsToLeft" CssClass="button"
                    AutoPostBack="False" Text="< Remove" ClientEnabled="False"
                    ToolTip="Remove selected items">
                    <ClientSideEvents Click="function(s, e) { RemoveSelectedRows(); }" />
                </dx:ASPxButton>
            </div>
            <div>
                <dx:ASPxButton ID="btnMoveAllItemsToLeft" runat="server" ClientInstanceName="btnMoveAllItemsToLeft" CssClass="button"
                    AutoPostBack="False" Text="<< Remove All" ClientEnabled="False"
                    ToolTip="Remove all items">
                    <ClientSideEvents Click="function(s, e) { RemoveAllRows(); }" />
                </dx:ASPxButton>
            </div>
            <div style="height: 32px">
            </div>
            <div>
                <dx:ASPxButton ID="btnRestore" runat="server" Text="Restore Database" OnClick="btnRestore_Click">
                </dx:ASPxButton>
            </div>
        </div>
        <div class="contentEditors">
            <dx:ASPxGridView ID="TargetGrid" runat="server" ClientInstanceName="targetGrid" AutoGenerateColumns="false" KeyFieldName="CategoryID"
                DataSourceID="TargetDS" OnCustomCallback="TargetGrid_CustomCallback">
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="Page" />
                    <dx:GridViewDataTextColumn FieldName="CategoryName" />
                    <dx:GridViewDataTextColumn FieldName="Description" />
                </Columns>
                <SettingsBehavior AllowSelectByRowClick="true" />
                <ClientSideEvents SelectionChanged="function(s, e) { UpdateButtonState(); }" />
            </dx:ASPxGridView>
            <asp:AccessDataSource ID="TargetDS" runat="server" DataFile="~/App_Data/nwindNew.mdb" 
                SelectCommand="SELECT [CategoryID], [CategoryName], [Description] FROM [CategoriesUpdated]">
            </asp:AccessDataSource>
        </div>
    </div>
    </form>
</body>
</html>
