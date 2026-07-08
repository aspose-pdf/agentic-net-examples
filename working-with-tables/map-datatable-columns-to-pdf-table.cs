using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Sample DataTable with three columns
        DataTable sourceTable = new DataTable();
        sourceTable.Columns.Add("ID", typeof(int));
        sourceTable.Columns.Add("Name", typeof(string));
        sourceTable.Columns.Add("Amount", typeof(double));

        sourceTable.Rows.Add(1, "Alice", 123.45);
        sourceTable.Rows.Add(2, "Bob",   678.90);
        sourceTable.Rows.Add(3, "Carol", 234.56);

        // Mapping: source column index -> target table column index
        // Example: ID (0) -> column 2, Name (1) -> column 0, Amount (2) -> column 1
        var columnMapping = new Dictionary<int, int>
        {
            { 0, 2 },
            { 1, 0 },
            { 2, 1 }
        };

        // Determine number of target columns (max mapped index + 1)
        int targetColumnCount = columnMapping.Values.Max() + 1;

        // Build a reordered DataTable that matches the target column order
        DataTable reorderedTable = new DataTable();

        // Create columns in target order
        for (int targetIdx = 0; targetIdx < targetColumnCount; targetIdx++)
        {
            // Find source column that maps to this target index
            int sourceIdx = columnMapping.FirstOrDefault(kv => kv.Value == targetIdx).Key;

            // If a mapping exists, copy the column definition and data
            if (columnMapping.ContainsKey(sourceIdx))
            {
                // Preserve original column name for readability
                string colName = sourceTable.Columns[sourceIdx].ColumnName;
                reorderedTable.Columns.Add(colName, sourceTable.Columns[sourceIdx].DataType);
            }
            else
            {
                // No source column mapped to this target position – add an empty placeholder column
                reorderedTable.Columns.Add($"Placeholder_{targetIdx}", typeof(string));
            }
        }

        // Populate rows according to the mapping
        foreach (DataRow srcRow in sourceTable.Rows)
        {
            DataRow newRow = reorderedTable.NewRow();
            for (int targetIdx = 0; targetIdx < targetColumnCount; targetIdx++)
            {
                int sourceIdx = columnMapping.FirstOrDefault(kv => kv.Value == targetIdx).Key;
                if (columnMapping.ContainsKey(sourceIdx))
                {
                    newRow[targetIdx] = srcRow[sourceIdx];
                }
                else
                {
                    newRow[targetIdx] = DBNull.Value;
                }
            }
            reorderedTable.Rows.Add(newRow);
        }

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Create a table with the required number of columns
            Table table = new Table();

            // Define column widths (equal width for simplicity)
            // Table.ColumnWidths is a string; set it to a space‑separated list of widths
            table.ColumnWidths = string.Join(" ", Enumerable.Repeat("100", targetColumnCount));

            // Import the reordered DataTable into the Aspose.Pdf.Table
            // Parameters: (DataTable, isColumnNamesImported, firstFilledRow, firstFilledColumn)
            table.ImportDataTable(reorderedTable, true, 0, 0);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save("MappedTable.pdf");
        }

        Console.WriteLine("PDF with mapped table saved as 'MappedTable.pdf'.");
    }
}
