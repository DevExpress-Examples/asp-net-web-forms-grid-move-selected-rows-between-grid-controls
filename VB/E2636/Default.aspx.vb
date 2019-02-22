Imports DevExpress.Web
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace E2636
	Partial Public Class [Default]
		Inherits System.Web.UI.Page

		Private fieldNames() As String = { "CategoryID", "CategoryName", "Description" }
		Private rowValues As List(Of Object) = Nothing
		Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

		End Sub

		Protected Sub TargetGrid_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs)
			rowValues = New List(Of Object)()

			Select Case e.Parameters
				Case "addSelectedRows"
					rowValues = SourceGrid.GetSelectedFieldValues(fieldNames)
					Dim categoryIDs = New StringBuilder()

					For i As Integer = 0 To rowValues.Count() - 1
						Dim categoryID = (TryCast(rowValues(i), Object()))(0)
						If i < rowValues.Count() - 1 Then
							categoryIDs.AppendFormat("{0}, ", categoryID)
						Else
							categoryIDs.Append(categoryID)
						End If
					Next i

					If categoryIDs.Length > 0 Then
						SourceDS.DeleteCommand = String.Format("DELETE FROM [Categories] WHERE [CategoryID] IN ({0})", categoryIDs)
						SourceDS.Delete()

						For Each rowValue As Object() In rowValues
							TargetDS.InsertCommand = String.Format("INSERT INTO [CategoriesUpdated] ([CategoryID], [CategoryName], [Description]) VALUES ({0}, '{1}', '{2}')", rowValue(0), rowValue(1), rowValue(2))
							TargetDS.Insert()
						Next rowValue
						TargetGrid.DataBind()
					End If
				Case "addAllRows"
					For i As Integer = 0 To SourceGrid.VisibleRowCount - 1
						rowValues.Add(SourceGrid.GetRowValues(i, fieldNames))
					Next i
					If rowValues.Count > 0 Then
						SourceDS.DeleteCommand = "DELETE * FROM [Categories]"
						SourceDS.Delete()

						For Each rowValue As Object() In rowValues
							TargetDS.InsertCommand = String.Format("INSERT INTO [CategoriesUpdated] ([CategoryID], [CategoryName], [Description]) VALUES ({0}, '{1}', '{2}')", rowValue(0), rowValue(1), rowValue(2))
							TargetDS.Insert()
						Next rowValue
						TargetGrid.DataBind()
					End If
			End Select
		End Sub

		Protected Sub SourceGrid_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs)
			rowValues = New List(Of Object)()
			Select Case e.Parameters
				Case "removeSelectedRows"
					rowValues = TargetGrid.GetSelectedFieldValues(fieldNames)
					Dim categoryIDs = New StringBuilder()

					For i As Integer = 0 To rowValues.Count() - 1
						Dim categoryID = (TryCast(rowValues(i), Object()))(0)
						If i < rowValues.Count() - 1 Then
							categoryIDs.AppendFormat("{0}, ", categoryID)
						Else
							categoryIDs.Append(categoryID)
						End If
					Next i

					If categoryIDs.Length > 0 Then
						TargetDS.DeleteCommand = String.Format("DELETE FROM [CategoriesUpdated] WHERE [CategoryID] IN ({0})", categoryIDs)
						TargetDS.Delete()

						For Each rowValue As Object() In rowValues
							SourceDS.InsertCommand = String.Format("INSERT INTO [Categories] ([CategoryID], [CategoryName], [Description]) VALUES ({0}, '{1}', '{2}')", rowValue(0), rowValue(1), rowValue(2))
							SourceDS.Insert()
						Next rowValue
						SourceGrid.DataBind()
					End If
				Case "removeAllRows"
					For i As Integer = 0 To TargetGrid.VisibleRowCount - 1
						rowValues.Add(TargetGrid.GetRowValues(i, fieldNames))
					Next i
					If rowValues.Count > 0 Then
						TargetDS.DeleteCommand = "DELETE * FROM [CategoriesUpdated]"
						TargetDS.Delete()

						For Each rowValue As Object() In rowValues
							SourceDS.InsertCommand = String.Format("INSERT INTO [Categories] ([CategoryID], [CategoryName], [Description]) VALUES ({0}, '{1}', '{2}')", rowValue(0), rowValue(1), rowValue(2))
							SourceDS.Insert()
						Next rowValue
						SourceGrid.DataBind()
					End If
			End Select
		End Sub

		Protected Sub btnRestore_Click(ByVal sender As Object, ByVal e As EventArgs)
			File.Copy(Server.MapPath("~/App_Data/nwindNew_backup.mdb"), Server.MapPath("~/App_Data/nwindNew.mdb"), True)

			SourceGrid.DataBind()
			SourceGrid.Selection.UnselectAll()

			TargetGrid.DataBind()
			TargetGrid.Selection.UnselectAll()
		End Sub
	End Class
End Namespace