using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath = "original.pdf";
        const string secondPdfPath = "modified.pdf";
        const string resultPath = "comparison_result.pdf";

        // Verify that both source files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load the PDF documents
            Document firstDoc = new Document(firstPdfPath);
            Document secondDoc = new Document(secondPdfPath);

            // Comparison options – form field values are compared by default, no extra flag needed
            // (the ComparisonOptions class does not expose an IncludeFormFields property)
            // If additional options are required, they can be set on SideBySideComparisonOptions.
            SideBySideComparisonOptions sideOptions = new SideBySideComparisonOptions();

            // Perform a visual side‑by‑side comparison. The comparer is a static class, so we call the static method.
            SideBySidePdfComparer.Compare(firstDoc, secondDoc, resultPath, sideOptions);

            Console.WriteLine($"Comparison completed. Result saved to '{resultPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}
