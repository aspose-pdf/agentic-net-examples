using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPath = "input.pdf";
        // Directory where split parts will be saved
        const string outputDir = "SplitParts";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF (using rule: document-disposal-with-using)
            using (Document srcDoc = new Document(inputPath))
            {
                // Aspose.Pdf uses 1‑based page indexing (rule: page-indexing-one-based)
                int pageCount = srcDoc.Pages.Count;

                // Iterate over each page and create a separate PDF part
                for (int i = 1; i <= pageCount; i++)
                {
                    // Create a new empty document for the current part
                    using (Document partDoc = new Document())
                    {
                        // Add the specific page from the source document
                        partDoc.Pages.Add(srcDoc.Pages[i]);

                        // Build the output file name (e.g., Part_1.pdf, Part_2.pdf, ...)
                        string outPath = Path.Combine(outputDir, $"Part_{i}.pdf");

                        // Save the part as a PDF (PDF format does not require explicit SaveOptions)
                        partDoc.Save(outPath);

                        Console.WriteLine($"Saved part {i} to '{outPath}'");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}