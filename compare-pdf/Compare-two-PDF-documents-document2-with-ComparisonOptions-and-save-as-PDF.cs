using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the source PDFs and the output comparison PDF
        const string doc1Path = "document1.pdf";
        const string doc2Path = "document2.pdf";
        const string resultPath = "comparison_result.pdf";

        // Verify that both input files exist
        if (!File.Exists(doc1Path) || !File.Exists(doc2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(doc1Path))
        using (Document doc2 = new Document(doc2Path))
        {
            // Create comparison options – you can customize properties as needed
            ComparisonOptions options = new ComparisonOptions();

            // Example customization (optional):
            // options.ExcludeTables = true; // ignore table differences

            // Perform the comparison and save the visual result as a PDF.
            // This overload writes the result directly to the specified file.
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPath);
        }

        Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
    }
}