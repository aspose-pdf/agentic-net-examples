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
            Console.Error.WriteLine("Input PDF files not found.");
            return;
        }

        // Load the two PDF documents
        using (Document doc1 = new Document(docPath1))
        using (Document doc2 = new Document(docPath2))
        {
            // Define rectangular regions to exclude from the first document
            // Rectangle(left, bottom, right, top)
            Aspose.Pdf.Rectangle[] excludeFromFirst = new Aspose.Pdf.Rectangle[]
            {
                new Aspose.Pdf.Rectangle(100, 500, 200, 600), // example area
                new Aspose.Pdf.Rectangle(300, 700, 400, 800)  // another area
            };

            // Define rectangular regions to exclude from the second document
            Aspose.Pdf.Rectangle[] excludeFromSecond = new Aspose.Pdf.Rectangle[]
            {
                new Aspose.Pdf.Rectangle(50, 400, 150, 500) // example area
            };

            // Set up comparison options with the excluded areas
            ComparisonOptions options = new ComparisonOptions
            {
                ExcludeAreas1 = excludeFromFirst,
                ExcludeAreas2 = excludeFromSecond,
                ExcludeTables = false // optional: keep table comparison enabled
            };

            // Perform page‑by‑page comparison and save the visual result PDF
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPath);

            Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
        }
    }
}