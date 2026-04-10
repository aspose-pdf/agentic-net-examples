// Ensure an empty file named 'AsposePdfApi.sourcelink.json' exists in the project root (can be an empty JSON object: {}).
using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Sanitization;

class PdfSanitizer
{
    // Path to the audit log file
    private readonly string _auditLogPath;

    public PdfSanitizer(string auditLogPath)
    {
        _auditLogPath = auditLogPath;
    }

    // Performs sanitization and logs start/end times
    public void Sanitize(string inputPdfPath, string outputPdfPath)
    {
        // Validate input file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        DateTime startTime = DateTime.UtcNow;
        Log($"Sanitization started for '{inputPdfPath}' at {startTime:O}");

        try
        {
            // Load the PDF document (creation rule)
            using (Document doc = new Document(inputPdfPath))
            {
                // Example sanitization step: enable signature sanitization
                // (this flag is true by default, but setting it explicitly documents intent)
                doc.EnableSignatureSanitization = true;

                // Save the sanitized PDF (save rule)
                doc.Save(outputPdfPath);
            }

            DateTime endTime = DateTime.UtcNow;
            Log($"Sanitization completed for '{inputPdfPath}' at {endTime:O}");
            Log($"Duration: {(endTime - startTime).TotalSeconds:F2} seconds");
        }
        catch (SanitizationException ex)
        {
            DateTime errorTime = DateTime.UtcNow;
            Log($"Sanitization failed for '{inputPdfPath}' at {errorTime:O}");
            Log($"Error: {ex.Message}");
            // Re‑throw if further handling is required
            throw;
        }
        catch (Exception ex)
        {
            DateTime errorTime = DateTime.UtcNow;
            Log($"Unexpected error during sanitization of '{inputPdfPath}' at {errorTime:O}");
            Log($"Error: {ex.Message}");
            throw;
        }
    }

    // Simple helper to append a line to the audit log file
    private void Log(string message)
    {
        try
        {
            File.AppendAllText(_auditLogPath, $"{DateTime.UtcNow:O} - {message}{Environment.NewLine}");
        }
        catch
        {
            // If logging fails, write to console as a fallback
            Console.Error.WriteLine($"Logging failed: {message}");
        }
    }
}

// Example usage
class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "sanitized_output.pdf";
        const string auditLog = "sanitization_audit.log";

        PdfSanitizer sanitizer = new PdfSanitizer(auditLog);
        sanitizer.Sanitize(inputPath, outputPath);

        Console.WriteLine("Sanitization process finished.");
    }
}