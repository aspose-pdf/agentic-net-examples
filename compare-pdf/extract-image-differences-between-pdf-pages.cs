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
        const string firstPdfPath  = "first.pdf";
        const string secondPdfPath = "second.pdf";
        const string outputDir      = "ImageDifferences";

        // Verify input files exist
        if (!File.Exists(firstPdfPath))
        {
            Console.Error.WriteLine($"File not found: {firstPdfPath}");
            return;
        }
        if (!File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine($"File not found: {secondPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load both PDF documents (core API only)
            using (Aspose.Pdf.Document doc1 = new Aspose.Pdf.Document(firstPdfPath))
            using (Aspose.Pdf.Document doc2 = new Aspose.Pdf.Document(secondPdfPath))
            {
                // For simplicity compare the first page of each document.
                // Adjust the page index as needed (Aspose.Pdf uses 1‑based indexing).
                Aspose.Pdf.Page page1 = doc1.Pages[1];
                Aspose.Pdf.Page page2 = doc2.Pages[1];

                // Create the graphical comparer
                Aspose.Pdf.Comparison.GraphicalPdfComparer comparer = new Aspose.Pdf.Comparison.GraphicalPdfComparer();

                // Obtain the image differences between the two pages
                using (Aspose.Pdf.Comparison.ImagesDifference diff = comparer.GetDifference(page1, page2))
                {
                    // Save the source page image
                    string sourceImagePath = Path.Combine(outputDir, "SourcePage.png");
                    diff.SourceImage.Save(sourceImagePath, ImageFormat.Png);

                    // Generate and save the destination image (second page with differences applied)
                    using (Bitmap destImage = diff.GetDestinationImage())
                    {
                        string destImagePath = Path.Combine(outputDir, "DestinationPage.png");
                        destImage.Save(destImagePath, ImageFormat.Png);
                    }

                    // Optionally, generate a visual diff image.
                    // This method requires System.Drawing.Color, which is avoided per guidelines,
                    // so we skip calling DifferenceToImage. If needed, uncomment the following lines:

                    // using (Bitmap diffImage = diff.DifferenceToImage(Color.Red, Color.Transparent))
                    // {
                    //     string diffImagePath = Path.Combine(outputDir, "DiffImage.png");
                    //     diffImage.Save(diffImagePath, ImageFormat.Png);
                    // }

                    Console.WriteLine($"Image differences saved to '{outputDir}'.");
                }
            }
        }
        catch (ArgumentException ex)
        {
            // Thrown if pages have different sizes or other argument issues
            Console.Error.WriteLine($"Argument error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}