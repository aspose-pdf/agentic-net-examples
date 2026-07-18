using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "logo.png";

        // Desired coordinates (from left and from bottom of the page)
        double xCoord = 100; // horizontal position
        double yCoord = 200; // vertical position

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

        // Load the PDF document (lifecycle: load)
        using (Document doc = new Document(inputPdf))
        {
            // Create an image stamp from the file (lifecycle: create)
            ImageStamp imgStamp = new ImageStamp(imagePath);

            // Set precise placement using XIndent/YIndent (coordinates start from left/bottom)
            imgStamp.XIndent = xCoord;
            imgStamp.YIndent = yCoord;

            // Optional: adjust opacity, background, etc.
            imgStamp.Opacity = 0.8; // 80% opacity

            // Add the stamp to the first page (pages are 1‑based)
            Page page = doc.Pages[1];
            page.AddStamp(imgStamp);

            // Save the modified PDF (lifecycle: save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp placed at ({xCoord}, {yCoord}) and saved to '{outputPdf}'.");
    }
}