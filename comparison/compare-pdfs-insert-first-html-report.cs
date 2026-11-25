using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string leftPdfPath = "left.pdf";
        const string rightPdfPath = "right.pdf";
        const string htmlReportPath = "comparison_report.html";

        if (!File.Exists(leftPdfPath) || !File.Exists(rightPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both PDFs with deterministic disposal
        using (Document leftDoc = new Document(leftPdfPath))
        using (Document rightDoc = new Document(rightPdfPath))
        {
            // Configure comparison to prioritize insert operations
            ComparisonOptions compOptions = new ComparisonOptions
            {
                EditOperationsOrder = EditOperationsOrder.InsertFirst
            };

            // Perform text‑based comparison – returns a list of diff operations per page
            var diffs = TextPdfComparer.CompareDocumentsPageByPage(leftDoc, rightDoc, compOptions);

            // Generate an HTML report from the diff operations
            var htmlGenerator = new HtmlDiffOutputGenerator();
            htmlGenerator.GenerateOutput(diffs, htmlReportPath);
        }

        Console.WriteLine($"HTML comparison report saved to '{htmlReportPath}'.");
    }
}
