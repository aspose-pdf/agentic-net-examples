using System;
using System.Collections.Generic;
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

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Define areas to exclude from comparison on the first document.
        // Rectangle coordinates are in points (1/72 inch) and use 1‑based page indexing.
        var excludeAreas1 = new List<Aspose.Pdf.Rectangle>
        {
            // Example: exclude a header region on page 1 (llx, lly, urx, ury)
            new Aspose.Pdf.Rectangle(0, 750, 595, 842) // top margin of a standard A4 page
        };

        // No exclusion areas for the second document in this example.
        var excludeAreas2 = new List<Aspose.Pdf.Rectangle>();

        // Prepare comparison options and assign the exclusion lists (as arrays).
        var comparisonOptions = new ComparisonOptions
        {
            ExcludeAreas1 = excludeAreas1.ToArray(),
            ExcludeAreas2 = excludeAreas2.ToArray()
        };

        // Load both PDFs inside using blocks for deterministic disposal.
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Perform page‑by‑page text comparison.
            // The method returns a list of diff operations per page.
            List<List<DiffOperation>> diffs = TextPdfComparer.CompareDocumentsPageByPage(
                doc1,
                doc2,
                comparisonOptions);

            // Generate JSON diff output.
            var jsonGenerator = new JsonDiffOutputGenerator();
            jsonGenerator.GenerateOutput(diffs, diffJsonPath);
        }

        Console.WriteLine($"Comparison completed. Diff saved to '{diffJsonPath}'.");
    }
}
