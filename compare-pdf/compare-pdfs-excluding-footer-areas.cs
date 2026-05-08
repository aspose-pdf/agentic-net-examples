using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string docPath1 = "document1.pdf";
        const string docPath2 = "document2.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(docPath1) || !File.Exists(docPath2))
        {
            Console.Error.WriteLine("One or both input files are missing.");
            return;
        }

        // Load the two PDFs inside using blocks for deterministic disposal
        using (Document doc1 = new Document(docPath1))
        using (Document doc2 = new Document(docPath2))
        {
            // Define footer rectangle for the first document (example coordinates)
            // Adjust llx, lly, urx, ury to match the actual footer area on your pages
            Aspose.Pdf.Rectangle footerArea1 = new Aspose.Pdf.Rectangle(0, 0, doc1.Pages[1].PageInfo.Width, 50);
            // Define footer rectangle for the second document (example coordinates)
            Aspose.Pdf.Rectangle footerArea2 = new Aspose.Pdf.Rectangle(0, 0, doc2.Pages[1].PageInfo.Width, 50);

            // Configure side‑by‑side comparison options
            SideBySideComparisonOptions options = new SideBySideComparisonOptions
            {
                // Exclude the defined footer areas from both documents
                ExcludeAreas1 = new[] { footerArea1 },
                ExcludeAreas2 = new[] { footerArea2 },

                // Optional: keep other defaults (e.g., ComparisonMode)
                // ComparisonMode = ComparisonMode.IgnoreSpaces
            };

            // Perform the comparison and save the result
            SideBySidePdfComparer.Compare(doc1, doc2, resultPath, options);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
    }
}