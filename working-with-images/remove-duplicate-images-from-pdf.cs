using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "deduped.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in using for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Store hashes of images that have been seen
            var uniqueImages = new Dictionary<string, XImage>();

            // Iterate over each page in the PDF
            foreach (Page page in doc.Pages)
            {
                // Collect images into a temporary list to allow removal during iteration
                var imagesOnPage = new List<XImage>();
                foreach (XImage img in page.Resources.Images)
                {
                    imagesOnPage.Add(img);
                }

                // Process each image
                foreach (XImage img in imagesOnPage)
                {
                    // Extract raw image bytes
                    byte[] imageData;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms);               // Save image to memory stream
                        imageData = ms.ToArray();   // Get raw bytes
                    }

                    // Compute a SHA256 hash of the image data
                    string hash;
                    using (var sha = SHA256.Create())
                    {
                        hash = BitConverter.ToString(sha.ComputeHash(imageData));
                    }

                    // If the hash already exists, the image is a duplicate
                    if (uniqueImages.ContainsKey(hash))
                    {
                        // Remove duplicate image from the page's image collection
                        page.Resources.Images.Remove(img);
                    }
                    else
                    {
                        // First occurrence – keep it and record its hash
                        uniqueImages[hash] = img;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Deduped PDF saved to '{outputPath}'.");
    }
}