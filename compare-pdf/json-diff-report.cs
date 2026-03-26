using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string originalPdfPath = "original.pdf";
        const string modifiedPdfPath = "modified.pdf";
        const string reportPath = "diff_report.json";

        if (!File.Exists(originalPdfPath) || !File.Exists(modifiedPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        using (Document originalDoc = new Document(originalPdfPath))
        using (Document modifiedDoc = new Document(modifiedPdfPath))
        {
            // Use the non‑static TextPdfComparer to obtain diff operations.
            var diffs = TextPdfComparer.CompareDocumentsPageByPage(
                originalDoc,
                modifiedDoc,
                new ComparisonOptions()
            );

            // JsonDiffOutputGenerator can write the JSON directly to a file.
            var jsonGenerator = new JsonDiffOutputGenerator();
            jsonGenerator.GenerateOutput(diffs, reportPath);

            Console.WriteLine($"Diff report saved to '{reportPath}'.");
        }
    }
}
