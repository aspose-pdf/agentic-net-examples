using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for ImageDeleteAction enum

class Program
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Dictionary to keep track of image hashes and the first image name that produced the hash
            var hashToImageName = new Dictionary<string, string>();

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];
                var images = page.Resources.Images;

                // Collect names of duplicate images to delete after the enumeration
                var duplicatesToDelete = new List<string>();

                // Enumerate each XImage on the current page
                foreach (XImage img in images)
                {
                    // Extract the raw image bytes by saving the XImage into a memory stream
                    byte[] rawBytes;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms);               // XImage.Save writes the image data to the stream
                        rawBytes = ms.ToArray();
                    }

                    // Compute a SHA‑256 hash of the image bytes for fast comparison
                    string hash;
                    using (SHA256 sha = SHA256.Create())
                    {
                        hash = BitConverter.ToString(sha.ComputeHash(rawBytes));
                    }

                    // If the hash already exists, the image is a duplicate
                    if (hashToImageName.TryGetValue(hash, out string existingName))
                    {
                        // Schedule this duplicate image for removal.
                        // Use ImageDeleteAction.Check to ensure the image is removed only if no other page references it.
                        duplicatesToDelete.Add(img.Name);
                    }
                    else
                    {
                        // First occurrence of this image – store its hash and name
                        hashToImageName[hash] = img.Name;
                    }
                }

                // Remove the duplicate images from the page's image collection
                foreach (string imgName in duplicatesToDelete)
                {
                    page.Resources.Images.Delete(imgName, ImageDeleteAction.Check);
                }
            }

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Duplicate images removed. Output saved to '{outputPath}'.");
    }
}