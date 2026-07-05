using System;
using Aspose.Pdf;
using Aspose.Pdf.Tagged;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and add it to the page
            Table table = new Table
            {
                // Define column widths (two columns in this example)
                ColumnWidths = "150 300"
            };
            page.Paragraphs.Add(table);

            // Add a row with content that may require height adjustment
            Row row = table.Rows.Add();
            // First cell – short text
            row.Cells.Add("ID");
            // Second cell – long text that should wrap and cause the row to grow
            row.Cells.Add("This is a very long description that should automatically increase the row height to fit all the wrapped text without truncation.");

            // -----------------------------------------------------------------
            // Enable automatic height adjustment for the row.
            // In Aspose.Pdf the Row class does not expose a Height property.
            // Leaving FixedRowHeight unset (or explicitly setting it to 0) tells the renderer
            // to calculate the height from the intrinsic content, which results in an
            // auto‑fitting row.
            // -----------------------------------------------------------------
            row.FixedRowHeight = 0; // 0 = AutoFit

            // Save the PDF
            doc.Save("AutoFitRow.pdf");
        }

        Console.WriteLine("PDF with auto‑fitting row saved as 'AutoFitRow.pdf'.");
    }
}
