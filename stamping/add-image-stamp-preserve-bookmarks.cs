using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string stampPath  = "stamp.png";

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

        // Load the existing PDF (bookmarks and outline are kept)
        using (Document doc = new Document(inputPath))
        {
            // Create an image stamp – configure its appearance as needed
            ImageStamp imgStamp = new ImageStamp(stampPath)
            {
                // Align stamp to the bottom‑right corner of each page
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Make the stamp semi‑transparent
                Opacity = 0.5,
                // Optional margins from the page edges
                RightMargin  = 10,
                BottomMargin = 10
            };

            // Apply the stamp to every page; existing bookmarks/outlines remain unchanged
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp added successfully. Output saved to '{outputPath}'.");
    }
}