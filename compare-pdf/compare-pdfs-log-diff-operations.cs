using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "doc1.pdf";
        const string pdfPath2 = "doc2.pdf";
        const string logPath  = "diff_audit.txt";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDFs. Using blocks ensure deterministic disposal.
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Default comparison options; customize if needed.
            ComparisonOptions options = new ComparisonOptions();

            // Perform a page‑by‑page text comparison.
            // The result is a list where each inner list contains the diffs for a specific page.
            List<List<DiffOperation>> diffsByPage = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);

            // Write the audit log: operation type and page number.
            using (StreamWriter writer = new StreamWriter(logPath, false))
            {
                for (int i = 0; i < diffsByPage.Count; i++)
                {
                    // The inner list corresponds to a specific page (1‑based index).
                    int pageNumber = i + 1;
                    foreach (DiffOperation diff in diffsByPage[i])
                    {
                        // DiffOperation exposes the type of change via the Operation property.
                        string operation = diff.Operation.ToString();
                        writer.WriteLine($"Page {pageNumber}: {operation}");
                    }
                }
            }

            Console.WriteLine($"Diff audit log created at '{logPath}'.");
        }
    }
}
