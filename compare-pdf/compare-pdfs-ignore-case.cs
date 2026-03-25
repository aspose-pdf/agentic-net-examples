using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string doc1Path = "doc1.pdf";
        const string doc2Path = "doc2.pdf";

        if (!File.Exists(doc1Path) || !File.Exists(doc2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        using (Document doc1 = new Document(doc1Path))
        using (Document doc2 = new Document(doc2Path))
        {
            // Create comparison options – only the documented properties are available.
            ComparisonOptions options = new ComparisonOptions();
            // NOTE: Aspose.Pdf.Comparison does not expose an IgnoreCase property.
            // Case‑insensitive comparison would require preprocessing the PDFs or a different API.

            List<DiffOperation> diffs = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options);
            Console.WriteLine($"Differences found: {diffs.Count}");
        }
    }
}
