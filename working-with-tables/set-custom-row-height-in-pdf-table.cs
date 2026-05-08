using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "table_row_height.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table with three columns
            Table table = new Table
            {
                ColumnWidths = "100 100 100", // widths in points
                DefaultCellBorder = new BorderInfo(BorderSide.All, 0.5f),
                DefaultCellPadding = new MarginInfo(5, 5, 5, 5)
            };

            // First row – default height
            Row row0 = table.Rows.Add();
            row0.Cells.Add("Cell 1");
            row0.Cells.Add("Cell 2");
            row0.Cells.Add("Cell 3");

            // Second row – custom fixed height (e.g., 50 points)
            Row row1 = table.Rows.Add();
            row1.FixedRowHeight = 50; // set custom row height
            row1.Cells.Add("Tall Cell 1");
            row1.Cells.Add("Tall Cell 2");
            row1.Cells.Add("Tall Cell 3");

            // Third row – default height
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Cell 4");
            row2.Cells.Add("Cell 5");
            row2.Cells.Add("Cell 6");

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the document (guarded for non‑Windows platforms where GDI+ may be missing)
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Skipping save on non‑Windows platform (GDI+ not available).");
            }
        }
    }
}