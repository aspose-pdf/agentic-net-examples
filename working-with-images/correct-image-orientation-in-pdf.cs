using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_fixed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Iterate over the image collection on the page
                int imgIndex = 1; // collection is also 1‑based
                foreach (XImage xImg in page.Resources.Images)
                {
                    // Obtain the original image bytes
                    using (MemoryStream originalStream = new MemoryStream())
                    {
                        // XImage.ToStream returns the raw image stream
                        using (Stream src = xImg.ToStream())
                        {
                            src.CopyTo(originalStream);
                        }

                        // Try to load the image with System.Drawing to read EXIF orientation
                        originalStream.Position = 0;
                        try
                        {
                            using (System.Drawing.Image sysImg = System.Drawing.Image.FromStream(originalStream))
                            {
                                // Check if the image contains the Orientation EXIF tag (0x0112)
                                const int orientationTag = 0x0112;
                                if (Array.Exists(sysImg.PropertyIdList, id => id == orientationTag))
                                {
                                    int orientation = sysImg.GetPropertyItem(orientationTag).Value[0];
                                    System.Drawing.RotateFlipType rotateFlip = System.Drawing.RotateFlipType.RotateNoneFlipNone;

                                    // Determine required rotation based on EXIF orientation value
                                    switch (orientation)
                                    {
                                        case 3: // 180°
                                            rotateFlip = System.Drawing.RotateFlipType.Rotate180FlipNone;
                                            break;
                                        case 6: // 90° CW (rotate 270° CCW)
                                            rotateFlip = System.Drawing.RotateFlipType.Rotate90FlipNone;
                                            break;
                                        case 8: // 270° CW (rotate 90° CCW)
                                            rotateFlip = System.Drawing.RotateFlipType.Rotate270FlipNone;
                                            break;
                                        // Values 1,2,4,5,7 mean no rotation needed for our purpose
                                    }

                                    if (rotateFlip != System.Drawing.RotateFlipType.RotateNoneFlipNone)
                                    {
                                        // Apply rotation
                                        sysImg.RotateFlip(rotateFlip);

                                        // Save the corrected image to a new stream (JPEG format)
                                        using (MemoryStream correctedStream = new MemoryStream())
                                        {
                                            sysImg.Save(correctedStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                                            correctedStream.Position = 0;

                                            // Replace the image in the PDF's XImage collection
                                            page.Resources.Images.Replace(imgIndex, correctedStream);
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // If the image cannot be processed (e.g., not a bitmap), skip it
                            Console.Error.WriteLine($"Page {pageNum}, image {imgIndex}: {ex.Message}");
                        }
                    }

                    imgIndex++;
                }
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
