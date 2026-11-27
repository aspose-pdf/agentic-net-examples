using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string zipPath = "table_borders.zip";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        // Create (or overwrite) the ZIP archive
        using (FileStream zipFileStream = new FileStream(zipPath, FileMode.Create))
        using (ZipArchive zip = new ZipArchive(zipFileStream, ZipArchiveMode.Update))
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                int imageIndex = 0;

                // Iterate through all pages
                foreach (Page page in doc.Pages)
                {
                    // Iterate through all images on the page
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Save the image to a memory stream (no ImageInfo available in core API)
                        using (MemoryStream imgStream = new MemoryStream())
                        {
                            img.Save(imgStream); // XImage.Save accepts a Stream
                            imgStream.Position = 0;

                            // Create a unique entry name inside the ZIP – default to PNG extension
                            string entryName = $"page{page.Number}_image{++imageIndex}{GetExtension(img)}";

                            // Add the image to the ZIP archive
                            ZipArchiveEntry entry = zip.CreateEntry(entryName, CompressionLevel.Optimal);
                            using (Stream entryStream = entry.Open())
                            {
                                imgStream.CopyTo(entryStream);
                            }
                        }
                    }
                }
            }
        }

        Console.WriteLine($"ZIP archive created: {zipPath}");
    }

    // Helper that returns a safe file extension for extracted images.
    // The core Aspose.Pdf XImage does not expose format information, so we default to PNG.
    private static string GetExtension(XImage img)
    {
        // If future versions expose format info, you could extend this method.
        return ".png";
    }
}
