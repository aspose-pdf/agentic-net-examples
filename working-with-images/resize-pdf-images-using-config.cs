using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Drawing;                     // Used for image resizing (Windows‑only)
using System.Drawing.Imaging;            // For ImageFormat
using Aspose.Pdf;
using Aspose.Pdf.Devices;                // For image devices if needed
using Aspose.Pdf.Text;                   // Not required here but safe
using Aspose.Pdf.LogicalStructure;       // Not required for this task

// Alias to disambiguate System.Drawing.Image from Aspose.Pdf.Image
using SysImage = System.Drawing.Image;

// Configuration model: target width and height (in PDF points) for a given page
public class PageSizeConfig
{
    public double Width { get; set; }
    public double Height { get; set; }
}

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output_resized.pdf";
        const string configFilePath = "resizeConfig.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(configFilePath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configFilePath}");
            return;
        }

        // Load configuration: JSON object where keys are page numbers (as strings)
        // Example:
        // {
        //   "1": { "Width": 200, "Height": 300 },
        //   "2": { "Width": 150, "Height": 250 }
        // }
        var configJson = File.ReadAllText(configFilePath);
        var rawConfig = JsonSerializer.Deserialize<Dictionary<string, PageSizeConfig>>(configJson) ?? new Dictionary<string, PageSizeConfig>();
        var pageConfig = new Dictionary<int, PageSizeConfig>();
        foreach (var kvp in rawConfig)
        {
            if (int.TryParse(kvp.Key, out int pageNum))
                pageConfig[pageNum] = kvp.Value;
        }

        // Open the PDF document (lifecycle rule: wrap in using)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all pages (1‑based indexing rule)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                if (!pageConfig.TryGetValue(pageIndex, out PageSizeConfig targetSize))
                    continue; // No resizing required for this page

                Page page = pdfDoc.Pages[pageIndex];

                // Absorb image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                // Process each image found on the page
                foreach (ImagePlacement imgPlacement in absorber.ImagePlacements)
                {
                    // Preserve original rectangle (position) – we will reposition with new size
                    var originalRect = imgPlacement.Rectangle;

                    // Extract the original image into a memory stream
                    using (MemoryStream originalStream = new MemoryStream())
                    {
                        imgPlacement.Image.Save(originalStream, ImageFormat.Png);
                        originalStream.Position = 0;

                        // Load into System.Drawing.Image for resizing (disambiguated via alias)
                        using (var originalImage = SysImage.FromStream(originalStream))
                        {
                            // Resize to target dimensions (width/height are in PDF points;
                            // convert points to pixels assuming 72 DPI for simplicity)
                            int targetPixelWidth  = (int)Math.Round(targetSize.Width);
                            int targetPixelHeight = (int)Math.Round(targetSize.Height);

                            using (Bitmap resizedBitmap = new Bitmap(targetPixelWidth, targetPixelHeight))
                            {
                                using (var graphics = Graphics.FromImage(resizedBitmap))
                                {
                                    graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                    graphics.DrawImage(originalImage, 0, 0, targetPixelWidth, targetPixelHeight);
                                }

                                // Encode resized bitmap back to a stream (PNG)
                                using (MemoryStream resizedStream = new MemoryStream())
                                {
                                    resizedBitmap.Save(resizedStream, ImageFormat.Png);
                                    resizedStream.Position = 0;

                                    // Hide the original image placement
                                    imgPlacement.Hide();

                                    // Define new rectangle based on original lower‑left corner
                                    double llx = originalRect.LLX;
                                    double lly = originalRect.LLY;
                                    double urx = llx + targetSize.Width;
                                    double ury = lly + targetSize.Height;
                                    Aspose.Pdf.Rectangle newRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

                                    // Add the resized image to the page at the new rectangle
                                    page.AddImage(resizedStream, newRect);
                                }
                            }
                        }
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: Save inside using)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPdfPath}'.");
    }
}
