using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        // Verify input files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load both documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Configure side‑by‑side comparison options as needed
                SideBySideComparisonOptions options = new SideBySideComparisonOptions
                {
                    // Example customizations (properties exist in the API):
                    // DeletionColor = Aspose.Pdf.Color.Red,
                    // InsertionColor = Aspose.Pdf.Color.Green,
                    // ShowDeletedContent = true,
                    // ShowInsertedContent = true
                };

                // Perform the comparison and save the result PDF
                SideBySidePdfComparer.Compare(doc1, doc2, resultPdfPath, options);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}