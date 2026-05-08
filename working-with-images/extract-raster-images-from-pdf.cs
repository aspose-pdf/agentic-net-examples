using System;
using System.IO;
using Aspose.Pdf;                     // Core Aspose.Pdf namespace
using System.Drawing.Imaging;        // For ImageFormat (PNG)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";               // Source PDF file
        const string outputDir = "ExtractedImages";         // Folder for PNG files

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create output directory if it does not exist
        Directory.CreateDirectory(outputDir);

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdf))
        {
            int imageIndex = 1; // Counter for naming output files

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            foreach (Page page in pdfDoc.Pages)
            {
                // Iterate over each image resource on the current page
                foreach (XImage xImg in page.Resources.Images)
                {
                    // Build a unique file name for each extracted image
                    string outPath = Path.Combine(outputDir, $"image_{imageIndex}.png");

                    // Save the XImage to a PNG file
                    using (FileStream outStream = new FileStream(outPath, FileMode.Create))
                    {
                        xImg.Save(outStream, ImageFormat.Png);
                    }

                    Console.WriteLine($"Saved image {imageIndex} → {outPath}");
                    imageIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}