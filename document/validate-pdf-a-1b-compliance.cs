using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string logPath   = "validation_log.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Validate the document against PDF/A‑1B and write the log
            bool validationResult = doc.Validate(logPath, PdfFormat.PDF_A_1B);

            // Output the compliance status and validation result
            Console.WriteLine($"Is PDF/A‑1B compliant: {doc.IsPdfaCompliant}");
            Console.WriteLine($"Validation succeeded (no errors): {validationResult}");
            Console.WriteLine($"Log file created at: {logPath}");
        }
    }
}