using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";

        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            if (doc1.Pages.Count == 0 || doc2.Pages.Count == 0)
            {
                Console.Error.WriteLine("One of the documents has no pages.");
                return;
            }

            // Pages are 1‑based indexed
            Page page1 = doc1.Pages[1];
            Page page2 = doc2.Pages[1];

            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            using (ImagesDifference diff = comparer.GetDifference(page1, page2))
            {
                Console.WriteLine($"Difference image height: {diff.Height}");
                Console.WriteLine($"Difference image stride: {diff.Stride}");
                Console.WriteLine($"Difference array length: {diff.Difference.Length}");
            }
        }
    }
}