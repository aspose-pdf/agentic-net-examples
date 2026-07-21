using System;
using System.Data;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "MultiPageTable.pdf";

        // Create a new empty PDF document
        using (Document doc = new Document())
        {
            // Add a single page – the table will automatically create additional pages as needed
            Page page = doc.Pages.Add();

            // Create a table and enable breaking across pages
            Table table = new Table
            {
                IsBroken = true,               // allow the table to split over multiple pages
                RepeatingRowsCount = 1,        // repeat the first row (header) on each new page
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f) // optional visual styling
            };

            // Define column widths (optional, adjust as needed)
            table.ColumnWidths = "50 100 100 100 100";

            // Build a DataTable with many rows to force pagination
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Category", typeof(string));
            dt.Columns.Add("Price", typeof(decimal));
            dt.Columns.Add("Quantity", typeof(int));

            // Populate with sample data (e.g., 200 rows)
            for (int i = 1; i <= 200; i++)
            {
                dt.Rows.Add(i, $"Item {i}", $"Category {((i - 1) % 5) + 1}", Math.Round(10.0 + i * 0.5, 2), i % 10 + 1);
            }

            // Import the DataTable into the Aspose.Pdf.Table
            // First row will be column names (header)
            table.ImportDataTable(dt, true, 0, 0);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multi‑page table saved to '{outputPath}'.");
    }
}