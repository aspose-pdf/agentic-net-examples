using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Prepare a temporary log file for the validation process
                string logPath = Path.Combine(Path.GetTempPath(), "pdfua_validation.log");

                // Validate the document against PDF/UA‑1 compliance.
                // Document.Validate returns a boolean indicating compliance.
                bool isPdfUaCompliant = doc.Validate(logPath, PdfFormat.PDF_UA_1);

                // Log the result for further processing
                Console.WriteLine($"PDF/UA compliance: {isPdfUaCompliant}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
