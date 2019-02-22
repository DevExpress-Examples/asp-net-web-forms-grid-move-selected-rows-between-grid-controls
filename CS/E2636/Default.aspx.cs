using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace E2636
{
    public partial class Default : System.Web.UI.Page
    {
        private string[] fieldNames = new string[] { "CategoryID", "CategoryName", "Description" };
        List<object> rowValues = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TargetGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            rowValues = new List<object>();
            
            switch (e.Parameters)
            {
                case "addSelectedRows":
                    rowValues = SourceGrid.GetSelectedFieldValues(fieldNames);
                    var categoryIDs = new StringBuilder();

                    for (int i = 0; i < rowValues.Count(); i++)
                    {
                        var categoryID = (rowValues[i] as object[])[0];
                        if (i < rowValues.Count() - 1)
                            categoryIDs.AppendFormat("{0}, ", categoryID);
                        else
                            categoryIDs.Append(categoryID);
                    }

                    if (categoryIDs.Length > 0)
                    {
                        SourceDS.DeleteCommand = string.Format("DELETE FROM [Categories] WHERE [CategoryID] IN ({0})", categoryIDs);
                        SourceDS.Delete();
                        
                        foreach (object[] rowValue in rowValues)
                        {
                            TargetDS.InsertCommand = string.Format(
                                "INSERT INTO [CategoriesUpdated] ([CategoryID], [CategoryName], [Description]) VALUES ({0}, '{1}', '{2}')", 
                                rowValue[0], rowValue[1], rowValue[2]);
                            TargetDS.Insert();
                        }
                        TargetGrid.DataBind();
                    }
                    break;
                case "addAllRows":
                    for (int i = 0; i < SourceGrid.VisibleRowCount; i++)
                        rowValues.Add(SourceGrid.GetRowValues(i, fieldNames));
                    if (rowValues.Count > 0)
                    {
                        SourceDS.DeleteCommand = "DELETE * FROM [Categories]";
                        SourceDS.Delete();

                        foreach (object[] rowValue in rowValues)
                        {
                            TargetDS.InsertCommand = string.Format(
                                "INSERT INTO [CategoriesUpdated] ([CategoryID], [CategoryName], [Description]) VALUES ({0}, '{1}', '{2}')", 
                                rowValue[0], rowValue[1], rowValue[2]);
                            TargetDS.Insert();
                        }
                        TargetGrid.DataBind();
                    }
                    break;
            }
        }

        protected void SourceGrid_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            rowValues = new List<object>();
            switch (e.Parameters)
            {
                case "removeSelectedRows":
                    rowValues = TargetGrid.GetSelectedFieldValues(fieldNames);
                    var categoryIDs = new StringBuilder();

                    for (int i = 0; i < rowValues.Count(); i++)
                    {
                        var categoryID = (rowValues[i] as object[])[0];
                        if (i < rowValues.Count() - 1)
                            categoryIDs.AppendFormat("{0}, ", categoryID);
                        else
                            categoryIDs.Append(categoryID);
                    }

                    if (categoryIDs.Length > 0)
                    {
                        TargetDS.DeleteCommand = string.Format("DELETE FROM [CategoriesUpdated] WHERE [CategoryID] IN ({0})", categoryIDs);
                        TargetDS.Delete();

                        foreach (object[] rowValue in rowValues)
                        {
                            SourceDS.InsertCommand = string.Format(
                                "INSERT INTO [Categories] ([CategoryID], [CategoryName], [Description]) VALUES ({0}, '{1}', '{2}')", 
                                rowValue[0], rowValue[1], rowValue[2]);
                            SourceDS.Insert();
                        }
                        SourceGrid.DataBind();
                    }
                    break;
                case "removeAllRows":
                    for (int i = 0; i < TargetGrid.VisibleRowCount; i++)
                        rowValues.Add(TargetGrid.GetRowValues(i, fieldNames));
                    if (rowValues.Count > 0)
                    {
                        TargetDS.DeleteCommand = "DELETE * FROM [CategoriesUpdated]";
                        TargetDS.Delete();

                        foreach (object[] rowValue in rowValues)
                        {
                            SourceDS.InsertCommand = string.Format(
                                "INSERT INTO [Categories] ([CategoryID], [CategoryName], [Description]) VALUES ({0}, '{1}', '{2}')", 
                                rowValue[0], rowValue[1], rowValue[2]);
                            SourceDS.Insert();
                        }
                        SourceGrid.DataBind();
                    }
                    break;
            }
        }

        protected void btnRestore_Click(object sender, EventArgs e)
        {
            File.Copy(
            Server.MapPath("~/App_Data/nwindNew_backup.mdb"),
            Server.MapPath("~/App_Data/nwindNew.mdb"),
            true);

            SourceGrid.DataBind();
            SourceGrid.Selection.UnselectAll();

            TargetGrid.DataBind();
            TargetGrid.Selection.UnselectAll();
        }
    }
}