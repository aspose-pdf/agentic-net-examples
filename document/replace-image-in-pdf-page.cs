using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string newImagePath   = "newImage.jpg";
        const string outputPdfPath  = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(newImagePath))
        {
            Console.Error.WriteLine($"Replacement image not found: {newImagePath}");
            return;
        }

        // Load the PDF document (using rule for loading)
        using (Document doc = new Document(inputPdfPath))
        {
            // Select the page that contains the image to replace (1‑based indexing)
            Page page = doc.Pages[1];

            // Verify that the page actually has image resources
            if (page.Resources.Images.Count == 0)
            {
                Console.WriteLine("No images found on the selected page.");
                doc.Save(outputPdfPath); // Save unchanged document
                return;
            }

            // Replace the first image resource while preserving its placement.
            // XImageCollection uses 1‑based indexing, so index = 1 replaces the first image.
            int imageIndex = 1;
            using (FileStream newImgStream = File.OpenRead(newImagePath))
            {
                page.Resources.Images.Replace(imageIndex, newImgStream);
            }

            // Save the modified PDF (using rule for saving)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image successfully replaced. Output saved to '{outputPdfPath}'.");
    }
}