using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using System.Drawing; // Used for Image and Bitmap handling

class Program
{
    static void Main()
    {
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";
        const string outputDir = "DiffOutput";

        // Verify that both input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("Input PDF files not found.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the two PDFs using deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Both documents must contain at least one page
            if (doc1.Pages.Count == 0 || doc2.Pages.Count == 0)
            {
                Console.Error.WriteLine("One of the documents has no pages.");
                return;
            }

            // Compare pages up to the smaller page count
            int pageCount = Math.Min(doc1.Pages.Count, doc2.Pages.Count);
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            for (int i = 1; i <= pageCount; i++) // 1‑based indexing
            {
                Page page1 = doc1.Pages[i];
                Page page2 = doc2.Pages[i];

                // Obtain image differences for the current page pair
                using (ImagesDifference diff = comparer.GetDifference(page1, page2))
                {
                    // Save the source image (first page)
                    string srcPath = Path.Combine(outputDir, $"Page_{i}_Source.png");
                    diff.SourceImage.Save(srcPath);
                    Console.WriteLine($"Saved source image: {srcPath}");

                    // Save the destination image (second page with differences applied)
                    using (Bitmap destImg = diff.GetDestinationImage())
                    {
                        string destPath = Path.Combine(outputDir, $"Page_{i}_Destination.png");
                        destImg.Save(destPath);
                        Console.WriteLine($"Saved destination image: {destPath}");
                    }

                    // Output basic metrics about the difference image
                    Console.WriteLine($"Page {i}: Height={diff.Height}, Stride={diff.Stride}");
                }
            }
        }
    }
}