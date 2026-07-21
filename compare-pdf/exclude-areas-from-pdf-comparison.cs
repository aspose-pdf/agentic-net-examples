using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string docPath1 = "doc1.pdf";
        const string docPath2 = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(docPath1) || !File.Exists(docPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Define rectangular areas to exclude from the first document.
        // Example: exclude a header and a footer region.
        Aspose.Pdf.Rectangle[] excludeFromFirst = new Aspose.Pdf.Rectangle[]
        {
            new Aspose.Pdf.Rectangle(0, 750, 595, 842), // top area
            new Aspose.Pdf.Rectangle(0, 0, 595, 50)    // bottom area
        };

        // Define rectangular areas to exclude from the second document.
        Aspose.Pdf.Rectangle[] excludeFromSecond = new Aspose.Pdf.Rectangle[]
        {
            new Aspose.Pdf.Rectangle(0, 750, 595, 842),
            new Aspose.Pdf.Rectangle(0, 0, 595, 50)
        };

        // Set up comparison options with the excluded areas.
        SideBySideComparisonOptions options = new SideBySideComparisonOptions
        {
            ExcludeAreas1 = excludeFromFirst,
            ExcludeAreas2 = excludeFromSecond
            // ExcludeTables = true; // optional, if tables should be ignored
        };

        // Load both PDFs inside using blocks for deterministic disposal.
        using (Document doc1 = new Document(docPath1))
        using (Document doc2 = new Document(docPath2))
        {
            // Perform side‑by‑side comparison and save the result.
            SideBySidePdfComparer.Compare(doc1, doc2, resultPath, options);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
    }
}