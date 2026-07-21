using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "form1.pdf";
        const string pdfPath2 = "form2.pdf";
        const string jsonReportPath = "formDiffReport.json";
        const string pdfReportPath = "formDiffReport.pdf";

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

        try
        {
            // Load the two PDF documents (using statements ensure proper disposal)
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Create default comparison options
                ComparisonOptions options = new ComparisonOptions();

                // Compare the documents page by page; returns a list of diff operations per page
                var diff = TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options);

                // Generate a JSON diff report
                JsonDiffOutputGenerator jsonGen = new JsonDiffOutputGenerator();
                jsonGen.GenerateOutput(diff, jsonReportPath);
                Console.WriteLine($"JSON diff report saved to '{jsonReportPath}'.");

                // Generate a visual PDF diff report (optional)
                PdfOutputGenerator pdfGen = new PdfOutputGenerator();
                pdfGen.GenerateOutput(diff, pdfReportPath);
                Console.WriteLine($"PDF diff report saved to '{pdfReportPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}