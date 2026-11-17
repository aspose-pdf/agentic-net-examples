using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for annotation types if needed

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPath     = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF, add an image stamp to each page, and preserve existing annotations
        using (Document pdfDocument = new Document(inputPath))
        {
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                Background          = false, // place stamp over page content
                Opacity             = 0.5,   // semi‑transparent
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Apply the stamp to every page individually (per‑page AddStamp)
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF; annotations remain intact
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Image stamp added and saved to '{outputPath}'.");
    }
}