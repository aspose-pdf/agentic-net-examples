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
            Console.Error.WriteLine("One or both input PDF files are missing.");
            return;
        }

        try
        {
            // Load the two PDFs to be compared
            using (Document doc1 = new Document(docPath1))
            using (Document doc2 = new Document(docPath2))
            {
                // Define footer rectangle for the first document (example coordinates)
                // Adjust the coordinates to match the actual footer location.
                Rectangle footerArea1 = new Rectangle(
                    llx: 0,                     // left
                    lly: 0,                     // bottom (footer at bottom of page)
                    urx: doc1.Pages[1].PageInfo.Width, // right (full page width)
                    ury: 50                     // top (height of footer region)
                );

                // Define footer rectangle for the second document (example coordinates)
                Rectangle footerArea2 = new Rectangle(
                    llx: 0,
                    lly: 0,
                    urx: doc2.Pages[1].PageInfo.Width,
                    ury: 50
                );

                // Configure comparison options with the exclude areas
                SideBySideComparisonOptions options = new SideBySideComparisonOptions
                {
                    ExcludeAreas1 = new Rectangle[] { footerArea1 },
                    ExcludeAreas2 = new Rectangle[] { footerArea2 },
                    // Optional: exclude tables if not needed
                    ExcludeTables = false
                };

                // Perform side‑by‑side comparison and save the result
                SideBySidePdfComparer.Compare(doc1, doc2, resultPath, options);
                Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}
