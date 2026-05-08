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
        const string auditLogPath  = "audit.txt";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files are missing.");
            return;
        }

        // Load the two documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Set up comparison options (default configuration)
            ComparisonOptions options = new ComparisonOptions();

            // Perform page‑by‑page text comparison
            List<List<DiffOperation>> diffByPage = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);

            // Write audit information: operation type and page number
            using (StreamWriter writer = new StreamWriter(auditLogPath, false))
            {
                for (int pageIdx = 0; pageIdx < diffByPage.Count; pageIdx++)
                {
                    int pageNumber = pageIdx + 1; // pages are 1‑based
                    List<DiffOperation> diffs = diffByPage[pageIdx];

                    foreach (DiffOperation diff in diffs)
                    {
                        // DiffOperation.Operation is an enum; no need for null‑conditional operator
                        string operationType = diff.Operation.ToString();
                        writer.WriteLine($"Operation: {operationType}, Page: {pageNumber}");
                    }
                }
            }

            Console.WriteLine($"Audit log written to '{auditLogPath}'.");
        }
    }
}