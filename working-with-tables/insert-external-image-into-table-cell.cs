using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string imagePath = "image.png";      // external image file
        const string outputPdf = "output.pdf";

        // Ensure the image file exists
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document and add a page
        using (Document pdf = new Document())
        {
            pdf.Pages.Add();

            // Create a table with one row and one cell
            Table table = new Table
            {
                // Optional: set table width to page width
                ColumnWidths = "500"
            };
            var row = table.Rows.Add();          // use var to avoid explicit TableRow type
            var cell = row.Cells.Add();          // use var to avoid explicit TableCell type

            // Insert the external image using the Image class (ImageFragment is not available in this version)
            Image img = new Image
            {
                // Load image from file stream
                ImageStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read),
                // Optional: scale the image if needed
                ImageScale = 0.5
            };
            cell.Paragraphs.Add(img);

            // Add the table to the page
            pdf.Pages[1].Paragraphs.Add(table);

            // Save the PDF
            pdf.Save(outputPdf);
        }

        Console.WriteLine($"PDF with image inserted into cell saved to '{outputPdf}'.");
    }
}
