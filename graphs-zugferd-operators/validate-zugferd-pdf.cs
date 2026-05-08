using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "invoice.pdf";               // PDF with ZUGFeRD data
        const string logFile  = "zugferd_validation.log";   // Validation report

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF and validate it against the ZUGFeRD specification
        using (Document doc = new Document(inputPdf))
        {
            bool isValid = doc.Validate(logFile, PdfFormat.ZUGFeRD);

            Console.WriteLine($"ZUGFeRD validation result: {(isValid ? "Valid" : "Invalid")}");
            Console.WriteLine($"Validation log saved to: {logFile}");
        }
    }
}