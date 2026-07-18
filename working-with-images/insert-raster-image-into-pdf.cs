using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the image to insert, and the output PDF
        const string pdfPath   = "input.pdf";
        const string imgPath   = "image.png";
        const string outputPdf = "output.pdf";

        // Verify that source files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(imgPath))
        {
            Console.Error.WriteLine($"Image not found: {imgPath}");
            return;
        }

        // Load the existing PDF document (using block ensures proper disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Choose the page where the image will be placed (1‑based index)
            Page page = doc.Pages[1];

            // Define the rectangle where the image will be positioned.
            // Coordinates are in points (1 point = 1/72 inch).
            // Example: lower‑left (100, 500), upper‑right (300, 700)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Insert the raster image at the specified rectangle.
            // This overload adds the image and centers it within the rectangle,
            // preserving the image's aspect ratio.
            page.AddImage(imgPath, rect);

            // Save the modified PDF to a new file.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image inserted and saved to '{outputPdf}'.");
    }
}