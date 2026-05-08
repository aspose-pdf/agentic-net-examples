using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, modify, and save – all within using blocks for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a text stamp with the desired content
            TextStamp stamp = new TextStamp("Sample Text");

            // Align to bottom‑left corner
            stamp.HorizontalAlignment = HorizontalAlignment.Left;
            stamp.VerticalAlignment   = VerticalAlignment.Bottom;

            // Set 10‑point margins from the left and bottom edges
            stamp.LeftMargin   = 10;
            stamp.BottomMargin = 10;

            // Apply the stamp to each page (or a specific page)
            foreach (Page page in doc.Pages)
            {
                stamp.Put(page);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text stamp applied and saved to '{outputPath}'.");
    }
}