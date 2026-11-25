using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";
        const string diffJsonPath = "diff.json";

        // Verify input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both PDF files were not found.");
            return;
        }

        // Load both PDFs inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Text‑based page‑by‑page comparison requires a ComparisonOptions instance
            var options = new ComparisonOptions();
            var diffs = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);

            // Generate JSON diff report directly to file
            var jsonGenerator = new JsonDiffOutputGenerator();
            jsonGenerator.GenerateOutput(diffs, diffJsonPath);
        }

        Console.WriteLine($"Diff JSON saved to '{diffJsonPath}'.");
    }
}
