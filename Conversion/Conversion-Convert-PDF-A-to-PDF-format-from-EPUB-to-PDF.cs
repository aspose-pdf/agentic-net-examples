using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, LoadOptions, etc.)

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string epubInputPath   = "input.epub";   // EPUB source file
        const string pdfFromEpubPath = "output_from_epub.pdf";

        const string pdfaInputPath   = "input_pdfa.pdf"; // PDF/A source file
        const string pdfFromPdfaPath = "output_from_pdfa.pdf";

        // -----------------------------------------------------------------
        // 1. Convert EPUB → PDF
        // -----------------------------------------------------------------
        if (!File.Exists(epubInputPath))
        {
            Console.Error.WriteLine($"EPUB file not found: {epubInputPath}");
        }
        else
        {
            try
            {
                // Document constructor automatically detects the source format (EPUB here)
                using (Document epubDoc = new Document(epubInputPath))
                {
                    // Save as PDF – no SaveOptions required for default PDF output
                    epubDoc.Save(pdfFromEpubPath);
                }

                Console.WriteLine($"EPUB successfully converted to PDF: {pdfFromEpubPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error converting EPUB to PDF: {ex.Message}");
            }
        }

        // -----------------------------------------------------------------
        // 2. Convert PDF/A → PDF (standard PDF)
        // -----------------------------------------------------------------
        if (!File.Exists(pdfaInputPath))
        {
            Console.Error.WriteLine($"PDF/A file not found: {pdfaInputPath}");
        }
        else
        {
            try
            {
                // PDF/A is already a PDF; loading it as a Document works fine
                using (Document pdfaDoc = new Document(pdfaInputPath))
                {
                    // Saving without any special options writes a regular PDF (PDF 1.7 by default)
                    pdfaDoc.Save(pdfFromPdfaPath);
                }

                Console.WriteLine($"PDF/A successfully converted to standard PDF: {pdfFromPdfaPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error converting PDF/A to PDF: {ex.Message}");
            }
        }
    }
}