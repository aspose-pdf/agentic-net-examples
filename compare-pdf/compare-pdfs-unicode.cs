using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdf1Path = "doc_en.pdf";
        const string pdf2Path = "doc_ru.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(pdf1Path) || !File.Exists(pdf2Path))
        {
            Console.Error.WriteLine("Input PDF files not found.");
            return;
        }

        try
        {
            using (Document doc1 = new Document(pdf1Path))
            using (Document doc2 = new Document(pdf2Path))
            {
                // ComparisonOptions does not expose EnableTextComparison – text comparison is enabled by default.
                ComparisonOptions options = new ComparisonOptions();
                // If a Unicode flag exists in a newer version it can be set here, otherwise the default handles Unicode.
                // options.EnableUnicode = true;

                // Perform a flat (whole‑document) comparison and save a visual diff PDF.
                List<DiffOperation> diffs = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options, resultPath);

                Console.WriteLine($"Found {diffs.Count} differences.");
                foreach (DiffOperation diff in diffs)
                {
                    // DiffOperation provides an Operation string and the affected Text.
                    // Page information is not part of DiffOperation for flat comparison; use ToString() as a fallback.
                    string output = $"{diff.Operation} – \"{diff.Text}\"";
                    Console.WriteLine(output);
                }

                Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
