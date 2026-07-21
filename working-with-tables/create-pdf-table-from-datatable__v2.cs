using System;
using System.Data;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "template.pdf";   // will be created on‑the‑fly if missing
        const string outputPdf = "output.pdf";     // result PDF

        // ------------------------------------------------------------
        // Ensure the input template exists – create a minimal PDF inline
        // ------------------------------------------------------------
        if (!System.IO.File.Exists(inputPdf))
        {
            using (Document seed = new Document())
            {
                // Add a blank page (you could add a header/footer here if desired)
                seed.Pages.Add();
                seed.Save(inputPdf);
            }
        }

        // ------------------------------------------------------------
        // Prepare source data (could be read from DB, CSV, etc.)
        // ------------------------------------------------------------
        DataTable sourceTable = GetSourceData();

        // ------------------------------------------------------------
        // Load the PDF document (wrapped in using for deterministic disposal)
        // ------------------------------------------------------------
        using (Document doc = new Document(inputPdf))
        {
            // --------------------------------------------------------
            // Create a Table instance and configure basic appearance
            // --------------------------------------------------------
            Table table = new Table
            {
                // Auto‑fit columns to content
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent,

                // Simple border for all cells
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),

                // Padding inside each cell
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // --------------------------------------------------------
            // Define column widths (equal widths in this example)
            // --------------------------------------------------------
            int columnCount = sourceTable.Columns.Count;
            // Table.ColumnWidths expects a space‑separated string, e.g. "100 100 100"
            string widths = string.Join(" ", Enumerable.Repeat("100", columnCount));
            table.ColumnWidths = widths;

            // --------------------------------------------------------
            // Import the DataTable into the Aspose.Pdf.Table
            // - true  => import column names as the first row
            // - start at row 0, column 0 (zero‑based indices)
            // --------------------------------------------------------
            table.ImportDataTable(sourceTable, true, 0, 0);

            // --------------------------------------------------------
            // Position the table on the first page
            // --------------------------------------------------------
            Page page = doc.Pages[1];          // 1‑based page indexing
            table.Top  = 100;                  // distance from bottom of page
            table.Left = 50;                   // distance from left edge
            page.Paragraphs.Add(table);        // add the table to the page

            // --------------------------------------------------------
            // Save the modified PDF (PDF output by default)
            // --------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Added table with {sourceTable.Rows.Count} data rows to '{outputPdf}'.");
    }

    // --------------------------------------------------------------------
    // Example method that builds a DataTable with a dynamic number of rows
    // --------------------------------------------------------------------
    static DataTable GetSourceData()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ID",       typeof(int));
        dt.Columns.Add("Product",  typeof(string));
        dt.Columns.Add("Quantity", typeof(int));

        // Simulate dynamic record count (e.g., from a database query)
        for (int i = 1; i <= 15; i++)
        {
            dt.Rows.Add(i, $"Item {i}", i * 10);
        }

        return dt;
    }
}
