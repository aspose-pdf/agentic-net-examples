using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Devices;

// Alias to avoid ambiguity between System.Drawing.Color and Aspose.Pdf.Color
using PdfColor = Aspose.Pdf.Color;
using SysColor = System.Drawing.Color;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";
        const string diffPath = "diff.png";

        // Verify source PDFs exist
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

        // Load the documents
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Configure the graphical comparer
            var comparer = new GraphicalPdfComparer
            {
                // 150 DPI resolution
                Resolution = new Resolution(150),
                // Highlight colour for differences (red) – convert System.Drawing.Color to Aspose.Pdf.Color
                Color = PdfColor.FromRgb(SysColor.Red),
                // No pixel‑percentage threshold – report all changes
                Threshold = 0.0
            };

            // Compare the first pages of both PDFs and obtain the pixel‑difference image
            using (ImagesDifference diff = comparer.GetDifference(doc1.Pages[1], doc2.Pages[1]))
            {
                // Render the difference to a bitmap (transparent background, red highlights)
                using (Bitmap bitmap = diff.DifferenceToImage(
                    PdfColor.Transparent,          // background colour (transparent)
                    PdfColor.FromRgb(SysColor.Red) // colour used to highlight differences
                ))
                {
                    bitmap.Save(diffPath, ImageFormat.Png);
                }
            }
        }

        Console.WriteLine($"Diff image saved to '{diffPath}'.");
    }
}
