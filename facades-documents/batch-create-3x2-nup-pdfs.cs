using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of source PDF files to be processed.
        string[] inputFiles = new string[]
        {
            "document1.pdf",
            "document2.pdf",
            "document3.pdf"
        };

        // Ensure the output directory exists.
        string outputDirectory = "NupOutput";
        Directory.CreateDirectory(outputDirectory);

        // PdfFileEditor provides N‑up functionality without needing a Document object.
        PdfFileEditor pdfEditor = new PdfFileEditor();

        foreach (string inputPath in inputFiles)
        {
            // Verify that the source file exists before processing.
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Source file not found: {inputPath}");
                continue;
            }

            // Build the output file name (e.g., document1_nup.pdf).
            string baseName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDirectory, $"{baseName}_nup.pdf");

            // Create a 3‑column by 2‑row N‑up version of the PDF.
            // MakeNUp returns true on success.
            bool result = pdfEditor.MakeNUp(inputPath, outputPath, 3, 2);

            if (result)
            {
                Console.WriteLine($"N‑up PDF created: {outputPath}");
            }
            else
            {
                Console.Error.WriteLine($"Failed to create N‑up PDF for: {inputPath}");
            }
        }
    }
}