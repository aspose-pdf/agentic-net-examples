using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input files not found.");
            return;
        }

        // Load the two documents inside using blocks for deterministic disposal.
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Define footer areas to be excluded from comparison.
            // Adjust the coordinates (llx, lly, urx, ury) to match the actual footer location.
            // Example: a footer spanning the full width at the bottom 50 points of the page.
            Aspose.Pdf.Rectangle footerArea = new Aspose.Pdf.Rectangle(0, 0, doc1.Pages[1].PageInfo.Width, 50);

            // Create side‑by‑side comparison options and assign the exclude areas for both documents.
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                // Exclude the same footer region from both documents.
                ExcludeAreas1 = new[] { footerArea },
                ExcludeAreas2 = new[] { footerArea },

                // Optional: keep tables in the comparison (default is false).
                ExcludeTables = false
            };

            // Perform the side‑by‑side comparison and save the result.
            SideBySidePdfComparer.Compare(doc1, doc2, resultPdfPath, options);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
    }
}