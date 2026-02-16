using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Devices; // for Resolution type

class Program
{
    static void Main()
    {
        // Input PDF files
        const string firstPdfPath = "first.pdf";
        const string secondPdfPath = "second.pdf";
        // Output PDF file that will contain the graphical comparison result
        const string outputPdfPath = "comparison_result.pdf";

        // Verify that the input files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {secondPdfPath}");
            return;
        }

        try
        {
            // Load the two documents to be compared
            Document doc1 = new Document(firstPdfPath);
            Document doc2 = new Document(secondPdfPath);

            // Create the graphical comparer and configure optional properties
            GraphicalPdfComparer comparer = new GraphicalPdfComparer
            {
                // Change flag color (default is red)
                Color = Aspose.Pdf.Color.Red,
                // Resolution of the generated images (default 150 dpi)
                Resolution = new Resolution(150),
                // Threshold percentage to ignore minor differences (default 0%)
                Threshold = 0
            };

            // Perform the comparison and generate the result PDF
            comparer.CompareDocumentsToPdf(doc1, doc2, outputPdfPath);

            Console.WriteLine($"Graphical comparison completed successfully. Result saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during comparison: {ex.Message}");
        }
    }
}
