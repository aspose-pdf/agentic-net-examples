using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "original.pdf";   // PDF with default compression
        const string pdfPath2 = "compressed.pdf"; // Same content, different compression

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both PDFs inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Comparison options – defaults are sufficient for text‑only comparison
            ComparisonOptions options = new ComparisonOptions();

            // Perform a flat text comparison; compression differences are ignored
            List<DiffOperation> diffs = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options);

            Console.WriteLine($"Text differences found: {diffs?.Count ?? 0}");

            // Optional: list each difference
            if (diffs != null)
            {
                foreach (DiffOperation diff in diffs)
                {
                    Console.WriteLine(diff);
                }
            }
        }
    }
}