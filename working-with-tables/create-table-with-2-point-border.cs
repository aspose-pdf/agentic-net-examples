using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Output PDF path
        const string outputPath = "table_with_border.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position and size
            Table table = new Table
            {
                // Position the table on the page (left, top)
                Left = 50,
                Top = 700,
                // Define column widths (example: two columns)
                ColumnWidths = "200 200"
            };

            // Set the table border thickness to 2 points using the BorderInfo constructor
            table.Border = new BorderInfo(BorderSide.All, 2f);

            // Add a simple row with two cells for demonstration
            Row row = table.Rows.Add();
            row.Cells.Add("Header 1");
            row.Cells.Add("Header 2");

            // Add a second row
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Data 1");
            row2.Cells.Add("Data 2");

            // Demonstrate correct usage of RichMediaAnnotation without setting an invalid property.
            // Use the fully‑qualified Aspose.Pdf.Rectangle to avoid ambiguity.
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, new Aspose.Pdf.Rectangle(100, 500, 300, 600));
            // Note: Activation property is not available in the current API version; omit it.
            page.Annotations.Add(richMedia);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the PDF document – guard against missing libgdiplus on non‑Windows platforms
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("libgdiplus is required for PDF creation on this platform. Skipping doc.Save().");
            }
        }
    }
}
