using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;

#pragma warning disable CS8618 // Suppress non‑nullable field warnings for generated classes

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // List to hold resolution info for each image
            var imageInfos = new List<ImageResolutionInfo>();

            // Iterate over all pages (pages are 1‑based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Iterate the XImage collection directly (not a dictionary)
                foreach (XImage img in page.Resources.Images)
                {
                    // Width and Height are in points (1/72 inch). Convert to pixels if needed.
                    // Older Aspose.Pdf versions do not expose DPI via ImageInfo, so we default to 0.
                    var info = new ImageResolutionInfo
                    {
                        PageNumber = i,
                        WidthPoints = img.Width,
                        HeightPoints = img.Height,
                        DpiX = 0, // DPI not available in this API version
                        DpiY = 0  // DPI not available in this API version
                    };
                    imageInfos.Add(info);
                }
            }

            // Report the collected metadata
            Console.WriteLine("Image Resolution Report:");
            foreach (var info in imageInfos)
            {
                Console.WriteLine(
                    $"Page {info.PageNumber}: Width={info.WidthPoints}pt, Height={info.HeightPoints}pt, " +
                    $"DPI X={info.DpiX}, DPI Y={info.DpiY}");
            }
        }
    }

    // Simple DTO to hold image resolution data
    private class ImageResolutionInfo
    {
        public int PageNumber { get; set; }
        public double WidthPoints { get; set; }
        public double HeightPoints { get; set; }
        public int DpiX { get; set; }
        public int DpiY { get; set; }
    }
}
#pragma warning restore CS8618 // Re‑enable the warning for the rest of the project