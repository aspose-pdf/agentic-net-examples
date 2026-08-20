using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Devices; // needed for Resolution struct

class PdfComparisonDemo
{
    static void Main()
    {
        // Input PDF files (different page sizes)
        const string pdfPath1 = "documentA.pdf";
        const string pdfPath2 = "documentB.pdf";
        const string resultPath = "comparisonResult.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files are missing.");
            return;
        }

        try
        {
            // Load the first PDF
            using (Document doc1 = new Document(pdfPath1))
            // Load the second PDF
            using (Document doc2 = new Document(pdfPath2))
            {
                // Ensure both documents have at least one page
                if (doc1.Pages.Count == 0 || doc2.Pages.Count == 0)
                {
                    Console.Error.WriteLine("One of the documents has no pages.");
                    return;
                }

                // Align page sizes: make the first page of doc2 match the size of the first page of doc1
                // This avoids ArgumentException thrown by the comparer when sizes differ.
                Page page1 = doc1.Pages[1];
                Page page2 = doc2.Pages[1];

                // Copy page dimensions (MediaBox) from page1 to page2
                page2.PageInfo = page1.PageInfo;

                // Create the graphical comparer
                GraphicalPdfComparer comparer = new GraphicalPdfComparer
                {
                    // Example: set a custom change flag color (optional)
                    Color = Aspose.Pdf.Color.Red,
                    // Example: increase resolution for finer detection (optional)
                    Resolution = new Resolution(200), // correct type
                    // Example: ignore tiny differences below 1%
                    Threshold = 1
                };

                // Perform the comparison and save the result PDF
                comparer.ComparePagesToPdf(page1, page2, resultPath);

                Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
            }
        }
        catch (ArgumentException ex)
        {
            // This block catches size‑mismatch errors if alignment logic is insufficient
            Console.Error.WriteLine($"Argument error during comparison: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
