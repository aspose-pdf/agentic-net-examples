using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose a page to use as the stamp source (e.g., the first page)
            Aspose.Pdf.Page sourcePage = doc.Pages[1];

            // Create a PdfPageStamp from the source page
            PdfPageStamp pageStamp = new PdfPageStamp(sourcePage)
            {
                // Example: place the stamp as background and set opacity
                Background = true,
                Opacity    = 0.5f
            };

            // Apply the stamp to pages 5 through 10 (inclusive)
            // Ensure we do not exceed the actual page count
            int start = 5;
            int end   = Math.Min(10, doc.Pages.Count);

            for (int i = start; i <= end; i++)
            {
                // Each target page receives the same stamp
                doc.Pages[i].AddStamp(pageStamp);
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}