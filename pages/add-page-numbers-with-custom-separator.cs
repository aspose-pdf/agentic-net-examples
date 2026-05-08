using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for PageNumberStamp (inherits TextStamp)
using Aspose.Pdf.Facades;   // not needed but kept for completeness

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "paged_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create a page number stamp with custom format "current/total"
                PageNumberStamp pageNumberStamp = new PageNumberStamp
                {
                    // "#" is replaced with the current page number.
                    // The format "#/#" results in "1/10", "2/10", etc.
                    Format = "#/#",
                    // Position the stamp at the bottom center of the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Bottom,
                    BottomMargin        = 20, // distance from the bottom edge
                    // Optional: set font size and color
                    TextState = { FontSize = 12, ForegroundColor = Aspose.Pdf.Color.Black }
                };

                // Add the stamp to the current page
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified PDF (explicit SaveOptions not required for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}