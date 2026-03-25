using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string pdf1Path = "first.pdf";
        const string pdf2Path = "second.pdf";
        const string diffImagePath = "difference.png";

        if (!File.Exists(pdf1Path) || !File.Exists(pdf2Path))
        {
            Console.Error.WriteLine("Input PDFs not found.");
            return;
        }

        using (Document doc1 = new Document(pdf1Path))
        using (Document doc2 = new Document(pdf2Path))
        {
            // Ensure both documents have at least one page
            if (doc1.Pages.Count == 0 || doc2.Pages.Count == 0)
            {
                Console.Error.WriteLine("One of the documents has no pages.");
                return;
            }

            Page page1 = doc1.Pages[1];
            Page page2 = doc2.Pages[1];

            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            using (ImagesDifference diff = comparer.GetDifference(page1, page2))
            {
                // Save the visual difference as an image
                using (Bitmap result = diff.GetDestinationImage())
                {
                    result.Save(diffImagePath, ImageFormat.Png);
                    Console.WriteLine($"Difference image saved to '{diffImagePath}'.");
                }

                // Output some diagnostic information
                Console.WriteLine($"Difference array length: {diff.Difference.Length}");
                Console.WriteLine($"Image height: {diff.Height}, stride: {diff.Stride}");
            }
        }
    }
}