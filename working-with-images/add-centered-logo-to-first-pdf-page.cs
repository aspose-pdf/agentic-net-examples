using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string logoImagePath = "logo.png";
        const string outputPdfPath = "output.pdf";

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

        // Wrap Document in a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page firstPage = doc.Pages[1];

            // Create an ImageStamp for the logo
            ImageStamp logoStamp = new ImageStamp(logoImagePath)
            {
                // Center the stamp on the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
                // The stamp keeps the original image size; adjust Width/Height if scaling is required
            };

            // Add the stamp only to the first page
            firstPage.AddStamp(logoStamp);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Logo added to first page and saved as '{outputPdfPath}'.");
    }
}