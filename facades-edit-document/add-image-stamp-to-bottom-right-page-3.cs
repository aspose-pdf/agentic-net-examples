using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string logoPath = "logo.png";
        const string outputPath = "output.pdf";

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create an ImageStamp from the logo file
            ImageStamp stamp = new ImageStamp(logoPath)
            {
                // Position the stamp at the bottom‑right corner of the page
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,
                XIndent = 0,
                YIndent = 0
                // Width and Height can be set here if scaling is required
            };

            // Ensure the document has at least three pages (1‑based indexing)
            if (doc.Pages.Count >= 3)
            {
                // Apply the stamp to page three
                doc.Pages[3].AddStamp(stamp);
            }
            else
            {
                Console.Error.WriteLine("The PDF contains fewer than 3 pages.");
                return;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}