using System;
using System.IO;
using System.IO.Compression;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputZipPath = "images.zip";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Extract images using Aspose.Pdf.Facades.PdfExtractor
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the source PDF
            extractor.BindPdf(inputPdfPath);
            // Extract images from all pages (or set StartPage/EndPage if needed)
            extractor.ExtractImage();

            // Create the ZIP archive to store extracted images
            using (FileStream zipFileStream = new FileStream(outputZipPath, FileMode.Create))
            using (ZipArchive zipArchive = new ZipArchive(zipFileStream, ZipArchiveMode.Create, leaveOpen: false))
            {
                int imageIndex = 1;
                // Iterate over all extracted images
                while (extractor.HasNextImage())
                {
                    // Retrieve the next image into a memory stream
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        extractor.GetNextImage(imageStream);
                        imageStream.Position = 0; // reset for reading

                        // Determine the image file extension from the image data
                        string extension = GetImageExtension(imageStream);
                        // Reset again after inspection
                        imageStream.Position = 0;

                        string entryName = $"image_{imageIndex}.{extension}";

                        // Add the image as a new entry in the ZIP archive
                        ZipArchiveEntry zipEntry = zipArchive.CreateEntry(entryName);
                        using (Stream entryStream = zipEntry.Open())
                        {
                            imageStream.CopyTo(entryStream);
                        }

                        imageIndex++;
                    }
                }
            }
        }

        Console.WriteLine($"Extraction complete. Images saved to '{outputZipPath}'.");
    }

    /// <summary>
    /// Determines a suitable file extension for an image contained in the supplied stream.
    /// The stream position is left unchanged.
    /// </summary>
    private static string GetImageExtension(Stream imageStream)
    {
        // Ensure the stream can be read as an Image
        try
        {
            using (Image img = Image.FromStream(imageStream, useEmbeddedColorManagement: false, validateImageData: false))
            {
                if (img.RawFormat.Equals(ImageFormat.Jpeg))
                    return "jpg";
                if (img.RawFormat.Equals(ImageFormat.Png))
                    return "png";
                if (img.RawFormat.Equals(ImageFormat.Gif))
                    return "gif";
                if (img.RawFormat.Equals(ImageFormat.Bmp))
                    return "bmp";
                if (img.RawFormat.Equals(ImageFormat.Tiff))
                    return "tiff";
                // Fallback for unknown formats
                return "bin";
            }
        }
        catch
        {
            // If the stream cannot be interpreted as an image, default to a generic binary extension
            return "bin";
        }
    }
}
