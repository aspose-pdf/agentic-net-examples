using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main(string[] args)
    {
        // Paths to the PDFs to compare and the output file
        string pdfPath1 = "input1.pdf";
        string pdfPath2 = "input2.pdf";
        string outputPath = "comparison_result.pdf";

        // Verify that the source files exist
        if (!File.Exists(pdfPath1))
        {
            Console.Error.WriteLine($"File not found: {pdfPath1}");
            return;
        }
        if (!File.Exists(pdfPath2))
        {
            Console.Error.WriteLine($"File not found: {pdfPath2}");
            return;
        }

        // Load the two documents (creation + loading)
        Document doc1 = new Document(pdfPath1);
        Document doc2 = new Document(pdfPath2);

        // Create the graphical comparer
        GraphicalPdfComparer comparer = new GraphicalPdfComparer();

        // Optional: customize comparer settings
        // comparer.Color = Aspose.Pdf.Color.Red;      // default flag color
        // comparer.Resolution = 150;                  // DPI for comparison images
        // comparer.Threshold = 0;                     // percentage threshold

        // Compare the documents and write the result directly to a PDF file
        comparer.CompareDocumentsToPdf(doc1, doc2, outputPath);

        Console.WriteLine($"Comparison PDF saved to '{outputPath}'.");
    }
}