using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Prepare a dynamic data collection (DataTable) – rows can be added at runtime
            DataTable data = new DataTable();

            // Define columns
            data.Columns.Add("ID", typeof(int));
            data.Columns.Add("Name", typeof(string));
            data.Columns.Add("Quantity", typeof(int));

            // Populate the table with sample data (could be any runtime collection)
            for (int i = 1; i <= 15; i++) // Example: 15 rows; adjust as needed
            {
                data.Rows.Add(i, $"Item {i}", i * 10);
            }

            // Create an Aspose.Pdf Table
            Table table = new Table
            {
                // Optional: set table appearance
                Border = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5),
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Gray)
            };

            // Define column widths before importing data – required to avoid NullReferenceException
            // Table.ColumnWidths is a string that contains space‑separated width values.
            // Build a string with a width of 100 points for each column.
            var widthValues = Enumerable.Repeat("100", data.Columns.Count);
            table.ColumnWidths = string.Join(" ", widthValues);

            // Import the DataTable into the PDF table.
            // Parameters:
            //   data               – source DataTable
            //   true               – import column names as the first row
            //   0                  – start importing at the first row of the PDF table (zero‑based)
            //   0                  – start importing at the first column of the PDF table (zero‑based)
            table.ImportDataTable(data, true, 0, 0);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save("DynamicTable.pdf");
        }

        Console.WriteLine("PDF with dynamic table created: DynamicTable.pdf");
    }
}
