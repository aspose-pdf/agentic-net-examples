using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string logFilePath   = "comparison_audit.txt";

        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }

        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        try
        {
            // Load the two PDF documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Set up comparison options (default options are sufficient for a basic diff)
                ComparisonOptions options = new ComparisonOptions();

                // Perform page‑by‑page text comparison.
                // The result is a list where each element corresponds to a page
                // and contains the list of DiffOperation objects for that page.
                List<List<DiffOperation>> diffByPage = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);

                // Write the audit log.
                using (StreamWriter writer = new StreamWriter(logFilePath, false))
                {
                    // Pages are 1‑based in Aspose.Pdf, so start counting from 1.
                    for (int pageIndex = 0; pageIndex < diffByPage.Count; pageIndex++)
                    {
                        int pageNumber = pageIndex + 1;
                        List<DiffOperation> diffs = diffByPage[pageIndex];

                        // If there are no differences on this page, optionally note it.
                        if (diffs == null || diffs.Count == 0)
                        {
                            writer.WriteLine($"Page {pageNumber}: No differences detected.");
                            continue;
                        }

                        // Log each diff operation type for the current page.
                        foreach (DiffOperation diff in diffs)
                        {
                            // DiffOperation exposes an Operation property that indicates the type of change.
                            // The enum value is converted to its name for readability.
                            writer.WriteLine($"Page {pageNumber}: {diff.Operation}");
                        }
                    }
                }

                Console.WriteLine($"Comparison audit written to '{logFilePath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}