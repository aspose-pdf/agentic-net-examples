using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.Json;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for ImagePlacementAbsorber

// Configuration model: target width and height for a specific page
public class PageImageSizeConfig
{
    public int Width { get; set; }
    public int Height { get; set; }
}

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string configFilePath = "resizeConfig.json";   // JSON file with page‑to‑size mapping
        const string outputPdfPath  = "output_resized.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(configFilePath))
        {
            Console.Error.WriteLine($"Config file not found: {configFilePath}");
            return;
        }

        // Load configuration: {"1":{"Width":300,"Height":200},"3":{"Width":400,"Height":300}}
        Dictionary<int, PageImageSizeConfig> resizeConfig;
        try
        {
            string json = File.ReadAllText(configFilePath);
            resizeConfig = JsonSerializer.Deserialize<Dictionary<int, PageImageSizeConfig>>(json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read config: {ex.Message}");
            return;
        }

        // Open the PDF document (lifecycle rule: wrap in using)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                if (!resizeConfig.TryGetValue(pageNum, out PageImageSizeConfig targetSize))
                    continue; // No resizing required for this page

                // Absorb image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                pdfDoc.Pages[pageNum].Accept(absorber);

                foreach (ImagePlacement placement in absorber.ImagePlacements)
                {
                    // Extract the original image into a memory stream
                    using (MemoryStream originalStream = new MemoryStream())
                    {
                        placement.Image.Save(originalStream, ImageFormat.Png);
                        originalStream.Position = 0;

                        // Load the image into a Bitmap for resizing
                        using (Bitmap originalBitmap = new Bitmap(originalStream))
                        {
                            // Create a resized bitmap with the target dimensions
                            using (Bitmap resizedBitmap = new Bitmap(originalBitmap, targetSize.Width, targetSize.Height))
                            {
                                // Save the resized bitmap back to a stream
                                using (MemoryStream resizedStream = new MemoryStream())
                                {
                                    resizedBitmap.Save(resizedStream, ImageFormat.Png);
                                    resizedStream.Position = 0;

                                    // Replace the image in the PDF with the resized version
                                    placement.Replace(resizedStream);
                                }
                            }
                        }
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: Save inside using block)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPdfPath}'.");
    }
}