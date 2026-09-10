using System;
using System.IO;
using System.Drawing; // retained for potential future use (e.g., Color)
using Aspose.Pdf;

// Alias the System.Drawing.Image type to avoid ambiguity with Aspose.Pdf.Image
using SysImage = System.Drawing.Image;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath     = "logo.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the source PDF (using rule: document-disposal-with-using)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Load the image once to obtain its original dimensions
            using (SysImage img = SysImage.FromFile(imagePath))
            {
                double imgOrigWidth  = img.Width;   // pixels
                double imgOrigHeight = img.Height;  // pixels

                // Iterate over all pages (1‑based indexing per rule)
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    Page page = pdfDoc.Pages[pageNum];

                    // Page width in points (1 point = 1/72 inch)
                    double pageWidth = page.PageInfo.Width;

                    // Compute scaling factor to fit the image to the full page width
                    double scale = pageWidth / imgOrigWidth;

                    // Create the image stamp
                    ImageStamp imgStamp = new ImageStamp(imagePath)
                    {
                        // Preserve aspect ratio by applying the same scale to both dimensions
                        Width  = imgOrigWidth  * scale,
                        Height = imgOrigHeight * scale,

                        // Place the stamp at the top of the page (YIndent measured from bottom)
                        YIndent = page.PageInfo.Height - (imgOrigHeight * scale),

                        // Center horizontally
                        HorizontalAlignment = HorizontalAlignment.Center
                    };

                    // Add the stamp to the current page
                    page.AddStamp(imgStamp);
                }
            }

            // Save the modified PDF (standard save, no extra SaveOptions needed)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdfPath}'.");
    }
}