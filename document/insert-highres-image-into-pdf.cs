using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the high‑resolution image, and the output PDF
        const string inputPdfPath  = "input.pdf";
        const string imagePath     = "highres.jpg";
        const string outputPdfPath = "output.pdf";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: use using for disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Select the page where the image will be placed (1‑based indexing)
            Page page = pdfDoc.Pages[1];

            // Define the rectangle that specifies the absolute position of the image.
            // Parameters: llx, lly, urx, ury (lower‑left X/Y, upper‑right X/Y)
            // Example places the image at (100, 500) with a width of 300 and height of 200.
            Aspose.Pdf.Rectangle imageRect = new Aspose.Pdf.Rectangle(100, 500, 400, 700);

            // Open the image file as a stream (high‑resolution source)
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // Add the image to the page.
                // The overload without explicit width/height keeps the image's aspect ratio.
                page.AddImage(imgStream, imageRect);
            }

            // Save the modified PDF (lifecycle rule: use Save)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"High‑resolution image inserted and saved to '{outputPdfPath}'.");
    }
}