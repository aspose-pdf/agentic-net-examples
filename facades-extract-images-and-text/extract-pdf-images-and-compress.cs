using System;
using System.IO;
using System.IO.Compression;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";               // source PDF
        const string outputFolder = "ExtractedImages";         // folder for results

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Use Aspose.Pdf.Facades.PdfExtractor to pull images from the PDF
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);    // load the PDF
            extractor.ExtractImage();           // prepare image extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Save the extracted image as PNG (lossless by nature)
                string pngPath = Path.Combine(outputFolder, $"image-{imageIndex}.png");
                extractor.GetNextImage(pngPath, ImageFormat.Png);

                // Compress the PNG using a lossless ZIP archive
                string zipPath = Path.Combine(outputFolder, $"image-{imageIndex}.zip");
                using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
                using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
                {
                    // Add the PNG to the archive with optimal (lossless) compression
                    archive.CreateEntryFromFile(pngPath, Path.GetFileName(pngPath), CompressionLevel.Optimal);
                }

                // Optionally delete the original PNG if only the compressed version is needed
                File.Delete(pngPath);

                Console.WriteLine($"Extracted and compressed image {imageIndex}");
                imageIndex++;
            }
        }

        Console.WriteLine("Image extraction and compression completed.");
    }
}