using System;
using System.IO;
using System.Runtime.InteropServices;
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

            // Create a table and set its width to 80% of the page width
            Table table = new Table
            {
                // Use ColumnWidths with a percentage value to define the table width
                ColumnWidths = "80%",
                // Center the table on the page for better appearance
                Alignment = HorizontalAlignment.Center
            };

            // Add a row with a single cell
            Row row = table.Rows.Add();
            row.Cells.Add("Sample cell content");

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF to a file – guard the call on non‑Windows platforms where libgdiplus may be missing
            string outputPath = "TableWidth80Percent.pdf";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Skipping Document.Save on non‑Windows platform because libgdiplus (GDI+) is required.");
            }
        }
    }
}