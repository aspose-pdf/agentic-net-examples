using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf;

// Alias to disambiguate System.Drawing.Image from Aspose.Pdf.Image
using SysImage = System.Drawing.Image;

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

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // XImageCollection is 1‑based as well
                for (int imgIndex = 1; imgIndex <= page.Resources.Images.Count; imgIndex++)
                {
                    XImage xImg = page.Resources.Images[imgIndex];

                    // Extract the original image bytes
                    using (MemoryStream originalStream = new MemoryStream())
                    {
                        xImg.Save(originalStream);
                        originalStream.Position = 0;

                        // Load into System.Drawing.Image to read EXIF orientation
                        using (SysImage sysImg = SysImage.FromStream(originalStream))
                        {
                            const int orientationTagId = 274; // EXIF orientation tag
                            if (Array.IndexOf(sysImg.PropertyIdList, orientationTagId) < 0)
                                continue; // No orientation metadata

                            int orientation = sysImg.GetPropertyItem(orientationTagId).Value[0];
                            RotateFlipType rotateFlip = RotateFlipType.RotateNoneFlipNone;

                            // Map EXIF orientation to RotateFlipType
                            switch (orientation)
                            {
                                case 1:  rotateFlip = RotateFlipType.RotateNoneFlipNone; break;
                                case 2:  rotateFlip = RotateFlipType.RotateNoneFlipX;    break;
                                case 3:  rotateFlip = RotateFlipType.Rotate180FlipNone; break;
                                case 4:  rotateFlip = RotateFlipType.Rotate180FlipX;    break;
                                case 5:  rotateFlip = RotateFlipType.Rotate90FlipX;    break;
                                case 6:  rotateFlip = RotateFlipType.Rotate90FlipNone; break;
                                case 7:  rotateFlip = RotateFlipType.Rotate270FlipX;   break;
                                case 8:  rotateFlip = RotateFlipType.Rotate270FlipNone;break;
                                default: rotateFlip = RotateFlipType.RotateNoneFlipNone; break;
                            }

                            if (rotateFlip == RotateFlipType.RotateNoneFlipNone)
                                continue; // Image already correctly oriented

                            // Apply rotation/flip
                            sysImg.RotateFlip(rotateFlip);

                            // Save the corrected image as JPEG (required by XImageCollection.Replace)
                            using (MemoryStream correctedStream = new MemoryStream())
                            {
                                sysImg.Save(correctedStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                                correctedStream.Position = 0;

                                // Replace the image in the PDF's image collection
                                page.Resources.Images.Replace(imgIndex, correctedStream);
                            }
                        }
                    }
                }
            }

            // Save the modified PDF (lifecycle rule: use Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
