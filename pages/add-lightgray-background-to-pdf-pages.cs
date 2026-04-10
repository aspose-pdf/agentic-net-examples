using System;
using System.IO;
using Aspose.Pdf; // Aspose.Pdf namespace contains Document, Page, Color, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "branded_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing) and set a LightGray background.
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                page.Background = Color.LightGray; // LightGray background for branding.
            }

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with LightGray background on all pages: {outputPath}");
    }
}