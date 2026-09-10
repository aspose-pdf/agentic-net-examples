using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath  = "background.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Background image not found: {imagePath}");
            return;
        }

        // Load the PDF document – wrapped in a using block for proper disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create an ImageStamp that will serve as the background image
            ImageStamp bgStamp = new ImageStamp(imagePath)
            {
                // Render the stamp behind the page content
                Background = true,
                // Set opacity to 30 % (value range 0.0‑1.0)
                Opacity = 0.3
            };

            // Apply the stamp to every page (Pages collection is 1‑based)
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(bgStamp);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with background image: {outputPath}");
    }
}