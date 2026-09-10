using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "multi_page_table.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a page to the document (first page is index 1)
            Page page = doc.Pages.Add();

            // Create a table that can break across pages automatically
            Table table = new Table
            {
                // Allow the table to be split when it exceeds page height
                IsBroken = true,
                // Repeat the first row (header) on each new page
                RepeatingRowsCount = 1,
                // Let Aspose.Pdf adjust column widths automatically
                ColumnAdjustment = ColumnAdjustment.AutoFitToContent,
                // Explicit column widths prevent a NullReferenceException in some versions
                // when AutoFitToContent tries to calculate widths before any data is present.
                // The widths are placeholders; they will be overridden by the auto‑fit logic.
                ColumnWidths = "100 100 100 100 100"
            };

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Build a DataTable with many rows to force pagination
            DataTable dt = new DataTable();

            // Define five columns
            for (int c = 0; c < 5; c++)
            {
                dt.Columns.Add($"Column {c + 1}", typeof(string));
            }

            // Populate 200 rows of sample data
            for (int r = 0; r < 200; r++)
            {
                DataRow row = dt.NewRow();
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    row[c] = $"R{r + 1}C{c + 1}";
                }
                dt.Rows.Add(row);
            }

            // Import the DataTable into the Aspose.Pdf Table.
            // 'true' imports column names as the first row (header).
            // Start importing at row 0, column 0 of the table.
            table.ImportDataTable(dt, true, 0, 0);

            // Save the PDF document to the specified file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
