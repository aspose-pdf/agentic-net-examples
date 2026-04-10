using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf;               // For PageLabel, NumberingStyle, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // Existing PDF (can be empty)
        const string outputPath = "output_with_label.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, add a blank page, set a custom page label “i”, and save.
        using (Document doc = new Document(inputPath))
        {
            // Insert a new blank page at the beginning (position 1, 1‑based indexing)
            // The inserted page will adopt the most common page size in the document.
            Page blankPage = doc.Pages.Insert(1);

            // Create a PageLabel with the desired prefix "i" and no numbering.
            PageLabel label = new PageLabel
            {
                Prefix = "i",
                NumberingStyle = NumberingStyle.None,
                StartingValue = 1   // Value is ignored when NumberingStyle is None
            };

            // PageLabelCollection uses zero‑based page indexes.
            // The newly inserted page is at index 0.
            doc.PageLabels.UpdateLabel(0, label);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom front‑matter label to '{outputPath}'.");
    }
}