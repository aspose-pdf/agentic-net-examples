using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string logFile = "validation_log.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Validate the document against PDF/A‑1B and write the log
            bool validationResult = doc.Validate(logFile, PdfFormat.PDF_A_1B);

            // Additional check via the IsPdfaCompliant property
            bool isCompliant = doc.IsPdfaCompliant;

            Console.WriteLine($"Validate method returned: {validationResult}");
            Console.WriteLine($"IsPdfaCompliant property: {isCompliant}");
            Console.WriteLine($"Validation log saved to: {logFile}");
        }
    }
}