using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string stampPath  = "logo.png";
        const string outputPath = "output.pdf";

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

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPath))
            {
                // Create an image stamp with semi‑transparent opacity
                ImageStamp logoStamp = new ImageStamp(stampPath)
                {
                    Background          = false,   // place on top of page content
                    Opacity             = 0.5,     // 50 % transparency
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Top
                };

                // Pages are 1‑based; add the stamp only to the first page
                Page firstPage = pdfDoc.Pages[1];
                firstPage.AddStamp(logoStamp);

                // Save the modified PDF
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Semi‑transparent logo stamp added to first page. Saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}