using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Comparison;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Paths to the two PDF files to compare
        const string pdfPath1 = "first.pdf";
        const string pdfPath2 = "second.pdf";

        // Output image that will contain the visual differences
        const string diffImagePath = "difference.png";

        // Verify that both source files exist
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

        try
        {
            // Load the two PDF documents inside using blocks for deterministic disposal
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            {
                // Ensure both documents have at least one page
                if (doc1.Pages.Count == 0 || doc2.Pages.Count == 0)
                {
                    Console.Error.WriteLine("One of the documents does not contain any pages.");
                    return;
                }

                // Create the graphical comparer – it works on rasterized page images
                GraphicalPdfComparer comparer = new GraphicalPdfComparer();

                // Compare the first pages of each document.
                // GetDifference returns an ImagesDifference object that holds the source image
                // and the pixel‑wise difference data.
                ImagesDifference diff = comparer.GetDifference(doc1.Pages[1], doc2.Pages[1]);

                // Example of processing the result:
                // - Height of the difference image
                // - Size of the raw difference byte array
                Console.WriteLine($"Difference image height: {diff.Height}");
                Console.WriteLine($"Difference byte array length: {diff.Difference.Length}");

                // Convert the raw difference data into a bitmap that visualizes the changes.
                // GetDestinationImage applies the difference to the source image and returns a Bitmap.
                using (Bitmap diffBitmap = diff.GetDestinationImage())
                {
                    // Save the bitmap to a PNG file.
                    diffBitmap.Save(diffImagePath, ImageFormat.Png);
                }

                // Dispose the ImagesDifference object when finished.
                diff.Dispose();

                Console.WriteLine($"Image differences saved to '{diffImagePath}'.");
            }
        }
        catch (ArgumentException ex)
        {
            // Thrown if the compared pages have different dimensions.
            Console.Error.WriteLine($"Argument error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}