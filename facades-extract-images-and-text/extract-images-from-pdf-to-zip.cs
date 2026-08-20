using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "sample.pdf";          // source PDF
        const string outputZipPath = "extracted_images.zip"; // destination ZIP

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Initialize the extractor and bind the PDF
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPdfPath);
                extractor.ExtractImage(); // prepare image extraction

                // Create the ZIP archive for the extracted images
                using (FileStream zipFileStream = new FileStream(outputZipPath, FileMode.Create))
                using (ZipArchive zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
                {
                    int imageIndex = 1;

                    // Iterate over all images in the PDF
                    while (extractor.HasNextImage())
                    {
                        // Extract the current image into a memory stream (default format is JPEG)
                        using (MemoryStream imageStream = new MemoryStream())
                        {
                            extractor.GetNextImage(imageStream);
                            imageStream.Position = 0; // reset for reading

                            // Add the image as a new entry in the ZIP archive
                            string entryName = $"image_{imageIndex}.jpg";
                            ZipArchiveEntry zipEntry = zipArchive.CreateEntry(entryName, CompressionLevel.Optimal);
                            using (Stream entryStream = zipEntry.Open())
                            {
                                imageStream.CopyTo(entryStream);
                            }
                        }

                        imageIndex++;
                    }
                }
            }

            Console.WriteLine($"All images extracted to ZIP: {outputZipPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}