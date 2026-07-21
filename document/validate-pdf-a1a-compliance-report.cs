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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Validate against PDF/A‑1A and write detailed log to reportPath
                bool isCompliant = doc.Validate(reportPath, PdfFormat.PDF_A_1A);

                // Output the validation outcome
                Console.WriteLine($"PDF/A‑1A compliance: {(isCompliant ? "Compliant" : "Non‑compliant")}");
                Console.WriteLine($"Validation report saved to: {reportPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Validation error: {ex.Message}");
        }
    }
}