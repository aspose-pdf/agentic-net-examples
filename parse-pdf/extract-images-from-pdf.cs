using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure the output folder exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];
                int imageIndex = 1;

                // Iterate through all images on the page
                foreach (XImage img in page.Resources.Images)
                {
                    string fileName = $"page{pageNum}_img{imageIndex}.png";
                    string outPath = Path.Combine(outputDir, fileName);

                    // Save the XImage to a memory stream, then re‑encode as PNG using System.Drawing
                    using (var ms = new MemoryStream())
                    {
                        img.Save(ms); // XImage.Save accepts a Stream
                        ms.Position = 0;
                        // Fully qualify System.Drawing.Image to avoid ambiguity with Aspose.Pdf.Image
                        using (System.Drawing.Image sysImg = System.Drawing.Image.FromStream(ms))
                        {
                            sysImg.Save(outPath, ImageFormat.Png);
                        }
                    }

                    imageIndex++;
                }
            }
        }

        Console.WriteLine($"All images have been extracted to '{outputDir}'.");
    }
}
