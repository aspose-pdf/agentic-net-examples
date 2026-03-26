using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Path for the audit log that records start/end times of sanitization operations
    private const string AuditLogPath = "sanitization_audit.log";

    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "sanitized.pdf";
        const string validationLogPath = "sanitization.log"; // existing validation log

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Enable signature sanitization (default is true)
                doc.EnableSignatureSanitization = true;

                DateTime startTime = DateTime.UtcNow;
                Console.WriteLine($"Sanitization started at {startTime:O}");
                AppendAuditLog($"Sanitization started at {startTime:O}\n");

                // Perform validation which also records sanitization details
                bool validationResult = doc.Validate(validationLogPath, PdfFormat.PDF_A_1B);

                DateTime endTime = DateTime.UtcNow;
                Console.WriteLine($"Sanitization ended at {endTime:O}");
                AppendAuditLog($"Sanitization ended at {endTime:O}\n");
                Console.WriteLine($"Validation result: {validationResult}");

                doc.Save(outputPath);
                Console.WriteLine($"Sanitized PDF saved as '{outputPath}'.");
            }
        }
        // Aspose.Pdf does not expose a specific SanitizationException; use the generic PdfException instead
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"Sanitization failed: {ex.Message}");
            AppendAuditLog($"Sanitization failed: {ex.Message}\n");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            AppendAuditLog($"Error: {ex.Message}\n");
        }
    }

    /// <summary>
    /// Appends a line to the audit log file. The method creates the file if it does not exist.
    /// </summary>
    private static void AppendAuditLog(string line)
    {
        try
        {
            File.AppendAllText(AuditLogPath, line);
        }
        catch
        {
            // Swallow any I/O errors to avoid breaking the main workflow; they are already reported to console.
        }
    }
}
