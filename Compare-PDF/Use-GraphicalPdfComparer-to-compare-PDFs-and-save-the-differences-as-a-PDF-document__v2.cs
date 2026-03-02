using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Paths to the source PDF files and the output comparison PDF
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";
        const string resultPdfPath = "differences.pdf";

        // Verify that both input files exist before proceeding
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
                // Create the comparer instance (default constructor)
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();

                // Optional: customize comparison appearance
                // comparer.Color = Aspose.Pdf.Color.Red;
                // comparer.Resolution = 200; // DPI
                // comparer.Threshold = 5;    // ignore changes below 5%

                // Perform the graphical comparison and save the result directly to a PDF file
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPdfPath);
            }

            Console.WriteLine($"Comparison PDF successfully saved to '{resultPdfPath}'.");
        }
        catch (Exception ex)
        {
            // Handle any errors (e.g., mismatched page sizes, I/O issues)
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}