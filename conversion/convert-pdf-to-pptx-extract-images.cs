using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file
        const string pdfPath = "input.pdf";
        // Output PPTX file
        const string pptxPath = "output.pptx";
        // Directory to store extracted images
        const string imagesDir = "ExtractedImages";

        // Validate input file
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Ensure the images directory exists
        Directory.CreateDirectory(imagesDir);

        // ---------- Convert PDF to PPTX ----------
        // Use Aspose.Pdf Document to load the PDF and save as PPTX.
        // SaveFormat.Pptx is defined in the Aspose.Pdf namespace; no extra SaveOptions class is required.
        using (Document pdfDoc = new Document(pdfPath))
        {
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
        }

        // ---------- Extract images from the generated PPTX ----------
        // A PPTX file is a ZIP archive. All slide images are stored under the "ppt/media" folder.
        // We can extract them without needing Aspose.Slides.
        using (ZipArchive archive = ZipFile.OpenRead(pptxPath))
        {
            int imageCounter = 1;
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                // Look for entries like "ppt/media/image1.png", "ppt/media/image2.jpeg", etc.
                if (entry.FullName.StartsWith("ppt/media/", StringComparison.OrdinalIgnoreCase) &&
                    (entry.Name.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                     entry.Name.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                     entry.Name.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                     entry.Name.EndsWith(".gif", StringComparison.OrdinalIgnoreCase) ||
                     entry.Name.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase)))
                {
                    string destPath = Path.Combine(imagesDir, $"image_{imageCounter}{Path.GetExtension(entry.Name)}");
                    entry.ExtractToFile(destPath, overwrite: true);
                    Console.WriteLine($"Extracted image saved to: {destPath}");
                    imageCounter++;
                }
            }
        }

        Console.WriteLine("PDF to PPTX conversion and image extraction completed.");
    }
}
