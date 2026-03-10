using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        // Input PDF files to compare
        const string pdfPath1 = "document1.pdf";
        const string pdfPath2 = "document2.pdf";
        // Path for the comparison result PDF
        const string resultPath = "comparison_result.pdf";

        // Verify that input files exist
        if (!File.Exists(pdfPath1))
        {
            Console.Error.WriteLine($"File not found: {pdfPath1}");
            return;
        }
        if (!File.Exists(pdfPath2))
        {
            Console.Error.WriteLine($"File not found: {pdfPath2}");
            return;
        }

        try
        {
            // Load the first document inside a using block (ensures disposal)
            using (Document doc1 = new Document(pdfPath1))
            // Load the second document inside a using block
            using (Document doc2 = new Document(pdfPath2))
            {
                // Create an instance of the graphical comparer
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();

                // Optional: customize comparison appearance
                // comparer.Color = Aspose.Pdf.Color.Red;
                // comparer.Threshold = 0; // 0% to detect all changes

                // Perform the comparison and save the result directly to a PDF file
                comparer.CompareDocumentsToPdf(doc1, doc2, resultPath);
            }

            Console.WriteLine($"Comparison PDF saved to '{resultPath}'.");
        }
        catch (ArgumentException ex)
        {
            // Thrown if pages have different sizes or resultPath is invalid
            Console.Error.WriteLine($"Argument error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}