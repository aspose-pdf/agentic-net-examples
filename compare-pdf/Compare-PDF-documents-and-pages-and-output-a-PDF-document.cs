using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Devices; // for Resolution

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string resultPdfPath = "comparison_result.pdf";

        // Verify input files exist
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

        try
        {
            // Load the two documents inside using blocks (lifecycle rule)
            using (Document doc1 = new Document(firstPdfPath))
            using (Document doc2 = new Document(secondPdfPath))
            {
                // Create a GraphicalPdfComparer instance
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();

                // Set the resolution correctly – Resolution is a class, not an int
                comparer.Resolution = new Resolution(150); // 150 DPI (default is 150)

                // Optional: change the highlight color for differences
                comparer.Color = Aspose.Pdf.Color.Red;

                // Compare the whole documents and write the visual diff to a PDF file
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPdfPath);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}