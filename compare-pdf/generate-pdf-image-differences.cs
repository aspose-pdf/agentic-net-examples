using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using Aspose.Pdf.Devices; // Added for Resolution
using System.Drawing; // Required for Color and Bitmap handling

class Program
{
    static void Main()
    {
        // Input PDF files
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";

        // Directory where result images will be saved
        const string outputDir = "DiffOutput";

        // Verify that both input files exist
        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both input PDF files were not found.");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Load both documents inside using blocks for deterministic disposal
        using (Document doc1 = new Document(pdfPath1))
        using (Document doc2 = new Document(pdfPath2))
        {
            // Both documents must contain at least one page for comparison
            if (doc1.Pages.Count == 0 || doc2.Pages.Count == 0)
            {
                Console.Error.WriteLine("One of the PDFs does not contain any pages.");
                return;
            }

            // Compare the first pages of each document
            Page page1 = doc1.Pages[1]; // 1‑based indexing
            Page page2 = doc2.Pages[1];

            // Create the graphical comparer
            GraphicalPdfComparer comparer = new GraphicalPdfComparer();

            // Optional configuration (default values are shown for clarity)
            comparer.Color = Aspose.Pdf.Color.Red;      // Highlight color for differences
            comparer.Threshold = 0;          // Detect all differences
            comparer.Resolution = new Resolution(150); // Fixed: use Resolution object instead of int

            // Obtain the image difference information
            using (ImagesDifference diff = comparer.GetDifference(page1, page2))
            {
                // Save the source image of the first page
                string sourceImgPath = Path.Combine(outputDir, "source_page1.png");
                diff.SourceImage.Save(sourceImgPath, System.Drawing.Imaging.ImageFormat.Png);
                Console.WriteLine($"Source image saved to: {sourceImgPath}");

                // Convert the raw difference array to a visual bitmap
                using (Bitmap diffBitmap = diff.DifferenceToImage(Aspose.Pdf.Color.Transparent, Aspose.Pdf.Color.Red))
                {
                    string diffImgPath = Path.Combine(outputDir, "diff_page1.png");
                    diffBitmap.Save(diffImgPath, System.Drawing.Imaging.ImageFormat.Png);
                    Console.WriteLine($"Difference image saved to: {diffImgPath}");
                }

                // Optionally retrieve the destination image (first page with differences applied)
                using (Bitmap destBitmap = diff.GetDestinationImage())
                {
                    string destImgPath = Path.Combine(outputDir, "dest_page1.png");
                    destBitmap.Save(destImgPath, System.Drawing.Imaging.ImageFormat.Png);
                    Console.WriteLine($"Destination image saved to: {destImgPath}");
                }
            }
        }
    }
}
