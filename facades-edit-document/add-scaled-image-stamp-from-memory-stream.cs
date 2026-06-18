using System;
using System.IO;
using Aspose.Pdf; // Core API types

class Program
{
    static void Main()
    {
        // Paths for input PDF, output PDF, and source image file (used to create the memory stream)
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imageFilePath = "logo.png";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imageFilePath))
        {
            Console.Error.WriteLine($"Image file not found: {imageFilePath}");
            return;
        }

        // Load the image into a memory stream
        byte[] imageBytes = File.ReadAllBytes(imageFilePath);
        using (MemoryStream imageStream = new MemoryStream(imageBytes))
        {
            // Create an ImageStamp from the memory stream
            Aspose.Pdf.ImageStamp imgStamp = new Aspose.Pdf.ImageStamp(imageStream);

            // Scale the stamp to 50% (Zoom factor 0.5)
            imgStamp.Zoom = 0.5; // 50% of original size

            // Optional: set position of the stamp on the page (coordinates from left and bottom)
            imgStamp.XIndent = 100; // 100 points from the left edge
            imgStamp.YIndent = 100; // 100 points from the bottom edge

            // Open the PDF document
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
            {
                // Add the stamp to the first page (pages are 1‑based)
                pdfDoc.Pages[1].AddStamp(imgStamp);

                // Save the modified PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Image stamp added and saved to '{outputPdfPath}'.");
        }
    }
}