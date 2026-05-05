using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Text;          // Required for text handling (if needed)

class InsertHighResImage
{
    static void Main()
    {
        // Input high‑resolution image file (PNG, JPEG, etc.)
        const string imagePath = "high_res_image.png";

        // Output PDF file that will contain the image.
        const string outputPdf = "output_with_image.pdf";

        // Verify that the image file exists.
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document.
        using (Document pdfDoc = new Document())
        {
            // Add a blank page to the document.
            Page page = pdfDoc.Pages.Add();

            // Define the rectangle where the image will be placed.
            // Coordinates are in points (1 point = 1/72 inch).
            // Example: place the image at (100, 500) lower‑left corner
            // with a width of 400 points and a height of 300 points.
            double llx = 100;   // lower‑left X
            double lly = 500;   // lower‑left Y
            double urx = llx + 400; // upper‑right X (width = 400)
            double ury = lly + 300; // upper‑right Y (height = 300)

            // Fully qualify the Rectangle type to avoid ambiguity.
            Aspose.Pdf.Rectangle imageRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Open the image file as a stream.
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // Add the image to the page at the specified rectangle.
                // The image will be scaled to fit the rectangle while preserving its resolution.
                page.AddImage(imgStream, imageRect);
            }

            // Save the PDF document.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with high‑resolution image saved to '{outputPdf}'.");
    }
}