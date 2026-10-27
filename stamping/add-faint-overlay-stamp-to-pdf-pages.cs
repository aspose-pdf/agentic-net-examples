using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // not needed for ImageStamp but kept for completeness

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string stampPath  = "overlay.png"; // image to use as stamp
        const string outputPath = "stamped_output.pdf";

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
        using (Document doc = new Document(inputPath))
        {
            // Create the stamp once; it will be reused for each page
            ImageStamp stamp = new ImageStamp(stampPath)
            {
                // Set opacity to 0.4 for a faint overlay
                Opacity = 0.4,
                // Optional: position the stamp (here centered on the page)
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}