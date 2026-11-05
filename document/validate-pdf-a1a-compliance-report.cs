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
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Validate the PDF against PDF/A‑1A conformance.
                // The Validate method writes a detailed log to the supplied file and returns a bool indicating compliance.
                bool isCompliant = doc.Validate(reportPath, PdfFormat.PDF_A_1A);

                // Append a short summary to the same report file.
                using (StreamWriter writer = new StreamWriter(reportPath, true))
                {
                    writer.WriteLine();
                    writer.WriteLine("--- Summary ---");
                    writer.WriteLine($"PDF/A‑1A compliant: {isCompliant}");
                }

                Console.WriteLine($"Validation completed. Report saved to '{reportPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
