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

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Validate the document against PDF/UA and write the XML compliance report
                bool isCompliant = doc.Validate(reportPath, PdfFormat.PDF_UA_1);
                Console.WriteLine($"Validation result: {(isCompliant ? "Compliant" : "Non‑compliant")}");
                Console.WriteLine($"XML report saved to: {reportPath}");

                // Additional check via the IsPdfUaCompliant property
                Console.WriteLine($"IsPdfUaCompliant property: {doc.IsPdfUaCompliant}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}