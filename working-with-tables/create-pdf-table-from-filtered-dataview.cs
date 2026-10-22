using System;
using System.Data;

class Program
{
    static void Main()
    {
        // Create source DataTable and populate it
        DataTable sourceTable = new DataTable("Source");
        sourceTable.Columns.Add("Id", typeof(int));
        sourceTable.Columns.Add("Name", typeof(string));
        sourceTable.Columns.Add("IsActive", typeof(bool));

        sourceTable.Rows.Add(1, "Alice", true);
        sourceTable.Rows.Add(2, "Bob", false);
        sourceTable.Rows.Add(3, "Charlie", true);
        sourceTable.Rows.Add(4, "Diana", false);

        // Filter rows using a DataView
        DataView view = new DataView(sourceTable);
        view.RowFilter = "IsActive = true";

        // Create destination table with the same schema
        DataTable destTable = sourceTable.Clone();

        // Import only the filtered rows
        foreach (DataRowView rowView in view)
        {
            destTable.ImportRow(rowView.Row);
        }

        // Display the filtered records
        Console.WriteLine("Filtered rows:");
        foreach (DataRow row in destTable.Rows)
        {
            Console.WriteLine($"{row["Id"]}, {row["Name"]}, {row["IsActive"]}");
        }
    }
}