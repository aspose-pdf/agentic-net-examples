using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";
        const string resultPath = "comparison_result.pdf";

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

        // Load both PDFs using 'using' to ensure proper disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Instantiate the graphical comparer
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // Optional: customize comparer settings
            // comparer.Color = Aspose.Pdf.Color.Red;
            // comparer.Resolution = 150;
            // comparer.Threshold = 0;

            // Compare the documents and write the visual diff to a PDF file
            comparer.CompareDocumentsToPdf(doc1, doc2, resultPath);
        }

        Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
    }
}