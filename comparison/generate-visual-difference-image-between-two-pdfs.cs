using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";
        const string outputPath = "difference.png";

        // Verify input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both PDF files not found.");
            return;
        }

        // Load both PDFs
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // GraphicalPdfComparer provides pixel‑level visual comparison
            var comparer = new GraphicalPdfComparer();

            // Get the pixel‑level difference between the first pages of the two documents
            using (ImagesDifference diff = comparer.GetDifference(doc1.Pages[1], doc2.Pages[1]))
            {
                // Convert the raw difference to a coloured image (red = difference, white = unchanged)
                using (System.Drawing.Image colouredDiff = diff.DifferenceToImage(
                    Aspose.Pdf.Color.Red,
                    Aspose.Pdf.Color.White))
                {
                    colouredDiff.Save(outputPath, ImageFormat.Png);
                }
            }
        }

        Console.WriteLine($"Difference image saved to '{outputPath}'.");
    }
}
