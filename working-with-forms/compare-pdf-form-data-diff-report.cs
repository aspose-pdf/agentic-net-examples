using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class FormDataComparer
{
    static void Main()
    {
        // Paths to the source PDF files
        const string pdfPath1 = "form1.pdf";
        const string pdfPath2 = "form2.pdf";

        // Output paths for the diff reports
        const string jsonReportPath = "diffReport.json";
        const string pdfReportPath  = "diffReport.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath1))
        {
            Console.Error.WriteLine($"File not found: {pdfPath1}");
            return;
        }
        if (!File.Exists(pdfPath2))
        {
            Console.Error.WriteLine($"File not found: {pdfPath2}");
            return;
        }

        // Load the two PDF documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Configure comparison options (default options are sufficient for form data)
            ComparisonOptions options = new ComparisonOptions();

            // Perform a page‑by‑page text comparison.
            // The result is a list where each entry corresponds to a page and contains the diff operations.
            List<List<DiffOperation>> diffByPage = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);

            // Generate a JSON diff report
            JsonDiffOutputGenerator jsonGenerator = new JsonDiffOutputGenerator();
            jsonGenerator.GenerateOutput(diffByPage, jsonReportPath);
            Console.WriteLine($"JSON diff report saved to '{jsonReportPath}'.");

            // Generate a PDF diff report that visually highlights mismatched values
            PdfOutputGenerator pdfGenerator = new PdfOutputGenerator();
            pdfGenerator.GenerateOutput(diffByPage, pdfReportPath);
            Console.WriteLine($"PDF diff report saved to '{pdfReportPath}'.");
        }
    }
}