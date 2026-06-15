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
            Console.Error.WriteLine("One or both input files are missing.");
            return;
        }

        // Define rectangular areas to exclude from each document (coordinates: llx, lly, urx, ury)
        Rectangle[] excludeFromFirst = new Rectangle[]
        {
            new Rectangle(100, 500, 300, 600), // example area
            new Rectangle(400, 200, 500, 300)  // another area
        };

        Rectangle[] excludeFromSecond = new Rectangle[]
        {
            new Rectangle(50, 400, 250, 550)   // example area
        };

        // Set up side‑by‑side comparison options
        SideBySideComparisonOptions options = new SideBySideComparisonOptions
        {
            ExcludeTables = false // optional, default is false
        };

        // Assign the excluded rectangle collections using the correct properties
        options.ExcludeAreas1 = excludeFromFirst;
        options.ExcludeAreas2 = excludeFromSecond;

        // Load both documents and perform side‑by‑side comparison
        using (Document doc1 = new Document(docPath1))
        using (Document doc2 = new Document(docPath2))
        {
            SideBySidePdfComparer.Compare(doc1, doc2, resultPath, options);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
    }
}
