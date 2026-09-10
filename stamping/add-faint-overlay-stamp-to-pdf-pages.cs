using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for TextStamp is in Aspose.Pdf namespace, but include for completeness

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp that will be applied to every page
            TextStamp stamp = new TextStamp("Overlay")
            {
                // Center the stamp on the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Make the stamp faint
                Opacity = 0.4,
                // Optional: adjust font size and color
                TextState = { FontSize = 48, ForegroundColor = Color.Gray }
            };

            // Apply the stamp to each page (pages are 1‑based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}