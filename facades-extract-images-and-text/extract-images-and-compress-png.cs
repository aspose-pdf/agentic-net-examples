using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Helper to obtain the PNG encoder.
    private static ImageCodecInfo GetPngEncoder()
    {
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.FormatID == ImageFormat.Png.Guid)
                return codec;
        }
        throw new InvalidOperationException("PNG encoder not found.");
    }

    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(outputFolder);

        // Use Aspose.Pdf.Facades.PdfExtractor to pull images from the PDF.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);

            // Extract only images that are actually rendered on the page.
            extractor.ExtractImageMode = Aspose.Pdf.ExtractImageMode.ActuallyUsed;
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Temporary file for the raw extracted PNG.
                string tempPngPath = Path.Combine(outputFolder, $"image_{imageIndex}.png");

                // Save the next image as PNG.
                extractor.GetNextImage(tempPngPath, ImageFormat.Png);

                // Re‑encode the PNG with maximum lossless compression (level 9).
                string compressedPngPath = Path.Combine(outputFolder, $"image_{imageIndex}_compressed.png");
                using (Bitmap bitmap = new Bitmap(tempPngPath))
                {
                    ImageCodecInfo pngEncoder = GetPngEncoder();

                    // Encoder.Compression for PNG accepts values 0‑9 (9 = best compression).
                    EncoderParameters encoderParams = new EncoderParameters(1);
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Compression, 9L);

                    bitmap.Save(compressedPngPath, pngEncoder, encoderParams);
                }

                // Clean up the intermediate file.
                File.Delete(tempPngPath);

                Console.WriteLine($"Extracted and compressed image saved to: {compressedPngPath}");
                imageIndex++;
            }
        }
    }
}