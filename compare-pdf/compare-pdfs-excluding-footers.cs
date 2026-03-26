using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdf = "document1.pdf";
        const string secondPdf = "document2.pdf";
        const string resultPdf = "comparison_result.pdf";

        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        using (Document doc1 = new Document(firstPdf))
        using (Document doc2 = new Document(secondPdf))
        {
            SideBySideComparisonOptions options = new SideBySideComparisonOptions();

            // Define a rectangle that covers the footer area (e.g., bottom 50 points of an A4 page).
            Aspose.Pdf.Rectangle footerArea = new Aspose.Pdf.Rectangle(0, 0, 595, 50);

            // Apply the same exclusion rectangle to both documents.
            options.ExcludeAreas1 = new Aspose.Pdf.Rectangle[] { footerArea };
            options.ExcludeAreas2 = new Aspose.Pdf.Rectangle[] { footerArea };

            // Perform side‑by‑side comparison while ignoring the defined footer regions.
            SideBySidePdfComparer.Compare(doc1, doc2, resultPdf, options);
        }

        Console.WriteLine("Comparison completed. Result saved to " + resultPdf);
    }
}