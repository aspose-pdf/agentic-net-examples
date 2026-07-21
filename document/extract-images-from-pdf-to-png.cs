using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF document (using the standard Document constructor)
        using (Document pdfDoc = new Document(inputPdf))
        {
            int imageCounter = 1;

            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Iterate over the image resources of the page
                foreach (XImage xImg in page.Resources.Images)
                {
                    // Build a unique file name for each extracted image
                    string outPath = Path.Combine(outputDir, $"image_{imageCounter}_page{pageNum}.png");

                    // Preserve the original resolution by using the image's native size.
                    // The XImage class provides a Save method that writes the image data
                    // directly to a file in its original format. To ensure PNG output,
                    // we render the XImage onto a PNG device with the image's own dimensions.
                    int width  = xImg.Width;
                    int height = xImg.Height;

                    // Create a PNG device with the image's native dimensions.
                    // Resolution is set to 72 DPI (default) because we are preserving the
                    // pixel dimensions exactly; no scaling is applied.
                    PngDevice pngDevice = new PngDevice(width, height);

                    // Render the XImage onto a temporary PDF page to use the device.
                    // Create a temporary one‑page PDF containing only the image.
                    using (Document tempDoc = new Document())
                    {
                        tempDoc.Pages.Add();
                        // Add the XImage to the temporary page.
                        tempDoc.Pages[1].Resources.Images.Add(xImg);
                        // Place the image at (0,0) covering the whole page.
                        tempDoc.Pages[1].Contents.Add(new Aspose.Pdf.Operators.GSave());
                        tempDoc.Pages[1].Contents.Add(new Aspose.Pdf.Operators.Do(xImg.Name));
                        tempDoc.Pages[1].Contents.Add(new Aspose.Pdf.Operators.GRestore());

                        // Save the temporary page as PNG.
                        using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                        {
                            pngDevice.Process(tempDoc.Pages[1], outStream);
                        }
                    }

                    Console.WriteLine($"Extracted image saved to: {outPath}");
                    imageCounter++;
                }
            }
        }
    }
}