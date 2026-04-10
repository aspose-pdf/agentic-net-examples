using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "document1.pdf";
        const string pdfPath2 = "document2.pdf";
        const string auditLogPath = "diff_audit.txt";

        // Verify input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both PDF files were not found.");
            return;
        }

        // Load the two PDF documents (lifecycle rule: use using for deterministic disposal)
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Create default comparison options
            ComparisonOptions options = new ComparisonOptions();

            // Perform a page‑by‑page text comparison
            // Returns a list where each element corresponds to a page and contains its diff operations
            List<List<DiffOperation>> diffsByPage = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);

            // Write the audit log: operation type and page number
            // Ensure the writer is disposed so the file handle is released before the build process may try to access it again.
            using (StreamWriter writer = new StreamWriter(auditLogPath, false))
            {
                for (int i = 0; i < diffsByPage.Count; i++)
                {
                    int pageNumber = i + 1; // Pages are 1‑based for reporting
                    List<DiffOperation> pageDiffs = diffsByPage[i];

                    foreach (DiffOperation diff in pageDiffs)
                    {
                        // DiffOperation.Operation provides the type of change (Insert, Delete, Replace, etc.)
                        writer.WriteLine($"Page {pageNumber}: {diff.Operation}");
                    }
                }
            }

            Console.WriteLine($"Diff audit log created at '{auditLogPath}'.");
        }
    }
}
