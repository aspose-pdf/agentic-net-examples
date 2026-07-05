using System;
using System.Data;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Drawing; // for BorderInfo

class Program
{
    static void Main()
    {
        // ---------- 1. Create an in‑memory DataTable with sample data ----------
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("Id", typeof(int));
        dataTable.Columns.Add("Name", typeof(string));
        dataTable.Columns.Add("Quantity", typeof(int));

        dataTable.Rows.Add(1, "Item A", 10);
        dataTable.Rows.Add(2, "Item B", 20);
        dataTable.Rows.Add(3, "Item C", 30);

        // ---------- 2. Create a PDF document and add a table ----------
        using (Document pdfDocument = new Document())
        {
            // Add a new page to the document
            Page page = pdfDocument.Pages.Add();

            // Create a Table instance
            Table table = new Table
            {
                // Adjust columns to fit the content (correct enum value)
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent,
                // Set a default border for all cells
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f, Aspose.Pdf.Color.Black)
            };

            // Initialise ColumnWidths – required by ImportDataTable to avoid NullReferenceException
            // ColumnWidths is a string containing space‑separated width values (in points).
            // Here we give each column a default width of 100 points.
            table.ColumnWidths = string.Join(" ", Enumerable.Repeat("100", dataTable.Columns.Count));

            // Import the DataTable into the Aspose.Pdf.Table
            // Parameters: (DataTable, isColumnNamesImported, firstFilledRow, firstFilledColumn)
            table.ImportDataTable(dataTable, true, 0, 0);

            // Add the populated table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document
            pdfDocument.Save("output.pdf");
        }

        Console.WriteLine("PDF with imported table has been created successfully.");
    }
}
