using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        // Input PDF files
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";

        // Output PNG diff image (first page comparison)
        const string diffImagePath = "diff.png";

        // Verify input files exist
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
            // Load the two PDFs inside using blocks for deterministic disposal
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Configure the graphical comparer – highlight differences in red
                var comparer = new GraphicalPdfComparer
                {
                    Color = Color.Red,                     // highlight colour
                    Resolution = new Resolution(150),      // DPI for rendering
                    Threshold = 0.0                        // 0% pixel change threshold (default)
                };

                // Compare only the first page of each document and output a PNG image
                comparer.ComparePagesToImage(doc1.Pages[1], doc2.Pages[1], diffImagePath);
            }

            Console.WriteLine($"Diff image saved to '{diffImagePath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}
