using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expect the input PDF path as the first argument
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-pdf-path> [output-pdf-path]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : null;

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document pdfDocument = new Document(inputPath))
            {
                // Example accessibility‑related check: does the document have a logical structure?
                bool hasTaggedContent = pdfDocument.TaggedContent != null;
                Console.WriteLine($"Pages: {pdfDocument.Pages.Count}");
                Console.WriteLine($"Tagged content present: {hasTaggedContent}");

                // If an output path is provided, save a copy of the document
                if (!string.IsNullOrEmpty(outputPath))
                {
                    // Ensure the output directory exists
                    string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
                    if (!Directory.Exists(outputDir))
                    {
                        Directory.CreateDirectory(outputDir);
                    }

                    // document-save rule
                    pdfDocument.Save(outputPath);
                    Console.WriteLine($"Document saved to: {outputPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}