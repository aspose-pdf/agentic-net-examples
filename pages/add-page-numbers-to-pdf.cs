using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, Page, PageNumberStamp, etc.)

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

        // Load the PDF document (using statement ensures deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a PageNumberStamp with the desired format.
            // The character '#' is replaced by the current page number,
            // and a second '#' is replaced by the total page count.
            PageNumberStamp pageNumberStamp = new PageNumberStamp("Page # of #");

            // Optional styling – adjust as needed.
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin        = 20;   // distance from bottom edge
            pageNumberStamp.TextState.FontSize  = 12;
            pageNumberStamp.TextState.ForegroundColor = Color.Black;

            // Apply the stamp to every page (pages are 1‑based).
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}