using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputFolder = "ExtractedImages";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // PdfFileMend cannot extract images – use PdfExtractor instead
        var extractor = new PdfExtractor();
        try
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdf);

            // Extract all images from the PDF
            extractor.ExtractImage();

            int imageCount = 0;
            while (extractor.HasNextImage())
            {
                imageCount++;
                string outPath = Path.Combine(outputFolder, $"image{imageCount}.png");
                // Save each image in PNG format (any ImageFormat can be used)
                extractor.GetNextImage(outPath, ImageFormat.Png);
            }

            Console.WriteLine($"Extracted {imageCount} image(s) to '{outputFolder}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during image extraction: {ex.Message}");
        }
        finally
        {
            // Release resources held by the extractor
            extractor.Close();
        }
    }
}
