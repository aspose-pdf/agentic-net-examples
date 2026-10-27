using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string stampPath = "stamp.png";
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
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create an ImageStamp with fixed dimensions (points)
            ImageStamp imgStamp = new ImageStamp(stampPath)
            {
                Width = 200,          // Fixed width regardless of page size
                Height = 100,         // Fixed height regardless of page size
                LeftMargin = 50,      // Fixed horizontal offset from the left edge
                BottomMargin = 50,    // Fixed vertical offset from the bottom edge
                Background = false    // Place stamp on top of existing content
            };

            // Apply the same stamp to each page individually
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}