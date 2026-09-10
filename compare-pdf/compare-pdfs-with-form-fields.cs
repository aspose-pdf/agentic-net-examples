using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class PdfFormFieldComparer
{
    static void Main()
    {
        const string pdfPath1 = "original.pdf";
        const string pdfPath2 = "modified.pdf";
        const string diffResultPdf = "comparison_result.pdf";
        const string diffResultJson = "comparison_result.json";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files are missing.");
            return;
        }

        // Load the two PDF documents inside using blocks for deterministic disposal.
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Configure comparison options. Form field values are compared by default in the
            // Aspose.Pdf.Comparison API, so no explicit flag is required.
            ComparisonOptions options = new ComparisonOptions();

            // Perform a page‑by‑page textual comparison.
            List<List<DiffOperation>> diffs = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);

            // Generate a JSON report of the differences.
            JsonDiffOutputGenerator jsonGenerator = new JsonDiffOutputGenerator();
            jsonGenerator.GenerateOutput(diffs, diffResultJson);
            Console.WriteLine($"JSON diff saved to '{diffResultJson}'.");

            // Create a side‑by‑side visual PDF showing the differences.
            SideBySideComparisonOptions visualOptions = new SideBySideComparisonOptions();
            SideBySidePdfComparer.Compare(doc1, doc2, diffResultPdf, visualOptions);
            Console.WriteLine($"Visual diff PDF saved to '{diffResultPdf}'.");
        }
    }
}
