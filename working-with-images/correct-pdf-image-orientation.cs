using System;
using System.IO;
using System.Drawing; // Keep for RotateFlipType enum (fully qualified usage)
using Aspose.Pdf;

class Program
{
    // Maps EXIF orientation values to System.Drawing.RotateFlipType
    static System.Drawing.RotateFlipType GetRotateFlip(int orientation)
    {
        return orientation switch
        {
            1 => System.Drawing.RotateFlipType.RotateNoneFlipNone,
            2 => System.Drawing.RotateFlipType.RotateNoneFlipX,
            3 => System.Drawing.RotateFlipType.Rotate180FlipNone,
            4 => System.Drawing.RotateFlipType.Rotate180FlipX,
            5 => System.Drawing.RotateFlipType.Rotate90FlipX,
            6 => System.Drawing.RotateFlipType.Rotate90FlipNone,
            7 => System.Drawing.RotateFlipType.Rotate270FlipX,
            8 => System.Drawing.RotateFlipType.Rotate270FlipNone,
            _ => System.Drawing.RotateFlipType.RotateNoneFlipNone,
        };
    }

    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_corrected.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int p = 1; p <= doc.Pages.Count; p++)
            {
                Page page = doc.Pages[p];
                var images = page.Resources.Images;

                // Iterate over images using 1‑based index (XImageCollection follows the same rule)
                for (int i = 1; i <= images.Count; i++)
                {
                    XImage xImg = images[i];

                    // Extract the original image bytes
                    using (MemoryStream originalStream = new MemoryStream())
                    {
                        xImg.Save(originalStream);
                        originalStream.Position = 0;

                        // Load into System.Drawing.Image to read EXIF orientation
                        using (System.Drawing.Image sysImg = System.Drawing.Image.FromStream(originalStream))
                        {
                            const int orientationTag = 0x0112; // EXIF orientation tag
                            if (Array.Exists(sysImg.PropertyIdList, id => id == orientationTag))
                            {
                                var prop = sysImg.GetPropertyItem(orientationTag);
                                int orientation = BitConverter.ToUInt16(prop.Value, 0);
                                System.Drawing.RotateFlipType rotateFlip = GetRotateFlip(orientation);

                                // Apply rotation only when needed
                                if (rotateFlip != System.Drawing.RotateFlipType.RotateNoneFlipNone)
                                {
                                    sysImg.RotateFlip(rotateFlip);

                                    // Save the corrected image back to a stream (preserve original format)
                                    using (MemoryStream correctedStream = new MemoryStream())
                                    {
                                        sysImg.Save(correctedStream, sysImg.RawFormat);
                                        correctedStream.Position = 0;

                                        // Replace the image in the PDF's image collection
                                        images.Replace(i, correctedStream);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: Save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
