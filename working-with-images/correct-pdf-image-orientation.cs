using System;
using System.IO;
using Aspose.Pdf;
using System.Drawing;               // Used only for EXIF handling (Windows‑only)
using System.Drawing.Imaging;      // For reading EXIF orientation

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_corrected.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing rule)
            for (int p = 1; p <= doc.Pages.Count; p++)
            {
                Page page = doc.Pages[p];

                // XImageCollection is enumerable; iterate directly
                foreach (XImage img in page.Resources.Images)
                {
                    // Export the image to a memory stream
                    using (MemoryStream originalStream = new MemoryStream())
                    {
                        img.Save(originalStream);
                        originalStream.Position = 0;

                        // Load the image with System.Drawing to inspect EXIF orientation
                        using (System.Drawing.Image sysImg = System.Drawing.Image.FromStream(originalStream))
                        {
                            const int OrientationId = 0x0112; // EXIF tag for orientation
                            if (Array.Exists(sysImg.PropertyIdList, id => id == OrientationId))
                            {
                                PropertyItem prop = sysImg.GetPropertyItem(OrientationId);
                                int orientation = BitConverter.ToUInt16(prop.Value, 0);

                                // If orientation is not the default (1), rotate/flip accordingly
                                if (orientation != 1)
                                {
                                    RotateFlipType rotateFlip = GetRotateFlipType(orientation);
                                    sysImg.RotateFlip(rotateFlip);

                                    // Save the corrected image back to a new memory stream (PNG to preserve lossless rotation)
                                    using (MemoryStream correctedStream = new MemoryStream())
                                    {
                                        sysImg.Save(correctedStream, ImageFormat.Png);
                                        correctedStream.Position = 0;

                                        // Replace the original XImage with the corrected one
                                        // Remove the old image by instance (Remove expects XImage, not name)
                                        page.Resources.Images.Remove(img);
                                        string addedName = page.Resources.Images.Add(correctedStream);
                                        XImage newImg = page.Resources.Images[addedName];
                                        // Preserve the original image name so page content stays valid
                                        newImg.Name = img.Name;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Save the updated PDF (save-to-non-pdf rule not needed – still PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }

    // Maps EXIF orientation values to .NET RotateFlipType
    private static RotateFlipType GetRotateFlipType(int orientation)
    {
        return orientation switch
        {
            2 => RotateFlipType.RotateNoneFlipX,
            3 => RotateFlipType.Rotate180FlipNone,
            4 => RotateFlipType.Rotate180FlipX,
            5 => RotateFlipType.Rotate90FlipX,
            6 => RotateFlipType.Rotate90FlipNone,
            7 => RotateFlipType.Rotate270FlipX,
            8 => RotateFlipType.Rotate270FlipNone,
            _ => RotateFlipType.RotateNoneFlipNone,
        };
    }
}
