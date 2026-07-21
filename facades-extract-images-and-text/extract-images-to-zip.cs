using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // ImageFormat for specifying output image type

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputZipPath = "extracted_images.zip";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Initialize the PDF extractor facade
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF file
            extractor.BindPdf(inputPdfPath);

            // Prepare the ZIP archive for output
            using (FileStream zipFileStream = new FileStream(outputZipPath, FileMode.Create))
            using (ZipArchive zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Create))
            {
                // Extract images one by one and add them to the ZIP
                int imageIndex = 1;
                extractor.ExtractImage(); // Start the extraction process

                while (extractor.HasNextImage())
                {
                    // Create a new entry in the ZIP for each image
                    string entryName = $"image_{imageIndex}.png";
                    ZipArchiveEntry zipEntry = zipArchive.CreateEntry(entryName, CompressionLevel.Optimal);

                    // Write the extracted image directly into the ZIP entry stream
                    using (Stream entryStream = zipEntry.Open())
                    {
                        // Save each image as PNG; you can change ImageFormat if needed
                        extractor.GetNextImage(entryStream, ImageFormat.Png);
                    }

                    imageIndex++;
                }
            }

            // No explicit Save needed for PdfExtractor; resources are released by Dispose()
        }

        Console.WriteLine($"All images extracted to ZIP: {outputZipPath}");
    }
}