using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF (can be empty or existing) and output PDF paths
        const string outputPdfPath = "output_with_image_table.pdf";
        // Path to the external image to insert into the table cell
        const string imagePath = "image.jpg";

        // Verify that the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document (pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a table with a single column
            Table table = new Table();
            // Optionally set column widths (percentage of page width)
            table.ColumnWidths = "100%";

            // Add a row to the table
            Row row = table.Rows.Add();

            // Add a cell to the row
            Cell cell = row.Cells.Add();

            // Create an Image object and point it to the external file
            Image img = new Image
            {
                File = imagePath
            };

            // Insert the image into the cell's paragraph collection
            cell.Paragraphs.Add(img);

            // Add the table to the page's paragraph collection
            page.Paragraphs.Add(table);

            // Save the resulting PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with image table saved to '{outputPdfPath}'.");
    }
}