using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the source PDF files and the result files
        const string pdfPath1 = "document1.pdf";
        const string pdfPath2 = "document2.pdf";
        const string sideBySideResult = "comparison_sidebyside.pdf";
        const string graphicalResult = "comparison_graphical.pdf";

        // Verify that both input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDFs inside using blocks to ensure deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // ------------------------------------------------------------
            // 1. Side‑by‑side comparison (text + layout)
            // ------------------------------------------------------------
            // Create default options; you can customize them if needed
            SideBySideComparisonOptions sideOptions = new SideBySideComparisonOptions();

            // Perform the comparison and save the result as a PDF
            SideBySidePdfComparer.Compare(doc1, doc2, sideBySideResult, sideOptions);
            Console.WriteLine($"Side‑by‑side comparison saved to '{sideBySideResult}'.");

            // ------------------------------------------------------------
            // 2. Graphical comparison (detect changes in vector graphics,
            //    ZUGFeRD data streams, JavaScript actions, etc.)
            // ------------------------------------------------------------
            GraphicalPdfComparer graphicalComparer = new GraphicalPdfComparer();

            // Optional visual settings for the diff overlay
            graphicalComparer.Color = Aspose.Pdf.Color.Red; // highlight color
            graphicalComparer.Threshold = 0;                // 0% = detect all differences

            // Compare the documents graphically and save the diff PDF
            graphicalComparer.CompareDocumentsToPdf(doc1, doc2, graphicalResult);
            Console.WriteLine($"Graphical comparison saved to '{graphicalResult}'.");
        }
    }
}