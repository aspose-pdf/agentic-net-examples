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
        const string outputPdfPath = "comparison_result.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the PDF documents
        using (Document firstDoc = new Document(firstPdfPath))
        using (Document secondDoc = new Document(secondPdfPath))
        {
            // Configure side‑by‑side comparison options with excluded rectangular areas
            var sideBySideOptions = new SideBySideComparisonOptions
            {
                ExcludeAreas1 = new[] { new Rectangle(100, 500, 300, 600) },
                ExcludeAreas2 = new[] { new Rectangle(120, 520, 320, 620) }
            };

            // Perform visual comparison; the method writes the result PDF to the specified path
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, outputPdfPath, sideBySideOptions);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{outputPdfPath}'.");
    }
}
