using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string doc1Path = "doc1.pdf";
        const string doc2Path = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(doc1Path) || !File.Exists(doc2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Define a rectangle that covers the footer area (adjust coordinates as needed)
        Aspose.Pdf.Rectangle footerArea = new Aspose.Pdf.Rectangle(0, 0, 595, 50);

        // Set up comparison options with excluded footer areas for both documents
        SideBySideComparisonOptions options = new SideBySideComparisonOptions
        {
            ExcludeAreas1 = new Aspose.Pdf.Rectangle[] { footerArea },
            ExcludeAreas2 = new Aspose.Pdf.Rectangle[] { footerArea }
        };

        // Load the PDFs and perform the side‑by‑side comparison
        using (Document doc1 = new Document(doc1Path))
        using (Document doc2 = new Document(doc2Path))
        {
            SideBySidePdfComparer.Compare(doc1, doc2, resultPath, options);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
    }
}