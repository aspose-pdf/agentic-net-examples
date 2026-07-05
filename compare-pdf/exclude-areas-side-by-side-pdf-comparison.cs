using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input PDF files
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";

        // Output file that will contain the side‑by‑side comparison result
        const string resultPdfPath = "comparison_result.pdf";

        // Verify that both source files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Define rectangular areas to be excluded from the comparison.
        // Coordinates are: lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y
        Aspose.Pdf.Rectangle[] excludeFromFirst = new Aspose.Pdf.Rectangle[]
        {
            new Aspose.Pdf.Rectangle(100, 500, 300, 600) // example area in the first document
        };

        Aspose.Pdf.Rectangle[] excludeFromSecond = new Aspose.Pdf.Rectangle[]
        {
            new Aspose.Pdf.Rectangle(50, 400, 250, 550) // example area in the second document
        };

        // Configure side‑by‑side comparison options.
        // Use the correct property names: ExcludeAreas1 and ExcludeAreas2.
        SideBySideComparisonOptions compareOptions = new SideBySideComparisonOptions
        {
            ExcludeAreas1 = excludeFromFirst,
            ExcludeAreas2 = excludeFromSecond,
            // Optional: set to true if you also want to ignore table differences
            ExcludeTables = false
        };

        // Load both documents inside using blocks to ensure proper disposal.
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Perform the comparison and save the visual result.
            SideBySidePdfComparer.Compare(doc1, doc2, resultPdfPath, compareOptions);
        }

        Console.WriteLine($"Side‑by‑side comparison saved to '{resultPdfPath}'.");
    }
}