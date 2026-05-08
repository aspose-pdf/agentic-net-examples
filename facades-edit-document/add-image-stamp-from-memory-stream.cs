using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath = "logo.png";

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

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Load the image into a memory stream (so the stream can be reused safely)
            byte[] imgBytes = File.ReadAllBytes(imagePath);
            using (MemoryStream imgStream = new MemoryStream(imgBytes))
            {
                // Create an ImageStamp from the memory stream
                ImageStamp imgStamp = new ImageStamp(imgStream);

                // Scale the stamp to 50% (both axes)
                imgStamp.Zoom = 0.5f; // Equivalent to setting ZoomX and ZoomY to 0.5

                // Optional: set the position of the stamp on the page
                imgStamp.XIndent = 100; // distance from the left edge
                imgStamp.YIndent = 500; // distance from the bottom edge

                // Add the stamp to each page of the document
                foreach (Page page in pdfDoc.Pages)
                {
                    page.AddStamp(imgStamp);
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdfPath}'.");
    }
}
