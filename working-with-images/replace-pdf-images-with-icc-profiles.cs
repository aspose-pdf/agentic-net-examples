using System;
using System.IO;
using Aspose.Pdf;

class ReplaceImagesWithICC
{
    static void Main()
    {
        // Input PDF and folder containing replacement images (with embedded ICC profiles)
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_icc.pdf";
        const string replacementImagesFolder = "Replacements";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        if (!Directory.Exists(replacementImagesFolder))
        {
            Console.Error.WriteLine($"Replacement images folder not found: {replacementImagesFolder}");
            return;
        }

        // Load the PDF document (using the standard load constructor)
        using (Document doc = new Document(inputPdfPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                // XImageCollection holds the image resources for the page
                XImageCollection images = page.Resources.Images;

                // If the page contains no images, skip it
                if (images.Count == 0)
                    continue;

                // Replace each image with a version that has an embedded ICC profile.
                // The replacement image file name is expected to match the original
                // resource name with a suffix "_icc" (e.g., "Image1_icc.jpg").
                for (int imgIndex = 1; imgIndex <= images.Count; imgIndex++)
                {
                    // Retrieve the original image name (e.g., "Im1")
                    string originalName = images[imgIndex].Name;

                    // Build the expected replacement file path
                    string replacementPath = Path.Combine(
                        replacementImagesFolder,
                        $"{originalName}_icc.jpg");

                    if (!File.Exists(replacementPath))
                    {
                        // If a matching replacement does not exist, keep the original image
                        Console.WriteLine($"No ICC‑profile image found for {originalName}, page {pageNum}.");
                        continue;
                    }

                    // Replace the image in the collection with the new stream.
                    // XImageCollection.Replace expects a 1‑based index.
                    using (FileStream replacementStream = File.OpenRead(replacementPath))
                    {
                        images.Replace(imgIndex, replacementStream);
                    }

                    Console.WriteLine($"Replaced image {originalName} on page {pageNum} with ICC‑profile version.");
                }
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with ICC‑profile images: {outputPdfPath}");
    }
}