using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "doc1.pdf";
        const string pdfPath2 = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("Input PDFs not found.");
            return;
        }

        try
        {
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Comparison options – defaults are sufficient for this demo
                SideBySideComparisonOptions options = new SideBySideComparisonOptions();

                // Perform side‑by‑side comparison; works even when page sizes differ
                SideBySidePdfComparer.Compare(doc1, doc2, resultPath, options);

                Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");

                // Verify the result – the output should contain pages from both documents
                using (Document resultDoc = new Document(resultPath))
                {
                    Console.WriteLine($"Result document contains {resultDoc.Pages.Count} pages.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}