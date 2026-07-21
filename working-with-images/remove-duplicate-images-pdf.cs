using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    // Helper to extract raw image bytes from an XImage
    static byte[] GetImageBytes(XImage img)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            // XImage can be saved to a stream; this yields the original image bytes
            img.Save(ms);
            return ms.ToArray();
        }
    }

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
            // Cache image bytes for each page to avoid repeated extraction
            var pageImageData = new Dictionary<int, List<(string Name, byte[] Data)>>();

            // First pass: collect image bytes per page
            for (int p = 1; p <= doc.Pages.Count; p++)
            {
                var page = doc.Pages[p];
                var images = new List<(string Name, byte[] Data)>();

                foreach (XImage img in page.Resources.Images)
                {
                    // Some images may be placeholders without data; skip nulls
                    byte[] data = GetImageBytes(img);
                    if (data != null && data.Length > 0)
                    {
                        images.Add((img.Name, data));
                    }
                }

                pageImageData[p] = images;
            }

            // Second pass: compare images across pages and remove duplicates
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                var imagesI = pageImageData[i];
                for (int j = i + 1; j <= doc.Pages.Count; j++)
                {
                    var pageJ = doc.Pages[j];
                    var imagesJ = pageImageData[j];
                    var namesToDelete = new List<string>();

                    foreach (var (nameI, dataI) in imagesI)
                    {
                        foreach (var (nameJ, dataJ) in imagesJ)
                        {
                            // Compare raw byte arrays
                            if (dataI.Length == dataJ.Length && dataI.SequenceEqual(dataJ))
                            {
                                // Mark the duplicate on the later page for removal
                                namesToDelete.Add(nameJ);
                            }
                        }
                    }

                    // Remove duplicates from the later page's image collection
                    foreach (string imgName in namesToDelete.Distinct())
                    {
                        // Use ForceDelete to ensure the image object is removed if no other references exist
                        pageJ.Resources.Images.Delete(imgName, ImageDeleteAction.ForceDelete);
                    }

                    // Update the cached list for page j after deletions
                    if (namesToDelete.Count > 0)
                    {
                        imagesJ.RemoveAll(item => namesToDelete.Contains(item.Name));
                    }
                }
            }

            // Save the modified document (lifecycle rule: use save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Duplicate images removed. Output saved to '{outputPath}'.");
    }
}