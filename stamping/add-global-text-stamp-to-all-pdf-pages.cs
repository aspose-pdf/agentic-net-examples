using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // ImageStamp resides here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string stampPath  = "stamp.png";
        const string outputPath = "stamped_output.pdf";

        // Verify files exist
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
        using (Document pdfDoc = new Document(inputPath))
        {
            // Create the stamp once; it will be applied to each page
            ImageStamp stamp = new ImageStamp(stampPath)
            {
                // Example positioning – center of the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Make the stamp semi‑transparent
                Opacity = 0.5,
                // Place it in front of page content
                Background = false
            };

            // Apply the stamp to every page individually
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(stamp);
            }

            // Save the modified document
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Stamp applied to all pages. Output saved to '{outputPath}'.");
    }
}