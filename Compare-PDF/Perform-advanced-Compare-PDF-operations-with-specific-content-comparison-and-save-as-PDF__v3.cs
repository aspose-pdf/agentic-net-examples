using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "document1.pdf";
        const string secondPdfPath = "document2.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        // Verify that both source files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load the two PDFs inside using blocks to ensure proper disposal
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // -----------------------------------------------------------------
                // 1. Configure content‑specific comparison options
                // -----------------------------------------------------------------
                ComparisonOptions compOptions = new ComparisonOptions
                {
                    // Example: do not compare tables (set to true to exclude them)
                    ExcludeTables = false,

                    // Example: limit comparison to a specific rectangular area
                    // ExtractionArea = new Aspose.Pdf.Rectangle(0, 0, 500, 800),

                    // Example: you can also define exclude areas for each document
                    // ExcludeAreas1 = new System.Collections.Generic.List<Aspose.Pdf.Rectangle>(),
                    // ExcludeAreas2 = new System.Collections.Generic.List<Aspose.Pdf.Rectangle>()
                };

                // -----------------------------------------------------------------
                // 2. Perform a text‑based comparison and obtain the list of differences
                // -----------------------------------------------------------------
                // The method returns a list where each entry corresponds to a page
                // and contains the diff operations for that page.
                var pageDiffs = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, compOptions);

                // -----------------------------------------------------------------
                // 3. Generate a PDF that visualises the textual differences
                // -----------------------------------------------------------------
                PdfOutputGenerator pdfGenerator = new PdfOutputGenerator();
                pdfGenerator.GenerateOutput(pageDiffs, resultPdfPath);

                // -----------------------------------------------------------------
                // 4. (Optional) Create a side‑by‑side visual comparison PDF
                // -----------------------------------------------------------------
                // This produces a document where pages from the two sources are placed
                // next to each other, useful for a quick visual audit.
                SideBySidePdfComparer.Compare(
                    doc1,
                    doc2,
                    Path.GetFileNameWithoutExtension(resultPdfPath) + "_sidebyside.pdf",
                    new SideBySideComparisonOptions()
                );
            }

            Console.WriteLine($"Text comparison PDF saved to '{resultPdfPath}'.");
            Console.WriteLine("Side‑by‑side comparison PDF also generated.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}