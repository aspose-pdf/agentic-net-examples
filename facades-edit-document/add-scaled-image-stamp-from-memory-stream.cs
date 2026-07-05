using System;
using System.IO;
using Aspose.Pdf;   // Document, Page, ImageStamp

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "logo.png";

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

        // Load the image into a memory stream
        using (FileStream imgStream = File.OpenRead(imagePath))
        {
            // Create an ImageStamp from the stream
            ImageStamp stamp = new ImageStamp(imgStream);

            // Scale the stamp to 50 % (both width and height)
            stamp.Zoom = 0.5;   // equivalent to setting ZoomX and ZoomY to 0.5

            // Open the PDF document (wrapped in using for deterministic disposal)
            using (Document pdfDoc = new Document(inputPdf))
            {
                // Add the stamp to the first page (Aspose.Pdf uses 1‑based indexing)
                pdfDoc.Pages[1].AddStamp(stamp);

                // Save the modified PDF
                pdfDoc.Save(outputPdf);
            }
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdf}'.");
    }
}