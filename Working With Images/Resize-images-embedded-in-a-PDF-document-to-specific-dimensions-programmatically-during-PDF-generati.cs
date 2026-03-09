using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Text;          // Required for text handling (if needed)

class Program
{
    static void Main()
    {
        // Input image to embed and desired dimensions (points; 1 point = 1/72 inch)
        const string imagePath   = "sample.jpg";
        const double targetWidth  = 200; // width in points
        const double targetHeight = 150; // height in points
        const string outputPdf   = "resized_image.pdf";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Create a new PDF document inside a using block (ensures proper disposal)
        using (Document pdfDoc = new Document())
        {
            // Add a blank page (pages are 1‑based)
            pdfDoc.Pages.Add();

            // Create an Image object and set its source file
            Image img = new Image();
            img.File = imagePath;

            // Resize the image by setting FixWidth and FixHeight.
            // These properties define the exact dimensions the image will occupy on the page.
            img.FixWidth  = targetWidth;
            img.FixHeight = targetHeight;

            // Optionally, set alignment or margins here (e.g., img.HorizontalAlignment = HorizontalAlignment.Center;)

            // Add the image to the first page's paragraph collection
            pdfDoc.Pages[1].Paragraphs.Add(img);

            // Save the PDF to the specified path
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with resized image saved to '{outputPdf}'.");
    }
}