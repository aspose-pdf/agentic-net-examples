using System;
using System.IO;
using Aspose.Pdf.Facades;

namespace PdfProcessor
{
    class Program
    {
        // Directory inside the container that will be mounted from the host
        private const string InputDirectory = "/data";

        static void Main(string[] args)
        {
            Console.WriteLine("PDF extraction service started.");
            if (!Directory.Exists(InputDirectory))
            {
                Console.Error.WriteLine($"Input directory '{InputDirectory}' does not exist.");
                return;
            }

            // Process all PDF files found in the mounted volume
            foreach (string pdfPath in Directory.EnumerateFiles(InputDirectory, "*.pdf", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    ExtractText(pdfPath);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to process '{pdfPath}': {ex.Message}");
                }
            }

            Console.WriteLine("PDF extraction service completed.");
        }

        // Uses Aspose.Pdf.Facades.PdfExtractor to extract text from a PDF file
        private static void ExtractText(string pdfPath)
        {
            // Output text file will be placed alongside the source PDF
            string txtPath = Path.ChangeExtension(pdfPath, ".txt");

            // PdfExtractor does NOT implement IDisposable, so no using block is required
            PdfExtractor extractor = new PdfExtractor();

            // Bind the PDF file to the extractor
            extractor.BindPdf(pdfPath);

            // Enable text extraction
            extractor.ExtractText();

            // Save extracted text to a .txt file
            extractor.GetText(txtPath);

            Console.WriteLine($"Extracted text from '{Path.GetFileName(pdfPath)}' to '{Path.GetFileName(txtPath)}'.");
        }
    }
}
