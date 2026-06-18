using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace AsposePdfBatchExtractor
{
    class Program
    {
        /// <summary>
        /// Batch extracts text from all PDF files in a directory using Aspose.Pdf.Facades.PdfExtractor.
        /// </summary>
        /// <param name="args">
        /// args[0] (optional) – Source directory containing PDF files. If omitted, the current working directory is used.
        /// args[1] (optional) – Output directory for the extracted .txt files. If omitted, a sub‑folder named "ExtractedText" is created under the source directory.
        /// </param>
        static void Main(string[] args)
        {
            // Resolve source directory (default: current directory)
            string sourceDirectory = args.Length > 0 && !string.IsNullOrWhiteSpace(args[0])
                ? args[0]
                : Directory.GetCurrentDirectory();

            // Resolve output directory (default: <sourceDirectory>\ExtractedText)
            string outputDirectory = args.Length > 1 && !string.IsNullOrWhiteSpace(args[1])
                ? args[1]
                : Path.Combine(sourceDirectory, "ExtractedText");

            // Ensure the output directory exists
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            // Get all PDF files in the source directory (non‑recursive)
            string[] pdfFiles = Directory.GetFiles(sourceDirectory, "*.pdf", SearchOption.TopDirectoryOnly);

            if (pdfFiles.Length == 0)
            {
                Console.WriteLine($"No PDF files found in '{sourceDirectory}'.");
                return;
            }

            foreach (string pdfPath in pdfFiles)
            {
                string baseName = Path.GetFileNameWithoutExtension(pdfPath);
                string txtPath = Path.Combine(outputDirectory, baseName + ".txt");

                try
                {
                    // Using statement ensures proper disposal of the extractor
                    using (PdfExtractor extractor = new PdfExtractor())
                    {
                        extractor.BindPdf(pdfPath);
                        extractor.ExtractText();
                        extractor.GetText(txtPath);
                    }
                    Console.WriteLine($"Extracted text from '{Path.GetFileName(pdfPath)}' to '{txtPath}'.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to extract text from '{pdfPath}': {ex.Message}");
                }
            }
        }
    }
}
