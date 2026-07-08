using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string logoPath   = "logo.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Create an image stamp from the logo file
            ImageStamp logoStamp = new ImageStamp(logoPath);

            // Set semi‑transparent opacity (0.0‑1.0)
            logoStamp.Opacity = 0.5;

            // Position the stamp (center of the page)
            logoStamp.HorizontalAlignment = HorizontalAlignment.Center;
            logoStamp.VerticalAlignment   = VerticalAlignment.Center;

            // Add the stamp to the first page (pages are 1‑based)
            doc.Pages[1].AddStamp(logoStamp);

            // Save the modified PDF (lifecycle rule: use Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Semi‑transparent logo stamp added to first page and saved as '{outputPath}'.");
    }
}