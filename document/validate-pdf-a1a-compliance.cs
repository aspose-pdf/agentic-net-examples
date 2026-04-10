using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";               // PDF to validate
        const string logFile  = "validation_report.txt";   // Validation report output

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Validate against PDF/A‑1A standard; the method returns true if validation succeeded
            bool validationResult = doc.Validate(logFile, PdfFormat.PDF_A_1A);

            // The IsPdfaCompliant property indicates whether the document conforms to PDF/A
            bool isCompliant = doc.IsPdfaCompliant;

            Console.WriteLine($"Validation completed. Result: {(validationResult ? "Success" : "Failure")}");
            Console.WriteLine($"PDF/A‑1A compliance: {(isCompliant ? "Compliant" : "Non‑compliant")}");
            Console.WriteLine($"Detailed report saved to: {logFile}");
        }
    }
}