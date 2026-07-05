using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Paths to the PDFs to compare
        const string pdfPath1 = "document1.pdf";
        const string pdfPath2 = "document2.pdf";

        // Directory where the comparison images will be saved
        const string outputDirectory = "ComparisonResults";

        // Verify that both source files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Create a graphical comparer instance
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // Set a custom tolerance (percentage) for image similarity.
            // This value tells the comparer to ignore differences below the specified threshold.
            comparer.Threshold = 5.0; // 5 % tolerance

            // Perform the comparison and output the results as PNG images.
            // Each differing page pair will generate an image file prefixed with "diff_".
            comparer.CompareDocumentsToImages(
                doc1,
                doc2,
                outputDirectory,
                "diff_",
                ImageFormat.Png);
        }

        Console.WriteLine("Image comparison completed successfully.");
    }
}