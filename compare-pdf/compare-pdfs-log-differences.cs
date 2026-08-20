using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF files
        const string pdfPath1 = "document1.pdf";
        const string pdfPath2 = "document2.pdf";

        // Output audit log file
        const string auditLogPath = "diff_audit.txt";

        // Verify that both source files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDF documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Create default comparison options (can be customized if needed)
            ComparisonOptions options = new ComparisonOptions();

            // Perform a page‑by‑page text comparison.
            // The method returns a list where each element corresponds to a page
            // and contains the DiffOperation objects for that page.
            List<List<DiffOperation>> diffsByPage =
                TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);

            // Prepare lines to be written to the audit log
            var logLines = new List<string>();

            // Iterate over each page's diff list
            for (int i = 0; i < diffsByPage.Count; i++)
            {
                int pageNumber = i + 1; // Aspose.Pdf uses 1‑based page indexing
                List<DiffOperation> pageDiffs = diffsByPage[i];

                foreach (DiffOperation diff in pageDiffs)
                {
                    // DiffOperation provides the type of change via the Operation property
                    string operationType = diff.Operation.ToString();

                    // Log format: "Page {pageNumber}: {operationType}"
                    logLines.Add($"Page {pageNumber}: {operationType}");
                }
            }

            // Write all log entries to the specified text file
            File.WriteAllLines(auditLogPath, logLines);
        }

        Console.WriteLine($"Diff audit completed. Log saved to '{auditLogPath}'.");
    }
}