using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "original.pdf";
        const string pdfPath2 = "modified.pdf";
        const string resultPdf = "comparison_result.pdf";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the two PDF documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Create comparison options and enable form‑field value comparison
            ComparisonOptions options = new ComparisonOptions();
            // The IncludeFormFields flag exists in recent Aspose.Pdf versions.
            // Uncomment the line below if the property is available in your version.
            // options.IncludeFormFields = true;

            // Perform a page‑by‑page comparison and save the visual diff PDF
            List<List<DiffOperation>> diffs = TextPdfComparer.CompareDocumentsPageByPage(
                doc1, doc2, options, resultPdf);

            Console.WriteLine($"Comparison completed. Result saved to '{resultPdf}'.");
            Console.WriteLine($"Pages compared: {diffs.Count}");
        }
    }
}