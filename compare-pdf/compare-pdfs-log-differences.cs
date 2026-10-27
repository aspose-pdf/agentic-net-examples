using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdf1Path = "doc1.pdf";
        const string pdf2Path = "doc2.pdf";
        const string logPath = "comparison_log.txt";

        if (!File.Exists(pdf1Path) || !File.Exists(pdf2Path))
        {
            Console.Error.WriteLine("One or both PDF files not found.");
            return;
        }

        try
        {
            // Load the two PDF documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(pdf1Path))
            using (Document doc2 = new Document(pdf2Path))
            {
                // Perform page‑by‑page text comparison – returns a list per page
                var pageDifferences = TextPdfComparer.CompareDocumentsPageByPage(
                    doc1,
                    doc2,
                    new ComparisonOptions()
                );

                bool anyDiff = false;
                using (StreamWriter writer = new StreamWriter(logPath, false))
                {
                    if (pageDifferences == null || pageDifferences.Count == 0)
                    {
                        writer.WriteLine("No differences found.");
                    }
                    else
                    {
                        for (int pageIndex = 0; pageIndex < pageDifferences.Count; pageIndex++)
                        {
                            List<DiffOperation> diffs = pageDifferences[pageIndex];
                            if (diffs == null) continue;

                            foreach (DiffOperation diff in diffs)
                            {
                                anyDiff = true;
                                // DiffOperation.Operation holds the type of change (e.g., "Text", "Font", etc.)
                                writer.WriteLine($"Page {pageIndex + 1}: {diff.Operation}");
                            }
                        }

                        if (!anyDiff)
                        {
                            writer.WriteLine("No differences found.");
                        }
                    }
                }

                Console.WriteLine($"Comparison completed. Log saved to '{logPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
