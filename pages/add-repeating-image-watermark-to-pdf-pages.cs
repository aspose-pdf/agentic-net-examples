using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "watermarked.pdf";
        const string watermarkImagePath = "watermark.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImagePath}");
            return;
        }

        // Load the watermark image using System.Drawing to obtain its size in points (1 point = 1/72 inch)
        double imgWidth, imgHeight;
        using (System.Drawing.Image img = System.Drawing.Image.FromFile(watermarkImagePath))
        {
            // Convert pixel dimensions to points based on image DPI
            imgWidth = img.Width * 72.0 / img.HorizontalResolution;
            imgHeight = img.Height * 72.0 / img.VerticalResolution;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Page dimensions in points
                double pageWidth = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Determine how many stamps are needed to cover the page
                int cols = (int)Math.Ceiling(pageWidth / imgWidth);
                int rows = (int)Math.Ceiling(pageHeight / imgHeight);

                // Place a stamp at each grid cell
                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        ImageStamp stamp = new ImageStamp(watermarkImagePath)
                        {
                            // Position the stamp
                            XIndent = col * imgWidth,
                            YIndent = row * imgHeight,
                            // Preserve original image size
                            Width = imgWidth,
                            Height = imgHeight,
                            // Render behind existing content
                            Background = true,
                            // Make the watermark semi‑transparent
                            Opacity = 0.2
                        };

                        // Add the stamp to the current page
                        page.AddStamp(stamp);
                    }
                }
            }

            // Save the modified PDF (still inside the using block)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}
