using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string doc1Path = "doc1.pdf";
        const string doc2Path = "doc2.pdf";
        const string resultPath = "comparison_result.pdf";

        if (!File.Exists(doc1Path) || !File.Exists(doc2Path))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load both PDFs inside using blocks for deterministic disposal
            using (Aspose.Pdf.Document doc1 = new Aspose.Pdf.Document(doc1Path))
            using (Aspose.Pdf.Document doc2 = new Aspose.Pdf.Document(doc2Path))
            {
                // Create the graphical comparer
                Aspose.Pdf.Comparison.GraphicalPdfComparer comparer = new Aspose.Pdf.Comparison.GraphicalPdfComparer();

                // Optional: configure comparison appearance
                comparer.Color = Aspose.Pdf.Color.Red; // highlight color for differences
                comparer.Resolution = new Aspose.Pdf.Devices.Resolution(150); // DPI for internal rendering
                comparer.Threshold = 0; // percentage threshold for ignoring minor changes

                // Perform the comparison and save the result as a PDF
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPath);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}