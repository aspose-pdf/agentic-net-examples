using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;

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

        // Load the PDF document (using the standard Document constructor)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            int imageIndex = 0;

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
            {
                Page page = pdfDoc.Pages[pageNum];

                // Iterate directly over the XImage collection (not as a dictionary)
                foreach (XImage img in page.Resources.Images)
                {
                    imageIndex++;

                    // Build a unique file name for each extracted image
                    string outputPath = Path.Combine(
                        outputFolder,
                        $"page{pageNum}_img{imageIndex}.png");

                    // Save the image as PNG preserving its original resolution.
                    // XImage.Save expects a Stream, so we open a FileStream.
                    using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs, ImageFormat.Png);
                    }
                }
            }
        }

        Console.WriteLine("All images have been extracted to PNG files.");
    }
}
