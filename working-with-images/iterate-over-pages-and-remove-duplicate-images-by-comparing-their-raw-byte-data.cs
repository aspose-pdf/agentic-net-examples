using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Facades;            // For any facade usage if needed

class RemoveDuplicateImages
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_duplicates.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using the recommended lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Dictionary to keep track of images we have already seen.
            // Key: SHA‑256 hash of the image bytes, Value: the first XImage instance.
            var seenImages = new Dictionary<string, XImage>();

            // Iterate over all pages (1‑based indexing as per the rule)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Collect images that need to be removed after the enumeration
                var imagesToRemove = new List<XImage>();

                // XImageCollection is enumerable; iterate directly.
                foreach (XImage img in page.Resources.Images)
                {
                    // Extract raw image bytes into a memory stream.
                    using (var ms = new MemoryStream())
                    {
                        // XImage.Save(Stream) writes the image data to the stream.
                        img.Save(ms);
                        byte[] imageBytes = ms.ToArray();

                        // Compute a SHA‑256 hash to identify identical images.
                        string hash;
                        using (SHA256 sha = SHA256.Create())
                        {
                            hash = Convert.ToBase64String(sha.ComputeHash(imageBytes));
                        }

                        // If we have already encountered an image with the same hash,
                        // mark this one for removal. Otherwise, remember it.
                        if (seenImages.ContainsKey(hash))
                        {
                            imagesToRemove.Add(img);
                        }
                        else
                        {
                            seenImages[hash] = img;
                        }
                    }
                }

                // Remove duplicate images from the page's resources.
                // XImageCollection supports removal by name.
                foreach (XImage dupImg in imagesToRemove)
                {
                    // XImage.Name gives the resource identifier.
                    // Deleting the image from the collection also removes its references
                    // from the page content (Aspose.Pdf handles the cleanup).
                    page.Resources.Images.Delete(dupImg.Name);
                }
            }

            // After manual removal, run the built‑in optimizer to clean any leftover unused resources.
            doc.OptimizeResources();

            // Save the modified document (using the prescribed save rule).
            doc.Save(outputPath);
        }

        Console.WriteLine($"Duplicate images removed. Output saved to '{outputPath}'.");
    }
}