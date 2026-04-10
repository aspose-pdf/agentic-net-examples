using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_duplicates.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Dictionary to keep track of image hashes already seen
            var seenHashes = new Dictionary<string, string>();

            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                var imagesToDelete = new List<string>();

                // Iterate over images on the current page
                foreach (XImage img in page.Resources.Images)
                {
                    // Get raw image bytes. XImage does not expose ImageData directly in recent versions,
                    // so we save the image to a MemoryStream and read the bytes.
                    byte[] rawData;
                    using (var ms = new MemoryStream())
                    {
                        img.Save(ms);
                        rawData = ms.ToArray();
                    }

                    if (rawData == null || rawData.Length == 0)
                        continue; // skip if image data is unavailable

                    // Compute a SHA‑256 hash of the image bytes
                    string hash;
                    using (SHA256 sha = SHA256.Create())
                    {
                        hash = BitConverter.ToString(sha.ComputeHash(rawData));
                    }

                    // If the same hash was already encountered, mark this image for deletion
                    if (seenHashes.ContainsKey(hash))
                    {
                        imagesToDelete.Add(img.Name);
                    }
                    else
                    {
                        // Remember this image hash and its name
                        seenHashes[hash] = img.Name;
                    }
                }

                // Delete duplicate images from the page's image collection
                foreach (string name in imagesToDelete)
                {
                    // ForceDelete removes the image object from the document
                    page.Resources.Images.Delete(name, ImageDeleteAction.ForceDelete);
                }
            }

            // Optional: clean up any now‑unused resources
            doc.OptimizeResources();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Duplicate images removed. Saved to '{outputPath}'.");
    }
}
