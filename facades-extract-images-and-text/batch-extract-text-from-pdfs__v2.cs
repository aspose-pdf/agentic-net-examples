using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfBatchExtract
{
    class Program
    {
        static void Main(string[] args)
        {
            // Default directories
            string sourceDirectory = Directory.GetCurrentDirectory();
            string outputDirectory = Path.Combine(sourceDirectory, "ExtractedText");

            // Optional command‑line arguments: [sourceDirectory] [outputDirectory]
            if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
                sourceDirectory = args[0];

            if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
                outputDirectory = args[1];

            // Validate source directory
            if (!Directory.Exists(sourceDirectory))
            {
                Console.Error.WriteLine($"Source directory does not exist: {sourceDirectory}");
                return;
            }

            // Ensure output directory exists
            Directory.CreateDirectory(outputDirectory);

            // Get all PDF files in the source directory (non‑recursive)
            string[] pdfFiles = Directory.GetFiles(sourceDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

            foreach (var pdfPath in pdfFiles)
            {
                string txtPath = Path.Combine(outputDirectory, Path.GetFileNameWithoutExtension(pdfPath) + ".txt");

                // PdfExtractor implements IDisposable – use a using block for deterministic cleanup
                using (var extractor = new PdfExtractor())
                {
                    extractor.BindPdf(pdfPath);
                    extractor.ExtractText();
                    extractor.GetText(txtPath);
                }

                Console.WriteLine($"Extracted text from '{Path.GetFileName(pdfPath)}' to '{txtPath}'.");
            }
        }
    }
}
// NOTE: Add a reference to Aspose.Pdf.dll (e.g., via NuGet or Add-Type) before building.