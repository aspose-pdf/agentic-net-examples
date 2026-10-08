using System;
using System.IO;
using Aspose.Pdf;

class PdfAValidatorProgram
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string logFilePath   = "validation_log.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document
        var document = new Document(inputPdfPath);

        // Validate PDF/A‑1B compliance. The Validate method writes a detailed log to the
        // supplied file and returns a boolean indicating overall compliance.
        bool isCompliant = document.Validate(logFilePath, PdfFormat.PDF_A_1B);

        // Write a short outcome to console and prepend it to the log file.
        using (var logWriter = new StreamWriter(logFilePath, true)) // append the summary
        {
            if (isCompliant)
            {
                string msg = "Document is PDF/A‑1B compliant.";
                Console.WriteLine(msg);
                logWriter.WriteLine(msg);
            }
            else
            {
                string msg = "Document is NOT PDF/A‑1B compliant. See detailed log above for non‑conformant elements.";
                Console.WriteLine(msg);
                logWriter.WriteLine(msg);
            }
        }

        Console.WriteLine($"Validation log written to '{logFilePath}'.");
    }
}
