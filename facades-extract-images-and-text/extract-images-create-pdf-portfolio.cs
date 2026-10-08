using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing.Imaging; // needed for ImageFormat when saving XImage

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string portfolioPath = "portfolio.pdf";
        const string tempImageDir = "temp_images";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure a temporary folder exists for extracted images
        Directory.CreateDirectory(tempImageDir);

        // List to keep track of extracted image file paths
        List<string> extractedImages = new List<string>();

        // Extract images from the source PDF
        using (Document srcDoc = new Document(inputPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            for (int pageNum = 1; pageNum <= srcDoc.Pages.Count; pageNum++)
            {
                Page page = srcDoc.Pages[pageNum];
                int imgCounter = 1;

                // XImageCollection is iterated directly (no dictionary)
                foreach (XImage img in page.Resources.Images)
                {
                    // Save each image as PNG to the temporary folder using a FileStream
                    string imgPath = Path.Combine(
                        tempImageDir,
                        $"page{pageNum}_img{imgCounter}.png");

                    using (FileStream fs = new FileStream(imgPath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs, ImageFormat.Png);
                    }

                    extractedImages.Add(imgPath);
                    imgCounter++;
                }
            }
        }

        // Create a new PDF document that will act as the portfolio
        Document portfolioDoc = new Document();
        // Initialise the collection that holds embedded files (portfolio entries)
        if (portfolioDoc.Collection == null)
            portfolioDoc.Collection = new Collection();

        // Add each extracted image file to the portfolio as an attachment
        foreach (string imgFile in extractedImages)
        {
            var fileSpec = new FileSpecification(imgFile, Path.GetFileName(imgFile))
            {
                // Load the file bytes into the specification
                Contents = new MemoryStream(File.ReadAllBytes(imgFile))
            };
            portfolioDoc.Collection.Add(fileSpec);
        }

        // Optional: give the portfolio a title/description
        portfolioDoc.Info.Title = "Image Portfolio";

        // Save the portfolio PDF
        portfolioDoc.Save(portfolioPath);

        // Clean up temporary image files
        foreach (string imgFile in extractedImages)
        {
            try { File.Delete(imgFile); } catch { /* ignore cleanup errors */ }
        }

        // Remove the temporary directory if empty
        try { Directory.Delete(tempImageDir, true); } catch { /* ignore */ }

        Console.WriteLine($"PDF portfolio created at: {portfolioPath}");
    }
}
