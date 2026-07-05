using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string imagePath = "logo.png";
        const string outputPdf = "table_with_image.pdf";

        // Verify the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document())
        {
            // Add a blank page
            Aspose.Pdf.Page page = pdfDoc.Pages.Add();

            // Create a table with one column (you can add more columns if required)
            Aspose.Pdf.Table table = new Aspose.Pdf.Table
            {
                // Set column widths (percentage of page width)
                ColumnWidths = "100%"
            };

            // Create a row
            Aspose.Pdf.Row row = table.Rows.Add();

            // Create a cell
            Aspose.Pdf.Cell cell = row.Cells.Add();

            // Create an Image object and assign the external file
            Aspose.Pdf.Image img = new Aspose.Pdf.Image
            {
                File = imagePath,
                // Optional: set image dimensions (in points). Adjust as needed.
                FixWidth = 150,
                FixHeight = 100
            };

            // Add the image to the cell's paragraph collection
            cell.Paragraphs.Add(img);

            // Add the table to the page
            page.Paragraphs.Add(table);

            // Save the PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}