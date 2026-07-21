using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string highResPngPath = "highres.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(highResPngPath))
        {
            Console.Error.WriteLine($"Replacement PNG not found: {highResPngPath}");
            return;
        }

        try
        {
            // Load the existing PDF
            using (Document doc = new Document(inputPdfPath))
            {
                // Verify that page three exists (Aspose.Pdf uses 1‑based indexing)
                if (doc.Pages.Count < 3)
                {
                    Console.Error.WriteLine("The document has fewer than three pages.");
                    return;
                }

                // Get page three
                Page page = doc.Pages[3];

                // Access the image collection for that page
                XImageCollection images = page.Resources.Images;

                // Ensure there is at least one image to replace
                if (images.Count == 0)
                {
                    Console.WriteLine("No images found on page 3 to replace.");
                }
                else
                {
                    // Replace the first image (index 1) with the higher‑resolution PNG
                    using (FileStream pngStream = File.OpenRead(highResPngPath))
                    {
                        // XImageCollection.Replace expects a 1‑based index
                        images.Replace(1, pngStream);
                    }

                    Console.WriteLine("Image on page 3 replaced successfully.");
                }

                // Save the modified PDF
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Modified PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}