using System;
using System.IO;
using Aspose.Pdf;               // Core API (Document, Page, PageLabel, NumberingStyle)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Insert a blank page at the very beginning (position 1, 1‑based indexing)
            doc.Pages.Insert(1);

            // Create a custom page label: prefix "i", no numbering (just the prefix)
            PageLabel customLabel = new PageLabel
            {
                Prefix          = "i",
                NumberingStyle  = NumberingStyle.None,
                StartingValue   = 0   // value is ignored when NumberingStyle is None
            };

            // Update the label for the newly inserted page (page index is 0‑based)
            doc.PageLabels.UpdateLabel(0, customLabel);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page with label \"i\" added and saved to '{outputPath}'.");
    }
}