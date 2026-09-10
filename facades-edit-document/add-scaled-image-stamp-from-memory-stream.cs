using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "image.png";

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

        // Load the PDF document (1‑based page indexing)
        using (Document doc = new Document(inputPdf))
        {
            // Load the image into a memory stream
            using (MemoryStream imgStream = new MemoryStream(File.ReadAllBytes(imagePath)))
            {
                // Create an ImageStamp from the stream
                Aspose.Pdf.ImageStamp imgStamp = new Aspose.Pdf.ImageStamp(imgStream);

                // Scale the stamp to 50% (both axes)
                imgStamp.Zoom = 0.5f;

                // Optional: set the position of the stamp on the page
                imgStamp.XIndent = 100; // distance from left edge
                imgStamp.YIndent = 500; // distance from bottom edge

                // Add the stamp to the first page
                doc.Pages[1].AddStamp(imgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdf}'.");
    }
}