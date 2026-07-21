using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string iccImagePath = "image_with_icc.jpg"; // JPEG that already contains an ICC profile

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(iccImagePath))
        {
            Console.Error.WriteLine($"ICC‑profile image not found: {iccImagePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                var images = page.Resources.Images; // XImageCollection

                // Replace each image on the page with the ICC‑profile version
                for (int imgIndex = 1; imgIndex <= images.Count; imgIndex++)
                {
                    // Open the replacement image stream
                    using (FileStream imgStream = File.OpenRead(iccImagePath))
                    {
                        // XImageCollection.Replace replaces the image at the given 1‑based index
                        images.Replace(imgIndex, imgStream);
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: save inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with ICC‑profile images to '{outputPath}'.");
    }
}