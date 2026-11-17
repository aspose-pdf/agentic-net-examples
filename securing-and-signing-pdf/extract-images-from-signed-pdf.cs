using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;

class ExtractImages
{
    static void Main()
    {
        const string inputPdf = "signed_input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Pages are 1‑based in Aspose.Pdf
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate over all images on the page
                int imgIndex = 1;
                foreach (XImage img in page.Resources.Images)
                {
                    // Build a file name: Page{page}_Img{index}.png
                    string fileName = $"Page{pageNum}_Img{imgIndex}.png";
                    string outPath = Path.Combine(outputDir, fileName);

                    // Save the image to a file stream. XImage.Save expects a Stream, not a file path.
                    using (FileStream fs = File.Create(outPath))
                    {
                        img.Save(fs, ImageFormat.Png);
                    }

                    Console.WriteLine($"Saved image to {outPath}");
                    imgIndex++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
