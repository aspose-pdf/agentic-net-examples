using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output paths (adjust as needed)
        const string outputPath = "table_width_output.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Create a table and define its column widths (optional)
            Table table = new Table
            {
                // Example: three columns with relative widths
                ColumnWidths = "100 150 200"
            };

            // Add a header row
            Row header = table.Rows.Add();
            header.Cells.Add("Header 1");
            header.Cells.Add("Header 2");
            header.Cells.Add("Header 3");

            // Add a few data rows
            for (int i = 1; i <= 5; i++)
            {
                Row row = table.Rows.Add();
                row.Cells.Add($"Row {i} - Col 1");
                row.Cells.Add($"Row {i} - Col 2");
                row.Cells.Add($"Row {i} - Col 3");
            }

            // Add the table to the page's paragraphs collection
            page.Paragraphs.Add(table);

            // At this point the table has been laid out.
            // Get the calculated width of the rendered table.
            double renderedWidth = table.GetWidth();

            // Output the width to the console
            Console.WriteLine($"Rendered table width: {renderedWidth}");

            // Optionally, add a text fragment showing the width on the page
            TextFragment tf = new TextFragment($"Table width = {renderedWidth:F2} points")
            {
                // Position the fragment below the table
                Position = new Position(50, page.PageInfo.Height - 50)
            };
            page.Paragraphs.Add(tf);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}