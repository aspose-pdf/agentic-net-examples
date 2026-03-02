using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string docPath1 = "doc1.pdf";
        const string docPath2 = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        // Verify that both source PDFs exist
        if (!File.Exists(docPath1) || !File.Exists(docPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the first document
        using (Document doc1 = new Document(docPath1))
        // Load the second document
        using (Document doc2 = new Document(docPath2))
        {
            // Instantiate the graphical comparer
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // Compare the two PDFs and write the visual diff to a new PDF file
            comparer.CompareDocumentsToPdf(doc1, doc2, resultPath);
        }

        Console.WriteLine($"Graphical comparison saved to '{resultPath}'.");
    }
}