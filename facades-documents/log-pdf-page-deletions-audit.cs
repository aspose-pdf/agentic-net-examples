using System;
using System.IO;
using Aspose.Pdf.Facades;

class AuditLogger
{
    private readonly string _logFilePath;

    public AuditLogger(string logFilePath)
    {
        _logFilePath = logFilePath;
        // Ensure the log file exists
        if (!File.Exists(_logFilePath))
        {
            using (File.Create(_logFilePath)) { }
        }
    }

    public void Log(string message)
    {
        string entry = $"{DateTime.UtcNow:O} - {message}";
        File.AppendAllText(_logFilePath, entry + Environment.NewLine);
    }
}

class PdfPageDeletionWithAudit
{
    // Deletes the specified pages from a PDF file and logs the operation.
    // Returns true if the deletion succeeded.
    public static bool DeletePages(string inputPdf, int[] pagesToDelete, string outputPdf, AuditLogger logger)
    {
        // Validate input
        if (!File.Exists(inputPdf))
        {
            logger.Log($"Input file not found: '{inputPdf}'. Deletion aborted.");
            return false;
        }

        // The number of pages we intend to remove
        int pagesCount = pagesToDelete?.Length ?? 0;
        logger.Log($"Attempting to delete {pagesCount} page(s) from '{Path.GetFileName(inputPdf)}'.");

        // Perform deletion using PdfFileEditor (facade API)
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.TryDelete(inputPdf, pagesToDelete, outputPdf);

        // Log the outcome
        if (success)
        {
            logger.Log($"Successfully deleted {pagesCount} page(s). Output saved to '{Path.GetFileName(outputPdf)}'.");
        }
        else
        {
            logger.Log($"Deletion failed for '{Path.GetFileName(inputPdf)}'. No output generated.");
        }

        return success;
    }

    static void Main()
    {
        // Example usage
        const string inputPath = "sample.pdf";
        const string outputPath = "sample_trimmed.pdf";
        const string logPath = "deletion_audit.log";

        // Pages to delete (1‑based indexing as required by Aspose.Pdf)
        int[] pagesToRemove = new int[] { 2, 4, 5 };

        AuditLogger logger = new AuditLogger(logPath);

        try
        {
            DeletePages(inputPath, pagesToRemove, outputPath, logger);
        }
        catch (Exception ex)
        {
            logger.Log($"Unexpected error: {ex.Message}");
        }

        Console.WriteLine("Operation completed. Check the audit log for details.");
    }
}