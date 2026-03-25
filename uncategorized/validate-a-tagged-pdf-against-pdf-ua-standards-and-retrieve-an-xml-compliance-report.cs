using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string reportPath = "pdfua_report.xml";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Validate against PDF/UA 1.0 and write the compliance log (XML) to reportPath
            bool validationPassed = doc.Validate(reportPath, PdfFormat.PDF_UA_1);

            Console.WriteLine($"PDF/UA validation result: {(validationPassed ? "Passed" : "Failed")}");
            Console.WriteLine($"Compliance report saved to: {reportPath}");
            Console.WriteLine($"IsPdfUaCompliant property: {doc.IsPdfUaCompliant}");
        }
    }
}