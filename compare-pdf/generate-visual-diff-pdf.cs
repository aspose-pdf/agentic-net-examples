using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "input1.pdf";
        const string secondPdfPath = "input2.pdf";
        const string resultPdfPath = "visual_diff.pdf";

        // Verify that both source files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load the two documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Create the comparer instance
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();

                // Optional: customize comparer settings
                // comparer.Color = Aspose.Pdf.Color.Red;
                // comparer.Resolution = 200; // DPI
                // comparer.Threshold = 5;    // percent

                // Perform the visual comparison and save the result as a PDF
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPdfPath);
            }

            Console.WriteLine($"Visual diff PDF generated at '{resultPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}