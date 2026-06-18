using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class FormDataComparer
{
    static void Main()
    {
        // Input PDF files containing form fields
        const string pdfPath1 = "form1.pdf";
        const string pdfPath2 = "form2.pdf";

        // Output files for the diff report
        const string jsonReportPath = "form_diff_report.json";
        const string pdfReportPath  = "form_diff_report.pdf";

        // Verify that both input files exist
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

        try
        {
            // Load the two PDF documents
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Configure comparison options (default options are sufficient for form data)
                ComparisonOptions options = new ComparisonOptions();

                // Perform a page‑by‑page text comparison.
                // The method returns a list of diff operations for each page.
                var pageDiffs = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);

                // Generate a JSON diff report
                JsonDiffOutputGenerator jsonGenerator = new JsonDiffOutputGenerator();
                jsonGenerator.GenerateOutput(pageDiffs, jsonReportPath);

                // Generate a PDF diff report (visual highlighting of mismatched values)
                PdfOutputGenerator pdfGenerator = new PdfOutputGenerator();
                pdfGenerator.GenerateOutput(pageDiffs, pdfReportPath);
            }

            Console.WriteLine($"Diff reports generated:");
            Console.WriteLine($"  JSON: {jsonReportPath}");
            Console.WriteLine($"  PDF : {pdfReportPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}