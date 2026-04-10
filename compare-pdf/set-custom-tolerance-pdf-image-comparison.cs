using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "original.pdf";
        const string pdfPath2 = "modified.pdf";
        const string outputDir = "ComparisonResults";
        const double customTolerance = 5.0; // tolerance in percentage

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files are missing.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the two PDF documents
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Create a graphical comparer and set the custom tolerance (Threshold)
            GraphicalPdfComparer comparer = new GraphicalPdfComparer
            {
                Threshold = customTolerance // ignore differences below this percentage
            };

            // Compare the documents and save the visual differences as images
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