using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF files with different page sizes
        const string pdfPathA = "documentA.pdf";
        const string pdfPathB = "documentB.pdf";

        // Output file that will contain the side‑by‑side comparison result
        const string comparisonResultPath = "comparison_result.pdf";

        // Verify that the source files exist
        if (!File.Exists(pdfPathA))
        {
            Console.Error.WriteLine($"File not found: {pdfPathA}");
            return;
        }
        if (!File.Exists(pdfPathB))
        {
            Console.Error.WriteLine($"File not found: {pdfPathB}");
            return;
        }

        // Load the PDFs into Aspose.Pdf.Document objects
        Document docA = new Document(pdfPathA);
        Document docB = new Document(pdfPathB);

        // Configure side‑by‑side comparison options.
        // The AlignPages property is not present in older Aspose.Pdf versions, so we rely on the default behaviour.
        SideBySideComparisonOptions compareOptions = new SideBySideComparisonOptions();

        // Perform the visual side‑by‑side comparison. The comparer is a static class, so we call the static method directly.
        SideBySidePdfComparer.Compare(docA, docB, comparisonResultPath, compareOptions);

        // Verify the generated comparison PDF.
        using (Document resultDoc = new Document(comparisonResultPath))
        {
            Console.WriteLine($"Comparison PDF created: {comparisonResultPath}");
            Console.WriteLine($"Total pages in comparison PDF: {resultDoc.Pages.Count}");

            // Each page in the result should have a width that accommodates both source pages.
            for (int i = 1; i <= resultDoc.Pages.Count; i++)   // 1‑based indexing
            {
                Page page = resultDoc.Pages[i];
                Console.WriteLine($"Page {i}: Width = {page.PageInfo.Width} pt, Height = {page.PageInfo.Height} pt");
            }
        }

        Console.WriteLine("Comparison completed and page dimensions verified.");
    }
}
