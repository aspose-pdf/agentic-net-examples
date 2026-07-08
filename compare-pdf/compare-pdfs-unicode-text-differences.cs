using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "doc_en.pdf";
        const string pdfPath2 = "doc_ru.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDF documents inside using blocks for deterministic disposal.
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Configure comparison options.
            ComparisonOptions options = new ComparisonOptions
            {
                // Compare the whole page content; no extraction area is set.
                ExtractionArea = null
                // Default ComparisonMode (Normal) correctly handles Unicode characters.
            };

            // Perform a flat (whole‑document) text comparison.
            // The overload with a result path also creates a PDF visualizing the differences.
            List<DiffOperation> diffs = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options, resultPath);

            // Report the detected differences.
            Console.WriteLine($"Unicode text differences detected: {diffs.Count}");
            foreach (DiffOperation diff in diffs)
            {
                // DiffOperation typically provides the type of edit and the affected text.
                Console.WriteLine($"{diff.Operation}: \"{diff.Text}\"");
            }

            Console.WriteLine($"Comparison result PDF saved to '{resultPath}'.");
        }
    }
}