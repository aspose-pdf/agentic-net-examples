using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // List of input PDF files to be processed
        string[] inputFiles = new string[]
        {
            "document1.pdf",
            "document2.pdf",
            "document3.pdf"
        };

        // Directory where N‑up PDFs will be saved
        string outputDirectory = "NupOutput";
        Directory.CreateDirectory(outputDirectory);

        // N‑up layout: 3 columns (x) and 2 rows (y)
        int columns = 3;
        int rows = 2;

        foreach (string inputPath in inputFiles)
        {
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"Input file not found: {inputPath}");
                continue;
            }

            // Build output file name (e.g., document1_nup.pdf)
            string outputFileName = Path.GetFileNameWithoutExtension(inputPath) + "_nup.pdf";
            string outputPath = Path.Combine(outputDirectory, outputFileName);

            // Create N‑up version using PdfFileEditor
            PdfFileEditor pdfEditor = new PdfFileEditor();
            bool result = pdfEditor.MakeNUp(inputPath, outputPath, columns, rows);

            if (result)
                Console.WriteLine($"Successfully created N‑up PDF: {outputPath}");
            else
                Console.Error.WriteLine($"Failed to create N‑up PDF for: {inputPath}");
        }
    }
}