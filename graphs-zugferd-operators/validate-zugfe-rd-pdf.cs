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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Validate against ZUGFeRD specification and write log
                bool isValid = doc.Validate(logFile, PdfFormat.ZUGFeRD);

                Console.WriteLine($"ZUGFeRD validation result: {isValid}");
                Console.WriteLine($"Validation log saved to: {logFile}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Validation failed: {ex.Message}");
        }
    }
}