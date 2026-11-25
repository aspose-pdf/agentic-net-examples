using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Color
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";
        const string diffPath = "diff.png";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            var comparer = new GraphicalPdfComparer
            {
                // Percent of changed pixels to ignore (0.0 – 1.0)
                Threshold = 0.05,
                // Convert System.Drawing.Color to Aspose.Pdf.Color (custom colour)
                Color = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Red)
                // Optional: increase resolution for finer diff (default is 96 DPI)
                // Resolution = new Aspose.Pdf.Devices.Resolution(150)
            };

            // Compare the first pages of each document and save the diff as a PNG image
            comparer.ComparePagesToImage(doc1.Pages[1], doc2.Pages[1], diffPath);
        }

        Console.WriteLine($"Diff image saved to '{diffPath}'.");
    }
}