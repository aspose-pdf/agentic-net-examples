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

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Validate against PDF/A‑1B and write the log
                bool validationSucceeded = doc.Validate(logPath, PdfFormat.PDF_A_1B);

                Console.WriteLine($"Validation succeeded: {validationSucceeded}");
                Console.WriteLine($"PDF/A‑1B compliant: {doc.IsPdfaCompliant}");
                Console.WriteLine($"Log file: {logPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}