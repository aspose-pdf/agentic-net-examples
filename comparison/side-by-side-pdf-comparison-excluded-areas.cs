using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";
        const string outputPath = "comparison_result.pdf";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input files not found.");
            return;
        }

        // Define exclusion rectangles (example coordinates)
        Aspose.Pdf.Rectangle excludeRect1 = new Aspose.Pdf.Rectangle(50, 700, 550, 750);
        Aspose.Pdf.Rectangle excludeRect2 = new Aspose.Pdf.Rectangle(100, 100, 400, 150);

        // Configure side‑by‑side comparison options
        var compareOptions = new SideBySideComparisonOptions
        {
            // Text comparison is performed by default; no CompareText property exists.
            ExcludeAreas1 = new[] { excludeRect1, excludeRect2 }
        };

        try
        {
            // Load the source documents
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Perform comparison and save the side‑by‑side PDF directly
                SideBySidePdfComparer.Compare(doc1, doc2, outputPath, compareOptions);
            }

            Console.WriteLine($"Comparison PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}
