using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string reportPath = "validation_report.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Validate against PDF/A‑1A and write detailed log to reportPath
                bool isCompliant = doc.Validate(reportPath, PdfFormat.PDF_A_1A);

                // Output result to console
                Console.WriteLine(isCompliant
                    ? "Document is PDF/A‑1A compliant."
                    : "Document is NOT PDF/A‑1A compliant. See report for details.");
                Console.WriteLine($"Validation report saved to '{reportPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during validation: {ex.Message}");
        }
    }
}