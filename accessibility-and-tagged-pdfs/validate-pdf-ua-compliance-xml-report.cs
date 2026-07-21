using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string reportFilePath = "validation_report.xml";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Validate the document against PDF/UA‑1. The method writes a log (XML) to the specified file.
            bool isCompliant = pdfDoc.Validate(reportFilePath, PdfFormat.PDF_UA_1);

            Console.WriteLine($"PDF/UA validation result: {(isCompliant ? "Compliant" : "Non‑compliant")}");

            // Display the generated XML report (if the file was created)
            if (File.Exists(reportFilePath))
            {
                string reportXml = File.ReadAllText(reportFilePath);
                Console.WriteLine("=== Validation Report (XML) ===");
                Console.WriteLine(reportXml);
            }

            // Additionally, the Document exposes a property that reflects PDF/UA compliance
            Console.WriteLine($"Document.IsPdfUaCompliant property: {pdfDoc.IsPdfUaCompliant}");
        }
    }
}