using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";      // source PDF
        const string outputZipPath = "images.zip";    // resulting ZIP archive

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // PdfExtractor implements IDisposable, so wrap it in a using block.
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Load the PDF document.
            extractor.BindPdf(inputPdfPath);

            // Prepare the extractor to retrieve images.
            extractor.ExtractImage();

            // Create the ZIP archive for the extracted images.
            using (FileStream zipFileStream = new FileStream(outputZipPath, FileMode.Create))
            using (ZipArchive zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
            {
                int imageIndex = 1;

                // Iterate over all images in the PDF.
                while (extractor.HasNextImage())
                {
                    // Store each image in a memory stream.
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        // Get the next image; default format is JPEG.
                        extractor.GetNextImage(imageStream);
                        imageStream.Position = 0; // reset for reading

                        // Create a ZIP entry for the image.
                        string entryName = $"image-{imageIndex}.jpg";
                        ZipArchiveEntry entry = zipArchive.CreateEntry(entryName, CompressionLevel.Optimal);

                        // Write the image data into the ZIP entry.
                        using (Stream entryStream = entry.Open())
                        {
                            imageStream.CopyTo(entryStream);
                        }
                    }

                    imageIndex++;
                }
            }

            // No explicit PreSave or additional save call is required.
        }

        Console.WriteLine($"All images extracted to '{outputZipPath}'.");
    }
}