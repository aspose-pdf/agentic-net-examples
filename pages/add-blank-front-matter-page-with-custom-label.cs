using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API (Document, Page, PageLabel, NumberingStyle)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // existing PDF
        const string outputPath = "output.pdf";  // result with blank front‑matter page

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Insert a new blank page at the very beginning (position 1, 1‑based indexing)
            // The returned Page object can be further customized if needed.
            doc.Pages.Insert(1);

            // Create a custom page label "i"
            PageLabel customLabel = new PageLabel
            {
                Prefix = "i",                     // literal prefix
                NumberingStyle = NumberingStyle.None, // no automatic numbering
                StartingValue = 0                 // not used when NumberingStyle is None
            };

            // PageLabelCollection uses zero‑based indexes, so the first page is index 0
            doc.PageLabels.UpdateLabel(0, customLabel);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page with label \"i\" added. Saved to '{outputPath}'.");
    }
}