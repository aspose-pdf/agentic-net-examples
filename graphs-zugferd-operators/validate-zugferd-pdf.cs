using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "invoice.pdf";
        const string logFile   = "validation_log.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF containing ZUGFeRD data
            using (Document doc = new Document(inputPdf))
            {
                // Validate against the ZUGFeRD specification and write log
                bool isCompliant = doc.Validate(logFile, PdfFormat.ZUGFeRD);

                Console.WriteLine($"ZUGFeRD validation result: {isCompliant}");
                Console.WriteLine($"Log saved to: {logFile}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Validation error: {ex.Message}");
        }
    }
}