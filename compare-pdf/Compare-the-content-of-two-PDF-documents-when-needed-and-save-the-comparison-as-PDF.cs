using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the source PDF files and the result PDF
        const string pdf1Path = "doc1.pdf";
        const string pdf2Path = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        // Verify that both input files exist
        if (!File.Exists(pdf1Path) || !File.Exists(pdf2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load both documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(pdf1Path))
            using (Document doc2 = new Document(pdf2Path))
            {
                // Create the graphical comparer (suitable for detecting visual changes)
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();

                // Optional: customize comparison appearance
                // comparer.Color = Aspose.Pdf.Color.Red;
                // comparer.Threshold = 5; // ignore changes smaller than 5%

                // Perform the comparison and write the result directly to a PDF file
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPath);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any errors that occur during loading or comparison
            Console.Error.WriteLine($"Error during PDF comparison: {ex.Message}");
        }
    }
}