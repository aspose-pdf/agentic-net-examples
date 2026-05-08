using System;
using System.IO;
using Aspose.Pdf;

class PdfAValidator
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string reportPath   = "validation_report.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Validate against PDF/A‑1A compliance; the method returns true if compliant
            bool isCompliant = doc.Validate(reportPath, PdfFormat.PDF_A_1A);

            Console.WriteLine(isCompliant
                ? "Document is PDF/A‑1A compliant."
                : "Document is NOT PDF/A‑1A compliant. See report for details.");

            Console.WriteLine($"Validation report saved to: {reportPath}");
        }
    }
}