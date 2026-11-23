using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input image file (any supported format)
        const string imagePath = "background.jpg";
        // Output PDF file
        const string outputPath = "output.pdf";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document and ensure deterministic disposal
        using (var doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Load the background image from a file stream and assign it to the page
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                var bgImage = new Image { ImageStream = imgStream };
                page.BackgroundImage = bgImage;
            }

            // Save the document as a PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
