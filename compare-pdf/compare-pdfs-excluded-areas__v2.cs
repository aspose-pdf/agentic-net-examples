using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath = "doc1.pdf";
        const string secondPdfPath = "doc2.pdf";
        const string resultPdfPath = "comparison.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Define rectangular areas to exclude from the first document
        Aspose.Pdf.Rectangle[] excludedFirst = new Aspose.Pdf.Rectangle[]
        {
            new Aspose.Pdf.Rectangle(100.0, 500.0, 300.0, 700.0) // left, bottom, right, top
        };

        // Define rectangular areas to exclude from the second document
        Aspose.Pdf.Rectangle[] excludedSecond = new Aspose.Pdf.Rectangle[]
        {
            new Aspose.Pdf.Rectangle(50.0, 400.0, 250.0, 600.0)
        };

        // Set up comparison options with the excluded areas
        ComparisonOptions options = new ComparisonOptions();
        options.ExcludeAreas1 = excludedFirst;
        options.ExcludeAreas2 = excludedSecond;
        // options.ExcludeTables = true; // optional

        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Perform page‑by‑page comparison and save the visual result
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPdfPath);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
    }
}