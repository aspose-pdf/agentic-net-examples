using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string logPath = "diff_log.txt";

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

        ComparisonOptions options = new ComparisonOptions();
        List<List<DiffOperation>> diffByPage;

        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            diffByPage = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);
        }

        using (StreamWriter writer = new StreamWriter(logPath, false))
        {
            for (int pageIndex = 0; pageIndex < diffByPage.Count; pageIndex++)
            {
                List<DiffOperation> pageDiffs = diffByPage[pageIndex];
                int pageNumber = pageIndex + 1; // 1‑based page numbering
                foreach (DiffOperation diff in pageDiffs)
                {
                    string operationName = diff.Operation.ToString();
                    writer.WriteLine($"Page {pageNumber}: {operationName}");
                }
            }
        }

        Console.WriteLine($"Diff log written to '{logPath}'.");
    }
}