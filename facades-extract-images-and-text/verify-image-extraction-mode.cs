using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Prepare a temporary folder for test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "PdfExtractorTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated PDF
        string pdfPath = Path.Combine(tempFolder, "test.pdf");

        // Create a PDF with an image added to the resources but NOT placed on the page
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Minimal 1x1 PNG image (transparent)
            byte[] pngData = Convert.FromBase64String(
                "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+XK6cAAAAASUVORK5CYII="
            );

            // Add the image to the page resources (this defines it in the PDF)
            using (MemoryStream imgStream = new MemoryStream(pngData))
            {
                // The returned name can be ignored; we just need the resource to exist
                page.Resources.Images.Add(imgStream);
            }

            // Save the PDF
            doc.Save(pdfPath);
        }

        // Helper to count extracted images for a given mode
        int CountImages(ExtractImageMode mode)
        {
            int count = 0;
            using (PdfExtractor extractor = new PdfExtractor())
            {
                extractor.BindPdf(pdfPath);
                extractor.ExtractImageMode = mode;
                extractor.ExtractImage();

                while (extractor.HasNextImage())
                {
                    // Retrieve the image into a memory stream (no file I/O needed)
                    using (MemoryStream ms = new MemoryStream())
                    {
                        extractor.GetNextImage(ms);
                    }
                    count++;
                }
            }
            return count;
        }

        // Count images when extracting all defined resources
        int definedCount = CountImages(ExtractImageMode.DefinedInResources);

        // Count images when extracting only actually used images
        int actuallyUsedCount = CountImages(ExtractImageMode.ActuallyUsed);

        // Verify the behavior
        if (definedCount != 1)
            throw new Exception($"Expected 1 image in DefinedInResources mode, but got {definedCount}.");

        if (actuallyUsedCount != 0)
            throw new Exception($"Expected 0 images in ActuallyUsed mode, but got {actuallyUsedCount}.");

        Console.WriteLine("PdfExtractor ImageExtractionMode test passed.");
        // Cleanup temporary files
        try { Directory.Delete(tempFolder, true); } catch { /* ignore cleanup errors */ }
    }
}