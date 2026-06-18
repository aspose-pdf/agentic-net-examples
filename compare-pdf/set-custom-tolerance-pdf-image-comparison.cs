using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using System.Drawing.Imaging; // for ImageFormat

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string outputDir     = "ComparisonResults";
        const double customTolerance = 5.0; // tolerance in percent

        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("Input PDF files not found.");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the two documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(firstPdfPath))
        using (Document doc2 = new Document(secondPdfPath))
        {
            // Create a GraphicalPdfComparer and set the custom tolerance (Threshold)
            GraphicalPdfComparer comparer = new GraphicalPdfComparer
            {
                Threshold = customTolerance // ignore differences below this percentage
            };

            // Compare the documents page‑by‑page and output the differences as images
            comparer.CompareDocumentsToImages(
                doc1,
                doc2,
                outputDir,
                "DiffPage_",
                ImageFormat.Png);
        }

        Console.WriteLine($"Comparison completed. Results saved in '{outputDir}'.");
    }
}