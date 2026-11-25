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
        const string outputPath = "comparison.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both PDFs inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Configure comparison options: disable additional change marks
            var compareOptions = new SideBySideComparisonOptions
            {
                AdditionalChangeMarks = false
            };

            // Perform side‑by‑side comparison of the whole documents
            // SideBySidePdfComparer is a static class; use its static Compare method.
            SideBySidePdfComparer.Compare(doc1, doc2, outputPath, compareOptions);
        }

        Console.WriteLine($"Side‑by‑side comparison saved to '{outputPath}'.");
    }
}
