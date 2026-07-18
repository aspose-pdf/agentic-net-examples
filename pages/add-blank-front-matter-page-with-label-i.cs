using System;
using System.IO;
using Aspose.Pdf;                     // Core API
using Aspose.Pdf.Facades;            // Not needed here but kept for completeness

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // existing PDF (can be empty if you just want a new file)
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Insert a blank page at the very beginning (position 1, 1‑based indexing)
            // The Insert method returns the newly created Page object.
            Page blankPage = doc.Pages.Insert(1);

            // Create a PageLabel that will display the custom prefix “i”.
            // Setting NumberingStyle to None ensures only the prefix is shown.
            PageLabel label = new PageLabel
            {
                Prefix = "i",
                NumberingStyle = NumberingStyle.None,
                StartingValue = 0   // value is ignored when NumberingStyle is None
            };

            // PageLabelCollection uses 0‑based indexing, so the first page has index 0.
            doc.PageLabels.UpdateLabel(0, label);

            // Save the modified PDF (lifecycle rule: use Save inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Blank page with label \"i\" added and saved to '{outputPath}'.");
    }
}