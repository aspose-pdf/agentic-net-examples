using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // PDF to be validated
        const string logFilePath  = "validation_log.txt"; // Where validation messages will be written

        // Verify the source file exists before proceeding
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Validate the document against PDF/A‑1B. The method returns true if the
                // validation process completed without internal errors (not necessarily compliant).
                // All non‑conformant elements are written to the specified log file.
                bool validationCompleted = pdfDoc.Validate(logFilePath, PdfFormat.PDF_A_1B);

                // The IsPdfaCompliant property indicates whether the document actually conforms.
                Console.WriteLine($"Validation completed: {(validationCompleted ? "Yes" : "No")}");
                Console.WriteLine($"PDF/A‑1B compliant: {pdfDoc.IsPdfaCompliant}");

                // Output the contents of the log file for inspection
                if (File.Exists(logFilePath))
                {
                    Console.WriteLine("\n--- Validation Log ---");
                    Console.WriteLine(File.ReadAllText(logFilePath));
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors (e.g., corrupted PDF, I/O issues)
            Console.Error.WriteLine($"Error during validation: {ex.Message}");
        }
    }
}