using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdf = "doc1.pdf";
        const string secondPdf = "doc2.pdf";
        const string logFile = "comparison_log.txt";

        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            using (Document doc1 = new Document(firstPdf))
            using (Document doc2 = new Document(secondPdf))
            {
                // Comparison options – can be customized as needed
                ComparisonOptions options = new ComparisonOptions();

                // Perform page‑by‑page text comparison
                List<List<DiffOperation>> diffs = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);

                // Write each diff operation and its page number to a text file
                using (StreamWriter writer = new StreamWriter(logFile, false))
                {
                    for (int i = 0; i < diffs.Count; i++)
                    {
                        int pageNumber = i + 1; // 1‑based page indexing
                        foreach (DiffOperation op in diffs[i])
                        {
                            writer.WriteLine($"Page {pageNumber}: {op.Operation}");
                        }
                    }
                }

                Console.WriteLine($"Comparison log saved to '{logFile}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}