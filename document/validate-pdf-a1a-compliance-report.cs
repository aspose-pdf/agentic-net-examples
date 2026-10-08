using System;
using System.IO;
using Aspose.Pdf;

class PdfAValidatorProgram
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string logPath = "validation_log.txt";
        const string reportPath = "validation_report.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Validate against PDF/A‑1A conformance.
            // Document.Validate returns a bool indicating overall validity and writes a detailed log file.
            bool isValid = doc.Validate(logPath, PdfFormat.PDF_A_1A);

            // Build a concise validation report and reference the detailed log.
            using (StreamWriter writer = new StreamWriter(reportPath))
            {
                writer.WriteLine($"PDF/A‑1A validation report for '{Path.GetFileName(inputPath)}'");
                writer.WriteLine($"IsValid: {isValid}");
                writer.WriteLine();
                writer.WriteLine($"A detailed validation log has been saved to '{logPath}'.");
                writer.WriteLine("You can inspect this file for the list of errors and warnings.");
            }

            Console.WriteLine($"Validation report saved to '{reportPath}'.");
        }
    }
}
