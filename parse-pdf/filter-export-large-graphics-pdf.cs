using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";
        const double minArea = 5000.0; // Minimum area (points²) to keep an image

        // Verify source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output folder exists
        Directory.CreateDirectory(outputDir);

        // Wrap Document in a using block (lifecycle rule)
        using (Document doc = new Document(inputPdf))
        {
            // Pages are 1‑based (page-indexing-one-based rule)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                int imageCounter = 0;

                // XImageCollection is iterable directly (ximage-collection-foreach-only-no-keys rule)
                foreach (XImage img in page.Resources.Images)
                {
                    imageCounter++;

                    // Compute bounding rectangle area
                    double area = img.Width * img.Height;

                    // Export only images exceeding the defined area
                    if (area > minArea)
                    {
                        // Use PNG as a generic output format
                        string outPath = Path.Combine(
                            outputDir,
                            $"page{pageNum}_img{imageCounter}.png");

                        // XImage.Save expects a Stream, not a file path.
                        // Open a FileStream and specify the PNG format explicitly.
                        using (FileStream fs = File.Create(outPath))
                        {
                            img.Save(fs, ImageFormat.Png);
                        }

                        Console.WriteLine($"Saved: {outPath} (Area = {area})");
                    }
                }
            }
        }
    }
}
