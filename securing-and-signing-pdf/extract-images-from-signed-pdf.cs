using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_input.pdf";
        const string outputRoot = "ExtractedImages";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the root output folder exists
        Directory.CreateDirectory(outputRoot);

        // Load the signed PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Create a folder for the current page
                string pageFolder = Path.Combine(outputRoot, $"Page_{pageIndex}");
                Directory.CreateDirectory(pageFolder);

                int imageCounter = 1;

                // Iterate over images in the page resources
                foreach (XImage img in page.Resources.Images)
                {
                    // Use a generic PNG extension – Aspose.Pdf will write the image in its native format
                    string imagePath = Path.Combine(pageFolder, $"Image_{imageCounter}.png");

                    // Save the image using the Stream overload (XImage.Save(string) is not available)
                    using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }

                    Console.WriteLine($"Saved {imagePath}");
                    imageCounter++;
                }
            }
        }

        Console.WriteLine("Image extraction completed.");
    }
}
