using System;
using System.IO;
using Aspose.Pdf;                     // Core API
using Aspose.Pdf.Security;           // For PdfFormat enum (belongs to Aspose.Pdf namespace)

// Validate a PDF for PDF/A‑1B compliance and write a log of non‑conformant elements.
class PdfA1BValidator
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // PDF to be validated
        const string logFile   = "validation_log.txt"; // Log of non‑conformant elements

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the document inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPdf))
            {
                // Perform validation against PDF/A‑1B. The method returns true if the document
                // complies, otherwise false. All validation messages are written to logFile.
                bool isCompliant = doc.Validate(logFile, PdfFormat.PDF_A_1B);

                // Additionally, the IsPdfaCompliant property reflects the result.
                Console.WriteLine($"PDF/A‑1B compliance: {isCompliant}");
                Console.WriteLine($"Detailed log written to: {logFile}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Validation failed: {ex.Message}");
        }
    }
}