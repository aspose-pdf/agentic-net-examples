using System;
using System.IO;
using System.IO.Compression;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths
        const string inputPdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";
        const string imagesOutputDir = "ExtractedImages";

        // Verify input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the directory for extracted images exists
        Directory.CreateDirectory(imagesOutputDir);

        // ---------- Convert PDF to PPTX using Aspose.Pdf only ----------
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Directly save as PPTX – no Aspose.Slides required
            pdfDoc.Save(outputPptxPath, SaveFormat.Pptx);
        }

        // ---------- Extract images from the generated PPTX ----------
        // A PPTX file is a ZIP archive; images are stored under "ppt/media"
        using (ZipArchive archive = ZipFile.OpenRead(outputPptxPath))
        {
            int imageCounter = 1;
            foreach (ZipArchiveEntry entry in archive.Entries)
            {
                // Look for image files inside the PPTX package
                if (entry.FullName.StartsWith("ppt/media/", StringComparison.OrdinalIgnoreCase) &&
                    IsImageFile(entry.Name))
                {
                    string extension = Path.GetExtension(entry.Name);
                    string destPath = Path.Combine(imagesOutputDir, $"image_{imageCounter}{extension}");
                    entry.ExtractToFile(destPath, overwrite: true);
                    imageCounter++;
                }
            }
        }

        Console.WriteLine("PDF successfully converted to PPTX and images extracted.");
    }

    // Helper to determine if a file name represents a common image format
    static bool IsImageFile(string fileName)
    {
        string ext = Path.GetExtension(fileName).ToLowerInvariant();
        return ext == ".png" || ext == ".jpg" || ext == ".jpeg" ||
               ext == ".bmp" || ext == ".gif" || ext == ".tif" || ext == ".tiff";
    }
}