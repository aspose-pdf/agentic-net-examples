using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputDir = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Page indexing in Aspose.Pdf is 1‑based; check that page 20 exists
            if (doc.Pages.Count < 20)
            {
                Console.Error.WriteLine("The document does not contain a page 20.");
                return;
            }

            // Get page 20
            Page page = doc.Pages[20];

            // Extract images from the page via the core Resources.Images collection
            int imageIndex = 1;
            foreach (XImage image in page.Resources.Images)
            {
                // Build a filename like "page20_image_1.png"
                string filePath = Path.Combine(outputDir, $"page20_image_{imageIndex}.png");

                // XImage.Save expects a Stream, not a file path
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    image.Save(fs);
                }

                Console.WriteLine($"Saved image {imageIndex} to '{filePath}'.");
                imageIndex++;
            }

            if (imageIndex == 1)
            {
                Console.WriteLine("No images were found on page 20.");
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
