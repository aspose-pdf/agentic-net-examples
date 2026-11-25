using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";
        const string diffImageFolder = "diff_images";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both PDF files not found.");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(diffImageFolder);

        // Load the two PDFs inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Create the graphical comparer and configure it
            var comparer = new GraphicalPdfComparer
            {
                // 300 DPI resolution – the Resolution class expects DPI for X and Y
                Resolution = new Resolution(300),
                // Highlight colour for changed pixels – convert System.Drawing.Color to Aspose.Pdf.Color
                Color = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Red),
                // Percentage of changed pixels to ignore – 0 means no tolerance
                Threshold = 0.0
            };

            // Compare each page that exists in both documents and write a PNG diff per page
            int pageCount = Math.Min(doc1.Pages.Count, doc2.Pages.Count);
            for (int i = 1; i <= pageCount; i++)
            {
                using (ImagesDifference diff = comparer.GetDifference(doc1.Pages[i], doc2.Pages[i]))
                {
                    // Render the pixel‑by‑pixel difference to a bitmap (changed pixels in Red, unchanged transparent)
                    using (Bitmap bitmap = diff.DifferenceToImage(
                        Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Red),
                        Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Transparent)))
                    {
                        string outPath = Path.Combine(diffImageFolder, $"diff_page_{i}.png");
                        bitmap.Save(outPath, ImageFormat.Png);
                        Console.WriteLine($"Page {i} diff saved to '{outPath}'.");
                    }
                }
            }
        }
    }
}
