using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace
using Aspose.Pdf.Facades;      // For ConvertErrorAction enum (if needed, but it's in Aspose.Pdf)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // Source PDF file
        const string outputPdf = "output_pdfx3.pdf";   // Destination PDF/X‑3 file
        const string logFile   = "conversion_log.txt"; // Log file for conversion errors

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Convert the document to PDF/X‑3 format.
                // The Convert method writes conversion errors to the specified log file.
                // ConvertErrorAction.Delete tells the engine to delete objects that cannot be converted.
                doc.Convert(logFile, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                // Save the converted document as a regular PDF file (PDF/X‑3 is a PDF variant)
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Conversion completed. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}