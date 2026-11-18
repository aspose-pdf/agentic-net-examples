using System;
using System.IO;
using Aspose.Pdf;
using System.Drawing; // kept for System.Drawing.Image and ImageFormat

class ReplaceJpegWithPng
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_replaced.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPath))
            {
                // Iterate through all pages (1‑based index)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];

                    int imageCount = page.Resources.Images.Count;
                    // XImageCollection indexer is read‑only; use Replace method to swap images.
                    for (int imgIdx = 1; imgIdx <= imageCount; imgIdx++)
                    {
                        XImage xImg = page.Resources.Images[imgIdx];

                        // Save the image to a memory stream to inspect its header
                        using (MemoryStream originalStream = new MemoryStream())
                        {
                            xImg.Save(originalStream);
                            originalStream.Position = 0;

                            bool isJpeg = false;
                            if (originalStream.Length >= 2)
                            {
                                byte[] header = new byte[2];
                                originalStream.Read(header, 0, 2);
                                // JPEG files start with 0xFF 0xD8
                                isJpeg = header[0] == 0xFF && header[1] == 0xD8;
                            }

                            if (isJpeg)
                            {
                                // Reset stream position for System.Drawing.Image loading
                                originalStream.Position = 0;
                                try
                                {
                                    // Fully qualify System.Drawing.Image to avoid ambiguity with Aspose.Pdf.Image
                                    using (System.Drawing.Image sysImg = System.Drawing.Image.FromStream(originalStream))
                                    {
                                        using (MemoryStream pngStream = new MemoryStream())
                                        {
                                            sysImg.Save(pngStream, System.Drawing.Imaging.ImageFormat.Png);
                                            pngStream.Position = 0;

                                            // Replace the original XImage with the newly created PNG XImage
                                            // XImageCollection.Replace replaces the image at the given 1‑based index.
                                            page.Resources.Images.Replace(imgIdx, pngStream);
                                        }
                                    }
                                }
                                catch (TypeInitializationException)
                                {
                                    // GDI+ not available (e.g., non‑Windows). Skip conversion.
                                    Console.WriteLine("Image conversion requires Windows GDI+. Skipping JPEG on this platform.");
                                }
                            }
                        }
                    }
                }

                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with PNG replacements to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
