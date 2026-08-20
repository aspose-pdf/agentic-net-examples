using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string pdf1Path = "doc1.pdf";
        const string pdf2Path = "doc2.pdf";
        const string outputDir = "ComparisonResults";
        const double customTolerance = 5.0; // percentage tolerance for image differences

        if (!File.Exists(pdf1Path) || !File.Exists(pdf2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        Directory.CreateDirectory(outputDir);

        using (Document doc1 = new Document(pdf1Path))
        using (Document doc2 = new Document(pdf2Path))
        {
            // Set up the graphical comparer with a custom threshold
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();
            comparer.Threshold = customTolerance; // ignore changes below this percentage

            // Perform the comparison and save result images
            comparer.CompareDocumentsToImages(
                doc1,
                doc2,
                outputDir,
                "diff",
                ImageFormat.Png);
        }

        Console.WriteLine("PDF comparison completed.");
    }
}