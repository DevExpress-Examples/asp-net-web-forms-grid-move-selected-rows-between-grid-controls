using System;
using System.IO;
using System.Collections.Generic;
using DevExpress.Web.ASPxGridView;

public partial class _Default : System.Web.UI.Page {
    protected void btnMove_Click(object sender, EventArgs e) {
        try {
            string[] fieldNames = new string[] { "CategoryID", "CategoryName" };
            List<object> columnValues = gridView.GetSelectedFieldValues(fieldNames);
            System.Text.StringBuilder categoryIDs = new System.Text.StringBuilder();
            System.Text.StringBuilder categoryNames = new System.Text.StringBuilder();
            foreach (object[] categoryID in columnValues) {
                categoryIDs.AppendFormat("{0}, ", categoryID[0]);
                categoryNames.AppendFormat("{0}, ", categoryID[1]);
            }
            if (categoryIDs.Length > 0) {
                categoryIDs.Remove(categoryIDs.Length - 2, 2);

                if (categoryNames.Length > 0)
                    categoryNames.Remove(categoryNames.Length - 2, 2);

                ds.DeleteCommand = string.Format("DELETE FROM [Categories] WHERE [CategoryID] IN ({0})", categoryIDs);
                int updatedRowsCount = ds.Delete();

                string insertCommandFormat = "INSERT INTO [CategoriesUpdated] ([CategoryID], [Description]) VALUES ({0}, 'UPDATED')";

                int totalByColumnCategoryID = 0;
                int insertedRowsCount = 0;

                foreach (object[] categoryID in columnValues) {
                    totalByColumnCategoryID += Convert.ToInt32(categoryID[0]);
                    dsUpdated.InsertCommand = string.Format(insertCommandFormat, categoryID[0]);
                    insertedRowsCount += dsUpdated.Insert();
                }

                lblUpdatedRowsCount.Text = updatedRowsCount.ToString();
                lblTotalByColumnCategoryID.Text = totalByColumnCategoryID.ToString();
                lblInsertedRowsCount.Text = insertedRowsCount.ToString();
                lblTotalByCategoryName.Text = categoryNames.ToString();

                gridViewUpdated.DataBind();
            } else
                RestoreLabels();
        } catch (System.Data.OleDb.OleDbException ex) {
            ClientScript.RegisterStartupScript(GetType(), "OleDbException", string.Format("alert('{0}')", ex.Message), true);
        }
        gridView.Selection.UnselectAll();
    }
    protected void btnRestore_Click(object sender, EventArgs e) {
        RestoreDatabase(gridView, gridViewUpdated);
    }
    private void RestoreDatabase(ASPxGridView grid1, ASPxGridView grid2) {
        File.Copy(
            Server.MapPath("~/App_Data/nwindNew_backup.mdb"),
            Server.MapPath("~/App_Data/nwindNew.mdb"),
            true);
        grid1.DataBind();
        grid1.Selection.UnselectAll();

        grid2.DataBind();
        grid2.Selection.UnselectAll();

        RestoreLabels();
    }
    private void RestoreLabels() {
        lblUpdatedRowsCount.Text = string.Empty;
        lblTotalByColumnCategoryID.Text = string.Empty;
        lblInsertedRowsCount.Text = string.Empty;
        lblTotalByCategoryName.Text = string.Empty;
    }
}