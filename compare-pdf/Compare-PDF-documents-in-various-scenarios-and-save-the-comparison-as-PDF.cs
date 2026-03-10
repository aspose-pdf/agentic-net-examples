using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Devices; // for Resolution if needed

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Configure side‑by‑side comparison options
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                // Use the static enum value; do NOT access via an instance
                ComparisonMode = ComparisonMode.IgnoreSpaces,

                // Optional: show change marks that appear on other pages
                AdditionalChangeMarks = true
            };

            // Perform the comparison and write the result directly to a PDF file
            SideBySidePdfComparer.Compare(doc1, doc2, resultPdfPath, options);
        }

        Console.WriteLine($"Side‑by‑side comparison PDF saved to '{resultPdfPath}'.");
    }
}