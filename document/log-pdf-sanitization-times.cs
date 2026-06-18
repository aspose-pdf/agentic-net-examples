using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Sanitization;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string logPath   = "sanitization_audit.log";
        const string outputPath = "sanitized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Ensure signature sanitization is enabled (default value)
                doc.EnableSignatureSanitization = true;

                // Record the start time of the sanitization operation
                DateTime startTime = DateTime.UtcNow;
                Console.WriteLine($"Sanitization started at {startTime:O}");

                // Perform a validation which also triggers sanitization logic.
                // The Validate method writes a detailed log to the specified file.
                bool validationResult = doc.Validate(logPath, PdfFormat.PDF_A_1B);

                // Record the end time of the sanitization operation
                DateTime endTime = DateTime.UtcNow;
                Console.WriteLine($"Sanitization ended at {endTime:O}");

                // Output duration and validation result for auditing
                TimeSpan duration = endTime - startTime;
                Console.WriteLine($"Duration: {duration.TotalSeconds:F2} seconds");
                Console.WriteLine($"Validation succeeded: {validationResult}");

                // Save the (potentially) sanitized document
                doc.Save(outputPath);
                Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
            }
        }
        catch (SanitizationException ex)
        {
            // Specific handling for sanitization failures
            Console.Error.WriteLine($"Sanitization failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}