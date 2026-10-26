using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_duplicate_images.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal (global rule)
        using (Document doc = new Document(inputPath))
        {
            // Keep track of image hashes we have already seen
            var seenHashes = new Dictionary<string, XImage>();

            // Aspose.Pdf uses 1‑based page indexing (global rule)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Collect images that are duplicates so we can remove them after iteration
                var duplicates = new List<XImage>();

                // Iterate over images using the correct foreach pattern (rule)
                foreach (XImage img in page.Resources.Images)
                {
                    // Extract raw image bytes
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms);               // Save image data to a memory stream
                        byte[] imageBytes = ms.ToArray();

                        // Compute a SHA‑256 hash of the bytes for comparison
                        string hash;
                        using (SHA256 sha = SHA256.Create())
                        {
                            hash = BitConverter.ToString(sha.ComputeHash(imageBytes));
                        }

                        // If we have seen this hash before, mark the image for removal
                        if (seenHashes.ContainsKey(hash))
                        {
                            duplicates.Add(img);
                        }
                        else
                        {
                            // First occurrence – store it
                            seenHashes[hash] = img;
                        }
                    }
                }

                // Remove duplicate images from the page's image collection
                foreach (XImage dup in duplicates)
                {
                    page.Resources.Images.Remove(dup);
                }
            }

            // Save the modified PDF (no special SaveOptions needed for PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Duplicate images removed. Output saved to '{outputPath}'.");
    }
}