using System;
using System.IO;
using System.IO.Compression;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        PdfExtractor extractor = new PdfExtractor();
        extractor.BindPdf(inputPdf);
        extractor.ExtractImage();

        int imageIndex = 1;
        while (extractor.HasNextImage())
        {
            string pngFileName = "image-" + imageIndex + ".png";
            // Save the next image as PNG
            extractor.GetNextImage(pngFileName, ImageFormat.Png);

            // Compress the PNG using GZip (lossless)
            string gzFileName = pngFileName + ".gz";
            using (FileStream originalStream = new FileStream(pngFileName, FileMode.Open, FileAccess.Read))
            using (FileStream compressedStream = new FileStream(gzFileName, FileMode.Create, FileAccess.Write))
            using (GZipStream gzip = new GZipStream(compressedStream, CompressionLevel.Optimal))
            {
                originalStream.CopyTo(gzip);
            }

            // Optionally delete the uncompressed PNG file
            File.Delete(pngFileName);

            imageIndex++;
        }

        Console.WriteLine("Image extraction and compression completed.");
    }
}