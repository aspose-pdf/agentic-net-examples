using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";

        // Directory where split pages will be saved
        const string outputDir = "output_pages";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Load the source PDF document
        Document sourceDoc = new Document(inputPath);

        // Iterate over each page (Aspose.Pdf collections are 1‑based)
        for (int i = 1; i <= sourceDoc.Pages.Count; i++)
        {
            // Create a new empty PDF document
            Document singlePageDoc = new Document();

            // Add a copy of the current page to the new document
            singlePageDoc.Pages.Add(sourceDoc.Pages[i]);

            // Build the output file name for this page
            string outPath = Path.Combine(outputDir, $"page_{i}.pdf");

            // Save the single‑page PDF (uses the provided document-save rule)
            singlePageDoc.Save(outPath);
        }

        Console.WriteLine($"Successfully split '{inputPath}' into {sourceDoc.Pages.Count} separate PDF files in '{outputDir}'.");
    }
}