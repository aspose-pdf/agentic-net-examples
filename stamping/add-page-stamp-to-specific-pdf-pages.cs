using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string stampPath  = "stamp.png";
        const string outputPath = "stamped_output.pdf";

        // Verify required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampPath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPath))
        {
            // Configure the image stamp
            ImageStamp imgStamp = new ImageStamp(stampPath)
            {
                Background          = false,
                Opacity             = 0.5,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Pages are 1‑based. Apply the stamp to pages 5 through 10 (or up to the last page).
            for (int i = 5; i <= 10 && i <= pdfDocument.Pages.Count; i++)
            {
                Page page = pdfDocument.Pages[i];
                page.AddStamp(imgStamp); // AddStamp is a method on each Page, not on the collection
            }

            // Save the modified PDF while the Document is still alive
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Stamp applied to pages 5‑10 and saved as '{outputPath}'.");
    }
}