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
        const string resultPdfPath = "comparison_result.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Define the rectangular areas to exclude from each document.
        // Rectangle constructor: (llx, lly, urx, ury)
        Aspose.Pdf.Rectangle[] excludeAreasFirst = new Aspose.Pdf.Rectangle[]
        {
            new Aspose.Pdf.Rectangle(100, 500, 300, 600) // example area on first PDF
        };

        Aspose.Pdf.Rectangle[] excludeAreasSecond = new Aspose.Pdf.Rectangle[]
        {
            new Aspose.Pdf.Rectangle(50, 400, 250, 500) // example area on second PDF
        };

        // Configure comparison options with the exclusion areas.
        var compareOptions = new SideBySideComparisonOptions
        {
            ExcludeAreas1 = excludeAreasFirst,
            ExcludeAreas2 = excludeAreasSecond
            // Additional options can be set here, e.g., ExcludeTables = true;
        };

        // Load the PDFs and perform side‑by‑side comparison.
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            SideBySidePdfComparer.Compare(doc1, doc2, resultPdfPath, compareOptions);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
    }
}