using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfComparisonExample
{
    static void Main()
    {
        // Input PDF files
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        // Output file that will contain the side‑by‑side comparison result
        const string outputPdfPath = "comparison_result.pdf";

        // Verify that both input files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        // Load the PDF documents
        Document firstDoc = new Document(firstPdfPath);
        Document secondDoc = new Document(secondPdfPath);

        // Define the rectangular area that covers the footer region.
        // Coordinates are in points (1/72 inch). The rectangle below
        // spans the full width of a typical A4 page (595 pt) and the
        // bottom 50 pt of the page.
        Rectangle footerArea = new Rectangle(0, 0, 595, 50);

        // Configure comparison options and add the footer rectangle to the
        // excluded‑areas collections for each document.
        SideBySideComparisonOptions compareOptions = new SideBySideComparisonOptions();
        compareOptions.ExcludeAreas1 = new[] { footerArea };
        compareOptions.ExcludeAreas2 = new[] { footerArea };

        // Perform the side‑by‑side visual comparison. The comparer is a static
        // class; its Compare method writes the result directly to the target
        // PDF file.
        SideBySidePdfComparer.Compare(firstDoc, secondDoc, outputPdfPath, compareOptions);

        Console.WriteLine($"Comparison completed. Result saved to '{outputPdfPath}'.");
    }
}
