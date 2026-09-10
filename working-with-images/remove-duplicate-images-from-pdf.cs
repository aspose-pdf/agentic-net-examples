using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Dictionary to keep track of image hashes already seen
            var seenHashes = new Dictionary<string, XImage>();

            // Iterate over all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                // XImageCollection is not a dictionary; iterate directly
                foreach (XImage img in page.Resources.Images)
                {
                    // Export the image to a memory stream to obtain raw bytes
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // XImage.Save writes the image to a stream in its original format
                        img.Save(ms);
                        byte[] imageBytes = ms.ToArray();

                        // Compute a SHA256 hash of the raw bytes for fast comparison
                        string hash;
                        using (SHA256 sha = SHA256.Create())
                        {
                            hash = BitConverter.ToString(sha.ComputeHash(imageBytes));
                        }

                        // If we have already encountered an identical image, remove this one
                        if (seenHashes.ContainsKey(hash))
                        {
                            // Remove the image from the page's resources.
                            // Use the image name (unique within the page) and force deletion.
                            // The ImageDeleteAction.ForceDelete ensures the object is removed from the document.
                            page.Resources.Images.Delete(img.Name, ImageDeleteAction.ForceDelete);
                        }
                        else
                        {
                            // First occurrence – keep it and store the hash
                            seenHashes[hash] = img;
                        }
                    }
                }
            }

            // Optional: further compress resources (merges any remaining identical resources)
            doc.OptimizeResources();

            // Save the modified PDF (lifecycle rule: use save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Duplicate images removed. Output saved to '{outputPath}'.");
    }
}