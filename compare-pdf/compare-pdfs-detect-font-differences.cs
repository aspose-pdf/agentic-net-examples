using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdf1Path = "doc1.pdf";
        const string pdf2Path = "doc2.pdf";
        const string diffOutputPath = "diff_output.pdf";

        if (!File.Exists(pdf1Path) || !File.Exists(pdf2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the PDFs
        Document doc1 = new Document(pdf1Path);
        Document doc2 = new Document(pdf2Path);

        // Comparison options – font detection is enabled by default.
        ComparisonOptions compOptions = new ComparisonOptions();

        // Perform textual comparison – returns diff operations per page
        List<List<DiffOperation>> pageDiffs = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, compOptions);

        // Flatten the list to a single collection of diff operations
        List<DiffOperation> allDiffs = pageDiffs.SelectMany(p => p).ToList();

        // Count how many diff operations correspond to font changes.
        // DiffOperation.Operation is an enum; convert it to string for the check.
        int fontDiffCount = allDiffs.Count(d =>
            d.Operation.ToString().IndexOf("Font", StringComparison.OrdinalIgnoreCase) >= 0);

        Console.WriteLine($"Total diff operations: {allDiffs.Count}");
        Console.WriteLine($"Font diff operations: {fontDiffCount}");

        // Optional: generate a side‑by‑side visual comparison PDF
        SideBySideComparisonOptions sideOptions = new SideBySideComparisonOptions();
        SideBySidePdfComparer.Compare(doc1, doc2, diffOutputPath, sideOptions);

        Console.WriteLine($"Diff PDF saved to '{diffOutputPath}'.");
    }
}
