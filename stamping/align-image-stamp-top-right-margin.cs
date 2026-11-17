using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string stampPath  = "stamp.png";
        const string outputPath = "output.pdf";

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
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create an ImageStamp and configure alignment to top‑right with margins
            ImageStamp imgStamp = new ImageStamp(stampPath)
            {
                // Align to the right edge and top edge of the page
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Top,
                // Use individual margin properties (ImageStamp has no Margin object)
                RightMargin = 10,   // offset from the right edge (points)
                TopMargin   = 10    // offset from the top edge (points)
            };

            // Apply the stamp to each page individually
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF while the Document is still alive
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Image stamp applied to top‑right corner and saved as '{outputPath}'.");
    }
}
