using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string reportPath = "validation_report.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPdf))
        {
            // Validate the document against PDF/A‑1A and write a detailed log file
            // Validate(string, PdfFormat) returns true if the document complies
            bool isCompliant = doc.Validate(reportPath, PdfFormat.PDF_A_1A);

            // Report the outcome
            Console.WriteLine($"PDF/A‑1A compliance: {(isCompliant ? "Compliant" : "Non‑compliant")}");
            Console.WriteLine($"Detailed validation report saved to: {reportPath}");
        }
    }
}