using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // existing PDF
        const string outputPdf = "output.pdf";  // result PDF
        const string imagePath = "image.png";   // raster image to insert

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document (using ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Select the target page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the image will be placed.
            // Parameters: llx, lly, urx, ury (lower‑left and upper‑right corners).
            // Example places the image at (100, 500) with width 200 and height 150.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 650);

            // Insert the raster image using the overload that takes a file path.
            page.AddImage(imagePath, rect);

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image inserted and saved to '{outputPdf}'.");
    }
}