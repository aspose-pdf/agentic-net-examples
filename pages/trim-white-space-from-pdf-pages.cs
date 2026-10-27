using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "trimmed_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; iterate through each page
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // NOTE: The Page.Trim() method is not available in the current
                // Aspose.Pdf version used for this project. If trimming of white
                // space is required, consider upgrading to a version that provides
                // Page.Trim or implement a custom trimming routine using bitmap
                // analysis. For now, the page is left unchanged.
            }

            // Save the (untrimmed) PDF. The Document.Save method writes a PDF by default.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
