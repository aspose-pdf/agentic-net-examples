using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using Aspose.Pdf;

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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // XImageCollection for the current page
                var images = page.Resources.Images;

                // XImageCollection is 1‑based; keep a manual index
                int index = 1;
                foreach (XImage xImg in images)
                {
                    // Extract the image data into a memory stream (JPEG format)
                    using (MemoryStream originalStream = new MemoryStream())
                    {
                        // Save the XImage as JPEG (preserves original bytes)
                        xImg.Save(originalStream, ImageFormat.Jpeg);
                        originalStream.Position = 0;

                        // Load the image with System.Drawing to read EXIF orientation
                        using (System.Drawing.Image sysImg = System.Drawing.Image.FromStream(originalStream))
                        {
                            const int OrientationTagId = 0x0112; // EXIF orientation tag

                            // Check if the orientation tag exists
                            if (sysImg.PropertyIdList.Contains(OrientationTagId))
                            {
                                var prop = sysImg.GetPropertyItem(OrientationTagId);
                                int orientation = BitConverter.ToUInt16(prop.Value, 0);

                                // Determine required rotation/flip based on orientation value
                                RotateFlipType rotateFlip = orientation switch
                                {
                                    1 => RotateFlipType.RotateNoneFlipNone,
                                    2 => RotateFlipType.RotateNoneFlipX,
                                    3 => RotateFlipType.Rotate180FlipNone,
                                    4 => RotateFlipType.Rotate180FlipX,
                                    5 => RotateFlipType.Rotate90FlipX,
                                    6 => RotateFlipType.Rotate90FlipNone,
                                    7 => RotateFlipType.Rotate270FlipX,
                                    8 => RotateFlipType.Rotate270FlipNone,
                                    _ => RotateFlipType.RotateNoneFlipNone
                                };

                                // Apply rotation/flip only if needed
                                if (rotateFlip != RotateFlipType.RotateNoneFlipNone)
                                {
                                    sysImg.RotateFlip(rotateFlip);

                                    // Save the corrected image back to a stream
                                    using (MemoryStream correctedStream = new MemoryStream())
                                    {
                                        sysImg.Save(correctedStream, ImageFormat.Jpeg);
                                        correctedStream.Position = 0;

                                        // Replace the image in the collection (index is 1‑based)
                                        images.Replace(index, correctedStream);
                                    }
                                }
                            }
                        }
                    }

                    index++; // advance to next image index
                }
            }

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}