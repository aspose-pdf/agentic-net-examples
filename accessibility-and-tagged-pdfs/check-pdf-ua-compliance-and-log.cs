using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string logPath = "validation.log"; // optional log file for validation details

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Perform PDF/UA‑1 validation. The Validate method returns a bool indicating compliance.
            bool isPdfUaCompliant = doc.Validate(logPath, PdfFormat.PDF_UA_1);

            // Log the boolean result for further processing
            Console.WriteLine($"PDF/UA compliance: {isPdfUaCompliant}");
        }
    }
}