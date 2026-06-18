using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ImageExtractorWithManifest
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string imagesFolder = "ExtractedImages";
        const string csvManifestPath = "manifest.csv";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        Directory.CreateDirectory(imagesFolder);

        // List to hold CSV rows: FileName,PageNumber,Width,Height
        List<string> csvRows = new List<string>();
        csvRows.Add("FileName,PageNumber,Width,Height");

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdfPath))
        // Initialize the PdfExtractor (facade)
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the document to the extractor
            extractor.BindPdf(doc);

            int globalImageIndex = 1;

            // Iterate over each page (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Restrict extraction to the current page
                extractor.StartPage = pageNum;
                extractor.EndPage   = pageNum;

                // Perform the extraction for this page
                extractor.ExtractImage();

                // Retrieve all images found on this page
                while (extractor.HasNextImage())
                {
                    // Build a unique file name that includes the page number
                    string imageFileName = $"image_{pageNum}_{globalImageIndex}.png";
                    string imagePath = Path.Combine(imagesFolder, imageFileName);

                    // Save the image (PNG format)
                    extractor.GetNextImage(imagePath, ImageFormat.Png);

                    // Determine image dimensions using System.Drawing.Image (fully qualified)
                    using (System.Drawing.Image img = System.Drawing.Image.FromFile(imagePath))
                    {
                        int width  = img.Width;
                        int height = img.Height;

                        // Add a row to the CSV manifest
                        csvRows.Add($"{imageFileName},{pageNum},{width},{height}");
                    }

                    globalImageIndex++;
                }
            }
        }

        // Write the CSV manifest
        File.WriteAllLines(csvManifestPath, csvRows);
        Console.WriteLine($"Extraction complete. Images saved to '{imagesFolder}'.");
        Console.WriteLine($"CSV manifest written to '{csvManifestPath}'.");
    }
}
