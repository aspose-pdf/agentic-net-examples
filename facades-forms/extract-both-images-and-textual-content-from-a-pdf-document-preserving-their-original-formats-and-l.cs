using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string inputPdf = "input.pdf";

        // Directory where extracted content will be saved
        const string outputDir = "ExtractedContent";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // PdfExtractor is a Facade that handles both text and image extraction
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdf);

            // -------------------------
            // Extract textual content
            // -------------------------
            extractor.ExtractText(); // Perform the extraction operation
            string textOutputPath = Path.Combine(outputDir, "document.txt");
            // Save the extracted text preserving the original order/layout
            extractor.GetText(textOutputPath);

            // -------------------------
            // Extract images
            // -------------------------
            extractor.ExtractImage(); // Prepare image extraction
            int imageIndex = 1;

            // Iterate through all images in the document
            while (extractor.HasNextImage())
            {
                // Build a file name that reflects the image order
                string imagePath = Path.Combine(outputDir, $"image-{imageIndex}.png");

                // Save each image as PNG (lossless for most raster images)
                // The overload with ImageFormat preserves the visual fidelity
                extractor.GetNextImage(imagePath, ImageFormat.Png);

                imageIndex++;
            }

            // Optional explicit close (Dispose will be called by the using statement)
            extractor.Close();
        }

        Console.WriteLine("Extraction of text and images completed successfully.");
    }
}