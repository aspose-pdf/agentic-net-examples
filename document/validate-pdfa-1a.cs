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

        using (Document doc = new Document(inputPath))
        {
            bool validationResult = doc.Validate(reportPath, PdfFormat.PDF_A_1A);
            Console.WriteLine($"Validation completed. Success: {validationResult}");
            Console.WriteLine($"PDF/A‑1A compliance flag: {doc.IsPdfaCompliant}");
            Console.WriteLine($"Detailed report saved to: {reportPath}");
        }
    }
}