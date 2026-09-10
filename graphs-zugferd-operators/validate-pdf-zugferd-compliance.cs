using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string logFile   = "validation_log.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Validate against the ZUGFeRD specification and write log
            bool isValid = doc.Validate(logFile, PdfFormat.ZUGFeRD);

            Console.WriteLine($"ZUGFeRD validation result: {isValid}");
            Console.WriteLine($"Log written to: {logFile}");
        }
    }
}