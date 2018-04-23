Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Collections.Generic
Imports DevExpress.Web.ASPxGridView

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Protected Sub btnMove_Click(ByVal sender As Object, ByVal e As EventArgs)
		Try
			Dim fieldNames() As String = { "CategoryID", "CategoryName" }
			Dim columnValues As List(Of Object) = gridView.GetSelectedFieldValues(fieldNames)
			Dim categoryIDs As New System.Text.StringBuilder()
			Dim categoryNames As New System.Text.StringBuilder()
			For Each categoryID As Object() In columnValues
				categoryIDs.AppendFormat("{0}, ", categoryID(0))
				categoryNames.AppendFormat("{0}, ", categoryID(1))
			Next categoryID
			If categoryIDs.Length > 0 Then
				categoryIDs.Remove(categoryIDs.Length - 2, 2)

				If categoryNames.Length > 0 Then
					categoryNames.Remove(categoryNames.Length - 2, 2)
				End If

				ds.DeleteCommand = String.Format("DELETE FROM [Categories] WHERE [CategoryID] IN ({0})", categoryIDs)
				Dim updatedRowsCount As Integer = ds.Delete()

				Dim insertCommandFormat As String = "INSERT INTO [CategoriesUpdated] ([CategoryID], [Description]) VALUES ({0}, 'UPDATED')"

				Dim totalByColumnCategoryID As Integer = 0
				Dim insertedRowsCount As Integer = 0

				For Each categoryID As Object() In columnValues
					totalByColumnCategoryID += Convert.ToInt32(categoryID(0))
					dsUpdated.InsertCommand = String.Format(insertCommandFormat, categoryID(0))
					insertedRowsCount += dsUpdated.Insert()
				Next categoryID

				lblUpdatedRowsCount.Text = updatedRowsCount.ToString()
				lblTotalByColumnCategoryID.Text = totalByColumnCategoryID.ToString()
				lblInsertedRowsCount.Text = insertedRowsCount.ToString()
				lblTotalByCategoryName.Text = categoryNames.ToString()

				gridViewUpdated.DataBind()
			Else
				RestoreLabels()
			End If
		Catch ex As System.Data.OleDb.OleDbException
			ClientScript.RegisterStartupScript(Me.GetType(), "OleDbException", String.Format("alert('{0}')", ex.Message), True)
		End Try
		gridView.Selection.UnselectAll()
	End Sub
	Protected Sub btnRestore_Click(ByVal sender As Object, ByVal e As EventArgs)
		RestoreDatabase(gridView, gridViewUpdated)
	End Sub
	Private Sub RestoreDatabase(ByVal grid1 As ASPxGridView, ByVal grid2 As ASPxGridView)
		File.Copy(Server.MapPath("~/App_Data/nwindNew_backup.mdb"), Server.MapPath("~/App_Data/nwindNew.mdb"), True)
		grid1.DataBind()
		grid1.Selection.UnselectAll()

		grid2.DataBind()
		grid2.Selection.UnselectAll()

		RestoreLabels()
	End Sub
	Private Sub RestoreLabels()
		lblUpdatedRowsCount.Text = String.Empty
		lblTotalByColumnCategoryID.Text = String.Empty
		lblInsertedRowsCount.Text = String.Empty
		lblTotalByCategoryName.Text = String.Empty
	End Sub
End Class