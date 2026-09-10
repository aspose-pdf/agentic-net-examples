using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using Aspose.Pdf;

// Alias ambiguous types to avoid CS0104 errors
using PdfImage = Aspose.Pdf.XImage; // XImage is the correct type for raster images
using SysImage = System.Drawing.Image;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Load the PDF document using the core Document API (no Facades)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            int imageCounter = 1;

            // Iterate through each page and its image resources
            foreach (Page page in pdfDoc.Pages)
            {
                // page.Resources.Images is an ImageCollection, iterate directly
                foreach (PdfImage pdfImg in page.Resources.Images)
                {
                    // Save the Aspose.Pdf.XImage to a memory stream first
                    using (MemoryStream ms = new MemoryStream())
                    {
                        pdfImg.Save(ms);
                        ms.Position = 0;

                        // Load the stream into a System.Drawing.Image so we can export as PNG
                        using (SysImage sysImg = SysImage.FromStream(ms))
                        {
                            string outputPath = Path.Combine(outputFolder, $"image_{imageCounter}.png");
                            sysImg.Save(outputPath, ImageFormat.Png);
                        }
                    }

                    imageCounter++;
                }
            }
        }

        Console.WriteLine("All raster images have been extracted and saved as PNG files.");
    }
}
