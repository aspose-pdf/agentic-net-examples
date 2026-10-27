using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Compress the stamp image to low JPEG quality (10%) in memory
        byte[] lowQualityImageBytes = GetLowQualityJpegBytes(stampImagePath, 10L);
        using (var lowQualityImageStream = new MemoryStream(lowQualityImageBytes))
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create an ImageStamp from the low‑quality image stream
            ImageStamp imgStamp = new ImageStamp(lowQualityImageStream)
            {
                Background = false,                    // draw on top of page content
                Opacity = 0.5,                         // optional semi‑transparent effect
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Apply the stamp to each page individually
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF while still inside the using block
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }

    /// <summary>
    /// Loads an image from the given path and re‑encodes it as a JPEG with the specified quality.
    /// Returns the JPEG bytes.
    /// </summary>
    private static byte[] GetLowQualityJpegBytes(string imagePath, long quality)
    {
        // Fully qualify System.Drawing.Image to avoid ambiguity with Aspose.Pdf.Image
        using (System.Drawing.Image original = System.Drawing.Image.FromFile(imagePath))
        using (var ms = new MemoryStream())
        {
            ImageCodecInfo jpegCodec = GetEncoder(ImageFormat.Jpeg);
            var encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, quality);
            original.Save(ms, jpegCodec, encoderParams);
            return ms.ToArray();
        }
    }

    private static ImageCodecInfo GetEncoder(ImageFormat format)
    {
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.FormatID == format.Guid)
                return codec;
        }
        return null;
    }
}
