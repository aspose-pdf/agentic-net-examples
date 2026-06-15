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
            // Total number of pages (Aspose.Pdf uses 1‑based indexing)
            int totalPages = doc.Pages.Count;

            // Create a PageNumberStamp with a custom format.
            // The placeholder '#' will be replaced by the current page number.
            // The total page count is inserted directly into the format string.
            PageNumberStamp pageNumberStamp = new PageNumberStamp($"Page # of {totalPages}");

            // Position the stamp at the bottom center of each page
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin        = 20; // optional margin from the bottom edge

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