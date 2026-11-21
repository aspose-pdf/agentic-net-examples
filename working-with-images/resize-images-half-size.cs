using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF with deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Iterate over each page
                foreach (Page page in doc.Pages)
                {
                    // XImageCollection uses 1‑based indexing
                    var images = page.Resources.Images;
                    for (int idx = 1; idx <= images.Count; idx++)
                    {
                        XImage xImg = images[idx];

                        // Export the original image to a memory stream
                        using (MemoryStream originalStream = new MemoryStream())
                        {
                            // XImage can be saved to a stream via the Save method
                            xImg.Save(originalStream);
                            originalStream.Position = 0;

                            // Load into System.Drawing for resizing (fully qualified to avoid ambiguity)
                            using (System.Drawing.Image sysImg = System.Drawing.Image.FromStream(originalStream))
                            {
                                int newWidth = sysImg.Width / 2;
                                int newHeight = sysImg.Height / 2;

                                // Create a resized bitmap
                                using (System.Drawing.Bitmap resizedBmp = new System.Drawing.Bitmap(sysImg, newWidth, newHeight))
                                {
                                    using (MemoryStream resizedStream = new MemoryStream())
                                    {
                                        // Preserve original format when saving
                                        resizedBmp.Save(resizedStream, sysImg.RawFormat);
                                        resizedStream.Position = 0;

                                        // Replace the image in the PDF resources
                                        images.Replace(idx, resizedStream);
                                    }
                                }
                            }
                        }
                    }
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
