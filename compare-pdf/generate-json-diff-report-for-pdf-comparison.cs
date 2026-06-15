using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string jsonReportPath = "diffReport.json";

        // Verify input files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDF documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Obtain diff operations using the TextPdfComparer (returns a collection of DiffOperation objects)
            var diffs = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, new ComparisonOptions());

            // Generate JSON representation of the differences and write directly to a file
            var jsonGenerator = new JsonDiffOutputGenerator();
            jsonGenerator.GenerateOutput(diffs, jsonReportPath);

            Console.WriteLine($"Diff report saved to '{jsonReportPath}'.");
        }
    }
}