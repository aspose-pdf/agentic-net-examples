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
        const string diffPdfPath   = "diff_output.pdf";

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                comparer.CompareDocumentsToPdf(doc1, doc2, diffPdfPath);
            }

            Console.WriteLine($"Diff PDF saved to '{diffPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}