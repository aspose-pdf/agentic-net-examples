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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Load the image into a memory stream
            using (FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            using (MemoryStream imageStream = new MemoryStream())
            {
                fileStream.CopyTo(imageStream);
                imageStream.Position = 0; // reset stream position

                // Create an ImageStamp from the memory stream
                ImageStamp imgStamp = new ImageStamp(imageStream);

                // Scale the stamp to 50% (Zoom factor = 0.5)
                imgStamp.Zoom = 0.5;

                // Position the stamp on the page – use XIndent/YIndent instead of SetOrigin
                imgStamp.XIndent = 100; // distance from the left edge
                imgStamp.YIndent = 500; // distance from the bottom edge

                // Optional: control layering (foreground/background)
                imgStamp.Background = false; // draw over existing content

                // Add the stamp to the page
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPdfPath}'.");
    }
}
