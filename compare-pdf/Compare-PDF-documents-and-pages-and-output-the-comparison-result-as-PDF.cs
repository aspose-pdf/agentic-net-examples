using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF files to compare
        const string pdfPath1 = "input1.pdf";
        const string pdfPath2 = "input2.pdf";

        // Output PDF that will contain the visual comparison result
        const string resultPdfPath = "comparison_result.pdf";

        // Verify that both source files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        try
        {
            // Load the two documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Create the graphical comparer
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();

                // The Resolution property expects a Resolution object, not an int.
                // Here we set the DPI to 150 (default is also 150).
                comparer.Resolution = new Resolution(150);

                // Optional: set the color used to highlight differences (default is red)
                comparer.Color = Aspose.Pdf.Color.Red;

                // Perform the comparison and write the result directly to a PDF file
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPdfPath);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPdfPath}'.");
        }
        catch (Exception ex)
        {
            // Catch any errors (e.g., mismatched page sizes) and report them
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}