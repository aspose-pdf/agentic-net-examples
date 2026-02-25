using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, PdfFormat, ConvertErrorAction)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output_pdfx3.pdf";   // PDF/X‑3 result
        const string logFile   = "conversion_log.txt"; // conversion log (optional but recommended)

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
                // Convert to PDF/X‑3 format.
                // ConvertErrorAction.Delete tells the engine to delete objects that cannot be converted.
                bool conversionResult = doc.Convert(logFile, PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

                if (!conversionResult)
                {
                    Console.WriteLine("Conversion completed with warnings. See log for details.");
                }

                // Save the converted document. No SaveOptions needed because we are saving as PDF.
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF successfully converted to PDF/X‑3 and saved as '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}