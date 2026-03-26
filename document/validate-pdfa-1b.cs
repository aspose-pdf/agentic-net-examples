using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string logPath = "validation.log";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            bool validationResult = doc.Validate(logPath, PdfFormat.PDF_A_1B);
            Console.WriteLine($"Validation completed: {(validationResult ? "Success" : "Failed")}");
            Console.WriteLine($"PDF/A-1B compliance: {(doc.IsPdfaCompliant ? "Compliant" : "Non‑compliant")}");
            Console.WriteLine($"Log file written to: {logPath}");
        }
    }
}