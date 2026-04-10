using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";               // source PDF
        const string imagesFolder = "ExtractedImages";    // folder for PNGs
        const string zipPath = "ExtractedImages.zip";    // final ZIP archive

        // Verify that the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"Source PDF '{pdfPath}' not found. Operation aborted.");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(imagesFolder);

        // Extract images using Aspose.Pdf.Facades.PdfExtractor
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);          // bind source PDF
            extractor.ExtractImage();            // prepare image extraction

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                // Retrieve the next image into a memory stream
                using (MemoryStream rawImageStream = new MemoryStream())
                {
                    extractor.GetNextImage(rawImageStream);
                    rawImageStream.Position = 0; // reset for reading

                    // Write the raw image bytes directly to a PNG file.
                    // Most PDFs store images in PNG/JPEG format already; if the image is not PNG,
                    // the file will retain its original format but will still be losslessly stored
                    // inside the ZIP archive.
                    string pngFile = Path.Combine(imagesFolder, $"image-{imageIndex}.png");
                    using (FileStream fileStream = new FileStream(pngFile, FileMode.Create, FileAccess.Write))
                    {
                        rawImageStream.CopyTo(fileStream);
                    }
                }
                imageIndex++;
            }
        }

        // Compress the extracted PNG files into a ZIP archive (lossless compression)
        using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
        {
            foreach (string filePath in Directory.GetFiles(imagesFolder, "*.png"))
            {
                ZipArchiveEntry entry = archive.CreateEntry(Path.GetFileName(filePath), CompressionLevel.Optimal);
                using (Stream entryStream = entry.Open())
                using (FileStream imageStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    imageStream.CopyTo(entryStream);
                }
            }
        }

        Console.WriteLine("Image extraction and compression completed.");
    }
}
