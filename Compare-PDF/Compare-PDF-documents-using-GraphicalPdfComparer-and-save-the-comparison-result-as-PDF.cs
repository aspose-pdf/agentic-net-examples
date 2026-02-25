using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPath = "first.pdf";
        const string secondPath = "second.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(firstPath) || !File.Exists(secondPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load both PDF documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPath))
        using (Document doc2 = new Document(secondPath))
        {
            // Instantiate the graphical comparer
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // Optional: customize comparer settings
            // comparer.Color = Aspose.Pdf.Color.Red; // default is red
            // comparer.Resolution = 150; // default DPI
            // comparer.Threshold = 0; // default percentage

            // Compare the documents and save the visual diff as a PDF
            comparer.CompareDocumentsToPdf(doc1, doc2, resultPath);
        }

        Console.WriteLine($"Graphical comparison PDF saved to '{resultPath}'.");
    }
}