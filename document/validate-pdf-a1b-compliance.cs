using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the PDF to be validated
        const string inputPdfPath = "input.pdf";

        // Path where the validation log will be written
        const string validationLogPath = "validation_log.txt";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document and automatically dispose it when done
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Validate the document against PDF/A‑1B compliance.
            // The method returns true if validation succeeds, false otherwise.
            bool validationResult = pdfDoc.Validate(validationLogPath, PdfFormat.PDF_A_1B);

            // Output the validation outcome and the compliance flag.
            Console.WriteLine($"PDF/A‑1B validation result: {(validationResult ? "Passed" : "Failed")}");
            Console.WriteLine($"Document.IsPdfaCompliant: {pdfDoc.IsPdfaCompliant}");
        }
    }
}