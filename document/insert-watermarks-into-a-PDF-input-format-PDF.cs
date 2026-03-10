using System;
using System.IO;
using System.Drawing;                     // For System.Drawing.Image
using Aspose.Pdf;               // Core Aspose.Pdf namespace

// Alias to disambiguate the Image class between System.Drawing and Aspose.Pdf
using SysImage = System.Drawing.Image;

class Program
{
    static void Main()
    {
        // Input PDF and watermark image paths
        const string inputPdfPath = "input.pdf";
        const string watermarkImgPath = "watermark.png";
        const string outputPdfPath = "watermarked_output.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(watermarkImgPath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImgPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Load the watermark image using System.Drawing.Image (required by Watermark ctor)
            using (SysImage sysImage = SysImage.FromFile(watermarkImgPath))
            {
                // Define the position and size of the watermark on the page.
                // Rectangle constructor: (llx, lly, urx, ury)
                // Adjust these values as needed for your layout.
                Aspose.Pdf.Rectangle watermarkRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

                // Create a Watermark object using the System.Drawing.Image and rectangle
                Watermark pageWatermark = new Watermark(sysImage, watermarkRect);

                // Apply the watermark to every page in the document.
                // Aspose.Pdf uses 1‑based page indexing.
                for (int i = 1; i <= pdfDoc.Pages.Count; i++)
                {
                    pdfDoc.Pages[i].Watermark = pageWatermark;
                }
            }

            // Save the modified PDF. No SaveOptions are needed because the output format is PDF.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}
