using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string file1 = "doc1.pdf";
        const string file2 = "doc2.pdf";
        const string result = "diff.pdf";

        if (!File.Exists(file1) || !File.Exists(file2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            using (Document doc1 = new Document(file1))
            using (Document doc2 = new Document(file2))
            {
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();
                // Optional: customize comparer properties
                // comparer.Color = Aspose.Pdf.Color.Red;
                // comparer.Threshold = 5; // ignore changes below 5%
                // comparer.Resolution = 200; // DPI for internal images

                comparer.CompareDocumentsToPdf(doc1, doc2, result);
            }

            Console.WriteLine($"Visual diff PDF saved to '{result}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}