using System;
using System.IO;
using System.IO.Compression;
using System.Drawing.Imaging; // Added for ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Facades; // PdfExtractor, ImageFormat (Aspose enum)

class Program
{
    static void Main()
    {
        // -----------------------------------------------------------------
        // 1. Create a self‑contained sample PDF (input.pdf) that contains at least
        //    one image. This guarantees the example runs in the sandbox where no
        //    external files exist.
        // -----------------------------------------------------------------
        const string pdfPath = "input.pdf";
        CreateSamplePdfWithImage(pdfPath);

        // -----------------------------------------------------------------
        // 2. Directory where extracted PNG images will be saved
        // -----------------------------------------------------------------
        const string outputDir = "ExtractedImages";
        Directory.CreateDirectory(outputDir);

        // -----------------------------------------------------------------
        // 3. Extract images from the PDF using PdfExtractor (Facades API)
        // -----------------------------------------------------------------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(pdfPath);
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string pngFile = Path.Combine(outputDir, $"image-{imageIndex}.png");
                // Use System.Drawing.Imaging.ImageFormat for PNG output.
                extractor.GetNextImage(pngFile, ImageFormat.Png);
                imageIndex++;
            }
        }

        // -----------------------------------------------------------------
        // 4. Compress the extracted PNG files using a lossless ZIP archive
        // -----------------------------------------------------------------
        const string zipPath = "ExtractedImages.zip";
        using (FileStream zipStream = new FileStream(zipPath, FileMode.Create))
        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
        {
            foreach (string pngFile in Directory.GetFiles(outputDir, "*.png"))
            {
                archive.CreateEntryFromFile(pngFile, Path.GetFileName(pngFile), CompressionLevel.Optimal);
            }
        }

        Console.WriteLine("Image extraction and lossless compression completed successfully.");
    }

    /// <summary>
    /// Generates a minimal PDF containing a single embedded PNG image.
    /// The PNG data is created from a hard‑coded 1×1 pixel red image (base64).
    /// </summary>
    private static void CreateSamplePdfWithImage(string path)
    {
        // 1×1 red PNG (base64 encoded). This avoids any System.Drawing usage.
        const string base64Png = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK6cAAAAASUVORK5CYII=";
        byte[] pngBytes = Convert.FromBase64String(base64Png);

        using (MemoryStream imgStream = new MemoryStream(pngBytes))
        {
            // Create a new PDF document.
            Document doc = new Document();
            Page page = doc.Pages.Add();

            // Add the image to the page.
            Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
            {
                ImageStream = imgStream,
                // Position the image at (100,500) with its original size.
                FixWidth = 100,
                FixHeight = 100,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            page.Paragraphs.Add(pdfImage);

            // Save the PDF so the extractor can work on it.
            doc.Save(path);
        }
    }
}
