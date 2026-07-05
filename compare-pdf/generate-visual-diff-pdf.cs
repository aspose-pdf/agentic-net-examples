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
        const string resultPdfPath = "visual_diff.pdf";

        // Verify that both source files exist before proceeding.
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load the two documents inside using blocks for deterministic disposal.
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Create the comparer and generate a visual diff PDF.
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPdfPath);
            }

            Console.WriteLine($"Visual diff PDF created at '{resultPdfPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any errors that may occur during loading or comparison.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}