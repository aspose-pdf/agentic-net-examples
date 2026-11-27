using System;
using System.IO;
using Aspose.Pdf;
using System.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputDir = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Page indexing is 1‑based; page 2 is at index 2
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document has fewer than 2 pages.");
                return;
            }

            Page pageTwo = doc.Pages[2];

            int imageIndex = 1;
            // Iterate over the image resources of page two
            foreach (XImage img in pageTwo.Resources.Images)
            {
                string outPath = Path.Combine(outputDir, $"image_{imageIndex}.jpg");
                // Save the embedded image as JPEG using System.Drawing.Imaging.ImageFormat
                using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                {
                    img.Save(fs, ImageFormat.Jpeg);
                }
                Console.WriteLine($"Saved image {imageIndex} to {outPath}");
                imageIndex++;
            }

            if (imageIndex == 1)
                Console.WriteLine("No images found on page 2.");
        }
    }
}
