using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // required by task specification

class Program
{
    static void Main()
    {
        const string pdfPath  = "input.pdf";
        const string zipPath  = "images.zip";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Create the ZIP archive file
            using (FileStream zipFileStream = new FileStream(zipPath, FileMode.Create, FileAccess.Write))
            using (ZipArchive zip = new ZipArchive(zipFileStream, ZipArchiveMode.Create, leaveOpen: false))
            {
                int imageCounter = 0;

                // Pages are 1‑based in Aspose.Pdf
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Iterate over images; XImageCollection is not a dictionary
                    foreach (XImage img in page.Resources.Images)
                    {
                        imageCounter++;

                        // Generate a unique file name for each image
                        string entryName = $"image_page{pageIndex}_{imageCounter}.png";

                        // Create a new entry in the ZIP archive
                        ZipArchiveEntry entry = zip.CreateEntry(entryName, CompressionLevel.Optimal);

                        // Save the image to the ZIP entry
                        using (Stream entryStream = entry.Open())
                        using (MemoryStream imgStream = new MemoryStream())
                        {
                            // XImage.Save writes the image in its native format
                            img.Save(imgStream);
                            imgStream.Position = 0;
                            imgStream.CopyTo(entryStream);
                        }
                    }
                }
            }
        }

        Console.WriteLine($"All images extracted to ZIP archive: {zipPath}");
    }
}