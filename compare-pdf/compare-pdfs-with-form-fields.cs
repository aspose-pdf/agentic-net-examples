using System;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the PDFs that contain form fields
        const string firstPdfPath  = "form1.pdf";
        const string secondPdfPath = "form2.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        // Ensure the source files exist before proceeding
        if (!System.IO.File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!System.IO.File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Create comparison options.
            // The default options already consider form field values.
            // If additional configuration is needed, set properties here.
            ComparisonOptions options = new ComparisonOptions
            {
                // Example: keep tables in the comparison (default is false)
                ExcludeTables = false
            };

            // Perform a page‑by‑page comparison and save the visual diff to a PDF file.
            // The overload with a result path writes the diff output automatically.
            TextPdfComparer.CompareDocumentsPageByPage(doc1, doc2, options, resultPdfPath);
        }

        Console.WriteLine($"Comparison completed. Result saved to '{resultPdfPath}'.");
    }
}