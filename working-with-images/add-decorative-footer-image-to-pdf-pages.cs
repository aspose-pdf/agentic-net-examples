using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string footerImagePath = "footer.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(footerImagePath))
        {
            Console.Error.WriteLine($"Footer image not found: {footerImagePath}");
            return;
        }

        // Load the image with System.Drawing to obtain its original pixel dimensions and DPI.
        using (System.Drawing.Image img = System.Drawing.Image.FromFile(footerImagePath))
        {
            double imgPixelWidth = img.Width;   // pixels
            double imgPixelHeight = img.Height; // pixels
            double imgDpiX = img.HorizontalResolution; // typically 72 or 96

            // Convert pixel dimensions to points (1 point = 1/72 inch).
            double imgWidthPoints = imgPixelWidth * 72.0 / imgDpiX;
            double imgHeightPoints = imgPixelHeight * 72.0 / imgDpiX;

            using (Document doc = new Document(inputPath))
            {
                // Iterate pages (1‑based indexing required by Aspose.Pdf)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];
                    double pageWidth = page.PageInfo.Width; // page width in points

                    // Scale the image so its width matches the page width while preserving aspect ratio.
                    double scale = pageWidth / imgWidthPoints;
                    double scaledHeight = imgHeightPoints * scale;

                    // Create an ImageStamp for the footer.
                    ImageStamp stamp = new ImageStamp(footerImagePath)
                    {
                        Width = pageWidth,          // stretch to full page width
                        Height = scaledHeight,      // maintain aspect ratio
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        BottomMargin = 0,
                        TopMargin = 0
                    };

                    // Add the stamp to the current page.
                    page.AddStamp(stamp);
                }

                // Save the modified PDF.
                doc.Save(outputPath);
            }
        }

        Console.WriteLine($"Footer added to each page. Saved as '{outputPath}'.");
    }
}
