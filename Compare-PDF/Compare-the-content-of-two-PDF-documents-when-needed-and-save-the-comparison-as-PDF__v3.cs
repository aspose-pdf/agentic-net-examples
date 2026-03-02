using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the input PDF files and the output comparison PDF
        const string pdf1Path = "first.pdf";
        const string pdf2Path = "second.pdf";
        const string resultPath = "comparison_result.pdf";

        // Verify that both input files exist
        if (!File.Exists(pdf1Path) || !File.Exists(pdf2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load the two PDF documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(pdf1Path))
            using (Document doc2 = new Document(pdf2Path))
            {
                // Create an instance of the graphical comparer
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();

                // Perform the comparison and save the result as a PDF file
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPath);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any errors that may occur during loading or comparison
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}