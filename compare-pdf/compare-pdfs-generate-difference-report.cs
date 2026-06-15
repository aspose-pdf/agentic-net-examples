using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "document1.pdf";
        const string pdfPath2 = "document2.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        if (!File.Exists(pdfPath1))
        {
            Console.Error.WriteLine($"File not found: {pdfPath1}");
            return;
        }
        if (!File.Exists(pdfPath2))
        {
            Console.Error.WriteLine($"File not found: {pdfPath2}");
            return;
        }

        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Configure comparison options. Text comparison is enabled by default.
            ComparisonOptions options = new ComparisonOptions
            {
                ExcludeTables = false,
                ExtractionArea = null
            };

            // Perform a flat (whole‑document) comparison. The overload with a result path also creates a PDF showing the differences.
            List<DiffOperation> differences = TextPdfComparer.CompareFlatDocuments(
                doc1,
                doc2,
                options,
                resultPdfPath);

            Console.WriteLine($"Total differences found: {differences.Count}");
            foreach (DiffOperation diff in differences)
            {
                // DiffOperation does not expose PageNumber or Offset properties. Use its string representation which includes operation type, page index, and text details.
                Console.WriteLine(diff.ToString());
            }
        }

        Console.WriteLine("Comparison completed.");
    }
}
