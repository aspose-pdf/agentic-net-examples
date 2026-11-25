using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";
        const string diffImagePath = "diff.png";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Load the PDF documents.
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Configure the graphical comparer – highlight differences in green.
            var comparer = new GraphicalPdfComparer
            {
                Color = Aspose.Pdf.Color.Green,
                // Optional: adjust tolerance and resolution if needed.
                Threshold = 0.0, // percent of changed pixels to ignore
                Resolution = new Resolution(96) // DPI for rendering
            };

            // For simplicity, generate a PNG diff for the first page of each document.
            // If you need diffs for all pages, loop over doc1.Pages.Count.
            comparer.ComparePagesToImage(doc1.Pages[1], doc2.Pages[1], diffImagePath);
        }

        Console.WriteLine($"Diff image saved to '{diffImagePath}'.");
    }
}
