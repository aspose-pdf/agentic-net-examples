using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfUniversalComparer
{
    // Performs a generic comparison of two PDFs and saves the result.
    // Uses TextPdfComparer.CompareFlatDocuments which works for any PDF content,
    // including embedded multimedia, because it compares the whole document text
    // and can also generate a visual diff PDF.
    static void Main()
    {
        const string pdfPath1 = "document1.pdf";
        const string pdfPath2 = "document2.pdf";
        const string resultPath = "comparison_result.pdf";

        // Validate input files.
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load both PDFs inside using blocks for deterministic disposal.
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Configure comparison options as needed.
                ComparisonOptions options = new ComparisonOptions
                {
                    // Example: ignore tables during comparison.
                    ExcludeTables = false,
                    // Example: set extraction area to null (compare whole pages).
                    ExtractionArea = null
                };

                // Perform the comparison and obtain the list of differences.
                // This overload also writes a PDF with visual differences.
                List<DiffOperation> differences = TextPdfComparer.CompareFlatDocuments(
                    doc1,
                    doc2,
                    options,
                    resultPath);

                // Output a simple summary to the console.
                Console.WriteLine($"Comparison completed. Differences found: {differences.Count}");
                Console.WriteLine($"Result PDF saved to '{resultPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}