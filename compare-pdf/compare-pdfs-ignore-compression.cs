using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "original_compressed.pdf";
        const string pdfPath2 = "recompressed.pdf";
        const string diffResultPdf = "diff_output.pdf";
        const string diffJson = "diff_output.json";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load the two PDFs (different compression settings are irrelevant for text comparison)
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Default comparison options compare text only; compression differences are ignored.
                ComparisonOptions options = new ComparisonOptions();

                // Perform a flat (whole‑document) text comparison and save a visual diff PDF.
                List<DiffOperation> diffs = TextPdfComparer.CompareFlatDocuments(doc1, doc2, options, diffResultPdf);

                Console.WriteLine($"Text differences detected: {diffs.Count}");

                // Optionally output the diff information as JSON.
                JsonDiffOutputGenerator jsonGenerator = new JsonDiffOutputGenerator();
                jsonGenerator.GenerateOutput(diffs, diffJson);
                Console.WriteLine($"JSON diff saved to '{diffJson}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Comparison failed: {ex.Message}");
        }
    }
}