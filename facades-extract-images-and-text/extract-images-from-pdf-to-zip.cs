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
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Initialize the extractor and bind the PDF file
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(inputPdf);
                extractor.ExtractImage(); // Prepare image extraction

                // Create the ZIP archive
                using (FileStream zipFile = new FileStream(outputZip, FileMode.Create))
                using (ZipArchive zip = new ZipArchive(zipFile, ZipArchiveMode.Create))
                {
                    int imageIndex = 1;

                    // Iterate through all extracted images
                    while (extractor.HasNextImage())
                    {
                        // Retrieve the next image into a memory stream
                        using (MemoryStream imgStream = new MemoryStream())
                        {
                            extractor.GetNextImage(imgStream);
                            imgStream.Position = 0; // Reset for reading

                            // Add the image to the ZIP archive
                            string entryName = $"image-{imageIndex}.jpg";
                            ZipArchiveEntry entry = zip.CreateEntry(entryName);
                            using (Stream entryStream = entry.Open())
                            {
                                imgStream.CopyTo(entryStream);
                            }
                        }

                        imageIndex++;
                    }
                }
            }

            Console.WriteLine($"All images extracted to '{outputZip}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}