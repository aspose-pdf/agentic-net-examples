using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdf1Path = "doc1.pdf";
        const string pdf2Path = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        // Verify that both source PDFs exist
        if (!File.Exists(pdf1Path) || !File.Exists(pdf2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Wrap Document objects in using blocks for deterministic disposal
            using (Document doc1 = new Document(pdf1Path))
            using (Document doc2 = new Document(pdf2Path))
            {
                // Create comparison options (default settings are sufficient for most cases)
                SideBySideComparisonOptions options = new SideBySideComparisonOptions();

                // Example of customizing the difference highlight color (optional)
                // options.DifferenceColor = Aspose.Pdf.Color.Red; // Aspose.Pdf.Color is cross‑platform

                // Perform side‑by‑side comparison and save the result as a PDF
                SideBySidePdfComparer.Compare(doc1, doc2, resultPath, options);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}