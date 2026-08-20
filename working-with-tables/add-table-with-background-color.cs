using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and set its position and size
            Table table = new Table
            {
                // Position the table on the page
                Left = 50,
                Top = 500,
                // Define column widths (example: two columns)
                ColumnWidths = "200 200"
            };

            // ------------------------------------------------------------
            // NOTE:
            // The Table.BackgroundColor property accepts only a solid
            // Aspose.Pdf.Color value. Aspose.Pdf does not provide a
            // LinearGradientBrush or similar API that can be assigned
            // directly to this property. Therefore a true gradient background
            // cannot be applied to a Table via the BackgroundColor property.
            // ------------------------------------------------------------

            // Set a solid background color as a fallback
            table.BackgroundColor = Aspose.Pdf.Color.LightGray;

            // Add a header row
            Row header = table.Rows.Add();
            header.BackgroundColor = Aspose.Pdf.Color.Gray;
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");

            // Add a data row
            Row row = table.Rows.Add();
            row.Cells.Add("Cell 1");
            row.Cells.Add("Cell 2");

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // Save the PDF
            string outputPath = "TableWithBackground.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'.");
        }
    }
}