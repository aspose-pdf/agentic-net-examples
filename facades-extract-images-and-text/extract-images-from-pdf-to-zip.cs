using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputZip = "images.zip";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Extract images and add them to a zip archive
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage();

            using (FileStream zipFile = new FileStream(outputZip, FileMode.Create))
            using (ZipArchive zip = new ZipArchive(zipFile, ZipArchiveMode.Create))
            {
                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        // Extract next image as JPEG (default format) into the memory stream
                        extractor.GetNextImage(imgStream);
                        imgStream.Position = 0;

                        // Create a zip entry for the image
                        string entryName = $"image-{imageIndex}.jpg";
                        ZipArchiveEntry entry = zip.CreateEntry(entryName, CompressionLevel.Optimal);
                        using (Stream entryStream = entry.Open())
                        {
                            imgStream.CopyTo(entryStream);
                        }
                    }
                    imageIndex++;
                }
            }
        }

        Console.WriteLine($"Images extracted to '{outputZip}'.");
    }
}