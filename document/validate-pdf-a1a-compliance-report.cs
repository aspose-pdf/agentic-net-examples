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
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Validate the document against PDF/A‑1A and write a detailed log
                bool isCompliant = doc.Validate(reportPath, PdfFormat.PDF_A_1A);

                Console.WriteLine($"Validation result: {(isCompliant ? "Compliant" : "Non‑compliant")}");
                Console.WriteLine($"Detailed report saved to: {reportPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}