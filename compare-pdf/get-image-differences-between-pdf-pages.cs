using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both PDF files not found.");
            return;
        }

        // Load both PDFs with deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // GraphicalPdfComparer is the correct API for visual (pixel) differences
            var comparer = new GraphicalPdfComparer
            {
                Threshold = 1.0, // percent of changed pixels to ignore
                // Convert System.Drawing.Color to Aspose.Pdf.Color using FromRgb
                Color = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Red),
                Resolution = new Aspose.Pdf.Devices.Resolution(100)
            };

            int pageCount = Math.Min(doc1.Pages.Count, doc2.Pages.Count);
            bool anyDifference = false;

            for (int i = 1; i <= pageCount; i++)
            {
                // GetDifference works on Page objects, not on Document objects
                using (ImagesDifference diff = comparer.GetDifference(doc1.Pages[i], doc2.Pages[i]))
                {
                    // Count changed pixels
                    int changedPixels = 0;
                    foreach (int v in diff.Difference)
                        if (v != 0) changedPixels++;

                    if (changedPixels > 0)
                    {
                        anyDifference = true;
                        Console.WriteLine($"Page {i}: {changedPixels} changed pixel(s) (height: {diff.Height}, width: {diff.Stride / 4}).");
                        // Optional: save a visual diff image
                        // diff.DifferenceToImage(System.Drawing.Color.Red, System.Drawing.Color.Transparent).Save($"page_{i}_diff.png");
                    }
                }
            }

            if (!anyDifference)
                Console.WriteLine("No image differences found.");
        }
    }
}
