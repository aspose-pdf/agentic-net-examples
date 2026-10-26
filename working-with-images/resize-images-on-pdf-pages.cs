using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

namespace PdfImageResizer
{
    // Represents the target dimensions for images on a specific page.
    public class PageImageSize
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }

    class Program
    {
        static void Main()
        {
            const string inputPdfPath = "input.pdf";
            const string outputPdfPath = "output_resized.pdf";
            const string configPath = "image_sizes.json";

            if (!File.Exists(inputPdfPath))
            {
                Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
                return;
            }

            if (!File.Exists(configPath))
            {
                Console.Error.WriteLine($"Configuration file not found: {configPath}");
                return;
            }

            // Load configuration: a JSON object where keys are page numbers (as strings) and values are width/height.
            // Example:
            // {
            //   "1": { "Width": 200, "Height": 150 },
            //   "3": { "Width": 300, "Height": 250 }
            // }
            Dictionary<string, PageImageSize> config;
            try
            {
                string json = File.ReadAllText(configPath);
                config = JsonSerializer.Deserialize<Dictionary<string, PageImageSize>>(json);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to read configuration: {ex.Message}");
                return;
            }

            // Open the PDF document inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPdfPath))
            {
                // Iterate pages using 1‑based indexing (Aspose.Pdf requirement).
                for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
                {
                    // Check if a size is defined for this page.
                    if (!config.TryGetValue(pageNumber.ToString(), out PageImageSize targetSize))
                        continue; // No resizing needed for this page.

                    Page page = doc.Pages[pageNumber];

                    // Resize raster images that are placed on the page via the high‑level Image class.
                    // The Image class (Aspose.Pdf.Drawing.Image) does not expose Width/Height properties.
                    // Use FixWidth and FixHeight to set the desired dimensions.
                    foreach (var img in page.Paragraphs.OfType<Image>())
                    {
                        img.FixWidth = targetSize.Width;
                        img.FixHeight = targetSize.Height;
                    }
                }

                // Save the modified PDF. No SaveOptions needed for PDF output.
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Resized PDF saved to '{outputPdfPath}'.");
        }
    }
}
