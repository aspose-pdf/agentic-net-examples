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
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            if (doc.Pages.Count < 8)
            {
                Console.Error.WriteLine("The document has fewer than 8 pages.");
                return;
            }

            // Get page 8
            Page page = doc.Pages[8];

            int imageIndex = 1;
            // Iterate the XImage collection (it is not a dictionary)
            foreach (XImage img in page.Resources.Images)
            {
                string outputPath = Path.Combine(outputFolder, $"Page8_Image{imageIndex}.jpg");

                // XImage.Save expects a Stream, not a file path.
                using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    img.Save(fs, ImageFormat.Jpeg);
                }

                Console.WriteLine($"Saved image {imageIndex} to '{outputPath}'.");
                imageIndex++;
            }

            if (imageIndex == 1)
                Console.WriteLine("No images found on page 8.");
        }
    }
}
