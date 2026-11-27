using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string manifestPath = "image_manifest.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Verify the document has at least four pages (page indexing is 1‑based)
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("Document contains fewer than 4 pages.");
                return;
            }

            // Access page 4 (1‑based index)
            Page page = doc.Pages[4];

            // Create a manifest file that lists each image on page 4
            using (StreamWriter writer = new StreamWriter(manifestPath, false))
            {
                int imageIndex = 0;

                // Iterate over the image collection; XImageCollection is not a dictionary
                foreach (XImage img in page.Resources.Images)
                {
                    imageIndex++;
                    // Write page number and image index (both 1‑based) to the manifest
                    writer.WriteLine($"Page: 4, ImageIndex: {imageIndex}");
                }

                // If no images were found, note it in the manifest
                if (imageIndex == 0)
                {
                    writer.WriteLine("Page: 4, No images found.");
                }
            }

            Console.WriteLine($"Image manifest written to '{manifestPath}'.");
        }
    }
}