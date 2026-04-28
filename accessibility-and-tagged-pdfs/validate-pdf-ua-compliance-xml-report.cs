using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Input PDF to be validated
        const string inputPdfPath = "input.pdf";

        // Path where the validation log (XML report) will be written
        const string reportPath = "validation_report.xml";

        // Verify that the source file exists before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Validate the document against PDF/UA (PDF_UA_1) standard.
            // The method returns true if the document complies, false otherwise.
            bool isCompliant = pdfDoc.Validate(reportPath, PdfFormat.PDF_UA_1);

            // Output the validation result to the console
            Console.WriteLine($"PDF/UA validation result: {(isCompliant ? "Compliant" : "Non‑compliant")}");

            // The validation log (XML) is saved at the location specified by reportPath
            Console.WriteLine($"Validation report saved to: {reportPath}");

            // Optional: also display the built‑in compliance flag
            Console.WriteLine($"Document.IsPdfUaCompliant property: {pdfDoc.IsPdfUaCompliant}");
        }
    }
}