using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Path to the input PDF, output PDF and audit log file
    private const string InputPdfPath = "input.pdf";
    private const string OutputPdfPath = "sanitized_output.pdf";
    private const string LogFilePath = "sanitization_audit.log";

    static void Main()
    {
        if (!File.Exists(InputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {InputPdfPath}");
            return;
        }

        // Record the start time of the sanitization operation
        DateTime operationStart = DateTime.UtcNow;
        Log($"Sanitization started at {operationStart:O}");

        try
        {
            // Load, sanitize and save the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(InputPdfPath))
            {
                // ---- Sanitization ----
                // Remove all annotations from every page (fallback when Document.Sanitize() is unavailable)
                foreach (Page page in doc.Pages)
                {
                    page.Annotations.Clear();
                }

                // Remove all embedded files (EmbeddedFileCollection uses 1‑based indexing and has no Clear method)
                if (doc.EmbeddedFiles != null && doc.EmbeddedFiles.Count > 0)
                {
                    for (int i = doc.EmbeddedFiles.Count; i >= 1; i--)
                    {
                        var fileSpec = doc.EmbeddedFiles[i];
                        if (fileSpec != null && !string.IsNullOrEmpty(fileSpec.Name))
                        {
                            doc.EmbeddedFiles.Delete(fileSpec.Name);
                        }
                    }
                }

                // Optional: clear metadata for privacy (do not assign null to non‑nullable DateTime fields)
                doc.Info.Title = string.Empty;
                doc.Info.Author = string.Empty;
                doc.Info.Subject = string.Empty;
                doc.Info.Keywords = string.Empty;
                doc.Info.Creator = string.Empty;
                doc.Info.Producer = string.Empty;
                // CreationDate and ModDate are non‑nullable; setting them to DateTime.MinValue effectively clears them
                doc.Info.CreationDate = DateTime.MinValue;
                doc.Info.ModDate = DateTime.MinValue;

                // Save the sanitized PDF
                doc.Save(OutputPdfPath);
            }
        }
        catch (Exception ex)
        {
            Log($"Sanitization failed: {ex.Message}");
            Console.Error.WriteLine($"Error during sanitization: {ex.Message}");
            return;
        }

        // Record the end time of the sanitization operation
        DateTime operationEnd = DateTime.UtcNow;
        Log($"Sanitization completed at {operationEnd:O}");
        Log($"Duration: {(operationEnd - operationStart).TotalSeconds:F2} seconds");
    }

    // Simple helper that appends a line to the audit log file
    private static void Log(string message)
    {
        try
        {
            File.AppendAllText(LogFilePath, $"{DateTime.UtcNow:O} - {message}{Environment.NewLine}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Logging failed: {ex.Message}");
        }
    }
}
