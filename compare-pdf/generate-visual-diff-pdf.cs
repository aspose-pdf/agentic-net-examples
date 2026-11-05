using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string leftPath = "left.pdf";
        const string rightPath = "right.pdf";
        const string diffPath = "visual_diff.pdf";

        // Verify that both input PDFs exist
        if (!File.Exists(leftPath) || !File.Exists(rightPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the PDFs inside using blocks for deterministic disposal
        using (Document leftDoc = new Document(leftPath))
        using (Document rightDoc = new Document(rightPath))
        {
            // Instantiate the visual comparer from Aspose.Pdf.Comparison
            var comparer = new GraphicalPdfComparer();

            // Generate a visual diff PDF; this method writes the result to diffPath
            comparer.CompareDocumentsToPdf(leftDoc, rightDoc, diffPath);
        }

        Console.WriteLine($"Visual diff PDF saved to '{diffPath}'.");
    }
}
