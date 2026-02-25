using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, PdfFormat, ConvertErrorAction)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output_pdfe.pdf";    // target PDF/E file
        const string logPath    = "conversion_log.txt"; // conversion log

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Convert the document to PDF/E (engineered PDF) format.
                // Aspose.Pdf uses PdfFormat.PDF_X_3 for PDF/E conversion.
                // The Convert method also writes a log file with any conversion issues.
                bool success = doc.Convert(logPath, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                if (!success)
                {
                    Console.Error.WriteLine("Conversion reported failures. Check the log file for details.");
                }

                // Save the converted document. No SaveOptions are needed because we are saving as PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Conversion completed. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}