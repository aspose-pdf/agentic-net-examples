using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextFragment if needed

class Program
{
    static void Main()
    {
        const string outputPath = "table_page.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new blank page to the document
            Page page = doc.Pages.Add();

            // Create a table with three columns
            Table table = new Table
            {
                // Define column widths (in points)
                ColumnWidths = "100 150 200",
                // Add a thin border around the table
                Border = new BorderInfo(BorderSide.All, 0.5f)
            };

            // ----- Header row -----
            Row header = table.Rows.Add();
            // Set background color for the header (use Aspose.Pdf.Color to avoid System.Drawing)
            header.BackgroundColor = Aspose.Pdf.Color.LightGray;
            header.Cells.Add("Product");
            header.Cells.Add("Quantity");
            header.Cells.Add("Price");

            // ----- First data row -----
            Row row1 = table.Rows.Add();
            row1.Cells.Add("Widget A");
            row1.Cells.Add("10");
            row1.Cells.Add("$5.00");

            // ----- Second data row -----
            Row row2 = table.Rows.Add();
            row2.Cells.Add("Widget B");
            row2.Cells.Add("7");
            row2.Cells.Add("$7.50");

            // Add the table to the page's paragraph collection so it gets rendered
            page.Paragraphs.Add(table);

            // Save the PDF document (PDF format by default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with table saved to '{outputPath}'.");
    }
}