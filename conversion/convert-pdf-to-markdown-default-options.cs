using System;
using System.IO;
using Aspose.Pdf;               // Aspose.Pdf contains Document and all SaveOptions subclasses

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.md";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Create MarkdownSaveOptions with default settings
                MarkdownSaveOptions mdOptions = new MarkdownSaveOptions();

                // Save the document as Markdown; explicit SaveOptions are required for non‑PDF formats
                doc.Save(outputPath, mdOptions);
            }

            Console.WriteLine($"PDF successfully converted to Markdown: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
    }
}