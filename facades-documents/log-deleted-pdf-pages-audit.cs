using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfPageDeletionAuditor
{
    // Path to the audit log file
    private const string AuditLogPath = "deletion_audit.log";

    /// <summary>
    /// Deletes the specified pages from a PDF file using PdfFileEditor.TryDelete
    /// and logs the number of pages actually removed.
    /// </summary>
    /// <param name="inputFile">Path to the source PDF.</param>
    /// <param name="pagesToDelete">Array of page numbers to delete (1‑based).</param>
    /// <param name="outputFile">Path where the resulting PDF will be saved.</param>
    public static void DeletePagesAndLog(string inputFile, int[] pagesToDelete, string outputFile)
    {
        // Ensure the input file exists
        if (!File.Exists(inputFile))
        {
            Console.Error.WriteLine($"Input file not found: {inputFile}");
            return;
        }

        // Get the original page count
        int originalPageCount;
        using (Document srcDoc = new Document(inputFile))
        {
            originalPageCount = srcDoc.Pages.Count;
        }

        // Perform the deletion using PdfFileEditor (facade API)
        PdfFileEditor editor = new PdfFileEditor();
        bool deleteResult = editor.TryDelete(inputFile, pagesToDelete, outputFile);

        // Determine how many pages were actually removed
        int pagesRemoved = 0;
        if (deleteResult)
        {
            // Load the resulting document to obtain the new page count
            int newPageCount;
            using (Document outDoc = new Document(outputFile))
            {
                newPageCount = outDoc.Pages.Count;
            }

            pagesRemoved = originalPageCount - newPageCount;
        }
        else
        {
            // Deletion failed – treat as zero pages removed
            pagesRemoved = 0;
        }

        // Build an audit entry
        string auditEntry = $"{DateTime.UtcNow:u} | Input: {Path.GetFileName(inputFile)} | " +
                            $"DeletedPages: [{string.Join(", ", pagesToDelete)}] | " +
                            $"RemovedCount: {pagesRemoved} | Success: {deleteResult}";

        // Append the entry to the audit log
        try
        {
            File.AppendAllText(AuditLogPath, auditEntry + Environment.NewLine);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write audit log: {ex.Message}");
        }

        // Inform the user
        Console.WriteLine(deleteResult
            ? $"Deleted pages. {pagesRemoved} page(s) removed. Audit logged."
            : "Deletion operation failed. No pages removed.");
    }

    // Example usage
    static void Main()
    {
        // Example: delete pages 2 and 3 from sample.pdf and save as result.pdf
        string inputPdf = "sample.pdf";
        string outputPdf = "result.pdf";
        int[] pagesToRemove = new int[] { 2, 3 };

        DeletePagesAndLog(inputPdf, pagesToRemove, outputPdf);
    }
}