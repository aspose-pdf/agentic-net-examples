using System;
using System.IO;
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

        // 1. Side‑by‑side visual comparison
        CompareSideBySide(doc1Path, doc2Path, "SideBySideResult.pdf");

        // 2. Text‑only comparison (highlights textual differences)
        CompareText(doc1Path, doc2Path, "TextComparisonResult.pdf");

        // 3. Graphical comparison (highlights visual changes)
        CompareGraphical(doc1Path, doc2Path, "GraphicalComparisonResult.pdf");
    }

    static void CompareSideBySide(string path1, string path2, string resultPath)
    {
        using (Document doc1 = new Document(path1))
        using (Document doc2 = new Document(path2))
        {
            var options = new SideBySideComparisonOptions
            {
                // Show change marks that appear on other pages as well
                AdditionalChangeMarks = true
            };

            // The static Compare method creates a new PDF that contains pages from both documents
            SideBySidePdfComparer.Compare(doc1, doc2, resultPath, options);
        }

        Console.WriteLine($"Side‑by‑side comparison saved to '{resultPath}'.");
    }

    static void CompareText(string path1, string path2, string resultPath)
    {
        using (Document doc1 = new Document(path1))
        using (Document doc2 = new Document(path2))
        {
            var options = new ComparisonOptions
            {
                // Example setting – do not exclude tables from the comparison
                ExcludeTables = false
            };

            // Perform a page‑by‑page text comparison and write the diff PDF
            // The method returns a list of changes; the PDF is already saved.
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPath);
        }

        Console.WriteLine($"Text comparison saved to '{resultPath}'.");
    }

    static void CompareGraphical(string path1, string path2, string resultPath)
    {
        using (Document doc1 = new Document(path1))
        using (Document doc2 = new Document(path2))
        {
            var comparer = new GraphicalPdfComparer
            {
                // Customize appearance of change markers
                Color = Aspose.Pdf.Color.Red,
                Threshold = 0 // No threshold – all differences are reported
            };

            // Generate a PDF that highlights graphical differences
            comparer.CompareDocumentsToPdf(doc1, doc2, resultPath);
        }

        Console.WriteLine($"Graphical comparison saved to '{resultPath}'.");
    }
}