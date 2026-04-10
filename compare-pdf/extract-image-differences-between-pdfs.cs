using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";
        const string outputDir = "Differences";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Load the two PDF documents inside using blocks (ensures proper disposal)
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Ensure both documents have the same number of pages for a fair comparison
            int pageCount = Math.Min(doc1.Pages.Count, doc2.Pages.Count);
            if (pageCount == 0)
            {
                Console.WriteLine("No pages to compare.");
                return;
            }

            // Create the graphical comparer (default settings)
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            for (int i = 1; i <= pageCount; i++)
            {
                Page page1 = doc1.Pages[i];
                Page page2 = doc2.Pages[i];

                // Get the image difference between the two pages
                ImagesDifference diff = comparer.GetDifference(page1, page2);

                // Obtain a bitmap that visualizes the differences
                // Option 1: Use the destination image (source image with differences applied)
                using (Bitmap destImage = diff.GetDestinationImage())
                {
                    string destPath = Path.Combine(outputDir, $"diff_page_{i}_dest.png");
                    destImage.Save(destPath);
                    Console.WriteLine($"Saved destination difference image: {destPath}");
                }

                // Option 2: Create a separate diff image with custom colors (e.g., red for changes, white for unchanged)
                using (Bitmap diffImage = diff.DifferenceToImage(Aspose.Pdf.Color.Red, Aspose.Pdf.Color.White))
                {
                    string diffPath = Path.Combine(outputDir, $"diff_page_{i}_mask.png");
                    diffImage.Save(diffPath);
                    Console.WriteLine($"Saved mask difference image: {diffPath}");
                }

                // Dispose the ImagesDifference instance
                diff.Dispose();
            }
        }

        Console.WriteLine("Image difference extraction completed.");
    }
}