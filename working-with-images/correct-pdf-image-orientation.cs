using System;
using System.IO;
using Aspose.Pdf;
using System.Drawing.Imaging; // needed for ImageFormat, PropertyItem, RotateFlipType

// Alias System.Drawing types to avoid ambiguity with Aspose.Pdf.Image
using SysImage = System.Drawing.Image;
using SysImageFormat = System.Drawing.Imaging.ImageFormat;
using SysRotateFlipType = System.Drawing.RotateFlipType;
using SysPropertyItem = System.Drawing.Imaging.PropertyItem;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                var images = page.Resources.Images; // XImageCollection
                int imageCount = images.Count;      // 1‑based count

                // Process each image on the page
                for (int i = 1; i <= imageCount; i++)
                {
                    XImage xImg = images[i];

                    // Extract the original image bytes into a memory stream
                    using (MemoryStream originalStream = new MemoryStream())
                    {
                        xImg.Save(originalStream); // Save raw image data
                        originalStream.Position = 0;

                        // Load the image with System.Drawing to read EXIF orientation
                        using (SysImage sysImg = SysImage.FromStream(originalStream))
                        {
                            const int orientationTag = 274; // EXIF orientation tag ID
                            int orientationIndex = Array.IndexOf(sysImg.PropertyIdList, orientationTag);

                            // If the image has an orientation tag, handle it
                            if (orientationIndex >= 0)
                            {
                                SysPropertyItem prop = sysImg.GetPropertyItem(orientationTag);
                                ushort orientation = BitConverter.ToUInt16(prop.Value, 0);

                                // Determine required rotation/flip based on EXIF value
                                SysRotateFlipType rotateFlip = SysRotateFlipType.RotateNoneFlipNone;
                                switch (orientation)
                                {
                                    case 1: continue; // Normal – no action needed
                                    case 2: rotateFlip = SysRotateFlipType.RotateNoneFlipX; break;
                                    case 3: rotateFlip = SysRotateFlipType.Rotate180FlipNone; break;
                                    case 4: rotateFlip = SysRotateFlipType.Rotate180FlipX; break;
                                    case 5: rotateFlip = SysRotateFlipType.Rotate90FlipX; break;
                                    case 6: rotateFlip = SysRotateFlipType.Rotate90FlipNone; break;
                                    case 7: rotateFlip = SysRotateFlipType.Rotate270FlipX; break;
                                    case 8: rotateFlip = SysRotateFlipType.Rotate270FlipNone; break;
                                    default: continue; // Unknown value – skip
                                }

                                // Apply rotation/flip
                                sysImg.RotateFlip(rotateFlip);

                                // Save the corrected image back to a JPEG stream
                                using (MemoryStream correctedStream = new MemoryStream())
                                {
                                    sysImg.Save(correctedStream, SysImageFormat.Jpeg);
                                    correctedStream.Position = 0;

                                    // Replace the image in the PDF (XImageCollection.Replace)
                                    images.Replace(i, correctedStream);
                                }
                            }
                        }
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: use Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
