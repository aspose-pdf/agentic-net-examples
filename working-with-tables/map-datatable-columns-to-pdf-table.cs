using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Prepare a sample DataTable
        DataTable dt = new DataTable();
        dt.Columns.Add("Id", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Quantity", typeof(int));
        dt.Columns.Add("Price", typeof(decimal));

        dt.Rows.Add(1, "Apple", 10, 0.5m);
        dt.Rows.Add(2, "Banana", 20, 0.3m);
        dt.Rows.Add(3, "Cherry", 15, 0.8m);

        // Mapping: DataTable column name -> target Table column index (zero‑based)
        // Example: place "Name" in column 0, "Quantity" in column 2, "Price" in column 4
        var columnMapping = new Dictionary<string, int>
        {
            { "Name", 0 },
            { "Quantity", 2 },
            { "Price", 4 }
        };

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Create a table with enough columns (max target index + 1)
            int totalColumns = columnMapping.Values.Max() + 1;
            Table table = new Table
            {
                // Optional: set column widths (equal width for simplicity)
                ColumnWidths = string.Join(" ", Enumerable.Repeat("50", totalColumns))
            };

            // Build source column list based on the mapping keys
            int[] sourceColumnList = columnMapping.Keys
                .Select(colName => dt.Columns[colName].Ordinal)
                .ToArray();

            // Build source row list (all rows)
            int[] sourceRowList = Enumerable.Range(0, dt.Rows.Count).ToArray();

            // Determine the first column where import will start
            int firstFilledColumn = columnMapping.Values.Min();

            // Import the selected columns into the table.
            // showColumnNamesAsFirstRow = true (adds column headers)
            // isHtmlSupported = false (plain text)
            table.ImportDataTable(
                dt,
                sourceRowList,
                sourceColumnList,
                firstFilledRow: 0,
                firstFilledColumn: firstFilledColumn,
                showColumnNamesAsFirstRow: true,
                isHtmlSupported: false);

            // Add the table to the first page
            Page page = doc.Pages.Add();
            page.Paragraphs.Add(table);

            // Save the PDF
            doc.Save("MappedTable.pdf");
        }

        Console.WriteLine("PDF with mapped table saved as 'MappedTable.pdf'.");
    }
}