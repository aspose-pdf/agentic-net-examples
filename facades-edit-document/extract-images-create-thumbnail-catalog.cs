using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string catalogPdfPath = "catalog.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Temporary folder for extracted images
        string tempFolder = Path.Combine(Path.GetTempPath(), "PdfImageExtract_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        var extractedImagePaths = new List<string>();

        // ---------- Extract images from the source PDF ----------
        using (PdfExtractor extractor = new PdfExtractor())
        {
            extractor.BindPdf(inputPdfPath);
            extractor.ExtractImage();

            int imageIndex = 1;
            while (extractor.HasNextImage())
            {
                string imagePath = Path.Combine(tempFolder, $"image_{imageIndex}.png");
                // Save the extracted image (default format)
                extractor.GetNextImage(imagePath);
                extractedImagePaths.Add(imagePath);
                imageIndex++;
            }
        }

        // ---------- Build a new PDF catalog with thumbnails ----------
        using (Document catalogDoc = new Document())
        {
            foreach (string imgPath in extractedImagePaths)
            {
                // Add a new page for each thumbnail
                Page page = catalogDoc.Pages.Add();

                // Add the image as a thumbnail
                Aspose.Pdf.Image img = new Aspose.Pdf.Image();
                img.File = imgPath;
                img.FixWidth = 200; // thumbnail width (height scales proportionally)
                page.Paragraphs.Add(img);

                // Optional: add a caption with the image file name
                TextFragment caption = new TextFragment(Path.GetFileName(imgPath));
                caption.Position = new Aspose.Pdf.Text.Position(10, 10);
                caption.TextState.FontSize = 12;
                // Fully‑qualified Aspose.Pdf.Color to avoid ambiguity with System.Drawing.Color
                caption.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                page.Paragraphs.Add(caption);
            }

            // Save the catalog PDF
            catalogDoc.Save(catalogPdfPath);
        }

        // Clean up temporary images
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // If cleanup fails, ignore – temporary files will be removed by the OS later.
        }

        Console.WriteLine($"Image catalog created at '{catalogPdfPath}'.");
    }
}
