using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for TextFragment if needed

class Program
{
    static void Main()
    {
        // Input image file to be placed inside the table cell
        const string imagePath = "image.jpg";

        // Output PDF file
        const string outputPdf = "table_with_image.pdf";

        // Verify the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // Add a blank page to host the table
            Page page = pdfDoc.Pages.Add();

            // ------------------------------------------------------------
            // Create a table with two columns (adjust widths as needed)
            // ------------------------------------------------------------
            Table table = new Table
            {
                // Define column widths (in points). Example: 200pt each.
                ColumnWidths = "200 200"
            };

            // Add the table to the page
            page.Paragraphs.Add(table);

            // ------------------------------------------------------------
            // Create a header row (optional)
            // ------------------------------------------------------------
            Row headerRow = table.Rows.Add();
            Cell headerCell1 = headerRow.Cells.Add();
            headerCell1.Paragraphs.Add(new TextFragment("Product"));
            Cell headerCell2 = headerRow.Cells.Add();
            headerCell2.Paragraphs.Add(new TextFragment("Image"));

            // ------------------------------------------------------------
            // Create a data row and insert the image into the second cell
            // ------------------------------------------------------------
            Row dataRow = table.Rows.Add();

            // First cell – some text
            Cell textCell = dataRow.Cells.Add();
            textCell.Paragraphs.Add(new TextFragment("Sample Item"));

            // Second cell – the image
            Cell imageCell = dataRow.Cells.Add();

            // Create an Image object, set its source file, and add it to the cell
            Image img = new Image
            {
                File = imagePath,
                // Optional: scale the image to fit the cell
                ImageScale = 0.5f
            };
            imageCell.Paragraphs.Add(img);

            // ------------------------------------------------------------
            // Save the resulting PDF
            // ------------------------------------------------------------
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with image in table cell saved to '{outputPdf}'.");
    }
}