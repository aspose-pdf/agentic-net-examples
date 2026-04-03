using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Drawing;                     // Used only for image resizing
using Aspose.Pdf;                         // Core PDF API
using Aspose.Pdf.Devices;                 // For image devices (if needed)
using Aspose.Pdf.Text;                    // Not required here but kept for completeness

class Program
{
    // Configuration entry for a single page
    private class PageSizeConfig
    {
        public double Width { get; set; }   // Target width in points (1/72 inch)
        public double Height { get; set; }  // Target height in points
    }

    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output_resized.pdf";
        const string configJsonPath = "page_sizes.json";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(configJsonPath))
        {
            Console.Error.WriteLine($"Configuration file not found: {configJsonPath}");
            return;
        }

        // Load configuration: {"1":{"Width":200,"Height":300},"3":{"Width":400,"Height":500},...}
        Dictionary<int, PageSizeConfig> pageSizeMap;
        try
        {
            string json = File.ReadAllText(configJsonPath);
            pageSizeMap = JsonSerializer.Deserialize<Dictionary<int, PageSizeConfig>>(json);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
            return;
        }

        // Open the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                if (!pageSizeMap.TryGetValue(pageIndex, out PageSizeConfig targetSize))
                    continue; // No resizing required for this page

                Page page = pdfDoc.Pages[pageIndex];

                // Absorb all image placements on the current page
                ImagePlacementAbsorber absorber = new ImagePlacementAbsorber();
                page.Accept(absorber);

                // Process each found image
                foreach (ImagePlacement imgPlacement in absorber.ImagePlacements)
                {
                    // Preserve original position (lower‑left corner)
                    double llx = imgPlacement.Rectangle.LLX;
                    double lly = imgPlacement.Rectangle.LLY;

                    // Retrieve the original image bytes
                    using (MemoryStream originalStream = new MemoryStream())
                    {
                        imgPlacement.Image.Save(originalStream, System.Drawing.Imaging.ImageFormat.Png);
                        originalStream.Position = 0;

                        // Resize the image using System.Drawing
                        using (System.Drawing.Image originalImage = System.Drawing.Image.FromStream(originalStream))
                        using (Bitmap resizedBitmap = new Bitmap((int)targetSize.Width, (int)targetSize.Height))
                        using (Graphics graphics = Graphics.FromImage(resizedBitmap))
                        {
                            graphics.DrawImage(originalImage, 0, 0, (int)targetSize.Width, (int)targetSize.Height);
                            using (MemoryStream resizedStream = new MemoryStream())
                            {
                                resizedBitmap.Save(resizedStream, System.Drawing.Imaging.ImageFormat.Png);
                                resizedStream.Position = 0;

                                // Hide the original image placement
                                imgPlacement.Hide();

                                // Add the resized image back to the page at the original location
                                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(
                                    llx,
                                    lly,
                                    llx + targetSize.Width,
                                    lly + targetSize.Height);

                                page.AddImage(resizedStream, rect);
                            }
                        }
                    }
                }
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPdfPath}'.");
    }
}
