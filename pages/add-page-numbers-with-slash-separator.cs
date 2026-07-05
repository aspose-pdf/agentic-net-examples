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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a page number stamp with custom format "current/total"
            // The placeholder character '#' will be replaced by the page number.
            // Using "#/#" inserts a '/' delimiter between current page and total pages.
            PageNumberStamp pageNumberStamp = new PageNumberStamp("#/#")
            {
                // Position the stamp at the bottom center of each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Bottom,
                BottomMargin        = 20, // distance from the bottom edge
                // Optional: adjust font size, color, etc.
                TextState = { FontSize = 12, ForegroundColor = Color.Black }
            };

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}