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
        const string outputPath = "output_no_duplicates.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Dictionary to keep track of unique image hashes and their resource names
            var uniqueImages = new Dictionary<string, string>();

            // Iterate over all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Aspose.Pdf.Page page = doc.Pages[pageNum];
                var imagesToDelete = new List<string>();

                // Iterate over images on the current page
                foreach (Aspose.Pdf.XImage img in page.Resources.Images)
                {
                    // Extract raw image bytes by saving to a memory stream
                    byte[] imageBytes;
                    using (var ms = new MemoryStream())
                    {
                        img.Save(ms);
                        imageBytes = ms.ToArray();
                    }

                    // Compute a hash of the image data (SHA‑256)
                    string hash = BitConverter.ToString(SHA256.Create().ComputeHash(imageBytes));

                    if (!uniqueImages.ContainsKey(hash))
                    {
                        // First occurrence of this image – store its name
                        uniqueImages[hash] = img.Name;
                    }
                    else
                    {
                        // Duplicate image found – schedule it for removal
                        imagesToDelete.Add(img.Name);
                    }
                }

                // Delete the duplicate images from the page's resources
                foreach (string imgName in imagesToDelete)
                {
                    // Use ImageDeleteAction.Check to delete only if no other page references it
                    page.Resources.Images.Delete(imgName, Aspose.Pdf.ImageDeleteAction.Check);
                }
            }

            // Optional: run the built‑in optimizer to clean up any remaining unused resources
            doc.OptimizeResources();

            // Save the cleaned document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Duplicate images removed. Saved to '{outputPath}'.");
    }
}