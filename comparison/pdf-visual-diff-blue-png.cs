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
        // Input PDF file paths
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";

        // Output PNG diff image path (diff of the first page)
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

        // Load the two PDFs inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Create the graphical comparer and configure it
            var comparer = new GraphicalPdfComparer
            {
                // Highlight colour for differences – blue as required
                Color = Aspose.Pdf.Color.Blue,
                // Optional: set a low threshold so even a single pixel change is reported
                Threshold = 0.0,
                // Optional: increase resolution for higher‑quality images
                Resolution = new Aspose.Pdf.Devices.Resolution(150)
            };

            // Get the visual difference for the first page of each document
            // (GraphicalPdfComparer works page‑by‑page)
            using (ImagesDifference diff = comparer.GetDifference(doc1.Pages[1], doc2.Pages[1]))
            {
                // Render the difference to a System.Drawing.Image. The first colour is the
                // highlight colour for changed pixels, the second is the background colour.
                using (System.Drawing.Image diffImage = diff.DifferenceToImage(Aspose.Pdf.Color.Blue, Aspose.Pdf.Color.White))
                {
                    diffImage.Save(diffImagePath, ImageFormat.Png);
                }
            }
        }

        Console.WriteLine($"Diff image saved to '{diffImagePath}'.");
    }
}
