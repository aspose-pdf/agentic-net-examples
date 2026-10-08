using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string logoImagePath = "logo.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(logoImagePath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoImagePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Create an ImageStamp for the logo
            ImageStamp logoStamp = new ImageStamp(logoImagePath)
            {
                // Align the stamp to the left side of the page
                HorizontalAlignment = HorizontalAlignment.Left,
                // Align the stamp to the top of the page (header area)
                VerticalAlignment   = VerticalAlignment.Top,
                // Ensure the stamp does not obscure existing content
                Background = false,
                // Adjust opacity if desired (1.0 = fully opaque)
                Opacity = 1.0
            };

            // Position the stamp 10 points from the left and 10 points from the top
            logoStamp.XIndent = 10f; // left offset in points
            logoStamp.YIndent = 10f; // top offset in points

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(logoStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Header with logo added to each page. Saved as '{outputPdfPath}'.");
    }
}
