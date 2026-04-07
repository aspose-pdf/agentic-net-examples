using System;
using System.IO;
using Aspose.Pdf;               // Core PDF classes
using Aspose.Pdf.Text;          // For text handling (optional)

class Program
{
    static void Main()
    {
        // Input image file to be placed inside the table cell
        const string imagePath = "image.jpg";

        // Output PDF file
        const string outputPdf = "TableWithImage.pdf";

        // Verify that the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // Build a simple table with two columns (you can adjust as needed)
            // ------------------------------------------------------------
            Table table = new Table
            {
                // Define column widths (e.g., first column 200 points, second column 200 points)
                ColumnWidths = "200 200"
            };

            // Add a row to the table
            Row row = table.Rows.Add();

            // ------------------------------------------------------------
            // First cell: add some text (optional, just to illustrate)
            // ------------------------------------------------------------
            Cell textCell = row.Cells.Add();
            TextFragment tf = new TextFragment("Sample text in first cell");
            textCell.Paragraphs.Add(tf);

            // ------------------------------------------------------------
            // Second cell: insert the external image
            // ------------------------------------------------------------
            Cell imageCell = row.Cells.Add();

            // Create an Image object and point it to the external file
            Image img = new Image
            {
                File = imagePath
                // Optional: set explicit dimensions
                // FixWidth = 150,
                // FixHeight = 100
            };

            // Add the image to the cell's paragraph collection
            imageCell.Paragraphs.Add(img);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with image in table cell saved to '{outputPdf}'.");
    }
}