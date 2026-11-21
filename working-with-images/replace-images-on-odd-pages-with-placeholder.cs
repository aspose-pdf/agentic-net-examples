using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string placeholderImgPath = "placeholder.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(placeholderImgPath))
        {
            Console.Error.WriteLine($"Placeholder image not found: {placeholderImgPath}");
            return;
        }

        // Load the source PDF inside a using block (lifecycle rule)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate pages using 1‑based indexing (global rule)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                // Process only odd‑numbered pages
                if (pageNum % 2 == 0) continue;

                Page page = pdfDoc.Pages[pageNum];

                // Collect existing images to a list (cannot modify collection while iterating)
                var existingImages = new List<XImage>();
                foreach (XImage img in page.Resources.Images)
                {
                    existingImages.Add(img);
                }

                // Replace each original image with a placeholder stamp preserving dimensions
                foreach (XImage originalImg in existingImages)
                {
                    // Preserve the original image dimensions (in points)
                    double imgWidth = originalImg.Width;
                    double imgHeight = originalImg.Height;

                    // Create a stamp that uses the placeholder image
                    ImageStamp stamp = new ImageStamp(placeholderImgPath)
                    {
                        // Position the stamp at (0,0). Adjust if exact coordinates are known.
                        XIndent = 0,
                        YIndent = 0,
                        Width = imgWidth,
                        Height = imgHeight,
                        // Draw over existing content
                        Background = false
                    };

                    // Add the placeholder stamp to the current page
                    page.AddStamp(stamp);
                }
            }

            // Save the modified PDF (still PDF format, no special SaveOptions needed)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdfPath}'.");
    }
}
