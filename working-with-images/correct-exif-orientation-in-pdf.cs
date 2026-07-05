using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;

class Program
{
    // Map EXIF orientation values to System.Drawing.RotateFlipType
    static RotateFlipType GetRotateFlipType(int orientation)
    {
        return orientation switch
        {
            2 => RotateFlipType.RotateNoneFlipX,               // Mirrored horizontal
            3 => RotateFlipType.Rotate180FlipNone,             // Rotate 180
            4 => RotateFlipType.Rotate180FlipX,                // Mirrored vertical
            5 => RotateFlipType.Rotate90FlipX,                 // Mirrored horizontal then rotate 90 CW
            6 => RotateFlipType.Rotate90FlipNone,              // Rotate 90 CW
            7 => RotateFlipType.Rotate270FlipX,                // Mirrored horizontal then rotate 270 CW
            8 => RotateFlipType.Rotate270FlipNone,             // Rotate 270 CW
            _ => RotateFlipType.RotateNoneFlipNone,            // Normal orientation
        };
    }

    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_corrected.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load and process the PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageIdx = 1; pageIdx <= doc.Pages.Count; pageIdx++)
            {
                Page page = doc.Pages[pageIdx];
                int imageCount = page.Resources.Images.Count;

                // Iterate through all images on the page (also 1‑based)
                for (int imgIdx = 1; imgIdx <= imageCount; imgIdx++)
                {
                    XImage xImg = page.Resources.Images[imgIdx];

                    // Export the current image to a memory stream (JPEG format)
                    using (MemoryStream originalStream = new MemoryStream())
                    {
                        xImg.Save(originalStream);
                        originalStream.Position = 0;

                        // Load the image with System.Drawing to read EXIF orientation
                        using (System.Drawing.Image sysImg = System.Drawing.Image.FromStream(originalStream))
                        {
                            const int orientationTag = 0x0112; // EXIF orientation tag
                            int orientation = 1; // Default (no rotation)

                            // Check if the orientation tag exists
                            if (Array.IndexOf(sysImg.PropertyIdList, orientationTag) >= 0)
                            {
                                var propItem = sysImg.GetPropertyItem(orientationTag);
                                orientation = propItem.Value[0];
                            }

                            // If orientation is not the default, rotate and replace the image
                            if (orientation != 1)
                            {
                                using (Bitmap bmp = new Bitmap(sysImg))
                                {
                                    // Apply the required rotation/flip
                                    bmp.RotateFlip(GetRotateFlipType(orientation));

                                    // Save the corrected image back to a stream (JPEG)
                                    using (MemoryStream correctedStream = new MemoryStream())
                                    {
                                        bmp.Save(correctedStream, ImageFormat.Jpeg);
                                        correctedStream.Position = 0;

                                        // Replace the image in the PDF's image collection
                                        page.Resources.Images.Replace(imgIdx, correctedStream);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Save the modified PDF (standard Save overload)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}