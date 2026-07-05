using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Sanitization; // Contains SanitizationException

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "sanitized_output.pdf";
        const string auditLogPath = "sanitization_audit.log";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document doc = new Document(inputPath))
            {
                // Enable signature sanitization (optional, but relevant to sanitization)
                doc.EnableSignatureSanitization = true;

                // Record start time
                DateTime startTime = DateTime.UtcNow;
                File.AppendAllText(auditLogPath,
                    $"Sanitization started: {startTime:O}{Environment.NewLine}");

                // Perform a sanitization/validation operation.
                // Validate writes its own log; we also capture the result.
                bool validationResult = doc.Validate("validation_log.txt", PdfFormat.PDF_A_1B);

                // Record end time
                DateTime endTime = DateTime.UtcNow;
                TimeSpan duration = endTime - startTime;

                File.AppendAllText(auditLogPath,
                    $"Sanitization ended: {endTime:O}{Environment.NewLine}");
                File.AppendAllText(auditLogPath,
                    $"Duration (seconds): {duration.TotalSeconds:F2}{Environment.NewLine}");
                File.AppendAllText(auditLogPath,
                    $"Validation result: {validationResult}{Environment.NewLine}");

                // Save the (potentially) sanitized PDF (lifecycle rule: use Save without extra options)
                doc.Save(outputPath);
            }

            Console.WriteLine("PDF sanitization completed successfully.");
        }
        catch (SanitizationException ex)
        {
            // Log sanitization-specific failures
            File.AppendAllText(auditLogPath,
                $"SanitizationException: {ex.Message}{Environment.NewLine}");
            Console.Error.WriteLine($"Sanitization failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Log any other unexpected errors
            File.AppendAllText(auditLogPath,
                $"Unexpected error: {ex.Message}{Environment.NewLine}");
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}