using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string newImagePath = "newImage.jpg";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(newImagePath))
        {
            Console.Error.WriteLine($"Replacement image not found: {newImagePath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Work with the first page – replace the first image found on this page.
            Page page = doc.Pages[1];
            XImageCollection images = page.Resources.Images;

            if (images.Count == 0)
            {
                Console.WriteLine("No images found on the first page.");
            }
            else
            {
                // XImageCollection uses 1‑based indexing.
                using (FileStream newImgStream = File.OpenRead(newImagePath))
                {
                    images.Replace(1, newImgStream);
                }
                Console.WriteLine("Image replaced successfully while preserving layout.");
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved as '{outputPath}'.");
    }
}
