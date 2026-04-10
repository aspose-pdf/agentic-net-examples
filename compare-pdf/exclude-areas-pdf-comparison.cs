using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string outputPdfPath = "comparison_result.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input files are missing.");
            return;
        }

        // Load the two documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Define rectangular areas to exclude from the first document
            Rectangle[] excludeFromFirst = new Rectangle[]
            {
                // Example rectangle: lower‑left (100,100), upper‑right (200,200)
                new Rectangle(100, 100, 200, 200),
                // Add more rectangles as needed
            };

            // Define rectangular areas to exclude from the second document
            Rectangle[] excludeFromSecond = new Rectangle[]
            {
                new Rectangle(300, 400, 450, 550)
                // Add more rectangles as needed
            };

            // Configure side‑by‑side comparison options with the excluded areas
            var options = new SideBySideComparisonOptions
            {
                ExcludeAreas1 = excludeFromFirst,
                ExcludeAreas2 = excludeFromSecond,
                // Additional options can be set here, e.g., HighlightColor = Color.Yellow;
            };

            // Perform the side‑by‑side comparison – the result is saved to a PDF file
            SideBySidePdfComparer.Compare(doc1, doc2, outputPdfPath, options);

            Console.WriteLine($"Compared {doc1.Pages.Count} pages with {doc2.Pages.Count} pages.");
            Console.WriteLine($"Comparison PDF saved to '{outputPdfPath}'.");
        }
    }
}
