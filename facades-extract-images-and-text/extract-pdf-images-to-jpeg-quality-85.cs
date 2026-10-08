using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string pdfPath   = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        Directory.CreateDirectory(outputDir);

        // Initialize the Facades extractor and bind the PDF file
        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(pdfPath);

        // Extract all images from the PDF
        extractor.ExtractImage();

        int imageIndex = 1;
        while (extractor.HasNextImage())
        {
            // Retrieve the next image into a memory stream (original format)
            using (MemoryStream imgStream = new MemoryStream())
            {
                extractor.GetNextImage(imgStream);
                imgStream.Position = 0;

                // Load the image using System.Drawing (Windows‑only)
                using (Image img = Image.FromStream(imgStream))
                {
                    // Configure JPEG encoder with quality = 85
                    ImageCodecInfo jpegCodec = GetEncoder(ImageFormat.Jpeg);
                    EncoderParameters encoderParams = new EncoderParameters(1);
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 85L);

                    string outPath = Path.Combine(outputDir, $"Image_{imageIndex}.jpg");
                    img.Save(outPath, jpegCodec, encoderParams);
                    Console.WriteLine($"Saved: {outPath}");
                }
            }
            imageIndex++;
        }

        Console.WriteLine("Image extraction completed.");
    }

    // Helper method to obtain the JPEG encoder
    private static ImageCodecInfo GetEncoder(ImageFormat format)
    {
        foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageDecoders())
        {
            if (codec.FormatID == format.Guid)
                return codec;
        }
        return null;
    }
}