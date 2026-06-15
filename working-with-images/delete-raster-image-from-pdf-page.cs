using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const int    pageNumber = 1;   // 1‑based page index
        const int    imageIndex = 0;   // index of the image to delete (0‑based in the collection)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the target page (pages are 1‑based)
            Page page = doc.Pages[pageNumber];

            // Verify that the page contains at least one raster image
            if (page.Resources.Images.Count > imageIndex)
            {
                // Delete the image and remove its reference from the page contents.
                // ImageDeleteAction.None removes the image from the collection and from the page contents,
                // but keeps the image object in the document (file size unchanged).
                page.Resources.Images.Delete(imageIndex, ImageDeleteAction.None);
            }
            else
            {
                Console.WriteLine("No image found at the specified index.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image deleted and PDF saved to '{outputPath}'.");
    }
}