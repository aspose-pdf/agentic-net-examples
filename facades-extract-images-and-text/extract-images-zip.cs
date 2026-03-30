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

        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdf);
            extractor.ExtractImage();

            using (FileStream zipFileStream = new FileStream(outputZip, FileMode.Create))
            using (ZipArchive zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
            {
                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        bool success = extractor.GetNextImage(imageStream);
                        if (success)
                        {
                            imageStream.Position = 0;
                            string entryName = $"image-{imageIndex}.png";
                            ZipArchiveEntry entry = zipArchive.CreateEntry(entryName);
                            using (Stream entryStream = entry.Open())
                            {
                                imageStream.CopyTo(entryStream);
                            }
                            imageIndex++;
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Extracted images saved to {outputZip}");
    }
}